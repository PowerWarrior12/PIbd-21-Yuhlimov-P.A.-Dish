using System;
using System.Collections.Generic;

namespace DishProjectListImplement.Models
{
    class WareHouse
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string FIO { get; set; }
        public DateTime DateCreate { get; set; }
        public Dictionary<int, int> StoreComponents { get; set; }
    }
}
