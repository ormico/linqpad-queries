<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <NuGetReference>Sendgrid</NuGetReference>
  <Namespace>SendGrid</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Mail</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>SendGrid.Helpers.Mail</Namespace>
</Query>

void Main()
{
	string to = Prompt.ShowDialog("To:", "Send To");
	string from = Prompt.ShowDialog("From:", "Send From");
	string apiKey = Prompt.ShowDialog("API Key:", "SendGrid");
	
	string text = "Hello, Test";
	string html = "<table style=\"border: solid 1px #000; background-color: #666; font-family: verdana, tahoma, sans-serif; color: #fff;\"><tr><td><h2>Hello,</h2><p>Test</p></td></tr></table>";
	
	// Create the email object first, then add the properties.
	SendGridMessage myMessage = new SendGridMessage();
	myMessage.AddTo(to);
	myMessage.From = new SendGrid.Helpers.Mail.EmailAddress(from, "Test");
	myMessage.Subject = "Testing";
	myMessage.PlainTextContent = text;
	myMessage.HtmlContent = html;

	var client = new SendGridClient(apiKey);
	var response = client.SendEmailAsync(myMessage).Result;

	Console.WriteLine("Message Sent.");
}

//using this to prompt for input b/c I don't want to store
//email addresses and credentials in source control
//http://stackoverflow.com/questions/5427020/prompt-dialog-in-windows-forms
public static class Prompt
{
    public static string ShowDialog(string text, string caption)
    {
        Form prompt = new Form();
        prompt.Width = 500;
        prompt.Height = 150;
        prompt.Text = caption;
        prompt.StartPosition = FormStartPosition.CenterScreen;
        Label textLabel = new Label() { Left = 50, Top=20, Text=text };
        TextBox textBox = new TextBox() { Left = 50, Top=50, Width=400 };
        Button confirmation = new Button() { Text = "Ok", Left=350, Width=100, Top=70 };
        confirmation.Click += (sender, e) => { prompt.Close(); };
        prompt.Controls.Add(textBox);
        prompt.Controls.Add(confirmation);
        prompt.Controls.Add(textLabel);
        prompt.AcceptButton = confirmation;
        prompt.ShowDialog();
        return textBox.Text;
    }
}