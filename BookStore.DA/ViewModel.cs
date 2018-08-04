﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DA
{
    public class ViewModel
    {
        public List<Book> mostPopularBooks { get; set; }
        public List<Book> recentPurchasedBooks { get; set; }
        public List<Book> browsingHistoryBooks { get; set; }
        public List<Book> Books { get; set; }
    }
}