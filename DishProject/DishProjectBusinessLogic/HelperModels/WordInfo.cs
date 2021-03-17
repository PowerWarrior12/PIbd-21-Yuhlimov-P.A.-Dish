using DishProjectBusinessLogic.ViewModels;
using System.Collections.Generic;


namespace DishProjectBusinessLogic.HelperModels
{
    class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ComponentViewModel> Components { get; set; }
    }
}
