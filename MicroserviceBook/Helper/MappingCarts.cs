
using AutoMapper;
using MicroserviceBook.DTOs.Cart;
using MicroserviceBook.Entities;
//using MicroserviceBook.ViewModels.CartVM;

namespace MicroserviceAccount.Helper
{
    public class MappingCarts : Profile
    {
        public MappingCarts()
        {
            CreateMap<CreateCartDTO, Cart>();
         
        }
    }
}
