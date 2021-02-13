using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.Receiving
{
    public class ReceiveOrderItemDetails
    {
        public int ReceiveOrderID { get; set; }
        public int OrderDetailID { get; set; }
        public int ItemQuantity { get; set; } // Single Item value
    }
}
