<Query Kind="Statements" />

// Array Sort Lesser to Greater and Greater to Lesser
int[] a = {1,5,0,4};
Array.Sort(a);
for(int i=0;i<a.Length;i++)
{
	Console.WriteLine(a[i]);
}
Console.WriteLine();

Array.Sort(a, (i, j) => { return j - i; });
for (int i = 0; i < a.Length; i++)
{
	Console.WriteLine(a[i]);
}
Console.WriteLine();

//-----------------------------------
var b = new List<int>() { 1, 5, 0, 4 };
b.Sort();
for (int i = 0; i < b.Count(); i++)
{
	Console.WriteLine(b[i]);
}
Console.WriteLine();

// use custom compare function to reverse left and right compare to reverse sort
b.Sort((l, r) => { return r.CompareTo(l); });
for (int i = 0; i < b.Count(); i++)
{
	Console.WriteLine(b[i]);
}
