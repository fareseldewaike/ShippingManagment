﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.core.Models
{
    public class Rejection
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool isDeleted { get; set; } = false;
    }
}
