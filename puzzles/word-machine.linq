<Query Kind="Program" />

/*
create a 'word machine' that interprets a string as a sequence of commands seperated by a single space.
return:
if there is an error, return error code -1
otherwise, pop the integer off the top of the stack and return it
if there are no integers on the stack return error code -1
assumptions:
input is a valid sequence of word machine operations
commands:
POP: pop the last command off the stack
DUP: push onto the stack a duplicate of the last item pushed onto the stack
	if the stack is empty, return error code -1
+: pop the last 2 ints off the stack, add them together, and push the result onto the stack.
	if the stack does not contain at least two integers, return error code -1
-: pop the last 2 ints off the stack where the first is n1 and the second is n2 and subtrack 
	n2 from n1. push the result onto the stack.
	if the stack does not contain at least two integers, return error code -1
<integer>:push the integer onto the stack. the integer must be between 0 and (2^20 - 1). if 
	the integer fails to parse or is outside of the accepted range, return error code -1
*/
void Main()
{
	var sol = new Solution();
	string ex1 = "4 5 6 - 7 +";
	Console.WriteLine(sol.solution(ex1));

	string ex2 = "13 DUP 4 POP 5 DUP + DUP + -";
	Console.WriteLine(sol.solution(ex2));

	Console.WriteLine(sol.solution("5 6 + -"));
	Console.WriteLine(sol.solution("3 DUP 5 - -"));
}

/// <summary>
/// Break input into array of words.
/// Process each word as a command and return the final value.
/// </summary>
class Solution
{
	const int errorCode = -1;
	const char wordSeperator = ' ';
	
	// compute word machine integer min and max values
	readonly int intMin = 0;
	readonly int intMax = (int)Math.Pow(2, 20) - 1;

	class WordCommands
	{
		public const string Pop = "POP";
		public const string Dup = "DUP";
		public const string Add = "+";
		public const string Subtract = "-";
	}

	public int solution(string S)
	{
		// default to error code
		int rc = errorCode;
		Stack<int> stack = new Stack<int>();
		
		try
		{
			// exercise says we can assume S is a valid sequence of word machine operations
			// so we don't need to check for validity such as invalid words or
			// too many seperator characters
			var words = S.Split(wordSeperator);
			bool error = false;
			
			foreach(var w in words)
			{
				if(w.Equals(WordCommands.Pop))
				{
					stack.Pop();
				}
				else if(w.Equals(WordCommands.Dup))
				{
					stack.Push(stack.Peek());
				}
				else if(w.Equals(WordCommands.Add))
				{
					int n1 = stack.Pop(), n2 = stack.Pop();
					stack.Push(n1 + n2);
				}
				else if (w.Equals(WordCommands.Subtract))
				{
					int n1 = stack.Pop(), n2 = stack.Pop();
					stack.Push(n1 - n2);
				}
				else // must be integer
				{
					int n = int.Parse(w);
					// validate the integer is within the given range
					if(n < intMin || n > intMax)
					{
						error = true;
						break;
					}
					
					stack.Push(n);
				}
			}
			
			// if an error is detected, the loop will break early
			// and we should return the error code
			if(error)
			{
				rc = errorCode;
			}
			else
			{
				rc = stack.Pop();
			}
		}
		catch(Exception e)
		{
			// if any error occurs such as failure to parse int
			// or stack overflow, then return error code
			rc = errorCode;
		}

		return rc;
	}
}