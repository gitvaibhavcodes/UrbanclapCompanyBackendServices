﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Dtos
{
    public class CustomerReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Location { get; set; }
        
    }
}
