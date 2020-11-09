<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

byte[] entropy = { 9, 8, 7, 6, 5, 100, 105, 203, 215, 1, 9, 22, 30, 56, 27, 88, 97, 143, 173 };
entropy = Encoding.ASCII.GetBytes("Talmer");

string message = @"{""__type"":""UserQuery.Alpha:#"",""Betas"":[{""Id"":""beta1"",""Name"":""x-men""},{""Id"":""beta2"",""Name"":""super-man""},{""Id"":""beta3"",""Name"":""spider-man""},{""Id"":""beta4"",""Name"":""iron man""},{""Id"":""beta5"",""Name"":""wonder woman""}],""CreateDt"":""\/Date(950590800000-0500)\/"",""Id"":""alpha1"",""Name"":""Bob Smith"",""Value"":101.09}";

byte[] data = Encoding.ASCII.GetBytes(message);
DataProtectionScope scope = DataProtectionScope.LocalMachine;

byte[] encrypted = ProtectedData.Protect(data, entropy, scope);

Console.WriteLine("Encrypted Data:");
for(int i=0; i<encrypted.Length; i++)
{
	Console.Write(encrypted[i]);
	Console.Write(", ");
}

Console.WriteLine();
Console.WriteLine();
//----
//entropy = new byte[entropy.Length];
entropy = Encoding.ASCII.GetBytes("Talmer");
//encrypted = new byte[encrypted.Length];
//----

byte[] decrypted = ProtectedData.Unprotect(encrypted, entropy, DataProtectionScope.LocalMachine);
Console.WriteLine("Decrypted Data:");
for(int i=0; i<decrypted.Length; i++)
{
	Console.Write(decrypted[i]);
	Console.Write(", ");
}

Console.WriteLine();
Console.WriteLine();

Console.WriteLine("Decrypted Message: \"{0}\"", Encoding.ASCII.GetString(decrypted));