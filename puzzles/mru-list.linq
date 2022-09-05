<Query Kind="Statements" />

/*
create a most recently used list with capacity
most recently used item should be on top
when user gets an item from list it should be moved to top
when a new item is added it should be added to top
when list is at capacity and a new item is added the oldest item
should be removed first.

implemented using a linked list to easily add and remove items and move items to the top
and a Dictionary (hash table) to act as an index for random lookup by key
*/
var mru = new MruList();
mru.Add("alpha", "one");
mru.Add("beta", "two");
mru.Add("gamma", "three");
mru.Add("delta", "four");
mru.Add("epsilon", "five");
mru.Add("zeta", "six");
// zeta/six should be top

_ = mru.Get("epsilon");
_ = mru.Get("zeta");
// zeta/six should be top again with epsilon next

mru.Add("eta", "seven");
mru.Add("theta", "eight");
mru.Add("iota", "nine");
// iota/nine should be top

_ = mru.Get("eta");
_ = mru.Get("beta");
_ = mru.Get("alpha");
// alpha/one is top

mru.Add("kappa", "ten");
mru.Add("labda", "eleven");
mru.Add("mu", "twelve");
// mu/twelve is top

var first = "mu";

for(int i = 0; i < 1000; i++)
{
	var n = i.ToString();
	mru.Add(n, n);
	mru.Get(first);
}

Console.WriteLine("======================================");

foreach(var i in mru.GetList())
{
	Console.WriteLine(i);
}

public class MruList
{
	readonly int capacity;
	LinkedList<MruItem> items = new LinkedList<MruItem>();
	Dictionary<string, LinkedListNode<MruItem>> index = new Dictionary<string, LinkedListNode<MruItem>>();

	record MruItem(string key, string value);

	public MruList(int capacity = 10)
	{
		this.capacity = capacity;
	}
	
	public string Get(string key)
	{
		string rc = null;
		if(this.index.ContainsKey(key))
		{
			rc = this.index[key].Value.value;
			
			// move item to top
			var llitem = this.index[key];
			this.items.Remove(llitem);
			this.items.AddFirst(llitem);			
		}
		return rc;
	}
	
	public string[] GetList()
	{
		return this.items.Select(i => i.value).ToArray();
	}
	
	public string GetTop()
	{
		var first = this.items.First;
		var firstvalue = first.Value;
		var firstvaluevalue = firstvalue.value;
		return firstvaluevalue;
	}
	
	public void Add(string key, string value)
	{
		if (this.index.ContainsKey(key))
		{
			throw new Exception("key already in list");
		}
		else
		{
			if(this.items.Count >= this.capacity && this.capacity > 1)
			{
				var last = this.items.Last;
				this.index.Remove(last.Value.key);
				this.items.RemoveLast();
			}
			
			var newItem = new MruItem(key, value);
			var llitem = this.items.AddFirst(newItem);
			this.index.Add(key, llitem);
		}
	}	
}