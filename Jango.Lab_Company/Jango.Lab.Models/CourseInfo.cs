///////////////////////////////////////////////////////////
//  CourseInfo.cs
//  Implementation of the Class CourseInfo

//  Created on:      29-8��-2016 9:52:01
//  Original author: Jango
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



using Jango.Lab.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Jango.Lab.Models
{
    public class CourseInfo
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(1000)]
        public string Desc { get; set; }
        public EnumCourseType m_EnumCourseType { get; set; }
        public decimal IntegralUse { get; set; }
        public decimal BalanceUse { get; set; }
        public EnumCourseType CourseType { get; set; }

        [NotMapped]
        public CourseCategory m_CourseCategory { get; set; }
        [NotMapped]
        public Coacher m_Coacher { get; set; }
        [NotMapped]
        public long CoacherID { get; set; }
        public long m_CourseCategoryId { get; set; }
        public DateTime CourseBeginTime { get; set; }
        public DateTime CourseEndTime { get; set; }
        [NotMapped]
        public bool IsReserved { get; set; }
    }//end CourseInfo

}//end namespace Jango.Lab.Models