using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DishProjectBusinessLogic.ViewModels
{
    [DataContract]
    public class ReportDishComponentViewModel
    {
        [DataMember]
        public string DishName { get; set; }
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public List<Tuple<string, int>> Components { get; set; }
    }
}
