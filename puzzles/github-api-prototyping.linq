<Query Kind="Program" />

void Main()
{
	
}

class DataStorage
{
	object StorageLock = new object();
	Dictionary<string, RegionDataStore> Regions = new Dictionary<string, UserQuery.RegionDataStore>();
	
	public DataStorage()
	{
	}
	
	StoreDataResult StoreObject(string region, string data)
	{
		StoreDataResult rc = null;

		//todo: if region doesn't exist create region
		
		//todo: store data
		
		return rc;
	}
	
	string CreateUniqueId()
	{
		//todo: is NewGuid() ok or should we create a cryptographic GUID?
		// use N formatter to remove all un-necessary characters
		// will help with transportability and prevent un-necessary escaping
		return Guid.NewGuid().ToString("N");
	}
	
	GetDataResult GetObject(string region, string objectId)
	{
		GetDataResult rc = null;
		RegionDataStore rds = null;
		
		lock(this.StorageLock)
		{	
			//todo: if region doesn't exist return NOT FOUND
			//todo: make Regions Dictionary case insensitive on RegionName
			if(this.Regions.ContainsKey(region))
			{
				rds = this.Regions[region];
			}
		}		
		
		// if region does exist, then search for objectId
		if(rds == null)
		{
			//todo: should I add region name to result?
			rc = new GetDataResult(objectId);
		}
		else
		{
			// RegionDataStore is threadsafe, so we can
			// release StorageLock lock as soon as possible so we 
			// don't block requests for data for other regions
			rc = rds.Get(objectId);
		}

		return rc;
	}	
}

class StoreDataResult
{
	public StoreDataResult(string ObjectId, uint DataSize)
	{
		this.ObjectId = ObjectId;
		this.DataSize = DataSize;
	}

	public string ObjectId { get; }
	public uint DataSize { get; }
}

class GetDataResult
{
	public GetDataResult(string ObjectId, string DataValue)
	{
		this.ObjectId = ObjectId;
		this.Data = DataValue;
		this.Found = true;
	}

	public GetDataResult(string ObjectId)
	{
		this.ObjectId = ObjectId;
		this.Found = false;
	}

	public string ObjectId { get; }
	public string Data { get; }
	public bool Found { get; }	
}

class RegionDataStore
{
	object lockObject = new object();
	
	public string RegionName { get; }
	
	// store DATA in Dictionary for fast lookup by objectID
	// and in HashSet<> for fast lookup by string hash to
	// to test if DATA is already stored
	// Dictionary<> ContainsKey() is O(N)
	// HashSet<> Contains() is O(1)
	
	// this won't work if DATA is large but this Excercise 
	// specifies that DATA can be stored in Memory, Disk, or where ever 
	// so assuming that DATA will be small enought to fit in memory
	// should be safe.
	// future optimization may expand to handle large DATA items.
	Dictionary<string, string> DataDict = new Dictionary<string, string>();
	HashSet<string> DataHash = new HashSet<string>();
	
	public bool Contains(string data)
	{
		lock(lockObject)
		{
			return this.DataHash.Contains(data);
		}
	}
	
	public GetDataResult Get(string objectId)
	{
		GetDataResult rc = null;
		
		lock(lockObject)
		{
			if(this.DataDict.ContainsKey(objectId))
			{
				rc = new GetDataResult(objectId, this.DataDict[objectId]);
			}
		}

		// don't hold lock any longer than necessary
		// if key not found, release lock then create 'not found' result
		if(rc == null)
		{
			rc = new GetDataResult(objectId);
		}

		return rc;
	}
	
	/// <summary>
	/// Remove Data item from store.
	/// If Data item is not found, Delete() does not error.
	/// After the operation, Data item will not be in store
	/// whether it was there at the begining of operation or not.
	/// </summary>
	public void Delete(string objectId)
	{
		lock (lockObject)
		{
			if (this.DataDict.ContainsKey(objectId))
			{
				this.DataHash.Remove(this.DataDict[objectId]);
				this.DataDict.Remove(objectId);
			}
		}
	}
}
