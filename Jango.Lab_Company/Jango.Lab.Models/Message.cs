///////////////////////////////////////////////////////////
//  Message.cs
//  Implementation of the Class Message

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
    public class Message
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }


        [MaxLength(50)]
        public string OpenID { get; set; }
        [MaxLength(500)]
        public string Content { get; set; }
        public EnumMessageType MsgType { get; set; }
        public DateTime SendTime { get; set; }
        public int Status { get; set; }
        public long UserID { get; set; }

    }//end Message

}//end namespace Jango.Lab.Models