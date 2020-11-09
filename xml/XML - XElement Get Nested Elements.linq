<Query Kind="Program" />

void Main()
{
	XElement xelem = XElement.Parse("<top><middle><inner>blah</inner></middle></top>");
	GetElement(xelem, "middle", "inner").Dump();
}

public XElement GetElement(XElement parent, params string[] args)
{
	XElement rc = parent;
	if (parent != null)
	{
		for (int i = 0; i < args.Length; i++)
		{
			rc = rc?.Element(args[i]);
		}
	}
	return rc;
}
