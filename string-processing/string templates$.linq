<Query Kind="Statements" />

DateTime? x = DateTime.Now;
$"blah {x:s} blah".Dump();
x = null;
$"blah {x:s} blah".Dump();
// see also "string.Format Test"