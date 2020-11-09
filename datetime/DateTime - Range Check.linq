<Query Kind="Program" />

void Main()
{
	F(DateTime.MinValue, DateTime.MinValue, 0);
	F(DateTime.MinValue, DateTime.MinValue, 1);
	F(DateTime.MinValue, DateTime.MinValue, 2);
	F(DateTime.MinValue, DateTime.MinValue, 3);
	F(DateTime.MinValue, DateTime.MinValue, 4);

	F(DateTime.MinValue, DateTime.MinValue.AddDays(1), 0);
	F(DateTime.MinValue, DateTime.MinValue.AddDays(1), 1);
	F(DateTime.MinValue, DateTime.MinValue.AddDays(1), 2);
	F(DateTime.MinValue, DateTime.MinValue.AddDays(1), 3);
	F(DateTime.MinValue, DateTime.MinValue.AddDays(1), 4);

	F(DateTime.MaxValue, DateTime.MaxValue, 0);
	F(DateTime.MaxValue, DateTime.MaxValue, 1);
	F(DateTime.MaxValue, DateTime.MaxValue, 2);
	F(DateTime.MaxValue, DateTime.MaxValue, 3);
	F(DateTime.MaxValue, DateTime.MaxValue, 4);

	F(DateTime.Parse("01/01/2000"), DateTime.Parse("01/01/2000"), 0);
	F(DateTime.Parse("01/01/2000"), DateTime.Parse("01/01/2000"), 1);
	F(DateTime.Parse("01/03/2000"), DateTime.Parse("01/01/2000"), 0);
	F(DateTime.Parse("01/03/2000"), DateTime.Parse("01/01/2000"), 1);
	F(DateTime.Parse("01/01/2000"), DateTime.Parse("01/03/2000"), 0);
	F(DateTime.Parse("01/01/2000"), DateTime.Parse("01/03/2000"), 1);
	F(DateTime.Parse("01/01/2000"), DateTime.Parse("01/03/2000"), 2);
}

// is a within Days before b
bool F(DateTime a, DateTime b, float Days)
{
	bool rc = false;
	if((b - DateTime.MinValue).TotalDays <= Days)
	{
		rc = true;
	}
	else
	{
		if(a >= b.AddDays(-1 * Math.Abs(Days)))
		{
			rc = true;
		}
	}
	Console.WriteLine("savedt: {0} lastdt: {1} days: {2} TotalDaysFromLastDtToMin: {3}  rc: {4}",
		a, b, Days, (b - DateTime.MinValue).TotalDays, rc);
	return rc;
}