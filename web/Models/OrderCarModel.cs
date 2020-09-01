using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Models
{
    public class OrderCarModel
    {
        public string CarName { get; set; }

        public int Year { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }
    }
}
