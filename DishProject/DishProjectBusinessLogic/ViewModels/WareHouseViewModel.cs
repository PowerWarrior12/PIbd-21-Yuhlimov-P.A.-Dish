using System.Collections.Generic;
using System;
using System.ComponentModel;
using DishProjectBusinessLogic.Attributes;

namespace DishProjectBusinessLogic.ViewModels
{
    public class WareHouseViewModel
    {
        [Column(title: "Номер", width: 100)]
        public int? Id { get; set; }
        [Column(title: "Название склада", width: 100)]
        [DisplayName("Название склада")]
        public string Name { get; set; }
        [Column(title: "ФИО ответственного", width: 100)]
        [DisplayName("ФИО ответственного")]
        public string FIO { get; set; }
        [Column(title: "Дата создания", width: 40,format:FormatsEnum.FirstFormat)]
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
        public Dictionary<int, (string, int)> StoreComponents { get; set; }
    }
}
