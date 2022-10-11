using MicroserviceBook.ViewModels.CartVM;

namespace MicroserviceBook.Interfaces
{
    public interface ICartRepository
    {
        //public Task<int> CreateCart(CreateCartDTO model);
        public Task<int> DeleteCart(int id);
        public Task<GetCartVM> GetCartAsync(int id);
        public Task<GetCartVM> GetCartByIdUser(int idUser);


    }
}
