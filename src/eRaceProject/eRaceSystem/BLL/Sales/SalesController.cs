using eRaceSystem.DAL;
using eRaceSystem.Entities;
using eRaceSystem.ViewModels;
using System.Collections.Generic;using System;

using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eRaceSystem.ViewModels.Sale;

namespace eRaceSystem.BLL.Sales
{
    [DataObject]
    public class SalesController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
       
        public List<CategoryInfo> ListCategory()
        {
            using (var context = new eRaceContext())
            {
                var result = from x in context.Categories
                             select new CategoryInfo
                             { CategoryName=x.Description,
                               ID=x.CategoryID
                             };
                return result.ToList();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<InvoiceInfo> GetNewInvoiceId()
        {
            using (var context = new eRaceContext())
            {
                var newID = from x in context.Invoices
                             orderby x.InvoiceID descending
                             select new InvoiceInfo
                             {
                                 InvoiceID=x.InvoiceID
                             };

                return newID.ToList();

            }


        }


        public int ReturnNewInvoiceID()
        {
            using (var context = new eRaceContext())
            {
                var date = (from x in context.InvoiceDetails orderby x.InvoiceID descending select x.InvoiceID).FirstOrDefault();

                return date;
            }
        }


        public int Get_CategoryID(string product)
        {
            using (var context = new eRaceContext())
            {
                var id = (from x in context.Products
                          where x.ItemName.Equals(product)
                          select x.CategoryID).FirstOrDefault();
                return id;
            };
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ProductInfo> Products_GetByCategoryID(int categoryID)
        {
            using (var context = new eRaceContext())
            {
                var result = from x in context.Products
                             where x.CategoryID == categoryID
                             select new ProductInfo
                             { 
                                ProductID=x.ProductID,
                                ItemName=x.ItemName
                             
                             };

                return result.ToList();
            }
        }

        public ProductInfo GetProduct(int productId)
        {
            using (var context = new eRaceContext())
            {
                var result = from item in context.Products
                             where item.ProductID == productId
                             select new ProductInfo
                             {
                                 ProductID = item.ProductID,
                                 ItemName = item.ItemName,
                                 QuantityOnHand = item.QuantityOnHand,
                                 ItemPrice = item.ItemPrice
                             };
                return result.Single();
            }
        }

        public ProductInfo Product_Get(string product)
        {
            using (var context = new eRaceContext())
            {
                var result = from item in context.Products
                             where item.ItemName == product
                             select new ProductInfo
                             {
                                 ProductID = item.ProductID,
                              
                             };
                return result.Single();
            }
        }





    }
}
