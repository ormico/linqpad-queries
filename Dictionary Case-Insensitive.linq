<Query Kind="Statements" />

Dictionary<string, string> dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
dict.Add("alpha", "aaaa");

if(dict.Keys.Contains("Alpha"))
{
	dict["Alpha"] = "AAAA";
}

dict.Dump();