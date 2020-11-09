<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	byte[] bytes = Encoding.Unicode.GetBytes("abcd");
	SHA256Managed hashstring = new SHA256Managed();
	byte[] hash = hashstring.ComputeHash(bytes);
	//hash.Dump(true);
	Console.WriteLine(F(hash));

	Alpha a = new Alpha()
	{
		id = 100,
		name = "Omega 1",
		updatedt = DateTime.Parse("01/10/2000"),
		value = 333.7
	};
	
	using(MemoryStream ms = new MemoryStream())
	using(BinaryWriter bw = new BinaryWriter(ms))
	{
		bw.Write(a.id);
		bw.Write(a.name);
		bw.Write(a.updatedt.ToBinary());
		bw.Write(a.value);
		hash = hashstring.ComputeHash(ms);
	}
	
	Console.WriteLine(F(hash));
}

string F(byte[] d)
{
	StringBuilder rc = new StringBuilder();
	
	foreach (byte x in d)
	{
		rc.AppendFormat("{0:x2}", x);
	}
	
	return rc.ToString();
}

class Alpha
{
	public int id { get; set; }
	public string name {get; set;}
	public DateTime updatedt {get; set;}
	public double value {get; set;}
}