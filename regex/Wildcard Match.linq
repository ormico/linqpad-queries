<Query Kind="Statements" />

// use RegEx to do a Wildcard match (match using * instead of a regex pattern)

// escape any chars in pattern that might be a regex command then replace the escaped star with (.*)
string rx = Regex.Escape("ABC-(*).csv").Replace("\\*", "(.*)");
string x = "ABC-(201501110001).csv";
Regex r = new Regex(rx, RegexOptions.IgnoreCase);

r.IsMatch(x).Dump();


