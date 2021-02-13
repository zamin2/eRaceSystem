using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.Receiving
{
    public class ForceCloseDetails
    {
        public int OrderID { get; set; }
        public bool Closed { get; set; }
        public string Comment { get; set; }
        public IEnumerable<ForceCloseItems> Items { get; set; }
    }
}
