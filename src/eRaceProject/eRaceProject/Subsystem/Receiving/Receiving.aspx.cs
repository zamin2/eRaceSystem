using eRaceProject.Admin.Security;
using eRaceSystem.BLL.Receiving;
using eRaceSystem.ViewModels.Receiving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eRaceProject.Subsystem.Receiving
{
    public partial class Receiving : System.Web.UI.Page
    {
        private int? EmployeeId
        {
            get
            {
                int? id = null;
                if (Request.IsAuthenticated)
                {
                    var controller = new SecurityController();
                    id = controller.GetCurrentUserEmployeeId(User.Identity.Name);
                }
                return id;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Request.IsAuthenticated && EmployeeId.HasValue))
            {
                Response.Redirect("~/Account/Login", true);
            }               
            //else if (!User.IsInRole(Settings.ClerkRole) || !User.IsInRole(Settings.FoodServiceRole))
            //{
            //    Response.Redirect("~/Subsystem/Receiving/Default");
            //}
            
        }

        protected void Open_Click(object sender, EventArgs e)
        {
            var controller = new ReceivingController();

            controller.ClearUnOrderedItems();

            UnorderedListView.DataSourceID = "UnorderedODS";

            if (OrderListDropDown.SelectedIndex > 0)
            {
                var vendorinfo = controller.GetVendorContact(int.Parse(OrderListDropDown.SelectedValue));
                
                CompanyName.Text = vendorinfo.Name;
                CompanyAddress.Text = vendorinfo.Address;
                CompanyContact.Text = vendorinfo.Contact;
                CompanyPhone.Text = vendorinfo.Phone;

                var iteminfo = controller.GetOrderDetails(int.Parse(OrderListDropDown.SelectedValue));
                OrderGridView.DataSource = iteminfo;
                OrderGridView.DataBind();

                ReceiveShimpment.Visible = true;
                UnorderedLabel.Visible = true;
                UnorderedListView.Visible = true;
                ForceClose.Visible = true;
                Comments.Visible = true;
              
            }
            else
            {
                CompanyName.Text = null;
                CompanyAddress.Text = null;
                CompanyContact.Text = null;
                CompanyPhone.Text = null;

                OrderGridView.DataSource = null;
                OrderGridView.DataBind();

                ReceiveShimpment.Visible = false;
                UnorderedLabel.Visible = false;
                UnorderedListView.Visible = false;
                ForceClose.Visible = false;
                Comments.Visible = false;
            }


        }

        protected void ReceiveShimpment_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() => {
                int result = 0;
                var controller = new ReceivingController();
                var receivedOrderDetail = new ReceivedOrderDetails();
                var receivedOrderInfo = new ReceiveOrdersInfo();
                var receivedOrderItems = new List<ReceiveOrderItemDetails>();
                var productDetails = new List<ProductDetails>();
                var returnedOrderItems = new List<ReturnOrderItemDetails>();

                receivedOrderInfo.OrderID = int.Parse(OrderListDropDown.SelectedValue);
                receivedOrderInfo.ReceiveDate = DateTime.Now;
                receivedOrderInfo.EmployeeID = EmployeeId.Value;

                foreach (ListViewItem item in UnorderedListView.Items)
                {
                    var unorderedItem = new ReturnOrderItemDetails();

                    var unOrderedItemController = item.FindControl("ItemNameLabel") as Label;
                    var itemQuantityController = item.FindControl("QuantityLabel") as Label;
                    var vendorController = item.FindControl("VendorIDLabel") as Label;

                    unorderedItem.Comment = "Not on original order";
                    unorderedItem.UnOrderedItem = unOrderedItemController.ToString();
                    unorderedItem.ItemQuantity = int.Parse(itemQuantityController.Text);
                    unorderedItem.VendorProductID = vendorController.ToString();

                    returnedOrderItems.Add(unorderedItem);
                }

                foreach (GridViewRow item in OrderGridView.Rows)
                {
                    var unorderedItem = new ReturnOrderItemDetails();

                    var itemQuantityController = item.FindControl("RejectedUnits") as TextBox;
                    var commentController = item.FindControl("RejectedReason") as TextBox;
                    var orderDetailIDController = item.FindControl("OrderDetailID") as Label;

                    if (int.TryParse(itemQuantityController.Text, out result))
                    {
                        if(int.Parse(itemQuantityController.Text) > 0)
                        {
                            unorderedItem.OrderDetailID = int.Parse(orderDetailIDController.Text);
                            unorderedItem.Comment = commentController.ToString();
                            unorderedItem.ItemQuantity = int.Parse(itemQuantityController.Text);

                            returnedOrderItems.Add(unorderedItem);
                        }                       
                    }
                }

                foreach (GridViewRow item in OrderGridView.Rows)
                {
                    var newreceiveditem = new ReceiveOrderItemDetails();
                    var newProductInfo = new ProductDetails();

                    var salvagedItems = item.FindControl("SalvagedItems") as TextBox;
                    var receivedItems = item.FindControl("ReceivedUnits") as TextBox;
                    var orderDetailIDController = item.FindControl("OrderDetailID") as Label;
                    var unitSize = item.FindControl("UnitSize") as Label;

                    var salvage = 0;
                    var receive = 0;
                    var size = 0;

                    if (int.TryParse(unitSize.Text, out result) == true)
                    {
                        size = int.Parse(unitSize.Text);
                    }

                    if (int.TryParse(salvagedItems.Text, out result) == true)
                    {
                        salvage = int.Parse(salvagedItems.Text);
                    }

                    if (int.TryParse(receivedItems.Text, out result) == true)
                    {
                        receive = int.Parse(receivedItems.Text);
                    }


                    var totalQuantity = salvage + (receive * size);

                    if (totalQuantity > 0)
                    {
                        newreceiveditem.OrderDetailID = int.Parse(orderDetailIDController.Text);
                        newreceiveditem.ItemQuantity = totalQuantity;

                        newProductInfo.ProductID = int.Parse(orderDetailIDController.Text);
                        newProductInfo.QuantityReceived = totalQuantity;

                        receivedOrderItems.Add(newreceiveditem);
                        productDetails.Add(newProductInfo);
                    }                                 
                }
                receivedOrderDetail.OrderID = int.Parse(OrderListDropDown.SelectedValue);
                receivedOrderDetail.Products = productDetails;
                receivedOrderDetail.ReceivedOrder = receivedOrderInfo;
                receivedOrderDetail.ReceiveOrderItems = receivedOrderItems;
                receivedOrderDetail.ReturnOrderItems = returnedOrderItems;

                controller.ReceiveShimpmentOrder(receivedOrderDetail);
            }, "Receive Shipment", "Shipment Received");

            Response.Redirect("~/Subsystem/Receiving/Receiving");
        }

        protected void ForceClose_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {
                var controller = new ReceivingController();

                var closeOrder = new ForceCloseDetails();

                var itemInfo = new List<ForceCloseItems>();

                foreach (GridViewRow item in OrderGridView.Rows)
                {
                    var itemProductID = item.FindControl("OrderDetailID") as Label;
                    var itemOutstanding = item.FindControl("ItemQuantityOutstanding") as Label;

                    var product = new ForceCloseItems();

                    product.ProductID = int.Parse(itemProductID.Text);
                    product.QuantityOutstanding = int.Parse(itemOutstanding.Text);

                    itemInfo.Add(product);
                }

                closeOrder.OrderID = int.Parse(OrderListDropDown.SelectedValue);
                closeOrder.Closed = true;
                closeOrder.Comment = Comments.Text;
                closeOrder.Items = itemInfo;

                controller.ForceCloseOrder(closeOrder);
            }, "Force Close Order", "Order has been force closed");

            Response.Redirect("~/Subsystem/Receiving/Receiving");
        }
    }
}