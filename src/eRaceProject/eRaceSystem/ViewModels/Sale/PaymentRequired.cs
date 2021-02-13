using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.Sale
{
    public class PaymentRequired
    {
        public List<InvoiceDetailInfo> ReuquiredDetail { get; set; }

        public List<InvoiceInfo> RequiredInvoice { get; set; }
    }
}
