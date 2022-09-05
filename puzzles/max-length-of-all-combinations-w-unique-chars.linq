<Query Kind="Program" />

/*
PROBLEM:
Given an array of strings arr. String s is a concatenation of a sub-sequence of arr which have unique characters.
Return the maximum possible length of s.

Example 1:
Input: arr = ["un","iq","ue"]
Output: 4
Explanation: All possible concatenations are "","un","iq","ue","uniq" and "ique".
Maximum length is 4.

Example 2:
Input: arr = ["cha","r","act","ers"]
Output: 6
Explanation: Possible solutions are "chaers" and "acters".

Example 3:
Input: arr = ["abcdefghijklmnopqrstuvwxyz"]
Output: 26

Constraints:
1 <= arr.length <= 16
1 <= arr[i].length <= 26
arr[i] contains only lower case English letters.

LINKS:
https://www.geeksforgeeks.org/print-all-possible-combinations-of-r-elements-in-a-given-array-of-size-n/

*/
//string[] s = new string[] { "un", "iq", "ue", "ab", "cd", "r", "ers" };
//comb.Add("");
void Main()
{
	var s = new int[] { 1, 2, 3, 4, 5, 6 };
	List<HashSet<int>> comb = new List<HashSet<int>>();
	for(int size = 0; size < s.Length; size++)
	{
		var indexes = new int[size];
		for(int i=0; i < size; i++) indexes[i] = i;
		
		int indexToIncr = size - 1;
		while(true)
		{
			bool breakNow = true;
			for(int b = 0; b < indexes.Length; b++)
			{
				if(indexes[b] != size)
				{
					breakNow = false;
					break;
				}
			}
			
			if(breakNow) break;
			
			HashSet<int> newComb = new HashSet<int>();
			for(int x = 0; x < indexes.Length; x++) newComb.Add(indexes[x]);
			
			indexes[indexToIncr]++;
			
			if(indexes[indexToIncr] >= size)
			{
				indexes[indexToIncr] = 0;
				indexToIncr
			}
			else
			{
				
			}
		}
	}
}

int IncIndex(int[] indexes, int i, int size)
{
	int rc = i;
	indexes[i]++;
	
	if (indexes[i] >= size)
	{
		indexes[i] = 0;
		
		if (i == 0)
			rc = i;
		else
			rc = IncIndex(indexes, i - 1, size);
	}

	return rc;
}
/* { "ab", "cd", "ef", "gh", "ij" }
1) ""
2) "ab", "cd", "ef", "gh", "ij"
3) "abcd", "abef", "abgh", "abij"
4) "cbef", "cbgh", "cbij"
5) "efgh", "efij"
6) "ghij"
7) foreach(x in (3)) 
*/


