using Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.HelperModels
{
    class WordInfoAvto
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<CarViewModel> Cars { get; set; }
    }
}
