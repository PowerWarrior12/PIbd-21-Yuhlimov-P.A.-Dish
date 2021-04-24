using DishProjectBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace DishProjectBusinessLogic.HelperModels
{
    class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportOrdersViewModel> Orders { get; set; }
    }
}
