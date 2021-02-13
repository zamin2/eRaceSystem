using eRaceSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels
{
    public class ProductInfo
    {
        public int ProductID { get; set; }
        public string ItemName { get; set; }

        public decimal ItemPrice { get; set; }

        public int QuantityOnOrder { get; set; }

        public int QuantityOnHand { get; set; }

        public int ReOrderLevel { get; set; }
        public decimal ReStockCharge { get; set; }
        public int CategoryID { get; set; }
        
    }
}
