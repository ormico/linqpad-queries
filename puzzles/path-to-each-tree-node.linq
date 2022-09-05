<Query Kind="Program" />

/*
print path of every node in tree.
Exampel1: 
    5
   / \
  3   7
 / \   \
2   4   9

print:
5
5,3
5,3,2
5,3,4
5,7
5,7,9
*/
void Main()
{
	//note: if all you want to do is PRINT the answers
	// you don't need to keep the answer List. you can just write it
	// out from the dfs function
	List<List<int>> answer = new List<List<int>>();
	var root = CreateExample1();
	//Stack<int> stack = new Stack<int>();
	List<int> stack = new List<int>();
	
	dfs(root, n =>
	{
		//todo: is there a better way to do this than stack and Reverse();
		// could use a List<int> instead of stack with Remove() and Add() instead
		// of Push() and Pop() and then could iterate over List<int>
		// instead of calling Reverse
		//stack.Push(n.value);
		//var a = new List<int>(stack.Reverse());
		
		stack.Add(n.value);
		var a = new List<int>(stack);
		answer.Add(a);
	}, () =>
	{
		//stack.Pop();
		stack.RemoveAt(stack.Count - 1);
	});	
	
	for(int i = 0; i < answer.Count; i++)
	{
		var path = answer[i];
		for(int j=0; j < path.Count; j++)
		{
			Console.Write($"{path[j]},");
		}
		Console.WriteLine();
	}
}

void dfs(TreeNode n, Action<TreeNode> fnode, Action fend)
{
	if(n == null)
	{
		return;
	}
	
	fnode(n);
	
	dfs(n.left, fnode, fend);
	dfs(n.right, fnode, fend);
	fend();
}

//---------------------------------------

class TreeNode
{
	public TreeNode left;
	public TreeNode right;
	public int value;
}

/*
    5
   / \
  3   7
 / \   \
2   4   9
*/
TreeNode CreateExample1()
{
	TreeNode t1 = new TreeNode
	{
		left = new TreeNode
		{
			left = new TreeNode
			{
				value = 2
			},
			right = new TreeNode
			{
				value = 4
			},
			value = 3
		},
		right = new TreeNode
		{
			right = new TreeNode
			{
				value = 9
			},
			value = 7
		},
		value = 5
	};

	return t1;
}