<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	int decryptionKeySize = 24;
	int validationKeySize = 64;
	string[] commandLineArgs = System.Environment.GetCommandLineArgs();
	string decryptionKey = CreateKey(decryptionKeySize);
	string validationKey = CreateKey(validationKeySize);
	
	Console.WriteLine("<machineKey\r\n\tvalidationKey=\"{0}\"\r\n\tdecryptionKey=\"{1}\"\r\n\tvalidation=\"SHA1\"/>", validationKey, decryptionKey);
}	

static string CreateKey(int numBytes) 
{
	RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
	byte[] buff = new byte[numBytes];
	
	rng.GetBytes(buff);
	return BytesToHexString(buff);
}

static string BytesToHexString(byte[] bytes) 
{
	StringBuilder hexString = new StringBuilder(64);
	
	for (int counter = 0; counter < bytes.Length; counter++) 
	{
	hexString.Append(String.Format("{0:X2}", bytes[counter]));
	}
	return hexString.ToString();
}