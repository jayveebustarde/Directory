using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserLoginDTO
    {
        public Guid UserId { get; set; }
        
        public string LoginProvider { get; set; }
        
        public string ProviderKey { get; set; }

        public UserDTO User { get; set; }
    }
}
