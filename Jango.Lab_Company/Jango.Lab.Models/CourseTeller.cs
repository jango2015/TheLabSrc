///////////////////////////////////////////////////////////
//  CourseTeller.cs
//  Implementation of the Class CourseTeller

//  Created on:      29-8��-2016 9:52:01
//  Original author: Jango
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jango.Lab.Models {
	public class CourseTeller {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID{ get; set; }
		public long CourseID{ get; set; }
        public long UserID { get; set; }
        [MaxLength(50)]
        public string QRCode { get; set; }
        public DateTime CreatedAt{ get; set; }
		public bool IsValid{ get; set; }


	}//end CourseTeller

}//end namespace Jango.Lab.Models