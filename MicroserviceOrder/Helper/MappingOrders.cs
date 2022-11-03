using AutoMapper;
using MicroserviceOrder.DTOs.Order;
using MicroserviceOrder.ViewModels.OrderVM;
using PBL6.BookStore.Models.Entities.Order;

namespace MicroserviceOrder.Helper
{
    public class MappingOrders : Profile
    {
        public MappingOrders()
        {
            CreateMap<CreateOrderDTO, Order>();
            CreateMap<UpdateOrderDTO, Order>();
            CreateMap<Order, GetAllOrdersVM>().ForMember(c => c.Payment, d => d.MapFrom(e => e.Payment.Name));
            CreateMap<Order, GetOrderVM>().ForMember(c => c.Payment, d => d.MapFrom(e => e.Payment.Name));
        }
    }
}
