using System.Collections.Generic;

class Program
{
    static List<string> dishes = new List<string>();
    static List<string> ingredients = new List<string>();
    static List<string> employees = new List<string>();
    static List<string> tables = new List<string>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Add Dish");
            Console.WriteLine("2. Add Ingredient");
            Console.WriteLine("3. Add Employee");
            Console.WriteLine("4. Add Table");
            Console.WriteLine("5. View Dishes");
            Console.WriteLine("6. View Ingredients");
            Console.WriteLine("7. View Employees");
            Console.WriteLine("8. View Tables");
            Console.WriteLine("0. Exit");

            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.WriteLine("Enter dish name:");
                    string dishName = Console.ReadLine();
                    dishes.Add(dishName);
                    Console.WriteLine("Dish added successfully!");
                    break;

                case 2:
                    Console.WriteLine("Enter ingredient name:");
                    string ingredientName = Console.ReadLine();
                    ingredients.Add(ingredientName);
                    Console.WriteLine("Ingredient added successfully!");
                    break;

                case 3:
                    Console.WriteLine("Enter employee name:");
                    string employeeName = Console.ReadLine();
                    employees.Add(employeeName);
                    Console.WriteLine("Employee added successfully!");
                    break;

                case 4:
                    Console.WriteLine("Enter table number:");
                    string tableNumber = Console.ReadLine();
                    tables.Add(tableNumber);
                    Console.WriteLine("Table added successfully!");
                    break;

                case 5:
                    Console.WriteLine("Dishes:");
                    foreach (string d in dishes)
                    {
                        Console.WriteLine(d);
                    }
                    break;

                case 6:
                    Console.WriteLine("Ingredients:");
                    foreach (string i in ingredients)
                    {
                        Console.WriteLine(i);
                    }
                    break;

                case 7:
                    Console.WriteLine("Employees:");
                    foreach (string e in employees)
                    {
                        Console.WriteLine(e);
                    }
                    break;

                case 8:
                    Console.WriteLine("Tables:");
                    foreach (string t in tables)
                    {
                        Console.WriteLine(t);
                    }
                    break;

                case 0:
                    Console.WriteLine("Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }
}