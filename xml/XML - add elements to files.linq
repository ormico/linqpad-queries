<Query Kind="Statements" />

/*
1. read all xml files in input folder
2. add UpdatedAsOf and Company elements to Applications elements
3. save each file to output folder
*/
var fileList = Directory.GetFiles(@"c:\xml-in\", "*.xml");
string destpath = @"c:\xml-out\";

foreach (var p in fileList)
{
	XDocument xdoc = XDocument.Load(p);
	var root = xdoc.Element("Applications");
	foreach (var applic in root.Elements())
	{
		applic.AddFirst(new XElement("UpdatedAsOf", "2017-06-01T00:00:00"));
		applic.AddFirst(new XElement("Company", "4000"));
	}
	
	XmlWriterSettings settings = new XmlWriterSettings();
	settings.OmitXmlDeclaration = true;
	settings.Indent = true;
	StringWriter sw = new StringWriter();
	using (XmlWriter xw = XmlWriter.Create(Path.Combine(destpath, Path.GetFileName(p)), settings))
	{
		xdoc.Save(xw);
	}
}