<Query Kind="Program" />

/*
given a starting node of a singly linked list
check to see if the list contains any cycles
*/
void Main()
{
	var list = GetList();
	var c = list;
	Dictionary<int, bool> ids = new Dictionary<int, bool>();
	bool cycle = false;
	while(c != null)
	{
		if(ids.ContainsKey(c.id))
		{
			cycle = true;
			break;
		}
		else
		{
			ids.Add(c.id, true);
			c = c.next;
		}
	}
	
	if(cycle == false)
	{
		Console.WriteLine("no cycle");
	}
	else
	{
		Console.WriteLine("there is a cycle");
	}

	//-----------------------------------------------------
	SortedList<int, Node> slids = new SortedList<int, Node>();
	cycle = false;
	while (c != null)
	{
		if (slids.ContainsKey(c.id))
		{
			cycle = true;
			break;
		}
		else
		{
			slids.Add(c.id, c);
			c = c.next;
		}
	}

	if (cycle == false)
	{
		Console.WriteLine("no cycle");
	}
	else
	{
		Console.WriteLine("there is a cycle");
	}
}

// You can define other methods, fields, classes and namespaces here
class Node
{
	public Node(int id, Node next = null)
	{
		this.id = id;
		this.next = next;
	}
	public Node next { get; set; }
	public int id { get; set; }
}

Node GetList()
{
	int sequence = 0;
	Node c;
	Node l;
	Node rc = new Node(sequence++, 
		new Node(sequence++, 
		c = new Node(sequence++, 
		new Node(sequence++,
		l = new Node(sequence++)))));
		
	l.next = c;
	
	return rc;
}