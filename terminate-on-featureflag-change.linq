<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>LINQPad.Controls</Namespace>
</Query>

/*
Title:
Terminate worker on feature flag state change

Author:
Zack Moore
https://github.com/ormico/

Description:
Your application may want to restart or perform re-initilization if a feature flag changes.
In this example, we start a background timer using System.Threading.Timer that will check
the state of the feature flag.

After reading the initial value of the flag, if the state changes then the timer will
trigger the cancelation token via the CancellationTokenSource object.

written targetting .net framework 4.8.1
but it should work on .net 8 also
*/

static Timer featureFlagTimer;
static readonly object featureFlagReEnterLock = new object();

async Task Main(string[] args)
{
	// Get Starting Parameters
	var passCancellationTokenToWorkerLoop = Util.ReadLine<bool>("Pass CancellationToken to WorkerLoop()?", true);
	var featureFlagTimerIntervalSeconds = Util.ReadLine<double>("Feature Flag Timer Interval (seconds)", 3.0);
	var workerLoopIterations = Util.ReadLine<int>("Worker Loop Iterations before Return", 200);
	var workerLoopIntervalSeconds = Util.ReadLine<double>("Worker Loop Interval per Loop (seconds)", 1.0);
	var intervalSecondsBeforeForcedExit = Util.ReadLine<double>("Interval to wait after cancellation before forced Exit (seconds)", 2.0);
	featureFlagTriggerValue = Util.ReadLine<int>("How many times will the Featue Flag be checked before the simulation changes the value?", 5);

	CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
	var ffState = new FeatureFlagProperties
	{
		CancellationTokenSource = cancellationTokenSource,
		IntervalSecondsBeforeForcedExit = intervalSecondsBeforeForcedExit
	};

	var multiThreadedButton = new Button("Cancel", b => cancellationTokenSource.Cancel()).Dump();
	multiThreadedButton.IsMultithreaded = true;

	Log("Starting application...");

	// Initialize feature flag value
	ffState.FeatureFlagInitialValue = FetchFeatureFlagStatus();

	// Timer setup
	var featureFlagTimerInterval = TimeSpan.FromSeconds(featureFlagTimerIntervalSeconds);
	Log($"Starting feature flag monitor with {featureFlagTimerInterval} timer interval");
	featureFlagTimer = new Timer(CheckFeatureFlag, ffState, TimeSpan.Zero, featureFlagTimerInterval);
	
	try
	{
		// Perform work
		if(passCancellationTokenToWorkerLoop)
		{
			Log("awaiting WorkerLoop() with cancellation token");
			await WorkerLoop(workerLoopIterations, workerLoopIntervalSeconds, cancellationTokenSource.Token);
		}
		else
		{
			Log("awaiting WorkerLoop() WITHOUT cancellation token");
			await WorkerLoop(workerLoopIterations, workerLoopIntervalSeconds);
		}
	}
	finally
	{
		// if app exits w/o cancelling WorkerLoop() finally block doesn't run
		
		// Cleanup resources
		featureFlagTimer.Dispose();
		Log("Application shutting down gracefully.");
	}
}

async Task WorkerLoop(int iterations, double workIntervalSeconds, CancellationToken cancellationToken = default(CancellationToken))
{
	var workInterval = TimeSpan.FromSeconds(workIntervalSeconds);
	Log($"begining work...{iterations} iterations");
	for (int i = 0; i < iterations; i++)
	{
		// if cancellationToken is default(CancellationToken) then it will not
		// be cancellable
		if(cancellationToken.CanBeCanceled == false)
		{
			Log($"work1...{workInterval}");
			// WorkerLoop() will keep running until either number of loops (i) runs out
			// or app is terminated. 
			
			// can use System.Threading.Thread.Sleep(workInterval) 
			// or cancellationToken.WaitHandle.WaitOne(workInterval)
			cancellationToken.WaitHandle.WaitOne(workInterval);
		}
		else
		{
			Log($"work2...{workInterval}");
			cancellationToken.WaitHandle.WaitOne(workInterval);

			if (cancellationToken.IsCancellationRequested)
			{
				Log("cancellation requested. breaking out of work loop.");
				break;
			}
		}
	}
	Log("stopping work!");
}

void CheckFeatureFlag(object state)
{
	var ffState = state as FeatureFlagProperties;

	try
	{	        
		if(state == null)
			throw new ArgumentNullException(nameof(state));
		
		if(ffState == null)
			throw new ApplicationException("Feature Flag State is invalid");
		
		// Prevent re-entrance
		if (!Monitor.TryEnter(featureFlagReEnterLock))
		{
			Log("Previous execution still in progress. Skipping this iteration.");
			return;
		}
	}
	catch (Exception ex)
	{
		Log(ex.ToString());
		return;
	}

	Log("Checking Feature Flag");
	try
	{
		// feature flag check
		bool featureFlagCurrentValue = FetchFeatureFlagStatus();
		
		if (ffState.FeatureFlagInitialValue != featureFlagCurrentValue)
		{
			Log("Feature flag status changed. Initiating shutdown...");
			ffState.CancellationTokenSource.Cancel();
			// give application enough time to gracefully exit
			var forcedExitInterval = TimeSpan.FromSeconds(ffState.IntervalSecondsBeforeForcedExit);
			Log($"waiting {forcedExitInterval} before forcing EXIT");
			System.Threading.Thread.Sleep(forcedExitInterval);
			// system still running; triggering exit
			Log("calling Exit(1)");
			Environment.Exit(1);
		}
	}
	catch (Exception ex)
	{
		Log($"Error checking feature flag: {ex.Message}");
	}
	finally
	{
		Monitor.Exit(featureFlagReEnterLock);
	}
}

static int featureFlagCount = 0;
static int featureFlagTriggerValue = 0;

static bool FetchFeatureFlagStatus()
{
	// Placeholder for actual feature flag check logic
	// Simulate changing feature flag state change after 3 calls
	return (featureFlagCount++) == featureFlagTriggerValue;
}

static void Log(string msg)
{
	var currentThread = System.Threading.Thread.CurrentThread;
	Console.WriteLine($"[{DateTime.UtcNow:o}] {currentThread.Name} ({currentThread.ManagedThreadId}): {msg}");
}

class FeatureFlagProperties
{
	public CancellationTokenSource CancellationTokenSource { get; set; }
	public bool FeatureFlagInitialValue { get; set; }
	public double IntervalSecondsBeforeForcedExit { get; set; }
}