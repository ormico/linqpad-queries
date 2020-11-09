<Query Kind="Statements" />

XDocument xdoc = XDocument.Parse(
@"<?xml version=""1.0"" encoding=""us-ascii""?>
<a>
	<b>
		<c>charlie</c>
		<z>zeta</z>
	</b>
</a>");
Console.WriteLine(xdoc.ToString());

var aa = xdoc.Element("aa");
if(aa!=null)
{
	// returns null if aa not found
	Console.WriteLine(aa.ToString());
}

var a = xdoc.Element("a");
if(a!=null)
{
	Console.WriteLine(a.ToString());
}

var getC = from c in xdoc.Element("a").Element("b").Elements("c")
			select c.Value;
			
foreach(var c in getC)
{
	Console.WriteLine(c);
}

var getElements = from c in xdoc.Element("a").Element("b").Elements()
			select c.Value;
			
foreach(var e in getElements)
{
	Console.WriteLine(e);
}