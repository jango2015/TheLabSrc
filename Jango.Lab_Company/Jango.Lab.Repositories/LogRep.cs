using Jango.Lab.Models;

using Jango.Lab.Repositories.Lab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.Repositories
{
    public class LogRep : BaseRepository<Log>,ILogRep
    {
    }
    public interface ILogRep : IBaseRepository<Log> { }
}
