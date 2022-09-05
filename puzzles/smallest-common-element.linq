<Query Kind="Statements" />

/*Find Smallest Common Element in All Rows 
https://leetcode.com/explore/challenge/card/march-leetcoding-challenge-2021/591/week-4-march-22nd-march-28th/3680/

Given an m x n matrix mat where every row is sorted in strictly increasing order, return the smallest common element in all rows.

If there is no common element, return -1.

Constraints:

m == mat.length
n == mat[i].length
1 <= m, n <= 500
1 <= mat[i][j] <= 104
mat[i] is sorted in strictly increasing order.
*/

var s = new Solution();

var test1 = new int[][] { new int[] {1,2,3,4,5}, new int[] {2,4,5,8,10}, new int[] {3,5,7,9,11}, new int[] {1,3,5,7,9} };
Console.WriteLine($"1) {s.SmallestCommonElement(test1)}");

var test2 = new int[][] { 
	new int[] { 1, 2, 3, 4, 5, 99, 120, 500, 501, 505, 555 }, 
	new int[] { 2, 4, 5, 8, 10, 99, 101, 50, 51, 55, 555 }, 
	new int[] { 3, 5, 7, 9, 11, 99, 103, 200, 201, 205, 555 }, 
	new int[] { 1, 3, 5, 7, 9, 99, 130, 300, 301, 305, 355 }
	};
Console.WriteLine($"2) {s.SmallestCommonElement(test2)}");

var test3 = new int[][] {
	new int[] { 1, 2, 3, 4, 5, 99, 120, 500, 501, 505, 555 },
	new int[] { 2, 4, 5, 8, 10, 99, 101, 50, 51, 55, 555 },
	new int[] { 3, 5, 7, 9, 11, 99, 103, 200, 201, 205, 555 },
	new int[] { 1, 3, 6, 7, 9, 99, 130, 300, 301, 305, 355 }
	};
Console.WriteLine($"3) {s.SmallestCommonElement(test3)}");

var test4 = new int[][] {
	new int[] { 1, 2, 3, 4, 5, 94, 120, 500, 501, 505, 555 },
	new int[] { 2, 4, 5, 8, 10, 93, 101, 50, 51, 55, 555 },
	new int[] { 3, 5, 7, 9, 11, 92, 103, 200, 201, 205, 555 },
	new int[] { 1, 3, 6, 7, 9, 91, 130, 300, 301, 305, 355 }
	};
Console.WriteLine($"4) {s.SmallestCommonElement(test4)}");

var rand = new Random();
var test5 = new int[rand.Next(10000, 50000)][];
Console.Write($"Generating {test5.Length} random data items for test5");
for(int i = 0; i < test5.Length; i++)
{
	test5[i] = new int[test5.Length];
	for(int j = 0; j < test5[i].Length; j++)
	{
		test5[i][j] = rand.Next(0, 5000);
	}
	
	if(i%100 == 0)
		Console.Write(".");
}
Console.WriteLine();
Console.WriteLine("Running test.");
Console.WriteLine($"5) {s.SmallestCommonElement(test5)}");

public class Solution
{
	public int SmallestCommonElement(int[][] mat)
	{
		int rc = -1;
		int c = mat.Length;
		Dictionary<int, uint> count = new Dictionary<int, uint>();
		for(int i = 0; i < c; i++)
		{
			var m = mat[i];
			for(int j = 0; j < m.Length; j++)
			{
				if(!count.TryAdd(m[j], 1))
				{
					count[m[j]] = count[m[j]] + 1;
				}
			}
		}
		
		foreach(var i in count)
		{
			// we are looking for SMALLEST common
			// relying on the lists being sorted
			// we can scan from 0 to n and stop on first
			// and match and stop on first entry where
			// the accumulated count == mat.Length
			if(i.Value == c)
			{
				rc = i.Key;
				break;
			}
		}
		
		return rc;
	}
}