<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Extensions.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.Activation.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.ApplicationServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Services.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.Services.Client.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.Services.Design.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Data.Entity.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Design.dll</Reference>
  <Namespace>System.Web.Script.Serialization</Namespace>
</Query>

// http://msdn.microsoft.com/en-us/library/bb412179(v=vs.110).aspx 
// http://stackoverflow.com/questions/13278459/json-serialization-in-c-sharp 
void Main()
{
	var thing = new Thing() { Weight = 10.0 };
	var json = new JavaScriptSerializer().Serialize(thing);
	Console.WriteLine(json);
	
	List<Thing> things = new List<Thing>();
	things.Add(new Thing() { Weight = 11.6 });
	things.Add(new Chair() { Weight = 11.6, NumLegs = 3 });
	things.Add(new Rock() { Weight = 11.6, SquareFeet = 2.1, Color = "Red" });
	things.Add(new Beanbag() { Weight = 11.6, NumLegs = 0, PercentFull = 0.35 });
	
	var json2 = new JavaScriptSerializer().Serialize(things);
	Console.WriteLine(json2);
}

public class Thing
{
	public double Weight { get; set; }
	public string TypeString { get { return this.GetType().ToString(); } }
}

public class Chair: Thing
{
	public int NumLegs { get; set; }
}

public class Rock: Thing
{
	public double SquareFeet { get; set; }
	public string Color { get; set; }
}

public class Beanbag: Chair
{
	public double PercentFull { get; set; }
}
