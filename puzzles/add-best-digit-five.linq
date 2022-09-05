<Query Kind="Program" />

void Main()
{
	var s = new Solution();
	Console.WriteLine(s.solution(268));
	Console.WriteLine(s.solution(999));
	Console.WriteLine(s.solution(1));
	Console.WriteLine(s.solution(90));
	Console.WriteLine(s.solution(-1));
	Console.WriteLine(s.solution(-90));
	Console.WriteLine(s.solution(-999));
}

class Solution
{
	public int solution(int N)
	{
		const int specialDigit = 5;
		const char specialChar = '5';
		// convert int to string so we can process digits one at a time
		string S = $"{N}";
		// determine if digit is positive or negative
		bool negative = N < 0;
		
		// construct new int as string so we can easily insert '5' in best location
		StringBuilder sb = new StringBuilder();
		if(negative)
		{
			sb.Append("-");
		}

		// track wheter '5' has already been added or still needs to be added
		bool five = false;
		
		for(int i = 0; i < S.Length; i++)
		{
			if(negative && i == 0)
			{
				continue;
			}
			
			char c = S[i];
			
			// if '5' hasn't been added, determine if this is best place
			if(!five)
			{
				// convert current digit back to int so we can determine it's value
				int digit = int.Parse($"{c}");				
				if(negative)
				{
					if (digit >= specialDigit)
					{
						sb.Append(specialDigit);
						five = true;
					}
				}
				else
				{
					if (digit <= specialDigit)
					{
						sb.Append(specialDigit);
						five = true;
					}
				}
			}

			sb.Append(c);
		}

		// if entire string has been processed and no better location has been found
		// then add '5' at the end
		if(!five)
		{
			sb.Append(specialChar);
		}
		
		return int.Parse(sb.ToString());
	}
}
