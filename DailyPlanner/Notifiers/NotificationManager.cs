using DailyPlanner.Events;
using Microsoft.Extensions.Configuration;

namespace DailyPlanner.Notifiers
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

        public void Notify(PlannedEvent evnt)
        {
            if (UseEmail)
            {
                _emailNotifier.Notify(evnt);
            }
            if (UseWindows)
            {
                _winNotifier.Notify(evnt);
            }
        }
    }
}
