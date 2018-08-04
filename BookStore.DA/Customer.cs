using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.DA
{
    public class Customer
    {
        public Customer()
        {
            Cart = new HashSet<Cart>();
            Order = new HashSet<Order>();
            ShippingAddress = new HashSet<ShippingAddress>();
        }
 
        public int Id { get; set; }

        [RegularExpression(@"^[A-Z)+[a-zA-Z''-'\s]*$", ErrorMessage = "First name contains only alphabets and space ")]
        [MinLength(3, ErrorMessage = "First name must be atleast 3 characters")]
        [Required(ErrorMessage = "First name is mandatory")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[A-Z)+[a-zA-Z''-'\s]*$", ErrorMessage = "Last name contains only alphabets and space ")]
        [MinLength(3, ErrorMessage = "Last name must be atleast 3 characters")]
        [Required(ErrorMessage = "Last name is mandatory")]
        public string LastName { get; set; }

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email contains characters and specail characters")]
        [MinLength(3, ErrorMessage = "Email must be atleast 3 characters")]
        [Required(ErrorMessage = "Email is mandatory")]
        public string Email { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage="Phone number contains only numbers")]
        [MaxLength(10,ErrorMessage="Phone number contails only 10 digits")]
        [Required(ErrorMessage = "Phone number  is mandatory")]
        public string Phone { get; set; }

        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Date of birth is mandatory")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? BirthDate { get; set; }

        [Display(Name="Status")]
        public bool IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }

        public virtual ICollection<Order> Order { get; set; }

        public virtual ICollection<ShippingAddress> ShippingAddress { get; set; }
        [NotMapped]
        public string Error { get; set; }
        [NotMapped]
        public string Succuss { get; set; }

    }
}
