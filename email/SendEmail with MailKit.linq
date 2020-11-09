<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <NuGetReference>MailKit</NuGetReference>
  <Namespace>MailKit</Namespace>
  <Namespace>MailKit.Net.Smtp</Namespace>
  <Namespace>MimeKit</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>MimeKit.Text</Namespace>
</Query>

void Main()
{
	string to = Prompt.ShowDialog("To:", "Send To");
	string from = Prompt.ShowDialog("From:", "Send From");
	string password = Prompt.ShowDialog("Password:", "Password");
	string smtpserver = Prompt.ShowDialog("SMTP Server:", "SMTP Server");
	string smtpport = Prompt.ShowDialog("SMTP Port:", "SMTP Port");

	var message = new MimeMessage();
	message.From.Add(new MailboxAddress(from));
	message.To.Add(new MailboxAddress(to));
	message.Subject = "test message";

	message.Body = new TextPart(TextFormat.Html)
	{
		Text = $"<span>This is for testing SMTP mail from {smtpserver} SMTP</span>",
	};

	using (var client = new SmtpClient())
	{
		client.Connect(smtpserver, int.Parse(smtpport), true);
		client.Authenticate("apikey", password);

		client.Send(message);
		client.Disconnect(true);
	}
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
		Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
		TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
		Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70 };
		confirmation.Click += (sender, e) => { prompt.Close(); };
		prompt.Controls.Add(textBox);
		prompt.Controls.Add(confirmation);
		prompt.Controls.Add(textLabel);
		prompt.AcceptButton = confirmation;
		prompt.ShowDialog();
		return textBox.Text;
	}
}
