namespace DailyPlanner.Settings
{
    public class JsonSettings
    {
        public ConnectionStrings ConnectionStrings = new ConnectionStrings();
        public MainSettings MainSettings = new MainSettings();
        public EmailSettings EmailSettings = new EmailSettings();
        public WinSettings WinSettings = new WinSettings();
    }
    public class ConnectionStrings
    {
        public string DefaultConnection = "Data Source=MainDb.db";
    }
    public class MainSettings
    {
        public string RecieveEmailNotifications = "false";
        public string RecieveWindowsNotifications = "true";
    }
    public class EmailSettings
    {
        public string Addressee = "mix.nazarov3000@gmail.com";
        public string SmtpEmail = "postmaster@sandboxe43491c6dc7b4e8dbc2cfd463e46864e.mailgun.org";
        public string SmtpPassword = "d28df2b70a73a60c56895a321c8ef1dc-7764770b-2ffb068c";
        public string SmtpServer = "smtp.mailgun.org";
        public string SmtpPort = "587";
        public string SmtpUseSsl = "false";
    }
    public class WinSettings
    {

    }

}
