<Query Kind="Statements" />

Regex r = new Regex("[^0-9]");
string example = "(478) 714-5555";
string rc = r.Replace(example, string.Empty);
rc.Dump();