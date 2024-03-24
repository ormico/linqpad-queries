<Query Kind="Program" />

void Main()
{
	string[] solutionFolders = new string[]
	{
		@"C:\repos\CRM.Solutions.SustainabilityService\solutions\SustainabilityDataModel\Solution",
		@"C:\repos\CRM.Solutions.SustainabilityService\solutions\Sustainability\Solution",
		@"C:\repos\CRM.Solutions.SustainabilityService\solutions\SustainabilityWaterDataModel\Solution"
	};
	Dictionary<string, Entity> entities = new(); 
	foreach (var s in solutionFolders)
	{
		LoadEntities(Path.Combine(s, "entities"), entities);
		//LoadRelationships(Path.Combine(rootPath, "Other", "Relationships"), entities);
	}

	Console.WriteLine();
	Console.WriteLine("' --------------------------------------------------------------------------------------- '");
	Console.WriteLine();
	
	foreach(var i in entities.Values)
	{
		Console.WriteLine($"entity \"{i.Name}\" as {i.Name} {{");

		foreach (var j in i.Attributes.Values)
		{
			if(!string.Equals(j.DataType, "primarykey", StringComparison.OrdinalIgnoreCase))
			{
				continue;
			}
			
			Console.WriteLine($"    *{j.Name} {j.DataType}");
		}
		
		Console.WriteLine($"    --");
		
		foreach (var j in i.Attributes.Values)
		{
			if (string.Equals(j.DataType, "primarykey", StringComparison.OrdinalIgnoreCase) || SkipAttribute(j.Name))
			{
				continue;
			}

			var rl = string.Empty;
			var rl2 = string.Empty;
			if(string.Equals(j.RequiredLevel,"required", StringComparison.OrdinalIgnoreCase))
			{
				rl = "*";
			}
			else if(string.Equals(j.RequiredLevel,"systemrequired", StringComparison.OrdinalIgnoreCase))
			{
				rl = "* **";
				rl2 = "**";
			}
			
			string dataType = j.DataType;
			if(j.MaxLength.HasValue)
			{
				dataType = $"{j.DataType}({j.MaxLength})";
			}
			
			if(!string.IsNullOrWhiteSpace(j.Format) && !string.Equals(j.Format, "none", StringComparison.OrdinalIgnoreCase) && !string.Equals(j.Format, "required", StringComparison.OrdinalIgnoreCase))
			{
				dataType = $"{dataType} <<{j.Format}>>";
			}
			
			Console.WriteLine($"    {rl}{j.Name} {dataType}{rl2}");
		}
		
		Console.WriteLine("}");
		
		Console.WriteLine();
	}

	/*
	foreach (var folder in Directory.GetDirectories(rootPath))
	{
		Console.WriteLine(folder);
		//var i = Path.GetDirectoryName(folder)?.ToLowerInvariant();
		var i = Path.GetFileName(folder)?.ToLowerInvariant();


		switch(i)
		{
			case "entities":
				entities = LoadEntities(folder);
				break;
			case "optionsets":
				break;
			case "other"://Relationships
				//LoadRelationships(Path.Combine(folder, "Relationships"), entities);
				break;
		}		
	}
	DFS(rootPath, (f) => {
		Console.WriteLine(f);
		return;
	});
	*/
}

class Entity
{
	public string Name { get; set; }
	public Dictionary<string, Attribute> Attributes { get; set; } = new();
	public Dictionary<string, Relationship> Relationships { get; set; } = new();
}

class Attribute
{
	public string Name { get; set; }
	public string DataType { get; set; }
	public string RequiredLevel { get; set; }
	public string Format { get; set; }
	public int? MaxLength { get; set; }	
}

class Relationship
{
	public string Name { get; set; }
	public string RelationshipType { get; set; }
	
	public Entity ReferencingEntity { get; set; }
	public string ReferencingAttributeName { get; set; }

	public Entity ReferencedEntity { get; set; }
	public string ReferencedAttributeName { get; set; }

}

void LoadEntities(string folder, Dictionary<string, Entity> entities)
{
	Dictionary<string, Entity> rc = new();
	Console.WriteLine($"Folder '{folder}'");

	foreach (var subfolder in Directory.GetDirectories(folder))
	{
		string entityFilePathName = Path.Combine(subfolder, "Entity.xml");

		if(File.Exists(entityFilePathName))
		{
			Entity entity = null;
			XDocument xdoc = XDocument.Load(entityFilePathName);			
			var xEntity = xdoc.Element("Entity")?.Element("EntityInfo")?.Element("entity");
			
			if(xEntity == null)
			{
				Console.WriteLine($"Skipped '{entityFilePathName}'");
				continue;
			}
			
			var tmpEntName = xEntity.Attribute("Name").Value;
			
			if (entities.ContainsKey(tmpEntName))
			{
				entity = entities[tmpEntName];
			}
			else
			{
				entity = new();
				entity.Name = tmpEntName;
				entities.Add(entity.Name, entity);
			}

			var xAttributes = xEntity.Element("attributes").Elements("attribute");
			foreach(var xa in xAttributes)
			{
				Attribute a = new();
				// PhysicalName or Name?
				a.Name = xa.Element("Name").Value;
				a.DataType = xa.Element("Type").Value;
				a.RequiredLevel = xa.Element("RequiredLevel").Value;
				a.Format = xa.Element("RequiredLevel").Value;
				var xMaxLength = xa.Element("MaxLength");
				if(xMaxLength != null && !xMaxLength.IsEmpty)
				{
					a.MaxLength = int.Parse(xa.Element("MaxLength").Value);
				}
				
				if(entity.Attributes.ContainsKey(a.Name))
				{
					Console.WriteLine($"[{entity.Name}].[{a.Name}] already exists.");
					var a1 = entity.Attributes[a.Name];
					Console.WriteLine($"OLD) {a1.DataType} {a1.MaxLength} {a1.RequiredLevel} {a1.Format}");
					Console.WriteLine($"NEW) {a.DataType} {a.MaxLength} {a.RequiredLevel} {a.Format}");
					entity.Attributes[a.Name] = a;
				}
				else
				{
					entity.Attributes.Add(a.Name, a);
				}
				
			}			
		}
	}
}

bool SkipAttribute(string attributeName)
{
	bool rc = false;
	switch(attributeName?.ToLowerInvariant())
	{
		case "createdby":
		case "createdon":
		case "createdonbehalfby":
		case "importsequencenumber":
		case "modifiedby":
		case "modifiedon":
		case "modifiedonbehalfby":
		case "overriddencreatedon":
		case "statecode":
		case "statuscode":
		case "timezoneruleversionnumber":
		case "utcconversiontimezonecode":
		case "owningbusinessunit":
		case "owningteam":
		case "owninguser":
		case "ownerid":
			rc = true;
			break;
	}
	return rc;
}

/*
void LoadRelationships(string folder, Dictionary<string, Entity> entities)
{
	foreach (var fn in Directory.GetFiles(folder, "*.xml"))
	{		
		Entity entity = new();

		XDocument xdoc = XDocument.Load(entityFilePathName);
		var xEntity = xdoc.Element("Entity").Element("EntityInfo").Element("entity");
		entity.Name = xEntity.Attribute("Name").Value;
		var xAttributes = xEntity.Element("attributes").Elements("attribute");
		foreach (var xa in xAttributes)
		{
			Attribute a = new();
			// PhysicalName or Name?
			a.Name = xa.Element("Name").Value;
			a.DataType = xa.Element("Type").Value;
			a.RequiredLevel = xa.Element("RequiredLevel").Value;
			entity.Attributes.Add(a.Name, a);
		}

	}

	return rc;
}
*/

void DFS(string folder, Action<string> visit)
{
	if (folder != null)
	{
		visit(folder);
		foreach (var subfolder in Directory.GetDirectories(folder))
		{
			//Console.WriteLine(subfolder);
			DFS(subfolder, visit);
		}
	}
}

