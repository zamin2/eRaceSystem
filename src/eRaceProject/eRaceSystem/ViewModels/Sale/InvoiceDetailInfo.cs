using eRaceSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.Sale
{
    public class InvoiceDetailInfo
    {
        public int InvoiceDetailID { get; set; }

        public int InvoiceID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        public decimal? Price { get; set; }

        public List<InvoiceInfo> RequireInvoice { get; set; }

        public List<StoreRefundInfo> RequireStoreInfo { get; set; }
    }
}
