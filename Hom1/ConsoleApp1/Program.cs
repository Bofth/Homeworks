using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Security.Authentication;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RestorantManagment a = new RestorantManagment();
            while (true)
            {
                Console.WriteLine("Please select-an-option:");
                Console.WriteLine("1. Add Dish");
                Console.WriteLine("2. Add Ingredient");
                Console.WriteLine("3. Add Employee");
                Console.WriteLine("4. Add Table");
                Console.WriteLine("5. Add Order");
                Console.WriteLine("6. Add Client");
                Console.WriteLine("7. View Dishes");
                Console.WriteLine("8. View Ingredients");
                Console.WriteLine("9. View Employees");
                Console.WriteLine("10. View Tables");
                Console.WriteLine("11. View Clients");
                Console.WriteLine("0. Exit");
                int option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        a.AddDish();
                        break;
                    case 2:
                        a.AddIngredient();
                        break;
                    case 3:
                        a.AddEmployee();
                        break;
                    case 4:
                        a.AddTable();
                        break;
                    case 5:
                        a.AddOrder();
                        break;
                    case 6:
                        a.AddClient();
                        break;
                    case 7:
                        a.ViewDishes();
                        break;
                    case 8:
                        a.ViewIngredients();
                        break;
                    case 9:
                        a.ViewEmployees();
                        break;
                    case 10:
                        a.ViewTables();
                        break;
                    case 11:
                        a.ViewClients();
                        break;
                    case 12:
                        a.ViewOrder();
                        break;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }
            }
        }

        

    }
    public class RestorantManagment
    {
        List<string> dishes = new List<string>();

        List<string> order = new List<string>();

        List<string> ingredients = new List<string>();

        List<string> employees = new List<string>();

        List<int> tables = new List<int>();

        List<string> position = new List<string>();

        List<int> priseIngridient = new List<int>();

        List<int> numberOfSeats = new List<int>();

        List<string> nameClient = new List<string>();

        List<int> nummerClient = new List<int>();

        List<int> priseDish = new List<int>();

        List<string> buyer = new List<string>();

        List<string> orderedDish = new List<string>();

        public void AddDish()
        {
            Console.WriteLine("Enter dish name:");
            dishes.Add(Console.ReadLine());
            Console.WriteLine("Enter Prise of Dish:");
            priseDish.Add(Int32.Parse(Console.ReadLine()));
            Console.WriteLine("Enter amount of ingridients:");
            int i = Int32.Parse(Console.ReadLine());
            while (i != 0)
            {
                AddIngredient();
                i--;
            }
            Console.WriteLine("Dish-added successfully!");
            Console.WriteLine();
        }
        public void AddIngredient()
        {
            Console.WriteLine("Enter Ingridient name:");
            ingredients.Add(Console.ReadLine());
            Console.WriteLine("Enter prise for this Ingridient:");
            priseIngridient.Add(Int32.Parse(Console.ReadLine()));
            Console.WriteLine("Ingredient added successfully!");
            Console.WriteLine();
        }

        public void AddEmployee()
        {
            Console.WriteLine("Enter employee name:");
            employees.Add(Console.ReadLine());
            Console.WriteLine("Enter the position of the employee:");
            position.Add(Console.ReadLine());
            Console.WriteLine("Employee added successfully!");
            Console.WriteLine();
        }
        public void AddTable()
        {
            Console.WriteLine("Enter table number:");
            tables.Add(Int32.Parse(Console.ReadLine()));
            Console.WriteLine("Enter the number of seats at the table:");
            numberOfSeats.Add(Int32.Parse(Console.ReadLine()));
            Console.WriteLine("Table-added successfully!");
            Console.WriteLine();
        }
        public void AddClient()
        {
            Console.WriteLine("Enter Client name:");
            nameClient.Add(Console.ReadLine());
            Console.WriteLine("Enter the customer's phone number:");
            nummerClient.Add(Int32.Parse(Console.ReadLine()));
            Console.WriteLine("Client added successfully!");
            Console.WriteLine();
        }
        public void AddOrder()
        {
            Console.WriteLine("Who made the order?:");
            ViewClients();
            buyer.Add(nameClient[Int32.Parse(Console.ReadLine())]);
            Console.WriteLine("What do we order?(Whrite -1 for stop):");
            ViewDishes();
            int costOrder = 0;
            while (true)
            {
                int dishesname =Int32.Parse(Console.ReadLine());
                if (dishesname == -1){break;}
                orderedDish.Add(dishes[dishesname]);
                Console.WriteLine("Dish added");
                costOrder += priseDish[dishesname];
            }
            Console.WriteLine($"The cost of the order:{costOrder}");
        }
        public void ViewClients()
        {
            Console.WriteLine("Clients:");
            for (int i = 0;i < nameClient.Count;i++)
            {
                Console.WriteLine($"Name:{i + 1}.{nameClient[i]} Nummber phone {nummerClient[i]}");
            }
            Console.WriteLine();
        }
        public void ViewDishes()
        {
            for (int i = 0; i < dishes.Count; i++)
            {
                Console.WriteLine($"Name:{dishes[i]} Prise: {priseDish[i]}");
            }
            Console.WriteLine();
        }
        public void ViewIngredients()
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                Console.WriteLine($"Name:{ingredients[i]} Prise: {priseIngridient[i]}");
            }
            Console.WriteLine();
        }
        public void ViewEmployees()
        {
            for (int i = 0; i < employees.Count; i++)
            {
                Console.WriteLine($"Name:{employees[i]} Position: {position[i]}");
            }
            Console.WriteLine();
        }
        public void ViewTables()
        {
            for (int i = 0; i < tables.Count; i++)
            {
                Console.WriteLine($"Name:{tables[i]} Number of seats: {numberOfSeats[i]}");
            }
            Console.WriteLine();
        }
        public void ViewOrder()
        {
            Console.WriteLine("What kind of order are you interested in?:");
            for (int i = 0; i < buyer.Count; i++)
            {
                Console.WriteLine($"Name:{buyer[i]} Nummer of Order:{i + 1}");
            }

        }
    }
}
