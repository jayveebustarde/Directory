using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserRoleDTO
    {
        public Guid UserId { get; set; }
        
        public Guid RoleId { get; set; }

        public UserDTO User { get; set; }

        public RoleDTO Role { get; set; }
    }
}
