///////////////////////////////////////////////////////////
//  OrderItem.cs
//  Implementation of the Class OrderItem

//  Created on:      29-8ÔÂ-2016 9:52:02
//  Original author: Jango
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jango.Lab.Models {
	public class OrderItem {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long ID{ get; set; }
		public long OrderID{ get; set; }
		public long ProductID{ get; set; }
        public int ProductType { get; set; }
        public string ProductName{ get; set; }
		public decimal ProductPrice{ get; set; }

        [MaxLength(500)]
        public string ProductRemark{ get; set; }
        
	}//end OrderItem

}//end namespace Jango.Lab.Models