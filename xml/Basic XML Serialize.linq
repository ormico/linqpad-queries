<Query Kind="Program">
  <Namespace>System.Xml</Namespace>
  <Namespace>System.Xml.Serialization</Namespace>
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
	
	XmlSerializer x = new XmlSerializer(typeof(Alphas), new Type[] { typeof(Beta), typeof(Alpha) });
	XmlWriterSettings settings = new XmlWriterSettings();
	settings.Indent = true;
	settings.Encoding = System.Text.ASCIIEncoding.ASCII;
	
	// **
	//settings.ConformanceLevel = ConformanceLevel.Auto;
	// **
	
	// do not print namespaces for xsi and xsd
	XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
	ns.Add("", "");
	
	MemoryStream ms = new MemoryStream();
	using (XmlWriter writer = XmlWriter.Create(ms, settings))
	{
		x.Serialize(writer, new Alphas() { a, a });
		//x.Serialize(writer, a);
	}
	
	Console.WriteLine("------------------------------------------------------------------");
	ms.Position = 0;
	using(StreamReader sr = new StreamReader(ms))
	{
		Console.WriteLine(sr.ReadToEnd());
	}
	Console.WriteLine("------------------------------------------------------------------");
}

[XmlRoot(ElementName="Alphas")]
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