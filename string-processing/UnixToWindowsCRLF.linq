<Query Kind="Statements" />

string x = "abcd\refgh\nijklmn\r\n12345";

for(int i=0;i<x.Length;i++)
{
	Console.Write("{0:000}|", (byte)x[i]);
}
Console.WriteLine();

x = x.Replace("\r\n", "\r").Replace("\n", "\r").Replace("\r", "\r\n");

for(int i=0;i<x.Length;i++)
{
	Console.Write("{0:000}|", (byte)x[i]);
}
Console.WriteLine();
