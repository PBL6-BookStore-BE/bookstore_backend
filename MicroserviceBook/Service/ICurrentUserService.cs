namespace MicroserviceBook.Services
{
    public interface ICurrentUserService
    {
        string Email { get; }
        string Id { get; }
        string Username { get; }
    }
}