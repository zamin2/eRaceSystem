using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FreeCode.Exceptions;
using eRaceSystem.ViewModels.Purchasing;
using eRaceSystem.DAL;
using eRaceSystem.Entities;

namespace eRaceSystem.BLL.Purchasing
{
    [DataObject]
    public class PurchasingController
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<SelectionItem> ListVendors()
        {
            using (var context = new eRaceContext())
            {
                var result = from a in context.Vendors
                             select new SelectionItem
                             {
                                 IDValue = a.VendorID,
                                 DisplayText = a.Name

                             };
                return result.ToList();
            }
        }

        public void DeleteOrder(OrderInfo a)
        {
            using(var context = new eRaceContext())
            {
                var order = context.Orders.Find(a.OrderID);

                foreach(var x in a.OrderDetails)
                {
                    var orderitem = context.OrderDetails.Find(x.OrderDetailID);
                    context.OrderDetails.Remove(orderitem);
                }
                
                
                context.Orders.Remove(order);
                
                context.SaveChanges();




            }
        }

        public void PlaceOrder(UpdatedOrder item)
        {
            using (var context = new eRaceContext())
            {
                if (item.OrderID == 0)
                { // adding a new order
                    var order = new Order();

                    order.TaxGST = item.Tax;
                    order.SubTotal = item.SubTotal;
                    order.VendorID = item.VendorID;
                    order.Comment = item.VendorComment;
                    order.Closed = false;
                    order.OrderDate = DateTime.Now;
                    var list = (from a in context.Orders
                                where a.OrderNumber != null
                                select a.OrderNumber);
                    order.OrderNumber = int.Parse(list.Max().ToString()) + 1;
                    order.EmployeeID = 1;

                    context.Orders.Add(order);
                    foreach (var y in item.UpdatedOrderDetails)
                    {
                        var additem = new OrderDetail
                        {
                            ProductID = y.ProductID,
                            Quantity = y.OrderQty,
                            Cost = y.UnitCost,
                            OrderUnitSize = y.UnitSize

                        };
                        var product = context.Products.Find(additem.ProductID);
                        product.QuantityOnOrder += additem.Quantity * additem.OrderUnitSize;
                        context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                        order.OrderDetails.Add(additem);
                    }
                    context.SaveChanges();


                }
                else // updating an pending order
                {
                    var order = context.Orders.Find(item.OrderID);
                    var orderdetails = context.OrderDetails.Where(x => x.OrderID == order.OrderID).ToList();

                    order.OrderID = item.OrderID;
                    order.TaxGST = item.Tax;
                    order.SubTotal = item.SubTotal;
                    order.VendorID = item.VendorID;
                    order.Comment = item.VendorComment;
                    order.Closed = false;
                    order.OrderDate = DateTime.Now;
                    var list = (from a in context.Orders
                                where a.OrderNumber != null
                                select a.OrderNumber);
                    order.OrderNumber = int.Parse(list.Max().ToString()) + 1;
                    order.EmployeeID = 1;

                    foreach (var a in orderdetails)
                    {
                        var existingitem = item.UpdatedOrderDetails.SingleOrDefault(x => x.ProductID == a.ProductID);
                        
                        if (existingitem == null)
                        {
                            context.Entry(a).State = System.Data.Entity.EntityState.Deleted;
                        }
                        else
                        {
                            a.Cost = existingitem.UnitCost;
                            a.Quantity = existingitem.OrderQty;
                            context.Entry(a).State = System.Data.Entity.EntityState.Modified;
                            var product = context.Products.Find(a.ProductID);
                            product.QuantityOnOrder += existingitem.OrderQty * existingitem.UnitSize;
                            context.Entry(product).State = System.Data.Entity.EntityState.Modified;

                        }


                    }
                    foreach (var y in item.UpdatedOrderDetails)
                    {
                        var newproduct = !orderdetails.Any(x => x.ProductID == y.ProductID);
                        if (newproduct == true)
                        {

                            var additem = new OrderDetail
                            {
                                ProductID = y.ProductID,
                                OrderID = item.OrderID,
                                Quantity = y.OrderQty,
                                Cost = y.UnitCost,
                                OrderUnitSize = y.UnitSize

                            };
                            order.OrderDetails.Add(additem);
                            var product = context.Products.Find(additem.ProductID);
                            product.QuantityOnOrder += additem.Quantity * additem.OrderUnitSize;
                            context.Entry(product).State = System.Data.Entity.EntityState.Modified;

                        }
                    }
                    context.Entry(order).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }

            }

        }
        public void SaveOrder(UpdatedOrder item, int? EmployeeID)
        {
            using(var context = new eRaceContext())
            {
                if (item.OrderID == 0) { // adding a new order
                    var order = new Order();
                    
                    order.TaxGST = item.Tax;
                    order.SubTotal = item.SubTotal;
                    order.VendorID = item.VendorID;
                    order.Comment = item.VendorComment;
                    order.Closed = false;
                    order.OrderDate = null;
                    order.OrderNumber = null;
                    order.EmployeeID = EmployeeID.Value;

                    context.Orders.Add(order);
                    foreach(var y in item.UpdatedOrderDetails)
                    {
                        var addskill = new OrderDetail
                        {
                            ProductID = y.ProductID,
                            
                            Quantity = y.OrderQty,
                            Cost = y.UnitCost,
                            OrderUnitSize = y.UnitSize

                        };
                        order.OrderDetails.Add(addskill);
                    }
                    context.SaveChanges();


                }
                else // updating an existing order
                {
                    var order = context.Orders.Find(item.OrderID);
                    var orderdetails = context.OrderDetails.Where(x => x.OrderID == order.OrderID).ToList();

                    order.OrderID = item.OrderID;
                    order.TaxGST = item.Tax;
                    order.SubTotal = item.SubTotal;
                    order.VendorID = item.VendorID;
                    order.Comment = item.VendorComment;
                    order.Closed = false;
                    order.OrderDate = null;
                    order.OrderNumber = null;
                    order.EmployeeID = 1;

                    foreach (var a in orderdetails)
                    {
                        var existingitem = item.UpdatedOrderDetails.SingleOrDefault(x => x.ProductID == a.ProductID);
                        if (existingitem == null)
                        {
                            context.Entry(a).State = System.Data.Entity.EntityState.Deleted;
                        }
                        else
                        {
                            a.Cost = existingitem.UnitCost;
                            a.Quantity = existingitem.OrderQty;
                            context.Entry(a).State = System.Data.Entity.EntityState.Modified;
                        }


                    }
                    foreach (var y in item.UpdatedOrderDetails)
                    {
                        var newproduct = !orderdetails.Any(x => x.ProductID == y.ProductID);
                        if (newproduct == true)
                        {

                            var addskill = new OrderDetail
                            {
                                ProductID = y.ProductID,
                                OrderID = item.OrderID,
                                Quantity = y.OrderQty,
                                Cost = y.UnitCost,
                                OrderUnitSize = y.UnitSize

                            };
                            order.OrderDetails.Add(addskill);

                        }
                    }
                    context.Entry(order).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
                
            }
        }

        public VendorInfo GetVendorInfo(int vendorid)
        {
            using (var context = new eRaceContext())
            {
                var result = from a in context.Vendors
                             where a.VendorID == vendorid
                             select new VendorInfo
                             {
                                 Name = a.Name,
                                 Address = a.Address,
                                 City = a.City,
                                 PostalCode = a.PostalCode,
                                 Phone = a.Phone
                             };
                return result.SingleOrDefault();

            }
        }

        public List<VendorProducts> GetVendorProducts(int vendorid)
        {
            using (var context = new eRaceContext())
            {
                var result = from a in context.VendorCatalogs
                             where a.VendorID == vendorid
                             group a by a.Product.Category.Description into GB1
                             select new VendorProducts
                             {
                                 CategoryName = GB1.Key,
                                 Items = from x in GB1
                                         where !(from q in context.Orders
                                                where q.VendorID == vendorid && q.OrderNumber == null
                                                from w in q.OrderDetails
                                                select w.ProductID 
                                                ).Contains(x.ProductID)
                                         select new VendorCatalogInfo
                                         {
                                             Category = x.Product.Category.Description,
                                             ProductID = x.ProductID,
                                             ProductName = x.Product.ItemName,
                                             ItemCost = x.Product.ItemPrice,
                                             ReorderQty = x.Product.ReOrderLevel,
                                             QuantityOnHand = x.Product.QuantityOnHand,
                                             QuantityOnOrder = x.Product.QuantityOnOrder,
                                             UnitSize = x.OrderUnitSize
                                         }
                             };
                return result.ToList();
            }
        }

        public OrderInfo GetOrder(int vendorid)
        {
            using(var context = new eRaceContext())
            {
                var result = from a in context.Orders
                             where a.VendorID == vendorid && a.OrderNumber == null
                             select new OrderInfo
                             {
                                 OrderID = a.OrderID,
                                 VendorComment = a.Comment,
                                 SubTotal = Math.Round(a.SubTotal, 2),
                                 Tax = Math.Round(a.TaxGST, 2),
                                 Total = Math.Round((a.SubTotal + a.TaxGST), 2),
                                 OrderNumber = a.OrderNumber,
                                 OrderDate = a.OrderDate,
                                 OrderDetails = from x in a.OrderDetails
                                                select new OrderDetailInfo
                                                {
                                                    OrderDetailID = x.OrderDetailID,
                                                    ProductID = x.ProductID,
                                                    ProductName = x.Product.ItemName,
                                                    OrderQty = x.Quantity,
                                                    UnitSize = x.OrderUnitSize,
                                                    UnitCost = Math.Round(x.Cost, 2),
                                                    ItemPrice = Math.Round(x.Product.ItemPrice, 2),
                                                    ItemCost = Math.Round(x.Cost, 2) / x.OrderUnitSize,
                                                    ExtendedCost = Math.Round((x.Quantity * x.Cost), 2),
                                                    ReorderQty = x.Product.ReOrderLevel,
                                                    QuantityOnHand = x.Product.QuantityOnHand,
                                                    QuantityOnOrder = x.Product.QuantityOnOrder,
                                                    CategoryName = x.Product.Category.Description.Trim()

                                                }


                                 

                             };
                return result.FirstOrDefault();

            }
        }

    }
}
