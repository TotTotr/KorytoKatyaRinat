using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Models
{
    public class CarDetail
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public int DetailId { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public decimal Sum { get; set; }

        public virtual Detail Detail { get; set; }

        public virtual Car Car { get; set; }
    }
}
