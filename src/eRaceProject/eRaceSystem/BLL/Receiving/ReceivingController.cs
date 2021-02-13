using eRaceSystem.DAL;
using eRaceSystem.Entities;
using eRaceSystem.ViewModels;
using eRaceSystem.ViewModels.Receiving;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FreeCode.Exceptions;
using System.Threading.Tasks;

namespace eRaceSystem.BLL.Receiving
{
    [DataObject]
    public class ReceivingController
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<SelectionList> ListOrders()
        { /* query from Orders */
            using (var context = new eRaceContext())
            {
                var results = from order in context.Orders
                              where order.Closed.Equals(false)
                              select new SelectionList
                              {
                                    IDValueField = order.OrderID,
                                    DisplayText = order.OrderNumber + " - " + order.Vendor.Name
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public VendorContactInfo GetVendorContact(int orderId)
        { /* query from Orders & Vendors */
            using (var context = new eRaceContext())
            {
                var result = from vendor in context.Orders
                             where vendor.OrderID == orderId
                             select new VendorContactInfo
                             {
                                 VendorId = vendor.VendorID,
                                 Name = vendor.Vendor.Name,
                                 Address = vendor.Vendor.Address + " " + vendor.Vendor.City,
                                 Contact = vendor.Vendor.Contact,
                                 Phone = vendor.Vendor.Phone
                             };
                return result.FirstOrDefault();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<OrderDetailInfo> GetOrderDetails(int orderId)
        { /* query from Orders, OrderDetails, ReceivedOrderItems & Products */
            using (var context = new eRaceContext())
            {
                var results = from item in context.OrderDetails
                              where item.OrderID == orderId
                              group item by item into itemDetails
                              orderby itemDetails.Key.ProductID
                              select new OrderDetailInfo
                              {
                                  OrderDetailID = itemDetails.Key.OrderDetailID,
                                  ProductName = itemDetails.Key.Product.ItemName,
                                  BulkQuantityOrdered = itemDetails.Key.Quantity* itemDetails.Key.OrderUnitSize,
                                  ItemUnitSize = itemDetails.Key.OrderUnitSize,
                                  ItemQuantityOutstanding = (itemDetails.Key.Quantity * itemDetails.Key.OrderUnitSize) - (itemDetails.Key.ReceiveOrderItems.Select(x => x.ItemQuantity)).Sum()
                              };
                
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void ClearUnOrderedItems()
        { /* use CRUD to delete all items in table (Try .Clear()) */
            using (var context = new eRaceContext())
            {
                var results = from item in context.UnOrderedItems
                              select item;

                foreach (var detail in results)
                {
                    context.UnOrderedItems.Remove(detail);
                }

                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public void ForceCloseOrder(ForceCloseDetails order)
        { /* command modify Products, Order and OrderDeatails */
            // Validation

            List<Exception> errors = new List<Exception>();

            if (order == null)
                throw new ArgumentNullException(nameof(order), $"No {nameof(ForceCloseDetails)} was supplied for an existing PO.");
            else 
            {
                if (order.Comment.Trim().Equals(null))
                    throw new Exception("Please give a reason in the comment box for why the order is being closed.");
            }
                

            using (var context = new eRaceContext())
            {
                var given = context.Orders.Find(order.OrderID);
                if (given == null)
                    throw new ArgumentException($"The given order id of {order.OrderID} does not exist in the database.");

                given.Comment = order.Comment;
                given.Closed = true;

                var existingOrder = context.Entry(given);

                existingOrder.Property(nameof(given.Comment)).IsModified = true;
                existingOrder.Property(nameof(given.Closed)).IsModified = true;

                foreach (var item in order.Items)
                {
                    var givenOrder = context.OrderDetails.Find(item.ProductID);
                    var givenProduct = context.Products.Find(givenOrder.ProductID);

                    if (givenProduct == null)
                        throw new ArgumentException($"The given product id of {item.ProductID} does not exist in the database.");

                    givenProduct.QuantityOnOrder = givenProduct.QuantityOnOrder - item.QuantityOutstanding;

                    if (givenProduct.QuantityOnOrder < 0) givenProduct.QuantityOnOrder = 0;

                    var existingProduct = context.Entry(givenProduct);

                    existingProduct.Property(nameof(givenProduct.QuantityOnOrder)).IsModified = true;
                }   

                context.SaveChanges();
            }
        }

       
        public void ReceiveShimpmentOrder(ReceivedOrderDetails order)
        { /* command modify Products, ReceiveOrders, ReceiveOrderItems and ReturnOrderItems */
            List<Exception> errors = new List<Exception>();

            using (var context = new eRaceContext())
            {
                var existing = context.Orders.Find(order.OrderID);

                if (order == null)
                    errors.Add(new ArgumentNullException(nameof(order), $"No {nameof(order)} was supplied for "));
                else
                {
                    if (existing.Closed == true)
                        errors.Add(new BusinessRuleException<bool>("The order you are accessing is already closed.", nameof(existing.Closed), existing.Closed));
                    if (order.OrderID == 0)
                        errors.Add(new BusinessRuleException<int>("The OrderID is required.",nameof(order.OrderID),order.OrderID));
                    if (order.ReceivedOrder.OrderID == 0)
                        errors.Add(new BusinessRuleException<int>("The OrderID is required.", nameof(order.ReceivedOrder.OrderID), order.ReceivedOrder.OrderID));
                    if (order.ReceivedOrder.EmployeeID == 0)
                        errors.Add(new BusinessRuleException<int>("The Employee ID is required.", nameof(order.ReceivedOrder.EmployeeID), order.ReceivedOrder.EmployeeID));
                    
                    foreach (var item in order.ReceiveOrderItems)
                    {
                        var existingOrderDetail = context.OrderDetails.Find(item.OrderDetailID);
                        var itemsize = context.OrderDetails.Find(item.OrderDetailID).OrderUnitSize;
                        if (existingOrderDetail == null)
                            errors.Add(new ArgumentNullException(nameof(item.OrderDetailID), $"There is no Order Detail with the ID given."));
                        if (item.ItemQuantity < 1)
                            errors.Add(new BusinessRuleException<int>("The item quantity must be greater than 0",nameof(item.ItemQuantity), item.ItemQuantity));
                        if (item.ItemQuantity / itemsize >= context.OrderDetails.Find(item.OrderDetailID).Quantity + 1)
                            errors.Add(new BusinessRuleException<int>("The total amount received cannot exceed the amount ordered by 1 package.", nameof(item.ItemQuantity), item.ItemQuantity));
                    }

                    foreach (var item in order.Products)
                    {
                        if (item.QuantityReceived < 1)
                            errors.Add(new BusinessRuleException<int>("The number of received items must be postive. ", nameof(item.QuantityReceived), item.QuantityReceived));
                    }

                    foreach (var item in order.ReturnOrderItems)
                    {
                        if (item.ItemQuantity < 1)
                            errors.Add(new BusinessRuleException<int>("The number of returned items is required.",nameof(item.ItemQuantity), item.ItemQuantity));
                    }
                }

                if (errors.Any())
                    throw new BusinessRuleCollectionException("Unable to receive shipment.", errors);

                // ReceiveOrder
                var newReceiveOrder = new ReceiveOrder
                {
                    OrderID = order.ReceivedOrder.OrderID,
                    ReceiveDate = order.ReceivedOrder.ReceiveDate,
                    EmployeeID = order.ReceivedOrder.EmployeeID
                };
                context.ReceiveOrders.Add(newReceiveOrder);

                int id = newReceiveOrder.ReceiveOrderID;
                
                                  
                // ReceiveOrderItems
                foreach (var item in order.ReceiveOrderItems)
                {
                    var newReceiveOrderItem = new ReceiveOrderItem
                    {
                        ReceiveOrderID = id,
                        OrderDetailID = item.OrderDetailID,
                        ItemQuantity = item.ItemQuantity
                    };
                    context.ReceiveOrderItems.Add(newReceiveOrderItem);
                }

                // ReturnOrderItems
                foreach (var item in order.ReturnOrderItems)
                {
                    var newReturnOrderItem = new ReturnOrderItem
                    {
                        ReceiveOrderID = id,
                        OrderDetailID = item.OrderDetailID,
                        ItemQuantity = item.ItemQuantity,
                        Comment = item.Comment,
                        UnOrderedItem = item.UnOrderedItem,
                        VendorProductID = item.VendorProductID
                    };
                    context.ReturnOrderItems.Add(newReturnOrderItem);
                }

                // Products
                foreach (var item in order.Products)
                {
                    var given = context.Products.Find(context.OrderDetails.Find(item.ProductID).ProductID);

                    if (given == null)
                        throw new ArgumentException($"The given product id of {item.ProductID} does not exist in the database.", nameof(item.ProductID));

                    given.QuantityOnHand = given.QuantityOnHand + item.QuantityReceived;
                    given.QuantityOnOrder = given.QuantityOnOrder - item.QuantityReceived;

                    if (given.QuantityOnOrder < 0)
                        given.QuantityOnOrder = 0;

                    var existingProduct = context.Entry(given);

                    existingProduct.Property(nameof(given.QuantityOnHand)).IsModified = true;
                    existingProduct.Property(nameof(given.QuantityOnOrder)).IsModified = true;
                }

                // Check if the order has been filled
                var outstanding = context.Orders.Find(order.OrderID).OrderDetails;
                var num = 0;
                foreach (var item in outstanding)
                {
                    if (context.OrderDetails.Find(item.OrderDetailID).Quantity * context.OrderDetails.Find(item.OrderDetailID).OrderUnitSize - (context.OrderDetails.Find(item.OrderDetailID).ReceiveOrderItems.Select(x => x.ItemQuantity)).FirstOrDefault() == 0)
                        num++;
                }
                if (num == outstanding.Count)
                {
                    var givenOrder = context.Orders.Find(order.OrderID);

                    givenOrder.Closed = true;
                    givenOrder.Comment = "Order Complete";

                    var existingOrder = context.Entry(givenOrder);

                    existingOrder.Property(nameof(givenOrder.Comment)).IsModified = true;
                    existingOrder.Property(nameof(givenOrder.Closed)).IsModified = true;
                }

                context.SaveChanges();
            }
                
        }

        #region Unordered CRUD

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<UnorderedInfo> ListUnorderedItems()
        {
            using (var context = new eRaceContext())
            {
                var results = from item in context.UnOrderedItems
                              select new UnorderedInfo
                              {
                                  ItemID = item.ItemID,
                                  ItemName = item.ItemName,
                                  VendorID = item.VendorProductID,
                                  Quantity = item.Quantity
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void AddUnorderedItem(UnorderedInfo item)
        {
            using (var context = new eRaceContext())
            {
                var newItem = new UnOrderedItem
                {
                    ItemName = item.ItemName,
                    VendorProductID = item.VendorID,
                    Quantity = item.Quantity
                };

                context.UnOrderedItems.Add(newItem);
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteUnorderedItem(UnorderedInfo item)
        {
            using (var context = new eRaceContext())
            {
                var existing = context.UnOrderedItems.Find(item.ItemID);               
                context.UnOrderedItems.Remove(existing);
                context.SaveChanges();
            }
        }

        #endregion
    }
}
