using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.Receiving
{
    public class OrderDetailInfo
    {
        public int OrderDetailID { get; set; }
        public string ProductName { get; set; }
        public int BulkQuantityOrdered { get; set; }
        public int ItemUnitSize { get; set; }
        public int ItemQuantityOutstanding { get; set; }
    }

}
