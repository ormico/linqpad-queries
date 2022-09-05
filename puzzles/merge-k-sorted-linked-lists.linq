<Query Kind="Program" />

void Main()
{
	// print starting data
	var data = CreateTestData1();
	for(int i = 0; i < data.Length; i++)
	{
		Console.Write($"{i}) ");
		var p = data[i];
		while (p != null)
		{
			Console.Write($"{p.val}|");
			p = p.next;
		}
		Console.WriteLine();
	}

	// compute answer
	var sol = new Solution();	
	var answer = sol.MergeKLists(CreateTestData1());
	
	//print answer
	var print = answer;
	Console.Write("answer) ");
	while(print != null)
	{
		Console.Write($"{print.val}|");
		print = print.next;
	}
	Console.WriteLine();
}

// SORTED from LEAST to GREATEST
ListNode[] CreateTestData1()
{
	ListNode[] rc = new ListNode[3];
	//rc[0] = new ListNode(1, new ListNode(4, new ListNode(5, new ListNode(0, new ListNode(0, new ListNode(0, new ListNode(0, new ListNode(0, new ListNode(0, )))))))));
	rc[0] = new ListNode(1, new ListNode(4, new ListNode(5)));
	rc[1] = new ListNode(1, new ListNode(3, new ListNode(4)));
	rc[2] = new ListNode(2, new ListNode(6));
	return rc;
}

public class ListNode
{
	public int val;
	public ListNode next;
	public ListNode(int val = 0, ListNode next = null)
	{
	this.val = val;
	this.next = next;
	}
}

public class Solution
{
	public ListNode MergeKLists(ListNode[] lists)
	{
		ListNode rc = null;
		ListNode rcC = null;
		ListNode[] current = new ListNode[lists.Length];
		// init to start
		for(int i = 0; i < lists.Length; i++)
		{
			current[i] = lists[i];
		}
		
		ListNode max = null;
		int maxIndex = 0;

		while (maxIndex != -1)
		{
			max = null;
			maxIndex = -1;
			for (int i = 0; i < current.Length; i++)
			{
				ListNode c = current[i];
				if(max == null)
				{
					max = c;
					maxIndex = i;
				}
				else if(c != null && c.val < max.val)
				{
					max = c;
					maxIndex = i;
				}
			}
			
			if(max != null)
			{
				var newcmx = max.next;
				if(rc == null)
				{
					rc = max;
					rc.next = null;
					rcC = rc;
				}
				else
				{
					rcC.next = max;
					rcC = rcC.next;
					rcC.next = null;
				}
				current[maxIndex] = newcmx;
			}
			else
			{
				maxIndex = -1;
			}
		}

		return rc;
	}
}