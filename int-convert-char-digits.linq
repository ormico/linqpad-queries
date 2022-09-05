<Query Kind="Statements" />

/*
givent a character c that contains a digit from 0 to 9,
convert c to an int
*/
char a = '1';
char b = '5';
char c = '0';

Console.WriteLine(a - '1' + 1);
Console.WriteLine(b - '1' + 1);
Console.WriteLine(c - '1' + 1);
Console.WriteLine();

Console.WriteLine(F('3') + F('2'));

int F(char c)
{
	return c - '1' + 1;
}