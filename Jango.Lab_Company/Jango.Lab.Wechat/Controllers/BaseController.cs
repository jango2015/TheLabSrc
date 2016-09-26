using AutoMapper;
using Jango.Lab.Models;
using Jango.Lab.Services;
using Jango.Lab.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jango.Lab.Wechat.Controllers
{
    public class BaseController : Controller
    {
        public string Code = string.Empty;
        public MemberVM MemberInfo;
        public readonly IUserService _userSrv = LoadServices._UserService;
        protected User _user;
        protected void LoadMemberInfo()
        {
            if (null == MemberInfo)
            {
                if (string.IsNullOrEmpty(Code))
                {
                    throw new ArgumentNullException("code");
                }
                var user = _userSrv.GetByCode(this.Code);

                var model = Mapper.Map<MemberVM>(user);
                MemberInfo = model; if (user != null || user.ID > 0)
                {
                    var acounts = _userSrv.GetAccountsByUserId(user.ID);
                    if (MemberInfo != null)
                    {
                        var its = acounts.Where(a => a.AccountType == (int)EnumAccountType.Integral);
                        var bls = acounts.Where(a => a.AccountType == (int)EnumAccountType.Balance);
                        MemberInfo.Integral = its.Any() ? its.Sum(x => x.Amount) : 0;
                        MemberInfo.Balance = bls.Any() ? bls.Sum(x => x.Amount) : 0;
                    }
                }
                _user = user;
                this.Code = MemberInfo != null ? MemberInfo.Code : this.Code;
            }
            MemberInfo = MemberInfo == null ? new MemberVM() : MemberInfo;
        }
    }
}