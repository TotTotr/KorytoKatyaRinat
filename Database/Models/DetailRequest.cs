using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Models
{
    public class DetailRequest
    {
        public int Id { get; set; }

        public int RequestId { get; set; }

        public int DetailId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Detail Detail { get; set; }

        public virtual Request Request { get; set; }
    }
}
