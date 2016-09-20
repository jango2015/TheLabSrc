using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.ViewModels.ViewModel
{
    public class UserAccountVM
    {
        public long ID { get; set; }
        public long UserID { get; set; }
        public int AccountType { get; set; }
        public decimal Amount { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
    }
}
