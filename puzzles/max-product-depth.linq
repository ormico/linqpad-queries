<Query Kind="Statements" />

/*
ProductA
ProductB
ProductC
ProductD
ProductE

ProductA
- ProductB

ProductB
- ProductC
- ProductD

ProductC

ProductD
- ProductC
- ProductE

class Product
{
string Id { get;}
List<Product> Children { get; }
}

int MaxProductDepth(Product p, List<Product> products)
*/
class Product
{
	public string Id { get; }
	public List<Product> Children { get; }
}

Dictionary<string, int> depthCount = new Dictionary<string, int>();

/// <summary>Assuming no cylces</summary>
int MaxProductDepth(Product p, List<Product> products)
{
	foreach(var i in products)
	{
		dfs(i);
	}
	int rc = -1;
	
	if(depthCount.ContainsKey(p.Id))
	{
		rc = depthCount[p.Id];
	}
	return rc;
}

void dfs(Product p, int depth = 1)
{
	if(p == null)
		return;	
	
	if (depthCount.ContainsKey(p.Id))
	{
		int cnt = depthCount[p.Id];
		if(cnt < depth)
		{
			depthCount[p.Id] = depth;
		}
	}
	else
	{
		depthCount.Add(p.Id, depth);
	}

	if (p.Children == null)
		return;

	for (int i = 0; i < p.Children.Count; i++)
	{
		dfs(p.Children[i], depth + 1);
	}
}

