﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Expenses
{
    public class CreateExpense
    {
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int CategoryId { get; set; }
    }
}
