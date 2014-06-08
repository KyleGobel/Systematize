using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Systematize.ServiceModel.Types;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.Logging;
using Systematize.ServiceModel;
using ServiceStack.OrmLite;

namespace Systematize.ServiceInterface
{
    public class JournalService : Service
    {
        private ILog Log;
        private readonly IDbConnectionFactory _connectionFactory;

        public JournalService(IDbConnectionFactory connectionFactory)
        {
            Log = LogManager.GetLogger(typeof (JournalService));
            _connectionFactory = connectionFactory;
        }

        public object Get(Journals request)
        {
            using (var db = _connectionFactory.Open())
            {
                var journals = db.Select<Journal>();
                return journals;
            }
        }

        public object Get(GetJournal message)
        {
            using (var db = _connectionFactory.Open())
            {
                var journal = db.SelectByIds<Journal>(new long[] {message.Id}).FirstOrDefault();
                if (journal == null)
                    return new HttpResult() {StatusCode = HttpStatusCode.NotFound};

                return journal;
            }
        }

        public object Post(CreateJournal message)
        {
            var sw = Stopwatch.StartNew();
            using (var db = _connectionFactory.Open())
            {
                var journal = new Journal
                {
                    Name = message.Name,
                    Description = message.Description,
                    CreatedAt = DateTime.Now
                };

                db.Insert(journal);
            }
            sw.Stop();
            return new JournalResponse {TimeTakenMs = sw.ElapsedMilliseconds};
        }

        public object Put(UpdateJournal message)
        {
            var sw = Stopwatch.StartNew();
            using (var db = _connectionFactory.Open())
            {
                var journal = db.SelectByIds<Journal>(new long[] {message.Id}).FirstOrDefault();
                if (journal == null)
                    return new HttpResult() {StatusCode = HttpStatusCode.NotFound};

                try
                {
                    journal.Name = message.Name;
                    journal.Description = message.Description;
                    db.Update(journal);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            sw.Stop();
            return new JournalResponse {TimeTakenMs = sw.ElapsedMilliseconds};
        }

        public object Delete(DeleteJournal message)
        {
            var sw = Stopwatch.StartNew();
            using (var db = _connectionFactory.Open())
            {
                var journal = db.SelectByIds<Journal>(new long[] {message.Id});
                if (journal == null)
                    return new HttpResult() {StatusCode = HttpStatusCode.NotFound};

                try
                {
                    db.Delete(journal);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            sw.Stop();
            return new JournalResponse {TimeTakenMs = sw.ElapsedMilliseconds};
        }
    }
}
