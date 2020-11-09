<Query Kind="Program" />

void Main()
{
	string ArchiveFolder = @"c:\temp\x";
	DateTime now = DateTime.Now;
	
	Console.WriteLine("FILES:");
	Console.WriteLine();
	
	//todo: delete files older than X days
	foreach(var file in Directory.GetFiles(ArchiveFolder,"*.*", SearchOption.AllDirectories))
	{
		var lastWriteTime = File.GetLastWriteTime(file);
		//if (Math.Abs((now - lastWriteTime).TotalDays) >= 7)
		if (Math.Abs((now - lastWriteTime).TotalSeconds) >= 90)
		{
			File.Delete(file);
			Console.WriteLine("{0}\t{1}\tDELETED", file, lastWriteTime);
		}
		else
		{
			Console.WriteLine("{0}\t{1}\tNot Deleted", file, lastWriteTime);
		}
	}
	
	Console.WriteLine();
	Console.WriteLine("DIRS:");
	Console.WriteLine();

	//todo: remove all empty directories
	foreach(var dir in Directory.GetDirectories(ArchiveFolder))
	{
		if(IsDirectoryEmpty(dir))
		{
			Directory.Delete(dir);
			Console.WriteLine("{0}\tDELETED", dir);
		}
		else
		{
			Console.WriteLine("{0}\tNot Deleted", dir);
		}
	}
}

public bool IsDirectoryEmpty(string path)
{
    return !Directory.EnumerateFileSystemEntries(path).Any();
}
