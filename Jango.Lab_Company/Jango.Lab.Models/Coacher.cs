///////////////////////////////////////////////////////////
//  Coacher.cs
//  Implementation of the Class Coacher

//  Created on:      29-8��-2016 9:52:01
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
    public class Coacher
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public long ShopID { get; set; }

        [MaxLength(100)]
        public string Avatar { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedUser { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        public EnumCoachStatus Status { get; set; }

        [NotMapped]
        public ICollection<CourseInfo> Courses { get; set; }

    }//end Coacher

  

}//end namespace Jango.Lab.Models