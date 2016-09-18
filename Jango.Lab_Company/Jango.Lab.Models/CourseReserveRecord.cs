///////////////////////////////////////////////////////////
//  CourseReserveRecord.cs
//  Implementation of the Class CourseReserveRecord

//  Created on:      29-8月-2016 9:52:01
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
    /// 课程预约记录
    /// </summary>
    public class CourseReserveRecord
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public long UserID { get; set; }
        public long CourseID { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsQRCodeUsded { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedUser { get; set; }

        [MaxLength(50)]
        public string QRCode { get; set; }
        public DateTime ReserveTime { get; set; }
        public EnumCourseReserveStatus Status { get; set; }


    }//end CourseReserveRecord

}//end namespace Jango.Lab.Models