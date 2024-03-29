﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.DA
{
    public class ShippingAddress
    {
        public ShippingAddress()
        {
           Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        
        public int CustomerId { get; set; }

        [RegularExpression(@"^[A-Z)+[a-zA-Z''-'\s]*$", ErrorMessage = "User name contains only alphabets and space ")]
        [MaxLength(20,ErrorMessage="User name maximum length is 20 characters ")]
        [MinLength(3,ErrorMessage="User name must be atleast 3 characters")]
        [Required(ErrorMessage=" User name is mandatory")]
        public string Name { get; set; }

        [MaxLength(50,ErrorMessage="Street maximum length is 50 characters")]
        [MinLength(3,ErrorMessage="Street1 must be atleast 3 characters")]
        [Required(ErrorMessage="Street1 is mandatory")]
        public string Street1 { get; set; }

        public string Street2 { get; set; }
        [MinLength(3, ErrorMessage = "Landmark must be atleast 3 characters")]
        [MaxLength(25, ErrorMessage = "Landmark maximum length is 25 characters")]
        [Required(ErrorMessage="Landmark is mandatory")]
        public string Landmark { get; set; }

        [MaxLength(25, ErrorMessage = "City maximum length is 25 characters")]
        [MinLength(3, ErrorMessage = "City must be atleast 3 characters")]
        [Required(ErrorMessage="City is mandatory")]
        public string City { get; set; }

        [MaxLength(25, ErrorMessage = "State maximum length is 25 characters")]
        [MinLength(3, ErrorMessage = "State must be atleast 3 characters")]
        [Required(ErrorMessage="State is mandatory")]
        public string State { get; set; }

        [Required(ErrorMessage="Zipcode is mandatory")]
        public int Zipcode { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Mobile number formate is not correct")]
        [MaxLength(10, ErrorMessage = "M obile number contails only 10 digits")]
        [Required(ErrorMessage="Mobile number is mandatory")]
        public string Mobile { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<Order> Order { get; set; }

        [NotMapped]
        public string Error { get; set; }

        [NotMapped]
        public string Success { get; set; }
    }
}
