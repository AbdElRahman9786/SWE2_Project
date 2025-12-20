using System;
using System.Linq;

public interface IDeliveryService
{
    string GetEstimatedTime();
}

public class FastDelivery : IDeliveryService
{
    public string GetEstimatedTime() => "30 minutes";
}

public class NormalDelivery : IDeliveryService
{
    public string GetEstimatedTime() => "45 minutes";
}

public interface IRestaurantFactory
{
    Restaurant GetRestaurant();
    IDeliveryService CreateDeliveryService(bool fastDelivery); 
}

public class FastBitesFactory : IRestaurantFactory
{
    public Restaurant GetRestaurant() =>
        JsonDatabase.Instance.Data.Restaurants.First(r => r.Name == "Fast Bites");

    public IDeliveryService CreateDeliveryService(bool fastDelivery)
    {
        return fastDelivery ? new FastDelivery() : new NormalDelivery();
    }
}

public class HealthyEatsFactory : IRestaurantFactory
{
    public Restaurant GetRestaurant() =>
        JsonDatabase.Instance.Data.Restaurants.First(r => r.Name == "Healthy Eats");

    public IDeliveryService CreateDeliveryService(bool fastDelivery)
    {
        return fastDelivery ? new FastDelivery() : new NormalDelivery();
    }
}

public class ItalianCornerFactory : IRestaurantFactory
{
    public Restaurant GetRestaurant() =>
        JsonDatabase.Instance.Data.Restaurants.First(r => r.Name == "Italian Corner");

    public IDeliveryService CreateDeliveryService(bool fastDelivery)
    {
        return fastDelivery ? new FastDelivery() : new NormalDelivery();
    }
}

public class OrientGrillFactory : IRestaurantFactory
{
    public Restaurant GetRestaurant() =>
        JsonDatabase.Instance.Data.Restaurants.First(r => r.Name == "Orient Grill");

    public IDeliveryService CreateDeliveryService(bool fastDelivery)
    {
        return fastDelivery ? new FastDelivery() : new NormalDelivery();
    }
}

public class KoreanFoodFactory : IRestaurantFactory
{
    public Restaurant GetRestaurant() =>
        JsonDatabase.Instance.Data.Restaurants.First(r => r.Name == "Korean Food");

    public IDeliveryService CreateDeliveryService(bool fastDelivery)
    {
        return fastDelivery ? new FastDelivery() : new NormalDelivery();
    }
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
