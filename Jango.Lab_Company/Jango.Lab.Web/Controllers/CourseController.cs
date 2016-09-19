using Jango.Lab.Models;
using Jango.Lab.Services;
using Jango.Lab.ViewModels.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jango.Lab.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseInfoService _courseSrv = LoadServices._CourseInfoService;
        private readonly ICoacherService _coacherSrv = LoadServices._CoacherService;

        public ActionResult Index(CourseQuery query)
        {
            var items = _courseSrv.GetCourseList(query);
            return View(items);
        }


        public ActionResult Edit(long id = 0)
        {
            var model = _courseSrv.GetCourseById(id);
            var categories = _courseSrv.GetCourseCategoryList(new CourseCategoryQuery() { PageSize = int.MaxValue });
            ViewBag.Coachers = _coacherSrv.GetAllList(new CoacherQuery() { PageSize = int.MaxValue }).Where(x => x.Status == EnumCoachStatus.On);
            ViewBag.categories = categories;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CourseInfo model)
        {
            try
            {
                _courseSrv.SaveCourse(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
