<Query Kind="Program" />

void Main()
{
	var s = new Solution();
	s.solution(1000);
}

// You can define other methods, fields, classes and namespaces here
class Solution
{
	public void solution(int N)
	{
		for (int i = 1; i <= N; i++)
		{
			if(i % 2 == 0)
			{
				Console.Write("Codility");
			}

			if (i % 3 == 0)
			{
				Console.Write("Test");
			}

			if (i % 5 == 0)
			{
				Console.Write("Coders");
			}
			
			if((i % 2 != 0) && (i % 3 != 0) && (i % 5 != 0))
			{
				Console.Write(i);
			}
			
			Console.WriteLine();
		}
	}
}
