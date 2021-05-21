using System.ComponentModel;
using DishProjectBusinessLogic.Attributes;

namespace DishProjectBusinessLogic.ViewModels
{
    /// <summary>
    /// Исполнитель, выполняющий заказы
    /// </summary>
    public class ImplementerViewModel
    {
        [Column(title: "Номер", width: 100)]
        public int Id { get; set; }
        [Column(title: "ФИО исполнителя", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DisplayName("ФИО исполнителя")]
        public string ImplementerFIO { get; set; }
        [Column(title: "Время на заказ", width: 50)]
        [DisplayName("Время на заказ")]
        public int WorkingTime { get; set; }
        [DisplayName("Время на перерыв")]
        [Column(title: "Время на перерыв", width: 50)]
        public int PauseTime { get; set; }
    }
}
