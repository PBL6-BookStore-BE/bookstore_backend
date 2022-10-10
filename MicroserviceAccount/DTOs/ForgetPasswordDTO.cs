using System.ComponentModel.DataAnnotations;

namespace MicroserviceAccount.DTOs
{
    public class ForgetPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}