using DailyPlanner.Events;

namespace DailyPlanner.Notifiers
{
    public interface INotifier
    {
        void Notify(PlannedEvent evnt);
    }
}
