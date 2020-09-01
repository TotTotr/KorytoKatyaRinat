using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Logic.ViewModel
{
    [DataContract]
    public class OrderCarViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int CarId { get; set; }

        [DataMember]
        public int? OrderId { get; set; }

        [DataMember]
        public string CarName { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}
