<Query Kind="Program" />

/*
Puzzle: Print all boundary edges.
(I've also seen "Print all boundary edges in counter clockwise order. This algorithm can do that with minor or no changes).
https://www.geeksforgeeks.org/boundary-traversal-of-binary-tree/

boundary nodes are left boundary, right boundary, root, and leaves.
left boundary is all nodes on path from root to left most node (no backtracking). Always go left if you can, right if you can't until you can't go further.
right boundary is same except reverse left and right.

in the above example 3, the left boundary is (8, 7, 4, 20, 21) and the right boundary is (8, 10, 11).
note that 12 and 13 are not in the right or left boundary. You don't back up from 11 to go down through 9 to 12 and 13.
12 and 13 are boundary nodes b/c they are leaf nodes.

this is a much easier problem than what I was trying to solve.

Example 1
               8
			  /\
			 /  \
			/    \
		   7      10
		  /      /\
		 4      9  11
		       / \
			  12 13
Boundary edges: (8, 7, 4, 12, 13, 11, 10)
everything except 9

Example 2
              10
			  /\
			 /  \
			/    \
		   5      15
		  /\      /\
		 3  7    13 17
		          \
				   14
Boundary edges: (10, 5, 3, 7, 14, 17, 15)

Example 3
               8
			  /\
			 /  \
			/    \
		   7      10
		  /      /\
		 4      9  11
		  \    / \
		   20 12 13
		   /
		  21
Boundary edges: (8, 7, 4, 20, 21, 12, 13, 11, 10)


I should try to solve the harder version (left most and right most node at each level + root and leaf nodes). This seams like it shouldn't be hard but 
I keep getting stuck.

todo: code a min heap, max heap (generic)
*/
void Main()
{
	// create a binary tree
	Node binaryTree = CreateNode(8,
		CreateNode(7, CreateNode(4, Right: CreateNode(20, CreateNode(21)))),
		CreateNode(10, CreateNode(9, CreateNode(12), CreateNode(13)), CreateNode(11)
		));

	Console.WriteLine("--BFS#2-----------------------------------------------------------------");

	// LeftBoundary() and RightBoundary() skip leaf nodes b/c they are reported
	// by LeafNodes()
	// Root gets reported twice but should only be reported once.
	// One way to prevent printing root twice is to print root first,
	// then call LeftBoundary(binaryTree?.Left, ...); and RightBoundary(binaryTree?.Right, ...);
	LeftBoundary(binaryTree, (n) =>
	{
		Console.WriteLine($"Left Boundary: {n?.Value}");
	});

	LeafNodes(binaryTree, (n) =>
	{
		Console.WriteLine($"Leaf Nodes: {n?.Value}");
	});

	RightBoundary(binaryTree, (n) =>
	{
		Console.WriteLine($"Right Boundary: {n?.Value}");
	});
}

// given
public class Node
{
	public int Value { get; set; }
	public Node Left { get; set; }
	public Node Right { get; set; }
}

// my code

// Helper code
Node CreateNode(int Value, Node Left = null, Node Right = null)
{
	return new Node { Value = Value, Left = Left, Right = Right };
}

void LeftBoundary(Node root, Action<Node> visit)
{
	if(root != null)
	{
		if(root.Left != null)
		{
			visit(root);
			LeftBoundary(root.Left, visit);
		}
		else if(root.Right != null)
		{
			visit(root);
			LeftBoundary(root.Right, visit);
		}
	}
}

void RightBoundary(Node root, Action<Node> visit)
{
	if(root != null)
	{
		if(root.Right != null)
		{
			// if you are doing something like boundary nodes counter-clockwise, then
			// do right before calling visit
			visit(root);
			RightBoundary(root.Right, visit);
		}
		else if(root.Left != null)
		{
			visit(root);
			RightBoundary(root.Left, visit);
		}
	}
}

void LeafNodes(Node root, Action<Node> visit)
{
	// do depth first search but only call visit if there are no children
	if(root != null)
	{
		if(root.Left != null)
		{
			LeafNodes(root.Left, visit);
		}
		
		if(root.Right != null)
		{
			LeafNodes(root.Right, visit);
		}
		
		if(root.Right == null && root.Left == null)
		{
			visit(root);
		}
	}
}
