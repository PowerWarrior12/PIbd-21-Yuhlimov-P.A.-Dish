using System;
using System.Collections.Generic;
using System.Text;

namespace DishProjectBusinessLogic.Enums
{
    /// <summary>
    /// Статус заказа
    /// </summary>
    public enum OrderStatus
    {
        Принят = 0,
        ТребуютсяМатериалы = 1,
        Выполняется = 2,
        Готов = 3,
        Оплачен = 4
    }
}
