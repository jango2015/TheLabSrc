﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.ViewModels
{
    public class MemberVM
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Code { get; set; }
        public decimal Integral { get; set; }
        public decimal Balance { get; set; }
        public DateTime Birthday { get; set; }
        public string BirthdayStr { get { return Birthday.ToString("yyyy-MM-dd"); } }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
    }
}
