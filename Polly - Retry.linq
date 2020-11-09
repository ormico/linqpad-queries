<Query Kind="Program">
  <NuGetReference>Polly</NuGetReference>
  <Namespace>Polly</Namespace>
</Query>

void Main()
{
	for (int i = 0; i < 10; i++)
	{
		try
		{
			Console.WriteLine("i = {0} X = {1}",
				i,
				FailFirstFiveTimes());
		}
		catch (Exception e)
		{
			Console.WriteLine("i = {0} exception = {1}", i, e.Message);
		}
	}

	Reset();
	Console.WriteLine("-------------------------------------------------------------");

	// exception, count or index, context
	Policy.Handle<Exception>().Retry(6, onRetry: (e, i, c) =>
	{
		Console.WriteLine("i = {0} e = {1}", i, e?.Message);
	})
	.Execute(() =>
	{
		FailFirstFiveTimes();
		Console.WriteLine("success?");
	});
}

int countFailFirstFiveTimes = 0;

bool FailFirstFiveTimes()
{
	if (this.countFailFirstFiveTimes++ < 5)
	{
		throw new Exception("fail");
	}

	return true;
}

void Reset()
{
	this.countFailFirstFiveTimes = 0;
}