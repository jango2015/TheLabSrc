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
    public class UserService : IUserService
    {
        private readonly IUserRep _userRep;
        //private readonly ILabUow _uow;
        public UserService(IUserRep userRep
            //, ILabUow uow
            )
        {
            _userRep = userRep;
            //_uow = uow;
        }

        public IEnumerable<User> GetUserList()
        {
            var users = _userRep.GetAllList();
            return users;
        }
    }

    public interface IUserService
    {
        IEnumerable<User> GetUserList();

    }
}
