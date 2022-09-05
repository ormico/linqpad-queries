<Query Kind="Program" />

/*
Given a binary tree root, a node X in the tree is named good if in the path from root to X there are no nodes with a value greater than X.

Return the number of good nodes in the binary tree.

Note: this solution keeps a list of good nodes but only returns the count. 
puzzle only asks for count, so you could improve by getting rid of list 
and just keeping count instead.
*/
void Main()
{
	var sol = new Solution();
	var answer = sol.GoodNodes(CreateTestTrees1());
	Console.WriteLine($"answer == {answer} is {answer == 4}");
}

public class TreeNode {
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val=0, TreeNode left=null, TreeNode right=null)
	{
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

public class Solution
{
	Stack<int> stack = new Stack<int>();
	
	public int GoodNodes(TreeNode root)
	{
		if(root == null)
			return 0;
			
		List<TreeNode> rc = new List<TreeNode>();
		stack.Push(root.val);
		dfs(root, n =>
		{
			//Console.WriteLine(n.val);
			int max = stack.Peek();
			if(n.val >= max)
			{
				rc.Add(n);
				max = n.val;
			}
			stack.Push(max);
		});
		
		return rc.Count;
	}
	
	void dfs(TreeNode n, Action<TreeNode> f)
	{
		if(n != null)
		{		
			f(n);
			
			dfs(n.left, f);
			dfs(n.right, f);
			stack.Pop();
		}
	}
}

TreeNode CreateTestTrees1()
{
	TreeNode t1 = new TreeNode
	{
		left = new TreeNode
		{
			left = new TreeNode
			{
				val = 3
			},
			val = 1
		},
		right = new TreeNode
		{
			right = new TreeNode
			{
				val = 5
			},
			left = new TreeNode
			{
				val = 1
			},
			val = 4
		},
		val = 3
	};

	return t1;
}