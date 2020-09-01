using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.BindingModel
{
    public class RequestBindingModel
    {
        public int? Id { get; set; }

        public int? DetailId { get; set; }

        public string RequestName { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public DateTime DateCreate { get; set; }

        public Dictionary<int, (string, int)> DetailRequests { get; set; }
    }
}
