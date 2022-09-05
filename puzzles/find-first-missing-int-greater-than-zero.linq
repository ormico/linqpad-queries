<Query Kind="Program" />

void Main()
{
	var sol = new Solution();

	// should return 1
	Console.WriteLine(sol.solution(new int[] { 3 }));

	Console.WriteLine(sol.solution(new int[] { -1, 0, 1, 3 }));
	Console.WriteLine(sol.solution(new int[] { 1, 2, 3 }));

	// should return 1
	Console.WriteLine(sol.solution(new int[] { -9, -2, 0, 3 }));

	// should return 1
	Console.WriteLine(sol.solution(new int[] { -9, 3 }));

	// should return 1
	Console.WriteLine(sol.solution(new int[] { 3 }));
}

class Solution {
	public int solution(int[] A)
	{
		// write your code in C# 6.0 with .NET 4.5 (Mono)
		int rc = 1;
		Array.Sort(A);
		
		Console.WriteLine("--------------");
		//for(int i = 0; i < A.Length; i++) Console.WriteLine("{0}", A[i]);
		int firstPositive = -1;
		
		for (int i = 0; i < A.Length; i++)
		{
			int v = A[i];
			
			if(v < 1)
				continue;

			if(firstPositive < 0)
				firstPositive = A[i];
			
			if(firstPositive > 1)
			{
				rc = 1;
				break;
			}
			
			if(i + 1 < A.Length)
			{
				int nv = A[i + 1];
				if(nv - v > 1)
				{
					rc = v + 1;
					break;
				}
			}
			else
			{
				rc = v + 1;
				
			}
			
			//Console.WriteLine($"{i} {v} {vn} {vn - v}");
		}
		
		return rc;
	}
}