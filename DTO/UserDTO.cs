using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserDTO : IUser<Guid>
    {
        public Guid Id { get; set; }
        
        public string UserName { get; set; }
        
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        public virtual int AccessFailedCount { get; set; }



        public List<UserLoginDTO> Logins { get; set; }

        public List<UserClaimDTO> Claims { get; set; }

        public List<UserRoleDTO> Roles { get; set; }
    }
}
