<Query Kind="Program" />

void Main()
{

	string testStr = "1234567890";
	ListNode test = new ListNode(testStr);
	string testOutputStr = test.ToString();

	Console.WriteLine($"input = {testStr}");
	Console.WriteLine($"output = {testOutputStr}");
	Console.WriteLine($"IsEqual? = {string.Equals(testStr, testOutputStr)}");
	//Console.WriteLine($"2nd = {ListNodeNumberToString(test)}");

	//a = [9,9,9,9,9,9,9]
	//b = [9,9,9,9]
	// answer should be a + b = 
	// [8,9,9,9,0,0,0,1] = reversed to > 10009998

	/*
	[9,9,9,9,9,9,9]
	[9,9,9,9]
	*/
	ListNode a = new ListNode("9999999");
	ListNode b = new ListNode("9999");
	//ListNode a = new ListNode("243");
	Console.WriteLine($"a = {a.ToString()}");
	//ListNode b = new ListNode("564");
	Console.WriteLine($"b = {b.ToString()}");

	var answer = AddTwoNumbers(a, b);
	//answer.Dump();

	Console.WriteLine(answer.ToString());

	// correct answer is 10009998
	//const string correctAnswer = "10009998";
	//Console.WriteLine($"correct? = {string.Equals(answer.ToString(), correctAnswer)}");
}

public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
{
	int l1int, l2int, carry = 0;
	//string aStr = "", bStr = "";
	ListNode cl1 = l1, cl2 = l2, rc = new ListNode();
	ListNode current = rc;
	while (cl1 != null || cl2 != null || carry > 0)
	{
		// w/o the parens if cl1.val is 9 and cl2.val is 9 and carry is zero, it gives 9 as the answer (incorrectly)
		// not sure why the parens are required, but w/them it gives correct answer of 18
		//int sum = (cl1?.val??0) + (cl2?.val??0) + carry;
		
		/* alt way of doing it
		*/
		int sum;
		int cl1int = 0;
		if(cl1 != null)
		{
			cl1int = cl1.val;
		}

		int cl2int = 0;
		if (cl2 != null)
		{
			cl2int = cl2.val;
		}
		
		sum = cl1int + cl2int + carry;

		/* 2nd alt way of doing it
		int cl1int = cl1?.val ?? 0;
		int cl2int = cl2?.val ?? 0;
		int sum = cl1int + cl2int + carry;
		*/

		if(sum > 9)
		{
			carry = 1;
			sum = sum - 10;
		}
		else
		{
			carry = 0;
		}
	
		current.val = sum;
		
		if(carry > 0 || cl1?.next != null || cl2?.next != null)
		{
			current.next = new ListNode();
			current = current.next;
		}

		if (cl1 != null)
		{
			cl1 = cl1.next;
		}

		if (cl2 != null)
		{
			cl2 = cl2.next;
		}
	}

	return rc;
}

public class ListNode
{
	public int val;
	public ListNode next;
	
	public ListNode(int val=0, ListNode next=null) 
	{
	 this.val = val;
	 this.next = next;
	}
	
	public ListNode(string val)
	{
		ListNode current = null;
		foreach(var c in val.Reverse())
		{
			if(current == null)
			{
				current = this;
			}
			else
			{
				current.next = new ListNode();
				current = current.next;
			}
			current.val = int.Parse(c.ToString());
		}
	}

	public override string ToString()
	{
		StringBuilder rc = new StringBuilder();
		ListNode current = this;
		
		List<int> backwardsNumber = new List<int>();
		
		while(current != null)
		{
			backwardsNumber.Add(current.val);
			current = current.next;
		}
		backwardsNumber.Reverse();
		foreach(var v in backwardsNumber)
		{
			rc.Append(v.ToString());
		}

		return rc.ToString();
	}
}
