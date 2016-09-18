///////////////////////////////////////////////////////////
//  UserAccount.cs
//  Implementation of the Class UserAccount

//  Created on:      29-8月-2016 9:52:02
//  Original author: Jango
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Jango.Lab.Models {
	/// <summary>
	/// 会员账户
	/// </summary>
	public class UserAccount {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID{ get; set; }
		public long UserID{ get; set; }
		public int AccountType{ get; set; }
		public decimal Amount{ get; set; }
		public DateTime CreatedAt{ get; set; }
		public DateTime ModifiedAt{ get; set; }

	}//end UserAccount

}//end namespace Jango.Lab.Models