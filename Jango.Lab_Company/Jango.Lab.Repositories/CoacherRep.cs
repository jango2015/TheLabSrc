using Jango.Lab.Models;
using Jango.Lab.Repositories.Lab;
using Jango.Lib.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.Repositories
{
    public class CoacherRep : LabBaseRepository<Coacher>, ICoacherRep
    {
        public CoacherRep(ILabDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
    public interface ICoacherRep : IRepository<Coacher>, ILabBaseRepository { }
}
