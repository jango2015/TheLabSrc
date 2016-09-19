
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.ViewModels.Query
{
    public class CourseReserveQuery : PageQuery
    {

    }

    public abstract class PageQuery
    {
        public int PageNumber
        {
            get { return id; }
        }

        public int PageSize = 10;

        public int id { get; set; }
    }
}
