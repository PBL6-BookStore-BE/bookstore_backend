using MicroserviceOrder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL6.BookStore.Models.Entities.Order
{
    public class Order : BaseEntity
    {
        public bool Status { get; set; }

        public int IdUser { get; set; }
        public int IdPayment { get; set; }
    }
}
