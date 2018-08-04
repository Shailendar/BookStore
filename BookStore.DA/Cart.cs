using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BookStore.DA
{
    public class Cart
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int BookId { get; set; }

        public int Quantity { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual Book Book { get; set; }

        public virtual Customer Customer { get; set; }

        [NotMapped]
        public string Error { get; set; }
        [NotMapped]
        public string Success { get; set; }
    }
}
