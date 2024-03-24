<Query Kind="Statements">
  <NuGetReference>DotPulsar</NuGetReference>
  <Namespace>DotPulsar</Namespace>
  <Namespace>DotPulsar.Extensions</Namespace>
</Query>

/*
# Apache Pulsar - Test Consumer from C#
https://pulsar.apache.org/

## dotPulsar - official Apache Pulsar .NET client library
https://github.com/apache/pulsar-dotpulsar

## To run a local test Pulsar cluster on Docker
https://pulsar.apache.org/docs/3.2.x/getting-started-docker/

(Windows Powershell syntax):
docker run -it `
    -p 6650:6650 `
    -p 8080:8080 `
    --mount source=pulsardata,target=/pulsar/data `
    --mount source=pulsarconf,target=/pulsar/conf `
    apachepulsar/pulsar:3.2.1 `
    bin/pulsar standalone

# Notes:
To run this test, run the Pulsar Producer and Consumer. The Producer will keep running 
until you manually Cancel or End it.
*/
Console.WriteLine("START");

const string myTopic = "persistent://public/default/mytopic";

// connect to localhost on default port to pulsar://localhost:6650
// to connect to a different url use the ServiceUrl() extension on Builder()
await using var client = PulsarClient.Builder()
	//.ServiceUrl(new Uri("pulsar://localhost:6650"))
	.Build();

// produce a message
await using var producer = client.NewProducer(Schema.String).Topic(myTopic).Create();

var r = new Random();
for (int i = 0; i < 100; i++)
{
	int s = r.Next(5);
	string msg = $"Hello World - {i} - {s}";
	System.Threading.Thread.Sleep(TimeSpan.FromSeconds(s));
	await producer.Send(msg);
	Console.WriteLine($"Sent: msg");
}

Console.WriteLine("END");



