<Query Kind="Statements" />

try
{
	Console.WriteLine("SingleOrDefault(): {0}", (new List<string>()).SingleOrDefault());
}
catch (Exception e)
{
	Console.WriteLine("SingleOrDefault(): {0}", e.Message);
}

try
{
	Console.WriteLine("SingleOrDefault(): {0}", (new List<string>() { "alpha" }).SingleOrDefault());
}
catch(Exception e)
{
	Console.WriteLine("SingleOrDefault(): {0}", e.Message);
}

try
{
	Console.WriteLine("SingleOrDefault(): {0}", (new List<string>() { "alpha", "beta" }).SingleOrDefault());
}
catch (Exception e)
{
	Console.WriteLine("SingleOrDefault(): {0}", e.Message);
}

try
{
	Console.WriteLine("Single(): {0}", (new List<string>() { "alpha" }).Single());
}
catch (Exception e)
{
	Console.WriteLine("Single(): {0}", e.Message);
}

try
{
	Console.WriteLine("Single(): {0}", (new List<string>() { "alpha", "beta" }).Single());
}
catch (Exception e)
{
	Console.WriteLine("Single(): {0}", e.Message);
}

try
{
	Console.WriteLine("FirstOrDefault(): {0}", (new List<string>() { "alpha" }).FirstOrDefault());
}
catch (Exception e)
{
	Console.WriteLine("FirstOrDefault(): {0}", e.Message);
}

try
{
	Console.WriteLine("FristOrDefault(): {0}", (new List<string>() { "alpha", "beta" }).FirstOrDefault());
}
catch (Exception e)
{
	Console.WriteLine("FirstOrDefault(): {0}", e.Message);
}

try
{
	Console.WriteLine("First(): {0}", (new List<string>() { "alpha" }).First());
}
catch (Exception e)
{
	Console.WriteLine("First(): {0}", e.Message);
}
try
{
	Console.WriteLine("First(): {0}", (new List<string>() { "alpha", "beta" }).First());
}
catch (Exception e)
{
	Console.WriteLine("First(): {0}", e.Message);
}
