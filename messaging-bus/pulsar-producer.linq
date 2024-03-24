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

const string myTopic = "persistent://public/default/mytopic";

// connecting to pulsar://localhost:6650
await using var client = PulsarClient.Builder().Build();

// produce a message
await using var producer = client.NewProducer(Schema.String).Topic(myTopic).Create();
await producer.Send("Hello World");

// consume messages
await using var consumer = client.NewConsumer(Schema.String)
	.SubscriptionName("MySubscription")
	.Topic(myTopic)
	.InitialPosition(SubscriptionInitialPosition.Earliest)
	.Create();

await foreach (var message in consumer.Messages())
{
	Console.WriteLine($"Received: {message.Value()}");
	await consumer.Acknowledge(message);
}
