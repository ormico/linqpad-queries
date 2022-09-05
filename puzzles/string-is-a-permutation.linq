<Query Kind="Program" />

void Main()
{
	string a = "abadaba";
	string b = "badabud";
	
	var a2 = a.ToCharArray();
	var b2 = b.ToCharArray();
	Array.Sort(a2);
	Array.Sort(b2);
	bool isPerm = false;
	
	if(a2.Length == b2.Length)
	{
		isPerm = true;
		for(int i = 0; i < a2.Length; i++)
		{
			if(a2[i] != b2[i])
			{
				isPerm = false;
				break;
			}
		}
	}
	
	if(isPerm)
	{
		Console.WriteLine("a is a permutation of b");
	}
	else
	{
		Console.WriteLine("a is not a permutation of b");
	}
}

// You can define other methods, fields, classes and namespaces here
