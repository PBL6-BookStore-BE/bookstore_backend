using MicroserviceBook.DTOs.Cart;

namespace MicroserviceBook.Interfaces
{
    public interface ICartRepository 
    {
        public Task<int> GetIdCart(string IdUser);

        public Task<int> CreateCart();
    }
}
