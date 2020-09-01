using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public string CarName { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal FullPrice { get; set; }

        [Required]
        public int Year { get; set; }

        [ForeignKey("CarId")]
        public virtual List<CarDetail> CarDetails { get; set; }
    }
}
