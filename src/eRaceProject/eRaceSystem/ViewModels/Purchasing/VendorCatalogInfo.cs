using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.Purchasing
{
    public class VendorCatalogInfo
    {
        public int VendorCatalogID { get; set; }
        public string Category { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int ReorderQty { get; set; }
        public int QuantityOnHand { get; set; }
        public int QuantityOnOrder { get; set; }
        public int UnitSize { get; set; }

        public decimal ItemCost { get; set; }
    }
}
