using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.ViewModel
{
    public class ReportDetailRequestViewModel
    {
        public string RequestName { get; set; }

        public DateTime DateCreate { get; set; }

        public string DetailName { get; set; }

        public int Count { get; set; }

        public List<Tuple<string, int>> Details { get; set; }
    }
}
