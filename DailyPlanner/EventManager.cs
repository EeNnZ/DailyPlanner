using DailyPlanner.Notifiers;
using Microsoft.Extensions.Configuration;
using PlannerCore;
using System.Timers;

namespace DailyPlanner
{
    public class EventManager
    {
        public readonly IConfigurationRoot Configuration;
        private readonly INotifier _notifier;
        private List<PlannedEvent> _localEventsCollection;
        private readonly System.Timers.Timer _timer;

        public EventManager(IConfigurationRoot configuration)
        {
            Configuration = configuration;
            _notifier = new NotificationManager(Configuration);
            using var context = new MainDbContext();
            _localEventsCollection = context.PlannedEvents?.ToList() ?? new List<PlannedEvent>();
            _timer = new System.Timers.Timer(5000);
            _timer.Elapsed += CheckForUpcomingEventsCallback;
            _timer.Start();
        }

        private void CheckForUpcomingEventsCallback(object? sender, ElapsedEventArgs e)
        {
            if (_localEventsCollection == null || _localEventsCollection.Count == 0) return;

            string dtformat = "MM/dd/yyyy h:mm tt";
            string now = DateTime.Now.ToString(dtformat);
            foreach (var evnt in _localEventsCollection)
            {
                if (now == evnt.NotificationDateTime.ToString(dtformat) && !evnt.IsDone)
                {
                    _notifier.Notify(evnt);
                }
            }
        }

        #region CRUD event 
        public class RecordChangedEventArgs : EventArgs
        {
            public enum RecordState { Deleted, Updated, Created }
            public readonly RecordState _state;
            public RecordChangedEventArgs(RecordState state) => _state = state;
        }
        public event EventHandler? RecordChanged;
        protected virtual void OnRecordChanged(RecordChangedEventArgs e)
        {
            var entries = Read();
            if (entries is not null) _localEventsCollection = entries;
            RecordChanged?.Invoke(this, e);
        }
        #endregion

        #region CRUD operations
        private bool Delete(int eventId)
        {
            using var db = new MainDbContext();
            var evnt = db.PlannedEvents?.FirstOrDefault(x => x.EventId == eventId);
            if (evnt is not null)
            {
                db.PlannedEvents?.Remove(evnt);
                db.SaveChanges();
                OnRecordChanged(new RecordChangedEventArgs(RecordChangedEventArgs.RecordState.Deleted));
                return true;
            }
            else return false;
        }
        public bool Delete(string evntName)
        {
            using var db = new MainDbContext();
            var evnt = db.PlannedEvents?.FirstOrDefault(x => x.Name == evntName);
            if (evnt is null) return false;
            return Delete(evnt.EventId);
        }
        private void ResetSqliteSequence()
        {
            //TODO: Reset sqlite sequence???
        }
        private void Update(int eventId, PlannedEvent updatedEvent)
        {
            using var db = new MainDbContext();
            var evnt = db.PlannedEvents?.Single(e => e.EventId == eventId);
            if (evnt is not null)
            {
                evnt.Name = updatedEvent.Name;
                evnt.Body = updatedEvent.Body;
                evnt.EventStartDateTime = updatedEvent.EventStartDateTime;
                evnt.NotificationDateTime = updatedEvent.NotificationDateTime;
                evnt.IsDone = updatedEvent.IsDone;
                db.PlannedEvents?.Update(evnt);
                db.SaveChanges();
                OnRecordChanged(new RecordChangedEventArgs(RecordChangedEventArgs.RecordState.Updated));
            }
        }
        public void Update(string evntName, PlannedEvent updatedEvent)
        {
            using var db = new MainDbContext();
            var evnt = db.PlannedEvents?.Single(e => e.Name == evntName);
            if (evnt is not null)
            {
                Update(evnt.EventId, updatedEvent);
                OnRecordChanged(new RecordChangedEventArgs(RecordChangedEventArgs.RecordState.Updated));
            }
        }
        public List<PlannedEvent>? Read()
        {
            using var db = new MainDbContext();
            var allEvents = db.PlannedEvents?.ToList();
            return allEvents;
        }
        private void Create(string name, DateTime startDateTime, DateTime notifyDateTime, string? body = "")
        {
            var newEvent = new PlannedEvent(name, startDateTime, notifyDateTime, body);
            using var db = new MainDbContext();
            if (db.PlannedEvents?.FirstOrDefault(e => e.Name == newEvent.Name
            && e.EventStartDateTime == newEvent.EventStartDateTime) is null)
            {
                db.PlannedEvents?.Add(newEvent);
                db.SaveChanges();
                OnRecordChanged(new RecordChangedEventArgs(RecordChangedEventArgs.RecordState.Created));
            }
        }
        public void Create(PlannedEvent newEvent)
        {
            if (newEvent is null) return;
            Create(newEvent.Name, newEvent.EventStartDateTime, newEvent.NotificationDateTime, newEvent.Body);
        }
        #endregion
    }
}
