<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\SMDiagnostics.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.Internals.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.Runtime.Serialization.Json</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
  <Namespace>System.Dynamic</Namespace>
</Query>

void Main()
{
	string json = @"{
		'Email': 'james@example.com',
		'Active': true,
		'CreatedDate': '2013-01-20T00:00:00Z',		
		'Roles': [
			{ alpha:'blah', beta:'blahblah' },
			'User',
			'Admin'
			]
	}";
	
	Alpha alpha = JsonConvert.DeserializeObject<Alpha>(json);
	Console.WriteLine(alpha.Roles[0].beta);

	List<string> examples = new List<string>() { "alpha", "beta", "gamma", "delta" };
	json = JsonConvert.SerializeObject(examples, Newtonsoft.Json.Formatting.Indented);

	List<dynamic> dlist = new List<dynamic>();
	dynamic d = new ExpandoObject();
	d.Alpha = "blah";
	d.Beta = "beta";
	d.Gamma = "gamma";
	dlist.Add(d);

	d = new ExpandoObject();
	d.Alpha = "blah";
	d.Beta = "beta";
	d.Gamma = "gamma";
	dlist.Add(d);

	d = new ExpandoObject();
	d.Alpha = "blah";
	d.Beta = "beta";
	d.Gamma = "gamma";
	dlist.Add(d);

	d = new ExpandoObject();
	d.Alpha = "blah";
	d.Beta = "beta";
	d.Gamma = "gamma";
	dlist.Add(d);

	json = JsonConvert.SerializeObject(dlist, Newtonsoft.Json.Formatting.Indented);
}

public class Alpha
{
	public string Email { get; set; }
	public bool Active { get; set; }
	public DateTime CreatedDate { get; set; }
	public List<dynamic> Roles { get; set; }
}