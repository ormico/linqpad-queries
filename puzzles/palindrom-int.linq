<Query Kind="Statements" />

var s = new Solution();

/* utility functions
Console.WriteLine($"0: {s.digitsInInt(0)}");
Console.WriteLine($"1: {s.digitsInInt(1)}");
Console.WriteLine($"10: {s.digitsInInt(10)}");
Console.WriteLine($"99: {s.digitsInInt(99)}");
Console.WriteLine($"123456: {s.digitsInInt(123456)}");
Console.WriteLine($"-123456: {s.digitsInInt(-123456)}");
Console.WriteLine($"-123456 false: {s.digitsInInt(-123456, false)}");
*/

int i;
i = 12321;
Console.WriteLine($"i = {i} a) {s.IsPalindrome(i)} b) {s.IsPalindrome2(i)}");
i = 1221;
Console.WriteLine($"i = {i} a) {s.IsPalindrome(i)} b) {s.IsPalindrome2(i)}");
i = 11122;
Console.WriteLine($"i = {i} a) {s.IsPalindrome(i)} b) {s.IsPalindrome2(i)}");

/*
21 mar 2021 - the (b) IsPalindrome2(int x) option is much faster. 
this is the string version
a) 00:18:38.4136247
b) 00:01:12.3518032

there might be a way to speed up. for example I could cache the Pow() calls as 
they might get executed enough times to make a difference but that woudl only bee
a speed up for multiple calls where caching would make sense or by pre-calculating stuff
like powers of of 10 (10^n)

modified (a) to use cached values for powers of 10 and it cut the time to 00:05:57.1932297
this is still slower than (b) but much faster than first attempt.
there is also some issue that was getting index out of range and I added some Math.Pow() calls for 
overflow but I think there is something wrong going on.
could try caching powers values using long instead of int

tried another version where I used long[] for the powers array but it looks like casting back and forth
made it take a little longer 00:06:48.2745566
*/
DateTime start = DateTime.Now;
for(int j = 0; j < int.MaxValue; j++)
{
	_ = s.IsPalindrome(j);
}
DateTime end = DateTime.Now;
Console.WriteLine($"a) {end - start}");

/*
start = DateTime.Now;
for (int j = 0; j < int.MaxValue; j++)
{
	_ = s.IsPalindrome2(j);
}
end = DateTime.Now;
Console.WriteLine($"b) {end - start}");
*/

public class Solution
{
	public bool IsPalindrome(int x)
	{
		bool rc = true;
		
		int i = 1, j = digitsInInt(x);
		while (i < j)
		{
			if (digitAt(x, i) != digitAt(x, j))
			{
				rc = false;
				break;
			}
			i++;
			j--;
		}
		return rc;
	}

	long[] pow = { 1, 10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000, 1000000000, 10000000000, 100000000000 };

	int digitAt(int n, int i)
	{
		/*
		int rc =
			(n / ((int)Math.Pow(10, i - 1)))
			-
			(10 * (n / ((int)Math.Pow(10, i))));
		*/
		long m = n;
		long rc =
			(m / PowPow(i-1))
			-
			(10 * (m / PowPow(i)));			
		return (int)rc;
	}
	
	long PowPow(int n)
	{
		//Console.WriteLine($">>{n}");
		//return (n < pow.Length ? pow[n] : (int)Math.Pow(10, n));
		return pow[n];
	}
	
	public int digitsInInt(int n, bool ignoreNegative = true)
	{
		int j = Math.Abs(n);
		int rc = 0;
		//while (rc == 0 || j / (int)Math.Pow(10, rc) > 0)
		while (rc == 0 || j / PowPow(rc) > 0)
		{
			rc++;
		}
		
		if(ignoreNegative == false && n < 0)
		{
			rc++;
		}
		
		return rc;
	}

	// using string
	public bool IsPalindrome2(int x)
	{
		bool rc = true;
		string s = x.ToString();
		int i = 0, j = s.Length - 1;
		while(i < j)
		{
			if(s[i] != s[j])
			{
				rc = false;
				break;
			}
			i++;
			j--;
		}
		return rc;
	}
}