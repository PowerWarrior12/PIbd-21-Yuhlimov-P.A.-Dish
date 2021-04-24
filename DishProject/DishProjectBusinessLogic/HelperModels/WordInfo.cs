using DishProjectBusinessLogic.ViewModels;
using System.Collections.Generic;


namespace DishProjectBusinessLogic.HelperModels
{
    class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<DishViewModel> Dishes { get; set; }
        public List<WareHouseViewModel> WareHouses { get; set; }
    }
}
