<Query Kind="Program">
  <NuGetReference>Microsoft.Extensions.Configuration</NuGetReference>
  <Namespace>Microsoft.Extensions.Configuration</Namespace>
</Query>

void Main()
{
	var config = CreateTestConfigurationSection();
	Console.WriteLine($"a = {ReadFeatureFlag(config, "useAlpha", true)}");
	Console.WriteLine($"b = {ReadFeatureFlag(config, "useBeta", true)}");
	Console.WriteLine($"c = {ReadFeatureFlag(config, "useGamma", true)}");
	Console.WriteLine($"d = {ReadFeatureFlag(config, "useDelta", true)}");
	Console.WriteLine($"e = {ReadFeatureFlag(config, "useEpsilon", true)}");
	Console.WriteLine($"f = {ReadFeatureFlag(config, "useZeta", true)}");
	Console.WriteLine($"g = {ReadFeatureFlag(config, "useEta", true)}");
	Console.WriteLine($"h = {ReadFeatureFlag(config, "useTheta", true)}");
	Console.WriteLine($"i = {ReadFeatureFlag(config, "useIota", true)}");
	Console.WriteLine($"j = {ReadFeatureFlag(config, "useKappa", true)}");
	Console.WriteLine($"k = {ReadFeatureFlag(config, "useLambda", true)}");
	Console.WriteLine($"l = {ReadFeatureFlag(config, "useMu", true)}");

	Console.WriteLine();
	Console.WriteLine($"a = {config.ReadFlag("useAlpha", true)}");
	Console.WriteLine($"b = {config.ReadFlag("useBeta")}");
}

public static IConfigurationSection CreateTestConfigurationSection()
{
	// prefix each key name with the name of the section
	string sectionName = "mySection";
	var configValues = new Dictionary<string, string>()
	{
		{$"{sectionName}:useAlpha", "true" },
		{$"{sectionName}:useBeta", "True" },
		{$"{sectionName}:useGamma", "false" },
		{$"{sectionName}:useDelta", "False" },
		{$"{sectionName}:useEpsilon", "Yes" },
		{$"{sectionName}:useZeta", "No" },		//true
		{$"{sectionName}:useEta", "TRUE" },
		{$"{sectionName}:useTheta", "FALSE" },	//true
		{$"{sectionName}:useIota", "T" },
		{$"{sectionName}:useKappa", "F" },		//true
		{$"{sectionName}:useLambda", "1" },
		{$"{sectionName}:useMu", "0" },			//true
	};

	// create a test IConfigurationSection by creating a Config Builder, adding the dictionary
	// through the In memory collection provider, building to create the IConfigurationRoot
	// and calling GetSection() to get the section under root that we populated.
	var root = new ConfigurationBuilder().AddInMemoryCollection(configValues).Build();
	var rc = root.GetSection(sectionName);
	return rc;
}

static bool ReadFeatureFlag(IConfigurationSection featureFlagsSection, string flagName, bool defaultValue)
{
	bool rc = bool.TryParse(featureFlagsSection[flagName], out rc) ? rc : defaultValue;
	return rc;
}

public static class IConfigurationSectionExtension
{
	public static bool ReadFlag(this IConfigurationSection featureFlagsSection, string flagName, bool defaultValue = false)
	{
		bool rc = bool.TryParse(featureFlagsSection[flagName], out rc) ? rc : defaultValue;
		return rc;
	}
}