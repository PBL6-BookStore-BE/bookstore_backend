using AutoMapper;
using MicroserviceOrder.DTOs.Payment;
using MicroserviceOrder.ViewModels.PaymentVM;
using PBL6.BookStore.Models.Entities.Order;

namespace MicroserviceOrder.Helper
{
    public class MappingPayment : Profile
    {
        public MappingPayment()
        {
            CreateMap<CreatePaymentDTO, Payment>();
            CreateMap<UpdatePaymentDTO, Payment>();

            CreateMap<Payment, GetAllPaymentsVM>();
            CreateMap<Payment,GetPaymentVM>();
        }
    }
}
