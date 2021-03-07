using System.ComponentModel.DataAnnotations;

namespace DishProjectDatabaseImplement
{
    /// <summary>
    /// Сколько компонентов, требуется при изготовлении изделия
    /// </summary>
    public class DishComponent
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public int ComponentId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Component Component { get; set; }
        public virtual Dish Dish { get; set; }
    }
}