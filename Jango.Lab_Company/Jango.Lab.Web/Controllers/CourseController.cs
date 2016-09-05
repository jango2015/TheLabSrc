using Jango.Lab.Models;
using Jango.Lab.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jango.Lab.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseInfoService _courseSrv;
        private readonly ICoacherService _coacherSrv;
        public CourseController(ICourseInfoService courseSrv, ICoacherService coacherSrv)
        {
            _courseSrv = courseSrv;
            _coacherSrv = coacherSrv;
        }
        // GET: Course
        public ActionResult Index()
        {
            var items = _courseSrv.GetCourseList();
            return View(items);
        }


        // GET: Course/Edit/5
        public ActionResult Edit(long id = 0)
        {
            var model = _courseSrv.GetCourseById(id);
            var categories = _courseSrv.GetCourseCategoryList();
            ViewBag.Coachers = _coacherSrv.GetAllList().Where(x => x.Status == EnumCoachStatus.On);
            ViewBag.categories = categories;
            return View(model);
        }

        // POST: Course/Edit/5
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

        // GET: Course/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Course/Delete/5
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
