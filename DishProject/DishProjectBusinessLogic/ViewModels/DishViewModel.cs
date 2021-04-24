using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace DishProjectBusinessLogic.ViewModels
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    [DataContract]
    public class DishViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("Название изделия")]
        public string DishName { get; set; }
        [DataMember]
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        [DataMember]
        public Dictionary<int, (string, int)> DishComponents { get; set; }
    }
}
