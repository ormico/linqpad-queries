<Query Kind="Program" />

/*
weird sums
*/
void Main()
{
	/*
	Console.WriteLine(hash(11));
	Console.WriteLine(hash(111));
	Console.WriteLine(hash(12));
	Console.WriteLine(hash(99));
	*/
	var sol = new Solution();
	var answer = sol.solution(new int[] {51, 71, 17, 42});
	Console.WriteLine(answer);

	answer = sol.solution(new int[] { 42,33,60 });
	Console.WriteLine(answer);

	answer = sol.solution(new int[] { 51,32,43 });
	Console.WriteLine(answer);
}

/// <summary>
/// compute the sum of the digits(refered to here as the hash) of each
/// integer and store in a Dictionary grouped by common sum values.
/// Compute the sum of each combination of integers for each
/// common hash and return the value with the greatest sum.
/// if no combinations exist such that they can be summed
/// then return -1.
/// </summary>
class Solution {
	public int solution(int[] A)
	{
		// init to error code. if no valid combination is found
		// this value will be returned
		int rc = -1;
		
		//find digit sums
		var hashes = new Dictionary<int, List<int>>();
		
		for(int i=0;i<A.Length;i++)
		{
			int n = A[i];
			int h = hash(n);
			if(hashes.ContainsKey(h))
			{
				hashes[h].Add(n);
			}
			else
			{
				hashes.Add(h, new List<int>() { n });
			}
		}
		
		// now do combinations of each set until you find the max value
		foreach(var k in hashes.Keys)
		{
			var list = hashes[k];
			// which is that better? cost of sort vs just doing the combinations?
			// I turned in the combination version, but I should have used the sort version.

			/* O(n^2)
			for(int i=0;i < list.Count - 1;i++)
			{
				for(int j=1;j < list.Count;j++)
				{
					int ni = list[i];
					int nj = list[j];
					int sum = ni + nj;
					if(sum > rc)
						rc = sum;
				}
			}
			*/
			
			// O(nlogn)
			// if there aren't at least 2 then we cannot sum
			if (list.Count >= 2)
			{
				// only sort if there are more than 2
				if (list.Count > 2)
				{
					// use custom comparer to sort biggest to smallest
					list.Sort((l, r) => r.CompareTo(l));
				}
				
				int ni = list[0];
				int nj = list[1];
				int sum = ni + nj;
				if (sum > rc)
					rc = sum;
			}
		}
		
		return rc;
	}

	/// <summary>
	/// Sum the digits of an integer and return the value.
	/// </summary>
	int hash(int n)
	{
		// convert integer to string
		string s = $"{n}";
		int rc = 0;
		// we can take advantage of the character's value to sum the digits
		foreach (var c in s)
		{
			rc += (c - '1' + 1);
		}
		return rc;
	}
}
