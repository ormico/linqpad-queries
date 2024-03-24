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
goto here;

//replace
XElement xNew = XElement.Parse(
@"<Customer>
	<blah>123</blah>
</Customer>");

var i = xdoc.Element("Customers").Elements().Where(j => j.Element("AccountNumber").Value == "A100010").FirstOrDefault();

Console.WriteLine(i);

i.ReplaceWith(xNew);

Console.WriteLine(xdoc);

// insert in specific order
here:
xdoc = XDocument.Parse(
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
	<Customer>
		<AccountNumber>A100021</AccountNumber>
		<Balance>131000</Balance>
	</Customer>
	<Customer>
		<AccountNumber>A100022</AccountNumber>
		<Balance>120100</Balance>
	</Customer>
</Customers>");

xNew = XElement.Parse(
@"<Customer>
	<AccountNumber>A100020</AccountNumber>
	<Balance>100000</Balance>
	<Special />
</Customer>");

string n = xNew?.Element("AccountNumber")?.Value;
XElement xprev = null;
foreach(var xcur in xdoc.Element("Customers").Elements())
{
	string p = xprev?.Element("AccountNumber")?.Value;
	string c = xcur.Element("AccountNumber")?.Value;

	int compareNP = string.Compare(n, p);
	int compareNC = string.Compare(n, c);

	if(p == null && compareNC < 0)
	{
		xcur.AddBeforeSelf(xNew);
		break;
	}
	else if(compareNC == 0)
	{
		xcur.ReplaceWith(xNew);
		break;
	}
	else if(compareNP > 0 && compareNC < 0)
	{
		xcur.AddBeforeSelf(xNew);
		break;
	}
	else if(compareNC > 0 && xcur.NextNode == null)
	{
		xcur.AddAfterSelf(xNew);
		break;
	}

	xprev = xcur;
}

Console.WriteLine("----------------------------=========================----------------------------");
Console.WriteLine(xdoc);

// insert in specific order
xdoc = XDocument.Parse(
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
	<Customer>
		<AccountNumber>A100021</AccountNumber>
		<Balance>131000</Balance>
	</Customer>
	<Customer>
		<AccountNumber>A100022</AccountNumber>
		<Balance>120100</Balance>
	</Customer>
</Customers>");

var y = xdoc.Element("Customers").Elements().Where(m => m.Element("AccountNumber").Value == "A100010").FirstOrDefault();
y.AddBeforeSelf(XElement.Parse("<Before />"));
y.AddAfterSelf(XElement.Parse("<After />"));
y.ReplaceWith(XElement.Parse("<Replacement />"));

Console.WriteLine("----------------------------=========================----------------------------");
Console.WriteLine(xdoc);
