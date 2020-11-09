<Query Kind="Program" />

void Main()
{
	System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
    sw.Start();

	IsDirectoryEmpty(@"c:\temp\x");
	
	sw.Stop();
	Console.WriteLine(sw.Elapsed);
}

public bool IsDirectoryEmpty(string path)
{
    return !Directory.EnumerateFileSystemEntries(path).Any();
}