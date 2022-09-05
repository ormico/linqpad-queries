<Query Kind="Program" />

void Main()
{
}

namespace Solution
{
	public class Server
	{

		public static void Main(string[] args)
		{
			// you can write to stdout for debugging purposes, e.g.
			Console.WriteLine("This is a debug message");

			perfDataExpiration = new Timer(perfDataExpirationCleanup, TimeSpan.FromMinutes(21), Timespan.FromMinutes(21));
		}

		LinkedList<UriDat> performanceData = new LinkedList<UriData>();
		Timer perfDataExpiration;
		object perfDataLock = new object();

		void perfDataExpirationCleanup(object state)
		{
			lock (perfDataLock)
			{
				DateTime cleanupTime = DateTime.Now;
				DateTime expirationTime = cleanupTime.AddMinues(-20);
				for (int i = performanceData.Length - 1; i > 0; i--)
				{
					UriData current = performanceData[i];
					if (current.eventTime < expirationTime)
					{
						performanceDate.TruncateListAt(i);
						break;
					}
				}
			}
		}

		void onRequestCompleted(string uri, long cpuUsed)
		{
			lock (perfDataLock)
			{
				performanceData.Add(new UriData { uri = uri, cpuUsed = cpuUsed, eventTime = DateTime.Now });
			}

		}

		List<(string, long)> getTop5()
		{
			List<(string, long)> rc = new List<(string, long)>();
			lock (perfDataLock)
			{
				Dictionary<string, long> aggPerfData = new Dictionary<string, long>();

				foreach (var i in performanceData)
				{
					if (aggPerfData.ContainsKey(i.uri))
					{
						long c = aggPerfData[i.uri];
						aggPerfData[i.uri] = c + i.cpuTime;
					}
					else
					{
						appPerfData.Add(i.uri, i.cpuTime);
					}
				}

				// sort dictionary by values and return top 5 values and keys
				SortedDictionary<string, long> x = new SortedDictionary<string, long>((v1, v2) =>
				{
					// add sort
				});
				rc = x.KeyValues.Take(5);
			}
			return rc;
		}
	}

	class UriDataAgg
	{
		public string uri { get; set; }
		public long cpuUsed { get; set; }
	}

	class UriData
	{
		public string uri { get; set; }
		public long cpuUsed { get; set; }
		public DateTime eventTime { get; set; }
	}

}
