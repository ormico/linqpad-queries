<Query Kind="Program">
  <NuGetReference>jint</NuGetReference>
  <Namespace>Jint</Namespace>
</Query>

void Main()
{
	DataUtil dataUtil = new DataUtil();
	var engine = new Engine()
		.SetValue("console", new JsConsole())
		.SetValue("util", dataUtil);
	string formatScript =
	@"
	var formatter={
		process: function(p)
		{
			console.log('postProcess(p) -> ' + p.Name + ' | ' + p.Value);		
			var rc = p.Value;
			if(p.Name === 'Id')
	        {
				rc = p.Value * 99;
			}
			else if(p.Name === 'Alpha')
			{
				rc = p.Value.trim();
			}
			else if(p.Name === 'Beta')
			{
				//if(p.Value == '//')
				//{
				//	p.Value = null;
				//}
				rc = util.GetDateTimeString(p.Value);
			}
			else if(p.Name === 'Gamma')
			{
				rc = util.ParseDateTime(p.Value);
			}
			else if(p.Name === 'Zeta')
			{
				//rc = util.ParseDateTime(p.Value);
				rc = util.GetDateTimeString(p.Value);
			}
			// JSON.stringify() doesn't work on a .net object
			//console.log(JSON.stringify(p));
			console.log(JSON.stringify({ n: p.Name, v: p.Value }));
			return rc;
		}
	};";
	DateTime now = DateTime.Now;
	Console.WriteLine("now='{0}' '{0:o}'", now);
	MyData md = new MyData()
	{
		Id = 101,
		Alpha = " blah ",
		Beta = "//",
		Gamma = now,
		Delta = 1000.00M,
		Zeta = DateTime.Now.ToString("o")
	};
	//md.Dump();
	MyData md2 = new MyData();
	foreach (var prop in md.GetType().GetProperties())
	{
		engine.Execute(formatScript);
		object propVal = prop.GetValue(md);
		
		// this step is necessary. if you don't the DateTime value will shift hours by the Timezone value
		// when it goes through the JavaScript layer
		if (propVal is DateTime)
		{
			propVal = ((DateTime)propVal).ToString("o");
		}
		//var p2 = new { Name = prop.Name, Value = prop.GetValue(md) };
		var p2 = new { Name = prop.Name, Value = propVal };
		//p2.Dump();
		var v = engine.Invoke("formatter.postProcess", p2);
		
		object x = v.ToObject();
		if (x is DateTime)
		{
			// this is coming over as UTC
			x = ((DateTime)x).ToLocalTime();
		}
		Console.WriteLine("foreach -> '{0}'", x??"<null>");

		// for some reason v==null test is not working so I also added x==null test.
		if (v == null || x == null)
		{
			prop.SetValue(md2, null);
		}
		else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
		{
			prop.SetValue(md2, (int)v.AsNumber());
		}
		else if (prop.PropertyType == typeof(double) || prop.PropertyType == typeof(double?))
		{
			prop.SetValue(md2, (double?)v.AsNumber());
		}
		else if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(decimal?))
		{
			prop.SetValue(md2, (decimal?)v.AsNumber());
		}
		else if (prop.PropertyType == typeof(string))
		{
			prop.SetValue(md2, v.AsString());
		}
		else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
		{
			prop.SetValue(md2, (DateTime)x);
		}
	}
	md2.Dump();
	Console.WriteLine("md2.Alpha -> '{0}'", md2.Alpha);

	if (md.Gamma != md2.Gamma)
	{
		Console.WriteLine("Error. Values do not match. md.Gamma '{0}' md2.Gamma '{1}'", md.Gamma, md2.Gamma);
	}

	TimeSpan ts = md.Gamma.Value - md2.Gamma.Value;
	if (ts.TotalMilliseconds > 10)
	{
		Console.WriteLine("Error. Values not within 10 Milliseconds. TotalMilliseconds={0} md.Gamma '{1:o}' md2.Gamma '{2:o}'", ts.TotalMilliseconds, md.Gamma, md2.Gamma);
	}
	else
	{
		Console.WriteLine("Success! Values within 10 Milliseconds. TotalMilliseconds={0} md.Gamma '{1:o}' md2.Gamma '{2:o}'", ts.TotalMilliseconds, md.Gamma, md2.Gamma);
	}
}

public class MyData
{
	public int Id { get; set; }
	public string Alpha { get; set; }
	public string Beta { get; set; }
	public DateTime? Gamma { get; set; }
	public decimal Delta { get; set; }
	public string Zeta { get; set; }
}

public class DataUtil
{
	public DateTime? ParseDateTime(string val)
	{
		Console.WriteLine("ParseDateTime(val -> '{0}')", val);
		DateTime? rc = null;
		if (!string.IsNullOrWhiteSpace(val) && !string.Equals(val.Trim(), "//", StringComparison.OrdinalIgnoreCase))
		{
			string tmp = val;
			DateTime dttmp;
			if (DateTime.TryParseExact(tmp, "o", null, System.Globalization.DateTimeStyles.None, out dttmp))
			{
				rc = dttmp;
			}
			else
			{
				Console.WriteLine("'{0}' failed to parse as DateTime", val);
			}
		}
		Console.WriteLine("'{0}' <- ParseDateTime(string val)", rc);
		return rc;
	}

	public string GetDateTimeString(string val)
	{
		string rc = null;
		DateTime? tmp = ParseDateTime(val);
		if (tmp.HasValue)
		{
			rc = tmp.Value.ToString("o");
		}
		return rc;
	}
}

public class JsConsole
{
	public void log(string message)
	{
		Console.WriteLine(message);
	}
}