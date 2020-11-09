<Query Kind="Program" />

void Main()
{
	for(int i = 0; i < 100; i++)
	{
		double t = Math.Pow(2,  (i + 1));
	
		Console.WriteLine("{0} => {1}", t, FormatLargeNumber(t));
	}
}

// Define other methods and classes here

string FormatLargeNumber(double n)
{
	string rc = null;
	if (n < 1000)
	{
		rc = n.ToString("#,#");
	}
	else if (n < 1000000)
	{
		rc = n.ToString("#,##0,K");
	}
	else if (n < 1000000000)
	{
		rc = n.ToString("#,##0,,M");
	}
	else if (n < 1000000000000)
	{
		rc = n.ToString("#,##0,,,B");
	}
	else
	{
		rc = n.ToString("#,##0,,,,T");
	}
	return rc;
}

string FormatLargeNumber(decimal n)
{
	string rc = null;
	if (n < 1000)
	{
		rc = n.ToString("#,#");
	}
	else if (n < 1000000)
	{
		rc = n.ToString("#,##0,K");
	}
	else if (n < 1000000000)
	{
		rc = n.ToString("#,##0,,M");
	}
	else if (n < 1000000000000)
	{
		rc = n.ToString("#,##0,,,B");
	}
	else
	{
		rc = n.ToString("#,##0,,,,T");
	}
	return rc;
}