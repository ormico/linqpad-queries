<Query Kind="Program" />

void Main()
{
	byte[] x = new byte[] { 0, 1, 2, 99 };
	StringExt.HexStr(x).Dump();
	StringExt.HexStr(x, true).Dump();
	
	x.ToHexString().Dump();
	x.ToHexString(true).Dump();
}

//todo: Hex to Byte array

// PZahra
// https://social.msdn.microsoft.com/Forums/vstudio/en-US/3928b8cb-3703-4672-8ccd-33718148d1e3/byte-array-to-hex-string?forum=csharpgeneral#06b6d378-9463-4071-b8cc-e4f1228303c5
static class StringExt
{
	public static string ToHexString(this byte[] a, bool prefix = false)
	{
		return HexStr(a, prefix);
	}

	public static string HexStr(byte[] p, bool prefix = false)
	{
		char[] c=null;
		byte b;
		int xstart;
		
		if(prefix)
		{
			c=new char[p.Length*2 + 2];
			c[0]='0';
			c[1]='x';
			xstart = 2;
		}
		else
		{
			c=new char[p.Length*2];
			xstart = 0;
		}
		
		for(int y=0, x=xstart; y<p.Length; ++y, ++x)
		{
			b=((byte)(p[y]>>4));
			c[x]=(char)(b>9 ? b+0x37 : b+0x30);
			b=((byte)(p[y]&0xF));
			c[++x]=(char)(b>9 ? b+0x37 : b+0x30);
		}
		
		return new string(c);
	}
}