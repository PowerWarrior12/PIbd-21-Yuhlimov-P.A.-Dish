using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DishProjectBusinessLogic.Enums;
using System;

namespace DishProjectDatabaseImplement
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public decimal Summ { get; set; }
        public int DishId { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
        [Required]
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
        public virtual Dish Dish{ get; set; }

    }
}
