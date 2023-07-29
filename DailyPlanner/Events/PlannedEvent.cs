using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DailyPlanner.Events
{
    [Index(nameof(Name), IsUnique = true)]
    public class PlannedEvent
    {
        public PlannedEvent(string name, DateTime eventStartDateTime, DateTime notificationDateTime, string? body, bool isDone = false)
        {
            Name = name;
            Body = body;
            EventStartDateTime = eventStartDateTime;
            NotificationDateTime = notificationDateTime;
            IsDone = isDone;
        }

        [Key]
        public int EventId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Body { get; set; }
        [Required]
        public bool IsDone { get; set; }
        [Required]
        public DateTime EventStartDateTime { get; set; }
        [Required]
        public DateTime NotificationDateTime { get; set; }
        [Required]
        public DateTime CreatedDateTime { get; set; }
        [Required]
        public DateTime ModifiedDateTime { get; set; }
    }
}