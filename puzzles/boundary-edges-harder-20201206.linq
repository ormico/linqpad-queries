<Query Kind="Program" />

/*
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

update:
better definition:
boundary nodes are left boundary, right boundary, root, and leaves.
left boundary is all nodes on path from root to left most node (no backtracking).
right boundary is "

in the above example, the left boundary is (8, 7, 4) and the right boundary is (8, 10, 11).
note that 12 and 13 are not in the right or left boundary. You don't back up from 11 to go down through 9 to 12 and 13.
12 and 13 are boundary nodes b/c they are leaf nodes.

this is a much easier problem than what I was trying to solve.

I should try to solve the harder version (left most and right most node at each level + root and leaf nodes). This seams like it shouldn't be hard but 
I keep getting stuck.

todo: code a min heap, max heap (generic)
*/
void Main()
{
	// create a binary tree
	Node binaryTree = CreateNode(8,
		CreateNode(7, CreateNode(4)),
		CreateNode(10, CreateNode(9, CreateNode(12), CreateNode(13)), CreateNode(11)
		));

	Console.WriteLine("--BFS#2-----------------------------------------------------------------");

	//List<List<NodeInfo>> levels = new List<System.Collections.Generic.List<UserQuery.NodeInfo>>();
	var levels = new Dictionary<int, List<NodeInfo>>();
	
	DFS(binaryTree, 0, null, (n, l, p) =>
	{
		if (n != null)
		{
			Console.WriteLine("value: {0} level: {1}", n.Value, l);
			if (!levels.ContainsKey(l))
			{
				levels.Add(l, new List<NodeInfo>());
			}

			if(p=="r")
			{
				levels[l].Add(new NodeInfo { Node = n, Score = (10 * l + 1) });
			}
			else if(p=="l")
			{
				levels[l].Add(new NodeInfo { Node = n, Score = (10 * l + 2) });
			}
			else
			{
				//root
				levels[l].Add(new NodeInfo { Node = n, Score = 0 });
			}
		}
	});

	Console.WriteLine("-------------------------------------------------------------------");

	for(int i = 0; i < 4; i++)
	{
		var l = levels[i];
		foreach(var n in l)
		{
			Console.WriteLine($"level: {i} Node: {n.Node.Value} Score: {n.Score}");
		}
	}

	Console.WriteLine("--DFS#1-----------------------------------------------------------------");
	BFS(binaryTree, (n) => { if (n != null) Console.WriteLine(n.Value); });
}

class NodeInfo
{
	public Node Node {get; set;}
	public int Score {get; set;}
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

/// <summary>Depth First In-Order Search</summary>
void DFS(Node node, int level, string position, Action<Node, int, string> visit)
{
	if(node != null)
	{
		visit(node, level, position);
		int childLevel = level + 1;
		DFS(node.Left, childLevel, "l", visit);
		DFS(node.Right, childLevel, "r", visit);
	}
}

/// <summary>Breadth First Search</summary>
void BFS(Node tree, Action<Node> visit)
{
	//dfs uses a queue instead of recursion
	var nodes = new Queue<Node>();
	//if the Node doesn't have property to use to mark visited
	//the you can use a Dictionary to mark visited state
	var visited = new Dictionary<Node, bool>();
	nodes.Enqueue(tree);
	
	// loop while stack is not empty 
	// set nodes as visited
	while(nodes.Count > 0)
	{
		Node c = nodes.Dequeue();
		if (!visited.ContainsKey(c))
		{
			visit(c);
			visited.Add(c, true);
		}

		if (c.Left != null)
		{
			nodes.Enqueue(c.Left);
		}

		if (c.Right != null)
		{
			nodes.Enqueue(c.Right);
		}
	}
}