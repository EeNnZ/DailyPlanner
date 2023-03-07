using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PlannerCore
{
    public class PlannedEvent
    {
        [Key]
        public int EventId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Body { get; set; }
        [Required]
        public DateTime EventStartDateTime { get; set; }
        [Required]
        public DateTime NotificationDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }


    }
}