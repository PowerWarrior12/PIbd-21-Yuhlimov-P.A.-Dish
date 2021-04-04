using System;
using System.Collections.Generic;
using System.Text;

namespace DishProjectBusinessLogic.ViewModels
{
    public class ReportWareHouseComponentViewModel
    {
        public string WareHouseName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Components { get; set; }
    }
}
