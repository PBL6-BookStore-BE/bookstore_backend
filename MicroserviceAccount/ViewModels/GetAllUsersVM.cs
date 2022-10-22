namespace MicroserviceAccount.ViewModels
{
    public class GetAllUsersVM
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}