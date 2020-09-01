using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Logic.BindingModel
{
    [DataContract]
    public class CarBindingModel
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public string CarName { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public decimal FullPrice { get; set; }

        [DataMember]
        public int Year { get; set; }

        public Dictionary<int, (string, int, decimal)> CarDetails { get; set; }
    }
}
