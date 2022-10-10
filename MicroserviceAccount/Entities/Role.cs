using Microsoft.AspNetCore.Identity;

namespace MicroserviceAccount.Entities
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }
    }
}
