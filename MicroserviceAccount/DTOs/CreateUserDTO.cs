using System.ComponentModel.DataAnnotations;

namespace MicroserviceAccount.DTOs
{
    public class CreateUserDTO
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }
    }
}