using eRaceProject.Admin.Security;
using eRaceSystem.BLL;
using eRaceSystem.BLL.Sales;
using eRaceSystem.ViewModels;
using eRaceSystem.ViewModels.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eRaceProject.Subsystem.Sales
{
    public partial class Refund : System.Web.UI.Page
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
            //else if (! User.IsInRole(Settings.ClerkRole))
            //{
            //    Response.Redirect("~/Account/Login", true);
            //}
            
            var controller = new eRaceController();
            var employee = controller.GetEmployeeName(EmployeeId);
            EmployeeUser.Text = $"Hello there! {employee.UserName} ({employee.EmployeeRole})";

        }

        protected void Search_Click(object sender, EventArgs e)
        {


            RefundInvoiceGridView.Visible = true;
            var id = int.Parse(InvocieNumber.Text);
         
                StoreRefundController controller = new StoreRefundController();
                var exists = controller.Get_Invoice(id); 

                if(exists.Count()==0)
                {
                    MessageUserControl.ShowInfo("Please enter a valid invoice number.");
                }
                
            
            
           

            decimal subtotal = 0;
            decimal tax = 0;
            decimal total = 0;
            SubtotalTextBox.Text = string.Format("${0:#,#.00}", subtotal.ToString());
            GSTTextBox.Text = string.Format("${0:#,#.00}", tax.ToString());
            TotalTextBox.Text = string.Format("${0:C}", total.ToString());
        }
        public int Get_CategoryFromProduct(string product)
        {
            SalesController controller = new SalesController();
            int id = controller.Get_CategoryID(product);
            return id;
        }

        protected void Clear_Click(object sender, EventArgs e)
        {


            
            RefundInvoiceGridView.Visible = false;
            decimal subtotal = 0;
            decimal tax = 0;
            decimal total = 0;
            SubtotalTextBox.Text = string.Format("${0:#,#.00}", subtotal.ToString());
            GSTTextBox.Text = string.Format("${0:#,#.00}", tax.ToString());
            TotalTextBox.Text = string.Format("${0:C}", total.ToString());
            InvocieNumber.Text = null;
            RefundInvoiceTextBox.Text = null;
            RefundOrder.Enabled = true;

        }

        protected void Refund_Click(object sender, EventArgs e)
        {
            string userName = User.Identity.Name;
            decimal subtotal = 0;
            decimal tax = 0;
            decimal total = 0;
            
            
            List<RefundDetail> newDetail = new List<RefundDetail>();
            RefundDetail newRow = null;
            CheckBox selection = null;
            int error = 0;
            for (int rowIndex = 0; rowIndex < RefundInvoiceGridView.Rows.Count; rowIndex++)
            {
                selection = RefundInvoiceGridView.Rows[rowIndex].FindControl("RefundCheckBox") as CheckBox;
                if(!selection.Checked)
                {
                    MessageUserControl.ShowInfo("please select refund product and enter a refund reason");
                }
                else
                {
                    
                    if (string.IsNullOrEmpty((RefundInvoiceGridView.Rows[rowIndex].FindControl("ReasonTextBox") as TextBox).Text))
                    {
                        MessageUserControl.ShowInfo("Please  enter a refund reason.");
                        error = 1;
                    }
                    else
                    {
                        newRow = new RefundDetail();
                        newRow.Product = (RefundInvoiceGridView.Rows[rowIndex].FindControl("ProductLabel") as Label).Text;
                        newRow.Qty = int.Parse((RefundInvoiceGridView.Rows[rowIndex].FindControl("QtyLabel") as Label).Text);
                        newRow.Price = decimal.Parse((RefundInvoiceGridView.Rows[rowIndex].FindControl("PriceLabel") as Label).Text);
                        newRow.Amount = decimal.Parse((RefundInvoiceGridView.Rows[rowIndex].FindControl("AmountLabel") as Label).Text);
                        newRow.RestockCharge = decimal.Parse((RefundInvoiceGridView.Rows[rowIndex].FindControl("RestockChargeLabel") as Label).Text);
                        newRow.Reason = (RefundInvoiceGridView.Rows[rowIndex].FindControl("ReasonTextBox") as TextBox).Text;
                        newDetail.Add(newRow);

                        subtotal = subtotal + newRow.Amount - newRow.RestockCharge;
                        tax =  subtotal * (decimal)0.05;
                        total = subtotal + tax;
                    }
                }
               

            }

            
            int refunded = 0;
            if (error == 0)
            {
                foreach (var exists in newDetail)
                {
                    int prodID = Get_ProductID(exists.Product);
                    int originvoiceID = int.Parse(InvocieNumber.Text);
                    refunded = CheckForRefunded(originvoiceID, prodID);
                    if (refunded == 1)
                    {

                    }
                    else
                    {



                        MessageUserControl.TryRun(() =>
                       {

                           SubtotalTextBox.Text = string.Format("${0:#,#.00}", subtotal.ToString());
                           GSTTextBox.Text = string.Format("${0:#,#.00}", tax.ToString());
                           TotalTextBox.Text = string.Format("${0:C}", total.ToString());


                           List<InvoiceDetailInfo> alldetaill = new List<InvoiceDetailInfo>();
                           List<StoreRefundInfo> refundlist = new List<StoreRefundInfo>();

                           foreach (var item in newDetail)
                           {
                               alldetaill.Add(new InvoiceDetailInfo
                               {
                                   ProductID = Get_ProductID(item.Product),
                                   Quantity = item.Qty,
                               });
                           }

                           foreach (var items in newDetail)
                           {
                               refundlist.Add(new StoreRefundInfo
                               {

                                   OriginalInvoiceID = int.Parse(InvocieNumber.Text),
                                   ProductID = Get_ProductID(items.Product),
                                   Reason = items.Reason,
                               }
                           );
                           }

                           List<InvoiceInfo> NewInvoice = new List<InvoiceInfo>();

                           NewInvoice.Add(new InvoiceInfo
                           {
                               EmployeeID = Get_EmpID(userName),
                               InvoiceDate = DateTime.Now,
                               SubTotal = (subtotal * -1),
                               GST = (tax * -1),
                               Total = (total * -1),
                           });


                           RefundRequired request = new RefundRequired
                           {
                               RequiredInvoice = NewInvoice,
                               ReuquiredDetail = alldetaill,
                               RequiredStore = refundlist
                           };

                           var controller = new StoreRefundController();
                           controller.CreateRefund(request);




                           foreach (GridViewRow row in RefundInvoiceGridView.Rows)
                           {

                               var checkbox = row.FindControl("RefundCheckBox") as CheckBox;
                               var reasonbox = row.FindControl("ReasonTextBox") as TextBox;

                               checkbox.Enabled = false;
                               reasonbox.Enabled = false;
                           }

                       }, "Refund successful", "Selected products have been refunded.");

                        RefundOrder.Enabled = false;
                        RefundInvoiceTextBox.Text = getinvocieid().ToString();


                    }

                }
            }
            else
            {
                MessageUserControl.ShowInfo("one of the product is not enter a reason ");
            }
            
        }

        public int getinvocieid()
        {
            StoreRefundController controller = new StoreRefundController();
            int id = controller.ReturnInvoiceID();
            return id;
        }


        public int Get_ProductID(string productName)
        {
            SalesController controller = new SalesController();
            int id = controller.Product_Get(productName).ProductID;
            return id;
        }

        public int Get_EmpID(string userName)
        {
            SecurityController controller = new SecurityController();
            return controller.GetCurrentUserEmployeeId(userName).Value;
        }
        public int CheckForRefunded(int originalInvoiceID, int productID)
        {
            StoreRefundController controller = new StoreRefundController();
            int refunded = controller.Check_Refunded(originalInvoiceID, productID);
            return refunded;
        }

        protected void RefundInvoiceGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in RefundInvoiceGridView.Rows)
            {
                string product = (row.FindControl("ProductLabel") as Label).Text;
                var checkbox = row.FindControl("RefundCheckBox") as CheckBox;
                var reasonbox = row.FindControl("ReasonTextBox") as TextBox;
                int category = Get_CategoryFromProduct(product);
                if (category == 3)
                {


                    checkbox.Enabled = false;
                    reasonbox.Enabled = false;
                    reasonbox.Text = "Refund not allow";

                }
            }
            int refunded = 0;
            foreach (GridViewRow row in RefundInvoiceGridView.Rows)
            {

                var product = row.FindControl("ProductLabel") as Label;
                var checkbox = row.FindControl("RefundCheckBox") as CheckBox;
                var reasonbox = row.FindControl("ReasonTextBox") as TextBox;

                int prodID = Get_ProductID(product.Text);
                int originvoiceID = int.Parse(InvocieNumber.Text);
                refunded = CheckForRefunded(originvoiceID, prodID);
                if (refunded == 1)
                {

                    checkbox.Enabled = false;
                    reasonbox.Enabled = false;
                    reasonbox.Text = "Refunded";

                }
            }
        }
    }
}