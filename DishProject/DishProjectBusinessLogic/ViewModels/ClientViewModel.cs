using System.Runtime.Serialization;
using DishProjectBusinessLogic.Attributes;

namespace DishProjectBusinessLogic.ViewModels
{
    [DataContract]
    public class ClientViewModel
    {
        [DataMember]
        [Column(title: "Номер", width: 100)]
        public int? Id { get; set; }
        [DataMember]
        [Column(title: "Клиент", width: 150)]
        public string ClientFIO { get; set; }
        [Column(title: "Логин", width: 100)]
        [DataMember]
        public string Email { get; set; }
        [Column(title: "Пароль", width: 100)]
        [DataMember]
        public string Password { get; set; }
    }
}
