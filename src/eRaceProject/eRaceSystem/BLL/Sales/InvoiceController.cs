using eRaceSystem.DAL;
using eRaceSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.BLL.Sales
{
    
    
    [DataObject]
   public  class InvoiceController
    {
        
        public List<RefundDetail> List_InvoiceDetailsList(int invoiceID)
        {
            using (var context = new eRaceContext())
            {
                var results = (from x in context.Invoices
                               where x.InvoiceID.Equals(invoiceID)
                               select x).FirstOrDefault();
                if (results == null)
                {
                    return null;
                }
                else
                {
                    var items = from x in context.InvoiceDetails
                                where x.InvoiceID.Equals(invoiceID)
                                select new RefundDetail
                                {
                                    Product = x.Product.ItemName,
                                    Qty = x.Quantity,
                                    Price = (decimal)x.Price,
                                    Amount = x.Quantity * (decimal)x.Price,
                                    RestockCharge = x.Product.ReStockCharge
                                };
                    return items.ToList();
                }
            }
        }
    }
}
