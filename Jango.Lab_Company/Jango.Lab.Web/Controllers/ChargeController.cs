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
    public class ChargeController : Controller
    {
        private readonly IChargeService _chargeSrv = LoadServices._ChargeService;

        public ActionResult Index(ChargeQuery query)
        {
            var items = _chargeSrv.GetAllChargeCardList(query);
            return View(items);
        }

        public ActionResult Edit(long id = 0)
        {
            var item = _chargeSrv.GetById(id);
            if (item.ID == 0)
            {
                item.CardNO = _chargeSrv.GetCardNo("CZ");
            }
            return View(item);
        }
        [HttpPost]
        public ActionResult Edit(ChargeCard model)
        {
            try
            {
                _chargeSrv.Save(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Edit", model);
                //throw ex;
                //return View();
            }
        }
    }
}