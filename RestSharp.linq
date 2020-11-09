<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>RestSharp</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>RestSharp</Namespace>
  <Namespace>RestSharp.Authenticators</Namespace>
</Query>

// without an api key, this will return an error but it will be a valid JSON object
// and will demonstrate that we can call the service

var client = new RestClient("https://maps.googleapis.com");
var request = new RestRequest("maps/api/place/nearbysearch/json", Method.GET);

request.AddParameter("location", "40,-100", ParameterType.GetOrPost);
request.AddParameter("radius", "500", ParameterType.GetOrPost);
request.AddParameter("types", "food", ParameterType.GetOrPost);
request.AddParameter("name", "cruise", ParameterType.GetOrPost);
//request.AddParameter("key", "", ParameterType.GetOrPost);

IRestResponse response = client.Execute(request);
response?.Content.Dump();