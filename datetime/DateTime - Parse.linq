<Query Kind="Program" />

void Main()
{
	//https://msdn.microsoft.com/en-us/library/az4se3k1(v=vs.110).aspx
	//https://msdn.microsoft.com/en-us/library/8kb3ddd4(v=vs.110).aspx
	MyParse("4/7/2017", "d");
	MyParse("4/7/2017 1:01 PM", "g");
	MyParse("4/7/2017 1:01:10 PM", "G");
	MyParse("2017-04-07T12:59:08.483Z");
	MyParse("2014-12-11T14:19:11", "s");
	MyParse("1999-12-31T23:00:00.0000000-05:00");
	
	string[] formats =
	{
		//"yyyy-MM-dd'T'HH:mm:ss.fff",
		"yyyy-M-d'T'HH:mm:ss.fff",
		"M/d/yyyy",
		"O"
	};
	MyParse("2013-09-17T14:27:24.733", formats);
	MyParse("2013-09-17T00:27:24.733", "yyyy-MM-dd'T'hh:mm:ss.fff");
	MyParse("2014-12-11T14:19:11.17", "yyyy-MM-dd'T'HH:mm:ss.ff");
	MyParse("Jan-2001", "MMM-yyyy");
}

// Define other methods and classes here
void MyParse(string str, string format)
{
	MyParse(str, new string[] { format });
}

void MyParse(string str, string[] formats)
{
	string msg;
	DateTime v;
	IFormatProvider fp = new System.Globalization.CultureInfo("en-US", true);
	if (DateTime.TryParseExact(str, formats, fp, System.Globalization.DateTimeStyles.AssumeLocal, out v))
	{
		msg = $"'{str}' parses to '{v:o}'";
	}
	else
	{
		msg = $"'{str}' didn't parses";
	}
	Console.WriteLine(msg);
}

void MyParse(string str)
{
	string[] formats = new string[]
	{
		"d",
		"g",
		"G",
        "yyyy-MM-dd'T'HH:mm:ss.fffffffZ",
		"yyyy-MM-dd'T'HH:mm:ss.ffffffZ",
		"yyyy-MM-dd'T'HH:mm:ss.fffffZ",
		"yyyy-MM-dd'T'HH:mm:ss.ffffZ",
		"yyyy-MM-dd'T'HH:mm:ss.fffZ",
		"yyyy-MM-dd'T'HH:mm:ss.ffZ",
		"yyyy-MM-dd'T'HH:mm:ss.fZ",
		"yyyy-MM-dd'T'HH:mm:ssZ",
		"O"
	};
	MyParse(str, formats);
}