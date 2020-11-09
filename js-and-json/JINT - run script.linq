<Query Kind="Program">
  <NuGetReference>jint</NuGetReference>
  <Namespace>Jint</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.Collections.ObjectModel</Namespace>
  <Namespace>System.Dynamic</Namespace>
</Query>

void Main()
{
	var engine = new Engine()
		.SetValue("console", new JsConsole());
	string formatScript =
	@"
	console.log('---------------');
	function f(x) {
		var rc = 0;
		for(var i in x.keys) {
			var k = x.keys[i];
			rc = rc + x.dict[k];
		}
		return rc;
	}
	
	function g(x) {
		var rc = 0;
		for(var i in x) {
			var k = x[i];
			//rc = rc + k;
			rc += k;
		}
		return rc;
	}

	console.log(""aBC"".toLowerCase().substring(0, 2) === ""ab"");
	
	console.log('===============');
	";
	DateTime now = DateTime.Now;
	engine.Execute(formatScript);

	var s = (new List<string>() { "a", "b", "c" }).AsReadOnly();
	//var rc = engine.Invoke("f", s.ToArray());
	//rc.Dump();
	//rc.AsArray().Dump();

	var s2 = new Dictionary<string, int>() { { "a", 1 }, { "b", 2 }, { "c", 3 } };
	var sx = new { dict = s2, keys = s2.Keys.ToArray() };
	//s2.Keys.to
	var rc2 = engine.Invoke("f", sx); 
	Console.WriteLine(rc2);
	//rc2.Dump();

	var s3 = new Dictionary<string, int>() { { "a", 1 }, { "b", 2 }, { "c", 3 } };
	
	dynamic sx3 = s3.Aggregate(new ExpandoObject() as IDictionary<string, Object>, (working, next) => { working.Add(next.Key, next.Value); return working; });
	var rc3 = engine.Invoke("g", sx3);
	Console.WriteLine(rc2);
	//rc2.Dump();
}

public class JsConsole
{
	public void log(string message)
	{
		Console.WriteLine(message);
	}
	
	public T[] ToArray<T>(IEnumerable<T> o)
	{
		return o.ToArray();
	}
}

