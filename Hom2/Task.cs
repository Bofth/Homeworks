using System;
using System.Collections.Generic;

namespace Hom2
{
    class Task
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public TaskStatus Status { get; set; }
        public List<TeamMember> TeamMembers { get; set; }

        public Task(string name, string description, DateTime deadline)
        {
            Name = name;
            Description = description;
            Deadline = deadline;
            Status = TaskStatus.NotStarted;
            TeamMembers = new List<TeamMember>();
        }
    }
}
