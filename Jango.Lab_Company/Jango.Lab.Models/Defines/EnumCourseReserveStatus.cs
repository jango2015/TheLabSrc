///////////////////////////////////////////////////////////
//  EnumCourseReserveStatus.cs
//  Implementation of the Enumeration EnumCourseReserveStatus

//  Created on:      29-8月-2016 9:52:01
//  Original author: Jango
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



namespace Jango.Lab.Models {
	public enum EnumCourseReserveStatus  {

		/// <summary>
		/// 待预约
		/// </summary>
		ToReserve,
		/// <summary>
		/// 已预约
		/// </summary>
		HasReserved,
		/// <summary>
		/// 已错过
		/// </summary>
		Passed

	}//end EnumCourseReserveStatus

}//end namespace Jango.Lab.Models