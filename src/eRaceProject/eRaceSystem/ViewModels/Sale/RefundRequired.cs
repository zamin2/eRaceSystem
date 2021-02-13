using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.Sale
{
    public class RefundRequired
    {
        public List<InvoiceDetailInfo> ReuquiredDetail { get; set; }

        public List<InvoiceInfo> RequiredInvoice { get; set; }

        public List<StoreRefundInfo> RequiredStore { get; set; }

    }
}
