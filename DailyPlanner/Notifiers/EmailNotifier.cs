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
        private readonly string _addressee;

        #region props
        public readonly IConfigurationRoot Configuration;
        public string SmtpEmail { get; private set; }
        private string SmtpPassword { get; set; }
        public string SmtpServer { get; private set; }
        public int SmtpPort { get; private set; }
        public bool SmtpUseSsl { get; private set; }
        #endregion

        public EmailNotifier(string addressee, IConfigurationRoot configuration)
        {
            _addressee = addressee;
            Configuration = configuration;
            SmtpServer = GetSmtpServer();
            SmtpEmail = GetSmtpEmail();
            SmtpPassword = GetSmtpPassword();
            SmtpPort = GetSmtpPort();
            SmtpUseSsl = GetUseSsl();
        }

        public async Task Notify(PlannedEvent evnt)
        {
            string? fromName = System.Reflection.Assembly.GetEntryAssembly()?.GetName()?.Name;
            if (string.IsNullOrEmpty(fromName))
            {
                throw new Exception("cannot get assembly name");
            }
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromName, SmtpEmail));
            message.To.Add(new MailboxAddress("client", _addressee));
            message.Subject = $"Your event \"{evnt.Name}\" will start at {evnt.EventStartDateTime}";
            message.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = evnt.Body };

            using var client = new SmtpClient();
            await client.ConnectAsync(SmtpServer, SmtpPort, SmtpUseSsl);
            await client.AuthenticateAsync(SmtpEmail, SmtpPassword);
            await client.SendAsync(message);

            await client.DisconnectAsync(true);
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
