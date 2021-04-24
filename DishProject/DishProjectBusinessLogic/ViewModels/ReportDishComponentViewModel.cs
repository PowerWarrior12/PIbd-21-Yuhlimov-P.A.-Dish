using System;
using System.Collections.Generic;

namespace DishProjectBusinessLogic.ViewModels
{
    public class ReportDishComponentViewModel
    {
        public string DishName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Components { get; set; }
    }
}
