using System;
using System.Collections.Generic;
using System.Text;

namespace DishProjectBusinessLogic.BindingModels
{
    public class ChangeComponentBindingModel
    {
        public Dictionary<int, (string, int)> Components { get; set; }
        public int DishCount { get; set; }
    }
}
