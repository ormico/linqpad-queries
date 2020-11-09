<Query Kind="Statements" />

string u = "http://bing.com/m";
Uri a = new Uri(u); 
UriBuilder b = new UriBuilder(u);

a.Dump();
a.Scheme.Dump();

//a.Scheme = "https";
//a.Dump();

b.Scheme.Dump();
b.Scheme = "https";

// set to -1 to tell it to use default port
// when we changed the scheme it didn't change the port so it now prints :80 (which is also wrong)
// setting to -1 tells it to use default port and not print it
b.Port = -1;
b.Dump();

b.ToString().Dump();
b.Uri.ToString().Dump();
b.Uri.AbsoluteUri.Dump();
a.ToString().Dump();

UriBuilder c = new UriBuilder("http://Bing.com");
c.Host.Dump();
