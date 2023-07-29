using DailyPlanner.Events;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace DailyPlanner.Notifiers
{
    public class EmailNotifier : INotifier
    {
        #region props
        public readonly IConfigurationRoot Configuration;
        public string Addressee => GetJsonSection(nameof(Addressee));
        public string SmtpEmail => GetJsonSection(nameof(SmtpEmail));
        private string SmtpPassword => GetJsonSection(nameof(SmtpPassword));
        public string SmtpServer => GetJsonSection(nameof(SmtpServer));
        public int SmtpPort
        {
            get
            {
                string? portStr = GetJsonSection(nameof(SmtpPort));
                try
                {
                    return int.Parse(portStr);
                }
                catch (ArgumentNullException) { throw; }
                catch (FormatException) { throw; }
                catch (OverflowException) { throw; }
            }
        }
        public bool SmtpUseSsl
        {
            get
            {
                string? useSslStr = GetJsonSection(nameof(SmtpUseSsl));
                try
                {
                    return bool.Parse(useSslStr);
                }
                catch (ArgumentNullException) { throw; }
                catch (FormatException) { throw; }
            }
        }
        #endregion

        public EmailNotifier(IConfigurationRoot configuration)
        {
            Configuration = configuration;
        }

        public void Notify(PlannedEvent evnt)
        {
            string? fromName = System.Reflection.Assembly.GetEntryAssembly()?.GetName()?.Name;
            if (string.IsNullOrEmpty(fromName))
            {
                throw new Exception("cannot get assembly name");
            }
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromName, SmtpEmail));
            message.To.Add(new MailboxAddress("client", Addressee));
            message.Subject = $"Your event \"{evnt.Name}\" will start at {evnt.EventStartDateTime}";
            message.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = evnt.Body };

            using var client = new SmtpClient();
            client.ConnectAsync(SmtpServer, SmtpPort, SmtpUseSsl);
            client.AuthenticateAsync(SmtpEmail, SmtpPassword);
            client.SendAsync(message);

            client.DisconnectAsync(true);
        }

        #region get
        private string GetJsonSection(string sectionName)
        {
            string? value = Configuration.GetSection("EmailSettings")[sectionName];
            if (string.IsNullOrEmpty(value))
            {
                throw new NullReferenceException($"Cannot obtain section {sectionName}. Check appsetting.json");
            }
            return value;
        }
        #endregion
    }
}
