using eRaceProject.Admin.Security;
using eRaceSystem.BLL;
using eRaceSystem.BLL.Sales;
using eRaceSystem.ViewModels;
using eRaceSystem.ViewModels.Sale;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eRaceProject.Subsystem.Sales
{
    public partial class Sales : System.Web.UI.Page
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
            else if (!User.IsInRole(Settings.ClerkRole))
            {
                Response.Redirect("~/Account/Login", true);
            }

            var controller2 = new eRaceController();
            var employee = controller2.GetEmployeeName(EmployeeId);
            EmployeeUser.Text = $"Hello there! {employee.UserName} ({employee.EmployeeRole})";

            if (!IsPostBack)
            {
                var controller = new SalesController();
                var date = controller.ListCategory();
                CategoryList.DataSource = date;
                CategoryList.DataTextField = nameof(CategoryInfo.CategoryName);
                CategoryList.DataValueField = nameof(CategoryInfo.ID);
                CategoryList.DataBind();
                CategoryList.Items.Insert(0, new ListItem("select a category", "0"));
            }
           
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            
            var controller = new SalesController();         
            var qty = int.Parse(QtyTextBox.Text);
            var existing = SaleCartItemsGridView.TemplateControl.FindControl("ProductLabel") as Label;


            if (CategoryList.SelectedIndex == 0)
            {
                MessageUserControl.ShowInfo("please select a product");

            }
            else
            {
                if (qty <= 0)
                {
                    var product = controller.GetProduct(int.Parse(productList.SelectedValue));
                    QtyTextBox.Text = 1.ToString();

                    if (existing == null)
                    {

                        var item = new SaleCartItem
                        {
                            ProductID = product.ProductID,
                            ProductName = product.ItemName,
                            Quantity = int.Parse(QtyTextBox.Text),
                            Price = product.ItemPrice,
                            Amount = product.ItemPrice * int.Parse(QtyTextBox.Text),
                        };

                        var salecart = Loopform();
                        salecart.Add(item);

                        SaleCartItemsGridView.DataSource = salecart;
                        SaleCartItemsGridView.DataBind();
                        MessageUserControl.ShowInfo("Add item to cart success ");
                        MoneyTotal();

                    }
                    else
                    {
                        MessageUserControl.ShowInfo("item already in cart ");
                    }


                }
                else
                {
                    var product = controller.GetProduct(int.Parse(productList.SelectedValue));
                    var item = new SaleCartItem
                    {
                        ProductID = product.ProductID,
                        ProductName = product.ItemName,
                        Quantity = qty,
                        Price = product.ItemPrice,
                        Amount = product.ItemPrice * qty,
                    };

                    var salecart = Loopform();
                    salecart.Add(item);

                    SaleCartItemsGridView.DataSource = salecart;
                    SaleCartItemsGridView.DataBind();
                    MessageUserControl.ShowInfo("Add item to cart success ");
                    MoneyTotal();

                }
            }
            

        }

        private IList<SaleCartItem> Loopform()
        {
            var product = new List<SaleCartItem>();

            foreach(GridViewRow  item in SaleCartItemsGridView.Rows)
            {

                product.Add(Displayitem(item));
            }
            return product;

        }

        private SaleCartItem Displayitem(GridViewRow row)
        {
            var id = row.FindControl("ProductIDLabel") as Label;
            var name = row.FindControl("ProductLabel") as Label;
            var qty = row.FindControl("QuantityTextBox") as TextBox;
            var price = row.FindControl("PriceLabel") as Label;
            
   

            var result = new SaleCartItem
            {
                ProductID = int.Parse(id.Text),
                ProductName = name.Text,
                Quantity=int.Parse(qty.Text),
                Price = decimal.Parse(price.Text),
                Amount = decimal.Parse(price.Text) * int.Parse(qty.Text)

            };

            return result;
        }

        protected void SaleCartItemsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           

            switch(e.CommandName)
            {
                case "DeleteQuote":


                            int productId = int.Parse(e.CommandArgument.ToString());          
                            var items = Loopform();
                            var toRemove = items.FirstOrDefault(x => x.ProductID == productId);
                            items.Remove(toRemove);
                            SaleCartItemsGridView.DataSource = items;
                            SaleCartItemsGridView.DataBind();
                            MoneyTotal();
                            MessageUserControl.ShowInfo("The items is delete");


                    break;

                case "Refresh":

                 
                            var form = Loopform();
                            SaleCartItemsGridView.DataSource = form;
                            SaleCartItemsGridView.DataBind();
                            MoneyTotal();
                            MessageUserControl.ShowInfo("The Price is update");
                     
                    break;

            }
        }
  
        protected void PayButton_Click(object sender, EventArgs e)
        {
            var form = Loopform();
            SaleCartItemsGridView.DataSource = form;
            SaleCartItemsGridView.DataBind();
            MoneyTotal();
            


            string userName = User.Identity.Name;
            int employeeID = Get_EmpID(userName);
            List<NewInvoiceDetail> invoiceRows = new List<NewInvoiceDetail>();
            List<string> reasons = new List<string>();
            
            
            
            NewInvoiceDetail newDetail = null;
            foreach (GridViewRow item in this.SaleCartItemsGridView.Rows)
            {
                newDetail = new NewInvoiceDetail();
                newDetail.ProductID = Get_ProductID((item.FindControl("ProductLabel") as Label).Text);
                newDetail.Quantity = int.Parse((item.FindControl("QuantityTextBox") as TextBox).Text);
                
                SalesController controller= new SalesController();
             
                
                    newDetail.Price = Get_ProductPrice(Get_ProductID((item.FindControl("ProductLabel") as Label).Text));
                    invoiceRows.Add(newDetail);
                
            }
            if (SubtotalTextBox.Text =="$0")
            {
                MessageUserControl.ShowInfo("please add some product");
            }
            else
            {



               
                    NewInvoice invoice = new NewInvoice();
                    invoice.EmployeeID = employeeID;
                    invoice.InvoiceDate = DateTime.Now;
                    invoice.Subtotal = decimal.Parse(SubtotalTextBox.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                    invoice.GST = decimal.Parse(GSTTextBox.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                    invoice.Total = decimal.Parse(TotalTextBox.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                    invoice.NewDetails = invoiceRows;

                    //pass to BLL
                    SaleCartItemsController controller = new SaleCartItemsController();
                    int rows = controller.Pay_ForCart(invoice);
                    MessageUserControl.ShowInfo("Payment successful!");

                    foreach (GridViewRow loop in this.SaleCartItemsGridView.Rows)
                    {
                        var qty = loop.FindControl("QuantityTextBox") as TextBox;
                        var Refreshbutton = loop.FindControl("RefreshButton") as ImageButton;
                        var deletebutton = loop.FindControl("ClearItemButton") as ImageButton;   
                        Refreshbutton.Enabled = false;
                        deletebutton.Enabled = false;
                        qty.Enabled = false;
                    }
                    AddButton.Enabled = false;
                    PayButton.Enabled = false;
                    QtyTextBox.Enabled = false;
                    CategoryList.Enabled = false;
                    productList.Enabled = false;

                NewIdBox.Text = Get_NEW_Invocieid().ToString();

             
            }

       

        }

        #region
        public int Get_NEW_Invocieid()
        {
            SalesController controller = new SalesController();
            int id = controller.ReturnNewInvoiceID();
            return id;
        }
        public int Get_EmpID(string userName)
        {
            SecurityController controller = new SecurityController();
            return controller.GetCurrentUserEmployeeId(userName).Value;
        }

        public decimal Get_ProductPrice(int productID)
        {
            SalesController controller = new SalesController();
            decimal price = controller.GetProduct(productID).ItemPrice;
            return price;
        }

        public string GetProductName(int productid)
        {
            SalesController controller = new SalesController();
            string Name = controller.GetProduct(productid).ItemName;
            return Name;
        }
        public int Get_ProductID(string productName)
        {
            SalesController controller = new SalesController();
            int id = controller.Product_Get(productName).ProductID;
            return id;
        }

       #endregion
        public void MoneyTotal()
        {
            int qty = 0;
            decimal price = 0;
            decimal subtotal = 0;
            decimal tax = 0;
            decimal total = 0;

          
            foreach (GridViewRow item in this.SaleCartItemsGridView.Rows)
            {
               
                qty = int.Parse((item.FindControl("QuantityTextBox") as TextBox).Text);
                price = Get_ProductPrice(Get_ProductID((item.FindControl("ProductLabel") as Label).Text));
                subtotal = subtotal + (qty * price);
                tax = subtotal * ((decimal)5.00 / 100);
                total = subtotal + tax;
            }

         
            SubtotalTextBox.Text = string.Format("${0:C}", subtotal.ToString());
            GSTTextBox.Text = string.Format("${0:C}", tax.ToString());
            TotalTextBox.Text = string.Format("${0:C}", total.ToString());
        }

        protected void ClearCartButton_Click(object sender, EventArgs e)
        {
            
            SaleCartItemsGridView.DataSource = null;
            SaleCartItemsGridView.DataBind();
            SubtotalTextBox.Text = null;
            GSTTextBox.Text = null;
            TotalTextBox.Text=null;
            foreach (GridViewRow loop in this.SaleCartItemsGridView.Rows)
            {
                var qty = loop.FindControl("QuantityTextBox") as TextBox;
                var Refreshbutton = loop.FindControl("RefreshButton") as ImageButton;
                var deletebutton = loop.FindControl("ClearItemButton") as ImageButton;
                Refreshbutton.Enabled = true;
                deletebutton.Enabled = true;
                qty.Enabled = true;
            }
            AddButton.Enabled = true;
            PayButton.Enabled = true;
            QtyTextBox.Enabled = true;
            CategoryList.Enabled = true;
            productList.Enabled = true;
            NewIdBox.Text = null;
            QtyTextBox.Text = 1.ToString();
            CategoryList.SelectedIndex = 0;
        }

       
    }

}