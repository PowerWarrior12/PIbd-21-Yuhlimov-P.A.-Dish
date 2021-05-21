using DishProjectBusinessLogic.BindingModels;
using DishProjectBusinessLogic.Interfaces;
using DishProjectBusinessLogic.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DishProjectDatabaseImplement
{
    public class DishStorage : IDishStorage
    {
        public List<DishViewModel> GetFullList()
        {
            using (var context = new DishProjectDatabase())
            {
                return context.Dishes
                .Include(rec => rec.DishComponents)
               .ThenInclude(rec => rec.Component)
               .ToList()
               .Select(rec => new DishViewModel
               {
                   Id = rec.Id,
                   DishName = rec.DishName,
                   Price = rec.Price,
                   DishComponents = rec.DishComponents
                .ToDictionary(recPC => recPC.ComponentId, recPC =>
               (recPC.Component?.ComponentName, recPC.Count))
               })
               .ToList();
            }
        }
        public List<DishViewModel> GetFilteredList(DishBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new DishProjectDatabase())
            {
                return context.Dishes
                .Include(rec => rec.DishComponents)
               .ThenInclude(rec => rec.Component)
               .Where(rec => rec.DishName.Contains(model.DishName))
               .ToList()
               .Select(rec => new DishViewModel
               {
                   Id = rec.Id,
                   DishName = rec.DishName,
                   Price = rec.Price,
                   DishComponents = rec.DishComponents
                .ToDictionary(recPC => recPC.ComponentId, recPC =>

                (recPC.Component?.ComponentName, recPC.Count))
               })
.ToList();
            }
        }
        public DishViewModel GetElement(DishBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new DishProjectDatabase())
            {
                var product = context.Dishes.Include(rec => rec.DishComponents).ThenInclude(rec => rec.Component).FirstOrDefault(rec => rec.DishName == model.DishName || rec.Id == model.Id);
                return product != null ?
                new DishViewModel
                {
                    Id = product.Id,
                    DishName = product.DishName,
                    Price = product.Price,
                    DishComponents = product.DishComponents
                .ToDictionary(recPC => recPC.ComponentId, recPC =>
               (recPC.Component?.ComponentName, recPC.Count))
                } :
               null;
            }
        }
        public void Insert(DishBindingModel model)
        {
            using (var context = new DishProjectDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Dish p = new Dish
                        {
                            DishName = model.DishName,
                            Price = model.Price
                        };
                        context.Dishes.Add(p);
                        context.SaveChanges();
                        CreateModel(model, p, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Update(DishBindingModel model)
        {
            using (var context = new DishProjectDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Dishes.FirstOrDefault(rec => rec.Id ==
                       model.Id);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        CreateModel(model, element, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(DishBindingModel model)
        {
            using (var context = new DishProjectDatabase())
            {
                Dish element = context.Dishes.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Dishes.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Dish CreateModel(DishBindingModel model, Dish dish,
       DishProjectDatabase context)
        {
            dish.DishName = model.DishName;
            dish.Price = model.Price;
            if (model.Id.HasValue)
            {
                var productComponents = context.DishComponents.Where(rec =>
               rec.DishId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.DishComponents.RemoveRange(productComponents.Where(rec =>
               !model.DishComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateComponent in productComponents)
                {
                    updateComponent.Count = model.DishComponents[updateComponent.ComponentId].Item2;
                    model.DishComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var pc in model.DishComponents)
            {
                context.DishComponents.Add(new DishComponent
                {
                    DishId = dish.Id,
                    ComponentId = pc.Key,
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }
            return dish;
        }
    }
}
