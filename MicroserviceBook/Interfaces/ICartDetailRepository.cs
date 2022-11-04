using MicroserviceBook.DTOs.Cart;
using MicroserviceBook.ViewModels.CartVM;

namespace MicroserviceBook.Interfaces
{
    public interface ICartDetailRepository
    {
        public Task<IEnumerable<GetCartDetailVM>> GetCartByUser();
        public Task<int> CreateCartDetail(CartDetailDTO model);
        public Task<int> DeleteCartDetail(DeleteCartDTO model);
        public Task<int> ChangeQuantity(CartDetailDTO model);

    }
}
