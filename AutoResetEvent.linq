<Query Kind="Program" />

static AutoResetEvent autoEvent = new AutoResetEvent(false);

static void Main()
{
	Console.WriteLine("Main starting.");
	
	ThreadPool.QueueUserWorkItem(new WaitCallback(WorkMethod), autoEvent);
	
	// Wait for work method to signal.
	autoEvent.WaitOne();
	Console.WriteLine("Work method 1 signaled.");
	
	//----------------
	// b/c using AutoResetEvent we shouldn't have to reset the wait event
	//----------------
	
	ThreadPool.QueueUserWorkItem(new WaitCallback(WorkMethod), autoEvent);
	
	// Wait for work method to signal.
	autoEvent.WaitOne();
	Console.WriteLine("Work method 2 signaled.\nMain ending.");
}

static void WorkMethod(object stateInfo) 
{
   Console.WriteLine("Work starting.");

   // Simulate time spent working.
   Thread.Sleep(TimeSpan.FromSeconds(10));

   // Signal that work is finished.
   Console.WriteLine("Work ending.");
   ((AutoResetEvent)stateInfo).Set();
}
