using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using BLL.Services;
using Core.Models;
using UI.Interfaces;

namespace UI.ConsoleManagers
{
    public class MemberConsoleManager : ConsoleManager<IMemberService, Member>, IConsoleManager
    {
        IMemberService memberService;
        public MemberConsoleManager(IMemberService memberService) : base(memberService)
        {
            this.memberService = memberService;
        }

        public override async Task PerformOperationsAsync()
        {
            Dictionary<string, Func<Task>> actions = new Dictionary<string, Func<Task>>
            {
                { "1", DisplayAllMembersAsync },
                { "2", AddMemberAsync },
                { "3", UpdateMemberAsync },
                { "4", DeleteMemberAsync },
            };

            while (true)
            {
                Console.WriteLine("\nMember operations:");
                Console.WriteLine("1. Display all members");
                Console.WriteLine("2. Add a new member");
                Console.WriteLine("3. Update a member");
                Console.WriteLine("4. Delete a member");
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
        
        public async Task DisplayAllMembersAsync()
        {
            var member = await GetAllAsync();
            foreach(var item in member)
            {
                Console.WriteLine($"Member:{item}");
            }
        }

        public async Task AddMemberAsync()
        {
            Member newMember = new Member();
            await memberService.RegisterMember(newMember);
        }

        public async Task UpdateMemberAsync()
        {
            Console.WriteLine("Enter Id of Member:");
            var memberWasChanget = await memberService.GetById(Guid.Parse(Console.ReadLine()));
            int choise;
            while (true)
            {
                Console.WriteLine("What do you whant to change?");
                Console.WriteLine("1.FirstName");
                Console.WriteLine("2.LastName");
                Console.WriteLine("3.DateOfBirth");
                Console.WriteLine("4.Email");
                Console.WriteLine("4.Email");
                Console.WriteLine("4.Email");
                Console.WriteLine("4.Email");
                Console.WriteLine("0.Exid");
                if (int.TryParse(Console.ReadLine(), out choise)) { }
                switch (choise)
                {
                    case 0:
                        return;
                    case 1:
                        Console.WriteLine("Enter new FirstName:");
                        memberWasChanget.FirstName = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Enter new LastName:");
                        memberWasChanget.LastName = Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("Enter new DateOfBirth (e.g. 10/22/1987):");
                        memberWasChanget.DateOfBirth = DateTime.Parse(Console.ReadLine());
                        break;
                    case 4:
                        Console.WriteLine("Enter new Email:");
                        memberWasChanget.Email = Console.ReadLine();
                        break;
                    case 5:
                        Console.WriteLine("Enter new Specialization:");
                        memberWasChanget.PhoneNumber = Console.ReadLine();
                        break;
                    case 6:
                        Console.WriteLine("Enter SubscriptionType:");
                        memberWasChanget.SubscriptionType = ChoiseSubscriptionType();
                        break;
                    case 7:
                        Console.WriteLine("Enter SubscriptionStartDate (e.g. 10/22/1987):");
                        memberWasChanget.SubscriptionStartDate = DateTime.Parse(Console.ReadLine());
                        break;
                    case 8:
                        Console.WriteLine("Enter SubscriptionEndDate (e.g. 10/22/1987):");
                        memberWasChanget.SubscriptionEndDate = DateTime.Parse(Console.ReadLine());
                        break;
                    case 9:
                        if(memberWasChanget.IsActive == true) 
                        {
                            memberWasChanget.IsActive =  false;
                        }
                        else
                        {
                            memberWasChanget.IsActive = true;
                        }
                        Console.WriteLine("Changet Secsessful");
                        break;
                }
            }
        }

        public async Task DeleteMemberAsync()
        {
            Console.WriteLine("Enter Id of member:");
            Guid id = Guid.Parse(Console.ReadLine());
            await DeleteAsync(id);
        }
    }
}