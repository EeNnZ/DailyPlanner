using PlannerCore;

namespace DailyPlanner.Notifiers
{
    public interface INotifier
    {
        void Notify(PlannedEvent evnt);
    }
}
