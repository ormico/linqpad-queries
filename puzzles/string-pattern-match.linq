<Query Kind="Statements" />

/*
Regular Expression Matching
Given an input string (s) and a pattern (p), implement regular expression matching with support for '.' and '*' where: 
'.' Matches any single character.​​​​
'*' Matches zero or more of the preceding element.
The matching should cover the entire input string (not partial).

Example 1:
Input: s = "aa", p = "a"
Output: false
Explanation: "a" does not match the entire string "aa".

Example 2:
Input: s = "aa", p = "a*"
Output: true
Explanation: '*' means zero or more of the preceding element, 'a'. Therefore, by repeating 'a' once, it becomes "aa".

Example 3:
Input: s = "ab", p = ".*"
Output: true
Explanation: ".*" means "zero or more (*) of any character (.)".

Example 4:
Input: s = "aab", p = "c*a*b"
Output: true
Explanation: c can be repeated 0 times, a can be repeated 1 time. Therefore, it matches "aab".

Example 5:
Input: s = "mississippi", p = "mis*is*p*."
Output: false 

Constraints:
0 <= s.length <= 20
0 <= p.length <= 30
s contains only lowercase English letters.
p contains only lowercase English letters, '.', and '*'.
It is guaranteed for each appearance of the character '*', there will be a previous valid character to match.
*/
var sol = new Solution();
Console.WriteLine("1) {0}", sol.IsMatch("aa", "a"));// should be false
Console.WriteLine("2) {0}", sol.IsMatch("aa", "a*"));// should be true
Console.WriteLine("3) {0}", sol.IsMatch("aa", ".*"));// should be true
Console.WriteLine("4) {0}", sol.IsMatch("aab", "c*a*b"));// should be true
Console.WriteLine("5) {0}", sol.IsMatch("mississippi", "mis*is*p*."));// should be false
Console.WriteLine("6) {0}", sol.IsMatch("aaabcddeffgh", "a.abcd*effgh*"));// should be true
Console.WriteLine("7) {0}", sol.IsMatch("aaabcdddddeffgh", "a.abc.*effgh*"));// should be true
Console.WriteLine("8) {0}", sol.IsMatch("ab", ".*c"));// should be false
Console.WriteLine("9) {0}", sol.IsMatch("aaa", "aaaa"));// should be false

// this should match. the first 2 'a' should match the .* and the last 2 'a' 
// should match the 'aa' constants.
Console.WriteLine("10) {0}", sol.IsMatch("aaaa", ".*aa"));// should be true


// cannot get this one to match
Console.WriteLine("11) {0}", sol.IsMatch("aaa", "ab*a*c*a"));// should be true

public class Solution
{
	public bool IsMatch(string s, string p)
	{
		return IsMatch(s, 0, p, 0);
	}
	
	void Debug(string sx, string px)
	{
		Console.WriteLine($"'{sx}' matches '{px}'");
	}
	
	bool IsMatch(string s, int si, string p, int pi)
	{
		string sx = si >= s.Length ? "<n/a>" : $"{s[si]}";
		string px = pi >= p.Length ? "<n/a>" : $"{p[pi]}";
		
		if(si >= s.Length)
		{
			if(pi < p.Length)
			{
				if(p[pi] == '*')
				{
					Debug(sx,px);
					return IsMatch(s, si, p, pi + 1);
				}
				else
					return false;
			}
			
			return true;
		}
		
		if(pi >= p.Length)
		{
			return false;
		}
		
		if(s[si] == p[pi])
		{
			Debug(sx, px);
			return IsMatch(s, si + 1, p, pi + 1);
		}
		
		if(p[pi] == '.')
		{
			Debug(sx, px);
			return IsMatch(s, si + 1, p, pi + 1);
		}
		
		// if pattern is *, and the pattern index is > 0
		// then check to see if the current string char == 
		// the pattern char right before the *
		if(p[pi] == '*' && pi > 0)
		{
			if(s[si] == p[pi - 1] || p[pi - 1] == '.')
			{
				// don't increment the pattern index since we might
				// continue matching the *
				Debug(sx, px);
				return IsMatch(s, si + 1, p, pi) || IsMatch(s, si + 1, p, pi + 1);
			}
			else
			{
				Debug(sx, px);
				return IsMatch(s, si, p, pi + 1);
			}
		}
		
		//todo: not sure this is correct in all cases
		return IsMatch(s, si, p, pi + 1);
	}
}
