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
    public class ChargeCardRep : LabBaseRepository<ChargeCard>, IChargeCardRep
    {
        public ChargeCardRep(ILabDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
    public interface IChargeCardRep : IRepository<ChargeCard>, ILabBaseRepository { }
}
