using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.DA
{
    public class PaymentType
    {
        public PaymentType()
        {
            Order = new HashSet<Order>();
            IsActive = true;
        }

        public int Id { get; set; }

        [RegularExpression(@"^[A-Z)+[a-zA-Z''-'\s]*$", ErrorMessage = "Payment type contains only alphabets and space ")]
        [MinLength(3, ErrorMessage = "payment type at leat 3 characters")]
        [Required(ErrorMessage = "Payment Type is mandatory")]
        [Display(Name = "Payment Type")]
        public string Type { get; set; }

        [RegularExpression(@"^[A-Z)+[a-zA-Z''-'\s]*$", ErrorMessage = "Description contains only alphabets and space ")]
        [MinLength(3, ErrorMessage = "Dscription at leat 3 characters")]
        [Required(ErrorMessage = "Description is mandatory")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Payment Type is mandatory")]
        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<Order> Order { get; set; }

        [NotMapped]
        public string Error { get; set; }
        [NotMapped]
        public string Success { get; set; }
    }
}
