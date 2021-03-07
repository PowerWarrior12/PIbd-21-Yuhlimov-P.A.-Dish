using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DishProjectDatabaseImplement
{
    public class Dish
    {
        public int Id { get; set; }
        [Required]
        public string DishName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ForeignKey("DishId")]
        public virtual List<DishComponent> DishComponents { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
