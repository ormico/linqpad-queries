<Query Kind="Program" />

/* are trees equal

*/
void Main()
{
	var A = CreateTestTrees1();
	var B = CreateTestTrees1();

	Console.WriteLine(AreEqual(A.Item1, B.Item1));
	Console.WriteLine(AreEqual(A.Item1, A.Item1));
	Console.WriteLine(AreEqual(A.Item1, A.Item2));

	var C = CreateTestTrees2();
	Console.WriteLine(IsSubset(C.Item1, C.Item1));
	Console.WriteLine(IsSubset(C.Item1, C.Item2));
}

class TreeNode
{
	public TreeNode left;
	public TreeNode right;
	public int value;
}


bool AreEqual(TreeNode a, TreeNode b)
{
	bool rc = false;

	if ((a == null) && (b == null))
	{
		rc = true;
	}
	else if ((a == null) || (b == null))
	{
		rc = false;
	}
	else
	{
		rc = a.value == b.value && AreEqual(a.left, b.left) && AreEqual(a.right, b.right);
	}

	return rc;
}

bool IsSubset(TreeNode subject, TreeNode test)
{
	bool rc = false;
	
	if((subject == null) && (test == null))
	{
		rc = true;
	}
	else if((subject != null) && (test == null))
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

/*
T1
		   1

	  2        3
  4      5         6
-----------------------------
T2
	2
  4
*/
(TreeNode, TreeNode) CreateTestTrees1()
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
	
	return (t1,t2);
}

/*
T1
	 2
  4     5
1  3   7 9
-----------------------------
T2
	2
  4
*/
(TreeNode, TreeNode) CreateTestTrees2()
{
	TreeNode t1 = new TreeNode
	{
		left = new TreeNode
		{
			left = new TreeNode
			{
				value = 1
			},
			right = new TreeNode
			{
				value = 3
			},
			value = 4
		},
		right = new TreeNode
		{
			left = new TreeNode
			{
				value = 7
			},
			right = new TreeNode
			{
				value = 9
			},
			value = 5
		},
		value = 2
	};
	TreeNode t2 = new TreeNode
	{
		left = new TreeNode
		{
			value = 4
		},
		value = 2
	};

	return (t1, t2);
}


