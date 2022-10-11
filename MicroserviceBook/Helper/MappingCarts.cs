using AutoMapper;

using MicroserviceBook.Entities;
using MicroserviceBook.ViewModels.CartVM;

namespace MicroserviceBook.Helper
{
    public class MappingCarts : Profile
    {
        public MappingCarts()
        {
           //CreateMap<CreateCartDTO,Cart>();
           CreateMap<Cart, GetCartVM>();
        }
    }
}
