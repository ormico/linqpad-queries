<Query Kind="Program" />

/*
Design a stack which supports the following operations.

Implement the CustomStack class:
CustomStack(int maxSize) Initializes the object with maxSize which is the maximum number of elements in the stack or do nothing if the stack reached the maxSize.
void push(int x) Adds x to the top of the stack if the stack hasn't reached the maxSize.
int pop() Pops and returns the top of stack or -1 if the stack is empty.
void inc(int k, int val) Increments the bottom k elements of the stack by val. If there are less than k elements in the stack, just increment all the elements in the stack.

Example 1:

Input
["CustomStack","push","push","pop","push","push","push","increment","increment","pop","pop","pop","pop"]
[[3],[1],[2],[],[2],[3],[4],[5,100],[2,100],[],[],[],[]]
Output
[null,null,null,2,null,null,null,null,null,103,202,201,-1]
Explanation
CustomStack customStack = new CustomStack(3); // Stack is Empty []
customStack.push(1);                          // stack becomes [1]
customStack.push(2);                          // stack becomes [1, 2]
customStack.pop();                            // return 2 --> Return top of the stack 2, stack becomes [1]
customStack.push(2);                          // stack becomes [1, 2]
customStack.push(3);                          // stack becomes [1, 2, 3]
customStack.push(4);                          // stack still [1, 2, 3], Don't add another elements as size is 4
customStack.increment(5, 100);                // stack becomes [101, 102, 103]
customStack.increment(2, 100);                // stack becomes [201, 202, 103]
customStack.pop();                            // return 103 --> Return top of the stack 103, stack becomes [201, 202]
customStack.pop();                            // return 202 --> Return top of the stack 102, stack becomes [201]
customStack.pop();                            // return 201 --> Return top of the stack 101, stack becomes []
customStack.pop();                            // return -1 --> Stack is empty return -1.

Constraints:
1 <= maxSize <= 1000
1 <= x <= 1000
1 <= k <= 1000
0 <= val <= 100
At most 1000 calls will be made to each method of increment, push and pop each separately.
*/
void Main()
{
	// I implemented this by using the build in Stack which made it easier, but
	// I believe slower to execute.
	var cs = new CustomStack(3);
	cs.Push(1);
	cs.Push(2);
	cs.Pop();
	cs.Push(2);
	cs.Push(3);
	cs.Push(4);
	cs.Increment(5, 100);
	cs.Increment(2, 100);

	Console.WriteLine(cs.Pop());
	Console.WriteLine(cs.Pop());
	Console.WriteLine(cs.Pop());

	Console.WriteLine("-------------------------------------");
	
	// CustomStack2 is signifigantly faster by using an internal array to store stack values
	// Increment is faster b/c it doesn't have to pop everything off the stack to increment 
	// the bottom values.
	// this implementation wasn't really much harder and was easier in some cases.
	var cs2 = new CustomStack2(3);
	cs2.Push(1);
	cs2.Push(2);
	cs2.Pop();
	cs2.Push(2);
	cs2.Push(3);
	cs2.Push(4);
	cs2.Increment(5, 100);
	cs2.Increment(2, 100);

	Console.WriteLine(cs2.Pop());
	Console.WriteLine(cs2.Pop());
	Console.WriteLine(cs2.Pop());
}

public class CustomStack
{
	readonly int maxSize;
	Stack<int> stack = new Stack<int>();

	public CustomStack(int maxSize)
	{
		this.maxSize = maxSize;
	}

	public void Push(int x)
	{
		if (this.stack.Count < this.maxSize)
		{
			this.stack.Push(x);
		}
	}

	public int Pop()
	{
		if (this.stack.Count > 0)
		{
			return this.stack.Pop();
		}
		else
		{
			return -1;
		}
	}

	public void Increment(int k, int val)
	{
		try
		{
			int z = Math.Min(k, this.stack.Count);
			Stack<int> temp = new Stack<int>();

			Console.WriteLine($"stack.Count={stack.Count} z={z} k={k}");

			while (this.stack.Count > 0)
			{
				temp.Push(this.stack.Pop());
			}

			Console.WriteLine($"temp.Count={temp.Count}");
			for (int i = 0; i < z; i++)
			{
				this.stack.Push(temp.Pop() + val);
			}

			while (temp.Count > 0)
			{
				this.stack.Push(temp.Pop());
			}
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
		}
	}
}

public class CustomStack2
{
	readonly int maxSize;
	int[] stack;
	int count = 0;

	public CustomStack2(int maxSize)
	{
		this.maxSize = maxSize;
		this.stack = new int[maxSize];
	}

	public void Push(int x)
	{
		if (this.count < this.maxSize)
		{
			this.stack[count] = x;
			this.count++;
		}
	}

	public int Pop()
	{
		int rc = -1;
		if (this.count > 0)
		{
			this.count--;
			rc = this.stack[count];
		}
		return rc;
	}

	public void Increment(int k, int val)
	{
		int z = Math.Min(k, this.count);
		for (int i = 0; i < z; i++)
		{
			this.stack[i]+=val;
		}
	}
}

