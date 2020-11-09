<Query Kind="Program" />

void Main()
{
	rand = System.Security.Cryptography.RandomNumberGenerator.Create();
	byte[] randBytes = new byte[4];
	for (int i = 0; i < 100; i++)
	{
		Console.WriteLine("{0}", GeneratePassword(10));
	}
}

const string LOWERCASE_CHARACTERS = "abcdefghijklmnopqrstuvwxyz";
const string UPPERCASE_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
const string NUMERIC_CHARACTERS = "0123456789";
const string SPECIAL_CHARACTERS = @"!#$%&*@\";

System.Security.Cryptography.RandomNumberGenerator rand;
byte[] randBytes = new byte[4];

string GeneratePassword(int length)
{
	string rc = "";
	StringBuilder sb = new StringBuilder();

	// pick 1 random char from each
	sb.Append(LOWERCASE_CHARACTERS[GetRand(LOWERCASE_CHARACTERS.Length)]);
	sb.Append(UPPERCASE_CHARACTERS[GetRand(UPPERCASE_CHARACTERS.Length)]);
	sb.Append(NUMERIC_CHARACTERS[GetRand(NUMERIC_CHARACTERS.Length)]);

	// add random characters until length is achieved
	string allChars = $"{LOWERCASE_CHARACTERS}{UPPERCASE_CHARACTERS}{NUMERIC_CHARACTERS}";
	int allCharsLength = allChars.Length;
	
	for(int i = 3; i < length; i++)
	{
		sb.Append(allChars[GetRand(allCharsLength)]);
	}

	// now sort characters in a random order
	rc = string.Concat(sb.ToString().ToArray().OrderBy(o => GetRand(length)));

	return rc;
}

int GetRand(int max)
{
	rand.GetBytes(randBytes);
	int randomNumber = Math.Abs(BitConverter.ToInt32(randBytes, 0));
	int rc = randomNumber % max;
	return rc;
}
