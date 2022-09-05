<Query Kind="Program" />

/*
Reverse digits of an integer.
if original integer is negative, reversed should also be negative

i expected that ReverseUsingMath() would be faster but it only seems to be faster
with low digit integers at least with the current implementation
*/
void Main()
{
	var watch = Stopwatch.StartNew();
	// reverse digits of a number
	int n = -7890;
	int rc=0;
	Console.WriteLine($"n = {n}");

	watch.Start();
	rc = ReverseUsingStringMethod(n);
	watch.Stop();
	Console.WriteLine($"1) {rc} elapsed time = {watch.Elapsed}");

	watch.Reset();
	watch.Start();
	rc = ReverseUsingMath(n);
	watch.Stop();
	Console.WriteLine($"2) {rc} elapsed time = {watch.Elapsed}");

	n = 0;
	Console.WriteLine($"n = {n}");

	watch.Reset();
	watch.Start();
	rc = ReverseUsingStringMethod(n);
	watch.Stop();
	Console.WriteLine($"1) {rc} elapsed time = {watch.Elapsed}");

	watch.Reset();
	watch.Start();
	rc = ReverseUsingMath(n);
	watch.Stop();
	Console.WriteLine($"2) {rc} elapsed time = {watch.Elapsed}");

	n = 82371;
	Console.WriteLine($"n = {n}");

	watch.Reset();
	watch.Start();
	rc = ReverseUsingStringMethod(n);
	watch.Stop();
	Console.WriteLine($"1) {rc} elapsed time = {watch.Elapsed}");

	watch.Reset();
	watch.Start();
	rc = ReverseUsingMath(n);
	watch.Stop();
	Console.WriteLine($"2) {rc} elapsed time = {watch.Elapsed}");
	
	Console.WriteLine("TESTS >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
	TestInteger(watch, 0);
	TestInteger(watch, 1);
	TestInteger(watch, 1234);
	TestInteger(watch, 9999);
	TestInteger(watch, 8230);
	TestInteger(watch, 293489562);
	TestInteger(watch, -8230);
	TestInteger(watch, -42630);
}

//1) easiest way is to do it as a string
int ReverseUsingStringMethod(int n)
{
	int rc = 0;
	string ns = $"{n}";
	StringBuilder nssb = new StringBuilder(ns.Length);
	int sign = 1;
	for (int i = ns.Length - 1; i >= 0; i--)
	{
		if (i == 0 && ns[i] == '-')
		{
			sign = -1;
		}
		else
		{
			nssb.Append(ns[i]);
		}
	}

	rc = int.Parse(nssb.ToString()) * sign;
	return rc;
}

//2) can you do it using math operations? is this faster?
// this method doesn't work on negative numbers (yet)
int ReverseUsingMath(int n)
{
	int rc = 0;
	
	// this algorithm doens't work with negative numbers so handle negative seperatly
	// (might be a way to fix this differently)
	int sign = 1;
	int pn = n;
	if(n < 0)
	{
		pn *= -1;
		sign = -1;
	}
	
	// calculate how many digits it has
	int k = 1;
	while (pn > Math.Pow(10, k))
	{
		k++;
	}
	//Console.WriteLine($"number of digits = {k}");

	// reset rc to zero
	rc = 0;
	for (int i = 0; i < k; i++)
	{
		int a = (pn / (int)Math.Pow(10, i));
		int b = (pn / (int)Math.Pow(10, i + 1)) * 10;
		int c = a - b;
		//	Console.WriteLine($"{i}) {a} - {b} = {a - b}");

		// add each digit to rc by multiplying by a factor of 10 for each
		// to reverse I have to reverse i 
		// why k - i - 1 instead of k - i? b/c k - i for i -> (i<k) -> 0..3 would be
		// (k - i) -> 4..1 but we want 3..0 so need the (i - 1) instead of i
		rc += (c * (int)Math.Pow(10, k - i - 1)) * sign;
	}

	return rc;
}

void TestInteger(Stopwatch watch, int n)
{
	int rc = 0;
	const int iterations = 100000;

	Console.WriteLine($"n = {n}");

	watch.Reset();
	watch.Start();
	for (int t = 0; t < iterations; t++) rc = ReverseUsingStringMethod(n);
	watch.Stop();
	Console.WriteLine($"1) {rc} elapsed time = {watch.Elapsed}");


	watch.Reset();
	watch.Start();
	for (int t = 0; t < iterations; t++) rc = ReverseUsingMath(n);
	watch.Stop();
	Console.WriteLine($"2) {rc} elapsed time = {watch.Elapsed}");
}