using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.Purchasing
{
    public class OrderDetailInfo
    {
        public int OrderDetailID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int OrderQty { get; set; }
        public int UnitSize { get; set; }
        public decimal UnitCost { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal ItemCost { get; set; }
        public decimal ExtendedCost { get; set; }
        public string warning { get; set; }

        public int ReorderQty { get; set; }
        public int QuantityOnHand { get; set; }
        public int QuantityOnOrder { get; set; }

        public string CategoryName { get; set; }
    }
}
