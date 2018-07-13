using DTO;
using Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Identity
{
    public class ApplicationRoleManager : RoleManager<RoleDTO, Guid>
    {
        public ApplicationRoleManager(IRoleStore<RoleDTO, Guid> store) : base(store)
        {

        }
    }
}
