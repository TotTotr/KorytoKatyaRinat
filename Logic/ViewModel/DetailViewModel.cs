using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace Logic.ViewModel
{
    [DataContract]
    public class DetailViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Название детали")]
        public string DetailName { get; set; }

        [DataMember]
        [DisplayName("Цена детали")]
        public int Price { get; set; }

        [DataMember]
        [DisplayName("Количество детали")]
        public int TotalAmount { get; set; }
    }
}
