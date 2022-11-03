namespace MicroserviceOrder.Services
{
    public interface ICurrentUserService
    {
        string Email { get; }
        string Id { get; }
    }
}