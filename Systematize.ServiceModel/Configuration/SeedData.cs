using System;
using System.Collections.Generic;
using Systematize.ServiceModel.Types;

namespace Systematize.ServiceModel.Configuration
{
    public static class SeedData
    {
        public static List<Journal> JournalSeed = new List<Journal>();
        public static List<JournalNote> JournalNoteSeed = new List<JournalNote>();
        public static List<Session> SessionSeed = new List<Session>();
        public static List<SessionNote> SessionNoteSeed = new List<SessionNote>();
        public static List<Task> TaskSeed = new List<Task>();

        static SeedData()
        {

            //All Seeds for a "Deerso" Journal and all enclosed objects.
            JournalSeed.Add(new Journal
            {
                Id = 1,
                Name = "Deerso",
                Description = "All things Deerso.",
                CreatedAt = DateTime.Now
            });

            JournalNoteSeed.Add(new JournalNote
            {
                Id = 1,
                JournalId = 1,
                Note = "Need to get Deerso.com published and figure out wtf is wrong with it.",
                CreatedAt = DateTime.Now
            });

            SessionSeed.Add(
                new Session
                {
                    Id = 1,
                    JournalId = 1,
                    Name = "6/8/2014 - Deerso.com Debugging",
                    CreatedAt = DateTime.Now,
                    Description = "I hope to get deerso.com finished and published in this session."
                });

            TaskSeed.Add(
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

            SessionNoteSeed.Add(new SessionNote
            {
                Id = 1,
                SessionId = 1,
                Note = "Dont forget to check on staging before publishing.",
                CreatedAt = DateTime.Now,
                TaskId = 1
            });
        }
    }
}
