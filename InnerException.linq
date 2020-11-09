<Query Kind="Program" />

void Main()
{
	try
	{
		Go();
		Console.WriteLine("No Exception.");
	}
	catch(Exception ex)
	{
		Console.WriteLine(ex.ToString());
	}
}

void Go()
{
	try
	{
		Do();
	}
	catch(Exception ex)
	{
		var aex = new ApplicationException("My Exception", ex);
		
		throw aex;
	}
}

void Do()
{
	var ex = new Exception("My Inner Exception");
	
	throw ex;
}
