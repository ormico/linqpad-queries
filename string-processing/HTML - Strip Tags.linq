<Query Kind="Program" />

void Main()
{
	StripTagsCharArray("<font color=red>Did Not Meet Projection</font>").Dump();
}

// http://stackoverflow.com/questions/4878452/remove-html-tags-in-string
public static string StripTagsCharArray(string source)
{
	char[] array = new char[source.Length];
	int arrayIndex = 0;
	bool inside = false;
	for (int i = 0; i < source.Length; i++)
	{
		char let = source[i];
		if (let == '<')
		{
			inside = true;
			continue;
		}
		
		if (let == '>')
		{
			inside = false;
			continue;
		}
		
		if (!inside)
		{
			array[arrayIndex] = let;
			arrayIndex++;
		}
	}
	
	return new string(array, 0, arrayIndex);
}