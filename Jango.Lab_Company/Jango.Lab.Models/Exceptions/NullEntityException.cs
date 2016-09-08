using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.Models.Exceptions
{
    public class NullEntityException : Exception
    {
        public NullEntityException(string message) : base(message) { }
    }
}
