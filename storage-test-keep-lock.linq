<Query Kind="Program">
  <NuGetReference>Azure.Storage.Blobs</NuGetReference>
  <Namespace>Azure</Namespace>
  <Namespace>Azure.Storage.Blobs</Namespace>
  <Namespace>Azure.Storage.Blobs.Specialized</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async Task Main()
{
	const string storageKey = "";
	const string storageConnectionString = "";
	var cancelSrc = new CancellationTokenSource();

	var z = new AzureMutex(storageConnectionString, "beta");
	if(z.AquireLock())
	{
		System.Threading.Thread.Sleep(TimeSpan.FromSeconds(20));
		z.ReleaseLock();
	}
	else
	{
		Console.WriteLine("Failed to aquire lock!");
	}
	
	cancelSrc.Cancel();
	System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
}

// can use this for Leader Election Pattern
// for non-.net clients either write this same code in other lang like python
// or can write a utility that will aquire and keep a lock for timespan or until killed or provide a way to signal to exit

//todo: test if this works with storage simulator

//todo: rename to AzureMutex or something.
//todo: move hardcoded values to params or consts or defaults
public class AzureMutex: IDisposable
{
	readonly string id;
	Timer timer;
	BlobContainerClient bcc;
	PageBlobClient pbc;
	BlobLeaseClient blc;
	Azure.Storage.Blobs.Models.BlobLease lease;
	
	public AzureMutex(string connectionString, string id)
	{
		this.id = id;
		this.bcc = new BlobContainerClient(connectionString, "keys");
		this.pbc = new PageBlobClient(connectionString, "keys", id);
	}
	
	public bool AquireLock()
	{
		bool rc = false;
		
		try
		{
			//todo: get lock	
			var bccResponse = bcc.CreateIfNotExists();
			bccResponse?.Value.Dump();
			
			var pbcResponse = pbc.CreateIfNotExists(0);
			pbcResponse?.Value.Dump();
			blc = pbc.GetBlobLeaseClient();
	
			//todo: make sure we don't already have a lock (timer != null)
			var lease = blc.Acquire(TimeSpan.FromSeconds(60));
			lease?.Dump();
			this.lease = lease?.Value;		
			
			Console.WriteLine("lock aquired: {0}", this.id);
	
			//start timer that will make sure lock doesn't expire
			//this.timer = new Timer(this.KeepLock, null, TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(45));
			this.timer = new Timer(this.KeepLock, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(15));
			
			rc = true;
		}
		catch(RequestFailedException e)
		{
			bcc = null;
			pbc = null;
			blc = null;
			Console.WriteLine("Failed to aquire lease");
		}
		return rc;
	}
	
	public void ReleaseLock()
	{
		Console.WriteLine("Releasing Lock");
		this.blc?.Release();
		this.timer?.Dispose();
		this.timer = null;
	}
	
	private void KeepLock(object state)
	{
		Console.WriteLine("renew lock...");
		blc.Renew();
	}

	public void Dispose()
	{
		//todo: check for GC
		this.ReleaseLock();
	}
}
