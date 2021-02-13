<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Refund.aspx.cs" Inherits="eRaceProject.Subsystem.Sales.Refund" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Refund Subsystem</h1>
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <asp:ValidationSummary ID="Validation" runat="server" HeaderText="Concerns with the given invoice number" ValidationGroup="group1"/>
      <asp:Label runat="server" ID="EmployeeUser"></asp:Label>
    <br />
    <div class="row">
       &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
         <asp:RequiredFieldValidator ID="OrigInvoiceReqValidator" runat="server" ErrorMessage="Please enter a valid invoice number" 
             ControlToValidate="InvocieNumber" Display="None" ValidationGroup="group1"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="OrigInvoiceCompValidator" runat="server" ErrorMessage="Invoice number must be a positive whole number" 
            ControlToValidate="InvocieNumber" ValueToCompare="0" Operator="GreaterThanEqual" Display="None" Type="Integer" ValidationGroup="group1"></asp:CompareValidator>
        <asp:TextBox ID="InvocieNumber" runat="server"></asp:TextBox>&nbsp;&nbsp;
        <asp:Button ID="Searchbutton" runat="server" Text="Lookup Invoice" OnClick="Search_Click" ValidationGroup="group1" CausesValidation="true"/>&nbsp;&nbsp;
        <asp:Button ID="Clearbutton" runat="server" Text="Clear" OnClick="Clear_Click" />&nbsp;&nbsp;
    </div>
    <br />
    <div class="row">
        &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:GridView ID="RefundInvoiceGridView" runat="server" DataSourceID="RefundDetailDate" AllowPaging="True" OnRowDataBound="RefundInvoiceGridView_RowDataBound"   
            AutoGenerateColumns="False" >
            <Columns>
                <asp:TemplateField HeaderText="Product">
                    <ItemTemplate>
                        <asp:Label ID="ProductLabel" runat="server" Text='<%# Eval("Product") %>' Width="200px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Qty">
                    <ItemTemplate>
                        <asp:Label ID="QtyLabel" runat="server" Text='<%# Eval("Qty") %>' Width="50px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price">
                    <ItemTemplate>
                        <asp:Label ID="PriceLabel" runat="server" Text='<%# Eval("Price") %>' Width="100px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount">
                    <ItemTemplate>
                        <asp:Label ID="AmountLabel" runat="server" Text='<%# Eval("Amount") %>' Width="100px" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Restock Charge">
                    <ItemTemplate>                
                        <asp:Label ID="RestockChargeLabel" runat="server" Text='<%# Eval("RestockCharge") %>' Width="100px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Refund Reason">
                    <ItemTemplate>
                       
                        <asp:CheckBox ID="RefundCheckBox" runat="server"
                            AutoPostBack="true"/>
                        <asp:TextBox ID="ReasonTextBox" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <br />
    <div class="col-md-4">  
        <div class="row">
            <div class="col-sm-3">
                <asp:Label ID="SubtotalLabel" runat="server" Text="Subtotal"></asp:Label>
            </div>&nbsp;&nbsp;
            <div class="col-sm-2">
                <asp:TextBox ID="SubtotalTextBox" runat="server" Enabled="false"></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-3">
                <asp:Label ID="GSTLabel" runat="server" Text="Tax"></asp:Label>
            </div>&nbsp;&nbsp;
            <div class="col-sm-3">
                <asp:TextBox ID="GSTTextBox" runat="server" Enabled="false"></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-3">
                <asp:Label ID="TotalLabel" runat="server" Text="Total"></asp:Label>
            </div>&nbsp;&nbsp;
            <div class="col-sm-3">
                <asp:TextBox ID="TotalTextBox" runat="server" Enabled="false"></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="row">
                <div class="col-sm-3">
                    <asp:Button ID="RefundOrder" runat="server" Text="Refund" OnClick="Refund_Click"/>
                </div>
                <div class="col-sm-3">
                    <asp:Label ID="RefundInvoice" runat="server" Text="Refund Invoice "></asp:Label>
                    <asp:TextBox ID="RefundInvoiceTextBox" runat="server" Enabled="false"></asp:TextBox>
                </div>
            </div>
        </div>



    <asp:ObjectDataSource ID="RefundDetailDate" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="List_InvoiceDetailsList" TypeName="eRaceSystem.BLL.Sales.InvoiceController">
        <SelectParameters>
            <asp:ControlParameter ControlID="InvocieNumber" PropertyName="Text" Name="invoiceID" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
