<Query Kind="Statements" />

string folder = @"C:\input-folder\";
string[] searchPatterns =
{
	"*.js",
	"*.sln",
	"*.vcproj",
	"*.vbproj",
	"*.ps1",
	"*.xml",
	"*.txt",
	"*.cs",
	"*.vb",
	"*.config",
	"*.css",
	"*.htm",
	"*.html",
	"*.aspx",
	"*.asmx",
	"*.asax",
	"*.sql"
};

foreach(var pattern in searchPatterns)
{
	foreach(var p in Directory.GetFiles(folder, pattern, SearchOption.AllDirectories))
	{
		var str = File.ReadAllText(p);
		// fix any newline errors
		str = str.Replace("\r\n", "\r").Replace("\n", "\r").Replace("\r", "\r\n");
		File.WriteAllText(p, str, Encoding.UTF8);
	}
}
/*
foreach(var pattern in searchPatterns)
{
	foreach(var p in Directory.GetFiles(folder, pattern))
	{
		Console.WriteLine(p);
		using(var f = File.OpenRead(p))
		{
			for(int i = 0; i < f.Length; i++)
			{
				Console.WriteLine(f.ReadByte());
			}
		}
		Console.WriteLine();
	}
}
*/