using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.Purchasing
{
    public class UpdatedOrderDetail
    {
        public int? OrderDetailID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitCost { get; set; }
        public int OrderQty { get; set; }

        public int UnitSize { get; set; }
    }
}
