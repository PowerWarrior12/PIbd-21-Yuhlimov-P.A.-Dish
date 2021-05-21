﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishProjectDatabaseImplement
{
    public class Implementer
    {
        public int Id { get; set; }
        public string ImplementerFIO { get; set; }
        public int WorkingTime { get; set; }
        public int PauseTime { get; set; }
        [ForeignKey("ImplementerId")]
        public List<Order> Order { get; set; }
    }
}
