<Query Kind="Statements" />

int rem;
for(int i=0;i<1000;i++)
{
	var x = i%100;
	if(x == 0)
	{
		Math.DivRem(i, 100, out rem);
		Console.WriteLine("{0}:{1} Rem:{2}", i, x, rem);
	}
}