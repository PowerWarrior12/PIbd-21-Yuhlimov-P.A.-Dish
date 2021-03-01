using DishProjectBusinessLogic.BindingModels;
using DishProjectBusinessLogic.Interfaces;
using DishProjectBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DishProjectListImplement
{
    public class DishStorage : IDishStorage
    {
        private readonly DataListSingleton source;
        public DishStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<DishViewModel> GetFullList()
        {
            List<DishViewModel> result = new List<DishViewModel>();
            foreach (var component in source.Dishes)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }

        public List<DishViewModel> GetFilteredList(DishBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<DishViewModel> result = new List<DishViewModel>();
            foreach (var dish in source.Dishes)
            {
                if (dish.DishName.Contains(model.DishName))
                {
                    result.Add(CreateModel(dish));
                }
            }
            return result;
        }
        public DishViewModel GetElement(DishBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var dish in source.Dishes)
            {
                if (dish.Id == model.Id || dish.DishName ==
                model.DishName)
                {
                    return CreateModel(dish);
                }
            }
            return null;
        }
        public void Insert(DishBindingModel model)
        {
            Dish tempDish = new Dish
            {
                Id = 1,
                DishComponents = new
            Dictionary<int, int>()
            };
            foreach (var dish in source.Dishes)
            {
                if (dish.Id >= tempDish.Id)
                {
                    tempDish.Id = dish.Id + 1;
                }
            }
            source.Dishes.Add(CreateModel(model, tempDish));
        }
        public void Update(DishBindingModel model)
        {
            Dish tempDish = null;
            foreach (var dish in source.Dishes)
            {
                if (dish.Id == model.Id)
                {
                    tempDish = dish;
                }
            }
            if (tempDish == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempDish);
        }
        public void Delete(DishBindingModel model)
        {
            for (int i = 0; i < source.Dishes.Count; ++i)
            {
                if (source.Dishes[i].Id == model.Id)
                {
                    source.Dishes.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Dish CreateModel(DishBindingModel model, Dish dish)
        {
            dish.DishName = model.DishName;
            dish.Price = model.Price;
            // удаляем убранные
            foreach (var key in dish.DishComponents.Keys.ToList())
            {
                if (!model.DishComponents.ContainsKey(key))
                {
                    dish.DishComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var component in model.DishComponents)
            {
                if (dish.DishComponents.ContainsKey(component.Key))
                {
                    dish.DishComponents[component.Key] =
                    model.DishComponents[component.Key].Item2;
                }
                else
                {
                    dish.DishComponents.Add(component.Key,
                    model.DishComponents[component.Key].Item2);
                }
            }
            return dish;
        }
        private DishViewModel CreateModel(Dish dish)
        {
            // требуется дополнительно получить список компонентов для изделия с названиями и их количество
            Dictionary<int, (string, int)> dishComponents = new
            Dictionary<int, (string, int)>();
            foreach (var pc in dish.DishComponents)
            {
                string componentName = string.Empty;
                foreach (var component in source.Components)
                {
                    if (pc.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }
                }
                dishComponents.Add(pc.Key, (componentName, pc.Value));
            }
            return new DishViewModel
            {
                Id = dish.Id,
                DishName = dish.DishName,
                Price = dish.Price,
                DishComponents = dishComponents
            };
        }
    }
}