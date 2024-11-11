﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public decimal Discount { get; set; }

        public ICollection<Automobile>? Automobiles{ get; set; }
        //public ICollection<Guid>? AutomobilesId { get; set; }
    }
}
