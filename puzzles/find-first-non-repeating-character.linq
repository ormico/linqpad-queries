<Query Kind="Program" />

/* find first non-repeating character

I got this one in an MS interview but I kept trying to find a better solution
but I should have gone with my first instinct. Ended up not solving it in time
although I was close.
*/
void Main()
{
	Console.WriteLine($"answer = '{findFirstNonRepeating("teeter")}'");
}

char? findFirstNonRepeating(string s)
{
	char? rc = null;
	
	var d = new Dictionary<char, int>();
	foreach(var c in s)
	{
		if (!d.TryAdd(c, 1))
			d[c] = d[c] + 1;
	}
	
	for(int i = 0; i < s.Length; i++)
	{
		if(d[s[i]] == 1)
		{
			rc = s[i];
			break;
		}
	}
	
	return rc;
}

