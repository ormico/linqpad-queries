<Query Kind="Program" />

void Main()
{
	
	// "John Smith" (O365) john@ms.com; "John Smith" (Azure) johnS@ms.com;alice@ms.com;bot(Test)@ms.com;
	// Display Name double quotes
	// () comment
	// email 
	// record delimiter is semi-colon	
	
	// return all email addresses
	//string input = @"""John Smith"" (O365) john@ms.com; ""John Smith"" (Azure) johnS@ms.com;alice@ms.com;bot(Test)@ms.com;zack@example.com";
	//string input = @"""John \"bob\" Smith"" (O(aaa365) john@ms.com; ""John Smith"" (Azure) johnS@ms.com;alice@ms.com;bot(Test)@ms.com;zack@example.com";
	string input = @"""John  Smith"" (O365) john@ms.com; ""John Smith"" (Azure) johnS@ms.com;alice@ms.com;bot(Test)@ms.com;zack@example.com";
	var emails = GetEmailsFromRecords(input);
	emails.Dump();
}

List<string> GetEmailsFromRecords(string records)
{
    if(string.IsNullOrWhiteSpace(records))
    {
        // how to handle empty record? return null or empty list or throw exception?
        return null;
    }
    
    List<string> recordsList = new List<string>();
    
    //todo: parse into seperate records by semi-colon; 
    StringBuilder sb = new StringBuilder();
    string state = "start";
    foreach(char c in records)
    {
        if(state == "start")
        {
            if(c == '(')
            {
                // comment
                state = "comment";
                continue;
            }
            else if(c == '"')
            {
				// display name
				state = "display-name";
				continue;
			}

			if(c == ';')
            {
                recordsList.Add(sb.ToString().Trim());
                sb.Clear();
            }
            else
            {
                sb.Append(c);
            }
        }
        else if(state == "comment" || state == "display-name")
        {
            if(state == "comment" && c == ')' )
            {
            
                state = "start";            
            }

            if(state == "display-name" && c == '\\')
            {
                state = "display-name-escape-char";
            }

            
            if(state == "display-name" && c == '"')
            {
            
                state = "start";
            }

			// do nothing
			continue;
		}
	}

	if(sb.Length > 0)
    {
        recordsList.Add(sb.ToString().Trim());
    }
    
    return recordsList;
}
