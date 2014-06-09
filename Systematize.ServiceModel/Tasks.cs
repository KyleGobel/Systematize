﻿using ServiceStack;

namespace Systematize.ServiceModel
{
    [Route("/tasks/{SessionId}", "GET")]
    public class Tasks
    {
         public long SessionId { get; set; }
    }

    [Route("/tasks/{SessionId}", "POST")]
    public class CreateTask
    {
        public long SessionId { get; set; }
        public string Description { get; set; }
    }

}