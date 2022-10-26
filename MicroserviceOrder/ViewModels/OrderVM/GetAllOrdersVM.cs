using PBL6.BookStore.Models.Entities.Order;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroserviceOrder.ViewModels.OrderVM
{
    public class GetAllOrdersVM
    {
        public bool Id { get; set; }
        public bool Status { get; set; }
        public string User { get; set; }
        public string Payment { get; set; }
    }
}
