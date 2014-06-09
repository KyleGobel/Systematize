using System;
using ServiceStack.DataAnnotations;

namespace Systematize.ServiceModel.Types
{
    public class SessionNote
    {
        [AutoIncrement, PrimaryKey]
        public long Id { get; set; }

        [ForeignKey(typeof(Session))]
        public long SessionId { get; set; }
        
        [ForeignKey(typeof(Task))]
        public long? TaskId { get; set; }
        
        public string Note { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}