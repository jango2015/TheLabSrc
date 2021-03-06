﻿using AutoMapper;
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
        private IUserService _userSrv;
        protected User _user;
        public BaseController(IUserService userSrv)
        {
            _userSrv = userSrv;
        }
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
                MemberInfo = model;
                _user = user;
                this.Code = MemberInfo != null ? MemberInfo.Code : this.Code;
            }
            MemberInfo = MemberInfo == null ? new MemberVM() : MemberInfo;
        }
    }
}