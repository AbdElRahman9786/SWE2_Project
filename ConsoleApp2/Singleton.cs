using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public sealed class JsonDatabase
{

    private static JsonDatabase _instance;
    private static readonly object _lock = new object();

    private readonly string _filePath;
    public RootData Data { get; private set; } = new RootData();

    private JsonDatabase()
    {
        _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.json");
        Load();
    }

    public static JsonDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new JsonDatabase();
                }
            }
            return _instance;
        }
    }

    private void Load()
    {
        if (!File.Exists(_filePath))
        {
            Console.WriteLine("data.json not found. Creating new file...");
            Data = new RootData();
            Save();
            return;
        }

        try
        {
            string json = File.ReadAllText(_filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            Data = JsonSerializer.Deserialize<RootData>(json, options)
                   ?? new RootData();

            Data.Restaurants ??= new();
            Data.Orders ??= new();

            Console.WriteLine("Database loaded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JSON corrupted: {ex.Message}");
            Console.WriteLine("Resetting database...");
            Data = new RootData();
            Save();
        }
    }

    public void Save()
    {
        lock (_lock)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(Data, options);
                File.WriteAllText(_filePath, json);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save database: {ex.Message}");
            }
        }
    }
}



public class RootData
{
    public List<Restaurant> Restaurants { get; set; } = new();
    public List<Order> Orders { get; set; } = new();
}


public class Restaurant
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "";
    public string Type { get; set; } = "";
    public List<MenuItem> Menu { get; set; } = new();
    public List<Customization> Customizations { get; set; } = new();
}


public class MenuItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
}


public class Customization
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public List<string> ApplicableItems { get; set; } = new();
}


public class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string RestaurantId { get; set; } = "";
    public List<OrderItem> Items { get; set; } = new();
    public decimal Total { get; set; }
}


public class OrderItem
{
    public string ItemId { get; set; } = "";
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public List<Customization> Customizations { get; set; } = new();
}
