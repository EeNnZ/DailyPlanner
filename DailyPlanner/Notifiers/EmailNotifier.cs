using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using PlannerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPlanner.Notifiers
{
    public class EmailNotifier : INotifier
    {
        #region props
        public readonly IConfigurationRoot Configuration;
        public string Addressee { get; }
        public string SmtpEmail { get; private set; }
        private string SmtpPassword { get; set; }
        public string SmtpServer { get; private set; }
        public int SmtpPort { get; private set; }
        public bool SmtpUseSsl { get; private set; }
        #endregion

        public EmailNotifier(IConfigurationRoot configuration)
        {
            Configuration = configuration;
            Addressee = GetAddressee();
            SmtpServer = GetSmtpServer();
            SmtpEmail = GetSmtpEmail();
            SmtpPassword = GetSmtpPassword();
            SmtpPort = GetSmtpPort();
            SmtpUseSsl = GetUseSsl();
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

        private string GetAddressee()
        {
            string? addressee = Configuration.GetSection("EmailSettings")["Addressee"];
            if (string.IsNullOrEmpty(addressee))
            {
                throw new NullReferenceException("Cannot obtain addressee email from appsettings.json");
            }
            return addressee;
        }
        #region Obtain smtp settings
        private string GetSmtpServer()
        {
            string? smtp = Configuration.GetSection("EmailSettings")["SmtpServer"];
            if (string.IsNullOrEmpty(smtp))
            {
                throw new NullReferenceException("Cannot obtain smtp server address from appsettings.json");
            }
            return smtp;
        }
        private bool GetUseSsl()
        {
            string? useSslStr = Configuration.GetSection("EmailSettings")["SmtpUseSsl"];
            if (string.IsNullOrEmpty(useSslStr))
            {
                throw new NullReferenceException("Cannot obtain UseSsl from appsettings.json");
            }

            try
            {
                return bool.Parse(useSslStr);
            }
            catch (ArgumentNullException) { throw; }
            catch (FormatException) { throw; }
        }
        private int GetSmtpPort()
        {
            string? port = Configuration.GetSection("EmailSettings")["SmtpPort"];
            if (string.IsNullOrEmpty(port))
            {
                throw new NullReferenceException("Cannot obtain smtp port from appsettings.json");
            }

            try
            {
                return int.Parse(port);
            }
            catch (ArgumentNullException) { throw; }
            catch (FormatException) { throw; }
            catch (OverflowException) { throw; }
        }
        private string GetSmtpPassword()
        {
            string? password = Configuration.GetSection("EmailSettings")["SmtpPassword"];
            if (string.IsNullOrEmpty(password))
            {
                throw new NullReferenceException("Cannot obtain smtp password from appsettings.json");
            }
            return password;
        }
        private string GetSmtpEmail()
        {
            string? email = Configuration.GetSection("EmailSettings")["SmtpEmail"];
            if (string.IsNullOrEmpty(email))
            {
                throw new NullReferenceException("Cannot obtain smtp email from appsettings.json");
            }
            return email;
        }
        #endregion
    }
}
