<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
</Query>

void Main()
{
	Alpha a = new Alpha();
	a.Id = "alpha1";
	a.Name = "Bob 'Joker' Smith";
	a.CreateDt = new DateTime(2000, 2, 15);
	a.Value = 101.09M;
	a.Betas.Add(new Beta() { Id="beta1", Name="x-men" });
	a.Betas.Add(new Beta() { Id="beta2", Name="super-man" });
	a.Betas.Add(new Beta() { Id="beta3", Name="spider-man" });
	a.Betas.Add(new Beta() { Id="beta4", Name="iron man" });
	a.Betas.Add(new Beta() { Id="beta5", Name="wonder woman" });

	// serialize a but exclude CreateDt
	string json = JsonConvert.SerializeObject(a, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { ContractResolver = ShouldSerializeContractResolver.Instance });
	
	string json2 = JsonConvert.SerializeObject(new List<Alpha>() { a, a });
	Console.WriteLine("------------------------------------------------------------------");
	Console.WriteLine(json);
	Console.WriteLine("------------------------------------------------------------------");
	Console.WriteLine(json2);
	Console.WriteLine("------------------------------------------------------------------");
	
	// using anonymous and dynamic
	//http://stackoverflow.com/questions/4535840/deserialize-json-object-into-dynamic-object-using-json-net
	string json3 = JsonConvert.SerializeObject(new { name="bob", age=33, value=99 });
	json3.Dump();
	
	
	dynamic obj3 = JsonConvert.DeserializeObject(json3);
	Console.WriteLine("{0} {1} {2}", obj3.name,  obj3.age,  obj3.value);
	try
	{
		Console.WriteLine("Value2: '{0}'", obj3.value2);
	}
	catch(Exception ex)
	{
		ex.Dump();
	}
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

public class ShouldSerializeContractResolver : DefaultContractResolver
{
	public static readonly ShouldSerializeContractResolver Instance = new ShouldSerializeContractResolver();

	protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
	{
		JsonProperty property = base.CreateProperty(member, memberSerialization);

		if (property.DeclaringType == typeof(Alpha) && property.PropertyName == "CreateDt")
		{
			property.ShouldSerialize =
				instance =>
				{
					Alpha a = (Alpha)instance;
					return false;
				};
		}

		return property;
	}
	/* OR like this
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);

        // only serializer properties that start with the specified character
        properties = properties.Where(p => p.PropertyName != "CreateDt").ToList();

        return properties;
	}
	*/
}