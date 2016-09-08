using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.Models
{
    public class ChargeRecord
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public long CardID { get; set; }
        public long UserID { get; set; }
        public decimal Price { get; set; }

        public EnumOrderStatus CardOrderStatus { get; set; }
        public EnumPayStatus PaySatus { get; set; }

        public DateTime SubmitAt { get; set; }
        public DateTime PaiedAt { get; set; }
    }
}
