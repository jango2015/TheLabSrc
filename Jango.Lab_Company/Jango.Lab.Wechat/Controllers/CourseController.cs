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
        private readonly ICourseReserveService _courseReserveSrv;
        public CourseController(IUserService userSrv, ICourseInfoService courseInfoSrv,
            ICourseReserveService courseReserveSrv) : base(userSrv)
        {
            _courseInfoSrv = courseInfoSrv;
            _courseReserveSrv = courseReserveSrv;
        }

        // GET: Course
        public ActionResult Index(string code)
        {
            ViewBag.SearchDate = DateTime.Now.ToString("yyyy-MM-dd");
            Code = code;
            ViewBag.Code = Code;
            return View();
        }

        public ActionResult GetCourseList(string code, DateTime? searDate)
        {
            try
            {
                var courseList = _courseInfoSrv.GetCourseList(new ViewModels.Query.CourseQuery() { PageSize = int.MaxValue, SearchDate = searDate });

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
                LoadMemberInfo();
                var courseReserveList = _courseReserveSrv.GetCourseReserveListByUserId(_user.ID);

                return Json(new { success = true, data = new { CourseItems = groupCourseList, CourseReserveList = courseReserveList } }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ReserveCourse(string code, long courseId)
        {
            try
            {
                Code = code;
                LoadMemberInfo();
                _courseReserveSrv.ReserveCourse(_user.ID, courseId);

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}