using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using BLL.Services;
using Core.Enums;
using Core.Models;
using UI.Interfaces;

namespace UI.ConsoleManagers
{
    public class SubscriptionConsoleManager : ConsoleManager<ISubscriptionService, Subscription>, IConsoleManager
    {
        ISubscriptionService subscriptionService;
        IMemberService memberService;
        IGenericService<BaseEntity> genericService;
        public SubscriptionConsoleManager(ISubscriptionService subscriptionService, IMemberService memberService , IGenericService<BaseEntity> genericService) : base(subscriptionService)
        {
            this.subscriptionService = subscriptionService;
            this.memberService = memberService;
            this.genericService = genericService;
        }

        public override async Task PerformOperationsAsync()
        {
            Dictionary<string, Func<Task>> actions = new Dictionary<string, Func<Task>>
            {
                { "1", DisplayAllSubscriptionsAsync },
                { "2", CreateSubscriptionAsync },
                { "3", UpdateSubscriptionAsync },
                { "4", DeleteSubscriptionAsync },
            };

            while (true)
            {
                Console.WriteLine("\nSubscription operations:");
                Console.WriteLine("1. Display all subscriptions");
                Console.WriteLine("2. Create a new subscription");
                Console.WriteLine("3. Update a subscription");
                Console.WriteLine("4. Delete a subscription");
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

        public async Task DisplayAllSubscriptionsAsync()
        {
            var subscriptions = await GetAllAsync();
            foreach (var subscription in subscriptions)
            {
                Console.WriteLine($"Trainer: {subscription}");
            }
        }

        public async Task CreateSubscriptionAsync()
        {
            Subscription newSubscription = new Subscription();
            await subscriptionService.CreateSubscription(newSubscription);
        }

        public async Task UpdateSubscriptionAsync()
        {
            Console.WriteLine("Enter Subcription Id:");
            var subscription = await subscriptionService.GetById(Guid.Parse(Console.ReadLine()));
            int choise;
            while (true)
            {
                Console.WriteLine("What do you whant to change?");
                Console.WriteLine("1.Member");
                Console.WriteLine("2.ChoiseSubscriptionType");
                Console.WriteLine("3.StartDate");
                Console.WriteLine("4.EndDate");
                Console.WriteLine("0.Exid");
                if (int.TryParse(Console.ReadLine(), out choise)) { }
                switch (choise)
                {
                    case 0:
                        return;
                    case 1:
                        Member newMember = new Member();
                        await memberService.RegisterMember(newMember);
                        break;
                    case 2:
                        Console.WriteLine("Enter new ChoiseSubscriptionType:");
                        ChoiseSubscriptionType();
                        break;
                    case 3:
                        Console.WriteLine("Enter new StartDate:");
                        subscription.StartDate = DateTime.Parse(Console.ReadLine());
                        break;
                    case 4:
                        Console.WriteLine("Enter new EndDate:");
                        subscription.EndDate = DateTime.Parse(Console.ReadLine());
                        break;
                    case 5:
                        if(subscription.IsActive == true)
                        {
                            subscription.IsActive = false;
                        }else
                        {
                            subscription.IsActive = true;
                        }
                        Console.WriteLine("Successfully changed");
                        break;
                }
            }
        }
    

        public async Task DeleteSubscriptionAsync()
        {
            Console.WriteLine("Enter MamberName:");
            var subscriptionToChange = await GetByPredicateAsync(i => i.Member.FirstName == Console.ReadLine());
            await DeleteAsync(subscriptionToChange.Id);
        }
    }
}