<Query Kind="Program" />

void Main()
{
	var sol = new Solution();
	Console.WriteLine($"answer='{sol.LongestPalindrome2("a")}'");
	Console.WriteLine($"answer1='{sol.LongestPalindrome("abababbabba")}' answer2='{sol.LongestPalindrome2("abababbabba")}'");
	/*
	Console.WriteLine(sol.LongestPalindrome("abababbabba"));
	Console.WriteLine(sol.LongestPalindrome("aaaabba"));
	Console.WriteLine(sol.LongestPalindrome("abcdfdcba1"));
	Console.WriteLine(sol.LongestPalindrome("abdabddba"));
	Console.WriteLine(sol.LongestPalindrome("abcdefghijklmnopqrstaabbaa"));
	*/
}

public class Solution
{
	public string LongestPalindrome(string s)
	{
		// O(n2) or O(n3)?
		string rc = "";
		for (int i = 0; i < s.Length; i++)
		{
			for (int subStrLen = rc.Length > 1 ? rc.Length : 1; subStrLen <= (s.Length - i); subStrLen++)
			{
				bool isPalindrom = IsPalindrom(s, i, subStrLen);
				if (isPalindrom && subStrLen > rc.Length)
				{
					rc = s.Substring(i, subStrLen);
				}
			}

			if (rc.Length >= (s.Length - i))
			{
				break;
			}
		}
		return rc;
	}
	
	public string LongestPalindrome2(string s)
	{
		// O(n2) or O(n3)?
		string rc = "";
		for (int i = 0; i < s.Length; i++)
		{
			//for (int subStrLen = rc.Length > 1 ? rc.Length : 1; subStrLen <= (s.Length - i); subStrLen++)
			for (int subStrLen = (s.Length - i); subStrLen >= (rc.Length > 1 ? rc.Length : 1); subStrLen--)
			{
				bool isPalindrom = IsPalindrom(s, i, subStrLen);
				if (isPalindrom && subStrLen > rc.Length)
				{
					rc = s.Substring(i, subStrLen);
					break;
				}
			}

			if (rc.Length >= (s.Length - i))
			{
				break;
			}
		}
		return rc;
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
}