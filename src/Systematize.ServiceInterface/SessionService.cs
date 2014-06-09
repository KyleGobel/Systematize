using System;
using System.Linq;
using System.Net;
using Systematize.ServiceModel;
using Systematize.ServiceModel.Types;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Systematize.ServiceInterface
{
    public class SessionService : IService
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public SessionService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public object Get(Sessions request)
        {
            using (var db = _connectionFactory.Open())
            {
                var sessions = db.Select<Session>().Where(x => x.JournalId == request.JournalId);
                return sessions;
            }
        }

        public object Get(GetSession request)
        {
            using (var db = _connectionFactory.Open())
            {
                var session =
                    db.Select<Session>(x => x.JournalId == request.JournalId && x.Id == request.Id).SingleOrDefault();

                if (session == null)
                    return new HttpResult() {StatusCode = HttpStatusCode.NotFound};

                return session;
            }
        }

        public object Post(CreateSession message)
        {
            using (var db = _connectionFactory.Open())
            {
                var session = new Session
                {
                    JournalId = message.JournalId,
                    Name = message.Name,
                    Description = message.Description,
                    CreatedAt = DateTime.Now
                };

                try
                {
                    long sessionId = db.Insert(session, true);
                    return new GeneralizedResponse {Message = "New Session Created! Id: " + sessionId};
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
        }
    }
}