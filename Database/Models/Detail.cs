using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models
{
    public class Detail
    {
        public int Id { get; set; }

        [Required]
        public string DetailName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int TotalAmount { get; set; }

        [ForeignKey("DetailId")]
        public virtual List<CarDetail> CarDetails { get; set; }

        [ForeignKey("DetailId")]
        public virtual List<DetailRequest> DetailRequests { get; set; }
    }
}
