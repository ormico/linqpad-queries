<Query Kind="Program" />

/*
Is tree T2 a subset of tree T1
T1
           1
      2        3
  4      5         6

T2
    2
  4

do a dfs() to find the matching first node, then call IsSubset() to check if 
subset is at current node, otherwise continue searching.
*/
void Main()
{
	TreeNode t1 = new TreeNode
	{
		left = new TreeNode
		{
			left = new TreeNode
			{
				value = 4
			},
			right = new TreeNode
			{
				value = 5
			},
			value = 2
		},
		right = new TreeNode
		{
			right = new TreeNode
			{
				value = 6	
			},
			value = 3	
		},
		value = 1
	};
	TreeNode t2 = new TreeNode
	{
		left = new TreeNode
		{
			value = 4
		},
		value = 2
	};
	Console.WriteLine(ContainsSubSet(t1, t2));
}

class TreeNode
{
	public TreeNode left;
	public TreeNode right;
	public int value;
}

bool ContainsSubSet(TreeNode t1, TreeNode t2)
{
	bool rc = false;
	
	dfs(t1, n =>
	{
		// Func: return true to continue or false to stop
		if(n.value == t2.value)
		{
			if(IsSubset(n, t2))
			{
				rc = true;
				return false;
			}
		}
		return true;
	});	

	return rc;
}

bool IsSubset(TreeNode subject, TreeNode test)
{
	bool rc = false;

	if ((subject == null) && (test == null))
	{
		rc = true;
	}
	else if ((subject != null) && (test == null))
	{
		rc = true;
	}
	else
	{
		rc = subject.value == test.value
			&& IsSubset(subject.left, test.left)
			&& IsSubset(subject.right, test.right);
	}

	return rc;
}

void inorder(TreeNode n, Action<TreeNode> f)
{
	if (n == null)
		return;
	inorder(n.left, f);
	f(n);
	inorder(n.right, f);
}

// Func: return true to continue or false to stop
void dfs(TreeNode n, Func<TreeNode, bool> f)
{
	if(n == null)
		return;
		
	if(f(n))
	{
		dfs(n.left, f);
		dfs(n.right, f);
	}
}

/*

bool IsSubSetWrong(TreeNode t1, TreeNode t2)
{
	bool rc = false;
	
	StringBuilder sb1 = new StringBuilder();
	inorder(t1, n =>
	{
		//Console.WriteLine($"{n.value}");
		sb1.AppendFormat("{0},", n.value);
	});
	StringBuilder sb2 = new StringBuilder();
	inorder(t2, n =>
	{
		//Console.WriteLine($"{n.value}");
		sb2.AppendFormat("{0},", n.value);
	});
	
	string s1 = sb1.ToString();
	string s2 = sb2.ToString();
	
	if(s1.Contains(s2))
		rc = true;
		
	return rc;
}

*/