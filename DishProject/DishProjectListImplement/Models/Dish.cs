using System.Collections.Generic;

namespace DishProjectListImplement
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    class Dish
    {
        public int Id { get; set; }
        public string DishName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, int> DishComponents { get; set; }
    }
}
