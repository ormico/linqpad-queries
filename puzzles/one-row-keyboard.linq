<Query Kind="Statements" />

/* 1 row keyboard
given a keyboard where a - z are all on 1 row
and the user's finger begins at 'a'
and it takes 1 unit of time for a user to move their finger 1 character
on the keyboard, 
calculate how long it would to take to type a given word.

the order of the letters appear on the keyboard must be determined from 
the keyboard parameter.

the parameters are:
keyboard: all 26 unique letters in the order they appear on the keyboard
word: the word being typed
*/

var s = new Solution();
Console.WriteLine(Test.Go(s, "abcdefghijklmnoqprstuvwxyz", "word"));
Console.WriteLine(Test.Go(s, "cba"));
//keyboard = "pqrstuvwxyzabcdefghijklmno", word = "leetcode"
Console.WriteLine(Test.Go(s, "pqrstuvwxyzabcdefghijklmno", "leetcode"));

class Test
{
	const string alphabet = "abcdefghijklmnoqprstuvwxyz";
	
	public static string Go(Solution s, string kb, string wd)
	{
		string rc = $"word: {wd} on keyboard '{kb}' = {s.CalculateTime(kb, wd)}";
		return rc;	
	}

	public static string Go(Solution s, string wd)
	{
		return Go(s, alphabet, wd);
	}
}

public class Solution
{
	public int CalculateTime(string keyboard, string word)
	{
		int rc = 0;
		Dictionary<char, int> kd = new Dictionary<char, int>();
		for(int i = 0; i < keyboard.Length; i++)
		{
			kd.Add(keyboard[i], i);
		}
		
		int prevPosition = 0;
		char prevChar = '!';
		
		for(int i = 0; i < word.Length; i++)
		{
			char c = word[i];
			int p = kd[c];
			rc += (int)Math.Abs(p - prevPosition);
			prevChar = c;
			prevPosition = p;
		}
		
		return rc;
	}
}