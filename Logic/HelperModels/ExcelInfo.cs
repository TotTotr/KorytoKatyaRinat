using Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.HelperModels
{
    class ExcelInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<ReportDetailRequestViewModel> DetailRequests { get; set; }
    }
}
