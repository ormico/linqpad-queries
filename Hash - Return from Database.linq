<Query Kind="Program">
  <NuGetReference>Dapper.Testable</NuGetReference>
  <Namespace>Dapper</Namespace>
</Query>

void Main()
{
	using(var con = new SqlConnection("Server=.;Database=Test;Trusted_Connection=True;"))
	{
		string sql = 
@"SELECT
	name
	,HashBytes('SHA', name) [HASH]
FROM Names";
	
		List<Alpha> list = con.Query<Alpha>(sql).ToList();
		list.Dump();
	}

	byte[] b1 = new byte[] { 1, 2, 3, 4, 0, 99 };
	byte[] b2 = new byte[] { 1, 2, 3, 4, 0, 99 };
	byte[] b3 = new byte[] { 1, 2, 3, 4, 0, 100 };
	byte[] b4 = new byte[] { 1, 2, 3, 4, 0, 99, 100 };
	
	Binary bb1 = b1;
	Binary bb2 = b2;
	Binary bb3 = b3;
	Binary bb4 = b4;
	(bb1 == bb2).Dump();
	(bb1 == bb3).Dump();
	(bb1 == bb4).Dump();

	((Binary)b1 == (Binary)b2).Dump();
}

class Alpha
{
	public string name {get; set;}
	//public string HASH {get; set;}
	public byte[] HASH {get; set;}
}