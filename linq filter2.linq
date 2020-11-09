<Query Kind="Program" />

void Main()
{
	List<X> x = PopulateX();
	Print(x);
	
	List<X> y = Query(x, 1, null, null, null); 
	
	Print(y);
}

List<X> Query(List<X> x, int? A, int? B, int? C, int? D)
{
	List<X> y = (from a in PopulateX() where a.A == 1 select a).ToList();
	
	return
	(
		from a in x
		where 
			y.Find((b) => {return b.A==a.A && b.B == a.B;}) != null
		select a
	).ToList();	
}

void Print(List<X> x)
{
	Console.WriteLine("Count: {0}", x.Count);
	foreach(var y in x)
	{
		Console.WriteLine("{0} {1} {2} {3}", y.A, y.B, y.C, y.D);
	}
}

List<X> PopulateX()
{
	List<X> x = new List<X>();
	x.Add(new X() { A=1, B=1, C=1, D=1 });
	x.Add(new X() { A=1, B=1, C=1, D=2 });
	x.Add(new X() { A=1, B=1, C=1, D=3 });
	x.Add(new X() { A=1, B=1, C=1, D=4 });
	x.Add(new X() { A=1, B=1, C=1, D=5 });
	x.Add(new X() { A=1, B=1, C=1, D=6 });
	x.Add(new X() { A=1, B=1, C=1, D=7 });
	x.Add(new X() { A=1, B=1, C=2, D=1 });
	x.Add(new X() { A=1, B=1, C=2, D=2 });
	x.Add(new X() { A=1, B=1, C=2, D=3 });
	x.Add(new X() { A=1, B=1, C=2, D=4 });
	x.Add(new X() { A=2, B=1, C=1, D=1 });
	x.Add(new X() { A=2, B=1, C=1, D=2 });
	x.Add(new X() { A=2, B=1, C=1, D=3 });
	x.Add(new X() { A=2, B=1, C=1, D=4 });
	x.Add(new X() { A=2, B=1, C=1, D=5 });
	x.Add(new X() { A=2, B=1, C=1, D=6 });
	x.Add(new X() { A=2, B=1, C=1, D=7 });
	x.Add(new X() { A=2, B=1, C=2, D=1 });
	x.Add(new X() { A=2, B=1, C=2, D=2 });
	x.Add(new X() { A=2, B=1, C=2, D=3 });
	x.Add(new X() { A=2, B=1, C=2, D=4 });
	
	return x;
}

class X
{
	public int? A {get;set;}
	public int? B {get;set;}
	public int? C {get;set;}
	public int? D {get;set;}	
}