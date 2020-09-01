using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Logic.BindingModel
{
    [DataContract]
    public class DetailBindingModel
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public string DetailName { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public int TotalAmount { get; set; }
    }
}
