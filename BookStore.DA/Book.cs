using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.DA
{
    public class Book
    {
        public Book()
        {
            Cart = new HashSet<Cart>();
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Category is mandatory")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        //[RegularExpression(@"^[A-Z)+[a-zA-Z''-'\s]*$", ErrorMessage = "Book name contains only alphabets and space ")]
        [Required(ErrorMessage = "Book Name is mandatory")]
        [Display(Name = "Book Name")]
        public string Name { get; set; }

        //[RegularExpression(@"^[A-Z)+[a-zA-Z''-'\s]*$", ErrorMessage = "Book description contains only alphabets and space ")]
        [Required(ErrorMessage = "Book Description is mandatory")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        //[RegularExpression("^[0-9,.]*$", ErrorMessage = "Unit Price must contain only digits")]
        [Required(ErrorMessage = "Book Unit Price is mandatory")]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        //[RegularExpression("^[0-9]*$", ErrorMessage = "Discount must contain only digits")]
        [Required(ErrorMessage = "Book Discount is mandatory")]
        [Display(Name = "Discount")]
        public decimal Discount { get; set; }

        //[RegularExpression("^[0-9,.]*$", ErrorMessage = "Shipping Cost must contain only digits")]
        [Required(ErrorMessage = "Book Shipping Cost is mandatory")]
        [Display(Name = "Shipping Cost")]
        public decimal ShippingCost { get; set; }

        //[RegularExpression("^[0-9]*$", ErrorMessage = "Quantity must contain only digits")]
        [Required(ErrorMessage = "Book Quantity is mandatory")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        public byte[] Image { get; set; }

        //[RegularExpression("^[0,1]*$", ErrorMessage = "Display Order must contain only 0 or 1")]
        [Required(ErrorMessage = "Book Display Order is mandatory")]
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }

        [Required(ErrorMessage = "Book Status is mandatory")]
        [Display(Name = "Status")]
        [ForeignKey("BookStatus")]
        public int Status { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [ForeignKey("Status")]
        public virtual BookStatus BookStatus { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        [NotMapped]
        public string Error { get; set; }
        [NotMapped]
        public string Success { get; set; }
        
        [NotMapped]
        public string FilePath { get; set; }

        [NotMapped]
        public IEnumerable<Category> CategoryList { get; set; }

        [NotMapped]
        public IEnumerable<BookStatus> BookStatusList { get; set; }
    }
}
