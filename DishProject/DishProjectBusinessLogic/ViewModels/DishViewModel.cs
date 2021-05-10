using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using DishProjectBusinessLogic.Attributes;

namespace DishProjectBusinessLogic.ViewModels
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    [DataContract]
    public class DishViewModel
    {
        [DataMember]
        [Column(title: "Номер", width: 100)]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("Название изделия")]
        [Column(title: "Название изделия", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string DishName { get; set; }
        [DataMember]
        [DisplayName("Цена")]
        [Column(title: "Цена", width: 50)]
        public decimal Price { get; set; }
        [DataMember]
        public Dictionary<int, (string, int)> DishComponents { get; set; }
    }
}
