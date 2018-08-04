using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.DA
{
    public class BookStatus
    {
        public BookStatus()
        {
            Book = new HashSet<Book>();
            IsActive = true;
        }

        public int Id { get; set; }

        [RegularExpression(@"^[A-Z)+[a-zA-Z''-'\s]*$", ErrorMessage = "Book status contains only alphabets and space ")]
        [MinLength(3, ErrorMessage = "Book status at leat 3 characters")]
        [Required(ErrorMessage = "Book Status is mandatory")]
        [Display(Name = "Book Status")]
        public string Code { get; set; }

        [RegularExpression(@"^[A-Z)+[a-zA-Z''-'\s]*$", ErrorMessage = "Description contains only alphabets and space ")]
        [MinLength(3, ErrorMessage = "Dscription at leat 3 characters")]
        [Required(ErrorMessage = "BookStatus Description is mandatory")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "BookStatus is mandatory")]
        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        
        public virtual ICollection<Book> Book { get; set; }

        [NotMapped]
        public string Error { get; set; }
        [NotMapped]
        public string Success { get; set; }
    }
}
