<Query Kind="Program" />

/*
Also given an integer W which represents bag capacity, find out the
maximum value subset of val[] such that sum of the weights of this subset is smaller than or equal to W.
You cannot break an item, either pick the complete item or don’t pick it

Example:
int wgt[] = new int[] {​​12, 2, 1, 1, 4 }​​;
int val[] = new int[] {​​4, 2, 1, 2, 10 }​​;

int W = 15;
*/
void Main()
{
	int[] wgt = { 12, 2, 1, 1, 4 };
	// TEST: int[] wgt = { 12, 9, 1, 1, 4 };
	int[] val = { 4, 2, 1, 2, 10 };
	int W = 15;
	
	var answer = Solve(val, wgt, W);
	for(int i = 0; i < answer.Length; i++)
	{
		Console.WriteLine(answer[i]);
	}	
}

class Bag
{
	public HashSet<int> Used;
	public List<int> Vals;
	public int TotalWeight;
	public int TotalValue;
}

int[] Solve(int[] values, int[] weights, int bagCapacity)
{
	Bag bag = new Bag { Vals = new List<int>(), TotalWeight = 0, TotalValue = 0, Used = new HashSet<int>() };
	Solve(values, weights, bagCapacity, bag, 0);	
	return bag?.Vals?.ToArray();
}

void Solve(int[] values, int[] weights, int bagCapacity, Bag bag, int start)
{		
	for (int i = start; i < values.Length; i++)
	{
		int cv = values[i];
		int cw = weights[i];
		
		if(!bag.Used.Contains(i) && bag.TotalValue + cv > bag.TotalValue && bag.TotalWeight + cw <= bagCapacity)
		{
			bag.Vals.Add(cv);
			bag.TotalValue += cv;
			bag.TotalWeight += cw;
			bag.Used.Add(i);
		}
		Solve(values, weights, bagCapacity, bag, i + 1);
	}
}
