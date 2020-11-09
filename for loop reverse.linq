<Query Kind="Statements" />

List<string> l = new List<string>();
l.Add("a");
l.Add("b");
l.Add("c");
l.Add("d");
l.Add("e");
l.Add("f");
l.Add("g");
l.Add("h");
l.Add("i");
l.Add("j");
l.Add("k");
l.Add("l");
l.Add("m");


for(int i=l.Count - 1; i >= 0; i--)
{
	Console.WriteLine("{0}", l[i]);
}