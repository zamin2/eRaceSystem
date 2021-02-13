using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.Purchasing
{
    public class VendorProducts
    {
        public string CategoryName { get; set; }
        public IEnumerable<VendorCatalogInfo> Items { get; set; }
    }
}
