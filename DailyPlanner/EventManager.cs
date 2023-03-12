using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
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
        //TODO: Timer
        //TODO: Notifiers
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

        #region CRUD operations
        private bool Delete(int eventId)
        {
            using var db = new MainDbContext();
            var evnt = db.PlannedEvents?.FirstOrDefault(x => x.EventId == eventId);
            if (evnt is not null)
            {
                db.PlannedEvents?.Remove(evnt);
                //ResetIndices(db);
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
            if(db.PlannedEvents?.FirstOrDefault(e => e.Name == newEvent.Name 
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
