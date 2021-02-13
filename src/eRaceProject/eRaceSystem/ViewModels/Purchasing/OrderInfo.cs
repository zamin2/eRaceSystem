using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.Purchasing
{
    public class OrderInfo
    {
        public int OrderID { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public string VendorComment { get; set; }

        public int? OrderNumber { get; set; }

        public DateTime? OrderDate { get; set; }
        public IEnumerable<OrderDetailInfo> OrderDetails { get; set; }
    }
}
