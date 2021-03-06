///////////////////////////////////////////////////////////
//  CourseSignInRecord.cs
//  Implementation of the Class CourseSignInRecord

//  Created on:      29-8��-2016 9:52:01
//  Original author: Jango
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Jango.Lab.Models {
	public class CourseSignInRecord {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID{ get; set; }
		public long CourseID{ get; set; }
        public long UserID { get; set; }
        public long CourseTellerID{ get; set; }
		public DateTime SignInTime{ get; set; }

	}//end CourseSignInRecord

}//end namespace Jango.Lab.Models