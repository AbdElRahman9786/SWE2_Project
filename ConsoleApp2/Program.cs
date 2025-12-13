using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var restaurants = JsonDatabase.Instance.Data.Restaurants;

            if (restaurants.Count == 0)
            {
                Console.WriteLine("No restaurants available.");
                return;
            }

            bool continueOrdering = true;

            while (continueOrdering)
            {
             

                Console.WriteLine("                                                      ========================================               ");
                Console.WriteLine("                                                            Welcome to Food Delivery App");
                Console.WriteLine("                                                      ========================================              \n");

               
                Console.WriteLine("Available Restaurants:");
                for (int i = 0; i < restaurants.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {restaurants[i].Name}");
                }


                int rChoice;
                while (true)
                {
                    Console.Write("\nChoose Restaurant: ");
                    if (int.TryParse(Console.ReadLine(), out rChoice) &&
                        rChoice >= 1 && rChoice <= restaurants.Count)
                    {
                        rChoice -= 1;
                        break;
                    }
                    Console.WriteLine("Invalid choice, try again.");
                }

                string restaurantName = restaurants[rChoice].Name;

                Console.WriteLine("\n----------------------------------------");
                Console.WriteLine($"          You Selected: {restaurantName}");
                Console.WriteLine("----------------------------------------\n");

                
                var menuFactory = MenuFactoryProvider.GetMenuFactory(restaurantName);
                var menu = menuFactory.GetMenu();

                if (menu.Count == 0)
                {
                    Console.WriteLine("No menu items available.");
                    continue;
                }

                Console.WriteLine($"----- {restaurantName} Menu -----");
                for (int i = 0; i < menu.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {menu[i].Name} - {menu[i].Price}");
                }

                int fChoice;
                while (true)
                {
                    Console.Write("\nChoose Food Item: ");
                    if (int.TryParse(Console.ReadLine(), out fChoice) &&
                        fChoice >= 1 && fChoice <= menu.Count)
                    {
                        fChoice -= 1;
                        break;
                    }
                    Console.WriteLine("Invalid choice, try again.");
                }

                var selectedFood = menu[fChoice];

                Console.WriteLine($"\nYou selected: {selectedFood.Name} - {selectedFood.Price} LE");
                Console.WriteLine("\nOrder placed successfully ");

                
                Console.Write("\nDo you want to order again? (y/n): ");
              string answer = Console.ReadLine()?.ToLower() ?? "";


                if (answer != "y" && answer != "yes")
                {
                    continueOrdering = false;
                }
            }

            Console.WriteLine("\nThank you for using Food Delivery App ");
        }
    }
}
