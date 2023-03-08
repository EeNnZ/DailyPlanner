using PlannerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPlanner.Notifiers
{
    public interface INotifier
    {
        Task Notify(PlannedEvent evnt);
    }
}
