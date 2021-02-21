﻿using System;
using System.Collections.Generic;

namespace DishProjectBusinessLogic.BindingModels
{
    public class WareHouseBindingModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string FIO { get; set; }
        public DateTime DateCreate { get; set; }
        public Dictionary<int, (string, int)> StoreComponents { get; set; }
    }
}
