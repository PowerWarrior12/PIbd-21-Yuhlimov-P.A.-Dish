using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DishProjectDatabaseImplement
{
    public class WareHouse
    {
        public int WareHouseId { get; set; }
        [Required]
        public string WareHouseName { get; set; }
        [Required]
        public string FIO { get; set; }
        public DateTime DateCreate { get; set; }
        [ForeignKey("WareHouseId")]
        public virtual List<WareHouseComponent> WareHouseComponents { get; set; }
    }
}
