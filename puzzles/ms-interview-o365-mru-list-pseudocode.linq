<Query Kind="Statements" />

/*
LRUCache cache = new LRUCache( 2 /* capacity */ );
cache.put(1, 1);
cache.put(2, 2);
cache.get(1);       // returns 1
cache.put(3, 3);    // evicts key 2
cache.get(2);       // returns -1 (not found)
cache.put(4, 4);    // evicts key 1
cache.get(1);       // returns -1 (not found)
cache.get(3);       // returns 3
cache.get(4);       // returns 4

*/
public class MostRecentlyUsed
{
	//todo: how do we want to track last used time? Datetime? epoc time? seconds?
	//public DateTime LastUsedTime { get; set; }

	// using epoc b/c it's easy to compare in heap
	public int EpocLastUsedTime { get; set; }
	public int Key { get; set; }
	public int Item { get; set; }
}

//var mruh = new MinHeap<MostRecentlyUsed>((l,r) => return int.Compare(l.EpocLastUsedTime, r.EpocLastUsedTime));
//var items = List<MostRecentlyUsed>();

dict = Dictionary<int, MostRecentlyUsed>
list = List<MostRecentlyUsed>

//O(logn)
public add(int key, int Item)
{
	var n = new MostRecentlyUsed(key, Item);
	List.Add(n);
	dict.Add(n.Key, n);
	
	// evict excess items
	if(List.Count() > max)
	{
		
	}
}

pubic MostRecentlyUsed get(int key)
{
	var rc = dict[key];
	
	// move rc to top of list
	
	return rc;
}

/* After interview notes:

design an MRU (most recently used) list data structure and algorithm. Key and Value are both ints.

i started with a Min Heap and a structure to hold the Key and Value which I called Item. I tracked last used time as epoc datetime.
this has a big O(logn) for the heap

after this he asked me to improve to O(1) or something.

took me a while and some hints but I came around to this algorithm.

var dict = Dictionary<int, MostRecentlyUsed>(); where the key is MostRecentlyUsed.Key
and
DoublyLinkedList<MostRecentlyUsed> list; but you have to keep the items in the list sorted by last used time or at least always in last used order.

so for add operations, add to dict and list but it would go to the top of the list. you would use the properties of the linked list to remove the oldest item.

for get() you would use the dict to find the item then use its linked list properties parent and next to remove from the ll and add it back as top.

*/