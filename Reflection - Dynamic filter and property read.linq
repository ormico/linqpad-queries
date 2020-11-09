<Query Kind="Program" />

void Main()
{
	List<MyClass> data = new List<MyClass>()
	{
		new MyClass() { ID = 1, Name = "Sue", CreatedDt = new DateTime(2016, 1, 1), Alpha = "blah", Beta = null, Gamma = 100.01M, Zeta = 2.123456 },
		new MyClass() { ID = 2, Name = "Adam", CreatedDt = new DateTime(2016, 2, 1), Alpha = "blah", Beta = null, Gamma = 100.01M, Zeta = 2.123456 },
		new MyClass() { ID = 3, Name = "Fred", CreatedDt = new DateTime(2016, 3, 1), Alpha = "blah", Beta = new DateTime(2015, 7, 9), Gamma = 200.01M, Zeta = 2.123456 },
		new MyClass() { ID = 4, Name = "Anne", CreatedDt = new DateTime(2016, 4, 1), Alpha = "blah", Beta = null, Gamma = 100.01M, Zeta = 11.1 },
		new MyClass() { ID = 5, Name = "Jen", CreatedDt = new DateTime(2016, 5, 1), Alpha = "blah", Beta = new DateTime(2015, 5, 3), Gamma = 101.01M, Zeta = 2.123456 },
		new MyClass() { ID = 6, Name = "Angel", CreatedDt = new DateTime(2016, 6, 1), Alpha = "blah", Beta = null, Gamma = 100.01M, Zeta = 7.123456 },
		new MyClass() { ID = 7, Name = "Gary", CreatedDt = new DateTime(2016, 7, 1), Alpha = "blah", Beta = null, Gamma = 100.01M, Zeta = 9.123456 }
	};

	foreach (var v in data)
	{
		Console.WriteLine(GetPropertyValue(v, "Zeta"));
	}

	foreach (var v in data)
	{
		if (SearchPropertyValues(v, "2.12"))
		{
			Console.WriteLine(v.ID);
		}
	}
}

class MyClass
{
	public int ID { get; set; }
	public string Name { get; set; }
	public DateTime CreatedDt { get; set; }
	public string Alpha { get; set; }
	public DateTime? Beta { get; set; }
	public decimal Gamma { get; set; }
	public double Zeta { get; set; }
}

object GetPropertyValue(object o, string propertyName)
{
	return o.GetType().GetProperty(propertyName).GetValue(o);
}

bool SearchPropertyValues(object o, string searchText)
{
	bool rc = false;
	Type t = o.GetType();
	foreach (var p in t.GetProperties())
	{
		//Console.WriteLine(p.Name);
		object v = t.GetProperty(p.Name).GetValue(o);
		
		//todo: Contains isn't case insensitive
		if (v != null && v.ToString().Contains(searchText))
		{
			rc = true;
			break;
		}
	}
	return rc;
}

