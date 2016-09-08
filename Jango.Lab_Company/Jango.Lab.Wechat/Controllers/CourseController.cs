using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jango.Lab.Services;

namespace Jango.Lab.Wechat.Controllers
{
    public class CourseController : BaseController
    {
        private readonly ICourseInfoService _courseInfoSrv;
        public CourseController(IUserService userSrv, ICourseInfoService courseInfoSrv) : base(userSrv)
        {
            _courseInfoSrv = courseInfoSrv;
        }

        // GET: Course
        public ActionResult Index(string code)
        {
            Code = code;
            ViewBag.Code = Code;
            return View();
        }

        public ActionResult GetCourseList(string code)
        {
            try
            {
                var courseList = _courseInfoSrv.GetCourseList(new ViewModels.Query.CourseQuery() { PageSize = int.MaxValue });

                var groupCourseList = from c in courseList
                                      group c by c.CourseBeginTime.ToString("yyyy-MM-dd") into g
                                      let b = g.ToList()
                                      select new
                                      {
                                          date = g.Key,
                                          week = DateTime.Parse(g.Key).DayOfWeek,
                                          items = b
                                      };
                //
                //courseReserveList
                return Json(new { success = true, data = new { CourseItems = groupCourseList, CourseReserveList = new { } } }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}