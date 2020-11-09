<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>RestSharp</NuGetReference>
  <NuGetReference>System.Net.Http</NuGetReference>
  <NuGetReference>System.Net.Http.Formatting.Extension</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>RestSharp</Namespace>
  <Namespace>RestSharp.Authenticators</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

// without an api key, this will return an error but it will be a valid JSON object
// and will demonstrate that we can call the service

HttpClient client = new HttpClient();
var response = await client.GetAsync("https://maps.googleapis.com/maps/api/place/nearbysearch/json");
response.StatusCode.Dump();
await response.Content.ReadAsStringAsync().Dump();
