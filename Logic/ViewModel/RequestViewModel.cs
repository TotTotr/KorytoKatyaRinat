using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace Logic.ViewModel
{
    [DataContract]
    public class RequestViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int DetailId { get; set; }

        [DataMember]
        [DisplayName("Название заявки")]
        public string RequestName { get; set; }

        [DataMember]
        [DisplayName("Дата оформления")]
        public DateTime DateCreate { get; set; }

        public Dictionary<int, (string, int)> DetailRequests { get; set; }
    }
}
