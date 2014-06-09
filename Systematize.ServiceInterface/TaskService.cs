using System;
using System.Linq;
using System.Threading.Tasks;
using Systematize.ServiceModel;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Task = Systematize.ServiceModel.Types.Task;
namespace Systematize.ServiceInterface
{
    public class TaskService : IService
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public TaskService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public object Get(Tasks request)
        {
            using (var db = _connectionFactory.Open())
            {
                var tasks = db.Select<Task>().Where(x => x.SessionId == request.SessionId);
                return tasks;
            }
        }

        public object Post(CreateTask message)
        {
            using (var db = _connectionFactory.Open())
            {
                var task = new Task {SessionId = message.SessionId, Description = message.Description};

                try
                {
                    db.Insert(task);
                    return this.Get(new Tasks {SessionId = message.SessionId});
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
        }
    }
}