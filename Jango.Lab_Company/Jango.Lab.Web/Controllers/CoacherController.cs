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
    public class CoacherController : Controller
    {
        private readonly ICoacherService _coacherSrv;
        public CoacherController(ICoacherService coacherSrv)
        {
            _coacherSrv = coacherSrv;
        }
        public ActionResult Index(CoacherQuery query)
        {
            var items = _coacherSrv.GetAllList(query);
            return View(items);
        }

        public ActionResult Edit(long id = 0)
        {
            var item = _coacherSrv.GetById(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Coacher model)
        {
            try
            {
                _coacherSrv.Save(model);
                return RedirectToAction("Index");
            }
            catch
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
