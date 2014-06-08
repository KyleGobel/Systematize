using System;
using System.Collections.Generic;
using System.Diagnostics;
using Systematize.ServiceModel;
using Systematize.ServiceModel.Types;
using ServiceStack;
using ServiceStack.Text;
using Xunit;

namespace Systematize.ServiceTests
{
    public class JournalServiceTests : IDisposable
    {
        private AppHost appHost;
        private Stopwatch stopWatch;

        public JournalServiceTests()
        {
            stopWatch = Stopwatch.StartNew();
            appHost = new AppHost();
            appHost.Init();
            appHost.Start("http://*:8888/");
        }

        [Fact(DisplayName = "GetAll returns same counts as SeedData")]
        public void ShouldReturnCorrectNumberOfJournals()
        {
            var client = new JsonServiceClient("http://localhost:8888/");
            var journalResponse = client.Get("/journals");
        }
        
    
        public void Dispose()
        {
            "Time Taken {0}ms".Fmt(stopWatch.ElapsedMilliseconds).Print();
            appHost.Dispose();
        }
    }
}