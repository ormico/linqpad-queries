<Query Kind="Program" />

/*
Given a string s, the power of the string is the maximum length of a non-empty substring that contains only one unique character.
Return the power of the string.

Example 1:
Input: s = "leetcode"
Output: 2
Explanation: The substring "ee" is of length 2 with the character 'e' only.

Example 2:
Input: s = "abbcccddddeeeeedcba"
Output: 5
Explanation: The substring "eeeee" is of length 5 with the character 'e' only.

Example 3:
Input: s = "triplepillooooow"
Output: 5

Example 4:
Input: s = "hooraaaaaaaaaaay"
Output: 11

Example 5:
Input: s = "tourist"
Output: 1

Constraints:
1 <= s.length <= 500
s contains only lowercase English letters.
*/
void Main()
{
	var sol = new Solution();
	var answer = sol.MaxPower("hooraaaaaaaaaaay");
	Console.WriteLine($"answer == {answer} is {answer == 11}");
}

public class Solution
{
	public int MaxPower(string s)
	{
		if(s == null || s.Length <= 0)
			return 0;
		
		int score = 1, rc = score;
		for(int i=1; i < s.Length; i++)
		{
			char prev = s[i - 1];
			char cur = s[i];
			if(cur == prev)
			{
				score++;
			}
			else
			{
				if(rc < score)
				{
					rc = score;
				}
				score = 1;
			}
		}

		if (rc < score)
		{
			rc = score;
		}

		return rc;
	}
}