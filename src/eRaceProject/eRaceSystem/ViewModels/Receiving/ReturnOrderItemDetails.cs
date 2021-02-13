using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.Receiving
{
    public class ReturnOrderItemDetails
    {
        public int ReceiveOrderID { get; set; }
        public int OrderDetailID { get; set; }
        public string UnOrderedItem { get; set; }
        public int ItemQuantity { get; set; }
        public string Comment { get; set; }
        public string VendorProductID { get; set; }
    }
}
