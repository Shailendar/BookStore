using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.DA
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        [Display(Name = "Order Id")]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public int ShippingAddressId { get; set; }

        public int PaymentTypeId { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal TotalShippingCost { get; set; }

        public decimal GrandTotal { get; set; }

        [ForeignKey("OrderStatus")]
        public int Status { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual Customer Customer { get; set; }

        [ForeignKey("Status")]
        public virtual OrderStatus OrderStatus { get; set; }

        public virtual PaymentType PaymentType { get; set; }

        public virtual ShippingAddress ShippingAddress { get; set; }
      
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        [NotMapped]
        public IEnumerable<OrderStatus> OrderStatusList { get; set; }
    }
}
