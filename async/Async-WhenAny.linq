<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async Task Main()
{
	MyTask toRemove = null;
	List<Task<Guid>> tlist = new List<Task<Guid>>();
	tlist.Add(FooFactory(5, "beta"));
	tlist.Add(FooFactory(7, "gamma"));
	tlist.Add(FooFactory(3, "alpha"));
	
	Task<Guid> t = null;
	
	t = await Task.WhenAny(tlist.ToArray());
	await t.Dump();
	toRemove = taskList.FirstOrDefault(i => i.Key == t.Result);
	tlist.Remove(toRemove.Task);
	taskList.Remove(toRemove);
	Console.WriteLine("tlist.Count -> {0}", tlist.Count);

	t = await Task.WhenAny(tlist.ToArray());
	await t.Dump();
	toRemove = taskList.FirstOrDefault(i => i.Key == t.Result);
	tlist.Remove(toRemove.Task);
	taskList.Remove(toRemove);
	Console.WriteLine("tlist.Count -> {0}", tlist.Count);

	t = await Task.WhenAny(tlist.ToArray());
	await t.Dump();
	toRemove = taskList.FirstOrDefault(i => i.Key == t.Result);
	tlist.Remove(toRemove.Task);
	taskList.Remove(toRemove);
	Console.WriteLine("tlist.Count -> {0}", tlist.Count);

	Console.WriteLine("---------------------------------------------------------");

	Task<Guid> toRemove2 = null;
	List<Task<Guid>> tlist2 = new List<Task<Guid>>();
	tlist2.Add(Foo(Guid.NewGuid(), 5, "beta"));
	tlist2.Add(Foo(Guid.NewGuid(), 7, "gamma"));
	tlist2.Add(Foo(Guid.NewGuid(), 3, "alpha"));

	toRemove2 = await Task.WhenAny(tlist2.ToArray());
	await toRemove2.Dump();
	tlist2.Remove(toRemove2);
	Console.WriteLine("tlist.Count -> {0}", tlist2.Count);

	toRemove2 = await Task.WhenAny(tlist2.ToArray());
	await toRemove2.Dump();
	tlist2.Remove(toRemove2);
	Console.WriteLine("tlist.Count -> {0}", tlist2.Count);

	toRemove2 = await Task.WhenAny(tlist2.ToArray());
	await toRemove2.Dump();
	tlist2.Remove(toRemove2);
	Console.WriteLine("tlist.Count -> {0}", tlist2.Count);

	Console.WriteLine("---------------------------------------------------------");

	toRemove2 = null;
	tlist2 = new List<Task<Guid>>();
	tlist2.Add(Foo(Guid.NewGuid(), 5, "beta"));
	tlist2.Add(Foo(Guid.NewGuid(), 7, "gamma"));
	tlist2.Add(Foo(Guid.NewGuid(), 3, "alpha"));

	while(tlist2!=null && tlist2.Any())
	{
		toRemove2 = await Task.WhenAny(tlist2.ToArray());
		await toRemove2.Dump();
		tlist2.Remove(toRemove2);
		Console.WriteLine("tlist.Count -> {0}", tlist2.Count);
	}

	System.Threading.Thread.Sleep(TimeSpan.FromSeconds(30));

	return;
}

List<MyTask> taskList = new List<UserQuery.MyTask>();

Task<Guid> FooFactory(int seconds, string s)
{
	MyTask rc = new MyTask
	{
		Key = Guid.NewGuid()
	};
	rc.Task = Foo(rc.Key, seconds, s);
	taskList.Add(rc);
	
	return rc.Task;
}

async Task<Guid> Foo(Guid key, int seconds, string s)
{
	//System.Threading.Thread.Sleep(TimeSpan.FromSeconds(seconds));
	await Task.Delay(seconds * 1000);
	Console.WriteLine("Foo -> '{0}'", s);
	return key;
}

class MyTask
{
	public Guid Key {get; set;}
	public Task<Guid> Task {get; set;}
}

/*
private static async Task<string> FirstRespondingUrlAsync(string urlA, string urlB)
{
	// Start both downloads concurrently.
	Task<string> downloadTaskA = _httpClient.GetHtmlResponseAsync(urlA);
	Task<string> downloadTaskB = _httpClient.GetHtmlResponseAsync(urlB);

	// Wait for either of the tasks to complete.
	Task<string> completedTask = await Task.WhenAny(downloadTaskA, downloadTaskB);

	// Return the length of the data retrieved from that URL.
	string data = await completedTask;

	return data;
}
*/