using System;
using System.Collections.Generic;
using System.Text;

namespace DishProjectBusinessLogic.BindingModels
{
    public class AddComponentBindingModel
    {
        public string WareHoseName { get; set; }
        public int ComponentName { get; set; }
        public int Count { get; set; }
    }
}
