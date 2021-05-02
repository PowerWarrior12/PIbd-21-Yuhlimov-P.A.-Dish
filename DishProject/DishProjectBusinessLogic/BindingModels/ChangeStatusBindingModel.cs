using System;
using System.Collections.Generic;
using System.Text;

namespace DishProjectBusinessLogic.BindingModels
{
    /// <summary>
    /// Данные для смены статуса заказа
    /// </summary>
    public class ChangeStatusBindingModel
    {
        public int OrderId { get; set; }
        public Dictionary<int, (string, int)> Components { get; set; }
        public int DishCount { get; set; }
        public int? ImplementerId { get; set; }
    }
}
