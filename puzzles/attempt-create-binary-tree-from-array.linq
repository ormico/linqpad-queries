<Query Kind="Program" />

void Main()
{
}

namespace Solution
{
	public class Solution
	{
		public static void Main(string[] args)
		{
			// you can write to stdout for debugging purposes, e.g.
			Console.WriteLine("This is a debug message");
		}

		public Node CreateTree(int[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values cannot be null")

			}

			if (values.Count <= 0)
			{
				throw new ArgumentException("values must have ")

			}

			Node root = new Node();
			//List<Node> parents = new List<Node>;
			Stack<Node> parents = new Stack<Node>();
			root.value = values[0];
			Node current = root;
			for (int i = 1; i < value.Count; i++)
			{
				int currentValue = values[i];

				Node c =

				try
				{

				}
				catch (Exception e)
				{

				}

				if (parent != null && curentValue > parent.value)
				{
					current = parent;
				}

				if (currentValue > current.value)
				{
					current.right = new Node
					{
						value = currentValue;
				};
				//parent = current;
				parents.Push(current);
				current = current.right;
			}

				else
			{
				current.left = new Node
				{
					value = currentValue;
			};
			//parent = current;
			parents.Push(current);
			current = current.left;
		}
	}
            
            return root;
        }
    }
    
    private class Node
{
	int value;
	Node left;
	Node right;
}
}
