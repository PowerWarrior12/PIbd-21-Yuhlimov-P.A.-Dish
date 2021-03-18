using System.Collections.Generic;
using System;
using System.ComponentModel;

namespace DishProjectBusinessLogic.ViewModels
{
    public class WareHouseViewModel
    {
        public int? Id { get; set; }
        [DisplayName("Название склада")]
        public string Name { get; set; }
        [DisplayName("ФИО ответственного")]
        public string FIO { get; set; }
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
        public Dictionary<int, (string, int)> StoreComponents { get; set; }
    }
}
