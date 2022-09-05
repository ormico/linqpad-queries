<Query Kind="Program">
  <NuGetReference>System.CodeDom</NuGetReference>
  <Namespace>System.CodeDom.Compiler</Namespace>
</Query>

/*
This puzzle was given to me in Microsoft Interview for Senior Software Engineer for Azure CXP.
(I didn't get this job)

--== this file is contains my updated attempt to solve this better, after the interview ==--
--== see file intersection-of-2-large-streams-original.linq for code I wrote during the interview and all my notes a/b that day==--
--==  ==--

The interviewer originally gave the puzzle as 2 streams but changed it to 2 arrays.

Puzzle:
Given 2 Streams (Stream ns1, Stream ns2), each stream contains 1 Billion int64 values.

Return a Stream that is the Intersection of the 2 streams.

The Interviewer defined intersection as each int64 value that is in both streams and also stated 
that I could assume that each value was unique within each stream.
-- 

In order to provide a more full solution, this version first creates 2 large files full of int64 values.
These files can then be opened to provide the 2 input streams.
The interviewer stated that I could assume that values were unique in each stream. For the time being,
that is not true in my test streams b/c I'm generating random values but I'm not preventing duplicates.
That would require replacing the duplicates with new random values that were also not duplicates
in order to maintain the set number of values in each stream.

*/
void Main()
{
	const int recordCount = 1000000000; // 1 Billion
	const string ns1filename = "c:/temp/ns1.stream";
	const string ns2filename = "c:/temp/ns2.stream";
	//const int recordsPerMergeFile = 100000000 / 8; // 100 MB / (size of int64)
	const int recordsPerMergeFile = 1000000000 / 8; // 1000 MB / (size of int64)

	// -- PUZZLE SETUP --
	// generate 2 files with 1 Billion int64 values each
	using TempFileCollection tfc = new TempFileCollection();
		
	var streams = CreateTempStreams(ns1filename, ns2filename);
	using Stream ns1 = streams.stream1;
	using Stream ns2 = streams.stream2;
	tfc.AddFile(ns1filename, false);
	tfc.AddFile(ns2filename, false);

	// -- SOLVE PUZZLE --
	//input streams are ns1 and ns2

	// do a merge sort to disk of both streams and open new sorted streams
	//todo: divide each large stream up into seperate files to sort and merge
	//split files using recordsPerMergeFile, sort before writing to disk

	// ns1
	BinaryReader br = new BinaryReader(ns1);
	List<FileStream> ns1Segments = new List<FileStream>();
	List<FileStream> ns2Segments = new List<FileStream>();
	FileStream currentFile1 = null;
	BinaryWriter currentWriter1 = null;
	FileStream currentFile2 = null;
	BinaryWriter currentWriter2 = null;

	//BinaryReader doesn't have an EOF indicator, but we know there are
	//exactly 1 Billion int64s

	Int64[] buffer1 = new Int64[recordsPerMergeFile];
	Int64[] buffer2 = new Int64[recordsPerMergeFile];
	int pos = -1;

	try
	{
		
	for(int i = 0; i < recordCount; i++)
	{
		pos = i % recordsPerMergeFile;
		if(pos == 0)
		{
			// add previous file stream to list of files to merge (segments)
			if(currentFile1 != null)
			{
				// if pos == 0 and currentFile is not null then array
				// should be full and ready to sort and write to disk
				// Sort is O(n log n)
				// Array.Sort() sorts from least to greatest.
				Array.Sort(buffer1);
				Array.Sort(buffer2);
				
				foreach(var j in buffer1)
				{
					currentWriter1.Write(j);
				}
				foreach (var j in buffer2)
				{
					currentWriter2.Write(j);
				}

				currentFile1.Position = 0;
				ns1Segments.Add(currentFile1);

				currentFile2.Position = 0;
				ns2Segments.Add(currentFile2);
			}

			string ns1SegmentFileName = $"c:/temp/ns1-{i / recordsPerMergeFile}";
			//currentFile1 = File.OpenWrite(ns1SegmentFileName);
			//currentFile2 = File.Open(ns1SegmentFileName, FileMode.CreateNew);
			currentFile1 = CreateFile(ns1SegmentFileName);
			currentWriter1 = new BinaryWriter(currentFile1);
			tfc.AddFile(ns1SegmentFileName, false);

			string ns2SegmentFileName = $"c:/temp/ns2-{i / recordsPerMergeFile}";
			//currentFile2 = File.Open(ns2SegmentFileName, FileMode.CreateNew);
			currentFile2 = CreateFile(ns2SegmentFileName);
			currentWriter2 = new BinaryWriter(currentFile2);
			tfc.AddFile(ns2SegmentFileName, false);
		}

		buffer1[pos] = br.ReadInt64();
		//buffer2[pos] = br.ReadInt64();
	}

	Console.WriteLine($"pos: {pos}");
	// write out last buffer and add final files to segments list
	Array.Sort(buffer1);
	Array.Sort(buffer2);

	foreach (var j in buffer1)
	{
		currentWriter1.Write(j);
	}
	foreach (var j in buffer2)
	{
		currentWriter2.Write(j);
	}

	currentFile1.Position = 0;
	ns1Segments.Add(currentFile1);

	currentFile2.Position = 0;
	ns2Segments.Add(currentFile2);

	//todo: merge files back together, sorted.
	// open readers for all segments
	BinaryReader[] ns1SegmentReaders = (from s in ns1Segments select new BinaryReader(s)).ToArray();
	BinaryReader[] ns2SegmentReaders = (from s in ns2Segments select new BinaryReader(s)).ToArray();

	// merge segments
	// while any segment still has data
	Int64 lastValWritten;
	Int64?[] currentValues = new Int64?[ns1SegmentReaders.Length];
	// I think currentValues inits to null, but make sure
	for(int i = 0; i < currentValues.Length; i++)
	{
		currentValues[i] = null;
	}

	Int64 minValue = Int64.MaxValue;;
	int minIndex = -1;
	const string ns1SortedFileName = @"c:/temp/ns1-sorted";
	FileStream ns1FileStream = File.OpenWrite(ns1SortedFileName);
	BinaryWriter ns1Writer = new BinaryWriter(ns1FileStream);
	tfc.AddFile(ns1SortedFileName, false);
	
	do
	{
		// foreach segment check to see if we need to read a new value or mark the segment as done
		for(int i = 0; i < currentValues.Length; i++)
		{
			var reader = ns1SegmentReaders[i];
			if(reader != null)
			{
				// if cannot read from segment anymore then mark it null
				// and move to next
				if (CanRead(reader) == false)
				{
					ns1SegmentReaders[i] = null;
					continue;
				}

				// if can read, check to see if we need a new value
				if (currentValues[i] == null)
				{
					currentValues[i] = reader.ReadInt64();
				}

				// check if current value is > max
				// if there is no maxValue set then set current value as max
				if(minIndex == -1 || currentValues[i].Value < minValue)
				{
					minIndex = i;
					minValue = currentValues[i].Value;
				}
			}
		}

		// write maxValue to outfile and set to null in currentValues
		ns1Writer.Write(minValue);
		currentValues[minIndex] = null;
		minIndex = -1;
		
		Console.WriteLine(minValue);
	} while(CanRead(ns1SegmentReaders));

		//todo: open output stream (file)
		//todo: find intersection
		//  - loop until at least 1 stream has reach end
		//  - read a value from each stream
		//  - keep track of current value in each stream
		//  - if current values are equal, then write value to output stream
		//  - if values are not equal then discard lowest value and read new value from stream lowest value came from
		//  - repeat loop
		//todo: reset output stream to position zero

		//* This means we only have to loop once to cover both steams, except for the sorting at the start *//

		// -- PUZZLE CLEANUP --
		//todo: delete temp files
	}
	finally
	{
		foreach(var w in ns1Segments)
		{
			w?.Dispose();
		}
		
		foreach (var w in ns2Segments)
		{
			w?.Dispose();
		}
	}
}

(FileStream stream1, FileStream stream2) CreateTempStreams(string stream1Filename, string stream2Filename)
{
	Random rand = new Random();

	//FileStream fs1 = File.Open(stream1Filename, FileMode.CreateNew);
	FileStream fs1 = CreateFile(stream1Filename);
	BinaryWriter bw1 = new BinaryWriter(fs1);
	//FileStream fs2 = File.Open(stream2Filename, FileMode.CreateNew);
	FileStream fs2 = CreateFile(stream2Filename);
	BinaryWriter bw2 = new BinaryWriter(fs2);
	byte[] int64Buffer = new byte[8];

	for (int i = 0; i < 1000000000; i++)
	{
		//stream1
		rand.NextBytes(int64Buffer);
		long rv = BitConverter.ToInt64(int64Buffer, 0);
		bw1.Write(rv);

		//stream2
		rand.NextBytes(int64Buffer);
		rv = BitConverter.ToInt64(int64Buffer, 0);
		bw2.Write(rv);
	}

	/*
	fs1.Position = 0;
	BinaryReader br1 = new BinaryReader(fs1);

	fs2.Position = 0;
	BinaryReader br2 = new BinaryReader(fs2);

	int64Buffer = br1.ReadBytes(8);
	Console.WriteLine(BitConverter.ToInt64(int64Buffer, 0));

	int64Buffer = br2.ReadBytes(8);
	Console.WriteLine(BitConverter.ToInt64(int64Buffer, 0));
	*/
	fs1.Position = 0;
	fs2.Position = 0;
	
	return (fs1, fs2);
}

/// <summary>return true if any stream in BinaryReader array is not at EOF</summary>
bool CanRead(BinaryReader[] readers)
{
	bool rc = false;
	foreach(var r in readers)
	{
		rc = CanRead(r);
		if(rc)
		{
			break;
		}
	}
	
	return rc;
}

bool CanRead(BinaryReader reader)
{
	bool rc = false;
	if (reader != null)
	{
		Stream s = reader.BaseStream;
		// this just check whether a Stream has any data left at all
		// not whether there is enough left for an int64
		if (s.Length != s.Position)
		{
			rc = true;
		}
	}
	return rc;
}

FileStream CreateFile(string fileName)
{
	if(File.Exists(fileName))
	{
		File.Delete(fileName);
	}
	
	FileStream rc = File.Open(fileName, FileMode.CreateNew);
	return rc;
}