﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.User
{
    public class CreateUser
    {
        public string Name { get; set; }

        public string Username { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int CurrencyId { get; set; }

        public Guid UserGroupId { get; set; } = Guid.Empty;
    }
}
