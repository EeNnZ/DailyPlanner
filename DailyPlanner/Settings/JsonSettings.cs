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
        public string RecieveEmailNotifications = string.Empty;
        public string RecieveWindowsNotifications = string.Empty;
    }
    public class EmailSettings
    {
        public string Addressee = "";
        public string SmtpEmail = "";
        public string SmtpPassword = "";
        public string SmtpServer = "";
        public string SmtpPort = "";
        public string SmtpUseSsl = "";
    }
    public class WinSettings
    {

    }

}
