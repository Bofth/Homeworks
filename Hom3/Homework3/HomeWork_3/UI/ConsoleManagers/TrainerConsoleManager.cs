using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using BLL.Services;
using Core.Models;
using UI.Interfaces;

namespace UI.ConsoleManagers
{
    public class TrainerConsoleManager : ConsoleManager<ITrainerService, Trainer>, IConsoleManager
    {
        ITrainerService trainerService;
        public TrainerConsoleManager(ITrainerService trainerService) : base(trainerService)
        {
            this.trainerService = trainerService;
        }

        public override async Task PerformOperationsAsync()
        {
            Dictionary<string, Func<Task>> actions = new Dictionary<string, Func<Task>>
            {
                { "1", DisplayAllTrainersAsync },
                { "2", CreateTrainerAsync },
                { "3", UpdateTrainerAsync },
                { "4", DeleteTrainerAsync },
            };

            while (true)
            {
                Console.WriteLine("\nTrainer operations:");
                Console.WriteLine("1. Display all trainers");
                Console.WriteLine("2. Create a new trainer");
                Console.WriteLine("3. Update a trainer");
                Console.WriteLine("4. Delete a trainer");
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

        public async Task DisplayAllTrainersAsync()
        {
           var trainers = await GetAllAsync();
            foreach (var trainer in trainers)
            {
                Console.WriteLine($"Trainer: {trainer}");
            }
        }

        public async Task CreateTrainerAsync()
        {
            Trainer newTrainer = new Trainer();
            await trainerService.AddTrainer(newTrainer);
        }

        public async Task UpdateTrainerAsync()
        {
            Console.WriteLine("Enter Name of User:");
            var trainerWasChanget = await trainerService.GetById(Guid.Parse(Console.ReadLine()));
            int choise;
            while (true)
            {
                Console.WriteLine("What do you whant to change?");
                Console.WriteLine("1.FirstName");
                Console.WriteLine("2.LastName");
                Console.WriteLine("3.Specialization");
                Console.WriteLine("4.AvailableDates");
                Console.WriteLine("0.Exid");
                if (int.TryParse(Console.ReadLine(), out choise)) { }
                switch (choise)
                {
                    case 0:
                        return;
                    case 1:
                        Console.WriteLine("Enter new FirstName:");
                        trainerWasChanget.FirstName = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Enter new LastName:");
                        trainerWasChanget.LastName = Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("Enter new Specialization:");
                        trainerWasChanget.Specialization = Console.ReadLine();
                        break;
                    case 4:
                        ICollection<DateTime> dates = new List<DateTime>();
                        while (true)
                        {
                            Console.Write("Enter a empty date (e.g. 10/22/1987): ");
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime inputtedDate))
                            {
                                dates.Add(inputtedDate);
                            }
                            else
                            {
                                break;
                            }
                        }
                        trainerWasChanget.AvailableDates = dates;
                        break;
                }
            }
        }

        public async Task DeleteTrainerAsync()
        {
            Console.WriteLine("Enter Name of User:");
            var trainerWasChanget = await trainerService.GetById(Guid.Parse(Console.ReadLine()));
            await DeleteAsync(trainerWasChanget.Id);
            await trainerService.Delete(trainerWasChanget.Id);
        }
    }
}