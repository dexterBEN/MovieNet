using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNet
{
    public interface IPassword
    {
        System.Security.SecureString Password { get;  }
    }
}
