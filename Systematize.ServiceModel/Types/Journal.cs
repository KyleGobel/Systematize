using System;
using ServiceStack.DataAnnotations;
namespace Systematize.ServiceModel.Types
{
    public class Journal
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}