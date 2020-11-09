<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.dll</Reference>
  <Namespace>System.Web.UI</Namespace>
</Query>

/*
.net framework
won't work on .net core
*/
void Main()
{
	Console.WriteLine(ToHtml(false));
	Console.WriteLine();
	Console.WriteLine(ToHtml(true));
}

string ToHtml(bool BodyOnly)
{
  string rc = string.Empty;

  using (MemoryStream ms = new MemoryStream())
  using (StreamWriter txtwriter = new StreamWriter(ms))
  using (HtmlTextWriter writer = new HtmlTextWriter(txtwriter))
  {
      ToHtml(writer, BodyOnly);

      writer.Flush();
      ms.Position = 0;
      using (StreamReader reader = new StreamReader(ms))
      {
          rc = reader.ReadToEnd();
      }
  }

  return rc;
}

void ToHtml(HtmlTextWriter Writer, bool BodyOnly)
{
  if (!BodyOnly)
  {
      //TODO: how to write doctype?
	  Writer.WriteLine("<!DOCTYPE html>");
      Writer.RenderBeginTag(HtmlTextWriterTag.Html);
      Writer.RenderBeginTag(HtmlTextWriterTag.Head);
      Writer.RenderBeginTag(HtmlTextWriterTag.Title);
      Writer.WriteEncodedText("Failure Report");
      Writer.RenderEndTag(); //</title>
      Writer.RenderEndTag(); //</head>
      Writer.RenderBeginTag(HtmlTextWriterTag.Body);
  }

  // write body here
	Writer.RenderBeginTag(HtmlTextWriterTag.Div);
	Writer.WriteEncodedText("Hello World!");
	Writer.RenderEndTag();

  // end body

  if (!BodyOnly)
  {
      Writer.RenderEndTag(); //</body>
      Writer.RenderEndTag(); //</html>
  }
}