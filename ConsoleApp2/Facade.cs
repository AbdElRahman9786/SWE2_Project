using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    public class RestaurantFacade
    {
        private readonly JsonDatabase _database;

        public RestaurantFacade()
        {
            _database = JsonDatabase.Instance; // Singleton
        }

        // ================= Singleton =================
        public RootData GetAllData()
        {
            return _database.Data;
        }

        public void SaveDatabase()
        {
            _database.Save();
        }

        // ================= Factory =================
        public Restaurant GetRestaurant(string restaurantName)
        {
            var factory = RestaurantFactoryProvider.GetFactory(restaurantName);
            return factory.GetRestaurant();
        }

        // ================= Abstract Factory =================
        public List<MenuItem> GetMenu(string restaurantName)
        {
            var menuFactory = MenuFactoryProvider.GetMenuFactory(restaurantName);
            return menuFactory.GetMenu();
        }

        // ================= Decorator =================
        public IOrderComponent CreateOrder(
            MenuItem menuItem,
            List<Customization> customizations)
        {
            IOrderComponent order = new BaseOrder(menuItem);

            foreach (var customization in customizations)
            {
                order = new CustomizationDecorator(order, customization);
            }

            return order;
        }

        public List<Customization> GetAllCustomizations()
        {
            return _database.Data.Customizations;
        }

        // ================= Helpers =================
        public void PrintRestaurants()
        {
            var restaurants = _database.Data.Restaurants;
            for (int i = 0; i < restaurants.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {restaurants[i].Name}");
            }
        }
    }
}
