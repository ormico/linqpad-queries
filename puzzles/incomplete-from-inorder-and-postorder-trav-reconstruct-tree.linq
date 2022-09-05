<Query Kind="Program" />

/*
given the inorder and postOrder traversal for a tree, reconstruct the tree

inorder[] = { 1, 2, 3, 5, 6, 7 };
postorder[] = { 1, 3, 2, 7, 6, 5 }

see here for help:
https://www.geeksforgeeks.org/construct-tree-from-given-inorder-and-preorder-traversal/
*/

void Main()
{
	//inorder(root, n => Console.WriteLine(n.value));

	int[] inorder = { 1, 2, 3, 5, 6, 7 };
	TreeNode current = new TreeNode { value = inorder[0] };
	string state = "parent";
	
	for (int i = 1; i < inorder.Length; i++)
	{
		if(state == "left")
		{
			
		}
		else if(state=="parent")
		{
			TreeNode n = new TreeNode { value = inorder[i], left = current };
			current = n;
			state = "right";
		}
		else // state == "right"
		{
			
		}
	}

}

TreeNode CreateInorderTree(TreeNode r, int[] v)
{
	for (int i = 1; i < inorder.Length; i++)
	{

	}

}


class TreeNode
{
	public TreeNode left;
	public TreeNode right;
	public int value;
}

void dfs(TreeNode n, Action<TreeNode> f)
{
	if(n == null)
		return;
		
	f(n);

	dfs(n.left, f);
	dfs(n.right, f);
}

void inorder(TreeNode n, Action<TreeNode> f)
{
	if (n == null)
		return;
		
	dfs(n.left, f);

	f(n);

	dfs(n.right, f);
}
