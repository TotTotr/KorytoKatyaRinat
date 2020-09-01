using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace Logic.ViewModel
{
    [DataContract]
    public class CarViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Название автомобиля")]
        public string CarName { get; set; }

        [DataMember]
        [DisplayName("Цена автомобиля без комплектации")]
        public decimal Price { get; set; }

        [DataMember]
        [DisplayName("Цена автомобиля")]
        public decimal FullPrice { get; set; }

        [DataMember]
        [DisplayName("Год выпуска")]
        public int Year { get; set; }

        public Dictionary<int, (string, int, decimal)> CarDetails { get; set; }
    }
}
