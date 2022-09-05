<Query Kind="Statements" />

/*Vowel Spellchecker
https://leetcode.com/explore/challenge/card/march-leetcoding-challenge-2021/591/week-4-march-22nd-march-28th/3681/

Given a wordlist, we want to implement a spellchecker that converts a query word into a correct word.

For a given query word, the spell checker handles two categories of spelling mistakes:

Capitalization: If the query matches a word in the wordlist (case-insensitive), then the query word is returned with the same case as the case in the wordlist.
Example: wordlist = ["yellow"], query = "YellOw": correct = "yellow"
Example: wordlist = ["Yellow"], query = "yellow": correct = "Yellow"
Example: wordlist = ["yellow"], query = "yellow": correct = "yellow"
Vowel Errors: If after replacing the vowels ('a', 'e', 'i', 'o', 'u') of the query word with any vowel individually, it matches a word in the wordlist (case-insensitive), then the query word is returned with the same case as the match in the wordlist.
Example: wordlist = ["YellOw"], query = "yollow": correct = "YellOw"
Example: wordlist = ["YellOw"], query = "yeellow": correct = "" (no match)
Example: wordlist = ["YellOw"], query = "yllw": correct = "" (no match)
In addition, the spell checker operates under the following precedence rules:

When the query exactly matches a word in the wordlist (case-sensitive), you should return the same word back.
When the query matches a word up to capitlization, you should return the first such match in the wordlist.
When the query matches a word up to vowel errors, you should return the first such match in the wordlist.
If the query has no matches in the wordlist, you should return the empty string.
Given some queries, return a list of words answer, where answer[i] is the correct word for query = queries[i].

Example 1:
Input: wordlist = ["KiTe","kite","hare","Hare"], queries = ["kite","Kite","KiTe","Hare","HARE","Hear","hear","keti","keet","keto"]
Output: ["kite","KiTe","KiTe","Hare","hare","","","KiTe","","KiTe"]

Example 2:
Input: wordlist = ["yellow"], queries = ["YellOw"]
Output: ["yellow"]
*/

var s = new Solution();
Test.Go(s, new string[] { "KiTe", "kite", "hare", "Hare" }, new string[] { "kite", "Kite", "KiTe", "Hare", "HARE", "Hear", "hear", "keti", "keet", "keto" });
Test.Go(s, new string[] { "apple", "pear", "orange", "grape", "Apple", "Pear", "Orange", "Grape", "bread" }, 
	new string[] { "upple", "APPLE", "cat", "pEAr", "peeR" });

class Test
{
	public static void Go(Solution s, string[] wordlist, string[] queries)
	{
		Console.WriteLine("-- Begin Test --");

		var answ = s.Spellchecker(wordlist, queries);
		for(int i = 0; i < queries.Length; i++)
		{
			Console.WriteLine($"{queries[i]} => {answ[i]}");
		}

		Console.WriteLine("-- End Test ----");
	}
}

public class Solution
{
	const char mask = '*';
	Dictionary<string, string> words = new Dictionary<string, string>();
	Dictionary<string, string> ciwords = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
	Dictionary<string, string> vwords = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
	
	public string[] Spellchecker(string[] wordlist, string[] queries)
	{
		if(queries == null)
			return null;
			
		string[] rc = new string[queries.Length];
		Init(wordlist);
		
		for(int i = 0; i < queries.Length; i++)
		{
			string q = queries[i];
			string t = "";
			if (!words.TryGetValue(q, out t))
			{
				if (!ciwords.TryGetValue(q, out t))
				{
					vwords.TryGetValue(vowelMask(q), out t);
				}
			}
			rc[i] = t??"";
		}
		
		return rc;
	}
	
	void Init(string[] wordlist)
	{
		words.Clear();
		vwords.Clear();
		
		foreach(var w in wordlist)
		{
			ciwords.TryAdd(w, w);
			words.TryAdd(w, w);
			vwords.TryAdd(vowelMask(w), w);
		}
	}
	
	string vowelMask(string w)
	{
		var sb = new StringBuilder(w.Length);		
		foreach(var c in w)
		{
			if(isVowel(c))
			{
				sb.Append(mask);
			}
			else
			{
				sb.Append(c);
			}
		}
		return sb.ToString();
	}
	
	bool isVowel(char c)
	{
		return c == 'a' || c == 'A' || c == 'e' || c == 'E' || c == 'i' || c == 'I' || c == 'o' || c == 'O' || c == 'u' || c == 'U';
	}
}