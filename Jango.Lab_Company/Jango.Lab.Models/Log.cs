///////////////////////////////////////////////////////////
//  Log.cs
//  Implementation of the Class Log

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
    public class Log
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [MaxLength(50)]
        public string Action { get; set; }
        public DateTime CreatedAt { get; set; }

        [MaxLength(4000)]
        public string Message { get; set; }
        [MaxLength(100)]
        public string Operator { get; set; }
        public long UserID { get; set; }

    }//end Log

}//end namespace Jango.Lab.Models