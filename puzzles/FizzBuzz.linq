<Query Kind="Statements" />

/*
Write a program that prints the numbers from 1 to 100. 
But for multiples of three print “Fizz” instead of the 
number and for the multiples of five print “Buzz”. For 
numbers which are multiples of both three and five print 
“FizzBuzz”.
http://wiki.c2.com/?FizzBuzzTest
*/
for (int i = 1; i < 100; i++)
{
	if (i % 3 == 0 && i % 5 == 0)
	{
		Console.WriteLine("FizzBuzz");
	}
	else if (i % 3 == 0)
	{
		Console.WriteLine("Fizz");
	}
	else if (i % 5 == 0)
	{
		Console.WriteLine("Buzz");
	}
	else
	{
		Console.WriteLine(i);
	}
}