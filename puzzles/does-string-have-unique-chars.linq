<Query Kind="Program" />

void Main()
{
	// call each one once to warm up
	Console.WriteLine(IsUnique("abcdefghijklmnopqrstuvxyz"));
	Console.WriteLine(IsUnique2("abcdefghijklmnopqrstuvxyz"));

	var watch = Stopwatch.StartNew();
	watch.Reset();
	watch.Start();
	for(int i = 0; i < 100000; i++)
	{
		IsUnique("a");
		IsUnique("aa");
		IsUnique("ab");
		IsUnique("abc");
		IsUnique("abcdefghijklmnopqrstuvxyz");
		IsUnique("abcdefghijklmnopqrstuvxyza");
		IsUnique("abcdefghijklmnopqadfadsfasdrstuvxyza");
	}
	watch.Stop();
	Console.WriteLine($"1) elapsed time = {watch.Elapsed}");

	watch.Reset();
	watch.Start();
	for (int i = 0; i < 100000; i++)
	{
		IsUnique2("a");
		IsUnique2("aa");
		IsUnique2("ab");
		IsUnique2("abc");
		IsUnique2("abcdefghijklmnopqrstuvxyz");
		IsUnique2("abcdefghijklmnopqrstuvxyza");
		IsUnique2("abcdefghijklmnopqadfadsfasdrstuvxyza");
	}
	watch.Stop();
	Console.WriteLine($"2) elapsed time = {watch.Elapsed}");
}

// O(log n) ?
// its less than O(n) b/c it can exit early
private bool IsUnique(string s)
{
	int[] alpha = new int[26];
	foreach (var c in s)
	{
		if (alpha[c - 'a'] + 1 > 1)
			return false;

		alpha[c - 'a']++;
	}
	return true;
}

// O(n) ?
// to work this has to hash every char, every time
bool IsUnique2(string s)
{
	HashSet<char> set = new HashSet<char>(s.ToCharArray());
	return s.Length == set.Count;
}
