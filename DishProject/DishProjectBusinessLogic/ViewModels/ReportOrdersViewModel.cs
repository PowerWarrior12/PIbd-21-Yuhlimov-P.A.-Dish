using System;
using DishProjectBusinessLogic.Enums;

namespace DishProjectBusinessLogic.ViewModels
{
    public class ReportOrdersViewModel
    {
        public DateTime DateCreate { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
