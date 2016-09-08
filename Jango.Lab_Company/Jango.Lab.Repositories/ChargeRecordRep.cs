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
    public class ChargeRecordRep : LabBaseRepository<ChargeCard>, IChargeRecordRep
    {
        public ChargeRecordRep(ILabDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
    public interface IChargeRecordRep : IRepository<ChargeCard>, ILabBaseRepository { }
}
