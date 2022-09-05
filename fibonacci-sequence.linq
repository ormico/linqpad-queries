<Query Kind="Program" />

void Main()
{
	for(ulong i = 0; i < 20; i++)
	{
		Console.WriteLine(fib(i));
	}

	Console.WriteLine("---------------------------------------------");
	Console.WriteLine("--\"Dynamic Progamming\"-----------------------");

	for (ulong i = 0; i < 10000; i++)
	{
		Console.WriteLine(fib2(i));
	}

	Console.WriteLine("---------------------------------------------");
	Console.WriteLine("--\"Dynamic Progamming2\"-----------------------");

	var f = new Fibonacci();
	for (ulong i = 0; i < 10000; i++)
	{
		Console.WriteLine(f.Calculate(i));
	}
}

// O(2^N)
ulong fib(ulong i)
{
	if(i==0) return 0;
	if(i==1) return 1;
	return fib(i - 1) + fib(i - 2);
}

// O(N)
// buffer values we already calculated instead of re-calculating each time
ulong fib2(ulong i)
{
	ulong fib2(ulong i, ulong[] buf)
	{
		if (i == 0) return 0;
		if (i == 1) return 1;
		if (buf[i] == 0)
		{
			buf[i] = fib2(i - 1, buf) + fib2(i - 2, buf);
		}
		return buf[i];
	}

	ulong[] buf = new ulong[i+1];
	return fib2(i, buf);
}

// cache calculated values in class
// for re-use on subsequent calls
class Fibonacci
{
	ulong[] buf = new ulong[1000];
	
	public ulong Calculate(ulong i)
	{
		if((i + 1) > (ulong)buf.LongLength)
		{
			ulong[] newbuf = new ulong[i * 2];
			Array.Copy(this.buf, newbuf, this.buf.Length);
			this.buf = newbuf;
		}
		return fib2(i);
	}

	private ulong fib2(ulong i)
	{
		if (i == 0) return 0;
		if (i == 1) return 1;
		if (buf[i] == 0)
		{
			buf[i] = fib2(i - 1) + fib2(i - 2);
		}
		return buf[i];
	}
}