using System;
using ServiceStack.DataAnnotations;

namespace Systematize.ServiceModel.Types
{
    public class JournalNote
    {
        [AutoIncrement, PrimaryKey]
        public long Id { get; set; }

        [ForeignKey(typeof(Journal))]
        public long JournalId { get; set; }

        public string Note { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}