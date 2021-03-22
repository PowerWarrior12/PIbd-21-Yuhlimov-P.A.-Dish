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
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        public List<ReportDishComponentViewModel> GetDishComponent()
        {
            var components = _componentStorage.GetFullList();
            var dishes = _dishStorage.GetFullList();
            var list = new List<ReportDishComponentViewModel>();
            foreach (var component in components)
            {
                var record = new ReportDishComponentViewModel
                {
                    ComponentName = component.ComponentName,
                    Dishes = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var dish in dishes)
                {
                    if (dish.DishComponents.ContainsKey(component.Id))
                    {
                        record.Dishes.Add(new Tuple<string, int>(dish.DishName,
                       dish.DishComponents[component.Id].Item2));
                        record.TotalCount +=
                       dish.DishComponents[component.Id].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }
        /// <summary>
        /// Получение списка изделий с указанием, какие компоненты используются
        /// </summary>
        /// <returns></returns>
        public List<ReportDishComponentViewModel> GetComponentsDish()
        {
            var components = _componentStorage.GetFullList();
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

                foreach (var component in components)
                {
                    if (dish.DishComponents.ContainsKey(component.Id))
                    {
                        record.Components.Add(new Tuple<string, int>(component.ComponentName, dish.DishComponents[component.Id].Item2));
                        record.TotalCount += dish.DishComponents[component.Id].Item2;
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
            return _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom =
           model.DateFrom,
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
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                Components = _componentStorage.GetFullList()
            });
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
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveDishComponentToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                DishComponents = GetDishComponent()
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
