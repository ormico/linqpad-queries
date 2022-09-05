<Query Kind="Program" />

void Main()
{
	var s = new Solution();
	Console.WriteLine(s.solution("eeedaaad"));
	Console.WriteLine(s.solution("eeeedeeee"));
}

class Solution
{
	// scan input string and write directly to string builder for output
	public string solution(string S)
	{
		// use string builder to construct new string 
		// w/o excessive repeats
		StringBuilder sb = new StringBuilder();
        // track how many times a char is repeated in a row
		int repeatCount = 0;
        // track prev and current char and initilize to starting values
		char prevChar = ' ';
		char cur = ' ';
        for(int i = 0; i < S.Length; i++)
		{
			prevChar = cur;
			cur = S[i];
			if (cur == prevChar)
			{
				// if same char, increment repeat count
				repeatCount++;
			}
			else
			{
				// if new char, then reset repeat count
				repeatCount = 1;
			}

			if (repeatCount < 3)
			{
				// if char isn't repeated too many times
				// add it to new output string
				sb.Append(cur);
			}
		}

		return sb.ToString();
	}

	// can you write directly to StringBuilder w/o marking array?
	public string solution3(string S)
	{
        // convert string to char array for processing
		var c = S.ToArray();
        // track how many times a char is repeated in a row
		int repeatCount = 0;
        // track prev and current char and initilize to starting values
		char prevChar = ' ';
		char cur = ' ';
        for(int i = 0; i < c.Length; i++)
		{
			prevChar = cur;
			cur = c[i];
			if (cur == prevChar)
			{
				repeatCount++;
			}
			else
			{
				repeatCount = 1;
			}

			if (repeatCount >= 3)
			{
				// if a char repeats too many times then mark it
				// for removal by replacing with a space
				c[i] = ' ';
				repeatCount--;
			}
		}

		// convert array back to string w/o repeating chars
		StringBuilder sb = new StringBuilder();
		for (int x = 0; x < c.Length; x++)
		{
			if (c[x] != ' ')
			{
				sb.Append(c[x]);
			}
		}

		return sb.ToString();
	}

	public string solution2(string S)
	{
		var c = S.ToList();
		int repeatCount = 0;
		char prevChar = ' ';
		char cur = ' ';
		int i = 0;
		while (i < c.Count())
		{
			prevChar = cur;
			cur = c[i];
			if (cur == prevChar)
			{
				repeatCount++;
			}
			else
			{
				repeatCount = 1;
			}

			if (repeatCount >= 3)
			{
				c.RemoveAt(i);
				repeatCount--;
				continue;
			}

			i++;
		}

		string rc = new string(c.ToArray());

		return rc;
	}
}