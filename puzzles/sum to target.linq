<Query Kind="Program" />

/*
puzzle:
giving an array of of ints and a target value, return index of 2 numbers that when summed equal the target value.
cannot use the same number twice.

*/
void Main()
{
	TwoSum(new int[] { 7, 1, 2, 5 }, 9).Dump();
	TwoSum(new int[] { 1, 2, 3, 5, 4 }, 9).Dump();

	TwoSum(new int[] { -1, -2, -3, -4, -5 }, -8).Dump();
}

public int[] TwoSum(int[] nums, int target)
{
	int[] rc = null;

	for (int i = 0; i < nums.Length - 1; i++)
	{
		for (int j = i + 1; j < nums.Length; j++)
		{
			//Console.WriteLine($"i:{i} j:{j}");
			if (nums[i] + nums[j] == target)
			{
				rc = new int[2];
				rc[0] = i;
				rc[1] = j;
				break;
			}
		}
	}
	return rc;
}