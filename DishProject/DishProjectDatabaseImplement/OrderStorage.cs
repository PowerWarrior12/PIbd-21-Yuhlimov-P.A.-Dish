using DishProjectBusinessLogic.BindingModels;
using DishProjectBusinessLogic.Interfaces;
using DishProjectBusinessLogic.ViewModels;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DishProjectDatabaseImplement
{
    public class OrderStorage : IOrderStorage
    {
        public void Delete(OrderBindingModel model)
        {
            using (var context = new DishProjectDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Orders.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new DishProjectDatabase())
            {
                var order = context.Orders.Include(rec => rec.Dish).Include(rec => rec.Client).Include(rec => rec.Implementer)
                .FirstOrDefault(rec => rec.Id == model.Id);
                return order != null ?
                new OrderViewModel
                {
                    Id = order.Id,
                    DishId = order.DishId,
                    ClientFIO = order.Client.ClientFIO,
                    ImplementerId = order.ImplementerId,
                    DishName = order.Dish.DishName,
                    ImplementerFIO = order.ImplementerId.HasValue ? order.Implementer.ImplementerFIO : string.Empty,
                    Count = order.Count,
                    Sum = order.Summ,
                    Status = order.Status,
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement,
                    ClientId = order.ClientId
                } :
                null;
            }
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new DishProjectDatabase())
            {
                return context.Orders.Include(rec => rec.Dish).Include(rec => rec.Client).Include(rec => rec.Implementer)
                .Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue &&
                rec.DateCreate.Date == model.DateCreate.Date) ||
                (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate.Date
                >= model.DateFrom.Value.Date && rec.DateCreate.Date <= model.DateTo.Value.Date) ||
                (model.ClientId.HasValue && rec.ClientId == model.ClientId))
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    DishId = rec.DishId,
                    ImplementerId = rec.ImplementerId,
                    ClientFIO = rec.Client.ClientFIO,
                    ImplementerFIO = rec.ImplementerId.HasValue ? rec.Implementer.ImplementerFIO : string.Empty,
                    DishName = rec.Dish.DishName,
                    Count = rec.Count,
                    Sum = rec.Summ,
                    Status = rec.Status,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                })
                .ToList();
            }
        }

        public List<OrderViewModel> GetFullList()
        {
            using (var context = new DishProjectDatabase())
            {
                return context.Orders.Include(rec => rec.Dish).Include(rec => rec.Client).Include(rec => rec.Implementer)
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    DishId = rec.DishId,
                    ImplementerId = rec.ImplementerId,
                    ClientFIO = rec.Client.ClientFIO,
                    DishName = rec.Dish.DishName,
                    ImplementerFIO = rec.ImplementerId.HasValue ? rec.Implementer.ImplementerFIO : string.Empty,
                    Count = rec.Count,
                    Sum = rec.Summ,
                    Status = rec.Status,
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                    ClientId = rec.ClientId
                })
                .ToList();
            }
        }

        public void Insert(OrderBindingModel model)
        {
            using (var context = new DishProjectDatabase())
            {
                Order order = new Order
                {
                    DishId = model.DishId,
                    Count = model.Count,
                    Summ = model.Sum,
                    Status = model.Status,
                    DateCreate = model.DateCreate,
                    DateImplement = model.DateImplement,
                    ClientId = (int)model.ClientId,
                    ImplementerId = model.ImplementerId
                };
                context.Orders.Add(order);
                context.SaveChanges();
                CreateModel(model, order, context);
                context.SaveChanges();
            }
        }

        public void Update(OrderBindingModel model)
        {
            using (var context = new DishProjectDatabase())
            {
                var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                element.DishId = model.DishId;
                element.Count = model.Count;
                element.Summ = model.Sum;
                element.Status = model.Status;
                element.DateCreate = model.DateCreate;
                element.DateImplement = model.DateImplement;
                element.ClientId = (int)model.ClientId;
                element.ImplementerId = (int)model.ImplementerId;
                CreateModel(model, element, context);
                context.SaveChanges();
            }
        }
        private Order CreateModel(OrderBindingModel model, Order order,
       DishProjectDatabase context)
        {
            if (model == null)
            {
                return null;
            }
            Dish element = context.Dishes.Include(rec => rec.Orders).FirstOrDefault(rec => rec.Id == model.DishId);
            if (element != null)
            {
                if (element.Orders == null)
                {
                    element.Orders = new List<Order>();
                }
                element.Orders.Add(order);
                context.Dishes.Update(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
            return order;
        }
    }
}
