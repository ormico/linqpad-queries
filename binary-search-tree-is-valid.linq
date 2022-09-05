<Query Kind="Program" />

void Main()
{
	// create a binary tree
	Node binaryTree = CreateNode(10,
		CreateNode(5, CreateNode(3), CreateNode(7)),
		CreateNode(15, CreateNode(13, Right: CreateNode(14)), CreateNode(17)
		));

	// search for node that is not valid BST node
	bool isBinarySearchTree = !DFS(binaryTree, (n) =>
	{
		bool rc = false;
		Console.WriteLine(n.Value);
		if(n.Left != null && n.Value > n.Left.Value)
		{
			rc = true;
		}
		else if (n.Right != null && n.Value < n.Right.Value)
		{
			rc = true;
		}
		return rc;
	});
	Console.WriteLine("Is Binary Search Tree? {0}", isBinarySearchTree);
}

// given
public class Node
{
	public int Value { get; set; }
	public Node Left { get; set; }
	public Node Right { get; set; }
}

// Helper code
Node CreateNode(int Value, Node Left = null, Node Right = null)
{
	return new Node { Value = Value, Left = Left, Right = Right };
}

bool DFS(Node root, Func<Node, bool> visit)
{
	if (root == null)
	{
		return true;
	}

	bool rc = false;
	if(visit(root) || 
		DFS(root.Left, visit) ||
		DFS(root.Right, visit))
	{
		rc = true;
	}
	return rc;
}

void DFS(Node root, Action<Node> visit)
{
	if(root == null)
	{
		return;
	}
	
	visit(root);
	DFS(root.Left, visit);
	DFS(root.Right, visit);
}