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
        public int Price { get; set; }

        [DataMember]
        public int FullPrice { get; set; }

        [DataMember]
        public int Year { get; set; }

        public Dictionary<int, (string, int, int)> CarDetails { get; set; }
    }
}
