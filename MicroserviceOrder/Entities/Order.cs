using MicroserviceAccount.Entities;
using MicroserviceOrder.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL6.BookStore.Models.Entities.Order
{
    public class Order : BaseEntity
    {
        public bool Status { get; set; }
        [ForeignKey("User")]
        public string IdUser { get; set; }
        [ForeignKey("Payment")]
        public int IdPayment { get; set; }
        public virtual User? User {get;set;}
        public virtual Payment? Payment { get; set; }
    }
}
