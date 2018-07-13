using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace RestApiProjectIdentity.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class ApplicationUser : IdentityUser<Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    //{
    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, Guid> manager, string authenticationType)
    //    {
    //        var userIdentity = await manager.CreateIdentityAsync(
    //            this, authenticationType);
    //        return userIdentity;
    //    }
    //}

    //public class ApplicationUserRole : IdentityUserRole<Guid> { }
    //public class ApplicationUserLogin : IdentityUserLogin<Guid> { }
    //public class ApplicationUserClaim : IdentityUserClaim<Guid> { }

    //public class ApplicationRole : IdentityRole<Guid, ApplicationUserRole>
    //{
    //    public ApplicationRole() { }
    //    public ApplicationRole(string name) { Name = name; }
    //}

    //public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, Guid,
    //    ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    //{
    //    public ApplicationUserStore(IdentityDbContext context) : base(context) { }
    //}

    //public class CustomRoleStore : RoleStore<ApplicationRole, Guid, ApplicationUserRole>
    //{
    //    public CustomRoleStore(IdentityDbContext context) : base(context) { }
    //}
    
}