<Query Kind="Statements" />

var d = new List<MyData>()
{
	new MyData { Alpha = "bob", Beta = 99},
	new MyData { Alpha = "bill", Beta = 100},
	new MyData { Alpha = "jill", Beta = 200},
	new MyData { Alpha = "fran", Beta = 99},
	new MyData { Alpha = "zack", Beta = 1},
	new MyData { Alpha = "bob", Beta = 90},
	new MyData { Alpha = "zack", Beta = 2},
	new MyData { Alpha = "Bill", Beta = 99},
	new MyData { Alpha = "chris", Beta = 99},
	new MyData { Alpha = "fred", Beta = 99},
};

d.Sort((l,r) =>
{
	var rc = string.Compare(l.Alpha, r.Alpha);
	if(rc == 0)
	{
		rc = l.Beta.CompareTo(r.Beta);
	}
	return rc;
});

foreach(var i in d)
{
	Console.WriteLine($"{i.Alpha}/{i.Beta}");
}

public class MyData
{
	public string Alpha { get; set; }
	public int Beta { get; set; }
}