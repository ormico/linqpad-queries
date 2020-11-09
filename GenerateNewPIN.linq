<Query Kind="Program" />

void Main()
{
	rand = System.Security.Cryptography.RandomNumberGenerator.Create();
	byte[] randBytes = new byte[4];
	for (int i = 0; i < 10; i++)
	{
		Console.WriteLine("{0}", GeneratePassword(8));
	}
}

System.Security.Cryptography.RandomNumberGenerator rand;
byte[] randBytes = new byte[4];

string GeneratePassword(int length)
{
	string rc = "";
	StringBuilder sb = new StringBuilder();

	for (int j = 0; j < length; j++)
	{
		sb.AppendFormat("{0}", GetRand(9));
		if(j == 3)
		{
			sb.Append(" ");
		}
	}
	
	rc = sb.ToString();

	return rc;
}

int GetRand(int max)
{
	rand.GetBytes(randBytes);
	int randomNumber = Math.Abs(BitConverter.ToInt32(randBytes, 0));
	int rc = randomNumber % max;
	return rc;
}