using System.Collections.Generic;

namespace DishProjectBusinessLogic.ViewModels
{
    public class ReportWareHouseViewModel
    {
        public string WareHouseName { get; set; }
        public Dictionary<int, (string, int)> StoreComponents { get; set; }
    }
}
