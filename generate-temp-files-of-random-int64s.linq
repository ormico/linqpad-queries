<Query Kind="Program">
  <Namespace>System.CodeDom.Compiler</Namespace>
</Query>

void Main()
{
	const string ns1filename = "c:/temp/ns1.stream";
	const string ns2filename = "c:/temp/ns2.stream";

	using(TempFileCollection tfc = new TempFileCollection())
	{
		Random rand = new Random();
		//byte[] buffer;

		tfc.AddFile(ns1filename, false);
		tfc.AddFile(ns2filename, false);
		
		using FileStream fs1 = File.Open(ns1filename, FileMode.CreateNew);
		BinaryWriter bw1 = new BinaryWriter(fs1);
		using FileStream fs2 = File.Open(ns2filename, FileMode.CreateNew);
		BinaryWriter bw2 = new BinaryWriter(fs2);
		byte[] int64Buffer = new byte[8];

		for (int i = 0; i < 1000000000; i++)
		{
			rand.NextBytes(int64Buffer);
			//a.Dump();
			long rv = BitConverter.ToInt64(int64Buffer, 0);
			bw1.Write(rv);
			//Console.WriteLine("{0}", rv);

			rand.NextBytes(int64Buffer);
			//a.Dump();
			rv = BitConverter.ToInt64(int64Buffer, 0);
			//Console.WriteLine("{0}", rv);
			bw2.Write(rv);
		}

		fs1.Position = 0;
		using BinaryReader br1 = new BinaryReader(fs1);

		fs2.Position = 0;
		using BinaryReader br2 = new BinaryReader(fs2);

		int64Buffer = br1.ReadBytes(8);
		Console.WriteLine(BitConverter.ToInt64(int64Buffer, 0));

		int64Buffer = br2.ReadBytes(8);
		Console.WriteLine(BitConverter.ToInt64(int64Buffer, 0));
	}
}

// You can define other methods, fields, classes and namespaces here
