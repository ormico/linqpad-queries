<Query Kind="Program" />

/*
We are given a list schedule of employees, which represents the working time for each employee.

Each employee has a list of non-overlapping Intervals, and these intervals are in sorted order.

Return the list of finite intervals representing common, positive-length free time for all employees, also in sorted order.

(Even though we are representing Intervals in the form [x, y], the objects inside are Intervals, 
not lists or arrays. For example, schedule[0][0].start = 1, schedule[0][0].end = 2, and schedule[0][0][0] is not defined).  
Also, we wouldn't include intervals like [5, 5] in our answer, as they have zero length.

Example 1:
Input: schedule = [[[1,2],[5,6]],[[1,3]],[[4,10]]]
Output: [[3,4]]
Explanation: There are a total of three employees, and all common
free time intervals would be [-inf, 1], [3, 4], [10, inf].
We discard any intervals that contain inf as they aren't finite.

Example 2:
Input: schedule = [[[1,3],[6,7]],[[2,4]],[[2,5],[9,12]]]
Output: [[5,6],[7,9]] 

Constraints:
1 <= schedule.length , schedule[i].length <= 50
0 <= schedule[i].start < schedule[i].end <= 10^8
*/
void Main()
{
	var sol = new Solution();
	var ex1 = GetExample1();
	var answer = sol.EmployeeFreeTime(ex1);
	//Console.WriteLine($"answer = {answer} answer should be ? ");

	Console.WriteLine($"answer = ");
	foreach(var i in answer)
	{
		Console.WriteLine($"{i.start} -> {i.end}");
	}
}

public class Interval {
    public int start;
    public int end;

    public Interval(){}
    public Interval(int _start, int _end) {
        start = _start;
        end = _end;
    }
}

public class Solution
{
	// using Sort makes this O(nlogn) i think
	public IList<Interval> EmployeeFreeTime(IList<IList<Interval>> schedule)
	{
		List<Interval> rc = new List<Interval>();		
		// merge and sort by start
		List<Interval> sch2 = new List<Interval>();
		for(int i = 0; i < schedule.Count; i++)
		{
			sch2.AddRange(schedule[i]);
		}
		sch2.Sort((l, r) => { return l.start.CompareTo(r.start); });
		//todo: can you improve by merging instead of concatenating and sorting?

		int start = sch2[0].start, end = sch2[0].end;
		for(int i = 1; i < sch2.Count; i++)
		{
			var c = sch2[i];
			if(c.start > end)
			{
				// current interval starts after prev interval
				// add time between as free
				rc.Add(new Interval(end, c.start));
				start = c.start;
				end = c.end;
			}
			else if(end < c.end)
			{
				// if overlap, the extend prev interval
				end = c.end;
				
				// if c is inside prev interval, then do nothing
			}
		}		

		return rc;
	}
}

IList<IList<Interval>> GetExample1()
{
	//schedule = 
	//	[
	//	[ [1,2],[5,6]   ],
	//	[ [1,3]			],
	//	[ [4,10]     	]
	//	]
	// answer should be start=3,end=4
	List<IList<Interval>> rc = new List<IList<Interval>>()
	{
		new List<Interval> { new Interval(1, 2), new Interval(5,6) },
		new List<Interval> { new Interval(1, 3) },
		new List<Interval> { new Interval(4, 10) },
	};
	
	return rc;
}