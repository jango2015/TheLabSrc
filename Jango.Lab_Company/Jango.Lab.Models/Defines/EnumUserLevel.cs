///////////////////////////////////////////////////////////
//  EnumUserLevel.cs
//  Implementation of the Enumeration EnumUserLevel

//  Created on:      29-8月-2016 9:52:02
//  Original author: Jango
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



namespace Jango.Lab.Models
{
    /// <summary>
    /// 会员等级
    /// </summary>
    public enum EnumUserLevel : int
    {

        /// <summary>
        /// 次卡
        /// </summary>
        CountCard,
        /// <summary>
        /// 月卡
        /// </summary>
        MonthCard,
        /// <summary>
        /// 全通次卡
        /// </summary>
        AllCountCard,
        /// <summary>
        /// 全通月卡
        /// </summary>
        AllMonthCard

    }//end EnumUserLevel

}//end namespace Jango.Lab.Models