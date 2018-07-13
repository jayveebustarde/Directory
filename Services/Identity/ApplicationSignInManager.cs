using DTO;
using Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Identity
{
    public class ApplicationSignInManager : SignInManager<UserDTO, Guid>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authManager) 
            : base(userManager, authManager)
        {

        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(UserDTO user)
        {
            return CreateUserIdentityAsync((ApplicationUserManager)UserManager, user);
        }

        public static async Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUserManager manager, UserDTO user)
        {
            var userIdentity = await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
