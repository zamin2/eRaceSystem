<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="eRaceProject.Subsystem.Sales.Sales" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Sales</h1>
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <asp:ValidationSummary ID="Validation" runat="server" HeaderText="Quantiy is not avaliable" ValidationGroup="group"/>
     <asp:Label runat="server" ID="EmployeeUser"></asp:Label>
    <br />
    <div>
        <asp:DropDownList ID="CategoryList" runat="server" AutoPostBack="true"></asp:DropDownList>
        <asp:DropDownList ID="productList" runat="server" DataSourceID="ProductDate" AutoPostBack="true"
            DataTextField="ItemName" DataValueField="ProductID" >
            
        </asp:DropDownList>
        <asp:TextBox ID="QtyTextBox" runat="server"  TextMode="Number" Text="1" Width="50px"></asp:TextBox>
        <asp:Button ID="AddButton" runat="server" Text="Add" OnClick="AddButton_Click" ValidationGroup="group" CausesValidation="true"/>
    </div>
      <br />
       <div>
        <asp:GridView ID="SaleCartItemsGridView" runat="server" 
            AutoGenerateColumns="False" OnRowCommand="SaleCartItemsGridView_RowCommand" 
            ItemType="eRaceSystem.ViewModels.SaleCartItem" DataKeyNames="ProductID">
            <Columns>  
                <asp:TemplateField HeaderText="Product" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="ProductIDLabel" runat="server" Text='<%# Eval("ProductID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Product">
                    <ItemTemplate>
                        <asp:Label ID="ProductLabel" runat="server" Text='<%# Eval("ProductName") %>' Width="200px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:CompareValidator ID="OrigInvoiceCompValidator" runat="server" ErrorMessage="quantity number must be a positive whole number" 
            ControlToValidate="QuantityTextBox" ValueToCompare="0" Operator="GreaterThan" Display="None" Type="Integer" ValidationGroup="group"></asp:CompareValidator>
                        <asp:TextBox ID="QuantityTextBox" runat="server" Text='<%# Eval("Quantity") %>' Width="50px" TextMode="Number"
                            CausesValidation="true" ValidationGroup="group"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price">
                    <ItemTemplate>
                        <asp:ImageButton ID="RefreshButton" runat="server" ImageUrl="~/images/Refresh.png" CommandName="Refresh" 
                            ValidationGroup="group" CausesValidation="true"/>
                        <asp:Label ID="dollor" runat="server">$</asp:Label>
                        <asp:Label ID="PriceLabel" runat="server" Text='<%# Eval("Price") %>' Width="150px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount">
                    <ItemTemplate>
                        <asp:Label ID="AmountLabel" runat="server" Text='<%# Eval("Amount","{0:C}")%>' Width="150px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="ClearItemButton" runat="server" ImageUrl="~/images/Clear.png" CommandName="DeleteQuote"
                            CommandArgument="<%# Item.ProductID %>"/>

                        

                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

     

    </div>
    <br />
      <div class="row">
        <div class="col-sm-1">
            <asp:Button ID="PayButton" runat="server" Text="Payment" BackColor="Green" Height="100px" OnClick="PayButton_Click"
                 ValidationGroup="group" CausesValidation="true"/>
        </div>
      
        <div class="col-sm-2">
            <asp:LinkButton ID="ClearCartButton" runat="server" CssClass="btn" OnClick="ClearCartButton_Click">Clear Cart</asp:LinkButton>
        </div>  

        <div class="col-md-4">  
            <div class="row">
                <div class="col-sm-2">
                    <asp:Label ID="SubtotalLabel" runat="server" Text="Subtotal"></asp:Label>
                </div>
                <div class="col-sm-1">
                    <asp:TextBox ID="SubtotalTextBox" runat="server" Enabled="false" Width="100px"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-2">
                    <asp:Label ID="GSTLabel" runat="server" Text="Tax"></asp:Label>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox ID="GSTTextBox" runat="server" Enabled="false" Width="100px"></asp:TextBox>
                </div>
            </div>   
            <br />
             <div class="row">
                <div class="col-sm-2">
                    <asp:Label ID="TotalLabel" runat="server" Text="Total"></asp:Label>
                </div>
                <div class="col-sm-2">
                    <asp:TextBox ID="TotalTextBox" runat="server" Enabled="false" Width="100px"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <br />
         <div class="row-sm-1">
              &nbsp;&nbsp;<asp:Label ID="InvocieN" runat="server" Text="Invoice Number"></asp:Label>
            &nbsp;<asp:TextBox ID="NewIdBox" runat="server" Enabled="false">
            </asp:TextBox>
        </div>





    <asp:ObjectDataSource ID="CategoryDate" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListCategory" TypeName="eRaceSystem.BLL.Sales.SalesController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ProductDate" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Products_GetByCategoryID" TypeName="eRaceSystem.BLL.Sales.SalesController">
        <SelectParameters>
            <asp:ControlParameter ControlID="CategoryList" PropertyName="SelectedValue" Name="categoryID" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>


</asp:Content>
