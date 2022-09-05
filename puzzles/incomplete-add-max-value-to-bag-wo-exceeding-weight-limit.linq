<Query Kind="Program" />

/*
Given two integer arrays val[0..n-1] and wt[0..n-1] which represent values and weights associated 
with n items respectively. 

Also given an integer W which represents bag capacity, find out the 
maximum value subset of val[] such that sum of the weights of this subset is smaller than or equal to W. 
You cannot break an item, either pick the complete item or don’t pick it

Example

int wgt[] = new int[]{​​12, 2, 1, 1, 4}​​;
int val[] = new int[]{​​4, 2, 1, 2, 10}​​;

int W = 15;
*/
void Main()
{
	int[] wgt = new int[] {​​12, 2, 1, 1, 4 }​​;
	int[] val = new int[] {​​4, 2, 1, 2, 10 }​​;
	int W = 15;
	
	var answer = Solve(val, wgt, bag);
}

class Bag
{
	List<int> Vals;
	int Weight;
}

int[] Solve(int[] values, int[] weights, int bagCapacity)
{
	List<int> rc = new List<int>();	
	List<Bag> bags = new List<UserQuery.Bag>();
	
	for (int v = 0; v < values.Length; v++)
	{
		
	}	
	
	return rc.ToArray();
}

