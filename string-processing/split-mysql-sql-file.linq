<Query Kind="Program">
  <Namespace>LINQPad.Controls</Namespace>
</Query>

/*
TITLE: Split MySQL SQL into multiple files
DESCRIPTION:
This utility parses a SQL file that contains multiple create, drop, etc. commands into a seperate file per command.
This algorithm groups drop and create commands into the same file for the same object.

This algorithm probably doesn't handle every syntax variation, but is designed to make it easy to break up large files
with the most command basic syntax.

SQL OBJECTS HANDLED:
	* trigger
	* procedure

KNOWN LIMITATIONS:
	* does not handle if there are sub BEGIN END code blocks
	* does not preserve comment blocks that precede create or drop commands
*/
void Main()
{
	var fpInput = new FilePicker().Dump("Pick input file. Output files will be created in subfolder named './split-files/'");
	var button = new Button("Go").Dump("Click Button to start conversion");
	button.Click += (sender, args) =>
	{
		Console.WriteLine("-------> Running...");
		var fileName = fpInput.Text;
		Console.WriteLine(fileName);
		using StreamReader sr = new StreamReader(fileName);
		ParseSqlFile(sr, Path.Combine(Path.GetDirectoryName(fileName), "split-files"));
		Console.WriteLine("<------- Complete!");
	};
}

string outputFolder = "";

void ParseSqlFile(StreamReader sr, string outputFolder)
{
	this.outputFolder = outputFolder;
	
	bool inString = false;
	bool inSingleLineComment = false;
	bool inMultiLineComment = false;
	var state = States.fileStart;
	int i = 0;
	int ci = sr.Read();
	if(ci == -1)
	{
		Console.WriteLine("no text");
		return;
	}
	i++;
	
	char c = ' ';
	string word;
	string name = "UNSET";
	StringBuilder wordBuilder = new StringBuilder();
	StringBuilder buffer = new StringBuilder();	
	
	while (ci != -1 && state != States.fileEndOf)
	{
		c = (char)ci;
		wordBuilder.Append(c);
		buffer.Append(c);

		switch (state)
		{
			case States.fileStart:
				if (c == '-')
				{
					SetState(States.commentStart, ref state, c);
				}
				else if(char.IsWhiteSpace(c))
				{
					// check if it's a recognized word
					word = wordBuilder.ToString().Trim();
					if(word.Trim().Length > 0)
					{					
						wordBuilder.Clear();
						if(string.Equals(word, "drop", StringComparison.InvariantCultureIgnoreCase))
						{
							SetState(States.drop, ref state, c);
						}
						else if(string.Equals(word, "create", StringComparison.InvariantCultureIgnoreCase))
						{
							SetState(States.create, ref state, c);
						}
						else
						{
							throw new Exception($"Parse error: unknown word: position {i} char '{c}' word '{word}' state {state}");
						}

						var tmp = buffer.ToString().TrimStart();
						buffer.Clear();
						buffer.Append(tmp);
					}
				}
				break;
			case States.commentStart:
				if (c == '-')
				{
					SetState(States.comment, ref state, c);
				}
				else
				{
					throw new Exception($"Parse error: position {i} char '{c}' word '{wordBuilder?.ToString()}' state {state}");
				}
				break;
			case States.comment:
				if (c == '\n')
				{
					wordBuilder.Clear();
					SetState(States.fileStart, ref state, c);
				}
				break;
			case States.drop:
				if (char.IsWhiteSpace(c))
				{
					// check if it's a recognized word
					word = wordBuilder.ToString().Trim();
					wordBuilder.Clear();
					if (string.Equals(word, "trigger", StringComparison.InvariantCultureIgnoreCase))
					{
						SetState(States.dropTrigger, ref state, c);
					}
					else if (string.Equals(word, "procedure", StringComparison.InvariantCultureIgnoreCase))
					{
						SetState(States.dropProcedure, ref state, c);
					}					
				}
				break;
			case States.dropTrigger:
				if (char.IsWhiteSpace(c))
				{
					// check if it's a recognized word
					word = wordBuilder.ToString();
					wordBuilder.Clear();
					name = word;
					SetState(States.dropTriggerContent, ref state, c);
				}
				break;
			case States.dropTriggerContent:
				// next word should be trigger name
				if (char.IsWhiteSpace(c))
				{
					word = wordBuilder.ToString();
					wordBuilder.Clear();
					name = word;
				}
				// after trigger name, read all text until semicolon
				else if (c == ';')
				{
					wordBuilder.Remove(wordBuilder.Length - 1, 1);
					name = wordBuilder.ToString();
					//WriteFile(buffer.ToString(), $"{name}.drop-trigger.sql");
					RegisterSqlObject(name, SqlObjectType.trigger, dropContent: buffer.ToString());
					SetState(States.dropTriggerEndOf, ref state, c);
				}
				break;
			// do you really need the dropTriggerEndOf state?
			case States.dropTriggerEndOf:
					buffer.Clear();
					wordBuilder.Clear();
					SetState(States.fileStart, ref state, c);
				break;
			case States.create:
				if (char.IsWhiteSpace(c))
				{
					// check if it's a recognized word
					word = wordBuilder.ToString().Trim();
					wordBuilder.Clear();
					if (string.Equals(word, "trigger", StringComparison.InvariantCultureIgnoreCase))
					{
						SetState(States.createTrigger, ref state, c);
					}
					else if (string.Equals(word, "procedure", StringComparison.InvariantCultureIgnoreCase))
					{
						SetState(States.createProcedure, ref state, c);
					}
				}
				break;
			case States.createTrigger:
				// next word should be trigger name
				if (char.IsWhiteSpace(c))
				{
					// check if it's a recognized word
					word = wordBuilder.ToString().Trim();
					wordBuilder.Clear();
					name = word;
					SetState(States.createTriggerContent, ref state, c);
				}
				break;
			case States.createTriggerContent:
				// Track the state within string and comment blocks
				inString = false;
				inSingleLineComment = false;
				inMultiLineComment = false;
				wordBuilder.Clear();
				
				do
				{					
					if (c == '\'' && !inSingleLineComment && !inMultiLineComment)
					{
						inString = !inString; // Toggle the string state when encountering a single quote
					}
					else if (c == '-' && !inString && !inMultiLineComment)
					{
						int nextChar = sr.Peek();
						if (nextChar == '-')
						{
							inSingleLineComment = true; // Enter single-line comment
						}
					}
					else if (c == '/' && !inString && !inSingleLineComment)
					{
						int nextChar = sr.Peek();
						if (nextChar == '*')
						{
							inMultiLineComment = true; // Enter multi-line comment
						}
					}
					else if(c == '\n' && inSingleLineComment)
					{
						inSingleLineComment = false;
					}
					else if (c == ';' && !inString && !inSingleLineComment && !inMultiLineComment)
					{
						// End of the trigger statement						
						if(string.Equals("END", wordBuilder.ToString(), StringComparison.OrdinalIgnoreCase))
						{
							//WriteFile(buffer.ToString(), $"{name}.create-trigger.sql");
							RegisterSqlObject(name, SqlObjectType.trigger, createContent: buffer.ToString());
							buffer.Clear();
							wordBuilder.Clear();
							// skip createTriggerEndOf
							SetState(States.fileStart, ref state, c);
							break; // Exit the loop when not inside string or comments
						}
					}
					else if (!inString && !inSingleLineComment && !inMultiLineComment)
					{
						if(char.IsWhiteSpace(c))
						{
							wordBuilder.Clear();
						}
						else
						{
							wordBuilder.Append(c);
						}
					}
					
					ci = sr.Read();
					c = (char)ci;
					buffer.Append(c);
					i++;
				} while (ci != -1);
				break;
			case States.createTriggerEndOf:
				buffer.Clear();
				wordBuilder.Clear();
				SetState(States.fileStart, ref state, c);
				break;
			case States.createProcedure:
				// next word should be Procedure name
				if (char.IsWhiteSpace(c))
				{
					// check if it's a recognized word
					word = wordBuilder.ToString().Trim();
					wordBuilder.Clear();
					name = word;
					SetState(States.createProcedureContent, ref state, c);
				}
				break;
			case States.createProcedureContent:
				// Track the state within string and comment blocks
				inString = false;
				inSingleLineComment = false;
				inMultiLineComment = false;
				wordBuilder.Clear();

				do
				{
					if (c == '\'' && !inSingleLineComment && !inMultiLineComment)
					{
						inString = !inString; // Toggle the string state when encountering a single quote
					}
					else if (c == '-' && !inString && !inMultiLineComment)
					{
						int nextChar = sr.Peek();
						if (nextChar == '-')
						{
							inSingleLineComment = true; // Enter single-line comment
						}
					}
					else if (c == '/' && !inString && !inSingleLineComment)
					{
						int nextChar = sr.Peek();
						if (nextChar == '*')
						{
							inMultiLineComment = true; // Enter multi-line comment
						}
					}
					else if (c == '\n' && inSingleLineComment)
					{
						inSingleLineComment = false;
					}
					else if (c == ';' && !inString && !inSingleLineComment && !inMultiLineComment)
					{
						// End of the Procedure statement
						// todo: parse out file name as the name of the Procedure

						if (string.Equals("END", wordBuilder.ToString(), StringComparison.OrdinalIgnoreCase))
						{
							//WriteFile(buffer.ToString(), $"{name}.create-procedure.sql");
							RegisterSqlObject(name, SqlObjectType.sproc, createContent: buffer.ToString());
							buffer.Clear();
							wordBuilder.Clear();
							// skip createProcedureEndOf
							SetState(States.fileStart, ref state, c);
							break; // Exit the loop when not inside string or comments
						}
					}
					else if (!inString && !inSingleLineComment && !inMultiLineComment)
					{
						if (char.IsWhiteSpace(c))
						{
							wordBuilder.Clear();
						}
						else
						{
							wordBuilder.Append(c);
						}
					}

					ci = sr.Read();
					c = (char)ci;
					buffer.Append(c);
					i++;
				} while (ci != -1);
				break;
			case States.createProcedureEndOf:
				buffer.Clear();
				wordBuilder.Clear();
				SetState(States.fileStart, ref state, c);
				break;
			case States.dropProcedure:
				if (char.IsWhiteSpace(c))
				{
					// check if it's a recognized word
					word = wordBuilder.ToString();
					wordBuilder.Clear();
					name = word;
					SetState(States.dropProcedureContent, ref state, c);
				}
				break;
			case States.dropProcedureContent:
				// next word should be Procedure name
				if (char.IsWhiteSpace(c))
				{
					word = wordBuilder.ToString();
					wordBuilder.Clear();
					name = word;
				}
				// after Procedure name, read all text until semicolon
				else if (c == ';')
				{
					wordBuilder.Remove(wordBuilder.Length - 1, 1);
					name = wordBuilder.ToString();
					//WriteFile(buffer.ToString(), $"{name}.drop-procedure.sql");
					RegisterSqlObject(name, SqlObjectType.sproc, dropContent: buffer.ToString());
					SetState(States.dropProcedureEndOf, ref state, c);
				}
				break;
			// do you really need the dropProcedureEndOf state?
			case States.dropProcedureEndOf:
				buffer.Clear();
				wordBuilder.Clear();
				SetState(States.fileStart, ref state, c);
				break;
			default:
				throw new Exception($"Parse error: unhandled state: position {i} char '{c}' word '{wordBuilder?.ToString()}' state {state}");
		}

		WriteSqlObjects();
		
		ci = sr.Read();
		i++;
	}

	WriteSqlObjects(true);
}

enum States
{
	fileStart,
	fileEndOf,
	drop,
	create,
	comment,
	commentStart,
	commentContent,
	commentEndOf,
	commentMultiLine,
	commentMultiLineStart,
	commentMultiLineContent,
	commentMultilineEndOf,
	dropTrigger,
	dropTriggerContent,
	dropTriggerEndOf,
	createTrigger,
	createTriggerContent,
	createTriggerEndOf,
	dropProcedure,
	dropProcedureContent,
	dropProcedureEndOf,
	createProcedure,
	createProcedureContent,
	createProcedureEndOf,
};

public enum SqlObjectType
{
	trigger,
	sproc
}

public class SqlObject
{
	public string DropSql { get; set; }
	public string CreateSql { get; set; }
	public SqlObjectType SqlObjectType { get; set; }
	
	public bool IsComplete
	{
		get
		{
			return !string.IsNullOrWhiteSpace(this.DropSql) && !string.IsNullOrWhiteSpace(this.CreateSql);
		}
	}
		
	public override string ToString()
	{
		return string.Concat(this.DropSql, Environment.NewLine, Environment.NewLine, this.CreateSql, Environment.NewLine);
	}
}

Dictionary<string, SqlObject> sqlObjectDict = new();

void WriteSqlObjects(bool all=false)
{
	foreach(var kv in this.sqlObjectDict)
	{
		var kvv = kv.Value;
		if(kvv.IsComplete || all)
		{
			Console.WriteLine($"* {kv.Key}{(kvv.IsComplete ? "":" !")}");
			WriteFile(kvv.ToString(), $"{kv.Key}.{kvv.SqlObjectType}.sql");
			this.sqlObjectDict.Remove(kv.Key);
		}
	}
}

void SetState(States newState, ref States oldState, char c)
{
	// Console.WriteLine($"[{oldState}] -> [{newState}] : '{c}'");
	oldState = newState;
}

void RegisterSqlObject(string name, SqlObjectType sot, string dropContent = null, string createContent = null)
{
	SqlObject so = null;
	if(this.sqlObjectDict.ContainsKey(name))
	{
		so = this.sqlObjectDict[name];
	}
	else
	{
		so = new();
		so.SqlObjectType = sot;
		this.sqlObjectDict.Add(name, so);
	}
	
	if(dropContent != null)
	{
		so.DropSql = dropContent;
	}
	
	if(createContent != null)
	{
		so.CreateSql = createContent;
	}
}

void WriteFile(string stringBuffer, string fileName)
{
	if(Directory.Exists(this.outputFolder) == false)
	{
		Directory.CreateDirectory(this.outputFolder);
	}
	
	var fullFilePath = Path.Combine(this.outputFolder, fileName);
	if(File.Exists(fullFilePath))
	{
		throw new Exception($"File already exists: '{fileName}'");
	}
	
	File.WriteAllText(fullFilePath, stringBuffer);
}
