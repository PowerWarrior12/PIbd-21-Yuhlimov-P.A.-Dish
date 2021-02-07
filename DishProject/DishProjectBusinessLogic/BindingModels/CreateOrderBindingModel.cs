using System;
using System.Collections.Generic;
using System.Text;

namespace DishProjectBusinessLogic.BindingModels
{
    /// <summary>
    /// Данные от клиента, для создания заказа
    /// </summary>
    public class CreateOrderBindingModel
    {
        public int DishId { get; set; }
        public string DishName { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
