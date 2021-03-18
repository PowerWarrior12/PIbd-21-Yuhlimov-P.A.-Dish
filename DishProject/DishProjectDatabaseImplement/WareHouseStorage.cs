using DishProjectBusinessLogic.BindingModels;
using DishProjectBusinessLogic.Interfaces;
using DishProjectBusinessLogic.ViewModels;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DishProjectDatabaseImplement
{
    public class WareHouseStorage : IWareHouseStorage
    {
        public List<WareHouseViewModel> GetFullList()
        {
            using (var context = new DishProjectDatabase())
            {
                return context.WareHouses
                .Include(rec => rec.WareHouseComponents)
                .ThenInclude(rec => rec.Component)
                .ToList()
                .Select(rec => new WareHouseViewModel
                {
                    Id = rec.WareHouseId,
                    Name = rec.WareHouseName,
                    FIO = rec.FIO,
                    DateCreate = rec.DateCreate,
                    StoreComponents = rec.WareHouseComponents
                    .ToDictionary(recTC => recTC.ComponentId, recTC => (recTC.Component?.ComponentName, recTC.Count))
                })
                .ToList();
            }
        }
        public List<WareHouseViewModel> GetFilteredList(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new DishProjectDatabase())
            {
                return context.WareHouses
                .Include(rec => rec.WareHouseComponents)
                .ThenInclude(rec => rec.Component)
                .Where(rec => rec.WareHouseName.Contains(model.Name))
                .ToList()
                .Select(rec => new WareHouseViewModel
                {
                    Id = rec.WareHouseId,
                    Name = rec.WareHouseName,
                    FIO = rec.FIO,
                    DateCreate = model.DateCreate,
                    StoreComponents = rec.WareHouseComponents
                    .ToDictionary(recTC => recTC.ComponentId, recTC => (recTC.Component?.ComponentName, recTC.Count))
                })
                .ToList();
            }
        }
        public WareHouseViewModel GetElement(WareHouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new DishProjectDatabase())
            {
                var WareHouse = context.WareHouses
                .Include(rec => rec.WareHouseComponents)
                .ThenInclude(rec => rec.Component)
                .FirstOrDefault(rec => rec.WareHouseName == model.Name || rec.WareHouseId == model.Id);
                return WareHouse != null ?
                new WareHouseViewModel
                {
                    Id = WareHouse.WareHouseId,
                    Name = WareHouse.WareHouseName,
                    FIO = WareHouse.FIO,
                    DateCreate = model.DateCreate,
                    StoreComponents = WareHouse.WareHouseComponents
                    .ToDictionary(recTC => recTC.ComponentId, recTC => (recTC.Component?.ComponentName, recTC.Count))
                } :
                null;
            }
        }
        public void Insert(WareHouseBindingModel model)
        {
            using (var context = new DishProjectDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        WareHouse p = new WareHouse { WareHouseName = model.Name, FIO = model.FIO, DateCreate = model.DateCreate, };
                        context.WareHouses.Add(p);
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
        public void Update(WareHouseBindingModel model)
        {
            using (var context = new DishProjectDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.WareHouses.FirstOrDefault(rec => rec.WareHouseId == model.Id);
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
        public void Delete(WareHouseBindingModel model)
        {
            using (var context = new DishProjectDatabase())
            {
                WareHouse element = context.WareHouses.FirstOrDefault(rec => rec.WareHouseId == model.Id);
                if (element != null)
                {
                    context.WareHouses.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private WareHouse CreateModel(WareHouseBindingModel model, WareHouse storehouse, DishProjectDatabase context)
        {
            storehouse.WareHouseName = model.Name;
            storehouse.FIO = model.FIO;
            if (model.Id.HasValue)
            {
                var productComponents = context.WareHouseComponents.Where(rec => rec.WareHouseId == model.Id.Value).ToList();
                foreach (var updateComponent in productComponents)
                {
                    updateComponent.Count = model.StoreComponents[updateComponent.ComponentId].Item2;
                    model.StoreComponents.Remove(updateComponent.ComponentId);
                }
                // добавили новые
                foreach (var tc in model.StoreComponents)
                {
                    WareHouseComponent wc = new WareHouseComponent
                    {
                        WareHouse = storehouse,
                        WareHouseId = storehouse.WareHouseId,
                        ComponentId = tc.Key,
                        Count = tc.Value.Item2
                    };
                    if (context.WareHouseComponents.FirstOrDefault(ws => ws.Id == wc.Id) == null)
                        context.WareHouseComponents.Add(wc);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbUpdateException e)
                    {
                        throw new Exception("ERROR");
                    }
                }
            }
            return storehouse;
        }
        public void ChangeComponents(ChangeComponentBindingModel model)
        {
            using (var context = new DishProjectDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var c in model.Components)
                        {
                            int count = c.Value.Item2 * model.DishCount;
                            foreach (WareHouse w in context.WareHouses)
                            {
                                WareHouseComponent comp = context.WareHouseComponents.FirstOrDefault(wc => (wc.Component == (context.Components.FirstOrDefault(rec => rec.Id == c.Key)) && w.WareHouseId == wc.WareHouseId));
                                if (comp != null)
                                    count -= comp.Count;
                            }
                            if (count > 0)
                            {
                                throw new Exception("На складе нет необходимых компонентов");
                            }
                        }
                        foreach (var c in model.Components)
                        {
                            int needCount = c.Value.Item2 * model.DishCount;
                            foreach (var warehouse in context.WareHouses)
                            {
                                WareHouseComponent warec = context.WareHouseComponents.FirstOrDefault(wc => (wc.Component == (context.Components.FirstOrDefault(rec => rec.Id == c.Key)) && warehouse.WareHouseId == wc.WareHouseId));
                                if (warec != null)
                                {
                                    int warecount = warec.Count;
                                    if (warecount > needCount)
                                    {
                                        warec.Count -= needCount;
                                        break;
                                    }
                                    else
                                    {
                                        warec.Count = 0;
                                        needCount -= warecount;
                                    }
                                }
                            }
                        }
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw new Exception("На складе нет необходимых компонентов");
                    }
                }
            }
        }
    }
}
