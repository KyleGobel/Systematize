﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Systematize.ServiceInterface;
using Systematize.ServiceModel.Configuration;
using Systematize.ServiceModel.Types;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.OrmLite;
using ServiceStack.Testing;
using Task = Systematize.ServiceModel.Types.Task;

namespace Systematize.ServiceTests
{
    public class AppHost : BasicAppHost
    {
        public AppHost() : base(typeof(JournalService).Assembly) { }

        public override ServiceStackHost Init()
        {
            
            return base.Init();
        }

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
            container.RegisterAutoWired<SessionService>();
            
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
