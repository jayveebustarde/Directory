using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class UserClaim
    {
        [Key, Required]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public User User { get; set; }
    }
}
