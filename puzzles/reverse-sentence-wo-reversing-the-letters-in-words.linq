<Query Kind="Program" />

/*
reverse all the words in a sentence:


"hi, my name is zack." -> ".zack is name my ,hi"
".kcaz si eman ym ,ih"
".zack is name my ,hi"

si 
.zack is 


hi,
my
name
is
zack.

seperators are any anything that is not a letter [a - z and A - Z] or number [0 - 9]
*/
void Main()
{
	Console.WriteLine(IsWordChar('f'));
	Console.WriteLine(IsWordChar('3'));
	Console.WriteLine(IsWordChar('G'));
	Console.WriteLine(IsWordChar('0'));
	Console.WriteLine(IsWordChar('9'));
	Console.WriteLine(IsWordChar(' '));
	Console.WriteLine(IsWordChar(';'));

	//Console.WriteLine(ReverseWords("hi, my name is zack."));
	
	char[] input = "hi, my name is zack.".ToCharArray();
	ReverseWords(input);
	
	foreach(char i in input)
	{
		Console.Write(i);
	}
	Console.WriteLine();
}

HashSet<char> validChars = new HashSet<char>();

void ReverseWords(char[] s)
{
//	"hi, my name is zack."-> ".zack is name my ,hi"
//  ".i, my name is zackh"

//".kcaz si eman ym ,ih"
//".zack is name my ,hi"
	char x;
	
	// reverse whole string
	Reverse(s, 0, s.Length - 1);
	//#
	foreach (char i in s)
	{
		Console.Write(i);
	}
	Console.WriteLine();
	//#


	//reverse each word in string	
	int start = -1;
	for(int i = 0; i < s.Length; i++)
	{
		// find start and end index of word then call Reverse
		char c = s[i];
		if(IsWordChar(c))
		{
			if(start == -1)
			{
				start = i;
			}
		}
		else
		{
			if(start != -1)
			{
				Reverse(s, start, i-1);
				start = -1;
			}
		}		
	}
}

void Reverse(char[] s, int start, int end)
{
	char x;
	int length = (end - start);
	for (int i = start; i <= start + length / 2; i++)
	{
		x = s[i];
		s[i] = s[end - i + start];
		s[end - i + start] = x;
	}
}

string ReverseWords(string s)
{
	StringBuilder sb = new StringBuilder(s.Length);
	//StringBuilder buf = new StringBuilder();
	List<char> buf = new List<char>();
	
	for(int i = s.Length - 1; i >= 0; i--)
	{
		char c = s[i];
		if(IsWordChar(c))
		{
			buf.Add(c);
		}
		else
		{
			if(buf.Count > 0)
			{
				// write buffer out in reverse
				for(int j = buf.Count - 1; j >= 0; j--)
				{
					sb.Append(buf[j]);
				}
				
				// reset buffer
				buf.Clear();
			}
			
			sb.Append(c);
		}
	}

	if (buf.Count > 0)
	{
		// write buffer out in reverse
		for (int j = buf.Count - 1; j >= 0; j--)
		{
			sb.Append(buf[j]);
		}
	}

	return sb.ToString();;
}

bool IsWordChar(char c)
{
	return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9');	
}
