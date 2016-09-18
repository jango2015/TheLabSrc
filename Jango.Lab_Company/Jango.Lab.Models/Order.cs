///////////////////////////////////////////////////////////
//  Order.cs
//  Implementation of the Class Order

//  Created on:      29-8ÔÂ-2016 9:52:02
//  Original author: Jango
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jango.Lab.Models
{
    public partial class Order
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        [MaxLength(50)]
        public string OrderNo { get; set; }
        /// <summary>
        /// transaction_id
        /// </summary>
	    public string TradeId { get; set; }
        public EnumDispatchStatus DispatchStatus { get; set; }
        public DateTime ModifiedAt { get; set; }
        [MaxLength(100)]
        public string ModifiedUser { get; set; }
        public decimal OrderPrice { get; set; }
        public EnumOrderStatus OrderStatus { get; set; }
        public DateTime PaidAt { get; set; }
        public EnumPayStatus PayStatus { get; set; }
        public EnumPayTerms PayTerm { get; set; }
        public DateTime SubmitAt { get; set; }
        public long UserID { get; set; }
        [NotMapped]
        public OrderItem[] m_OrderItem { get; set; }

        public DateTime CreatedAt { get; set; }

        [MaxLength(100)]
        public string CreatedUser { get; set; }

    }//end Order

}//end namespace Jango.Lab.Models