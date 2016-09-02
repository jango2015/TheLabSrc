using Jango.Lab.Models;
using Jango.Lib.Repository.Core;
using Jango.Lab.Repositories.Lab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.Repositories
{
    public class MessageRep : LabBaseRepository<Message>,IMessageRep
    {
        public MessageRep(ILabDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }
    }
    public interface IMessageRep : IRepository<Message>, ILabBaseRepository { }
}
