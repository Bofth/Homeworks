using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hom2
{
    class Program
    {
        static void Main(string[] args)
        {
            ProjeckFunKtions a = new();
            while (true)
            {
                Console.WriteLine("1. Create Project");
                Console.WriteLine("2. Add Task to Project");
                Console.WriteLine("3. Remove Task from Project");
                Console.WriteLine("4. Edit Project");
                Console.WriteLine("5. Manage Project Team");
                Console.WriteLine("6. View Project Status Reports");
                Console.WriteLine("0. Exit");

                int choice = 0;
                if (Int32.TryParse(Console.ReadLine(), out int j))
                {
                    choice = j;
                }
                else
                {
                    Console.WriteLine("String could not be parsed.");
                }
                a.Manage(choice);
                Console.WriteLine();
            }

        }
        
    }
    class TeamMember
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public TaskStatus Status { get; set; }
        public TeamMember(string name, string role)
        {
            Name = name;
            Role = role;
            Status = TaskStatus.NotStarted;
        }
        
    }
    class Project
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public double Budget { get; set; }
        public List<Task> Tasks { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
        public Project(string name, string description, DateTime deadline, double budget)
        {
            Name = name;
            Description = description;
            Deadline = deadline;
            Budget = budget;
            Tasks = new List<Task>();
            TeamMembers = new List<TeamMember>();
        }
    }
    class ProjeckFunKtions
    {
        List<Project> projects = new List<Project>();
        public void Manage(int choice)
        {
            if (choice == 0)
            {
                return;
            }
            switch (choice)
            {
                case 1:
                    Console.Write("Enter Project Name: ");
                    string projectName = Redline();
                    Console.WriteLine("Write to Deadline ((MM/dd/yyyy))");
                    DateTime projectDeadline = DateTime.Parse(Redline());
                    byte choceOfAdd = 1;
                    string projectDescription = "-";
                    double projectBudget = 0;
                    while (choceOfAdd != 0)
                    {
                        Console.WriteLine("Anything else you'd like to add?: ");
                        Console.WriteLine("1.Description");
                        Console.WriteLine("2.Project Budget");
                        choceOfAdd = byte.Parse(Redline());
                        if (choceOfAdd == 0) { break; }
                        switch(choceOfAdd)
                        {
                            case 1:
                                Console.WriteLine("Write a description: ");
                                projectDescription = Redline();
                                break;
                            case 2:
                                Console.WriteLine("Enter Project Budget: ");
                                projectBudget = double.Parse(Redline());
                                break;
                        }
                    }
                    Project project = new Project(projectName, projectDescription, projectDeadline, projectBudget);
                    projects.Add(project);
                    Console.WriteLine("Project created successfully.");
                    break;

                case 2:
                    Console.Write("Enter Project Name: ");
                    projectName = Redline();
                    project = projects.Find(p => p.Name == projectName);

                    if (project == null)
                    {
                        Console.WriteLine("Project not found.");
                        break;
                    }

                    Console.Write("Enter Task Name: ");
                    string taskName = Redline();
                    Console.Write("Enter Task Description: ");
                    string taskDescription = Redline();
                    Console.Write("Enter Task Deadline (MM/dd/yyyy): ");
                    DateTime taskDeadline = DateTime.Parse(Redline());

                    Task task = new Task(taskName, taskDescription, taskDeadline);
                    project.Tasks.Add(task);
                    Console.WriteLine("Task added successfully.");
                    break;

                case 3:
                    Console.Write("Enter Project Name: ");
                    projectName = Redline();
                    project = projects.Find(p => p.Name == projectName);

                    if (project == null)
                    {
                        Console.WriteLine("Project not found.");
                        break;
                    }

                    Console.Write("Enter Task Name: ");
                    taskName = Redline();
                    task = project.Tasks.Find(t => t.Name == taskName);

                    if (task == null)
                    {
                        Console.WriteLine("Task not found.");
                        break;
                    }

                    project.Tasks.Remove(task);
                    Console.WriteLine("Task removed successfully.");
                    break;

                case 4:
                    Console.Write("Enter Project Name: ");
                    projectName = Redline();
                    project = projects.Find(p => p.Name == projectName);

                    if (project == null)
                    {
                        Console.WriteLine("Project not found.");
                        break;
                    }

                    Console.WriteLine("1. Edit Name");
                    Console.WriteLine("2. Edit Description");
                    Console.WriteLine("3. Edit Deadline");
                    Console.WriteLine("4. Edit Budget");

                    int editChoice = int.Parse(Redline());
                    switch (editChoice)
                    {
                        case 1:
                            Console.Write("Enter New Project Name: ");
                            projectName = Redline();
                            project.Name = projectName;
                            Console.WriteLine("Project name updated successfully.");
                            break;
                        case 2:
                            Console.Write("Enter New Project Description: ");
                            projectDescription = Redline();
                            project.Description = projectDescription;
                            Console.WriteLine("Project description updated successfully.");
                            break;
                        case 3:
                            Console.Write("Enter New Project Deadline (MM/dd/yyyy): ");
                            projectDeadline = DateTime.Parse(Redline());
                            project.Deadline = projectDeadline;
                            Console.WriteLine("Project deadline updated successfully.");
                            break;
                        case 4:
                            Console.Write("Enter New Project Budget: ");
                            projectBudget = double.Parse(Redline());
                            project.Budget = projectBudget;
                            Console.WriteLine("Project budget updated successfully.");
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                    break;
                case 5:
                    Console.Write("Enter Project Name: ");
                    projectName = Redline();
                    project = projects.Find(p => p.Name == projectName);

                    if (project == null)
                    {
                        Console.WriteLine("Project not found.");
                        break;
                    }

                    Console.WriteLine("1. Add Team Member");
                    Console.WriteLine("2. Remove Team Member");
                    Console.WriteLine("3. Change Team Member Status");

                    int teamChoice = int.Parse(Redline());
                    switch (teamChoice)
                    {
                        case 1:
                            Console.Write("Enter Team Member Name: ");
                            string teamMemberName = Redline();
                            Console.Write("Enter Team Member Role: ");
                            string teamMemberRole = Redline();

                            TeamMember teamMember = new TeamMember(teamMemberName, teamMemberRole);
                            project.TeamMembers.Add(teamMember);
                            Console.WriteLine("Team member added successfully.");
                            break;
                        case 2:
                            Console.Write("Enter Team Member Name: ");
                            teamMemberName = Redline();
                            teamMember = project.TeamMembers.Find(tm => tm.Name == teamMemberName);

                            if (teamMember == null)
                            {
                                Console.WriteLine("Team member not found.");
                                break;
                            }

                            project.TeamMembers.Remove(teamMember);
                            Console.WriteLine("Team member removed successfully.");
                            break;
                        case 3:
                            Console.Write("Enter Team Member Name: ");
                            teamMemberName = Console.ReadLine();
                            teamMember = project.TeamMembers.Find(tm => tm.Name == teamMemberName);

                            if (teamMember == null)
                            {
                                Console.WriteLine("Team member not found.");
                                break;
                            }

                            Console.WriteLine("1. Set Status to In Progress");
                            Console.WriteLine("2. Set Status to Completed");

                            int statusChoice = int.Parse(Console.ReadLine());
                            switch (statusChoice)
                            {
                                case 1:
                                    teamMember.Status = TaskStatus.InProgress;
                                    Console.WriteLine("Team member status updated to In Progress.");
                                    break;
                                case 2:
                                    teamMember.Status = TaskStatus.Completed;
                                    Console.WriteLine("Team member status updated to Completed.");
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice.");
                                    break;
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                    break;

                case 6:
                    Console.Write("Enter Project Name: ");
                    projectName = Redline();
                    project = projects.Find(p => p.Name == projectName);

                    if (project == null)
                    {
                        Console.WriteLine("Project not found.");
                        break;
                    }

                    Console.WriteLine($"Project Name: {project.Name}");
                    Console.WriteLine($"Project Description: {project.Description}");
                    Console.WriteLine($"Project Deadline: {project.Deadline.ToShortDateString()}");
                    Console.WriteLine($"Project Budget: {project.Budget}");

                    foreach (Task t in project.Tasks)
                    {
                        Console.WriteLine($"Task Name: {t.Name}");
                        Console.WriteLine($"Task Description: {t.Description}");
                        Console.WriteLine($"Task Deadline: {t.Deadline.ToShortDateString()}");
                        Console.WriteLine($"Task Status: {t.Status}");

                        Console.WriteLine("Assigned Team Members:");
                        foreach (TeamMember tm in t.TeamMembers)
                        {
                            Console.WriteLine($"- {tm.Name} ({tm.Role})");
                        }

                        Console.WriteLine();
                    }

                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
        private string Redline()
        {
            return Console.ReadLine();
        }
    }
}
