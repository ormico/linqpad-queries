<Query Kind="Statements" />

/*
compare with mru-list
I did this one after mru-list. I think this one is most correct
but I like the record stuff in mru-list better.
*/
public class LRUCache
{
	readonly int capacity;
	LinkedList<LruItem> items = new LinkedList<LruItem>();
	Dictionary<int, LinkedListNode<LruItem>> index = new Dictionary<int, LinkedListNode<LruItem>>();

	struct LruItem
	{
		public LruItem(int key, int value)
		{
			this.key = key;
			this.value = value;
		}

		public int key { get; private set; }
		public int value { get; set; }
	}

	public LRUCache(int capacity)
	{
		this.capacity = capacity;
        Console.WriteLine($"capacity {capacity}");
	}

	public int Get(int key)
	{
		int rc = -1;
		if (this.index.ContainsKey(key))
		{
			rc = this.index[key].Value.value;

			// move item to top
			var llitem = this.index[key];
			this.items.Remove(llitem);
			this.items.AddFirst(llitem);
            Console.WriteLine("found");
		}
        else
            Console.WriteLine("not-found");
        
		return rc;
	}

	public void Put(int key, int value)
	{
        if(this.index.ContainsKey(key))
        {
			// move item to top
			var llitem = this.index[key];
			var it = llitem.Value;
			this.index.Remove(it.key);			
			this.items.Remove(llitem);
			it.value = value;
			llitem = this.items.AddFirst(it);
			this.index.Add(it.key, llitem);
		}
		else
		{
			if (this.items.Count >= this.capacity)
			{
				var last = this.items.Last;
				Console.WriteLine($"removing {last}");
				this.index.Remove(last.Value.key);
				this.items.RemoveLast();
			}

			var newItem = new LruItem(key, value);
			var llitem = this.items.AddFirst(newItem);
			this.index.Add(key, llitem);
		}
	}
}