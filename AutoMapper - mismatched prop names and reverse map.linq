<Query Kind="Program">
  <NuGetReference>AutoMapper</NuGetReference>
  <Namespace>AutoMapper</Namespace>
</Query>

void Main()
{
	var a = new Alpha
	{
		Id = 99,
		Name = "Stan"
	};
	var config = new MapperConfiguration(cfg =>
		{
			// create a mappting from Alpha to Alpha 2, 
			// then specify that FullName in destination should map from Name in source
			cfg.CreateMap<Alpha, Alpha2>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name)).ReverseMap();
		}
	);
	var mapper = config.CreateMapper();
	var a2 = mapper.Map<Alpha2>(a);
	
	a2.Dump();
	
	var a2B = new Alpha2
	{
		Id = 100,
		FullName = "Bob"
	};
	
	var aB = mapper.Map<Alpha>(a2B);
	aB.Dump();
}

// Define other methods and classes here

public class Alpha
{
	public int Id { get; set; }
	public string Name { get; set; }
}

public class Alpha2
{
	public int Id { get; set; }
	public string FullName { get; set; }
}

/*
class AutomapperLogicProfile : Profile
{
	public AutomapperLogicProfile()
	{
		CreateMap<Alpha, Alpha2>();
	}
}
*/