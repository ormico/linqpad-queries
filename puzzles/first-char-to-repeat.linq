<Query Kind="Statements" />

string input = "abcdefgaxyz";

HashSet<char> set = new HashSet<char>();

char? rc = null;
foreach(var i in input)
{
	if(set.Contains(i))
	{
		rc = i;
		break;
	}
	set.Add(i);
}

if(rc.HasValue)
	Console.WriteLine("First char to repeat: {0}", rc);
else
	Console.WriteLine("No char repeats");