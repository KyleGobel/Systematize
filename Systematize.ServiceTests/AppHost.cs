using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Systematize.ServiceInterface;
using Systematize.ServiceModel.Configuration;
using Systematize.ServiceModel.Types;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.OrmLite;
using Task = Systematize.ServiceModel.Types.Task;

namespace Systematize.ServiceTests
{
    public class AppHost : AppHostHttpListenerBase
    {
        public AppHost() : base("Test Systematize", typeof(AppHost).Assembly) { }

        public override void Configure(Funq.Container container)
        {
            container.Register<IDbConnectionFactory>(
                    c =>
                        new OrmLiteConnectionFactory("~/App_Data/db.sqlite".MapHostAbsolutePath(),
                            SqliteDialect.Provider)
                        {
                            ConnectionFilter = x => new ProfiledDbConnection(x, Profiler.Current)
                        });

            container.RegisterAutoWired<JournalService>();
            
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

        public static void Main(string[] args)
        {
            var appHost = new AppHost();
            appHost.Init();
            appHost.Start("http://*:8888/");
            Console.WriteLine("Listening on http://localhost:8888/ ...");
            System.Console.ReadLine();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
        }
    }

    
}
