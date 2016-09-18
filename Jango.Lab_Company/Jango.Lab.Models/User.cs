///////////////////////////////////////////////////////////
//  User.cs
//  Implementation of the Class User

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
    /// <summary>
    /// »áÔ±
    /// </summary>
    public class User
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime CreatedAt { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        public EnumUserLevel Level { get; set; }
        [MaxLength(50)]
        public string Mobile { get; set; }
        public DateTime ModifiedAt { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string OpenID { get; set; }
        [MaxLength(50)]
        public string Code { get; set; }
        [NotMapped]
        public UserAccount[] m_UserAccounts { get; set; }
        [NotMapped]
        public UserConsigneeInfo m_UserConsigneeInfo { get; set; }
        //public EnumUserLevel m_EnumUserLevel { get; set; }
        [NotMapped]
        public ChargeCard m_ChargeRecord { get; set; }
        [NotMapped]
        public CourseReserveRecord m_CourseReserveRecord { get; set; }


    }//end User

}//end namespace Jango.Lab.Models