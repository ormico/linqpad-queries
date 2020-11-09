<Query Kind="Program" />

void Main()
{
	// based on linqpad example "Extra - Dynamic Ordering - How it Works"
	// todo: rewrite and remove reference to original example
	// todo: it would be useful to compare this method to doing it through reflection
	// -Zack
	
	// Thanks to Matt Warren, of the Microsoft LINQ to SQL team, for illustrating how this is done.
	//
	// Suppose you want order a query based on string that you receive at runtime. The string
	// indicates a property or field name, such as "Price" or "Description" or "Date".

	// For this, you need to dynamically contruct an "OrderBy" MethodCallExpression. This, in turn,
	// requires a dynamically constructed LambdaExpression that references the property or field
	// upon which to sort. Here's the complete solution:

	List<MyClass> data = new List<MyClass>()
	{
		new MyClass() { ID = 1, Name = "Sue", CreatedDt = new DateTime(2016, 1, 1), Alpha = "blah", Beta = null, Gamma = 100.01M, Zeta = 2.123456 },
		new MyClass() { ID = 2, Name = "Adam", CreatedDt = new DateTime(2016, 2, 1), Alpha = "blah", Beta = null, Gamma = 100.01M, Zeta = 2.123456 },
		new MyClass() { ID = 3, Name = "Fred", CreatedDt = new DateTime(2016, 3, 1), Alpha = "blah", Beta = new DateTime(2015, 7, 9), Gamma = 200.01M, Zeta = 2.123456 },
		new MyClass() { ID = 4, Name = "Anne", CreatedDt = new DateTime(2016, 4, 1), Alpha = "blah", Beta = null, Gamma = 100.01M, Zeta = 11.1 },
		new MyClass() { ID = 5, Name = "Jen", CreatedDt = new DateTime(2016, 5, 1), Alpha = "blah", Beta = new DateTime(2015, 5, 3), Gamma = 101.01M, Zeta = 2.123456 },
		new MyClass() { ID = 6, Name = "Angel", CreatedDt = new DateTime(2016, 6, 1), Alpha = "blah", Beta = null, Gamma = 100.01M, Zeta = 7.123456 },
		new MyClass() { ID = 7, Name = "Gary", CreatedDt = new DateTime(2016, 7, 1), Alpha = "blah", Beta = null, Gamma = 100.01M, Zeta = 9.123456 }
	};
	
	// The original unordered query
	var query = from d in data
				where d.Gamma > 100M
				select d;
	var queryQ = query.AsQueryable();

	// string of property name to perform sort on
	// doesn't seem to matter if we get the case correct on property name
	string propToOrderBy = "zeta"; //"createddt"; //"Zeta";
	
	ParameterExpression purchaseParam = Expression.Parameter(typeof(MyClass), "d");
	MemberExpression member = Expression.PropertyOrField(purchaseParam, propToOrderBy);
	LambdaExpression lambda = Expression.Lambda(member, purchaseParam);
	Type[] exprArgTypes = { queryQ.ElementType, lambda.Body.Type };
	
	MethodCallExpression methodCall =
		Expression.Call(typeof(Queryable), "OrderBy", exprArgTypes, queryQ.Expression, lambda);
	
	IQueryable orderedQuery = queryQ.Provider.CreateQuery(methodCall);
	orderedQuery.Dump();
}

class MyClass
{
	public int ID { get; set; }
	public string Name { get; set; }
	public DateTime CreatedDt { get; set; }
	public string Alpha { get; set; }
	public DateTime? Beta { get; set; }
	public decimal Gamma { get; set; }
	public double Zeta { get; set; }
}
