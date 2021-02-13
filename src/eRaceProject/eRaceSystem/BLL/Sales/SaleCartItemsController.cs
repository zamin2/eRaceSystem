using eRaceSystem.DAL;
using eRaceSystem.Entities;
using eRaceSystem.ViewModels.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.BLL.Sales
{
    public class SaleCartItemsController
    {
        public int Pay_ForCart(NewInvoice invoice)
        {

            using (var context = new eRaceContext())
            { 

                Invoice newitem = new Invoice();
                newitem.EmployeeID = invoice.EmployeeID;
                newitem.InvoiceDate = invoice.InvoiceDate;
                newitem.SubTotal = invoice.Subtotal;
                newitem.GST = invoice.GST;
                newitem.Total = invoice.Total;

                newitem = context.Invoices.Add(newitem); 
                

                InvoiceDetail newDetail = null;                   
                foreach (var item in invoice.NewDetails)
                {                    
                    newDetail = new InvoiceDetail();
                    newDetail.ProductID = item.ProductID;
                    newDetail.Quantity = item.Quantity;
                    newDetail.Price = item.Price;
                    context.InvoiceDetails.Add(newDetail);  

                    var product = (from x in context.Products
                              where x.ProductID == item.ProductID
                              select x).FirstOrDefault();
                    product.QuantityOnHand -= newDetail.Quantity;
                    context.Entry(product).Property(y => y.QuantityOnHand).IsModified = true;  
                }

                return context.SaveChanges();
            }
        
        
        }
    }
}
