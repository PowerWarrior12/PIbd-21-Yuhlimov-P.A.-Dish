using DishProjectBusinessLogic.BindingModels;
using DishProjectBusinessLogic.Interfaces;
using DishProjectBusinessLogic.ViewModels;
using DishProjectFileImplement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DishProjectFileImplement
{
    public class DishStorage : IDishStorage
    {
        private readonly FileDataListSingleton source;
        public DishStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<DishViewModel> GetFullList()
        {
            return source.Dishes
            .Select(CreateModel)
            .ToList();
        }
        public List<DishViewModel> GetFilteredList(DishBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Dishes
            .Where(rec => rec.DishName.Contains(model.DishName))
            .Select(CreateModel)
            .ToList();
        }
        public DishViewModel GetElement(DishBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var product = source.Dishes
            .FirstOrDefault(rec => rec.DishName == model.DishName || rec.Id
           == model.Id);
            return product != null ? CreateModel(product) : null;
        }
        public void Insert(DishBindingModel model)
        {
            int maxId = source.Dishes.Count > 0 ? source.Dishes.Max(rec => rec.Id)
: 0;
            var element = new Dish
            {
                Id = maxId + 1,
                DishComponents = new
           Dictionary<int, int>()
            };
            source.Dishes.Add(CreateModel(model, element));
        }
        public void Update(DishBindingModel model)
        {
            var element = source.Dishes.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }
        public void Delete(DishBindingModel model)
        {
            Dish element = source.Dishes.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Dishes.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private Dish CreateModel(DishBindingModel model, Dish product)
        {
            product.DishName = model.DishName;
            product.Price = model.Price;
            // удаляем убранные
            foreach (var key in product.DishComponents.Keys.ToList())
            {
                if (!model.DishComponents.ContainsKey(key))
                {
                    product.DishComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var component in model.DishComponents)
            {
                if (product.DishComponents.ContainsKey(component.Key))
                {
                    product.DishComponents[component.Key] =
                   model.DishComponents[component.Key].Item2;
                }
                else
                {
                    product.DishComponents.Add(component.Key,
                   model.DishComponents[component.Key].Item2);
                }
            }
            return product;
        }
        private DishViewModel CreateModel(Dish product)
        {
            return new DishViewModel
            {
                Id = product.Id,
                DishName = product.DishName,
                Price = product.Price,
                ProductComponents = product.DishComponents
 .ToDictionary(recPC => recPC.Key, recPC =>
 (source.Components.FirstOrDefault(recC => recC.Id ==
recPC.Key)?.ComponentName, recPC.Value))
            };
        }
    }
}
