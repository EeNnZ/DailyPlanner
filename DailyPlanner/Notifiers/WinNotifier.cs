﻿using DailyPlanner.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Toolkit.Uwp.Notifications;

namespace DailyPlanner.Notifiers
{
    public class WinNotifier : INotifier
    {
        public readonly IConfigurationRoot Configuration;
        public WinNotifier(IConfigurationRoot configuration)
        {
            Configuration = configuration;
        }
        public void Notify(PlannedEvent evnt)
        {
            string toastTitle = $"{evnt.Name} will start at {evnt.EventStartDateTime}";
            string toastBody = evnt.Body ?? string.Empty;

            var toast = new ToastContentBuilder()
                .AddAppLogoOverride(new Uri(Path.Combine(Directory.GetCurrentDirectory(), "ico.ico")), ToastGenericAppLogoCrop.Circle)
                .AddArgument("eventName", evnt.Name)
                .AddArgument("eventId", evnt.EventId)
                .AddText(toastTitle)
                .AddText(toastBody)
                .AddButton(new ToastButton().SetContent("Delete event").AddArgument("action", "delete").SetBackgroundActivation())
                .AddButton(new ToastButton().SetContent("Mark as done").AddArgument("action", "mark")).SetBackgroundActivation();
            toast.Show();
        }
    }
}
