using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jango.Lab.Repositories.Lab;

namespace Jango.Lab.Repositories
{
    public static class LoadReps
    {
        public static ILabUow _uow = new Lazy<LabUow>().Value;
        public static IUserRep _userRep = new Lazy<UserRep>().Value;
        public static IUserAccountRep _userAccountRep = new Lazy<UserAccountRep>().Value;
        public static IUserConsigneeInfoRep _userConsigneeInfoRep = new Lazy<UserConsigneeInfoRep>().Value;
        public static ICourseCategoryRep _courseCategoryRep = new Lazy<CourseCategoryRep>().Value;
        public static ICourseCoacherRep _courseCoacherRep = new Lazy<CourseCoacherRep>().Value;
        public static ICourseInfoRep _courseInfoRep = new Lazy<CourseInfoRep>().Value;
        public static ICourseReserveRecordRep _courseReserveRecordRep = new Lazy<CourseReserveRecordRep>().Value;
        public static ICourseSignInRecordRep _courseSignInRecordRep = new Lazy<CourseSignInRecordRep>().Value;
        public static ICourseTellerRep _courseTellerRep = new Lazy<CourseTellerRep>().Value;
        public static ICoacherRep _coacherRep = new Lazy<CoacherRep>().Value;
        public static IChargeCardRep _chargeCardRep = new Lazy<ChargeCardRep>().Value;
        public static IChargeRecordRep _chargeRecordRep = new Lazy<ChargeRecordRep>().Value;
        public static ILogRep _logRep = new Lazy<LogRep>().Value;
        public static IOrderRep _orderRep = new Lazy<OrderRep>().Value;
        public static IMessageRep _messageRep = new Lazy<MessageRep>().Value;
    }
}
