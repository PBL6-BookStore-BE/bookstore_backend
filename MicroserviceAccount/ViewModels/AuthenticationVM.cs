namespace MicroserviceAccount.ViewModels
{
    public class AuthenticationVM
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}