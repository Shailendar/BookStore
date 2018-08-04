using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DA
{
    public class AspNetRoles
    {
        public AspNetRoles()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }

    }
}
