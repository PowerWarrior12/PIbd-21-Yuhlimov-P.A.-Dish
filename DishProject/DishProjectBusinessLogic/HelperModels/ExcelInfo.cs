using System.Collections.Generic;
using DishProjectBusinessLogic.ViewModels;

namespace DishProjectBusinessLogic.HelperModels
{
    class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportDishComponentViewModel> DishComponents { get; set; }
    }
}
