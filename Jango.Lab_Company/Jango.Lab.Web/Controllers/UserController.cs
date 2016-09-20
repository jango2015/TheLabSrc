using Jango.Lab.Models;
using Jango.Lab.Services;
using Jango.Lab.ViewModels.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jango.Lab.ViewModels.ViewModel;

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

        public ActionResult AccountIndex(UserAccountQuery query)
        {
            var users = _userSrv.GetAccounts(query);
            return View(users);
        }

        public ActionResult AccountEdit(long id = 0)
        {
            var model = _userSrv.GetAccountVmById(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult AccountEdit(UserAccountVM model)
        {
            try
            {
                _userSrv.Save(model);
                return RedirectToAction("AccountIndex");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}