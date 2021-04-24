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
        public ReportLogic(IDishStorage dishStorage, IComponentStorage
       componentStorage, IOrderStorage orderStorage)
        {
            _dishStorage = dishStorage;
            _componentStorage = componentStorage;
            _orderStorage = orderStorage;
        }
        /// <summary>
        /// Получение списка изделий с указанием, какие компоненты используются
        /// </summary>
        /// <returns></returns>
        public List<ReportDishComponentViewModel> GetComponentsDish()
        {
            var dishes = _dishStorage.GetFullList();
            var list = new List<ReportDishComponentViewModel>();
            foreach (var dish in dishes)
            {
                var record = new ReportDishComponentViewModel
                {
                    DishName = dish.DishName,
                    Components = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in dish.DishComponents)
                {
                    record.Components.Add(new Tuple<string, int>(component.Value.Item1, component.Value.Item2));
                    record.TotalCount += component.Value.Item2;
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
            return _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                DishName = x.DishName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status.ToString()
            })
           .ToList();
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
        /// Сохранение изделий с указанием компонент в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveComponentDishToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список изделий",
                ComponentsDish = GetComponentsDish()
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
