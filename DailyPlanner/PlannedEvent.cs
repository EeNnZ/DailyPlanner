using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PlannerCore
{
    public class PlannedEvent
    {
        [Key]
        public int EventId { get; set; }
        public string Name { get; set; }
        public string? Body { get; set; }
        public DateTime EventStartDateTime { get; set; }
        public DateTime NotificationDateTime { get; set; } = new();
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }


    }
}