using System.ComponentModel.DataAnnotations;

namespace MicroserviceAccount.DTOs
{
    public class RegisterDTO
    {
        //[Required]
        //public string FullName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        //public string Address { get; set; }
        //public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }
    }
}