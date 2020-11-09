<Query Kind="Statements" />

XDocument xdoc = XDocument.Parse(
@"<?xml version=""1.0""?>
<Customers>
	<Customer>
		<AccountNumber>A100009</AccountNumber>
		<Balance>100000</Balance>
		<FieldError>Invalid Customer Number</FieldError>
	</Customer>
	<Customer>
		<AccountNumber>A100010</AccountNumber>
		<Balance>120000</Balance>
	</Customer>
	<Error>Last Customer Generated an error</Error>
	<Customer>
		<AccountNumber>A100011</AccountNumber>
		<Balance>131000</Balance>
	</Customer>
	<Customer>
		<AccountNumber>A100012</AccountNumber>
		<Balance>120100</Balance>
	</Customer>
	<Prospect>
		<AccountNumber>P12000</AccountNumber>
		<Balance>120100</Balance>
	</Prospect>
	<Prospect>
		<AccountNumber>P12002</AccountNumber>
		<Balance>133100</Balance>
	</Prospect>
</Customers>");

foreach(var l in xdoc.Elements().Elements())
{
	if(l.Name == "Error")
	{
		Console.WriteLine("File Error: {0}", l.Value);
	}
	else
	{
		Console.WriteLine(l.Name);
	
		XElement xErr = l.Element("FieldError");
		if(xErr != null)
		{
			Console.WriteLine("{0}", xErr.Value);
		}
		else
		{	
			Console.WriteLine("{0}", l.Element("AccountNumber").Value);
			Console.WriteLine("{0}", l.Element("Balance").Value);
		}
	}
	
	Console.WriteLine();
}
