<Query Kind="Statements" />

/*
22 Mar 2021 - Amazon Assessment

Robot simulator
simulate a robot. make sure it stays in a circle
G go forward
L turn left and go 1
R turn right and go 1
--

ran out of time b/c I wasted too much time on previous problem
but attempted to run commands and make sure it landed back on 0,0


*/
var answer = doesCircleExist(new List<string>() { "R", "G" });
foreach(var i in answer)
{
	Console.WriteLine(i);
}

public static List<string> doesCircleExist(List<string> commands)
{
	List<string> rc = new List<string>();
	foreach(var i in commands)
	{
		rc.Add(doesCircleExist(i));
	}	
	return rc;
}

public static string doesCircleExist(string c)
{
	int x=0,y=0;
	char d = 'n';
	
	foreach(var i in c)
	{
		if(i == 'G')
		{
			if(d == 'n') y++;
			else if(d == 's') y--;
			else if(d == 'e') x++;
			else if(d == 'w') x--;
		}
		else if(c == 'R')
	}
	
	if(x == 0 && y == 0)
		return "YES";
	else
		return "NO";
}

