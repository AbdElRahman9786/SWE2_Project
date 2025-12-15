using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public interface IOrderComponent
    {
        string GetDescription();
        decimal GetPrice();
    }

    public class BaseOrder : IOrderComponent
    {
        private readonly MenuItem _menuItem;

        public BaseOrder(MenuItem menuItem)
        {
            _menuItem = menuItem;
        }

        public string GetDescription()
        {
            return _menuItem.Name;
        }

        public decimal GetPrice()
        {
            return _menuItem.Price;
        }
    }

    public abstract class OrderDecorator : IOrderComponent
    {
        protected IOrderComponent _orderComponent;

        public OrderDecorator(IOrderComponent orderComponent)
        {
            _orderComponent = orderComponent;
        }

        public virtual string GetDescription()
        {
            return _orderComponent.GetDescription();
        }

        public virtual decimal GetPrice()
        {
            return _orderComponent.GetPrice();
        }
    }

    public class CustomizationDecorator : OrderDecorator
    {
        private readonly Customization _customization;

        public CustomizationDecorator(IOrderComponent orderComponent, Customization customization)
            : base(orderComponent)
        {
            _customization = customization;
        }

        public override string GetDescription()
        {
            return $"{_orderComponent.GetDescription()} + {_customization.Name}";
        }

        public override decimal GetPrice()
        {
            return _orderComponent.GetPrice() + _customization.Price;
        }
    }
}
