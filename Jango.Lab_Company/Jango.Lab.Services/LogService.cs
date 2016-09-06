using Jango.Lab.Models;
using Jango.Lab.Repositories;
using Jango.Lab.Repositories.Lab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRep _logRep;
        private readonly ILabUow _uow;
        public LogService(ILogRep logRep, ILabUow uow)
        {
            _logRep = logRep;
            _uow = uow;
        }

        public void Add(Log log)
        {
            _logRep.Add(log);
            _uow.Commit();
        }

        public async Task AddAsync(Log log)
        {
            await _logRep.InsertAsync(log);
            await _uow.CommitAsync();
        }
    }

    public interface ILogService
    {
        void Add(Log log);

        Task AddAsync(Log log);
    }
}
