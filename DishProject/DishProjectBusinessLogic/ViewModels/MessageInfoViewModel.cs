using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using DishProjectBusinessLogic.Attributes;

namespace DishProjectBusinessLogic.ViewModels
{
    [DataContract]
    public class MessageInfoViewModel
    {
        [DataMember]
        [Column(title: "Номер", width: 100)]
        public string MessageId { get; set; }
        [DisplayName("Отправитель")]
        [DataMember]
        [Column(title: "Отправитель", width: 100)]
        public string SenderName { get; set; }
        [DisplayName("Дата письма")]
        [Column(title: "Дата письма", width: 100)]
        [DataMember]
        public DateTime DateDelivery { get; set; }
        [DisplayName("Заголовок")]
        [DataMember]
        [Column(title: "Заголовок", width: 150)]
        public string Subject { get; set; }
        [DisplayName("Текст")]
        [Column(title: "Текст", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string Body { get; set; }
    }
}
