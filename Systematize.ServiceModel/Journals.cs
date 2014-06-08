using System.Collections.Generic;
using Systematize.ServiceModel.Types;
using ServiceStack;

namespace Systematize.ServiceModel
{
    [Route("/journals", "GET")]
    public class Journals : IReturn<List<Journal>> 
    {    
    }

    [Route("/journals", "POST")]
    public class CreateJournal : IReturn<JournalResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    [Route("/journals/{Id}", "GET")]
    public class GetJournal : IReturn<Journal>
    {
        public long Id { get; set; }
    }

    [Route("/journals/{Id}", "PUT")]
    public class UpdateJournal : IReturn<JournalResponse>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    [Route("/journals/{Id}", "DELETE")]
    public class DeleteJournal : IReturn<JournalResponse>
    {
        public long Id { get; set; }
    }

    public class JournalResponse
    {
        public long TimeTakenMs { get; set; }
    }
}