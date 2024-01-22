<Query Kind="Statements" />

/*
ed1ebab1-7d4b-462e-914a-794c08b23987
abc87eb4-7a13-4db5-ad5a-b4bf025c2942
ad43ccd1-b1df-436e-9173-022260575cab
94e7a266-ef57-4746-b941-cf5512dec540
3085d673-303e-4423-a828-fa50fdd63baa
*/

List<Guid> guids = new()
{
	Guid.Parse("ed1ebab1-7d4b-462e-914a-794c08b23987"),
	Guid.Parse("abc87eb4-7a13-4db5-ad5a-b4bf025c2942"),
	Guid.Parse("ad43ccd1-b1df-436e-9173-022260575cab"),
	Guid.Parse("94e7a266-ef57-4746-b941-cf5512dec540"),
	Guid.Parse("3085d673-303e-4423-a828-fa50fdd63baa"),
};

foreach(var i in guids)
{
	Console.WriteLine(i);
}

Console.WriteLine("--------------------");
Console.WriteLine();

guids.Remove(Guid.Parse("ad43ccd1-b1df-436e-9173-022260575cab"));

foreach (var i in guids)
{
	Console.WriteLine(i);
}


