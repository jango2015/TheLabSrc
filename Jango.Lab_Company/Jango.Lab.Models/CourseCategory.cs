﻿///////////////////////////////////////////////////////////
//  CourseInfo.cs
//  Implementation of the Class CourseInfo

//  Created on:      29-8月-2016 9:52:01
//  Original author: Jango
///////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.Models
{
    public class CourseCategory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Remark { get; set; }
        public DateTime CreatedAt { get; set; }
        [MaxLength(50)]
        public string Creator { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedUser { get; set; }
    }
}
