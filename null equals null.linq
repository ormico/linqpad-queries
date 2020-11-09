<Query Kind="Statements" />

string a = null;
string b = null;
// in sql NULL <> NULL
// in c# null == null aparently
string.Equals(a, b).Dump();