using Hom2;
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
        InputOutput inputAndOutput = new InputOutput();
        public void Manage(int choice)
        {
            if (choice == 0)
            {
                return;
            }
            switch (choice)
            {
                case 1:
                    inputAndOutput.Output("Enter Project Name: ");
                    string projectName = inputAndOutput.Redline();
                    inputAndOutput.Output("Write to Deadline ((MM/dd/yyyy))");
                    DateTime projectDeadline = DateTime.Parse(inputAndOutput.Redline());
                    byte choceOfAdd = 1;
                    string projectDescription = "-";
                    double projectBudget = 0;
                    while (choceOfAdd != 0)
                    {
                        inputAndOutput.Output("Anything else you'd like to add?: ");
                        inputAndOutput.Output("1.Description");
                        inputAndOutput.Output("2.Project Budget");
                        choceOfAdd = byte.Parse(inputAndOutput.Redline());
                        if (choceOfAdd == 0) { break; }
                        switch(choceOfAdd)
                        {
                            case 1:
                                inputAndOutput.Output("Write a description: ");
                                projectDescription = inputAndOutput.Redline();
                                break;
                            case 2:
                                inputAndOutput.Output("Enter Project Budget: ");
                                projectBudget = double.Parse(inputAndOutput.Redline());
                                break;
                        }
                    }
                    Project project = new Project(projectName, projectDescription, projectDeadline, projectBudget);
                    projects.Add(project);
                    inputAndOutput.Output("Project created successfully.");
                    break;

                case 2:
                    inputAndOutput.Output("Enter Project Name: ");
                    projectName = inputAndOutput.Redline();
                    project = projects.Find(p => p.Name == projectName);

                    if (project == null)
                    {
                        Console.WriteLine("Project not found.");
                        break;
                    }

                    inputAndOutput.Output("Enter Task Name: ");
                    string taskName = inputAndOutput.Redline();
                    inputAndOutput.Output("Enter Task Description: ");
                    string taskDescription = inputAndOutput.Redline();
                    inputAndOutput.Output("Enter Task Deadline (MM/dd/yyyy): ");
                    DateTime taskDeadline = DateTime.Parse(inputAndOutput.Redline());

                    Task task = new Task(taskName, taskDescription, taskDeadline);
                    project.Tasks.Add(task);
                    inputAndOutput.Output("Task added successfully.");
                    break;

                case 3:
                    inputAndOutput.Output("Enter Project Name: ");
                    projectName = inputAndOutput.Redline();
                    project = projects.Find(p => p.Name == projectName);

                    if (project == null)
                    {
                        inputAndOutput.Output("Project not found.");
                        break;
                    }

                    inputAndOutput.Output("Enter Task Name: ");
                    taskName = inputAndOutput.Redline();
                    task = project.Tasks.Find(t => t.Name == taskName);

                    if (task == null)
                    {
                        inputAndOutput.Output("Task not found.");
                        break;
                    }

                    project.Tasks.Remove(task);
                    inputAndOutput.Output("Task removed successfully.");
                    break;

                case 4:
                    inputAndOutput.Output("Enter Project Name: ");
                    projectName = inputAndOutput.Redline();
                    project = projects.Find(p => p.Name == projectName);

                    if (project == null)
                    {
                        inputAndOutput.Output("Project not found.");
                        break;
                    }

                    inputAndOutput.Output("1. Edit Name");
                    inputAndOutput.Output("2. Edit Description");
                    inputAndOutput.Output("3. Edit Deadline");
                    inputAndOutput.Output("4. Edit Budget");

                    int editChoice = int.Parse(inputAndOutput.Redline());
                    switch (editChoice)
                    {
                        case 1:
                            inputAndOutput.Output("Enter New Project Name: ");
                            projectName = inputAndOutput.Redline();
                            project.Name = projectName;
                            inputAndOutput.Output("Project name updated successfully.");
                            break;
                        case 2:
                            inputAndOutput.Output("Enter New Project Description: ");
                            projectDescription = inputAndOutput.Redline();
                            project.Description = projectDescription;
                            inputAndOutput.Output("Project description updated successfully.");
                            break;
                        case 3:
                            inputAndOutput.Output("Enter New Project Deadline (MM/dd/yyyy): ");
                            projectDeadline = DateTime.Parse(inputAndOutput.Redline());
                            project.Deadline = projectDeadline;
                            inputAndOutput.Output("Project deadline updated successfully.");
                            break;
                        case 4:
                            inputAndOutput.Output("Enter New Project Budget: ");
                            projectBudget = double.Parse(inputAndOutput.Redline());
                            project.Budget = projectBudget;
                            inputAndOutput.Output("Project budget updated successfully.");
                            break;
                        default:
                            inputAndOutput.Output("Invalid choice.");
                            break;
                    }
                    break;
                case 5:
                    inputAndOutput.Output("Enter Project Name: ");
                    projectName = inputAndOutput.Redline();
                    project = projects.Find(p => p.Name == projectName);

                    if (project == null)
                    {
                        inputAndOutput.Output("Project not found.");
                        break;
                    }

                    inputAndOutput.Output("1. Add Team Member");
                    inputAndOutput.Output("2. Remove Team Member");
                    inputAndOutput.Output("3. Change Team Member Status");

                    int teamChoice = int.Parse(inputAndOutput.Redline());
                    switch (teamChoice)
                    {
                        case 1:
                            inputAndOutput.Output("Enter Team Member Name: ");
                            string teamMemberName = inputAndOutput.Redline();
                            inputAndOutput.Output("Enter Team Member Role: ");
                            string teamMemberRole = inputAndOutput.Redline();

                            TeamMember teamMember = new TeamMember(teamMemberName, teamMemberRole);
                            project.TeamMembers.Add(teamMember);
                            inputAndOutput.Output("Team member added successfully.");
                            break;
                        case 2:
                            inputAndOutput.Output("Enter Team Member Name: ");
                            teamMemberName = inputAndOutput.Redline();
                            teamMember = project.TeamMembers.Find(tm => tm.Name == teamMemberName);

                            if (teamMember == null)
                            {
                                inputAndOutput.Output("Team member not found.");
                                break;
                            }

                            project.TeamMembers.Remove(teamMember);
                            inputAndOutput.Output("Team member removed successfully.");
                            break;
                        case 3:
                            inputAndOutput.Output("Enter Team Member Name: ");
                            teamMemberName = inputAndOutput.Redline();
                            teamMember = project.TeamMembers.Find(tm => tm.Name == teamMemberName);

                            if (teamMember == null)
                            {
                                inputAndOutput.Output("Team member not found.");
                                break;
                            }

                            inputAndOutput.Output("1. Set Status to In Progress");
                            inputAndOutput.Output("2. Set Status to Completed");

                            int statusChoice = int.Parse(Console.ReadLine());
                            switch (statusChoice)
                            {
                                case 1:
                                    teamMember.Status = TaskStatus.InProgress;
                                    inputAndOutput.Output("Team member status updated to In Progress.");
                                    break;
                                case 2:
                                    teamMember.Status = TaskStatus.Completed;
                                    inputAndOutput.Output("Team member status updated to Completed.");
                                    break;
                                default:
                                    inputAndOutput.Output("Invalid choice.");
                                    break;
                            }
                            break;
                        default:
                            inputAndOutput.Output("Invalid choice.");
                            break;
                    }
                    break;

                case 6:
                    inputAndOutput.Output("Enter Project Name: ");
                    projectName = inputAndOutput.Redline();
                    project = projects.Find(p => p.Name == projectName);

                    if (project == null)
                    {
                        inputAndOutput.Output("Project not found.");
                        break;
                    }

                    inputAndOutput.Output($"Project Name: {project.Name}");
                    inputAndOutput.Output($"Project Description: {project.Description}");
                    inputAndOutput.Output($"Project Deadline: {project.Deadline.ToShortDateString()}");
                    inputAndOutput.Output($"Project Budget: {project.Budget}");

                    foreach (Task t in project.Tasks)
                    {
                        inputAndOutput.Output($"Task Name: {t.Name}");
                        inputAndOutput.Output($"Task Description: {t.Description}");
                        inputAndOutput.Output($"Task Deadline: {t.Deadline.ToShortDateString()}");
                        inputAndOutput.Output($"Task Status: {t.Status}");

                        inputAndOutput.Output("Assigned Team Members:");
                        foreach (TeamMember tm in t.TeamMembers)
                        {
                            inputAndOutput.Output($"- {tm.Name} ({tm.Role})");
                        }

                        Console.WriteLine();
                    }

                    break;
                default:
                    inputAndOutput.Output("Invalid choice.");
                    break;
            }
        }
        
    }
    class InputOutput
    {
        public string Redline()
        {
            return Console.ReadLine();
        }
        public void Output(string Output)
        {
           Console.WriteLine(Output);
        }
    }
    
}

