<Query Kind="VBStatements" />

Dim a as string = "blah"
Dim b as string = Nothing
Dim c as string

if a is nothing then
	Console.WriteLine("a is nothing")
else
	Console.WriteLine("a is not nothing")
end if

if b isnot nothing then
	Console.WriteLine("a is not nothing")
else
	Console.WriteLine("a is nothing")
end if

if not a is nothing then
	Console.WriteLine("a is not nothing")
end if

if b is nothing then
	Console.WriteLine("b is nothing")
end if

if not b is nothing then
	Console.WriteLine("b is not nothing")
end if

if c is nothing then
	Console.WriteLine("c is nothing")
end if

if not c is nothing then
	Console.WriteLine("c is not nothing")
end if