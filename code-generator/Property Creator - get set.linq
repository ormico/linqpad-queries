<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

void Main()
{
	string propListTxt = Prompt.ShowDialog("Property Names", "Property Names");
	string[] p = propListTxt.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
	
	for(int i=0; i<p.Length;i++)
	{
		string prop = p[i];
		if(!string.IsNullOrWhiteSpace(prop))
		{
			Console.WriteLine("public string {0} {{ get; set; }}", prop);
			Console.WriteLine();
		}
	}
}

// Define other methods and classes here
//using this to prompt for input b/c I don't want to store
//email addresses and credentials in source control
//http://stackoverflow.com/questions/5427020/prompt-dialog-in-windows-forms
public static class Prompt
{
    public static string ShowDialog(string text, string caption)
    {
        Form prompt = new Form();
        prompt.Width = 500;
        prompt.Height = 450;
        prompt.Text = caption;
        prompt.StartPosition = FormStartPosition.CenterScreen;
        Label textLabel = new Label() { Left = 50, Top=20, Text=text };
        TextBox textBox = new TextBox() { Left = 50, Top=50, Width=400, Height = 300, Multiline = true, AcceptsReturn = true };
        Button confirmation = new Button() { Text = "Ok", Left=350, Width=100, Top=370 };
        confirmation.Click += (sender, e) => { prompt.Close(); };
        prompt.Controls.Add(textBox);
        prompt.Controls.Add(confirmation);
        prompt.Controls.Add(textLabel);
        prompt.AcceptButton = confirmation;
        prompt.ShowDialog();
        return textBox.Text;
    }
}