using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.Receiving
{
    public class VendorContactInfo
    {
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; } // Look like (address) (city)
        public string Contact { get; set; }
        public string Phone { get; set; }
    }
}
