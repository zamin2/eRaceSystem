using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.Sale
{
    public class NewInvoice
    {
        public int EmployeeID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Subtotal { get; set; }
        public decimal GST { get; set; }
        public decimal Total { get; set; }
        public List<NewInvoiceDetail> NewDetails { get; set; }
    }
}
