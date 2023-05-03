using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Transactions;
using BLL.Abstractions.Interfaces;
using BLL.Services;
using Core.Models;
using UI.Interfaces;

namespace UI.ConsoleManagers
{
    public class BookingConsoleManager : ConsoleManager<IBookingService, Booking>, IConsoleManager
    {
        private readonly ClassConsoleManager _classConsoleManager;
        private readonly MemberConsoleManager _memberConsoleManager;
        private readonly IMemberService memberService;
        private readonly IClassService classServise;
        private readonly IBookingService bookingService;


        public BookingConsoleManager(IBookingService bookingService, ClassConsoleManager classConsoleManager, MemberConsoleManager memberConsoleManager, IClassService classServise,IMemberService memberService)
            : base(bookingService)
        {
            _classConsoleManager = classConsoleManager;
            _memberConsoleManager = memberConsoleManager;
            this.memberService = memberService;
            this.classServise = classServise;
            this.bookingService = bookingService;
        }

        public override async Task PerformOperationsAsync()
        {
            Dictionary<string, Func<Task>> actions = new Dictionary<string, Func<Task>>
            {
                { "1", DisplayAllBookingsAsync },
                { "2", CreateBookingAsync },
                { "3", UpdateBookingAsync },
                { "4", DeleteBookingAsync },
            };

            while (true)
            {
                Console.WriteLine("\nBooking operations:");
                Console.WriteLine("1. Display all bookings");
                Console.WriteLine("2. Create a new booking");
                Console.WriteLine("3. Update a booking");
                Console.WriteLine("4. Delete a booking");
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

        public async Task DisplayAllBookingsAsync()
        {
            var booking = await GetAllAsync();
            foreach (var item in booking)
            {
                Console.WriteLine($"Booking:{item}");
            }
        }

        public async Task CreateBookingAsync()
        {
            var booking = new Booking();
            Console.WriteLine("Enter Id of Member:");
            booking.Member = await memberService.GetById(Guid.Parse(Console.ReadLine()));
            Console.WriteLine("Enter Id of Class:");
            booking.Class = await classServise.GetById(Guid.Parse(Console.ReadLine()));
            Console.WriteLine("Enter the date when the class is booked:");
            booking.Date = DateTime.Parse(Console.ReadLine());
            booking.IsConfirmed = true;
            Console.WriteLine("Thank you class booked successfully");
        }

        public async Task UpdateBookingAsync()
        {
            Booking bookingToChange = null;
            if (Guid.TryParse(Console.ReadLine(), out var id))
                bookingToChange = await bookingService.GetById(id);
            else
            {
                Console.WriteLine("Didnt find a Booking");

            };
            while (true)
            {
                Console.WriteLine("What do you whant to Change?");
                int choise = int.Parse(Console.ReadLine());
                Console.WriteLine(@"1.Member
2.Class
3.Date
4.Booked
0.Exid");
                switch (choise)
                {
                    case 0: return;
                    case 1:
                        Console.WriteLine("Enter Id of new Member:");
                        bookingToChange.Member = await memberService.GetById(Guid.Parse(Console.ReadLine()));
                        break;
                    case 2:
                        Console.WriteLine("Enter Id of new Member:");
                        bookingToChange.Class = await classServise.GetById(Guid.Parse(Console.ReadLine()));
                        break;
                    case 3:
                        Console.WriteLine("Enter Id of new Member:");
                        bookingToChange.Date = DateTime.Parse(Console.ReadLine());
                        break;
                    case 4:
                        if (bookingToChange.IsConfirmed == true)
                            bookingToChange.IsConfirmed = false;
                        else bookingToChange.IsConfirmed = true;
                        break;
                }
            }
        }

        public async Task DeleteBookingAsync()
        {
            Console.WriteLine("Enter Id of Booking:");
            Guid id = Guid.Parse(Console.ReadLine());
            await DeleteAsync(id);
        }
    }
}