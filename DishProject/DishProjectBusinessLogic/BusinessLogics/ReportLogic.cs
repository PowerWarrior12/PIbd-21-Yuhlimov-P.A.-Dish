using DishProjectBusinessLogic.BindingModels;
using DishProjectBusinessLogic.HelperModels;
using DishProjectBusinessLogic.Interfaces;
using DishProjectBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DishProjectBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IComponentStorage _componentStorage;
        private readonly IDishStorage _dishStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly IWareHouseStorage _wareHouseStorage;
        public ReportLogic(IDishStorage dishStorage, IComponentStorage
       componentStorage, IOrderStorage orderStorage, IWareHouseStorage wareHouseStorage)
        {
            _dishStorage = dishStorage;
            _componentStorage = componentStorage;
            _orderStorage = orderStorage;
            _wareHouseStorage = wareHouseStorage;
        }
        /// <summary>
        /// Получение списка складов с указанием, какие компоненты используются
        /// </summary>
        /// <returns></returns>
        public List<ReportWareHouseComponentViewModel> GetComponentsWareHouse()
        {
            var components = _componentStorage.GetFullList();
            var wareHouses = _wareHouseStorage.GetFullList();
            var list = new List<ReportWareHouseComponentViewModel>();
            foreach (var wareHouse in wareHouses)
            {
                var record = new ReportWareHouseComponentViewModel
                {
                    WareHouseName = wareHouse.Name,
                    Components = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in wareHouse.StoreComponents)
                {
                    if (wareHouse.StoreComponents.ContainsKey(component.Key))
                    {
                        record.Components.Add(new Tuple<string, int>(component.Value.Item1, wareHouse.StoreComponents[component.Key].Item2));
                        record.TotalCount += wareHouse.StoreComponents[component.Key].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFullList().GroupBy(x => x.DateCreate.Date).Select(g => new ReportOrdersViewModel
            {
                DateCreate = g.FirstOrDefault().DateCreate.Date,
                Count = g.Count(),
                Sum = g.Sum(e => e.Sum)
            }).ToList();
        }
        /// <summary>
        /// Сохранение изделий в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveDishesToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список изделий",
                Dishes = _dishStorage.GetFullList()
            });
        }
        /// <summary>
        /// Сохранение складов в файл Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveWareHousesToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список изделий",
                WareHouses = _wareHouseStorage.GetFullList()
            }); ;
        }

        /// <summary>
        /// Сохранение изделий с указанием компонент в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveComponentWareHouseToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список изделий",
                ComponentsDish = GetComponentsWareHouse()
            });
        }
        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }
    }
}
