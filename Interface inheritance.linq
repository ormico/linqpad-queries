<Query Kind="Program" />

void Main()
{
	Parent parent = new Parent();	
	Console.WriteLine("parent is IBase {0}", parent is IBase);
	Xena xena = new Xena();
	Console.WriteLine("xena is IBase {0}", xena is IBase);
	Betty betty = new Betty();
	Console.WriteLine("betty is IBase {0}", betty is IBase);
	betty.B();
}

interface IBase
{
}

interface IAlpha : IBase
{
}

interface IBeta : IBase
{
	void B();
}

class Person : IBase
{
	
}

class Parent : Person
{
	
}

class Xena : IAlpha
{
}

class Bob
{
	public void B() { Console.WriteLine("Bob.B()"); }
}

class Betty : Bob, IBeta
{
	// Bob.B() can implement IBeta even though it is in Bob base class
}