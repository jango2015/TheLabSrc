using AutoMapper;
using Jango.Lab.Models;
using Jango.Lab.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jango.Lab.Wechat
{
    public class MapperStrapper
    {
        public static void ConfigureAutoMapper()
        {
            Mapper.Initialize(x => x.CreateMap<User, MemberVM>());
        }



    }
}