using DailyPlanner.Notifiers;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Crypto.Operators;
using PlannerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPlanner
{
    public class NotificationManager : INotifier
    {
        public readonly IConfigurationRoot Configuration;
        private readonly EmailNotifier _emailNotifier;
        private readonly WinNotifier _winNotifier;

        public bool UseEmail
        {
            get
            {
                string? useEmail = Configuration.GetSection("MainSettings")["RecieveEmailNotifications"];
                return bool.TryParse(useEmail, out bool use) && use;
            }
        }
        public bool UseWindows
        {
            get
            {
                string? useWindows = Configuration.GetSection("MainSettings")["RecieveWindowsNotifications"];
                return bool.TryParse(useWindows, out bool use) && use;
            }
        }
        public NotificationManager(IConfigurationRoot configuration) 
        {
            Configuration = configuration;
            _emailNotifier = new EmailNotifier(Configuration);
            _winNotifier = new WinNotifier(Configuration);
        }

        public async Task Notify(PlannedEvent evnt)
        {
            if (UseEmail)
            {
                await _emailNotifier.Notify(evnt);
            }
            if (UseWindows)
            {
                await _winNotifier.Notify(evnt);
            }
        }
    }
}
