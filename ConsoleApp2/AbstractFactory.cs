using System;
using System.Linq;
using System.Collections.Generic;

public interface IRestaurantFactory
{
    Restaurant GetRestaurant();
}


public class FastBitesFactory : IRestaurantFactory
{
    public Restaurant GetRestaurant() =>
        JsonDatabase.Instance.Data.Restaurants.First(r => r.Name == "Fast Bites");
}

public class HealthyEatsFactory : IRestaurantFactory
{
    public Restaurant GetRestaurant() =>
        JsonDatabase.Instance.Data.Restaurants.First(r => r.Name == "Healthy Eats");
}

public class ItalianCornerFactory : IRestaurantFactory
{
    public Restaurant GetRestaurant() =>
        JsonDatabase.Instance.Data.Restaurants.First(r => r.Name == "Italian Corner");
}

public class OrientGrillFactory : IRestaurantFactory
{
    public Restaurant GetRestaurant() =>
        JsonDatabase.Instance.Data.Restaurants.First(r => r.Name == "Orient Grill");
}

public class KoreanFoodFactory : IRestaurantFactory
{
    public Restaurant GetRestaurant() =>
        JsonDatabase.Instance.Data.Restaurants.First(r => r.Name == "Korean Food");
}


public static class RestaurantFactoryProvider
{
    public static IRestaurantFactory GetFactory(string name)
    {
        return name switch
        {
            "Fast Bites" => new FastBitesFactory(),
            "Healthy Eats" => new HealthyEatsFactory(),
            "Italian Corner" => new ItalianCornerFactory(),
            "Orient Grill" => new OrientGrillFactory(),
            "Korean Food" => new KoreanFoodFactory(),
            _ => throw new Exception("Invalid Restaurant")
        };
    }
}