using System;

namespace DotNetCoreDocker.Models
{
    public class Todo
    {
        public Todo() { }

        public Todo(string task, bool done)
        {
            Task = task;
            Done = done;
        }

        public int ID { get; set; }
        public string Task { get; set; }
        public bool Done { get; set; }
    }
}