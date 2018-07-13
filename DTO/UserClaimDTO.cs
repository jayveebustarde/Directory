using System;

namespace DTO
{
    public class UserClaimDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public UserDTO User { get; set; }
    }
}
