﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.ViewModels.Query
{
    public class CourseQuery : PageQuery
    {
       public DateTime? SearchDate { get; set; }
    }
}
