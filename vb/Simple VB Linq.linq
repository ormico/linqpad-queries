<Query Kind="VBStatements" />

Dim l as new List(of string)
l.Add("alpha")
l.Add("beta")
l.Add("gamma")
l.Add("delta")

l.Dump()

Dim x = From a in l
		Select Name = a, Length = 0

x.Dump()