﻿using Jango.Lib.CastleWindsor.MVC.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.Models.Query
{
    public class CourseCategoryQuery : PageQuery
    {
        public override int PageSize
        {
            get
            {
                return base.PageSize;
            }

            set
            {
                base.PageSize = 10;
            }
        }
    }
}
