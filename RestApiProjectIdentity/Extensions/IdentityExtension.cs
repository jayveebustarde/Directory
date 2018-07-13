using System;
using System.Security.Claims;
using System.Security.Principal;

namespace Microsoft.AspNet.Identity
{
    public static class IdentityExtensions
    {
        //
        // Summary:
        //     Return the user id using the UserIdClaimType
        //
        // Parameters:
        //   identity:
        public static Guid GetUserId(this IIdentity identity, bool test = true)
        {
            string id = identity.GetUserId();
            return Guid.Parse(id);
        }
    }
}