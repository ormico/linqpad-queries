<Query Kind="Program">
  <NuGetReference>Dapper</NuGetReference>
  <Namespace>Dapper</Namespace>
  <Namespace>System.Dynamic</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	Task.Run(async () => {
		try
		{			
			using (IDbConnection db = new SqlConnection("Server=.;Database=liverpoolProd-ui-20190930;Trusted_Connection=true"))
			{
				db.Open();
				var scalar = db.Query<string>("SELECT GETDATE()").SingleOrDefault();
				scalar.Dump("This is a string scalar result:");
		
				Console.WriteLine("results1");
				var results1 = (IEnumerable<IDictionary<string, object>>)db.Query(@"[dbo].[UserSelectAll]", commandType: CommandType.StoredProcedure);
				results1.Dump();
				/* */
				Console.WriteLine("results2");
				var results2 = await db.QueryAsync(@"[dbo].[UserSelectAll]", commandType: CommandType.StoredProcedure);
				var results2x = (IEnumerable<IDictionary<string, object>>)results2;
				results2x.Dump();
				
				Console.WriteLine("results3");
				var results3 = db.Query<dynamic>(@"[dbo].[UserSelectAll]", commandType: CommandType.StoredProcedure);
				results3.Dump();
		
				Console.WriteLine("results4");
				var results4 = await db.QueryAsync<dynamic>(@"[dbo].[UserSelectAll]", commandType: CommandType.StoredProcedure);
				results4.Dump();
	
				Console.WriteLine("results5");
				var results5 = await db.QueryAsync(@"[dbo].[UserSelectAll]", commandType: CommandType.StoredProcedure);
				results5.Dump();
			}
		}
		catch(Exception e)
		{
			Console.WriteLine("Error: {0}", e);
		}
	});
}
