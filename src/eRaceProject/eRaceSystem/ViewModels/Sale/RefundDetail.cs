using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels
{
    public class RefundDetail
    {
        public string Product { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public decimal RestockCharge { get; set; }
        public string Reason { get; set; }

        public int InvoiceID { get; set; }
    }
}
