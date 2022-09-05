<Query Kind="Program" />

/*
given a string, return a new string where all spaces have been encoded
as %20 w/o using .Replace() and using the string as a char[]
*/
void Main()
{
	string input = "my input string";
	char[] inputArray= input.ToCharArray();
	List<char> work = new List<char>();
	char[] space = "%20".ToCharArray();
	
	for(int i = 0; i < inputArray.Length; i++)
	{
		if(inputArray[i] != ' ')
		{
			work.Add(inputArray[i]);
		}
		else
		{
			work.AddRange(space);
		}
	}
	
	string output = new string(work.ToArray());
	Console.WriteLine(output);
}
