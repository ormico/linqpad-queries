<Query Kind="Program" />

/*
Parsing names is very, very difficult and when you take in cultural
differences may be impossible.
This script is just an example of possible algorithm(s) to try
when parsing a name cannot be avoided no matter how error prone.

* parses first and middle name into 'first name' field.
* does not handle titles or suffixes
*/
void Main()
{
	Test("Mores, Tom");
	Test("Mores, Thomas Frederick");
	Test("John Doe");
	Test("John T Doe");
}

void Test(string FN)
{
	string f;
	string l;
	ParseFullName(FN, out f, out l);
	Console.WriteLine("FullName: '{0}' FirstName: '{1}' LastName: '{2}'", FN, f, l);
}

public void ParseFullName(string FullName, out string FirstName, out string LastName)
{
  FirstName = string.Empty;
  LastName = string.Empty;

  StringBuilder NameBldr = new StringBuilder();

  if (!string.IsNullOrWhiteSpace(FullName))
  {
	string[] parts = FullName.Trim().Split(' ');
	if (parts.Length > 0)
	{
		// last, first[ middle]
		if(FullName.Contains(","))
		{
		  LastName = parts[0].Replace(",", "");

          bool first = true;
          for (int i = 1; i < parts.Length; i++)
          {
              if (!first)
              {
                  NameBldr.Append(" ");
              }
              else
              {
                  first = false;
              }
              NameBldr.Append(parts[i]);
          }

          FirstName = NameBldr.ToString();
		}
		// first[ middle] last
		else
		{
          LastName = parts[parts.Length - 1];

          bool first = true;
          for (int i = 0; i < parts.Length - 1; i++)
          {
              if (!first)
              {
                  NameBldr.Append(" ");
              }
              else
              {
                  first = false;
              }
              NameBldr.Append(parts[i]);
          }
          FirstName = NameBldr.ToString();
		}
	}
  }
}