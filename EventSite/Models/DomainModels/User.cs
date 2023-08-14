using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EventSite.Models.DomainModels;
using Microsoft.AspNetCore.Identity;

namespace EventSite.Models
{
    public class User : IdentityUser
    {
        [NotMapped]
        public IList<string> RoleNames { get; set; }

        public CartEntity Cart { get; set; }
    }
}
