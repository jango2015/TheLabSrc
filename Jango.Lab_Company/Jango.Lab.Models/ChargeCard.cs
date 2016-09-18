///////////////////////////////////////////////////////////
//  ChargeRecord.cs
//  Implementation of the Class ChargeRecord

//  Created on:      29-8ÔÂ-2016 9:52:01
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
    public class ChargeCard
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        [MaxLength(50)]
        public string CardNO { get; set; }
        public decimal Amount { get; set; }
        public decimal GiftIntegral { get; set; }
        public bool IsValid { get; set; }
        public decimal Price { get; set; }

        [MaxLength(500)]
        public string Remark { get; set; }


    }//end ChargeRecord

}//end namespace Jango.Lab.Models