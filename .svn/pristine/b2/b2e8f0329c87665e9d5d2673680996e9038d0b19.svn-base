using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.DA
{
    public class OrderDetails
    {
        public int Id { get; set; }
        
        [Display(Name = "Order Id")]
        public int OrderId { get; set; }

        public int BookId { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal ShippingCost { get; set; }

        public int Quantity { get; set; }

        public decimal Total { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        
        public virtual Book Book { get; set; }
        
        public virtual Order Order { get; set; }
    }
}
