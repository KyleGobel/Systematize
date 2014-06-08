using ServiceStack;

namespace Systematize.ServiceModel
{
    [Route("/journals", "GET")]
    public class Journals
    {    
    }

    [Route("/journals", "POST")]
    public class CreateJournal
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    [Route("/journals/{Id}", "GET")]
    public class GetJournal
    {
        public long Id { get; set; }
    }

    [Route("/journals/{Id}", "PUT")]
    public class UpdateJournal
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    [Route("/journals/{Id}", "DELETE")]
    public class DeleteJournal
    {
        public long Id { get; set; }
    }

    public class JournalResponse
    {
        public long TimeTakenMs { get; set; }
    }
}