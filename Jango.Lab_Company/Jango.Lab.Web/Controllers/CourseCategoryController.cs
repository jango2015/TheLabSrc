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
    public class CourseCategoryController : Controller
    {
        private readonly ICourseInfoService _courseInfoSrv;
        public CourseCategoryController(
            ICourseInfoService courseInfoSrv
            )
        {
            _courseInfoSrv = courseInfoSrv;
        }
        // GET: CourseCategory
        public ActionResult Index(CourseCategoryQuery query)
        {
            var categories = _courseInfoSrv.GetCourseCategoryList(query);
            return View(categories);
        }

        public ActionResult Edit(long id = 0)
        {
            var model = _courseInfoSrv.GetById(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(CourseCategory model)
        {
            try
            {

                _courseInfoSrv.Save(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}