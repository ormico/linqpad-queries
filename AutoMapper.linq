<Query Kind="Program">
  <NuGetReference>AutoMapper</NuGetReference>
  <Namespace>AutoMapper.Mappers</Namespace>
  <Namespace>AutoMapper.QueryableExtensions</Namespace>
</Query>

/*
AutoMapper test.

!! This doens't seem to be working now. Needs updating !!

Standard mapping: Alpha to Beta. Alpha has properties not in Beta and Beta has properties not in Alpha.
*/
void Main()
{
	Alpha alpha = new Alpha() { A = "aaa", /* B="bbb",*/ C = "ccc", D = "ddd" };

	//--------------------------------------------------------------------------------------------------------
	Console.WriteLine("standard mapping of Alpha to Beta. Any property that in Alpha that matches a property in Beta will map");
	//--------------------------------------------------------------------------------------------------------
	AutoMapper.Mapper.Initialize(
		cfg => cfg.CreateMap<Alpha, Beta>()
	);
	Beta beta = AutoMapper.Mapper.Map<Beta>(alpha);
	Console.WriteLine("{0}, {1}, {2}", beta.A, beta.B, beta.C);

	//--------------------------------------------------------------------------------------------------------
	Console.WriteLine("use map to copy obj in constructor w/protected setters");
	//--------------------------------------------------------------------------------------------------------
	AutoMapper.Mapper.Initialize(
		cfg => cfg.CreateMap<SelfCopy, SelfCopy>()
	);
	SelfCopy self = new SelfCopy("a", "b", "c", "d");
	SelfCopy selfCopy = new SelfCopy(self);
	Console.WriteLine(selfCopy.ToString());
	selfCopy = new SelfCopy(self, "blahblahblah");
	Console.WriteLine(selfCopy.ToString());

	//--------------------------------------------------------------------------------------------------------
	Console.WriteLine("if property C in Alpha is null, don't map it to a property value in Beta");
	//--------------------------------------------------------------------------------------------------------
	AutoMapper.Mapper.Initialize(
		cfg => cfg.CreateMap<Alpha, Beta>().ForMember(s => s.C, o => o.Condition(src => src.C != null))
	);
	alpha.A = "xxx";
	alpha.C = null;
	AutoMapper.Mapper.Map(alpha, beta);
	Console.WriteLine("{0}, {1}, {2}", beta.A, beta.B, beta.C);

	//--------------------------------------------------------------------------------------------------------
	Console.WriteLine("if any property value in Alpha is null, don't map it to a property value in Beta");
	//--------------------------------------------------------------------------------------------------------
	AutoMapper.Mapper.Initialize(
		cfg => cfg.CreateMap<Alpha, Beta>().ForAllMembers(o => o.Condition((aSrc, bDest, aSrcProp, bDestProp) =>
			{
				bool rc = (aSrcProp != null);
				return rc;
			}))
	);
	alpha.A = "zzz";
	alpha.C = null;
	AutoMapper.Mapper.Map(alpha, beta);
	Console.WriteLine("{0}, {1}, {2}", beta.A, beta.B, beta.C);

	//--------------------------------------------------------------------------------------------------------
	Console.WriteLine("standard map of objects with lists");
	//--------------------------------------------------------------------------------------------------------
	AutoMapper.Mapper.Initialize(
		cfg => cfg.CreateMap<Gamma, Delta>()
	);
	Gamma gamma = new Gamma() { A = "aaa", C = "ccc", D = "ddd", E = new List<string>() { "asdf", "jkl;", "qwer", "uiop" } };
	Delta delta = AutoMapper.Mapper.Map<Delta>(gamma);
	Console.WriteLine("{0}, {1}, {2}, {3}", delta.A, delta.B, delta.C, string.Join("|", delta.E));

	//--------------------------------------------------------------------------------------------------------
	Console.WriteLine("map update objects with lists");
	//--------------------------------------------------------------------------------------------------------
	AutoMapper.Mapper.Initialize(
		cfg => cfg.CreateMap<Gamma, Delta>()
	);
	gamma = new Gamma() { A = "xxx", C = null, D = "ddd", E = new List<string>() { "asdf", "jkl;", "qwer", "qqqq" } };
	AutoMapper.Mapper.Map(gamma, delta);
	Console.WriteLine("{0}, {1}, {2}, {3}", delta.A, delta.B, delta.C, string.Join("|", delta.E));

	//--------------------------------------------------------------------------------------------------------
	Console.WriteLine("map of objects with lists of sub-objects");
	//--------------------------------------------------------------------------------------------------------
	AutoMapper.Mapper.Initialize(cfg =>
	{
		cfg.CreateMap<Gamma, Delta>().ForMember(dest => dest.F, opts => opts.Ignore());
		cfg.CreateMap<GammaKeyValue, DeltaKeyValue>();
	});

	gamma = new Gamma()
	{
		A = "aaa", C = "ccc", D = "ddd", E = new List<string>() { "asdf", "jkl;", "qwer", "qqqq" },
		F = new List<GammaKeyValue>()
		{
			new GammaKeyValue() { Key = "first", Value = "asdf"},
			new GammaKeyValue() { Key = "second", Value = "jkl;"},
			new GammaKeyValue() { Key = "third", Value = "qwer"},
			new GammaKeyValue() { Key = "fourth", Value = "uiop"},
		}
	};
	delta = AutoMapper.Mapper.Map<Delta>(gamma);

	if (delta.F == null)
	{
		delta.F = new List<UserQuery.DeltaKeyValue>();
	}

	// this will update elements in delta that are also in gamma
	// but since we just created delta from gamma and just init'ed delta.F
	// to a new empty list, then nothing is in delta.F and nothing will get
	// updated.
	for (int i = 0; i < gamma.F.Count(); i++)
	{
		var curF = gamma.F[i];
		var updatedF = delta.F.FirstOrDefault(m => m.Key == curF.Key);
		AutoMapper.Mapper.Map(curF, updatedF);
	}

	Console.WriteLine("{0}, {1}, {2}, {3}", delta.A, delta.B, delta.C, string.Join("|", delta.E));
	foreach (var i in delta.F)
	{
		Console.WriteLine("- {0}", i);
	}

	//--------------------------------------------------------------------------------------------------------
	Console.WriteLine("map - merge objects, don't overwrite when null, merge lists of sub-objects");
	//--------------------------------------------------------------------------------------------------------
	AutoMapper.Mapper.Initialize(cfg =>
	{
		cfg.CreateMap<Gamma, Delta>()
			.ForMember(dest => dest.F, opts => opts.Ignore())
			.ForAllMembers(o => o.Condition((aSrc, bDest, aSrcProp, bDestProp) =>
			{
				bool rc = (aSrcProp != null);
				return rc;
			}));
		cfg.CreateMap<GammaKeyValue, DeltaKeyValue>();
	});
			
    gamma = new Gamma()
	{
		A = "aaa",
		C = "ccc",
		D = "ddd",
		E = new List<string>() { "asdf", "jkl;", "qwer", "qqqq" },
		F = new List<GammaKeyValue>()
		{
			new GammaKeyValue() { Key = "first", Value = "asdf"},
			new GammaKeyValue() { Key = "second", Value = "jkl;"},
			new GammaKeyValue() { Key = "third", Value = "qwer"},
			new GammaKeyValue() { Key = "fourth", Value = "uiop"}
		}
	};
	delta = new Delta()
	{
		B = "bbb",
		C = "xxx",
		E = new List<string>() { "1234" },
		F = new List<DeltaKeyValue>()
		{
			new DeltaKeyValue() { Key = "fifth", Value = "rrrr" },
			new DeltaKeyValue() { Key = "sixth", Value = "tttt" }
		}
	};
	AutoMapper.Mapper.Map(gamma, delta);

	if (delta.F == null)
	{
		delta.F = new List<UserQuery.DeltaKeyValue>();
	}

	// this will update elements in delta that are also in gamma
	// but since we just created delta from gamma and just init'ed delta.F
	// to a new empty list, then nothing is in delta.F and nothing will get
	// updated.
	for (int i = 0; i < gamma.F.Count(); i++)
	{
		var curF = gamma.F[i];
		var updatedF = delta.F.FirstOrDefault(m => m.Key == curF.Key);
		if(updatedF == null)
		{
			updatedF = new DeltaKeyValue();
			delta.F.Add(updatedF);
		}
		
		AutoMapper.Mapper.Map(curF, updatedF);
	}

	Console.WriteLine("{0}, {1}, {2}, {3}", delta.A, delta.B, delta.C, string.Join("|", delta.E));
	foreach (var i in delta.F)
	{
		Console.WriteLine("- {0}", i);
	}
}

class Alpha
{
	public string A { get; set; }
	//public string B { get; set; }
	public string C { get; set; }
	public string D { get; set; }
}

class Beta
{
	public string A { get; set; }
	public string B { get; set; }
	public string C { get; set; }
	//public string D { get; set; }
}

class Gamma: Alpha
{
	public List<string> E { get; set; }
	public List<GammaKeyValue> F { get; set;}
}

class GammaKeyValue
{
	public string Key { get; set; }
	public string Value { get; set; }
}

class Delta: Beta
{
	public List<string> E { get; set; }
	public List<DeltaKeyValue> F { get; set; }
}

class DeltaKeyValue
{
	public string Key { get; set; }
	public string Value { get; set; }
	public override string ToString()
	{
		return string.Format("k:{0} v:{1}", Key, Value);
	}
}

class SelfCopy
{
	public SelfCopy(string A, string B, string C, string D)
	{
		this.A = A;
		this.B = B;
		this.C = C;
		this.D = D;
	}
	
    public SelfCopy(SelfCopy source, string D = null)
	{
		AutoMapper.Mapper.Map(source, this);
		if (D != null)
		{
			this.D = D;
		}
	}

	public string A { get; protected set; }
	public string B { get; protected set; }
	public string C { get; protected set; }
	public string D { get; protected set; }

	public override string ToString()
	{
		return $"{A} {B} {C} {D}";
	}
}