<Query Kind="Statements" />

// http://stackoverflow.com/questions/1528724/converting-a-listint-to-a-comma-separated-list
List<string> list = new List<string>()
{
	"alpha",
	"beta",
	"gamma",
	"delta",
	"zeta"
};

Console.WriteLine(string.Join(",", list));

List<int> list2 = new List<int>()
{
	1, 5, 9, 10, 45, 11, 99, 82
};

// I think the .ToArray() is for performance
Console.WriteLine(string.Join(",", list2.ToArray()));
