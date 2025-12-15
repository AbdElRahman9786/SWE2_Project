using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    public class FoodOrderingFacade
    {
        private readonly JsonDatabase _db;

        public FoodOrderingFacade()
        {
            _db = JsonDatabase.Instance;
        }

        // Abstract Factory
        public Restaurant GetRestaurant(string restaurantName)
        {
            var factory = RestaurantFactoryProvider.GetFactory(restaurantName);
            return factory.GetRestaurant();
        }

        // Factory
        public List<MenuItem> GetMenu(string restaurantName)
        {
            var menuFactory = MenuFactoryProvider.GetMenuFactory(restaurantName);
            return menuFactory.GetMenu();
        }

        // Decorator
        public IOrderComponent CreateOrder(MenuItem item, List<Customization> customizations)
        {
            IOrderComponent order = new BaseOrder(item);

            foreach (var customization in customizations)
            {
                order = new CustomizationDecorator(order, customization);
            }

            return order;
        }

        // Singleton + Save Order
        public void PlaceOrder(string restaurantId, List<IOrderComponent> orders)
        {
            var order = new Order
            {
                RestaurantId = restaurantId,
                Items = new List<OrderItem>(),
                Total = orders.Sum(o => o.GetPrice())
            };

            foreach (var o in orders)
            {
                order.Items.Add(new OrderItem
                {
                    Name = o.GetDescription(),
                    Price = o.GetPrice()
                });
            }

            _db.Data.Orders.Add(order);
            _db.Save();
        }
    }
}
