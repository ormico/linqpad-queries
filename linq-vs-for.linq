<Query Kind="Program" />

void Main()
{
	const int steps = 1000000;
	Stopwatch sw = new Stopwatch();

	List<O1> list = new List<O1>()
	{
		new O1() { ParentId = null, OrganizationalUnitId = "100", EffectiveStartDate = DateTime.Parse("01/01/2000"), EffectiveEndDate = DateTime.Parse("01/01/2020") },
		new O1() { ParentId = "100", OrganizationalUnitId = "100.1", EffectiveStartDate = DateTime.Parse("01/01/2000"), EffectiveEndDate = DateTime.Parse("01/01/2020") },
		new O1() { ParentId = "100", OrganizationalUnitId = "100.2", EffectiveStartDate = DateTime.Parse("01/01/2000"), EffectiveEndDate = DateTime.Parse("01/01/2020") },
		new O1() { ParentId = "100", OrganizationalUnitId = "100.3", EffectiveStartDate = DateTime.Parse("01/01/2000"), EffectiveEndDate = DateTime.Parse("01/01/2020") },
		new O1() { ParentId = "100", OrganizationalUnitId = "100.4", EffectiveStartDate = DateTime.Parse("01/01/2000"), EffectiveEndDate = DateTime.Parse("01/01/2020") },
		new O1() { ParentId = "100", OrganizationalUnitId = "100.5", EffectiveStartDate = DateTime.Parse("01/01/2000"), EffectiveEndDate = DateTime.Parse("01/01/2020") },
		new O1() { ParentId = "100", OrganizationalUnitId = "100.6", EffectiveStartDate = DateTime.Parse("01/01/2000"), EffectiveEndDate = DateTime.Parse("01/01/2020") },
		new O1() { ParentId = "100", OrganizationalUnitId = "100.7", EffectiveStartDate = DateTime.Parse("01/01/2000"), EffectiveEndDate = DateTime.Parse("01/01/2020") },
		new O1() { ParentId = "100", OrganizationalUnitId = "100.8", EffectiveStartDate = DateTime.Parse("01/01/2000"), EffectiveEndDate = DateTime.Parse("01/01/2020") },
		new O1() { ParentId = "100", OrganizationalUnitId = "100.9", EffectiveStartDate = DateTime.Parse("01/01/2000"), EffectiveEndDate = DateTime.Parse("01/01/2020") },
		new O1() { ParentId = "100", OrganizationalUnitId = "100.10", EffectiveStartDate = DateTime.Parse("01/01/2000"), EffectiveEndDate = DateTime.Parse("01/01/2020") },
		new O1() { ParentId = "100", OrganizationalUnitId = "100.11", EffectiveStartDate = DateTime.Parse("01/01/2000"), EffectiveEndDate = DateTime.Parse("01/01/2020") },

		new O1() { ParentId = "100.1", OrganizationalUnitId = "100.1.1", EffectiveStartDate = DateTime.Parse("01/01/2000"), EffectiveEndDate = DateTime.Parse("01/01/2020") },
		new O1() { ParentId = "100.1", OrganizationalUnitId = "100.1.2", EffectiveStartDate = DateTime.Parse("01/01/2000"), EffectiveEndDate = DateTime.Parse("01/01/2020") },
		new O1() { ParentId = "100.1", OrganizationalUnitId = "100.1.3", EffectiveStartDate = DateTime.Parse("01/01/2000"), EffectiveEndDate = DateTime.Parse("01/01/2020") },
		new O1() { ParentId = "100.1", OrganizationalUnitId = "100.1.4", EffectiveStartDate = DateTime.Parse("01/01/2000"), EffectiveEndDate = DateTime.Parse("01/01/2020") },
		new O1() { ParentId = "100.1", OrganizationalUnitId = "100.1.5", EffectiveStartDate = DateTime.Parse("01/01/2000"), EffectiveEndDate = DateTime.Parse("01/01/2020") },
		
		new O1() { ParentId = "100.2", OrganizationalUnitId = "100.2.1", EffectiveStartDate = DateTime.Parse("01/01/2000"), EffectiveEndDate = DateTime.Parse("01/01/2020") }
	};
	var efDt = DateTime.Parse("05/01/2010");
	sw.Start();
	for (int i = 0; i < steps; i++)
	{
		F1(list, "100.1.1", efDt);
	}
	sw.Stop();
	Console.WriteLine("F1:\tMilliseconds = {0},\tTicks = {1}", sw.ElapsedMilliseconds, sw.ElapsedTicks);

	sw.Reset();
	sw.Start();
	for (int i = 0; i < steps; i++)
	{
		F2(list, "100.1.1", efDt);
	}
	sw.Stop();
	Console.WriteLine("F2:  \tMilliseconds = {0},\tTicks = {1}", sw.ElapsedMilliseconds, sw.ElapsedTicks);

	sw.Reset();
	sw.Start();
	for (int i = 0; i < steps; i++)
	{
		F1(list, "100.1.1", efDt);
	}
	sw.Stop();
	Console.WriteLine("F1:\tMilliseconds = {0},\tTicks = {1}", sw.ElapsedMilliseconds, sw.ElapsedTicks);


	sw.Reset();
	sw.Start();
	for (int i = 0; i < steps; i++)
	{
		F2(list, "100.1.1", efDt);
	}
	sw.Stop();
	Console.WriteLine("F2:  \tMilliseconds = {0},\tTicks = {1}", sw.ElapsedMilliseconds, sw.ElapsedTicks);


}

// not checking if any entry in list is null

string F1(List<O1> list, string orgId, DateTime effectiveDate)
{
	var parent = (from r in list
			  where r.OrganizationalUnitId == orgId
			  && r.EffectiveStartDate <= effectiveDate
			  && ((r.EffectiveEndDate >= effectiveDate)
			  || (r.EffectiveEndDate == DateTime.MinValue))
			  select r.ParentId).FirstOrDefault();
	return parent;
}

string F2(List<O1> list, string orgId, DateTime effectiveDate)
{
	//for(int i=0;i<list.Count;i++)
	foreach(var c in list)
	{
		//var c = list[i];
		if (c.OrganizationalUnitId == orgId && c.EffectiveStartDate <= effectiveDate
			  && ((c.EffectiveEndDate >= effectiveDate)
			  || (c.EffectiveEndDate == DateTime.MinValue)))
			  {
			  	return c.ParentId;
			  }
	}
	return null;
}

class O1
{
	public string ParentId { get; set; }
	public string OrganizationalUnitId { get; set; }
	public DateTime EffectiveStartDate { get; set; }
	public DateTime EffectiveEndDate { get; set; }
}