using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PlannerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DailyPlanner
{
    public class EventManager
    {
        #region CRUD event 
        public class RecordChangedEventArgs : EventArgs
        {
            public enum RecordState { Deleted, Updated, Created}
            public readonly RecordState _state;
            public RecordChangedEventArgs(RecordState state) => _state = state;
        }
        public event EventHandler? RecordChanged;
        protected virtual void OnRecordChanged(RecordChangedEventArgs e)
        {
            RecordChanged?.Invoke(this, e);
        }
        #endregion

        #region CRUD 
        public void Delete(int eventId)
        {
            using var db = new MainDbContext();
            var evnt = db.PlannedEvents?.FirstOrDefault(x => x.EventId == eventId);
            if (evnt is not null)
            {
                db.PlannedEvents?.Remove(evnt);
                db.SaveChanges();
            }
            OnRecordChanged(new RecordChangedEventArgs(RecordChangedEventArgs.RecordState.Deleted));
        }
        public void Update(int eventId, PlannedEvent updatedEvent)
        {
            using var db = new MainDbContext();
            var evnt = db.PlannedEvents?.Single(e => e.EventId == eventId);
            if (evnt is not null)
            {
                evnt.Name = updatedEvent.Name;
                evnt.Body = updatedEvent.Body;
                evnt.EventStartDateTime = updatedEvent.EventStartDateTime;
                evnt.NotificationDateTime = updatedEvent.NotificationDateTime;
                db.PlannedEvents?.Update(evnt);
                db.SaveChanges();
            }
            OnRecordChanged(new RecordChangedEventArgs(RecordChangedEventArgs.RecordState.Updated));
        }
        public List<PlannedEvent>? Read()
        {
            using var db = new MainDbContext();
            var allEvents = db.PlannedEvents?.ToList();
            return allEvents;
        }
        public void Create(string name, DateTime startDateTime, DateTime notifyDateTime, string? body = "")
        {
            var newEvent = new PlannedEvent()
            {
                Name = name,
                EventStartDateTime = startDateTime,
                NotificationDateTime = notifyDateTime,
                Body = body
            };
            using var db = new MainDbContext();
            if(db.PlannedEvents?.Find(name, startDateTime) is null)
            {
                db.PlannedEvents?.Add(newEvent);
                db.SaveChanges();
                OnRecordChanged(new RecordChangedEventArgs(RecordChangedEventArgs.RecordState.Created));
            }
        }
        #endregion

    }
}
