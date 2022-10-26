using AutoMapper;
using MicroserviceOrder.DTOs.OrderDetail;
using MicroserviceOrder.ViewModels.OrderDetailVM;
using PBL6.BookStore.Models.Entities.Order;

namespace MicroserviceOrder.Helper
{
    public class MappingOrderDetails : Profile
    {
        public MappingOrderDetails()
        {
            CreateMap<CreateOrderDetailDTO, OrderDetail>();
            CreateMap<UpdateOrderDetailDTO, OrderDetail>();
            CreateMap<OrderDetail, GetAllOrderDetailsVM>();
            CreateMap<OrderDetail, GetOrderDetailVM>();
            CreateMap<UpdateOrderDetailDTO,CreateOrderDetailDTO>();
        }
    }
}
