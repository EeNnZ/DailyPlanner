using Microsoft.Extensions.Configuration;
using PlannerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPlanner.Notifiers
{
    public class WinNotifier : INotifier
    {
        private readonly NotifyIcon _notifyIcon;
        public readonly IConfigurationRoot Configuration;
        public WinNotifier(NotifyIcon notifyIcon, IConfigurationRoot configuration)
        {
            _notifyIcon = notifyIcon;
            Configuration = configuration;
        }
        //TODO: Use UWP notifications package
        public async Task Notify(PlannedEvent evnt)
        {
            string tipTitle = $"{evnt.Name} will start at {evnt.EventStartDateTime}";
            _notifyIcon.ShowBalloonTip(60_000, tipTitle, evnt.Body, ToolTipIcon.Info);
        }
    }
}
