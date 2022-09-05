<Query Kind="Program" />

/*
Given a string (str) and a number of lines (numLines),
if the string is written in a zig zag pattern spanning numLines
up and down, write a function that would return the letters
going from left to write, one line at a time, minus
any spaces.

Ex. ZIGZAGFORM written in zig zag form on 3 lines would look like:
Z   A   R
 I Z G O M
  G   F
And if you read each character from left to right you would get:
ZARIZGOMGF
*/
void Main()
{
	//Console.WriteLine(zigZag(3, "ZIGZAGFORM"));
	Console.WriteLine(zigZag(1, "AB")); 
}

string zigZag(int numRows, string str)
{
	if (numRows < 1)
	{
		throw new ArgumentException("numRows must be greater than 1");
	}

	if (str == null)
	{
		throw new ArgumentNullException($"{nameof(str)} cannot be null");
	}

	List<StringBuilder> lines = new List<StringBuilder>();

	int n = 0;
	bool down = true;
	for (int i = 0; i < str.Length; i++)
	{
		if (lines.Count < n + 1)
		{
			lines.Add(new StringBuilder());
		}
		
		lines[n].Append(str[i]);

		if (down)
			n++;
		else
			n--;

		if (n >= numRows)
		{
			n -= 2;
			down = !down;
		}
		else if (n < 0)
		{
			n += 2;
			down = !down;
		}
	}

	StringBuilder rc = new StringBuilder();
	foreach (var sb in lines)
	{
		rc.Append(sb.ToString());
	}

	return rc.ToString();
}
