<Query Kind="Program" />

/*
Givent 2 sorted int arrays, return the medial value. should run in O(log (n + m))
*/
void Main()
{
	// should be 2
	// {1, 3} + { 2 } -> { 1, 2, 3 } and 2 is middle
	Console.WriteLine(FindMedianSortedArrays(new int[] { 1, 3 }, new int[] { 2 }));
	
	// should be 2.5
	// { 1, 3 } + { 2, 5 } -> { 1, 2, 3, 5 }
	// middle is 2 and 3.
	// (2 + 3) / 2 = 2.5
	Console.WriteLine(FindMedianSortedArrays(new int[] { 1, 3 }, new int[] { 2, 5 }));
}

public double FindMedianSortedArrays(int[] nums1, int[] nums2)
{
	double rc;
	int[] merged = MergeArrays(nums1, nums2);

	if (merged.Length % 2 == 0) // even
	{
		// must cast as double before dividing or result will be int, instead of double
		rc = (double)(merged[merged.Length/2] + merged[merged.Length/2 - 1]) / 2;
	}
	else // odd
	{
		rc = merged[merged.Length / 2];
	}

	return rc;
}

int[] MergeArrays(int[] nums1, int[] nums2)
{
	int[] rc = new int[nums1.Length + nums2.Length];
	int i = 0, n1i = 0, n2i = 0;

	while (n1i < nums1.Length || n2i < nums2.Length)
	{
		if (n1i < nums1.Length && n2i < nums2.Length)
		{
			if (nums1[n1i] < nums2[n2i])
			{
				rc[i++] = nums1[n1i++];
			}
			else // if(num1[n1i] >= num2[n2i])
			{
				rc[i++] = nums2[n2i++];
			}
		}
		else if (n1i < nums1.Length)
		{
			rc[i++] = nums1[n1i++];
		}
		else if (n2i < nums2.Length)
		{
			rc[i++] = nums2[n2i++];
		}
		else
		{
			// possible?
			Console.WriteLine("impossible");
		}
	}

	return rc;
}