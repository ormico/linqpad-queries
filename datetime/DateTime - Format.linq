<Query Kind="Statements" />

//https://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx
//https://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx
DateTime n = DateTime.Now;
n.Dump();
n.Kind.Dump();

Console.WriteLine("u: {0}", n.ToString("u"));
Console.WriteLine("U: {0}", n.ToString("U"));

//https://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#Roundtrip
Console.WriteLine("o: {0}", n.ToString("o"));
Console.WriteLine("o2: {0:o}", n);

Console.WriteLine("s: {0}", n.ToString("s"));
//Console.WriteLine("K: {0}", n.ToString("K"));
Console.WriteLine("custom: {0}", n.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffffZ"));
Console.WriteLine("custom2: {0}", n.ToString("yyyy-MM-ddZ"));

Console.WriteLine("g: {0}", n.ToString("g"));
Console.WriteLine("d: {0}", n.ToString("d"));
Console.WriteLine("D: {0}", n.ToString("D"));

n = n.ToUniversalTime();
Console.WriteLine();
Console.WriteLine("u: {0}", n.ToString("u"));
Console.WriteLine("U: {0}", n.ToString("U"));

//https://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx#Roundtrip
Console.WriteLine("o: {0}", n.ToString("o"));

Console.WriteLine("s: {0}", n.ToString("s"));
//Console.WriteLine("z: {0}", n.ToString("z"));
Console.WriteLine("custom: {0}", n.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffffZ"));
Console.WriteLine("custom: {0}", n.ToString("yyyy-MM-dd'T'HH:mm:ss.fff"));

Console.WriteLine("custom: {0}", n.ToString("yyyyMMdd-HHmm"));

DateTimeOffset.Now.Add(TimeSpan.FromMinutes(20.0)).Dump();

Console.WriteLine(DateTime.Now.ToString("MMM-yyyy"));