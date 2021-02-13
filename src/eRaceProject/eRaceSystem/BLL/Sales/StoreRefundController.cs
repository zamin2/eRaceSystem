using eRaceSystem.DAL;
using eRaceSystem.Entities;
using eRaceSystem.ViewModels;
using eRaceSystem.ViewModels.Sale;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.BLL.Sales
{
    [DataObject]
    public class StoreRefundController
    {
        public List<InvoiceInfo> Get_Invoice(int id)
        {
            using (var context = new eRaceContext())
            {
                var existing = from x in context.InvoiceDetails
                               where x.InvoiceID == id
                               select new InvoiceInfo
                               { 
                                  InvoiceID=x.InvoiceID
                               };

                return existing.ToList();
            };
        }
  




        public int Check_Refunded(int originalInvoiceID, int productID)
        {
            using (var context = new eRaceContext())
            {
                int exists = 0;
                var refunds = from x in context.StoreRefunds
                              where x.OriginalInvoiceID.Equals(originalInvoiceID)
                              select x;
                foreach (var item in refunds)
                {
                    if (item.ProductID.Equals(productID))
                    {
                        exists = 1;
                    }
                }
                return exists;
            };
        }

     public int ReturnInvoiceID()
        {
            using (var context = new eRaceContext())
            {
                var date = (from x in context.StoreRefunds orderby x.InvoiceID descending select x.InvoiceID).FirstOrDefault();

                return date;
            }
        }




        public void CreateRefund(RefundRequired request)
        {
            using(var context = new eRaceContext())
            {

                foreach (var item in request.RequiredInvoice)
                {
                    var newinvoice = new Invoice
                    {
                        EmployeeID = item.EmployeeID,
                        InvoiceDate=item.InvoiceDate,
                        SubTotal=item.SubTotal,
                        GST=item.GST,
                        Total=item.Total

                    };
                    context.Invoices.Add(newinvoice);                   
                }

                foreach(var item in request.ReuquiredDetail)
                {
                    var newdetail = new InvoiceDetail
                    { 
                       ProductID=item.ProductID,
                       Quantity=item.Quantity                       
                    };

                    var product = (from x in context.Products
                                   where x.ProductID == item.ProductID
                                   select x).FirstOrDefault();
                    product.QuantityOnHand += newdetail.Quantity;
                    context.Entry(product).Property(y => y.QuantityOnHand).IsModified = true;                  

                }
              

                foreach(var item in request.RequiredStore)
                {
                    var newstore = new StoreRefund
                    { 
                       OriginalInvoiceID=item.OriginalInvoiceID,
                       ProductID=item.ProductID,
                       Reason=item.Reason,
                    
                    };
                    context.StoreRefunds.Add(newstore);                                                
                }    
                context.SaveChanges();
                 
               
            }

        }



    }
}
