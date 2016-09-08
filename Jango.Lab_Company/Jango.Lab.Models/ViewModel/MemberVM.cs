using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.Models.ViewModel
{
    public class MemberVM
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public DateTime Birthday { get; set; }
        public int ProvindId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public string Address { get; set; }
    }
}
