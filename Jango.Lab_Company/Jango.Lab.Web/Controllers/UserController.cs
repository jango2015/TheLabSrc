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
    public class UserController : Controller
    {
        private readonly IUserService _userSrv = LoadServices._UserService;

        public ActionResult Index(UserQuery query)
        {
            var users = _userSrv.GetUserList(query);
            return View(users);
        }

        public ActionResult Edit(long id = 0)
        {
            var model = _userSrv.GetById(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(User model)
        {
            try
            {
                _userSrv.Save(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}