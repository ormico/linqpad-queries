<Query Kind="Statements">
  <Namespace>System.Text.RegularExpressions</Namespace>
</Query>

string f = @"/*
Put any assembly info here that you want included in each project.
Then include this file in each project (not a copy).
*/
using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyCompany(""Regency Hospital"")]
[assembly: AssemblyProduct(""RegencyHospital"")]
[assembly: AssemblyCopyright(""Copyright (c) 2010"")]
//[assembly: AssemblyVersion(""1.0.0.0"")]
[assembly: AssemblyVersion(""1.1.0.0"")]
[assembly: AssemblyDescription("""")]
";

Regex r = new Regex(@"^\[assembly: AssemblyVersion\(\""(\d+)\.(\d+)\.(\d+)\.(\d+)\""\)\]", RegexOptions.Multiline);

var matches = r.Matches(f);
matches.Count.Dump();
int build = -2;
//matches.Dump();

foreach(Match m in matches)
{
	GroupCollection groups = m.Groups;
	for(int i=0;i<groups.Count;i++)
	{
		string.Format("{0}: {1}", i, groups[i].Value).Dump();
	}

	string buildStr = groups[3].Value;
	build = int.Parse(buildStr);
	break;
}

build++;

string replStr = string.Format(@"[assembly: AssemblyVersion(""$1.$2.{0}.$4"")]", build);
string g = r.Replace(f, replStr);

f.Dump();
g.Dump();