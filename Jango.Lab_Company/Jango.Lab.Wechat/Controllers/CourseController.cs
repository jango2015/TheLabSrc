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
        private readonly ICoacherService _coacherSrv;
        public CourseController(IUserService userSrv, ICourseInfoService courseInfoSrv,
            ICoacherService coacherSrv,
            ICourseReserveService courseReserveSrv) : base(userSrv)
        {
            _courseInfoSrv = courseInfoSrv;
            _coacherSrv = coacherSrv;
            _courseReserveSrv = courseReserveSrv;
        }

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
                Code = code;
                LoadMemberInfo();
                var courseReserveList = _courseReserveSrv.GetCourseReserveListByUserId(_user.ID);

                var groupCourseList = from c in courseList
                                      group c by c.CourseBeginTime.ToString("yyyy-MM-dd") into g
                                      let b = g.ToList()
                                      select new
                                      {
                                          date = g.Key,
                                          week = DateTime.Parse(g.Key).DayOfWeek,
                                          items = from a in b
                                                  select new
                                                  {
                                                      BalanceUse = a.BalanceUse,
                                                      CoacherID = a.CoacherID,
                                                      ID = a.ID,
                                                      IntegralUse = a.IntegralUse,
                                                      CourseBeginTimeStr = a.CourseBeginTime.ToString("HH:mm"),
                                                      CourseEndTimeStr = a.CourseEndTime.ToString("HH:mm"),
                                                      CourseType = a.CourseType,
                                                      Title = a.Title,
                                                      Desc = a.Desc,
                                                      IsReserved = courseReserveList.Select(x => x.CourseID == a.ID).Any(),
                                                      m_Coacher = _coacherSrv.GetById(a.CoacherID)
                                                  }


                                      };
                //
                //courseReserveList

                return Json(new { success = true, data = new { CourseItems = groupCourseList } }, JsonRequestBehavior.AllowGet);
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

        public ActionResult GetReserveCourses(string code)
        {
            try
            {
                Code = code;
                LoadMemberInfo();
                var courseReserveList = _courseReserveSrv.GetCourseReserveListByUserId(_user.ID);
                var cids = courseReserveList.Select(x => x.CourseID).ToList();
                var courseList = _courseInfoSrv.GetCoursesByIds(cids);

                var groupCourseList = from c in (from c in courseList
                                                 join e in courseReserveList
                                                 on c.ID equals e.CourseID
                                                 select new
                                                 {
                                                     ID = c.ID,
                                                     Title = c.Title,
                                                     Desc = c.Desc,
                                                     QrCode = e.QRCode,
                                                     CourseBeginTime = c.CourseBeginTime,
                                                     CourseEndTime = c.CourseEndTime,
                                                     CourseType = c.CourseType
                                                 })
                                      group c by c.CourseBeginTime.ToString("yyyy-MM-dd") into g
                                      let b = g.ToList()
                                      select new
                                      {
                                          date = g.Key,
                                          week = DateTime.Parse(g.Key).DayOfWeek,
                                          items = from a in b
                                                  select new
                                                  {
                                                      ID = a.ID,
                                                      CourseType = a.CourseType,
                                                      Title = a.Title,
                                                      Desc = a.Desc,
                                                      CourseBeginTimeStr = a.CourseBeginTime.ToString("HH:mm"),
                                                      CourseEndTimeStr = a.CourseEndTime.ToString("HH:mm"),
                                                  }


                                      };
                return Json(new { success = true, data = new { CourseItems = groupCourseList } }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}