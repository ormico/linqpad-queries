<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async Task Main()
{
	Task<Guid> toRemove2 = null;
	List<Task<Guid>> tlist2 = new List<Task<Guid>>();

	toRemove2 = null;
	tlist2 = new List<Task<Guid>>();
	tlist2.Add(Foo(Guid.NewGuid(), 5, "beta"));
	tlist2.Add(Foo(Guid.NewGuid(), 7, "gamma"));
	tlist2.Add(Foo(Guid.NewGuid(), 3, "alpha"));

	int loop = 0;
	while(tlist2!=null && tlist2.Any())
	{
		Console.WriteLine("loop={0}", ++loop);
		// Task.WaitAny() returns index of task that completed in the array that was passed
		int i = Task.WaitAny(tlist2.ToArray(), TimeSpan.FromSeconds(2));
		if(i >= 0)
		{			
			toRemove2 = tlist2[i];
			await toRemove2.Dump();
			tlist2.Remove(toRemove2);
			Console.WriteLine("tlist.Count -> {0}", tlist2.Count);
		}
		else
		{
			Console.WriteLine("N/A");
		}
	}

	Console.WriteLine("Waiting to exit");
	System.Threading.Thread.Sleep(TimeSpan.FromSeconds(30));

	return;
}

async Task<Guid> Foo(Guid key, int seconds, string s)
{
	// Task.Delay() is like an awaitable sleep
	await Task.Delay(seconds * 1000);
	Console.WriteLine("Foo -> '{0}'", s);
	return key;
}
