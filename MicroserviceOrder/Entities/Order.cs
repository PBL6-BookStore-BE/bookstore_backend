using MicroserviceOrder.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL6.BookStore.Models.Entities.Order
{
    public class Order : BaseEntity
    {
        public bool Status { get; set; }
        //[ForeignKey("User")]
        [MaxLength(255)]
        public string? IdUser { get; set; }
        [ForeignKey("Payment")]
        public int IdPayment { get; set; }
        //public virtual User? User {get;set;}
        public string? OrderAddress { get; set; }
        public string? ReceiverName { get; set; }
        public string? Number { get; set; }
        public string? Total { get; set; }
        public virtual Payment? Payment { get; set; }
    }
}
