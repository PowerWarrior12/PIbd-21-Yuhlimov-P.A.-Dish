using DishProjectBusinessLogic.BindingModels;
using DishProjectBusinessLogic.Interfaces;
using DishProjectBusinessLogic.ViewModels;
using DishProjectFileImplement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DishProjectFileImplement
{
    public class WareHouseStorage : IWareHouseStorage
    {
        private readonly FileDataListSingleton source;
        public WareHouseStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void ChangeComponents(ChangeComponentBindingModel model)
        {
            foreach (var c in model.Components)
            {
                int count = c.Value.Item2 * model.DishCount;
                foreach (WareHouse w in source.WareHouses)
                {
                    if (w.StoreComponents.ContainsKey(c.Key))
                        count -= w.StoreComponents[c.Key];
                }
                if (count > 0)
                {
                    throw new Exception("На складе нет необходимых компонентов");
                }
            }
            foreach (var c in model.Components)
            {
                int needCount = c.Value.Item2 * model.DishCount;
                foreach (var warehouse in source.WareHouses)
                {
                    if (warehouse.StoreComponents.ContainsKey(c.Key))
                    {
                        int warecount = warehouse.StoreComponents[c.Key];
                        if (warecount > needCount)
                        {
                            warehouse.StoreComponents[c.Key] -= needCount;
                            break;
                        }
                        else
                        {
                            warehouse.StoreComponents.Remove(c.Key);
                            needCount -= warecount;
                        }
                    }
                }
            }
        }

        public void Delete(WareHouseBindingModel model)
        {
            WareHouse element = source.WareHouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.WareHouses.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public WareHouseViewModel GetElement(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var product = source.WareHouses
            .FirstOrDefault(rec => rec.Name == model.Name || rec.Id
           == model.Id);
            return product != null ? CreateModel(product) : null;
        }

        public List<WareHouseViewModel> GetFilteredList(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.WareHouses
            .Where(rec => rec.Name.Contains(model.Name))
            .Select(CreateModel)
            .ToList();
        }

        public List<WareHouseViewModel> GetFullList()
        {
            return source.WareHouses
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(WareHouseBindingModel model)
        {
            int maxId = source.WareHouses.Count > 0 ? source.WareHouses.Max(rec => rec.Id): 0;
            var element = new WareHouse
            {
                Id = maxId + 1,
                StoreComponents = new
           Dictionary<int, int>()
            };
            source.WareHouses.Add(CreateModel(model, element));
        }

        public void Update(WareHouseBindingModel model)
        {
            var element = source.WareHouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }

        private WareHouse CreateModel(WareHouseBindingModel model, WareHouse wareHouse)
        {
            wareHouse.Name = model.Name;
            wareHouse.FIO = model.FIO;
            wareHouse.DateCreate = model.DateCreate;
            wareHouse.StoreComponents = model.StoreComponents.ToDictionary(recPC => recPC.Key, recPC => recPC.Value.Item2);
            return wareHouse;
        }
        private WareHouseViewModel CreateModel(WareHouse wareHouse)
        {
            return new WareHouseViewModel
            {
                Id = wareHouse.Id,
                Name = wareHouse.Name,
                FIO = wareHouse.FIO,
                DateCreate = wareHouse.DateCreate,
                StoreComponents = wareHouse.StoreComponents.ToDictionary(recPC => recPC.Key, recPC =>
                 (source.Components.FirstOrDefault(recC => recC.Id ==
                recPC.Key)?.ComponentName, recPC.Value))
            };
        }
    }
}
