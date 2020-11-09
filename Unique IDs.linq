<Query Kind="Statements">
  <NuGetReference>MathNet.Numerics</NuGetReference>
  <Namespace>MathNet.Numerics.Distributions</Namespace>
  <Namespace>MathNet.Numerics.Random</Namespace>
</Query>

string id;
//dt.Dump();

// random byte id in hex
Console.WriteLine("random byte id in hex");
System.Random rng = SystemRandomSource.Default;
byte[] byteArray = rng.NextBytes(8);
id = BitConverter.ToString(byteArray).Replace("-", string.Empty);
id.Dump();
Console.WriteLine();

// date time id. will have collisions if events happen at same time
Console.WriteLine("date time id. will have collisions if events happen at same time");
var dt = DateTime.Now;
id = dt.ToString("yyyyMMddHHmmss");
id.Dump();
Console.WriteLine();

// make id unique but sortable by prepending date time
Console.WriteLine();
Console.WriteLine("make id unique but sortable by prepending date time");
id += "_" + BitConverter.ToString(byteArray).Replace("-", string.Empty);

id.Dump();