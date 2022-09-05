<Query Kind="Statements" />

var R = new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4))));
Console.WriteLine($"original: {ListNode.ToString(R)}");
var S = new Solution();
R = S.ReverseList(R);
Console.WriteLine($"reversed: {ListNode.ToString(R)}");

var R2 = S.CopyAndReverseList(R);
Console.WriteLine($"reversed again: {ListNode.ToString(R2)}");

public class ListNode
{
    public int val;
    public ListNode next;
	
    public ListNode(int val=0, ListNode next=null)
	{
		this.val = val;
        this.next = next;
    }

	public override string ToString()
	{
		return $"{val}";
	}

	public static string ToString(ListNode root)
	{
		StringBuilder sb = new StringBuilder();
		var c = root;
		bool first = true;
		while(c != null)
		{
			if (first)
			{
				sb.Append(c);
				first=false;
			}
			else
				sb.AppendFormat(",{0}",c);
			
			c = c.next;
		}
		return sb.ToString();
	}

	public static string ToString(IEnumerable<ListNode> col)
	{
		var scol = col.Select(i => i.ToString());
		StringBuilder sb = new StringBuilder();
		sb.AppendJoin(',', scol);
		return sb.ToString();
	}
}

public class Solution
{
	/// <summary>
	/// this method will reverse the List in O(n) and O(1) space
	/// the downside is that it replaces the list in place, thus changing the original list
	/// returns new head
	/// to understand this alg just remember the 3 pointers: prev, current, & next
	/// as you traverse the list, current changes its pointer from next to prev and then
	/// you advance all 3 pointers
	/// </summary>
	public ListNode ReverseList(ListNode head)
	{
		ListNode rc = null, current = head, prev = null, next;		
		
		while(current != null)
		{
			// grab ref to next node in chain
			next = current.next;
			// point current at previous to reverse this node's direction
			current.next = prev;
			// move pointers current to next node
			prev = current;
			current = next;			
		}
		
		// if current runs off the end of the list, it will == null
		// prev should point to last known node
		rc = prev;
		return rc;
	}
	
	/// <summary>
	/// This method reverses a list by making a copy.
	/// Original list is unchanged.
	/// O(n) and O(n) space
	/// </summary>
	public ListNode CopyAndReverseList(ListNode head)
	{
		ListNode rc = null, current = head, previous = null;
		
		while(current != null)
		{
			ListNode newNode = new ListNode(current.val, previous);
			previous = newNode;
			current = current.next;
		}
		rc = previous;
		return rc;
	}
}
