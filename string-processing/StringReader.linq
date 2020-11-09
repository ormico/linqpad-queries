<Query Kind="Statements" />

string data = @"Hello World!
This is
a test 
of StringReader!";

using(StringReader sr = new StringReader(data))
{
	string line;
	
	while((line = sr.ReadLine()) != null)
	{
		Console.WriteLine("{0} -- END LINE", line);
	}
}