<Query Kind="Statements">
  <RuntimeVersion>5.0</RuntimeVersion>
</Query>

using System.Text.Json;

var alpha = new Alpha()
{
	Id = "test",
	Name = "Alpha1",
	CreateDt = DateTime.Now,
	Value = 100000.0001m,
	Betas = new List<Beta>()
	{
		new Beta { Id = "1", Name = "one" },
		new Beta { Id = "2", Name = "two" },
		new Beta { Id = "3", Name = "three" },
		new Beta { Id = "4", Name = "four" }
	}
};
alpha.Dump();

string jsonString = JsonSerializer.Serialize(alpha);
jsonString.Dump();

var b = JsonSerializer.Deserialize(jsonString, typeof(Alpha));
b.Dump();

JsonDocument jdoc = JsonDocument.Parse(jsonString);
//jdoc.Dump();
var betas = jdoc.RootElement.GetProperty("Betas");
foreach(var i in betas.EnumerateArray())
{
	Console.WriteLine("---------");
	foreach(var p in i.EnumerateObject())
	{
		Console.WriteLine($"\t{p.Name}: {p.Value}");
	}
	Console.WriteLine();
}

// Define other methods and classes here
public class Alpha
{
	public Alpha()
	{
		this.Betas = new List<Beta>();
	}

	public string Id { get; set; }
	public string Name { get; set; }
	public DateTime CreateDt { get; set; }
	public decimal? Value { get; set; }
	public List<Beta> Betas { get; set; }
}

public class Beta
{
	public string Id { get; set; }
	public string Name { get; set; }
}
