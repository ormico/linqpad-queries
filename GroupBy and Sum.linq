<Query Kind="Program" />

void Main()
{
	List<X> xList = new List<X>()
	{
		new X() {Id="c",Val=1},
		new X() {Id="b",Val=1},
		new X() {Id="c",Val=1},
		new X() {Id="a",Val=1},
		new X() {Id="a",Val=1}		
	};
	
	var y = 
		from x in xList
		group x by x.Id into g
		orderby g.Key
		select new 
		{
			Id = g.Key,
			ValSum = g.Sum(x => x.Val),
			List = 
			(
				from xl in xList where xl.Id == g.Key select xl
			).ToList(),
			List2 = g.ToList()
		};
	
	y.Dump();
}

public class X
{
	public string Id {get;set;}
	public int Val {get;set;}
}
