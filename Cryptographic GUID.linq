<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	Guid g = CryptoGuid.New();
	g.Dump();
	
	/*
	byte b1 = 0;
	byte b2 = 0;
	string yourByteString;
	
	yourByteString = Convert.ToString(b1, 2).PadLeft(8, '0');
	Console.WriteLine(yourByteString);
	
	Console.WriteLine("Version");
	b1 = (byte)(b2 & 0x0F | 0x40);
	yourByteString = Convert.ToString(b1, 2).PadLeft(8, '0');
	Console.WriteLine(yourByteString);	

	b2 = (byte)(b2 & 0x4F | 0x40);
	yourByteString = Convert.ToString(b2, 2).PadLeft(8, '0');
	Console.WriteLine(yourByteString);	

	Console.WriteLine("Variant");
	b1 = (byte)(b2 & 0x3F | 0x80);
	yourByteString = Convert.ToString(b1, 2).PadLeft(8, '0');
	Console.WriteLine(yourByteString);	

	b2 = (byte)(b2 & 0xBF | 0x80);
	yourByteString = Convert.ToString(b2, 2).PadLeft(8, '0');
	Console.WriteLine(yourByteString);
	*/
}

// https://tools.ietf.org/html/rfc4122#section-4.1.1
public class CryptoGuid
{
	public static Guid New()
	{
		Guid rc;
		byte[] b = new byte[16];
		RandomNumberGenerator.Fill(b);
        
		// clear first 4 bits then set set first 4 bit to 0100
		if(BitConverter.IsLittleEndian)
		{
			b[7] = (byte)(b[7] & 0x0F | 0x40);
		}
		else
		{
			b[6] = (byte)(b[6] & 0x0F | 0x40);
		}

		// clear first 2 bits then set first to bits to 10**
		b[8] = (byte)(b[8] & 0x3F | 0x80);
		rc = new Guid(b);
		
		return rc;
	}
}

// https://tools.ietf.org/html/rfc4122#section-4.1.1
class CryptographicGuidGenerator
{
	public static Guid New()
	{
		byte[] b = new byte[16];
		RandomNumberGenerator.Fill(b);

		// clear first 4 bits then set set first 4 bit to 0100
		if (BitConverter.IsLittleEndian)
		{
			b[7] = (byte)(b[7] & 0x0F | 0x40);
		}
		else
		{
			b[6] = (byte)(b[6] & 0x0F | 0x40);
		}

		// clear first 2 bits then set first to bits to 10**
		b[8] = (byte)(b[8] & 0x3F | 0x80);
		Guid rc = new Guid(b);

		return rc;
	}
}