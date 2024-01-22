<Query Kind="Program">
  <NuGetReference>Microsoft.Data.SqlClient</NuGetReference>
</Query>

/*
todo: add a poly example
*/
void Main()
{
	SqlConnection con = null;
	
	for(int retry = 0; retry < 4; retry++)
	{
		Console.WriteLine("try {0}", retry+1);
		try
		{
			string connectionStr = Util.GetPassword("SQL Retry - Connection String", false);
			con = new SqlConnection(connectionStr + ";Connect Timeout=120;");
			Console.WriteLine(con.ConnectionTimeout);
			con.Open();
			break;
		}
		catch(Exception e)
		{
			con = null;
			Console.WriteLine(e.GetType().ToString());
			Console.WriteLine(e.Message);
			System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.1));
		}
	}

	if (con != null)
	{
		var cmd = con.CreateCommand();
		cmd.CommandText = "select getdate();";
		Console.WriteLine(cmd.ExecuteScalar());
	}
	else
	{
		Console.WriteLine("could not connect");	
	}
}