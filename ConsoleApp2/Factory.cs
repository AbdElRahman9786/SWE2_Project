using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    public interface IMenuFactory
    {
        List<MenuItem> GetMenu();
    }

    
    public class FastBitesMenuFactory : IMenuFactory
    {
        public List<MenuItem> GetMenu()
        {
            var restaurant = JsonDatabase.Instance.Data.Restaurants
                .FirstOrDefault(r => r.Name == "Fast Bites");
            if (restaurant == null)
                throw new Exception("Restaurant 'Fast Bites' not found!");
            return restaurant.Menu;
        }
    }

    public class HealthyEatsMenuFactory : IMenuFactory
    {
        public List<MenuItem> GetMenu()
        {
            var restaurant = JsonDatabase.Instance.Data.Restaurants
                .FirstOrDefault(r => r.Name == "Healthy Eats");
            if (restaurant == null)
                throw new Exception("Restaurant 'Healthy Eats' not found!");
            return restaurant.Menu;
        }
    }

    public class ItalianCornerMenuFactory : IMenuFactory
    {
        public List<MenuItem> GetMenu()
        {
            var restaurant = JsonDatabase.Instance.Data.Restaurants
                .FirstOrDefault(r => r.Name == "Italian Corner");
            if (restaurant == null)
                throw new Exception("Restaurant 'Italian Corner' not found!");
            return restaurant.Menu;
        }
    }

    public class OrientGrillMenuFactory : IMenuFactory
    {
        public List<MenuItem> GetMenu()
        {
            var restaurant = JsonDatabase.Instance.Data.Restaurants
                .FirstOrDefault(r => r.Name == "Orient Grill");
            if (restaurant == null)
                throw new Exception("Restaurant 'Orient Grill' not found!");
            return restaurant.Menu;
        }
    }

    public class KoreanFoodMenuFactory : IMenuFactory
    {
        public List<MenuItem> GetMenu()
        {
            var restaurant = JsonDatabase.Instance.Data.Restaurants
                .FirstOrDefault(r => r.Name == "Korean Food");
            if (restaurant == null)
                throw new Exception("Restaurant 'Korean Food' not found!");
            return restaurant.Menu;
        }
    }

    
    public static class MenuFactoryProvider
    {
        public static IMenuFactory GetMenuFactory(string restaurantName)
        {
            return restaurantName switch
            {
                "Fast Bites" => new FastBitesMenuFactory(),
                "Healthy Eats" => new HealthyEatsMenuFactory(),
                "Italian Corner" => new ItalianCornerMenuFactory(),
                "Orient Grill" => new OrientGrillMenuFactory(),
                "Korean Food" => new KoreanFoodMenuFactory(),
                _ => throw new Exception($"No menu factory found for '{restaurantName}'")
            };
        }
    }
}
