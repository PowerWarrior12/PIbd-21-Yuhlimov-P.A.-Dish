using System;
using System.Collections.Generic;
using System.Text;

namespace DishProjectBusinessLogic.BindingModels
{
    public class AddComponentBindingModel
    {
        public int Id { get; set; }
        public string ComponentName { get; set; }
        public int Count { get; set; }
    }
}
