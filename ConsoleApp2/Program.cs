using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var facade = new FoodOrderingFacade();
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

                var selectedRestaurant = restaurants[rChoice];
                string restaurantName = selectedRestaurant.Name;

                Console.WriteLine("\n----------------------------------------");
                Console.WriteLine($"          You Selected: {restaurantName}");
                Console.WriteLine("----------------------------------------\n");

               
                var menu = facade.GetMenu(restaurantName);

                if (menu.Count == 0)
                {
                    Console.WriteLine("No menu items available.");
                    continue;
                }

                Console.WriteLine($"----- {restaurantName} Menu -----");
                for (int i = 0; i < menu.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {menu[i].Name} - {menu[i].Price} LE");
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

                var selectedCustomizations = new List<Customization>();

                var availableCustomizations = selectedRestaurant.Customizations
                    .Where(c => c.ApplicableItems == null ||
                                c.ApplicableItems.Count == 0 ||
                                c.ApplicableItems.Contains(selectedFood.Id))
                    .ToList();

                if (availableCustomizations.Count > 0)
                {
                    bool addingCustomizations = true;

                    while (addingCustomizations)
                    {
                        Console.WriteLine($"\n----- Available Customizations for {selectedFood.Name} -----");
                        for (int i = 0; i < availableCustomizations.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}) {availableCustomizations[i].Name} - {availableCustomizations[i].Price} LE");
                        }
                        Console.WriteLine($"{availableCustomizations.Count + 1}) No more customizations");

                        int cChoice;
                        while (true)
                        {
                            Console.Write("\nChoose Customization: ");
                            if (int.TryParse(Console.ReadLine(), out cChoice) &&
                                cChoice >= 1 && cChoice <= availableCustomizations.Count + 1)
                            {
                                break;
                            }
                            Console.WriteLine("Invalid choice, try again.");
                        }

                        if (cChoice == availableCustomizations.Count + 1)
                        {
                            addingCustomizations = false;
                        }
                        else
                        {
                            var customization = availableCustomizations[cChoice - 1];
                            selectedCustomizations.Add(customization);
                            Console.WriteLine($"\n✓ Added: {customization.Name}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"\nNo customizations available for {selectedFood.Name}.");
                }

               
                var orderComponent = facade.CreateOrder(selectedFood, selectedCustomizations);

                Console.WriteLine("\n========================================");
                Console.WriteLine("           ORDER SUMMARY");
                Console.WriteLine("========================================");
                Console.WriteLine($"Restaurant: {restaurantName} ({selectedRestaurant.Type})");
                Console.WriteLine($"Item: {orderComponent.GetDescription()}");
                Console.WriteLine($"Total Price: {orderComponent.GetPrice()} LE");
                Console.WriteLine("========================================");
                Console.WriteLine("\nOrder placed successfully!");

                
                facade.PlaceOrder(
                    selectedRestaurant.Id,
                    new List<IOrderComponent> { orderComponent }
                );

                Console.Write("\nDo you want to order again? (y/n): ");
                string answer = Console.ReadLine()?.ToLower() ?? "";

                if (answer != "y" && answer != "yes")
                {
                    continueOrdering = false;
                }
            }

            Console.WriteLine("\nThank you for using Food Delivery App!");
        }
    }
}
