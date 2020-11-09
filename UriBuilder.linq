<Query Kind="Statements" />

Uri url = new Uri("https://www.google.com/query/q.aspx?a=abba&b=dabba");
url.Dump();

url.Scheme.Dump();
url.Host.Dump();
url.PathAndQuery.Dump();

UriBuilder urlb = new UriBuilder(url);

Console.WriteLine(urlb.ToString());

urlb.Query += "&c=doo";
Console.WriteLine(urlb.ToString());

urlb.Query = "c=doo";
Console.WriteLine(urlb.ToString());

Console.WriteLine(urlb.Uri.ToString());
