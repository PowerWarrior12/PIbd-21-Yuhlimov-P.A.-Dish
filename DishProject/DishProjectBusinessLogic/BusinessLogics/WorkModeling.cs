using DishProjectBusinessLogic.BindingModels;
using DishProjectBusinessLogic.Interfaces;
using DishProjectBusinessLogic.ViewModels;
using DishProjectBusinessLogic.HelperModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;


namespace DishProjectBusinessLogic.BusinessLogics
{
    public class WorkModeling
    {
        private readonly IImplementerStorage _implementerStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly OrderLogic _orderLogic;
        private readonly DishLogic _dishLogic;
        private readonly Random rnd;
        public WorkModeling(IImplementerStorage implementerStorage, IOrderStorage
       orderStorage, OrderLogic orderLogic, DishLogic dishLogic)
        {
            this._implementerStorage = implementerStorage;
            this._orderStorage = orderStorage;
            this._orderLogic = orderLogic;
            this._dishLogic = dishLogic;
            rnd = new Random(1000);
        }
        /// <summary>
        /// Запуск работ
        /// </summary>
        public void DoWork()
        {
            var implementers = _implementerStorage.GetFullList();
            var orders = _orderStorage.GetFilteredList(new OrderBindingModel
            {
                FreeOrders = true
            }).OrderByDescending(rec=>rec.Status).ToList();
            foreach (var implementer in implementers)
            {
                WorkerWorkAsync(implementer, orders);
            }
        }
        /// <summary>
        /// Иммитация работы исполнителя
        /// </summary>
        /// <param name="implementer"></param>
        /// <param name="orders"></param>
        private async void WorkerWorkAsync(ImplementerViewModel implementer,
       List<OrderViewModel> orders)
        {
            // ищем заказы, которые уже в работе (вдруг исполнителя прервали)
            var runOrders = await Task.Run(() => _orderStorage.GetFilteredList(new
           OrderBindingModel
            { ImplementerId = implementer.Id }));
            foreach (var order in runOrders)
            {
                // делаем работу заново
                Thread.Sleep(implementer.WorkingTime * rnd.Next(1, 5) * order.Count);
                _orderLogic.FinishOrder(new ChangeStatusBindingModel
                {
                    OrderId = order.Id
                });
                // отдыхаем
                Thread.Sleep(implementer.PauseTime);
            }
            await Task.Run(() =>
            {
                foreach (var order in orders)
                {
                    // пытаемся назначить заказ на исполнителя
                    try
                    {
                        _orderLogic.TakeOrderInWork(new ChangeStatusBindingModel
                        {
                            Components = _dishLogic.Read(null).Find(rec => rec.Id == order.DishId).DishComponents,
                            DishCount = _orderLogic.Read(null).Find(rec => rec.Id == order.Id).Count,
                            OrderId = order.Id,
                            ImplementerId = implementer.Id
                        });
                        // делаем работу
                        Thread.Sleep(implementer.WorkingTime * rnd.Next(1, 5) *
                        order.Count);
                        _orderLogic.FinishOrder(new ChangeStatusBindingModel
                        {
                            OrderId = order.Id
                        });
                        // отдыхаем
                        Thread.Sleep(implementer.PauseTime);
                    }
                    catch (LackingComponentsException)
                    {
                        try
                        {
                            _orderLogic.TakeOrderInRequiredComponents(new ChangeStatusBindingModel
                            {
                                OrderId = order.Id
                            });
                        }
                        catch (Exception) { }
                    }
                    catch (Exception) { }
                }
            });
        }
    }
}
