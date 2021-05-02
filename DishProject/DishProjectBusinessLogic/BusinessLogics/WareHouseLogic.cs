using DishProjectBusinessLogic.BindingModels;
using DishProjectBusinessLogic.Interfaces;
using DishProjectBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace DishProjectBusinessLogic.BusinessLogics
{
    public class WareHouseLogic
    {
        private readonly IWareHouseStorage _wareHouseStorage;
        public WareHouseLogic(IWareHouseStorage wareHouseStorage)
        {
            _wareHouseStorage = wareHouseStorage;
        }
        public List<WareHouseViewModel> Read(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return _wareHouseStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<WareHouseViewModel> { _wareHouseStorage.GetElement(model) };
            }
            return _wareHouseStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(WareHouseBindingModel model)
        {
            var element = _wareHouseStorage.GetElement(new WareHouseBindingModel
            {
                Name = model.Name
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            if (model.Id.HasValue)
            {
                _wareHouseStorage.Update(model);
            }
            else
            {
                _wareHouseStorage.Insert(model);
            }
        }
        public void AddNewComponent(AddComponentBindingModel model)
        {
            var wareHouse = _wareHouseStorage.GetElement(new WareHouseBindingModel
            {
                Id = model.WareHouseId
            });
            if (wareHouse == null)
            {
                throw new Exception("Не найден склад");
            }
            else 
            {
                if (wareHouse.StoreComponents.ContainsKey(model.ComponentId))
                    wareHouse.StoreComponents[model.ComponentId] =
                        ("", wareHouse.StoreComponents[model.ComponentId].Item2 + model.Count);
                else
                    wareHouse.StoreComponents.Add(model.ComponentId, ("", model.Count));
            }
            _wareHouseStorage.Update(new WareHouseBindingModel
            {
                Id = model.WareHouseId,
                Name = wareHouse.Name,
                FIO = wareHouse.FIO,
                DateCreate = wareHouse.DateCreate,
                StoreComponents = wareHouse.StoreComponents
            });
        }
        public void Delete(WareHouseBindingModel model)
        {
            var element = _wareHouseStorage.GetElement(new WareHouseBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _wareHouseStorage.Delete(model);
        }
    }
}
