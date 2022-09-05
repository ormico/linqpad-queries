<Query Kind="Program" />

void Main()
{
	string input = "zigzagisfun";
	int n = 5;

	Console.WriteLine(zigZag(input, n));
	Console.WriteLine(zigZag(input, 3));
	Console.WriteLine(zigZag("ab", 1));
	Console.WriteLine(zigZag("abc", 2));
}

public string zigZag(string input, int n)
{
	/*
	// this only works for the first wave (down and up) but fails on start of second
	// wave b/c the distance from end of first wave to begining of 2nd wave is
	// shorter than distance from begining of wave to end of wave.
	for (int c = 0; c < n; c++)
	{
		for (int i = c; i < input.Length; i += (n - c - 1) * 2)
		{
			Console.Write("{0},", input[i]);
		}
	}
	*/
	if(n <= 0)
	{
		throw new ArgumentException("cannot be zero or negative", nameof(n));
	}
	
	if(n == 1)
	{
		return input;
	}
	
	List<StringBuilder> levels = new List<System.Text.StringBuilder>();
	// init the string builder list
	for(int i = 0; i < n; i++)
	{
		levels.Add(new StringBuilder());
	}
	
	bool down = false;
	int l = 0;
	for(int i = 0; i < input.Length; i++)
	{
		int edge = n==1? 1 : i % (n - 1);
		if (edge == 0)
		{
			down = !down;
		}
	
		var input_i = input[i];
	
		//Console.WriteLine($"{l} {down}");
		levels[l].Append(input_i);
		
		l += down ? (1) : -1;
	}
	
	StringBuilder rc = new StringBuilder();
	foreach(var sb in levels)
	{
		rc.Append(sb.ToString());
	}
	return rc.ToString();
}

