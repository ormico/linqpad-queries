<Query Kind="Program" />

/*
              10
			  /\
			 /  \
			/    \
		   5      15
		  /\      /\
		 3  7    13 17
		          \
				   14
Depth First Search order (in order) should be:
10, 5, 3, 7, 15, 13, 14, 17

Breadth First Search should be (i think):
10, 5, 15, 3, 7, 13, 17, 14

todo: code a min heap, max heap (generic)
*/
void Main()
{
	// create a binary tree
	Node binaryTree = CreateNode(10, 
		CreateNode(5, CreateNode(3), CreateNode(7)), 
		CreateNode(15, CreateNode(13, Right:CreateNode(14)), CreateNode(17)
		));

	Console.WriteLine("--DFS#1-----------------------------------------------------------------");
	// perform a depth first search and add to list all nodes
	List<int> values = new List<int>();
	DFS(binaryTree, (n) => { if(n != null) values.Add(n.Value); });
	
	foreach(var v in values)
	{
		Console.WriteLine(v);
	}

	Console.WriteLine("--DFS#2-----------------------------------------------------------------");

	// i did the above intentionally with the extra step of looping over the list
	// to demonstrate what is happening more verbosely. 
	// to just print the list I could have done it like this
	DFS(binaryTree, (n) => { if (n != null) Console.WriteLine(n.Value); });


	Console.WriteLine("--DFS#3-----------------------------------------------------------------");
	// 14 and 17 don't print b/c search stops when it finds 13
	bool found = DFS(binaryTree, (n) =>
	{
		bool rc = false;
		if (n != null)
		{
			Console.WriteLine(n.Value);
			if(n.Value == 13)
			{
				rc = true;
			}
		}
		return rc;
	});
	Console.WriteLine("Found = {0}", found);

	Console.WriteLine("--BFS#1-----------------------------------------------------------------");
	BFS(binaryTree, (n) => { if (n != null) Console.WriteLine(n.Value); });

	Console.WriteLine("--BFS#2-----------------------------------------------------------------");
	found = BFS(binaryTree, (n) =>
	{
		bool rc = false;
		if (n != null)
		{
			Console.WriteLine(n.Value);
			if(n?.Value == 13)
			{
				rc = true;
			}
		}
		return rc;
	});
	Console.WriteLine("Found = {0}", found);
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
void DFS(Node node, Action<Node> visit)
{
	if(node != null)
	{
		visit(node);
		DFS(node.Left, visit);
		DFS(node.Right, visit);
	}
}

/// <summary>
/// visit should return true if found, false if not found
/// </summary>
/// <return>true if found, false if not found</return>
bool DFS(Node node, Func<Node, bool> visit)
{
	bool rc = false;
	
	if (node != null)
	{
		rc = visit(node);
		
		if(rc == false)
		{
			rc = DFS(node.Left, visit);
		}
		
		if(rc == false)
		{
			rc = DFS(node.Right, visit);
		}
	}
	return rc;
}

/// <summary>Breadth First Search</summary>
void BFS(Node tree, Action<Node> visit)
{
	//bfs uses a queue instead of recursion
	var nodes = new Queue<Node>();
	//if the Node doesn't have property to use to mark visited
	//the you can use a HashSet to track visited nodes
	var visited = new HashSet<Node>();
	nodes.Enqueue(tree);
	
	// loop while stack is not empty 
	// set nodes as visited
	while(nodes.Count > 0)
	{
		Node c = nodes.Dequeue();
		if(!visited.Contains(c))
		{
			visit(c);
			visited.Add(c);
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

/// <summary>
/// Breadth First Search, stop when target node found
/// this is single directin BFS
/// </summary>
bool BFS(Node tree, Func<Node, bool> visit)
{
	bool rc = false;
	//dfs uses a queue instead of recursion
	var nodes = new Queue<Node>();
	//if the Node doesn't have property to use to mark visited
	//the you can use a Dictionary to mark visited state
	var visited = new Dictionary<Node, bool>();
	nodes.Enqueue(tree);

	// loop while stack is not empty 
	// set nodes as visited
	while (rc == false && nodes.Count > 0)
	{
		Node c = nodes.Dequeue();
		if (!visited.ContainsKey(c))
		{
			rc = visit(c);
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
	return rc;
}