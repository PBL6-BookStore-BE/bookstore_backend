using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace MicroserviceAccount.Entities
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public DateTime? CreatedOn { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}