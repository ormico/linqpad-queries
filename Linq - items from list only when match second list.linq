<Query Kind="Program" />

void Main()
{
	var x = new List<a>()
	{
		new a() { name = "alpha" },
		new a() { name = "beta", value = 100 },
		new a() { name = "gamma" },
		new a() { name = "delta", value = 999 },
		new a() { name = "zeta" }	
	};
	var y = new List<a>()
	{
		new a() { name = "alpha", value = 99 },
		new a() { name = "beta", value = 100 },
		new a() { name = "gamma", value = 30 },
		new a() { name = "delta", value = 4 },
		new a() { name = "zeta", value = 25 }	
	};
	
	var list = from f in x
			where y.Any(i => i.name == f.name && i.value == f.value)
			select f;
	
	list.Dump();
}

class a
{
	public string name {get; set;}
	public int value {get; set;}
}

