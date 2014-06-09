using System.Collections.Generic;
using Systematize.ServiceModel.Types;
using ServiceStack;
namespace Systematize.ServiceModel
{
    [Route("/sessions/{JournalId}", "GET")]
    public class Sessions : IReturn<SessionList>
    {
        public long JournalId { get; set; }
    }

    [Route("/sessions/{JournalId}/{Id}", "GET")]
    public class GetSession
    {
        public long JournalId { get; set; }
        public long Id { get; set; }
    }

    [Route("/sessions", "POST")]
    public class CreateSession
    {
        public long JournalId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    [Route("/sessions/{Id}", "PUT")]
    public class UpdateSession
    {
        public long Id { get; set; }
        public long JournalId { get; set; }
        public long Name { get; set; }
        public long Description { get; set; }
    }

    [Route("/sessions/{Id}", "DELETE")]
    public class DeleteSession
    {
        public long Id { get; set; }
    }

    public class SessionList
    {
        public List<Session> Sessions = new List<Session>(); 
    }

    public class GeneralizedResponse
    {
        public string Message { get; set; }
        public long TimeTakenMs { get; set; }
    }
}