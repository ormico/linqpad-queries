<Query Kind="Statements">
  <Namespace>System.Text.Json</Namespace>
</Query>

/* Detect cycle (loop) in a directed graph
see for another description https://www.geeksforgeeks.org/detect-cycle-in-a-graph/
*/
var dir = Path.GetDirectoryName(Util.CurrentQueryPath);

var x = Vertex.LoadGraph(Path.Combine(dir, "example-graph.json"));
var hasCycle = Vertex.DetectCycle(x.Item1);
Console.WriteLine($"hasCycle: {hasCycle}");

x = Vertex.LoadGraph(Path.Combine(dir, "example-graph-w-cycle.json"));
hasCycle = Vertex.DetectCycle(x.Item1);
Console.WriteLine($"hasCycle: {hasCycle}");

// -- Declarations -----------------------------------------

class Vertex
{
	public string Id { get; set; }
	public HashSet<Vertex> AdjacentIds { get; } = new HashSet<Vertex>();

	public Vertex() { }
	
	public Vertex(string id)
	{
		this.Id = id;
		this.AdjacentIds = new HashSet<Vertex>();
	}

	public Vertex(string id, IEnumerable<Vertex> dependsOn)
	{
		this.Id = id;
		this.AdjacentIds = new HashSet<Vertex>(dependsOn);
	}

	public override string ToString()
	{
		 return Id;
	}

	public static string DetectCycle(IEnumerable<Vertex> verts)
	{
		string rc = null;
		// visited is the collection of all Vertices checked so far
		HashSet<Vertex> visited = new HashSet<Vertex>();
		// list of Vertices traversed to get to whatever is the current node
		// this changes as the search progresses and nodes are removed
		// as the search moves to different branches
		HashSet<Vertex> nAncestors = new HashSet<Vertex>();
		foreach(var v in verts)
		{
			if(DetectCycle(v, visited, nAncestors, ref rc))
				break;
		}
		return rc;
	}

	/// <summary>
	/// return true if there is a cycle.
	/// n: Current Vertext to examine
	/// visited: collection of Vertices visited so far during entire cycle detection process
	/// nAncestors: collection of Vertices visited on the current dfs search starting at a particular Vertices
	/// id: ref param used to output the id of the Vertex causing the cycle
	/// </summary>
	static bool DetectCycle(Vertex n, HashSet<Vertex> visited, HashSet<Vertex> nAncestors, ref string id)
	{
		// if n is null there is no cycle
		// if ancestors of current recursive dfs search contains current Vertex then there is a cycle
		// b/c it means the current dfs has already visited the current Vertex
		// if the current Vertex was already searched as part a different dfs search and no cycle was 
		// discovered, then there is no need to search it again
		// if we have not detected a cycle and we have not already searched the current Vertex then
		// add the current Vertex to the nAncestors and visited collection and recursivly search the 
		// Adjacent Vertices.
		// After searching the Adjacent Vertices, remove vertext from nAncestors but not from visited.
		// We remove from nAncestors because the recursion for this branch is complete and we are moving
		// to another branch, but we do not remove from visited b/c we want to keep this node marked
		// as already searched.
		bool rc = false;
		
		if(n == null)
		{
			rc = false;
		}
		else if(nAncestors.Contains(n))
		{
			id = n.Id;
			rc = true;
		}
		else if(visited.Contains(n))
		{
			rc = false;
		}
		else
		{
			nAncestors.Add(n);
			visited.Add(n);
			foreach(var i in n.AdjacentIds)
			{
				if(DetectCycle(i, visited, nAncestors, ref id))
				{
					rc = true;
					break;
				}
			}
			nAncestors.Remove(n);
		}

		return rc;
	}
	
	public static (IEnumerable<Vertex>, IDictionary<string, Vertex>) LoadGraph(string filePath)
	{
		if(File.Exists(filePath) == false)
		{
			throw new FileNotFoundException(filePath);	
		}
		
		var jsonText = File.ReadAllText(filePath);
		var jdoc = JsonDocument.Parse(jsonText);
		var graphDoc = jdoc.RootElement;
		var vertices = new List<Vertex>();
		var index = new Dictionary<string, Vertex>();

		foreach (var p in graphDoc.EnumerateArray())
		{
			var id = p.GetProperty("id").GetString();
			Vertex Vertex;

			if (!index.ContainsKey(id))
			{
				Vertex = new Vertex(id);
				index.Add(id, Vertex);
			}
			else
			{
				Vertex = index[id];
			}
			vertices.Add(Vertex);

			var dependsOn = p.GetProperty("adjacentIds").EnumerateArray().Select(i => i.GetString());
			foreach (var d in dependsOn)
			{
				Vertex dep;
				if (!index.ContainsKey(d))
				{
					dep = new Vertex(d);
					index.Add(d, dep);
				}
				else
				{
					dep = index[d];
				}

				Vertex.AdjacentIds.Add(dep);
			}
		}
		return (vertices,index);
	}
}
