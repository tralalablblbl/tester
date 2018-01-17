using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dto
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUserClaim : IdentityUserClaim<Guid>
    {
        public ApplicationUserClaim() : base()
        {
        }
    }
}
