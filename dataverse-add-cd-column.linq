<Query Kind="Program" />

void Main()
{
	/* TODO: 
	use datamodel and water data model entity folder
	use list of tables to modify
	for each table, check if CustomDimension column already exists
	if exists, then update
	if not, then add it.
	
	could avoid update if we want to just revert changes in git and re-run add
	
	need to add CustomDimension column manually to 1 table, then export to capture xml to add
	*/
	string rootFolder = @"C:\repos\CRM.Solutions.SustainabilityService\solutions\SustainabilityDataModel\Solution\Entities";
	//string rootFolder = @"C:\repos\CRM.Solutions.SustainabilityService\solutions\SustainabilityWaterDataModel\Solution\Entities";
	string cdAttributeXMLFilePath = @"C:\Users\zackmoore\OneDrive - Microsoft\Documents\CustomDimensions\dataverse-entity-customdimension-attribute-fragment.xml";
	HashSet<string> set = new()
	{
		"msdyn_businesstravel",
		"msdyn_capitalgood",
		"msdyn_emission",
		"msdyn_employeecommuting",
		"msdyn_endoflifetreatmentofsoldproducts",
		"msdyn_fugitiveemission",
		"msdyn_industrialprocess",
		"msdyn_mobilecombustion",
		"msdyn_purchasedenergy",
		"msdyn_purchasedgoodandservice",
		"msdyn_stationarycombustion",
		"msdyn_transportationanddistribution",
		"msdyn_wastegeneratedinoperations",
		"msdyn_waterquantity",
		"msdyn_watersample",
		"msdyn_watertestresult",
	};

	foreach (var subfolder in Directory.GetDirectories(rootFolder))
	{
		string tmpEntityFolderName = Path.GetFileName(subfolder);
		if(!set.Contains(tmpEntityFolderName))
		{
			continue;
		}
		
		string entityFilePathName = Path.Combine(subfolder, "Entity.xml");

		if(File.Exists(entityFilePathName))
		{
			XDocument xdoc = XDocument.Load(entityFilePathName);			
			var xEntity = xdoc.Element("Entity")?.Element("EntityInfo")?.Element("entity");
			
			if(xEntity == null)
			{
				Console.WriteLine($"Skipped '{entityFilePathName}' b/c no Entity");
				continue;
			}

			var entityName = xEntity.Attribute("Name").Value;
			Console.WriteLine($"Entity '{entityFilePathName}'");
			var xAttributes = xEntity.Element("attributes").Elements();
		
			XElement xNewCd = XElement.Load(cdAttributeXMLFilePath);			
			string n = xNewCd?.Element("Name")?.Value;
			XElement xprev = null;
			if(xAttributes.Count() == 0)
			{
				xEntity.Element("attributes").Add(xNewCd);
			}
			else
			{				
				foreach (var xcur in xAttributes)
				{
					string p = xprev?.Element("Name")?.Value;
					string c = xcur.Element("Name")?.Value;

					int compareNP = string.Compare(n, p);
					int compareNC = string.Compare(n, c);

					if (p == null && compareNC < 0)
					{
						//xcur.AddBeforeSelf(xNewCd);
						
						break;
					}
					else if (compareNC == 0)
					{
						xcur.ReplaceWith(xNewCd);
						break;
					}
					else if (compareNP >= 0 && compareNC < 0)
					{
						//xcur.AddBeforeSelf(xNewCd);
						xprev.AddAfterSelf(xNewCd);
						break;
					}
					else if (compareNC > 0 && xcur.NextNode == null)
					{
						xcur.AddAfterSelf(xNewCd);
						break;
					}

					xprev = xcur;
				}
			}

			xdoc.Save(entityFilePathName);
		}
	}
}
