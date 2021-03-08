using DishProjectBusinessLogic.BindingModels;
using DishProjectBusinessLogic.Interfaces;
using DishProjectBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DishProjectListImplement.Models
{
    public class WareHouseStorage : IWareHouseStorage
    {
        private readonly DataListSingleton source;
        public WareHouseStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public void ChangeComponents(ChangeComponentBindingModel model)
        {
            throw new Exception("Элемент не найден");
        }

        public void Delete(WareHouseBindingModel model)
        {
            for (int i = 0; i < source.WareHouses.Count; ++i)
            {
                if (source.WareHouses[i].Id == model.Id)
                {
                    source.WareHouses.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public WareHouseViewModel GetElement(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var wareHouse in source.WareHouses)
            {
                if (wareHouse.Id == model.Id || wareHouse.Name == model.Name)
                {
                    return CreateModel(wareHouse);
                }
            }
            return null;
        }

        public List<WareHouseViewModel> GetFilteredList(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<WareHouseViewModel> result = new List<WareHouseViewModel>();
            foreach (var wareHouse in source.WareHouses)
            {
                if (wareHouse.Name.Contains(model.Name))
                {
                    result.Add(CreateModel(wareHouse));
                }
            }
            return result;
        }

        public List<WareHouseViewModel> GetFullList()
        {
            List<WareHouseViewModel> result = new List<WareHouseViewModel>();
            foreach (var wareHouse in source.WareHouses)
            {
                result.Add(CreateModel(wareHouse));
            }
            return result;
        }

        public void Insert(WareHouseBindingModel model)
        {
            WareHouse tempWareHouse = new WareHouse
            {
                Id = 1,
                StoreComponents = new Dictionary<int, int>()
            };
            foreach (var wareHouse in source.WareHouses)
            {
                if (wareHouse.Id >= tempWareHouse.Id)
                {
                    tempWareHouse.Id = wareHouse.Id + 1;
                }
            }
            source.WareHouses.Add(CreateModel(model, tempWareHouse));
        }

        public void Update(WareHouseBindingModel model)
        {
            WareHouse tempWareHouse = null;
            foreach (var wareHouse in source.WareHouses)
            {
                if (wareHouse.Id == model.Id)
                {
                    tempWareHouse = wareHouse;
                }
            }
            if (tempWareHouse == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempWareHouse);
        }
        private WareHouse CreateModel(WareHouseBindingModel model, WareHouse wareHouse)
        {
            wareHouse.Name = model.Name;
            wareHouse.FIO = model.FIO;
            wareHouse.DateCreate = model.DateCreate;
            wareHouse.StoreComponents = wareHouse.StoreComponents;
            return wareHouse;
        }
        private WareHouseViewModel CreateModel(WareHouse wareHouse)
        {
            // требуется дополнительно получить список компонентов для изделия с названиями и их количество
            Dictionary<int, (string, int)> wareHouseComponents = new Dictionary<int, (string, int)>();
            foreach (var pc in wareHouse.StoreComponents)
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
                wareHouseComponents.Add(pc.Key, (componentName, pc.Value));
            }
            return new WareHouseViewModel
            {
                Id = wareHouse.Id,
                Name = wareHouse.Name,
                FIO = wareHouse.FIO,
                DateCreate = wareHouse.DateCreate,
                StoreComponents = wareHouseComponents
            };
        }
    }
}
