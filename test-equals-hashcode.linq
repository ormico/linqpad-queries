<Query Kind="Program" />

/*
example using dictionary and hashes to do lookup by region + id and 
reverse lookup by region + data
*/
void Main()
{	
	var list = new List<MyClass>()
	{
		new MyClass("alpha", "A100", new byte[] { 1, 4, 200, 34, 70, 23 }),
		new MyClass("alpha", "A101", new byte[] { 2, 4, 200, 30, 70, 23 }),
		new MyClass("alpha", "A102", new byte[] { 3, 4, 200, 39, 70, 23 }),
		new MyClass("alpha", "A103", new byte[] { 4, 4, 200, 36, 70, 23 }),
		new MyClass("alpha", "A104", new byte[] { 5, 4, 200, 34, 70, 23 }),
		new MyClass("alpha", "A105", new byte[] { 6, 4, 200, 37, 70, 23 }),
		new MyClass("alpha", "A106", new byte[] { 7, 4, 200, 32, 70, 23 }),
		new MyClass("alpha", "A107", new byte[] { 8, 4, 200, 33, 70, 23 }),
		new MyClass("alpha", "A108", new byte[] { 9, 4, 200, 39, 70, 23 }),
		new MyClass("beta", "B100", new byte[] { 10, 4, 200, 38, 70, 23 }),
		new MyClass("beta", "B101", new byte[] { 11, 4, 200, 37, 70, 23 }),
		new MyClass("beta", "B102", new byte[] { 12, 4, 200, 34, 70, 23 }),
		new MyClass("beta", "B103", new byte[] { 13, 4, 200, 31, 70, 23 }),
		new MyClass("beta", "B104", new byte[] { 14, 4, 200, 31, 70, 23 }),
		new MyClass("beta", "B105", new byte[] { 15, 4, 200, 32, 70, 23 }),
		new MyClass("beta", "B106", new byte[] { 16, 4, 200, 33, 70, 23 }),
	};
	
	//init with some starting values
	foreach(var i in list)
	{
		var kh = i.GetKeyHasher();
		dict.Add(kh, i);
		tcid.Add(i.GetDataHasher(), kh);
	}
	
	// manually check collections
	Console.WriteLine(dict[MyClass.CreateKeyHasher("alpha", "A107")].ToString());
	Console.WriteLine(tcid[MyClass.CreateDataHasher("beta", new byte[] { 16, 4, 200, 33, 70, 23 })].ToString());

	// use Find() and Add() methods
	Console.WriteLine(Add("beta", new byte[] { 16, 4, 200, 33, 70, 99 }));
	Console.WriteLine(Add("beta", new byte[] { 16, 4, 200, 33, 70, 99 }));
	Console.WriteLine(Add("beta", new byte[] { 16, 4, 200, 33, 70, 101 }));
	Console.WriteLine(Add("zeta", new byte[] { 16, 4, 200, 33, 70, 101 }));
	
	Console.WriteLine(Find("beta", "B103"));
}

Dictionary<MyClass.MyClassKeyHasher, MyClass> dict = new Dictionary<MyClass.MyClassKeyHasher, MyClass>();
Dictionary<MyClass.MyClassDataHasher, MyClass.MyClassKeyHasher> tcid = new Dictionary<MyClass.MyClassDataHasher, MyClass.MyClassKeyHasher>();

// add new values. if value already exist in given region
// then return same id instead of adding again
string Add(string RegionName, byte[] Data)
{
	string rc = null;
	var dh = MyClass.CreateDataHasher(RegionName, Data);
	if(tcid.ContainsKey(dh))
	{
		rc = tcid[dh].Id;
	}
	else
	{
		rc = Guid.NewGuid().ToString("n");
		var n = new MyClass(RegionName, rc, Data);
		var kh = n.GetKeyHasher();
		dict.Add(kh, n);
		tcid.Add(dh, kh);
	}
	return rc;
}

MyClass Find(string RegionName, string Id)
{
	return dict[MyClass.CreateKeyHasher(RegionName, Id)];
}

public class MyClass
{
	readonly string RegionName;
	readonly string Id;
	byte[] Data;
	
	public MyClass(string RegionName, string Id, byte[] Data)
	{
		this.RegionName = RegionName;
		this.Id = Id;
		this.Data = Data;
	}

	public override int GetHashCode()
	{
		return RegionName.GetHashCode() + Id.GetHashCode() + ((IStructuralEquatable)Data).GetHashCode(EqualityComparer<byte>.Default);
	}
	
	public MyClassKeyHasher GetKeyHasher()
	{
		return new MyClassKeyHasher(this.RegionName, this.Id);
	}

	public MyClassDataHasher GetDataHasher()
	{
		return new MyClassDataHasher(this.RegionName, this.Data);
	}

	public override bool Equals(object obj)
	{
		bool rc = false;
		if(obj is MyClass)
		{
			var o = (MyClass)obj;
			rc = string.Equals(this.RegionName, o.RegionName) && 
				string.Equals(this.Id, o.Id) && 
				Array.Equals(this.Data, o.Data);
		}
		return rc;
	}
	
	public static MyClassKeyHasher CreateKeyHasher(string RegionName, string Id)
	{
		return new MyClassKeyHasher(RegionName, Id);
	}
	
	public static MyClassDataHasher CreateDataHasher(string RegionName, byte[] Data)
	{
		return new MyClassDataHasher(RegionName, Data);		
	}

	public override string ToString()
	{
		return $"{RegionName}/{Id}:{Data.ToHexString()}";
	}

	public struct MyClassKeyHasher
	{
		public readonly string RegionName;
		public readonly string Id;
		readonly int HashValue;

		public MyClassKeyHasher(string RegionName, string Id)
		{
			this.RegionName = RegionName;
			this.Id = Id;
			this.HashValue = this.RegionName.GetHashCode() + Id.GetHashCode();
		}

		public override int GetHashCode()
		{
			return this.HashValue;
		}

		public override string ToString()
		{
			return $"{RegionName}/{Id}";
		}
	}

	public struct MyClassDataHasher
	{
		readonly int HashValue;
		
		public MyClassDataHasher(string RegionName, byte[] Data)
		{
			this.HashValue = RegionName.GetHashCode() + ((IStructuralEquatable)Data).GetHashCode(EqualityComparer<byte>.Default);
		}

		public override int GetHashCode()
		{
			return this.HashValue;
		}
	}
}

static class StringExt
{
	public static string ToHexString(this byte[] a, bool prefix = false)
	{
		return HexStr(a, prefix);
	}

	public static string HexStr(byte[] p, bool prefix = false)
	{
		char[] c = null;
		byte b;
		int xstart;

		if (prefix)
		{
			c = new char[p.Length * 2 + 2];
			c[0] = '0';
			c[1] = 'x';
			xstart = 2;
		}
		else
		{
			c = new char[p.Length * 2];
			xstart = 0;
		}

		for (int y = 0, x = xstart; y < p.Length; ++y, ++x)
		{
			b = ((byte)(p[y] >> 4));
			c[x] = (char)(b > 9 ? b + 0x37 : b + 0x30);
			b = ((byte)(p[y] & 0xF));
			c[++x] = (char)(b > 9 ? b + 0x37 : b + 0x30);
		}

		return new string(c);
	}
}