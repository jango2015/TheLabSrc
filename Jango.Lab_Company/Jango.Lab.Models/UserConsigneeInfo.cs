///////////////////////////////////////////////////////////
//  UserConsigneeInfo.cs
//  Implementation of the Class UserConsigneeInfo

//  Created on:      29-8��-2016 9:52:03
//  Original author: Jango
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Jango.Lab.Models
{
    public class UserConsigneeInfo
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public long UserID { get; set; }

        [MaxLength(100)]
        public string ConsigneeUserName { get; set; }

        [MaxLength(50)]
        public string ConsigneeUserMobile { get; set; }
        [MaxLength(200)]
        public string ConsigneeUserAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsValid { get; set; }
        public DateTime ModifiedAt { get; set; }

    }//end UserConsigneeInfo

}//end namespace Jango.Lab.Models