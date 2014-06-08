using System.Configuration;
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
using Systematize.ServiceModel.Configuration;

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
                    db.InsertAll(SeedData.JournalSeed);
                    db.InsertAll(SeedData.JournalNoteSeed);
                    db.InsertAll(SeedData.SessionSeed);
                    db.InsertAll(SeedData.TaskSeed);
                    db.InsertAll(SeedData.SessionNoteSeed);
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