<Query Kind="Program" />

/*
This puzzle was given to me in Microsoft Interview for Senior Software Engineer for Azure CXP.
(I didn't get this job)

--== This file contains the code that I wrote during the interview.==--
--== See file 'intersection-of-2-large-streams.linq' for an updated implementation==--

The interviewer originally gave the puzzle as 2 streams but changed it to 2 arrays.

Puzzle:
Given 2 Streams (Stream ns1, Stream ns2), each stream contains 1 Billion int64 values.

Return a Stream that is the Intersection of the 2 streams.

The Interviewer defined intersection as each int64 value that is in both streams and also stated 
that I could assume that each value was unique within each stream.
-- 
So I started out trying to analyze the problem and the first issue I ran into was around the fact
that it was 2 large Streams. This may have been a mistake, but here was my thinking:
	The puzzle seemed to have intentionally made the streams large (int64 is 8 bytes and 
	1 Billion * 8 Bytes ~= 8 Gigabytes).
	B/c it was a stream and not an array it seemed like I was being discouraged from trying to seek 
	over the 2 Streams and I didn't see any way to find the intersection w/o also buffering each 
	stream.
	
	If they were File Streams then it would be possible to seek backwards. If they were Network Streams 
	(they were named ns1 and ns2) I don't think you can seek backwards to search.
	
	Of course I could have buffered them into File Streams, but that just seemed weird for a puzzle.
	on the other hand maybe that is what they were looking for.
--
	So after I shared some of these thoughts with the interviewer, he changed it to 2 arrays instead of
	2 streams and I worked the problem from there.
-- 
	After working the puzzle like this for a while, he stopped me and asked me the Big-O of my solution.
	I was solving it using 2 nested for loops so I answered O(n^2) which was a guess but correct.
	
	He then asked me to suggest optimizations and what that would change the Big-O to.
	
	Here is what I recall answering a/b optimizations: 
	1. So the numbers in the arrays could be in any order which made optimization difficult, so my
	first thought was could we sort them. But I wasn't sure if sorting both arrays would be more
	or less efficient than just doing the bubble sort. 
	
	>> So the instructor gave me that QuickSort is O(n log n) but of course I didn't know if that was
	better or worse or how to combine the Big-O from the Sort with the Big-O from the search.
	For future reference, (n log n) is way better than (n^2). N squared, curves up faster and faster 
	while (n log n) curves up initially but flattens out into a horizontal line.
	
	* I believe you don't combine Big-O items mathematically. I think you are supposed to take the worse
	or something, but I need to review.
*/
namespace Solution
{
	public class Solution
	{
        public static void Main(string[] args) {
            
        }
        
        public Stream Foo(Stream ns1, Stream ns2)
        {
            
            
        }
        
        public long[] GetIntersection(long[] input1, long[] input2)
        {
            List<long> rc = new List<long>();
            // n log time to sort
            long[] sortedInput1 = input1.Sort();
            long[] sortedInput2 = input2.Sort();

			// (n logn) + (nlogn) + n^2
			// (n logn) + (nlogn) + 2n = nlogn + 2n

			int stop = 0;
			for (int i = 0; i < input1.Length; i++)
			{
				if (intput[i] < input2[j])
				{
					break;
				}

				for (int j = stop; j < input2.Length; j++)
				{
					if (input1[i] == input2[j])
					{
						rc.Add(input1[i]);
						stop = j + 1;
						break;
					}
					else (intput[i] < input2[j])

					{
						break;
					}
				}
			}

			return rc.ToArray();
		}

		// 0 1 2 3 4 5 6 7
		// 1 2 3 4 5 6 7 8
		// 1 2 3 4 6 7 8 9

		// 1 2 3 4 6

		// 1 2 3 4 6 7 8 9
		// 1 2 3 4 5 6 7 8

	}
}
