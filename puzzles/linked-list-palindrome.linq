<Query Kind="Statements" />

/* Palindrome Linked List
Given a linked list, determin if it is a palindrome.
*/
var root1 = new ListNode(new int[] { 1, 2, 3, 4 });
var root2 = new ListNode(new int[] { 1, 2, 3, 4, 5 });
var root3 = new ListNode(new int[] { 1, 2, 3, 4, 5, 6 });
var root4 = new ListNode(new int[] { 1, 2, 3, 1, 2, 3 });
var root5 = new ListNode(new int[] { 1, 2, 3, 4, 1, 2, 3 });
var root6 = new ListNode(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
var root7 = new ListNode(new int[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 });
var root8 = new ListNode(new int[] { 1, 2, 3, 4, 4, 3, 2, 1 });
var s = new SolutionDivideAndCompare();
Console.WriteLine($"root1: {ListNodeUtil.ToString(root1)}");
Console.WriteLine($"root2: {ListNodeUtil.ToString(root2)}");
Console.WriteLine($"root3: {ListNodeUtil.ToString(root3)}");

Console.WriteLine($"middle1: {s.FindMiddle(root1)}");
Console.WriteLine($"middle2: {s.FindMiddle(root2)}");
Console.WriteLine($"middle3: {s.FindMiddle(root3)}");

root1 = s.ReverseList(root1);
Console.WriteLine($"reversed: {ListNodeUtil.ToString(root1)}");

Console.WriteLine($"compare root1 & root1: {s.Compare(root1, root1)}");
Console.WriteLine($"compare root2 & root2: {s.Compare(root2, root2)}");
Console.WriteLine($"compare root1 & root2: {s.Compare(root1, root2)}");
Console.WriteLine($"compare root2 & root3: {s.Compare(root2, root3)}");
Console.WriteLine($"compare root4 first & second half: {s.Compare(root4, s.FindMiddle(root4))}");
Console.WriteLine($"compare root5 first & second half: {s.Compare(root5, s.FindMiddle(root5))}");


Console.WriteLine($"root6: {ListNodeUtil.ToString(root6)}");
//SolutionDivideAndCompare.ToString(root6)
var root6half = s.FindMiddle(root6);
var root6halfreversed = s.ReverseList(root6half);
Console.WriteLine($"reverse root6 after last half reversed: {ListNodeUtil.ToString(root6)}");
Console.WriteLine($"new root6 last half: {ListNodeUtil.ToString(root6halfreversed)}");
Console.WriteLine($"reverse root6 last half: {ListNodeUtil.ToString(root6)}");
Console.WriteLine($"compare root6 & root6halfreversed: {s.Compare(root6, root6halfreversed)}");

Console.WriteLine($"root7: {ListNodeUtil.ToString(root7)}");
Console.WriteLine($"IsPalindrome(root7): {s.IsPalindrome(root7)}");
Console.WriteLine($"root7 after: {ListNodeUtil.ToString(root7)}");

Console.WriteLine($"IsPalindrome(root8): {s.IsPalindrome(root8)}");

Console.WriteLine($"array after: {ListNodeUtil.ToString(new ListNode(new int[] { 7, 8, 9, 10, 11, 13 }))}");

Console.WriteLine($"IsPalindrome(7, 8, 9, 10, 11, 13): {s.IsPalindrome(new ListNode(new int[] { 7, 8, 9, 10, 11, 13 }))}");
Console.WriteLine($"IsPalindrome(test): {s.IsPalindrome(new ListNode(new int[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 }))}");
Console.WriteLine($"IsPalindrome(test): {s.IsPalindrome(new ListNode(new int[] { 1, 2, 3, 4, 4, 3, 2, 1 }))}");

/// <summary>
/// find middle, then reverse last half
/// finally compare first have and the reversed second half
/// remember that if there are an odd number the middle node belongs to both halves (or neither)
/// this method works but I think there is some sloppness around after the 2nd half is reversed
/// the first half still points to the node that was in the middle but is now the end of the 2nd half
/// ex. 1->2->3->4->5 
/// after last half reversed 1->2->3 but also 5->4->3
/// this method also has the side effect of modifying the original list but it could be modified to 
/// change it back before returning
/// </summary>
public class SolutionDivideAndCompare
{
	public bool IsPalindrome(ListNode head)
	{
		var half = this.FindMiddle(head);
		var newhalf = this.ReverseList(half);
		bool rc = this.Compare(head, newhalf);
		return rc;
	}
	
	/// <summary>
	/// use slow + fast pointers to find middle
	/// </summary>
	public ListNode FindMiddle(ListNode head)
	{
		ListNode slow = head, fast = head;
		int count = 0;
		while(fast != null)
		{
			//Console.WriteLine($"FindMiddle >> slow:{slow} fast:{fast}");
			fast = fast.next;
			count++;
			
			if(count%2==0)
			{
				count = 0;
				slow = slow.next;
			}
		}
		
		//if there are an odd number, move slow 1 more 
		//if(count == 1)
		//	slow = slow.next;
			
		return slow;
	}
	
	public ListNode ReverseList(ListNode head)
	{
		ListNode prev = null, current = head, next;
		while(current != null)
		{
			next = current.next;
			current.next = prev;
			prev = current;
			current = next;
		}
		return prev;
	}
	
	public bool Compare(ListNode head1, ListNode head2)
	{
		bool rc = true;
		ListNode c1 = head1, c2 = head2;
		while(c1 != null && c2 != null)
		{
			if(c1.val != c2.val)
			{
				rc = false;
				break;
			}
			c1 = c1.next;
			c2 = c2.next;
		}
		return rc;
	}
}

public class Solution2
{
	public bool IsPalindrome(ListNode head)
	{
		return false;
	}
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
	
	public ListNode(IEnumerable<int> values)
	{
		bool first = true;
		ListNode current = this;
		foreach(var i in values)
		{
			if(first)
			{
				this.val = i;
				first = false;
			}
			else
			{
				current.next = new ListNode(i);
				current = current.next;
			}
		}
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
		while (c != null)
		{
			if (first)
			{
				sb.Append(c);
				first = false;
			}
			else
				sb.AppendFormat(",{0}", c);

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

static class ListNodeUtil
{
	public static string ToString(ListNode root)
	{
		StringBuilder sb = new StringBuilder();
		bool first = true;
		ListNode current = root;
		while (current != null)
		{
			if (first)
			{
				sb.Append(current);
				first = false;
			}
			else
			{
				sb.AppendFormat(",{0}", current);
			}
			current = current.next;
		}

		return sb.ToString();
	}
}
