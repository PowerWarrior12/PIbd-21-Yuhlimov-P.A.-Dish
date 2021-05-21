using DishProjectBusinessLogic.Enums;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using DishProjectBusinessLogic.Attributes;


namespace DishProjectBusinessLogic.ViewModels
{
    [DataContract]
    public class OrderViewModel
    {
        [Column(title: "Номер", width: 100)]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ClientId { get; set; }
        [DataMember]
        public int? ImplementerId { get; set; }
        [DataMember]
        public int DishId { get; set; }
        [DataMember]
        [DisplayName("Клиент")]
        [Column(title: "Клиент", width: 150)]
        public string ClientFIO { get; set; }
        [DataMember]
        [DisplayName("Исполнитель")]
        [Column(title: "Исполнитель", width: 150)]
        public string ImplementerFIO { get; set; }
        [DataMember]
        [DisplayName("Изделие")]
        [Column(title: "Изделие", width:100)]
        public string DishName { get; set; }
        [DataMember]
        [DisplayName("Количество")]
        [Column(title: "Количество", width: 100)]
        public int Count { get; set; }
        [DataMember]
        [DisplayName("Сумма")]
        [Column(title: "Сумма", width: 50)]
        public decimal Sum { get; set; }
        [DataMember]
        [DisplayName("Статус")]
        [Column(title: "Статус", width: 100)]
        public OrderStatus Status { get; set; }
        [DataMember]
        [DisplayName("Дата создания")]
        [Column(title: "Дата создания", width: 100 , format:FormatsEnum.SecondFormat)]
        public DateTime DateCreate { get; set; }
        [DisplayName("Дата выполнения")]
        [DataMember]
        [Column(title: "Дата выполнения", width: 100)]
        public DateTime? DateImplement { get; set; }

    }
}