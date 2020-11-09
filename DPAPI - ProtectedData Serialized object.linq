<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Namespace>System.Runtime.Serialization.Json</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	SecureConfigData x = new SecureConfigData()
	{
		ID = "100", Name = "Moore", Updated = DateTime.Now
	};
	
	SaveSecureConfigData("Talmer", x);
	
	SecureConfigData z = GetSecureConfigData("Talmer");
	
	z.Dump();
}

public SecureConfigData GetSecureConfigData(string SourceName)
{
  SecureConfigData rc = null;
  if (!string.IsNullOrWhiteSpace(SourceName))
  {
      string dst = SourceName.Trim().ToLower();
      byte[] entropy = Encoding.ASCII.GetBytes(dst);
      string path = string.Format("{0}.dat", dst);
      if(File.Exists(path))
      {
          byte[] encryptedDat = File.ReadAllBytes(path);
          byte[] decryptedDat = ProtectedData.Unprotect(encryptedDat, entropy, DataProtectionScope.LocalMachine);
          string json = Encoding.ASCII.GetString(decryptedDat);
          DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SecureConfigData));

          using (MemoryStream ms = new MemoryStream(decryptedDat))
          {
              rc = ser.ReadObject(ms) as SecureConfigData;
          }
		  
		  File.Delete(path);
      }
      else
      {
          Console.WriteLine("Encrypted data file not found: '{0}'", Path.GetFullPath(path));
          //throw new ApplicationException("Encrypted data file not found");
      }
  }

  return rc;
}

public void SaveSecureConfigData(string SourceName, SecureConfigData Scd)
{
  if (!string.IsNullOrWhiteSpace(SourceName))
  {
      string dst = SourceName.Trim().ToLower();
      byte[] entropy = Encoding.ASCII.GetBytes(dst);
      string path = string.Format("{0}.dat", dst);

      Console.WriteLine("Saving Encrypted data file: '{0}'", Path.GetFullPath(path));

      DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(SecureConfigData));
      string json;

      using (MemoryStream ms = new MemoryStream())
      {
          ser.WriteObject(ms, Scd);
          ms.Position = 0;
          using (StreamReader sr = new StreamReader(ms))
          {
              json = sr.ReadToEnd();
          }
      }
      byte[] decryptedDat = Encoding.ASCII.GetBytes(json);
      byte[] encryptedDat = ProtectedData.Protect(decryptedDat, entropy, DataProtectionScope.LocalMachine);
      File.WriteAllBytes(path, encryptedDat);
  }
}

public class SecureConfigData
{
	public string ID { get; set; }
	public string Name { get; set; }
	public DateTime Updated { get; set; }
}