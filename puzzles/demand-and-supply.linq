<Query Kind="Statements" />

/*
List [[Itemid, date, qty]] Demand - 30 days of demand

List [[Itemid, date, qty]] Supply - 30 days of supply

----------------------------------------------------------- 

Absolutely no ordering in either one of the lists.

ItemId + Date is a unique key

Multiple items, multiple days, maybe no entries per item per date.

List [Itemid, date, qty] deficit - Consists of, only what I am missing from my demand.

30 days of timeframe.

[Item1, 1, 5], [Item1, 2, 3], [Item2, 1, 2] demand

[Item1, 1, 3], [item2, 3, 1] supply

// if there is no deficit, don't return an etry for that item for that day

write a function to return the items for each day where there is a deficit using format [item, date, deficit]
*/


class DailyInventory
{
	public string ItemId { get; set; }
	public uint DayOfMonth { get; set; }
	public uint Quantity { get; set; }
}

/// <summary>
/// this is a good solution but doesn't handle days where there is no supply record
/// </summary>
List<DailyInventory> F(List<DailyInventory> demand, List<DailyInventory> supply)
{
	List<DailyInventory> rc = new List<DailyInventory>();
	Dictionary<string, DailyInventory> demandDict = new Dictionary<string, DailyInventory>();
	// $"{ItemId}/{DayOfMonth}"
	
	foreach(var i in demand)
	{
		demandDict.Add($"{i.ItemId}/{i.DayOfMonth}", i);
	}
	
	foreach(var i in supply)
	{
		string key = $"{i.ItemId}/{i.DayOfMonth}";
		if(demandDict.ContainsKey(key))
		{
			var d = demandDict[key];
			var def = d.Quantity - i.Quantity;
			if(def < 0)
			{
				var n = new DailyInventory
				{
					ItemId = i.ItemId,
					DayOfMonth = i.DayOfMonth,
					Quantity = def
				}
				rc.Add(n);
			}
		}		
	}
	
	return rc;
}