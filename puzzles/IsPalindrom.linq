<Query Kind="Program" />

void Main()
{
	Console.WriteLine(IsPalindrom("abccba".ToCharArray()));
	Console.WriteLine(IsPalindrom("abcdcba".ToCharArray()));
	Console.WriteLine(IsPalindrom("a".ToCharArray()));
	Console.WriteLine(IsPalindrom("ab".ToCharArray()));
	Console.WriteLine(IsPalindrom("abc".ToCharArray()));
	Console.WriteLine(IsPalindrom("xyz".ToCharArray()));

	Console.WriteLine("------------------------------------------------------");

	Console.WriteLine(IsPalindrom("abccba", 0, 6));
	Console.WriteLine(IsPalindrom("abcdcba", 0, 7));
	Console.WriteLine(IsPalindrom("a", 0, 1));
	Console.WriteLine(IsPalindrom("ab", 0, 2));
	Console.WriteLine(IsPalindrom("abc", 0, 3));
	Console.WriteLine(IsPalindrom("xyz", 0, 3));

	Console.WriteLine("------------------------------------------------------");

	Console.WriteLine(IsPalindrom("abccba"));
	Console.WriteLine(IsPalindrom("abcdcba"));
	Console.WriteLine(IsPalindrom("a"));
	Console.WriteLine(IsPalindrom("ab"));
	Console.WriteLine(IsPalindrom("abc"));
	Console.WriteLine(IsPalindrom("xyz"));
}

bool IsPalindrom(string input)// Palindrom
{
	return IsPalindrom(input, 0, input?.Length??0);
}

bool IsPalindrom(string input, int startIndex, int length)
{
	bool rc = true;
	int i = startIndex, j = length + startIndex - 1;

	while (i < j)
	{
		if (input[i] != input[j])
		{
			rc = false;
			break;
		}

		i++;
		j--;
	}

	return rc;
}

bool IsPalindrom(char[] input)
{
	bool rc = true;
	int i = 0, j = input.Length - 1;
	
	while(i < j)
	{
		if(input[i] != input[j])
		{
			rc = false;
			break;
		}
		
		i++;
		j--;
	}
	
	return rc;	
}
