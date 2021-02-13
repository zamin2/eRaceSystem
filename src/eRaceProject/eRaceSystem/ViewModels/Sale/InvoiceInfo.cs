using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.Sale
{
    public class InvoiceInfo
    {
        public int InvoiceID { get; set; }

        public DateTime InvoiceDate { get; set; }

        public int EmployeeID { get; set; }
     
        public decimal SubTotal { get; set; }

        public decimal GST { get; set; }

        public decimal? Total { get; set; }
    }
}
