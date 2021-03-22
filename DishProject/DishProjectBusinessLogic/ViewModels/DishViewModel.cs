using System.Collections.Generic;
using System.ComponentModel;


namespace DishProjectBusinessLogic.ViewModels
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    public class DishViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название изделия")]
        public string DishName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> DishComponents { get; set; }
    }
}
