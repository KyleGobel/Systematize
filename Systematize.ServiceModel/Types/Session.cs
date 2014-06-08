using System;
using ServiceStack.DataAnnotations;

namespace Systematize.ServiceModel.Types
{
    public class Session
    {
        [AutoIncrement, PrimaryKey]
        public long Id { get; set; }

        [ForeignKey(typeof(Journal))]
        public long JournalId { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}