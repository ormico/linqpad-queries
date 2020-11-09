<Query Kind="Program" />

void Main()
{
	string csz = "Atlanta, GA 30303-1234";
	string csz2 = "San Francisco, CA 94101-1234";
	
	WriteCsz(csz);
	WriteCsz(csz2);
}

public void WriteCsz(string csz)
{
	string c,s,z;
	
	csz.ParseCSZ(out c, out s, out z);
	
	Console.WriteLine("city: '{0}'", c);
	Console.WriteLine("state: '{0}'", s);
	Console.WriteLine("zip: '{0}'", z);
	Console.WriteLine();
}

public static class StringEx
{
	public static void ParseCSZ(this string x, out string city, out string state, out string zip)
	{
		city = string.Empty;
		state = string.Empty;
		zip = string.Empty;
		
		if(x != null)
		{
			string[] w = x.Split(',');
			if(w!=null && w.Length > 0)
			{
				city = w[0].Trim();
				
				if(w.Length > 1)
				{
					//Console.WriteLine(">> w[1] '{0}'", w[1]);
					w = w[1].Trim().Split(' ');
					if(w!=null && w.Length > 0)
					{
						//Console.WriteLine(">>> w[0] '{0}'", w[0]);
						state = w[0].Trim();
						if(w.Length > 1)
						{
							zip = w[1].Trim();
						}					
					}				
				}
			}
		}
	}
}