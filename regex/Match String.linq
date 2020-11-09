<Query Kind="Statements" />

//Regex r = new Regex(".*c-.*", RegexOptions.IgnoreCase);//|RegexOptions.Compiled);
Regex r = new Regex("c-.*", RegexOptions.IgnoreCase);//|RegexOptions.Compiled);

for(int i = 0; i < 1; i++)
{
	Match m = r.Match(@"C-10/1 ARM");
	
	if(m.Success)
	{
		Console.WriteLine("match");
	}
	else
	{
		Console.WriteLine("no match");
	}
	
	Match m2 = r.Match("XXXX");
	
	if(m2.Success)
	{
		Console.WriteLine("match");
	}
	else
	{
		Console.WriteLine("no match");
	}
}

Console.WriteLine("-----------------------------------");

string data = "Connected... <br>INSERT failed: (1054 Loan #00000000-0000-0000-0000-000000000000) Unknown column 'contractcommitment' in 'field";
Regex r2 = new Regex(".*fail.*", RegexOptions.IgnoreCase);
var x = r2.Match(data?.Substring(0, 40));

if(x.Success)
{
	Console.WriteLine("match");
}
else
{
	Console.WriteLine("no match");
}
