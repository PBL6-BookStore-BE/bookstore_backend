using MicroserviceOrder.DTOs.Payment;
using MicroserviceOrder.ViewModels.PaymentVM;

namespace MicroserviceOrder.Interfaces
{
    public interface IPaymentRepository
    {
        public Task<IEnumerable<GetAllPaymentsVM>> GetAllPaymentsAsync();
        public Task<GetPaymentVM> GetPayment(int id);

        public Task<int> CreatePayment(CreatePaymentDTO model);

        public Task<int> UpdatePayment(UpdatePaymentDTO model);
        public Task<int> DeletePayment(int id);
    }
}
