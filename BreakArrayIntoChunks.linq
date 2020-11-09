<Query Kind="Statements" />

List<string> list = new List<string>()
{
	"alpha",
	"beta",
	"gamma",
	"delta",
	"zeta"
};

int chunkSize = 2;
int listSize = list.Count();
for (int n = 0; n < listSize; n += chunkSize)
{
    int curChunkSize = Math.Min(listSize - n, chunkSize);
	//Console.WriteLine("{0} {1} {2}", n, curChunkSize, list.Count());
    var chunk = list.GetRange(n, curChunkSize);
    // do something with chunk
	Console.WriteLine(string.Join(",", chunk));	
}

Console.WriteLine("whole list = {0} ", string.Join(",", list));