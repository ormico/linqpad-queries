<Query Kind="Statements" />

string folder = @"C:\input-folder\";
string[] searchPatterns = { "CreateConfigFiles.ps1", "CreateConfigFiles - Copy.ps1" };
/*
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
*/
foreach(var pattern in searchPatterns)
{
	foreach(var p in Directory.GetFiles(folder, pattern, SearchOption.AllDirectories))
	{
		Console.WriteLine(p);
		using(var f = File.OpenRead(p))
		{
			for(int i = 0; i < f.Length; i++)
			{
				Console.Write("{0:000}|", (byte)f.ReadByte());
			}
		}
		Console.WriteLine();
	}
}