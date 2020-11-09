<Query Kind="Statements" />

DateTime start;
DateTime stop;
StringBuilder sb = new StringBuilder();
string s1 = "abcdefghijklmnopqrstuvwxyz";
int i1 = 12345;
double d1 = 1234.56789;

object[] o = { s1, i1, d1 };

start = DateTime.Now;
for(int i=0;i<1000;i++)
{
	for(int j=0;j<o.Length;j++)
	{
		sb.Append(o[j].ToString());
		sb.Append(",");
	}
}
stop = DateTime.Now;

TimeSpan ts1 = stop - start;

sb = new StringBuilder();
start = DateTime.Now;
for(int i=0;i<1000;i++)
{
	for(int j=0;j<o.Length;j++)
	{
		sb.Append(o[j].GetHashCode());
		sb.Append(",");
	}
}
stop = DateTime.Now;

TimeSpan ts2 = stop - start;

ts1.Dump();
ts2.Dump();

string s2 = "abcdef";
string s3 = "ghijkl";
string s4 = "mnopqrstuv";
string s5 = "wxyz";

sb = new StringBuilder();
sb.Append(s2);
sb.Append(s3);
sb.Append(s4);
sb.Append(s5);

string s6 = sb.ToString();

s1.GetHashCode().Dump();
s6.GetHashCode().Dump();

int i2 = 116;

int i3 = 0;
for(int i=0;i<116;i++)
{
	i3++;
}

i2.GetHashCode().Dump();
i3.GetHashCode().Dump();
