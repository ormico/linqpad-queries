<Query Kind="Program" />

void Main()
{
	var sol = new Solution();
	char[][] board = {
		new char[] {'A','B','C','E'},
		new char[] {'S','F','C','S'},
		new char[] {'A','D','E','E'}
		};
	string word = "ABCCED";
	Console.WriteLine(sol.Exist(board, word));
}

public class Solution
{
	public bool Exist(char[][] board, string word)
	{
		// {"x,y"},{"0,0"}, ...
		HashSet<string> used = new HashSet<string>();
		bool rc = false;
		for (int y = 0; y < board.Length; y++)
		{
			for (int x = 0; x < board[y].Length; x++)
			{
				rc = Search(x, y, board, word, used);
				if (rc)
				{
					break;
				}
			}
			
			if (rc)
			{
				break;
			}
		}

		return rc;
	}

	bool Search(int x, int y, char[][] board, string word, HashSet<string> used)
	{
		Console.WriteLine($"searching {x},{y}");
		bool rc = false;

		if (word == null || word.Length == 0)
		{
			Console.WriteLine("end of word");
			return true;
		}

		if (board[y][x] != word[0])
		{
			Console.WriteLine($"char doesn't match: {board[y][x]} != {word[0]}");
			return false;
		}

		string coord = $"{x},{y}";
		if (used.Contains(coord))
		{
			Console.WriteLine("coord already used");
			return false;
		}

		used.Add(coord);

		// loop up
		if (y - 1 >= 0)
		{
			Console.WriteLine("searching up");
			rc = Search(x, y - 1, board, word.Substring(1, word.Length - 1), used);
		}

		// look down
		if (!rc && y + 1 < board.Length)
		{
			Console.WriteLine("searching down");
			rc = Search(x, y + 1, board, word.Substring(1, word.Length - 1), used);
		}

		// look right
		if (!rc && x + 1 < board[y].Length)
		{
			Console.WriteLine("searching right");
			rc = Search(x + 1, y, board, word.Substring(1, word.Length - 1), used);
		}

		// look left
		if (!rc && x - 1 >= 0)
		{
			Console.WriteLine("searching left");
			rc = Search(x - 1, y, board, word.Substring(1, word.Length - 1), used);
		}

		if (rc == false)
		{
			used.Remove(coord);
		}

		return rc;
	}
}