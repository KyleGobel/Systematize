using System;
using ServiceStack.DataAnnotations;

namespace Systematize.ServiceModel.Types
{
    public class Task
    {
        [AutoIncrement, PrimaryKey]
        public long Id { get; set; }
 
        [ForeignKey(typeof(Session))]
        public long SessionId { get; set; }

        public string Description { get; set; }
        
        public int CompletedPomodoros { get; set; }
        
        public int InterruptedPomodoros { get; set; }
        
        public int AbandonedPomodoros { get; set; }
        
        //Effort is the total time spent on a task in seconds
        public long Effort { get; set; }
        
        public bool Completed { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime CompletedAt { get; set; }
    }
}