using Jango.Lab.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.Services
{
    public static class LoadServices
    {
        public static IUserService _UserService = new Lazy<UserService>().Value;
        public static IChargeService _ChargeService = new Lazy<ChargeService>().Value;
        public static ICoacherService _CoacherService = new Lazy<CoacherService>().Value;
        public static ICourseInfoService _CourseInfoService = new Lazy<CourseInfoService>().Value;
        public static ICourseReserveService _CourseReserveService = new CourseReserveService();
        public static ILogService _LogService = new Lazy<LogService>().Value;
        public static IMessageService _MessageService = new Lazy<MessageService>().Value;
        public static IOrderService _OrderService = new Lazy<OrderService>().Value;


    }
}
