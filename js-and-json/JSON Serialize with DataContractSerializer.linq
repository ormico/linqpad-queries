<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Namespace>System.Runtime.Serialization.Json</Namespace>
</Query>

void Main()
{
	Alpha a = new Alpha();
	a.Id = "alpha1";
	a.Name = "Bob Smith";
	a.CreateDt = new DateTime(2000, 2, 15);
	a.Value = 101.09M;
	a.Betas.Add(new Beta() { Id="beta1", Name="x-men" });
	a.Betas.Add(new Beta() { Id="beta2", Name="super-man" });
	a.Betas.Add(new Beta() { Id="beta3", Name="spider-man" });
	a.Betas.Add(new Beta() { Id="beta4", Name="iron man" });
	a.Betas.Add(new Beta() { Id="beta5", Name="wonder woman" });
	
	DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Alphas), new Type[] { typeof(Beta), typeof(Alpha) });
	MemoryStream ms = new MemoryStream();
	//ser.WriteObject(ms, new Alphas() { a, a });
	ser.WriteObject(ms, a);
	
	Console.WriteLine("------------------------------------------------------------------");
	ms.Position = 0;
	using(StreamReader sr = new StreamReader(ms))
	{
		Console.WriteLine(sr.ReadToEnd());

		ms.Position = 0;
		var o = ser.ReadObject(ms);
		o.Dump();
	}
	Console.WriteLine("------------------------------------------------------------------");

	// I wanted to see what happened if I left out a nullable property so the code below DeSerializes
	// a json string that removes the ID from one of the Betas
	
	//string json = @"{""__type"":""UserQuery.Alpha:#"",""Betas"":[{""Id"":""beta1"",""Name"":""x-men""},{""Id"":""beta2"",""Name"":""super-man""},{""Id"":""beta3"",""Name"":""spider-man""},{""Id"":""beta4"",""Name"":""iron man""},{""Id"":""beta5"",""Name"":""wonder woman""}],""CreateDt"":""\/Date(950590800000-0500)\/"",""Id"":""alpha1"",""Name"":""Bob Smith"",""Value"":101.09}";
	string json = @"{""__type"":""UserQuery.Alpha:#"",""Betas"":[{""Name"":""x-men""},{""Id"":""beta2"",""Name"":""super-man""},{""Id"":""beta3"",""Name"":""spider-man""},{""Id"":""beta4"",""Name"":""iron man""},{""Id"":""beta5"",""Name"":""wonder woman""}],""CreateDt"":""\/Date(950590800000-0500)\/"",""Id"":""alpha1"",""Name"":""Bob Smith"",""Value"":101.09}";
	
	using(ms = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(json)))
	{
		ms.Position = 0;
		var o = ser.ReadObject(ms);
		o.Dump();
	}
}

public class Alphas: List<Alpha>
{
}

// Define other methods and classes here
public class Alpha
{
	public Alpha()
	{
		this.Betas = new List<Beta>();
	}
	
	public string Id {get; set;}
	public string Name {get; set;}
	public DateTime CreateDt {get; set;}
	public decimal? Value {get; set;}
	public List<Beta> Betas {get; set;}
}

public class Beta
{
	public string Id {get; set;}
	public string Name {get; set;}
}