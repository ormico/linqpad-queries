<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	Base64Decode("").Dump();
	//Base64Streams();
	string data = GetData();
	byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(data);
	
	using (MemoryStream src = new MemoryStream())
	using (MemoryStream dest = new MemoryStream())
	{
		src.Write(dataBytes, 0, dataBytes.Length);
		src.Position = 0;
		Base64Encode(src, dest);

		using (StreamReader sr = new StreamReader(dest))
		{
			Console.WriteLine(sr.ReadToEnd());
		}
	}
}

public string Base64Encode(string plainText)
{
	return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plainText));
}

public string Base64DecodeToString(string base64EncodedData)
{
	return System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(base64EncodedData));
}

public byte[] Base64Decode(string base64EncodedData)
{
	return System.Convert.FromBase64String(base64EncodedData);
}

public string Base64Encode(byte[] data)
{
	return System.Convert.ToBase64String(data);
}

public void Base64Streams()
{
	string Original = "foo bar, this is an example";
	byte[] ToBase64;
	string Decoded;

	using (MemoryStream ms = new MemoryStream())
	using (CryptoStream cs = new CryptoStream(ms, new ToBase64Transform(),
													CryptoStreamMode.Write))
	using (StreamWriter st = new StreamWriter(cs))
	{
		st.Write(Original);
		st.Flush();

		ToBase64 = ms.ToArray();
	}

	using (MemoryStream ms = new MemoryStream(ToBase64))
	using (CryptoStream cs = new CryptoStream(ms, new FromBase64Transform(),
													CryptoStreamMode.Read))
	using (StreamReader sr = new StreamReader(cs))
		Decoded = sr.ReadToEnd();

	Console.WriteLine(Original);
	Console.WriteLine(Encoding.Default.GetString(ToBase64));
	Console.WriteLine(Decoded);
}

void Base64Encode(Stream src, Stream dest)
{
	using (BinaryReader reader = new BinaryReader(src))
	{
		CryptoStream cs = new CryptoStream(dest, new ToBase64Transform(), CryptoStreamMode.Write);
		BinaryWriter writer = new BinaryWriter(cs);
		
		byte[] buffer = null;
		do
		{
			buffer = reader.ReadBytes(1024 * 100);
			writer.Write(buffer);
		} while(buffer != null && buffer.Length > 0);
		
		writer.Flush();
		dest.Position = 0;
	}
}

void Base64Encode(string data, Stream dest)
{
	using (CryptoStream cs = new CryptoStream(dest, new ToBase64Transform(), CryptoStreamMode.Write))
	using (StreamWriter st = new StreamWriter(cs))
	{
		st.Write(data);
		st.Flush();
	}
	dest.Position = 0;
}

string Base64Decode(Stream data)
{
	string rc = null;
	using (CryptoStream cs = new CryptoStream(data, new FromBase64Transform(), CryptoStreamMode.Read))
	using (StreamReader sr = new StreamReader(cs))
	{
		rc = sr.ReadToEnd();
	}
	return rc;
}

string GetData()
{
	return
@"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque viverra, lectus non tincidunt lacinia, lorem urna volutpat dui, vitae ultricies nibh lectus dignissim erat. Donec porta eros a laoreet pellentesque. Praesent et velit ipsum. Nunc at consectetur nisl. Proin a nulla eget nunc mollis viverra. Sed a imperdiet magna. Mauris venenatis sed justo et aliquam. Nunc interdum elit lectus, eu fringilla lectus egestas ac. Nulla facilisi. Etiam dignissim finibus risus. Vivamus fermentum dolor dui, at luctus urna aliquam a.

Duis libero nibh, venenatis in ante ac, gravida aliquet tellus. Aenean hendrerit vel justo ac lobortis. Sed sed metus scelerisque eros lobortis posuere ac vel nunc. Vestibulum pulvinar nunc a lectus facilisis, non sagittis lorem placerat. Fusce sed erat quam. Nam non lectus eu velit tristique rutrum faucibus sit amet sem. Etiam massa quam, dignissim vel eros sed, consectetur rhoncus augue. Sed laoreet egestas lacinia. Maecenas quis turpis ut diam convallis interdum vitae ut sem. Suspendisse purus lorem, varius ut ligula eget, pharetra dictum neque. Nulla viverra risus sollicitudin, ultrices odio vel, rutrum velit. Cras posuere dictum metus a imperdiet. Mauris ante metus, efficitur vel massa et, tristique euismod elit.

Maecenas commodo interdum tincidunt. Aliquam in maximus erat, vel egestas mauris. In rutrum semper porttitor. Nulla ac rhoncus nunc, quis accumsan ligula. Pellentesque dapibus malesuada tortor quis fermentum. Donec a risus eu tellus elementum pellentesque. Nunc eleifend in sapien non pretium. Duis id velit consequat, ornare ipsum eu, placerat purus. Donec bibendum rutrum commodo. Integer et nulla ac dolor feugiat tincidunt in eu enim. Nullam tellus lectus, malesuada ac facilisis ac, molestie sit amet metus.

Proin quam turpis, fermentum eget laoreet nec, semper vitae metus. Cras ultrices fermentum tortor in pellentesque. Morbi maximus sapien orci, vel accumsan mi molestie at. Mauris porta mi sit amet justo tincidunt, ac feugiat dui gravida. Nullam luctus posuere metus, tempus imperdiet dolor viverra ut. Cras sit amet fermentum ipsum, accumsan viverra nisl. Pellentesque nisi dolor, semper ac felis vel, iaculis bibendum dui. Interdum et malesuada fames ac ante ipsum primis in faucibus. Fusce varius molestie diam vel accumsan. Nam quis sem elementum, egestas augue id, lobortis sem. Duis sit amet tempus lorem.

Fusce rhoncus magna nec urna dictum, fermentum mollis eros malesuada. Duis quis tortor vitae turpis blandit scelerisque. Ut malesuada, purus et tempus volutpat, est libero suscipit sapien, ut fermentum augue leo et massa. Quisque et egestas est. Curabitur nisl risus, ornare ac lectus sit amet, bibendum feugiat nibh. Mauris non convallis velit, in posuere libero. Etiam blandit malesuada urna nec dapibus. Nullam placerat lorem nibh, a semper est varius sed. Ut ut diam porttitor, consectetur turpis eu, egestas purus. Quisque in finibus tortor. Quisque ullamcorper at libero sed varius.

Pellentesque tempor, nisi in elementum sollicitudin, dolor augue fringilla mi, at vulputate dolor dui id tellus. Nullam id lacus bibendum, volutpat justo in, eleifend arcu. Fusce cursus dui ac nisl lobortis euismod. Duis ut mauris neque. Sed commodo mi vel dolor viverra, vitae accumsan ante dignissim. Orci varius natoque penatibus et magnis dis parturient.";
}