using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using BLL.Services;
using Core.Models;
using UI.Interfaces;

namespace UI.ConsoleManagers
{
    public class ClassConsoleManager : ConsoleManager<IClassService, FitnessClass>, IConsoleManager
    {
        IClassService classService;
        ITrainerService trainerService;
        IMemberService memberService;
        public ClassConsoleManager(IClassService classService, ITrainerService trainerService,IMemberService memberService) : base(classService)
        {
            this.classService = classService;
            this.trainerService = trainerService;
            this.memberService = memberService;
        }

        public override async Task PerformOperationsAsync()
        {
            Dictionary<string, Func<Task>> actions = new Dictionary<string, Func<Task>>
            {
                { "1", DisplayAllClassesAsync },
                { "2", CreateClassAsync },
                { "3", UpdateClassAsync },
                { "4", DeleteClassAsync },
            };

            while (true)
            {
                Console.WriteLine("\nClass operations:");
                Console.WriteLine("1. Display all classes");
                Console.WriteLine("2. Create a new class");
                Console.WriteLine("3. Update a class");
                Console.WriteLine("4. Delete a class");
                Console.WriteLine("5. Exit");

                Console.Write("Enter the operation number: ");
                string input = Console.ReadLine();

                if (input == "5")
                {
                    break;
                }

                if (actions.ContainsKey(input))
                {
                    await actions[input]();
                }
                else
                {
                    Console.WriteLine("Invalid operation number.");
                }
            }
        }
        
        public async Task DisplayAllClassesAsync()
        {
            var clas = await GetAllAsync();
            foreach (var item in clas)
            {
                Console.WriteLine($"Member:{item}");
            }
        }

        public async Task CreateClassAsync()
        {
            FitnessClass newClass = new FitnessClass();

            ChangeName(newClass);

            ChangeType(newClass);

            ChangeResponsibleCoach(newClass);

            ChangeStartTime(newClass);
            
            ChangeDate(newClass);

            ChangeEndTime(newClass);

            ChangeVisitors(newClass);

            await CreateAsync(newClass);
        }

        private void ChangeName(FitnessClass clas)
        {
            Console.WriteLine("Enter Name:");
            clas.Name = Console.ReadLine();
        }
        private async void ChangeResponsibleCoach(FitnessClass clas)
        {
            Console.WriteLine("Enter the Id of the responsible coach:");
            clas.Trainer = await trainerService.GetById(Guid.Parse(Console.ReadLine()));
        }
        private void ChangeType(FitnessClass clas)
        {
            Console.WriteLine("Enter Type:");
            clas.Type = Console.ReadLine();
        }
        private void ChangeDate(FitnessClass clas)
        {
            Console.WriteLine("Enter Date:");
            clas.Date = DateTime.Parse(Console.ReadLine());
        }
        private void ChangeStartTime(FitnessClass clas)
        {
            Console.WriteLine("Enter StartTime:");
            clas.Date = DateTime.Parse(Console.ReadLine());
        }
        private void ChangeEndTime(FitnessClass clas)
        {
            Console.WriteLine("Enter EndTime:");
            clas.Date = DateTime.Parse(Console.ReadLine());
        }
        private async void ChangeVisitors(FitnessClass clas)
        {
            while (true)
            {
                Console.WriteLine("Enter visitor id or 0 to stop:");
                string choce = Console.ReadLine();
                var membrAnes = await memberService.GetById(Guid.Parse(choce));
                clas.Attendees.Add(membrAnes);
                if (int.Parse(choce) == 0) { break; }
            }
        }

        public async Task UpdateClassAsync()
        {
            FitnessClass classToChange = null;
            if (Guid.TryParse(Console.ReadLine(), out var id))
                classToChange = await classService.GetById(id);
            else {
                Console.WriteLine("Didnt find a Class");

                 };
            while (true)
            {
                Console.WriteLine("What do you whant to Change?");
                Console.WriteLine(@"1.Name
2.Type
3.Responsible coach
4.Start date
5.End time
6.Visitor
0.Exid");       //As for me, this withdrawal decision will be much faster, but it does not smell of beauty here :)
                int choise = int.Parse(Console.ReadLine());
                switch (choise)
                {
                    case 0:return;
                    case 1:
                        ChangeName(classToChange);
                        break;
                    case 2:
                        ChangeType(classToChange);
                        break;
                    case 3:
                        ChangeResponsibleCoach(classToChange);
                        break;
                    case 4:
                        ChangeStartTime(classToChange);
                        break;
                    case 5:
                        ChangeEndTime(classToChange);
                        break;
                    case 6:
                        ChangeVisitors(classToChange);
                        break;
                    
                }
            }
        }

        public async Task DeleteClassAsync()
        {
            Console.WriteLine("Enter Id of class:");
            Guid id = Guid.Parse(Console.ReadLine());
            await DeleteAsync(id);
        }
    }
}