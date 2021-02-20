using DishProjectBusinessLogic.Enums;
using System;

namespace DishProjectFileImplement
{
    class Order
    {
        public int Id { get; set; }
        public int DishId { get; set; }
        public string DishName { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
    }
}
