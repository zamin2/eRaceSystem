using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.Receiving
{
    public class ReceiveOrdersInfo
    {
        public int OrderID { get; set; }
        public DateTime ReceiveDate { get; set; }
        public int EmployeeID { get; set; }
    }
}
