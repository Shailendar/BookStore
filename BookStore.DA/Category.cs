using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.DA
{
    public class Category
    {
        public Category()
        {
            CategoryBooks = new HashSet<Book>();
            IsActive = true;
        }

        public int Id { get; set; }

        [RegularExpression(@"^[A-Z)+[a-zA-Z''-'\s]*$",ErrorMessage="Category name contains only alphabets and space ")]
        [MinLength(3,ErrorMessage = "category name at leat 3 characters")]
        [Required(ErrorMessage = "Category Name is mandatory")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [RegularExpression(@"^[A-Z)+[a-zA-Z''-'\s]*$", ErrorMessage = "Description name contains only alphabets and space ")]
        [MinLength(3, ErrorMessage = "Description at least 3 characters")]
        [Required(ErrorMessage = " Description is mandatory")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category Status is mandatory")]
        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<Book> CategoryBooks { get; set; }

        [NotMapped]
        public string Error { get; set; }
        [NotMapped]
        public string Success { get; set; }
    }
}
