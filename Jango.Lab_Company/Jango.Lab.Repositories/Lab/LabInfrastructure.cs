using Jango.Lab.Models;
using Jango.Lib.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.Repositories.Lab
{
    public interface ILabUow : IUow { }
    public class LabUow : GeneraicUow<LabModels>, ILabUow
    {
        public LabUow(IDataContextFactory<LabModels> dbContextFactory) : base(dbContextFactory)
        {
        }
    }

    public interface ILabDbContextFactory : IDataContextFactory<LabModels>
    {

    }

    public class LabDbContextFactory : GenericDataContextFactory<LabModels>, ILabDbContextFactory
    {
        public LabDbContextFactory() { }
    }

    public interface ILabBaseRepository
    {

    }
    public class LabBaseRepository<T> : GenericRepository<T, LabModels>, ILabBaseRepository where T : class
    {
        public LabBaseRepository(ILabDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
