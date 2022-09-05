<Query Kind="Statements" />

/*
implement int atoi() or ascii to integer
convert string to signed int
ignore any characters after last digit
ignore any leading whitespace
number in string is composed of digits 0 - 9 and may be prefixed by a + or -
which determins the sing of the returned int
if number is larger than int.MaxValue return int.MaxValue
if number is smaller than int.MinValue return int.MinValue
*/

int MaxValue = (int)Math.Pow(2, 31) - 1, MinValue = (int)Math.Pow(2, 31) * -1;

Console.WriteLine($"MinValue = {MinValue} MaxValue = {MaxValue}");
Console.WriteLine($"long.MinValue = {long.MinValue} long.MaxValue = {long.MaxValue}");
Console.WriteLine($"int.MinValue = {int.MinValue} int.MaxValue = {int.MaxValue}");

Console.WriteLine($"atoi = {atoi("meh 4193 with words")}");
Console.WriteLine($"atoi = {atoi("meh -4193 with words")}");
Console.WriteLine($"atoi = {atoi("4193 with words")}");
Console.WriteLine($"atoi = {atoi("4193")}");
Console.WriteLine($"atoi = {atoi("+4193")}");
Console.WriteLine($"atoi = {atoi("-4193")}");
Console.WriteLine($"atoi = {atoi(" 4193")}");
Console.WriteLine($"atoi = {atoi("  -4193")}");
Console.WriteLine($"atoi = {atoi("  +4193")}");
Console.WriteLine($"atoi = {atoi("41 93")}");
Console.WriteLine($"atoi = {atoi("-91283472332")}");

Console.WriteLine($"atoi = {atoi("20000000000000000000")}");
Console.WriteLine($"atoi = {atoi("  0000000000012345678")}");
Console.WriteLine($"atoi = {atoi("9223372036854775808")}");

// some character conversion tests
Console.WriteLine($"{char2int('0')} {char2int('1')} {char2int('9')}");

long l = 3;
int i = (int)l;
Console.WriteLine(i);

for(int j = '0'; j < '0' + 11; j++)
{
	Console.WriteLine($"j = {j} {(char)j}");
}

int atoi(string input)
{
	int rci = 0;
	long rc = 0;
	int start = 0, end = 0;
	
	start = FindFirstPlusMinusDigit(input);
	if(start == -1)
		return rci;
	
	end = FindLastPlusMinusDigit(input, start);
	long j = 1;
	for(int i = end; i >= start; i--, j*=10)
	{
		char c = input[i];
		if (c == '+')
		{
			continue;
		}
		else if(c == '-')
		{
			rc*=-1;
		}
		else if(end - i < 11 )
		{
			rc += char2int(c) * j;
		}
		else if (end - i >= 11 && c != '0')
		{
			rc = (long)MaxValue + 1;
		}
	}

	// convert to int
	if(rc > MaxValue)
		rci = MaxValue;
	else if(rc < MinValue)
		rci = MinValue;
	else
		rci = (int)rc;
		
	return rci;
}

bool IsDigit(char c)
{
	return (c >= '0' && c <= '9');
}

bool IsPlusMinusDigit(char c)
{
	return (c >= '0' && c <= '9') || c == '-' || c == '+';
}

// return -1 if you hit a character before first digit or +- that is not a space
int FindFirstPlusMinusDigit(string input)
{
	int rc = -1, i = 0;
	if (input == null)
		return rc;

	while (i < input.Length)
	{
		if (IsPlusMinusDigit(input[i]))
		{
			rc = i;
			break;
		}
		else if (input[i] == ' ')
		{
			i++;
		}
		else
		{
			rc = -1;
			break;
		}
	}
	return rc;
}

/* this version ignores everything before first digit or +-
int FindFirstPlusMinusDigit(string input)
{
	int rc = -1, i = 0;
	if(input == null)
		return rc;
		
	while(i < input.Length)
	{
		if(IsPlusMinusDigit(input[i]))
		{
			rc = i;
			break;
		}
		i++;
	}
	return rc;
}
*/

int FindLastPlusMinusDigit(string input, int start)
{
	int rc = start, i = start;
	char c;
	
	if(i < input.Length && (input[i] == '-' || input[i] == '+'))
	{
		i++;
	}
	
	while(i + 1 < input.Length && IsDigit(input[i + 1]))
	{
		i++;
	}
	
	rc = i;
	
	return rc;
}

int char2int(char c)
{
	return c - '0';
}