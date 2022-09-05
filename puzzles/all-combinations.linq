<Query Kind="Program" />

void Main()
{
	Console.WriteLine("==> Int Combinations");
	int[] data = { 1, 4, 2, 5, 2 };
	var rc = GetAllCombinations(data,
	(ns, ps, n) =>
	{
		bool rc = true;
		if(ps != null && ps.Contains(n))
		{
			rc = false;
		}
		return rc;
	});
	
	foreach(var i in rc)
	{
		Console.Write("{");
		foreach(var d in i)
		{
			Console.Write($"{d}|");
		}
		Console.WriteLine("}");
	}
	//--------------------------------------------//
	
	Console.WriteLine("==> String Combinations w/o duplicate chars");
	string[] data2 = { "ab", "bx", "fod", "cat", "tag" };
	// get all str combinations that don't share a character
	var rc2 = GetAllCombinations(data2);

	int maxLen = 0;
	foreach (var i in rc2)
	{
		if(i.Count > maxLen)
		{
			maxLen = i.Count;
		}
		
		Console.Write("{");
		foreach (var d in i)
		{
			Console.Write($"{d}|");
		}
		Console.WriteLine("}");
	}
	
	Console.WriteLine("Longest combinations w/o duplicate characters: {0}", maxLen);
}

List<HashSet<T>> GetAllCombinations<T>(T[] data)
{
	var rc = new List<HashSet<T>>();
	// add special case of none
	var p = new HashSet<T>();
	rc.Add(p);
	GetAllCombinations(data, p, 0, rc);
	return rc;
}

void GetAllCombinations<T>(T[] data, HashSet<T> parent, int startIndex, List<HashSet<T>> results)
{
	for (int i = startIndex; i < data.Length; i++)
	{
		var newParent = new HashSet<T>(parent);
		newParent.Add(data[i]);
		results.Add(newParent);
		GetAllCombinations(data, newParent, i + 1, results);
	}
}

List<HashSet<T>> GetAllCombinations<T>(T[] data, Func<HashSet<T>, HashSet<T>, T, bool> filter)
{
	var rc = new List<HashSet<T>>();
	// add special case of none
	var p = new HashSet<T>();
	if(filter(p, null, default))
	{
		rc.Add(p);
	}
	
	GetAllCombinations(data, p, 0, rc, filter);
	return rc;
}

//filter => Func<HashSet<T>, HashSet<T>, T, bool> filter => bool function(HashSet<T> newSet, HashSet<T> parentSet, T newData)
void GetAllCombinations<T>(T[] data, HashSet<T> parent, int startIndex, List<HashSet<T>> results, Func<HashSet<T>, HashSet<T>, T, bool> filter)
{
	for (int i = startIndex; i < data.Length; i++)
	{
		var newParent = new HashSet<T>(parent);
		newParent.Add(data[i]);
		if (filter(newParent, parent, data[i]))
		{
			results.Add(newParent);
		}

		GetAllCombinations(data, newParent, i + 1, results, filter);
	}
}

List<HashSet<char>> GetAllCombinations(string[] data)
{
	var rc = new List<HashSet<char>>();
	// add special case of none
	var p = new HashSet<char>();
	rc.Add(p);

	GetAllCombinations(data, p, 0, rc);
	return rc;
}

void GetAllCombinations(string[] data, HashSet<char> parent, int startIndex, List<HashSet<char>> results)
{
	for (int i = startIndex; i < data.Length; i++)
	{		
		var newParent = new HashSet<char>(parent);
		foreach(var c in data[i])
		{
			newParent.Add(c);
		}		
		
		bool include = true;
		foreach(var c in data[i])
		{
			if(parent.Contains(c))
			{
				include = false;
				break;
			}
		}
		
		if(include)
		{
			results.Add(newParent);
			GetAllCombinations(data, newParent, i + 1, results);
		}
	}
}
