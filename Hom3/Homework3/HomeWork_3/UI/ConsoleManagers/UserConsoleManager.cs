using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using BLL.Services;
using Core.Enums;
using Core.Models;
using UI.Interfaces;


namespace UI.ConsoleManagers
{
    public class UserConsoleManager : ConsoleManager<IUserService, User>, IConsoleManager
    {
        public List<User> userList = new List<User>();
        IUserService userService;

        public UserConsoleManager(IUserService userService) : base(userService)
        {
            this.userService = userService;
        }

        public override async Task PerformOperationsAsync()
        {
            Dictionary<string, Func<Task>> actions = new Dictionary<string, Func<Task>>
            {
                { "1", DisplayAllUsersAsync },
                { "2", CreateUserAsync },
                { "3", UpdateUserAsync },
                { "4", DeleteUserAsync },
            };

            while (true)
            {
                Console.WriteLine("\nUser operations:");
                Console.WriteLine("1. Display all users");
                Console.WriteLine("2. Create a new user");
                Console.WriteLine("3. Update a user");
                Console.WriteLine("4. Delete a user");
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

        public async Task DisplayAllUsersAsync()
        {
           var Users = await GetAllAsync();
           foreach (var user in Users) 
            {
                Console.WriteLine($"User: {user}");
            }
        }

        public async Task CreateUserAsync()
        {
            User newUser = new User();
            Console.WriteLine("Enter Name of User:");
            newUser.Username = Console.ReadLine();
            Console.WriteLine("Enter Password:");
            newUser.PasswordHash = HashPassword(Console.ReadLine());
            newUser.IsLocked = false;
            newUser.Role = ChoiseUserRole();
            await CreateAsync(newUser);
            userList.Add(newUser);
        }
        public UserRole ChoiseUserRole()
        {
            Console.WriteLine("Enter Role: (0 = Admin, 1 = Trainer, 2 = Member)");
            int choise = int.Parse(Console.ReadLine());
            switch (choise)
            {
                case 0:
                    return UserRole.Admin;

                case 1:
                    return UserRole.Trainer;
                    
                case 2:
                    return UserRole.Member;

                default: 
                    return UserRole.Member;
            }

        }

        public async Task UpdateUserAsync()
        {
            Console.WriteLine("Enter Name of User:");
            var userWasChanget = await userService.GetUserByUsername(Console.ReadLine());
            int choise;
            while (true)
            {
                Console.WriteLine("What do you whant to change?");
                Console.WriteLine("1.Name");
                Console.WriteLine("2.Password");
                Console.WriteLine("3.Role");
                Console.WriteLine("4.Swich Blocking");
                Console.WriteLine("0.Exid");
                if (int.TryParse(Console.ReadLine(), out choise)) { }
                switch (choise) 
                {
                    case 0:
                        return;
                    case 1:
                        Console.WriteLine("Enter new Name:");
                        userWasChanget.Username = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Enter new Password:");
                        userWasChanget.PasswordHash = HashPassword(Console.ReadLine());
                        break;
                    case 3:
                        Console.WriteLine("Enter new Role:");
                        userWasChanget.Role = ChoiseUserRole();
                        break;
                    case 4:
                        if(userWasChanget.IsLocked == false)
                        {
                            userWasChanget.IsLocked = true;
                        }
                        else
                        {
                            userWasChanget.IsLocked = false;
                        }
                        break;
                }

            }
        }

        public async Task DeleteUserAsync()
        {
            Console.WriteLine("Enter Id of user");
            Guid id = Guid.Parse(Console.ReadLine());
            var user = await GetByIdAsync(id);
            await DeleteAsync(id);
            userList.Remove(user);
        }

    }
}