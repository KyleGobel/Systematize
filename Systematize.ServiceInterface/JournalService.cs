using System.Diagnostics;
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
            var sw = Stopwatch.StartNew();
            using (var db = _connectionFactory.Open())
            {
                var journals = db.Select<Journal>();
                return journals;
            }
        }
    }
}
