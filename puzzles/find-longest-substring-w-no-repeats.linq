<Query Kind="Program" />

void Main()
{
	var sol = new Solution();
	int answer;
	//var answer = sol.LengthOfLongestSubstring("abcdabcd");
	//Console.WriteLine(answer);
	//answer = sol.LengthOfLongestSubstring("abcabcbb");
	//Console.WriteLine(answer);
	//"pwwkew" should return 3 {"wke", "kew" }
	answer = sol.LengthOfLongestSubstring("pwwkew");
	Console.WriteLine(answer);
}

public class Solution {
    public int LengthOfLongestSubstring(string s)
    {
        int rc = 0;
        string rcStr = null;
        int length = s.Length;
        int i=0;
        int j=0;
        var characters = new HashSet<char>();
        
        while(i < length && j < length)
        {
            char c = s[j];
            if(characters.Contains(c) == false)
            {
                characters.Add(c);
                j++;
                // just need max but I also want to know the string
                // j - i should equal characters.Count()?
                int subStringLen = j - i;
                if(subStringLen > rc)
                {
                    rc = j - i;
                    rcStr = s.Substring(i, subStringLen);
                }
            }
            else
            {
				// the new window contains a duplicate char so we need to change the window.
				// we are moving the window by shrinking it 1 char from the left
				// remove the leftmost char from the set and increment i by 1
				characters.Remove(s[i]);
				i++;
            }
        }
        
        Console.WriteLine(rcStr);
        return rc;
    }
    
    public int LengthOfLongestSubstringOld(string s)
    {
        // O(n log n)
        // first loop on starting index: i
        // second loop on subString length (breaking on first fail): subStrLen
        // third loop to find repeating chars (breaking on first repeat): HasRepeatingChars(s)
        string rc = "";
        for(int i = 0; i < s.Length; i++)
        {            
            for(int subStrLen = 1; subStrLen <= (s.Length - i); subStrLen++)
            {
                //Console.WriteLine($"i={i} subStrLen={subStrLen}");
                string subStr = s.Substring(i, subStrLen);
                //Console.WriteLine($"'{subStr}'");

                
                bool repeatingChars = HasRepeatingCharacters(subStr);
                if(repeatingChars)
                {
					// if there are repeating chars, then all larger substrings starting
					// at the current starting index will fail
					// break and loop to next i value
					break;
				}
				else if (!repeatingChars && subStr.Length > rc.Length)
				{
					rc = subStr;
				}
			}

			if (rc.Length >= (s.Length - i))
			{
				break;
			}
		}
		return rc.Length;
	}

	//O(n log n)
	public bool HasRepeatingCharacters(string s)
	{
		var characters = new HashSet<char>();
		for (int i = 0; i < s.Length; i++)
		{
			char c = s[i];
			if (characters.Contains(c))
			{
				return true;
			}
			else
			{
				characters.Add(c);
			}
		}
		return false;
	}
}