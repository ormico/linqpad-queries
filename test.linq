<Query Kind="Program" />

/*
Many multithreaded applications need timers to be maintained to handle thread and task scheduling. 
These timers are system calls made to the OS, which keeps track of all the timer requests and 
generates events to notify the applications. There can be several applications using this OS 
feature at one point in time. Design this timer management component of the OS.
*/
void Main()
{
	
}
/*
class MinHeap<T>
{
	void Add<T>(T n)
	{
	}
	
	T Get<T>()
	{
		return null;
	}
}
*/

class TimerData
{
	public long NextEpoc;
	public string Cron;
	public Action Callback;
}

class CentralTimer
{
	object TimerDataLock = new object();
	MinHeap<Timers> Timers = new MinHeap<Timers>();
	readonly ILogger<CentralTimer> Logger;
	
	public CentralTimer(ILogger<CentralTimer> logger)
	{		
		this.Logger = logger;	
	}
	
	public void AddTimer(string cron, Action callback)
	{
		if(string.IsNullOrWhiteSpace(cron))
		{
			throw new ArgumentNullException(nameof(cron));
		}
		
		if(callback == null)
		{
			throw new ArgumentNullException(nameof(callback));
		}
		
		var newTd = new TimerData
		{
			NextEpoc = CalculateNextEpoc(cron),
			Cron = cron,
			Callback = callback
		};
		
		if(newTd.NextEpoc != -1)
		{
			lock (TimerDataLock)
			{
				Timers.Add(newTd);
			}
		}
		else
		{
			throw new TimerException("Bad Timer.Timer will never execute");
		}
	}
	
	// call this whenever OS Timer Runs
	public void RunExpiredTimers()
	{
		try
		{
			
		lock(TimerDataLock)
		{
			var now = GetEpoc(DateTime.Now);

			var peek = Timers.Peek();
			while(peek != null && peek.NextEpoc > now)
			{
				var top = Timers.GetTop();
				try
				{
					top?.Callback();
					top.FailureCount = 0;
				}
				catch(Exception e)
				{
					this.Logger.LogException(e, "An unhandled exception was caught during the execution of a timer callback");
				}
				
				top.NextEpoc = CalculateNextEpoc(top.Cron);
				if(top.NextEpoc != -1)
				{
					Timers.Add(top);
				}

				peek = Timers.Peek();
			}
			}
		}
		catch()
		{
			
		}
	}
	
	private long CalculateNextEpoc(string cron)
	{
		// based on cron string passed in, calculate the next time
		// this timer should fire and return the output as 
		// epoc time
		return 0;
	}
}

