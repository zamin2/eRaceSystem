using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.Receiving
{
    public class ReceivedOrderDetails
    {
        public int OrderID { get; set; }
        public int Outstanding { get; set; }
        public ReceiveOrdersInfo ReceivedOrder { get; set; } // Creates a new ReceiveOrder Entity
        public IEnumerable<ReceiveOrderItemDetails> ReceiveOrderItems { get; set; } // Creates new ReceiveOrderItem Entities
        public IEnumerable<ProductDetails> Products { get; set; } //Updates the Products table with new values
        public IEnumerable<ReturnOrderItemDetails> ReturnOrderItems { get; set; } // Create new ReturnOrderItem Entities
    }
}
