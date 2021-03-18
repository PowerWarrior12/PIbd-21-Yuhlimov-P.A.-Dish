using System;
using System.Collections.Generic;
using System.Text;

namespace DishProjectBusinessLogic.BindingModels
{
    public class AddComponentBindingModel
    {
        public int WareHouseId { get; set; }
        public int Count { get; set; }
        public int ComponentId { get; set; }
    }
}
