using DishProjectBusinessLogic.BindingModels;
using DishProjectBusinessLogic.Interfaces;
using DishProjectBusinessLogic.ViewModels;
using DishProjectFileImplement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DishProjectFileImplement
{
    public class OrderStorage :IOrderStorage
    {
        private readonly FileDataListSingleton source;
        public OrderStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<OrderViewModel> GetFullList()
        {
            return source.Orders
            .Select(CreateModel)
           .ToList();
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Orders
            .Where(rec => rec.Id == model.Id && rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo)
           .Select(CreateModel)
            .ToList();
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var order in source.Orders)
            {
                if (order.Id == model.Id)
                {
                    return CreateModel(order);
                }
            }
            return null;
        }
        public void Insert(OrderBindingModel model)
        {
            int maxId = source.Orders.Count > 0 ? source.Orders.Max(rec => rec.Id) : 0;
            var element = new Order { Id = maxId + 1 };
            Order order = CreateModel(model, element);
            source.Orders.Add(order);
        }
        public void Update(OrderBindingModel model)
        {
            var element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }
        public void Delete(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Orders.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.DishId = model.DishId;
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }
        private OrderViewModel CreateModel(Order order)
        {
            string dishName = "";
            foreach (Dish dish in source.Dishes)
            {
                if (order.DishId == dish.Id)
                {
                    dishName = dish.DishName;
                    break;
                }
            }
            return new OrderViewModel
            {
                Id = order.Id,
                DishId = order.DishId,
                DishName = dishName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };
        }
    }
}
