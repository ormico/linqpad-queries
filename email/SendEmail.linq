<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Namespace>System.Net.Mail</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

void Main()
{
	string to = Prompt.ShowDialog("To:", "Send To");
	string from = Prompt.ShowDialog("From:", "Send From");
	string replyto = Prompt.ShowDialog("Reploy To:", "Blank to not use ReplyTo");
	string username = Prompt.ShowDialog("Username:", "SendGrid");
	string password = Prompt.ShowDialog("Password:", "SendGrid");
	string smtpserver = Prompt.ShowDialog("SMTP Server:", "Blank for Office365 Default");
	string smtpport = Prompt.ShowDialog("SMTP Port:", "Blank for Office365 Default");

	MailMessage mail = new MailMessage();	
	// you can send from yourself, but you cannot send from someone else
	// you can use the format "name <email@address.com>"
	mail.From = new MailAddress(from);

	if(!string.IsNullOrWhiteSpace(replyto))
	{
		// you can use the replytolist but it doesn't change the name displayed in the From.
		mail.ReplyToList.Add(replyto);
	}
	
	mail.To.Add(to);
	mail.Subject = "Test Mail";
	mail.Body = string.Format("This is for testing SMTP mail from {0} SMTP", smtpserver);
	
	SmtpClient SmtpServer;
	
	if(!string.IsNullOrWhiteSpace(smtpserver))
	{
		SmtpServer = new SmtpClient(smtpserver);
	}
	else
	{
		SmtpServer = new SmtpClient("smtp.office365.com");
	}
	
	int port;
	if(int.TryParse(smtpport, out port))
	{
		SmtpServer.Port = port;
	}
	else
	{
		SmtpServer.Port = 587;
	}
	
	SmtpServer.Credentials = new System.Net.NetworkCredential(username, password);
	SmtpServer.EnableSsl = true;
	SmtpServer.Send(mail);
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
