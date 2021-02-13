using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eRaceSystem.BLL.Purchasing;
using eRaceSystem.BLL;
using eRaceSystem.ViewModels.Purchasing;
using FreeCode.Exceptions;
using eRaceProject.Admin.Security;

namespace eRaceProject.Subsystem.Purchasing
{
    public partial class Purchasing : System.Web.UI.Page
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

        public static int OrderID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!(Request.IsAuthenticated && EmployeeId.HasValue)) {
                Response.Redirect("~/Account/Login", true);

            }
            //else if (!(User.IsInRole(Settings.DirectorRole) || User.IsInRole(Settings.OfficeManagerRole))){
            //    Response.Redirect("~/Subsystem/Purchasing/Default");
            //}


            var controller = new eRaceController();
            var employee = controller.GetEmployeeName(EmployeeId);
            EmployeeUser.Text = $"Hello there! {employee.UserName} ({employee.EmployeeRole})";


            if (int.Parse(VendorDropDown.SelectedIndex.ToString()) == 0) {
                SaveOrder.Visible = false;
                PlaceOrder.Visible = false;
                DeleteOrder.Visible = false;
                Subtotal.Text = "0";
                Tax.Text = "0";
                Total.Text = "0";

            }
               
        }



        protected void VendorSelect_Click(object sender, EventArgs e)
        {
            if (int.Parse(VendorDropDown.SelectedValue.ToString()) != 0)
            {
                var controller = new PurchasingController();
                var info = controller.GetVendorInfo(int.Parse(VendorDropDown.SelectedValue.ToString()));
                VendorName.Text = info.Name;
                VendorContact.Text = info.Address + ", " + info.City + ", " + info.PostalCode;
                VendorPhone.Text = info.Phone;
                var info2 = controller.GetOrder(int.Parse(VendorDropDown.SelectedValue.ToString()));
                if (info2 != null)
                {
                    if (info2.OrderNumber != null)
                    {
                        PlaceOrder.Visible = false;
                        SaveOrder.Visible = false;
                        DeleteOrder.Visible = true;
                        messenger.Text = $"Order already placed in {info2.OrderDate}. OrderNumber: {info2.OrderNumber}";
                        OrderGridview.Enabled = false;
                        VendorRepeater.Visible = false;

                    }
                    else
                    {
                        OrderGridview.Enabled = true;
                        VendorRepeater.Visible = true;
                        DeleteOrder.Visible = true;
                        PlaceOrder.Visible = true;
                        SaveOrder.Visible = true;

                        messenger.Text = "";
                    }

                    Subtotal.Text = Math.Round(info2.SubTotal, 2).ToString();
                    Tax.Text = Math.Round(info2.Tax, 2).ToString();
                    Total.Text = Math.Round(info2.Total, 2).ToString();
                    OrderID = info2.OrderID;
                    if (!string.IsNullOrEmpty(info2.VendorComment))
                    {
                        VendorComments.Text = info2.VendorComment;
                    }
                    var x = info2.OrderDetails;
                    foreach (var i in x)
                    {
                        i.ExtendedCost = Math.Round(i.ExtendedCost, 2);
                        i.ItemPrice = Math.Round(i.ItemPrice, 2);
                        i.ItemCost = Math.Round(i.ItemCost, 2);
                        i.UnitCost = Math.Round(i.UnitCost, 2);
                    }
                    OrderGridview.DataSource = x;
                    OrderGridview.DataBind();

                }
                else
                {

                    Subtotal.Text = "0";
                    Tax.Text = "0";
                    Total.Text = "0";
                    VendorComments.Text = "";
                    OrderGridview.DataSource = null;
                    OrderGridview.DataBind();
                    VendorRepeater.Visible = true;
                    OrderGridview.Enabled = true;
                    messenger.Text = "";
                    OrderID = 0;
                    DeleteOrder.Visible = false;
                    PlaceOrder.Visible = true;
                    SaveOrder.Visible = true;
                }

                var info3 = controller.GetVendorProducts(int.Parse(VendorDropDown.SelectedValue.ToString()));
                if (info3 != null)
                {

                    VendorRepeater.DataSource = info3;
                    VendorRepeater.DataBind();
                    int q = 0;
                    foreach (var i in info3) // binding the gridviews of repeater
                    {
                        GridView a = VendorRepeater.Items[q].FindControl("VendorProductsGridview") as GridView;
                        a.DataSource = i.Items;
                        a.DataBind();
                        q++;
                    }

                }
                
                OrderGridview.Visible = true;
            }

            


        }

        protected void VendorProductsGridview_RowCommand(object sender, GridViewCommandEventArgs e) // adding products to purchase order
        {
            MessageUserControl.TryRun(() => {
                Subtotal.Text = "0";
                Tax.Text = "0";
                Total.Text = "0";

                GridView sourceGridview = e.CommandSource as GridView;

                List<OrderDetailInfo> outputList = new List<OrderDetailInfo>();
                List<VendorCatalogInfo> inputlist = new List<VendorCatalogInfo>();

                int selectedindex;
                if (int.TryParse(e.CommandArgument.ToString(), out selectedindex)) 
                {
                    foreach (GridViewRow row in sourceGridview.Rows) // taking data from gridview and copying it to a List
                    {
                        var cat = row.FindControl("CategoryName") as HiddenField;
                        var productid = row.FindControl("ProductID") as HiddenField;
                        var productname = row.FindControl("VendorProductName") as Label;
                        var reorderqty = row.FindControl("ReOrderLabel") as Label;
                        var instockqty = row.FindControl("InStockLabel") as Label;
                        var onorderqty = row.FindControl("OnOrderLabel") as Label;
                        var size = row.FindControl("UnitSize") as HiddenField;
                        var itemcost = row.FindControl("ItemCost") as HiddenField;

                        var vendorcataloginfo = new VendorCatalogInfo();
                        vendorcataloginfo.ProductID = int.Parse(productid.Value.ToString());
                        vendorcataloginfo.ProductName = productname.Text;
                        vendorcataloginfo.Category = cat.Value.ToString().Trim();
                        vendorcataloginfo.ReorderQty = int.Parse(reorderqty.Text.ToString());
                        vendorcataloginfo.QuantityOnHand = int.Parse(instockqty.Text.ToString());
                        vendorcataloginfo.QuantityOnOrder = int.Parse(onorderqty.Text.ToString());
                        vendorcataloginfo.ItemCost = decimal.Parse(itemcost.Value.ToString());
                        vendorcataloginfo.UnitSize = int.Parse(size.Value.ToString());


                        inputlist.Add(vendorcataloginfo);
                    }

                    foreach (GridViewRow row in OrderGridview.Rows) // taking data from gridview and copying it to a List
                    {
                        var productid = row.FindControl("ProductID") as HiddenField;
                        var productname = row.FindControl("ProductNameLabel") as Label;
                        var unitsize = row.FindControl("UnitSize") as HiddenField;
                        var itemprice = row.FindControl("ItemPriceLabel") as HiddenField;
                        var itemcost = row.FindControl("PerItemCost") as Label;
                        var orderqty = row.FindControl("OrderQtyLabel") as TextBox;
                        var unitcost = row.FindControl("UnitCostLabel") as TextBox;
                        var extendedcost = row.FindControl("ExtendedCostLabel") as Label;
                        var categoryname = row.FindControl("CategoryName") as HiddenField;
                        var reorderqty = row.FindControl("ReorderQty") as HiddenField;
                        var instockqty = row.FindControl("QuantityOnHand") as HiddenField;
                        var onorderqty = row.FindControl("QuantityOnOrder") as HiddenField;
                        var warning = row.FindControl("warning") as Label;

                        var orderdetailinfo = new OrderDetailInfo();
                        orderdetailinfo.ProductID = int.Parse(productid.Value.ToString());
                        orderdetailinfo.ProductName = productname.Text.Trim();
                        orderdetailinfo.UnitSize = int.Parse(unitsize.Value.ToString());
                        orderdetailinfo.ItemPrice = decimal.Parse(itemprice.Value.ToString());
                        orderdetailinfo.ItemCost = decimal.Parse(itemcost.Text.ToString());
                        orderdetailinfo.OrderQty = int.Parse(orderqty.Text.ToString());
                        orderdetailinfo.UnitCost = decimal.Parse(unitcost.Text.ToString());
                        orderdetailinfo.ExtendedCost = decimal.Parse(extendedcost.Text.ToString());
                        orderdetailinfo.CategoryName = categoryname.Value.ToString().Trim();
                        orderdetailinfo.ReorderQty = int.Parse(reorderqty.Value.ToString());
                        orderdetailinfo.QuantityOnHand = int.Parse(instockqty.Value.ToString());
                        orderdetailinfo.QuantityOnOrder = int.Parse(onorderqty.Value.ToString());
                        orderdetailinfo.warning = warning.Text;




                        outputList.Add(orderdetailinfo);

                    }

                    var addto = inputlist[selectedindex];
                    var orderdetailinfo2 = new OrderDetailInfo();
                    orderdetailinfo2.ProductID = addto.ProductID;
                    orderdetailinfo2.ProductName = addto.ProductName;
                    orderdetailinfo2.ItemPrice = Math.Round(addto.ItemCost, 2);
                    orderdetailinfo2.ItemCost = orderdetailinfo2.ItemPrice;
                    orderdetailinfo2.UnitSize = addto.UnitSize;
                    orderdetailinfo2.CategoryName = addto.Category;

                    orderdetailinfo2.OrderQty = 1;
                    orderdetailinfo2.UnitCost = orderdetailinfo2.ItemPrice * orderdetailinfo2.UnitSize;
                    orderdetailinfo2.ExtendedCost = orderdetailinfo2.UnitCost;
                    orderdetailinfo2.QuantityOnHand = addto.QuantityOnHand;
                    orderdetailinfo2.QuantityOnOrder = addto.QuantityOnOrder;
                    orderdetailinfo2.ReorderQty = addto.ReorderQty;

                    outputList.Add(orderdetailinfo2);
                    OrderGridview.DataSource = outputList;
                    OrderGridview.DataBind();

                    inputlist.RemoveAt(selectedindex);
                    sourceGridview.DataSource = inputlist;
                    sourceGridview.DataBind();

                }

            },"Success","Product successfully added to the Purchase Order Form");
        }

        protected void OrderGridview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            MessageUserControl.TryRun(() => {
                Subtotal.Text = "";
                Tax.Text = "";
                Total.Text = "";
                if (e.CommandName == "Refresh")
                {




                }
                else
                {
                    GridView sourceGridview = e.CommandSource as GridView;
                    List<VendorCatalogInfo> outputList = new List<VendorCatalogInfo>();
                    List<OrderDetailInfo> inputlist = new List<OrderDetailInfo>();
                    int selectedindex;
                    if (int.TryParse(e.CommandArgument.ToString(), out selectedindex))
                    {
                        foreach (GridViewRow row in sourceGridview.Rows) // taking data from gridview and copying it to a List
                        {
                            var productid = row.FindControl("ProductID") as HiddenField;
                            var categoryname = row.FindControl("CategoryName") as HiddenField;
                            var productname = row.FindControl("ProductNameLabel") as Label;
                            var unitsize = row.FindControl("UnitSize") as HiddenField;
                            var itemprice = row.FindControl("ItemPriceLabel") as HiddenField;
                            var itemcost = row.FindControl("PerItemCost") as Label;
                            var orderqty = row.FindControl("OrderQtyLabel") as TextBox;
                            var unitcost = row.FindControl("UnitCostLabel") as TextBox;
                            var extendedcost = row.FindControl("ExtendedCostLabel") as Label;
                            var reorderqty = row.FindControl("ReorderQty") as HiddenField;
                            var instockqty = row.FindControl("QuantityOnHand") as HiddenField;
                            var onorderqty = row.FindControl("QuantityOnOrder") as HiddenField;

                            var orderdetailinfo = new OrderDetailInfo(); 
                            orderdetailinfo.ProductID = int.Parse(productid.Value.ToString());
                            orderdetailinfo.ProductName = productname.Text.Trim();
                            orderdetailinfo.UnitSize = int.Parse(unitsize.Value.ToString());
                            orderdetailinfo.ItemPrice = decimal.Parse(itemprice.Value.ToString());
                            orderdetailinfo.ItemCost = decimal.Parse(itemcost.Text.ToString());
                            orderdetailinfo.OrderQty = int.Parse(orderqty.Text.ToString());
                            orderdetailinfo.UnitCost = decimal.Parse(unitcost.Text.ToString());
                            orderdetailinfo.ExtendedCost = decimal.Parse(extendedcost.Text.ToString());
                            orderdetailinfo.ReorderQty = int.Parse(reorderqty.Value.ToString());
                            orderdetailinfo.QuantityOnHand = int.Parse(instockqty.Value.ToString());
                            orderdetailinfo.QuantityOnOrder = int.Parse(onorderqty.Value.ToString());
                            orderdetailinfo.CategoryName = categoryname.Value.ToString();

                            inputlist.Add(orderdetailinfo);

                        }

                        foreach (RepeaterItem item in VendorRepeater.Items)
                        {
                            var catname = item.FindControl("CategoryName") as Label; 
                            if (catname.Text.Trim() == inputlist[selectedindex].CategoryName.Trim()) // filtering by category
                            {
                                GridView outGridview = item.FindControl("VendorProductsGridview") as GridView; // taking data from gridview and copying it to a List
                                foreach (GridViewRow row in outGridview.Rows)
                                {
                                    var productname = row.FindControl("VendorProductName") as Label;
                                    var productid = row.FindControl("ProductID") as HiddenField;
                                    var reorderqty = row.FindControl("ReOrderLabel") as Label;
                                    var instockqty = row.FindControl("InStockLabel") as Label;
                                    var onorderqty = row.FindControl("OnOrderLabel") as Label;
                                    var size = row.FindControl("UnitSize") as HiddenField;
                                    var itemcost = row.FindControl("ItemCost") as HiddenField;
                                    var cat = row.FindControl("CategoryName") as HiddenField;

                                    var vendorcataloginfo = new VendorCatalogInfo();
                                    vendorcataloginfo.ProductID = int.Parse(productid.Value.ToString());
                                    vendorcataloginfo.ProductName = productname.Text;
                                    vendorcataloginfo.Category = cat.Value.ToString().Trim();
                                    vendorcataloginfo.ReorderQty = int.Parse(reorderqty.Text.ToString());
                                    vendorcataloginfo.QuantityOnHand = int.Parse(instockqty.Text.ToString());
                                    vendorcataloginfo.QuantityOnOrder = int.Parse(onorderqty.Text.ToString());
                                    vendorcataloginfo.ItemCost = decimal.Parse(itemcost.Value.ToString());
                                    vendorcataloginfo.UnitSize = int.Parse(size.Value.ToString());

                                    outputList.Add(vendorcataloginfo);

                                }
                                break;
                            }
                        }

                        var addto = inputlist[selectedindex];
                        var vendorcatinfo = new VendorCatalogInfo();
                        vendorcatinfo.ProductName = addto.ProductName;
                        vendorcatinfo.ReorderQty = addto.ReorderQty;
                        vendorcatinfo.QuantityOnHand = addto.QuantityOnHand;
                        vendorcatinfo.QuantityOnOrder = addto.QuantityOnOrder;
                        vendorcatinfo.UnitSize = addto.UnitSize;
                        vendorcatinfo.ItemCost = addto.ItemPrice;
                        vendorcatinfo.Category = addto.CategoryName;
                        outputList.Add(vendorcatinfo);


                        foreach (RepeaterItem item in VendorRepeater.Items)
                        {
                            var catname = item.FindControl("CategoryName") as Label;
                            if (catname.Text.Trim() == inputlist[selectedindex].CategoryName.Trim())
                            {
                                GridView outGridview = item.FindControl("VendorProductsGridview") as GridView;
                                outGridview.DataSource = outputList;
                                outGridview.DataBind();
                                break;

                            }
                        }
                        inputlist.RemoveAt(selectedindex);
                        sourceGridview.DataSource = inputlist;
                        sourceGridview.DataBind();
                    }
                }
            },"Success","Product successfully removed from the Purchase Order form");
            

        }

        protected void SaveOrder_Click(object sender, EventArgs e)
        {
            List<Exception> errors = new List<Exception>();
            MessageUserControl.TryRun(() =>
            {
                int vendorid = int.Parse(VendorDropDown.SelectedValue.ToString());
                string comments = VendorComments.Text;
                string subtotal = Subtotal.Text;
                string tax = Tax.Text;
                string total = Total.Text;


                decimal subtotalcount = 0;
                List<UpdatedOrderDetail> orderitems = new List<UpdatedOrderDetail>();
                if (!(OrderGridview.Rows.Count < 1))
                {
                    
                    foreach (GridViewRow row in OrderGridview.Rows)
                    {
                        var item = new UpdatedOrderDetail();
                        item.ProductID = int.Parse((row.FindControl("ProductID") as HiddenField).Value.ToString());
                        string a = (row.FindControl("UnitCostLabel") as TextBox).Text.ToString();
                        string b = (row.FindControl("OrderQtyLabel") as TextBox).Text.ToString();
                        if (String.IsNullOrEmpty(a) || String.IsNullOrEmpty(b))
                        {
                            throw new Exception("Please fill up the Order Quantity and Unit Cost textboxes before proceeding");
                        }

                        item.UnitCost = decimal.Parse(a);

                        item.OrderQty = int.Parse((row.FindControl("OrderQtyLabel") as TextBox).Text.ToString());
                        item.UnitSize = int.Parse((row.FindControl("UnitSize") as HiddenField).Value.ToString());
                        decimal itemprice = decimal.Parse((row.FindControl("ItemPriceLabel") as HiddenField).Value.ToString());
                        decimal extendedprice = decimal.Parse((row.FindControl("ExtendedCostLabel") as Label).Text.ToString());
                        subtotalcount += (item.UnitCost * item.OrderQty);
                        if (extendedprice != (item.UnitCost * item.OrderQty))
                        {
                            errors.Add(new Exception("Please press the refresh button to update the extended costs of the product rows"));
                        }
                        else
                        {
                            if (item.UnitCost <= 0)
                            {
                                errors.Add(new Exception("Unit Cost can not be zero or negative value. Please review the product rows with !!"));
                            }
                            if (item.OrderQty <= 0)
                            {
                                errors.Add(new Exception("Order Quantity can not be zero or negative value. Please review the product rows with !!"));
                            }
                            else if (itemprice > (item.UnitCost / item.UnitSize))
                            {
                                errors.Add(new Exception("Per Item cost is lower than the item's selling price. Please review the product rows with !!"));
                            }
                        }

                        orderitems.Add(item);
                    }
                    if (subtotal == "0")
                    {
                        errors.Add(new Exception("Subtotal can not be zero. Please press the refresh button"));
                    }


                    if (errors.Any())
                    {
                        throw new BusinessRuleCollectionException("Failure to proceed with the Save request", errors);
                    }
                    else
                    {
                        UpdatedOrder order = new UpdatedOrder();
                        order.OrderID = OrderID;

                        order.VendorComment = comments;
                        order.SubTotal = decimal.Parse(subtotal.ToString());
                        order.Tax = decimal.Parse(tax.ToString());
                        order.Total = decimal.Parse(total.ToString());
                        order.VendorID = vendorid;
                        order.UpdatedOrderDetails = orderitems;
                        var controller = new PurchasingController();
                        controller.SaveOrder(order, EmployeeId);
                        DeleteOrder.Visible = true;
                        messenger.Text = $"Order successfully saved";
                    }





                }
                else
                {
                    throw new Exception("Please select at least one Product item from the Vendor Catalog");
                }





            }, "Success", "Order successfully saved");
                




        }

        protected void refreshbutton_Click(object sender, ImageClickEventArgs e)
        {
            GridView sourceGridview = OrderGridview as GridView;
            List<OrderDetailInfo> inputlist = new List<OrderDetailInfo>();
            foreach (GridViewRow row in sourceGridview.Rows) // taking data from gridview and copying it to a List
            {
                var productid = row.FindControl("ProductID") as HiddenField;
                var categoryname = row.FindControl("CategoryName") as HiddenField;
                var productname = row.FindControl("ProductNameLabel") as Label;
                var unitsize = row.FindControl("UnitSize") as HiddenField;
                var itemprice = row.FindControl("ItemPriceLabel") as HiddenField;
                var itemcost = row.FindControl("PerItemCost") as Label;
                var orderqty = row.FindControl("OrderQtyLabel") as TextBox;
                var unitcost = row.FindControl("UnitCostLabel") as TextBox;
                var extendedcost = row.FindControl("ExtendedCostLabel") as Label;
                var reorderqty = row.FindControl("ReorderQty") as HiddenField;
                var instockqty = row.FindControl("QuantityOnHand") as HiddenField;
                var onorderqty = row.FindControl("QuantityOnOrder") as HiddenField;
                

                var orderdetailinfo = new OrderDetailInfo();
                orderdetailinfo.ProductID = int.Parse(productid.Value.ToString());
                orderdetailinfo.ProductName = productname.Text.Trim();
                orderdetailinfo.UnitSize = int.Parse(unitsize.Value.ToString());
                orderdetailinfo.ItemPrice = decimal.Parse(itemprice.Value.ToString());
                orderdetailinfo.OrderQty = int.Parse(orderqty.Text.ToString());
                orderdetailinfo.UnitCost = decimal.Parse(unitcost.Text.ToString());
                orderdetailinfo.ItemCost = orderdetailinfo.UnitCost / orderdetailinfo.UnitSize;
                orderdetailinfo.ExtendedCost = orderdetailinfo.OrderQty * orderdetailinfo.UnitCost;
                orderdetailinfo.ReorderQty = int.Parse(reorderqty.Value.ToString());
                orderdetailinfo.QuantityOnHand = int.Parse(instockqty.Value.ToString());
                orderdetailinfo.QuantityOnOrder = int.Parse(onorderqty.Value.ToString());
                orderdetailinfo.CategoryName = categoryname.Value.ToString();
                

                inputlist.Add(orderdetailinfo);
            }



            decimal subtotal = 0;
            decimal tax;
            decimal total;
            foreach (var a in inputlist)
            {
                subtotal += a.ExtendedCost;
                if (a.ItemCost < a.ItemPrice)
                {
                    a.warning = "!!";
                }
                else
                {
                    a.warning = "";
                }

            }
            tax = subtotal * 5 / 100;
            total = subtotal + tax;

            Subtotal.Text = Math.Round(subtotal,2).ToString();
            Tax.Text = Math.Round(tax,2).ToString();
            Total.Text = Math.Round(total,2).ToString();

            OrderGridview.DataSource = inputlist;
            OrderGridview.DataBind();
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Subtotal.Text = "0";
            Tax.Text = "0";
            Total.Text = "0";
            VendorRepeater.Visible = false;
            OrderGridview.Visible = false;
            DeleteOrder.Visible = false;
            PlaceOrder.Visible = false;
            SaveOrder.Visible = false;
   
            VendorDropDown.SelectedIndex = 0;
            VendorComments.Text = "";
            VendorContact.Text = "";
            VendorName.Text = "";
            VendorPhone.Text = "";
            messenger.Text = "";
        }

        protected void DeleteOrder_Click(object sender, EventArgs e)
        {

            MessageUserControl.TryRun(() => {
                var controller = new PurchasingController();
                var order = controller.GetOrder(int.Parse(VendorDropDown.SelectedValue.ToString()));

                controller.DeleteOrder(order);
                Subtotal.Text = "0";
                Tax.Text = "0";
                Total.Text = "0";
                VendorRepeater.Visible = false;
                OrderGridview.Visible = false;
                SaveOrder.Visible = false;
                PlaceOrder.Visible = false;
                DeleteOrder.Visible = false;
                VendorDropDown.SelectedIndex = 0;
                VendorComments.Text = "";
                VendorContact.Text = "";
                VendorName.Text = "";
                VendorPhone.Text = "";
                messenger.Text = "";
            },"Success","Order successfully deleted");


            

        }

        protected void PlaceOrder_Click(object sender, EventArgs e)
        {
            List<Exception> errors = new List<Exception>();
            MessageUserControl.TryRun(() => {
                var controller = new PurchasingController();
                int vendorid = int.Parse(VendorDropDown.SelectedValue.ToString());
                string comments = VendorComments.Text;
                string subtotal = Subtotal.Text;
                string tax = Tax.Text;
                string total = Total.Text;

                decimal subtotalcount = 0;
                List<UpdatedOrderDetail> orderitems = new List<UpdatedOrderDetail>();
                if (!(OrderGridview.Rows.Count < 1))
                {
                    foreach (GridViewRow row in OrderGridview.Rows)
                    {
                        var item = new UpdatedOrderDetail();
                        item.ProductID = int.Parse((row.FindControl("ProductID") as HiddenField).Value.ToString());
                        string a = (row.FindControl("UnitCostLabel") as TextBox).Text.ToString();
                        string b = (row.FindControl("OrderQtyLabel") as TextBox).Text.ToString();
                        if (String.IsNullOrEmpty(a) || String.IsNullOrEmpty(b))
                        {
                            throw new Exception("Please fill up the Order Quantity and Unit Cost textboxes before proceeding");
                        }
                        item.UnitCost = decimal.Parse(a);
                        item.OrderQty = int.Parse((row.FindControl("OrderQtyLabel") as TextBox).Text.ToString());
                        item.UnitSize = int.Parse((row.FindControl("UnitSize") as HiddenField).Value.ToString());
                        decimal itemprice = decimal.Parse((row.FindControl("ItemPriceLabel") as HiddenField).Value.ToString());
                        decimal extendedprice = decimal.Parse((row.FindControl("ExtendedCostLabel") as Label).Text.ToString());
                        subtotalcount += (item.UnitCost * item.OrderQty);
                        if (extendedprice != (item.UnitCost * item.OrderQty))
                        {
                            errors.Add(new Exception("Please press the refresh button to update the extended costs of the product rows"));
                        }
                        else
                        {
                            if (item.UnitCost <= 0)
                            {
                                errors.Add(new Exception("Unit Cost can not be zero or negative value. Please review the product rows with !!"));
                            }
                            if (item.OrderQty <= 0)
                            {
                                errors.Add(new Exception("Order Quantity can not be zero or negative value. Please review the product rows with !!"));
                            }
                            else if (itemprice > (item.UnitCost / item.UnitSize))
                            {
                                errors.Add(new Exception("Per Item cost is lower than the item's selling price. Please review the product rows with !!"));
                            }
                        }

                        orderitems.Add(item);
                    }
                    if (subtotal == "0")
                    {
                        errors.Add(new Exception("Subtotal can not be zero. Please press the refresh button"));
                    }


                    if (errors.Any())
                    {
                        throw new BusinessRuleCollectionException("Failure to proceed with the Save request", errors);
                    }
                    else
                    {
                        UpdatedOrder order = new UpdatedOrder();
                        var orderexists = controller.GetOrder(vendorid);
                        if (orderexists != null) // checking if pending order exists
                        {
                            order.OrderID = orderexists.OrderID;
                        }
                        else
                        {
                            order.OrderID = OrderID;
                        }


                        order.VendorComment = comments;
                        order.SubTotal = decimal.Parse(subtotal.ToString());
                        order.Tax = decimal.Parse(tax.ToString());
                        order.Total = decimal.Parse(total.ToString());
                        order.VendorID = vendorid;
                        order.UpdatedOrderDetails = orderitems;



                        controller.PlaceOrder(order);
                        DeleteOrder.Visible = false;
                        PlaceOrder.Visible = false;
                        SaveOrder.Visible = false;
                        messenger.Text = $"Order successfully placed";
                        OrderGridview.Enabled = false;
                        VendorRepeater.Visible = false;
                        VendorComments.Enabled = false;
                    }

                }
                else
                {
                    throw new Exception("Please select at least one Product item from the Vendor Catalog");
                }

                


            },"Success","Order successfully placed");
            
        }
    }
}