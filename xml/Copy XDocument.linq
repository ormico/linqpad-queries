<Query Kind="Statements" />

// copy reference
XDocument xdoc = XDocument.Parse("<root><a id=\"3\"/></root>");
XDocument xdoc2 = xdoc;
xdoc2.Element("root").Element("a").Attribute("id").Value = "999";

xdoc.Dump();
xdoc2.Dump();

// copy object using copy constructor
XDocument xdoc3 = XDocument.Parse("<root><a id=\"3\"/></root>");
XDocument xdoc4 = new XDocument(xdoc);
xdoc2.Element("root").Element("a").Attribute("id").Value = "999";

xdoc3.Dump();
xdoc4.Dump();


