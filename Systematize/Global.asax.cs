using System.Web.UI;
using Systematize.ServiceModel.Types;
using Funq;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using ServiceStack.Data;
using ServiceStack.Logging;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using ServiceStack.Logging.Log4Net;
using Systematize.ServiceInterface;

namespace Systematize
{
    public class Global : System.Web.HttpApplication
    {

        public class SystematizeAppHost : AppHostBase
        {
            /// <summary>
            /// Initializes a new instance of your ServiceStack application, with the specified name and assembly containing the services.
            /// </summary>
            public SystematizeAppHost() : base("Hello Web Services", typeof(JournalService).Assembly) { }
 
            /// <summary>
            /// Configure the container with the necessary routes for your ServiceStack application.
            /// </summary>
            /// <param name="container">The built-in IoC used with ServiceStack.</param>
            public override void Configure(Container container)
            {
                //Register user-defined REST-ful urls. You can access the service at the url similar to the following.
                //http://localhost/ServiceStack.Hello/servicestack/hello or http://localhost/ServiceStack.Hello/servicestack/hello/John%20Doe
                //You can change /servicestack/ to a custom path in the web.config.
                ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;
                JsConfig.PropertyConvention = PropertyConvention.Lenient;
                LogManager.LogFactory = new Log4NetFactory();
                container.RegisterAutoWired<JournalService>();
                ConfigureAndSeedSqlLiteDataBase(container);

                LogManager.GetLogger(typeof(SystematizeAppHost)).Debug("AppHost has started... (way to go copying kgobel)");
            }

            private void ConfigureAndSeedSqlLiteDataBase(Container container)
            {
                container.Register<IDbConnectionFactory>(
                    c =>
                        new OrmLiteConnectionFactory("~/App_Data/db.sqlite".MapHostAbsolutePath(),
                            SqliteDialect.Provider)
                        {
                            ConnectionFilter = x => new ProfiledDbConnection(x, Profiler.Current)
                        });

                using (var db = container.Resolve<IDbConnectionFactory>().Open())
                {
                    db.DropAndCreateTable<Journal>();
                    db.DropAndCreateTable<Session>();
                    db.DropAndCreateTable<Task>();
                    db.DropAndCreateTable<JournalNote>();
                    db.DropAndCreateTable<SessionNote>();

                    //Seed Lists
                    var journalSeed = new List<Journal>();
                    var journalNoteSeed = new List<JournalNote>();
                    var sessionSeed = new List<Session>();
                    var sessionNoteSeed = new List<SessionNote>();
                    var taskSeed = new List<Task>();

                    //All Seeds for a "Deerso" Journal and all enclosed objects.
                    journalSeed.Add(new Journal {Id = 1, Name = "Deerso", Description = "All things Deerso.", CreatedAt = DateTime.Now});
                    journalNoteSeed.Add(new JournalNote{Id = 1, JournalId = 1, Note = "Need to get Deerso.com published and figure out wtf is wrong with it.", CreatedAt = DateTime.Now});
                    sessionSeed.Add(
                        new Session
                        {
                            Id = 1, 
                            JournalId = 1, 
                            Name = "6/8/2014 - Deerso.com Debugging", 
                            CreatedAt = DateTime.Now, 
                            Description = "I hope to get deerso.com finished and published in this session."
                        });
                    taskSeed.Add(
                        new Task
                        {
                            Id = 1,
                            SessionId = 1,
                            Description = "Publish Deerso.com",
                            Completed = false,
                            Effort = 1500,
                            CompletedPomodoros = 1,
                            AbandonedPomodoros = 0,
                            InterruptedPomodoros = 0,
                            CreatedAt = DateTime.Now
                        });
                    sessionNoteSeed.Add(new SessionNote {Id = 1, SessionId = 1, Note = "Dont forget to check on staging before publishing.", CreatedAt = DateTime.Now, TaskId = 1 });

                    db.InsertAll(journalSeed);
                    db.InsertAll(journalNoteSeed);
                    db.InsertAll(sessionSeed);
                    db.InsertAll(taskSeed);
                    db.InsertAll(sessionNoteSeed);
                }
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            (new SystematizeAppHost()).Init();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}