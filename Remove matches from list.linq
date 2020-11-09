<Query Kind="Statements" />

List<string> list = new List<string>()
{
	"alpha", "beta", "gamma", "delta", "alpha alpha", "alpha beta"
};

list.RemoveAll(i => i.Contains("alp"));

list.Dump();