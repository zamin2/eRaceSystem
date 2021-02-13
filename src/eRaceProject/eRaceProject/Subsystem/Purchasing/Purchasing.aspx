<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Purchasing.aspx.cs" Inherits="eRaceProject.Subsystem.Purchasing.Purchasing" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jimbotron">
        <h1>Purchasing Subsystem</h1>
        <asp:Label runat="server" ID="EmployeeUser"></asp:Label>
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    </div>
    <div class="row">
        <div class="col-md-6">
            <h2>Purchase Order</h2>
            <asp:DropDownList ID="VendorDropDown" runat="server" CssClass="form-control" AppendDataBoundItems="true" DataTextField="DisplayText" DataValueField="IDValue" DataSourceID="VendorDataSource">
                <asp:ListItem Value="0">[Select a Vendor]</asp:ListItem>
            </asp:DropDownList>
            <asp:ObjectDataSource ID="VendorDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListVendors" TypeName="eRaceSystem.BLL.Purchasing.PurchasingController"></asp:ObjectDataSource>
            <asp:LinkButton ID="VendorSelect" runat="server" Text="Select" CssClass="btn btn-primary" OnClick="VendorSelect_Click"></asp:LinkButton>
            <asp:LinkButton OnClick="PlaceOrder_Click" ID="PlaceOrder" runat="server" Text="Place Order" CssClass="btn btn-primary"></asp:LinkButton>
            <asp:LinkButton OnClick="SaveOrder_Click" ID="SaveOrder" runat="server" Text="Save Order" CssClass="btn btn-primary"></asp:LinkButton>
            <asp:LinkButton OnClick="DeleteOrder_Click" ID="DeleteOrder" runat="server" Text="Delete" CssClass="btn btn-primary"></asp:LinkButton>
            <asp:LinkButton OnClick="CancelButton_Click" ID="CancelButton" runat="server" Text="Cancel" CssClass="btn btn-primary"></asp:LinkButton>
            <div class="row">
                <div class="col">
                    <asp:Label runat="server" ID="VendorName"></asp:Label><br />
                    <asp:Label runat="server" ID="VendorContact"></asp:Label><br />
                    <asp:Label runat="server" ID="VendorPhone"></asp:Label>
                       <br />
                    <br />
                    <asp:Label runat="server" ID="CommentsLabel" AssociatedControlID="VendorComments" Text="Vendor Comments:"></asp:Label>
                    <br />
                    <asp:TextBox runat="server" ID="VendorComments" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="col">
                    
                    <asp:Label ID="SubtotalLabel" AssociatedControlID="Subtotal" runat="server" Text="SubTotal(in $):"></asp:Label>
                    <asp:TextBox Enabled="false" runat="server" ID="Subtotal" CssClass="form-control"></asp:TextBox>
                    <br />
                    <asp:Label ID="TaxLabel" AssociatedControlID="Tax" runat="server" Text="Tax(in $):"></asp:Label>
                    <asp:TextBox Enabled="false" runat="server" ID="Tax" CssClass="form-control"></asp:TextBox>
                    <br />
                    <asp:Label ID="TotalLabel" runat="server" AssociatedControlID="Total" Text="Total(in $):"></asp:Label>
                    <asp:TextBox Enabled="false" ID="Total" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                
            </div>
            <asp:Label runat="server" ID="messenger"></asp:Label>
            <div class="row">
                <br />
                <asp:GridView ID="OrderGridview" OnRowCommand="OrderGridview_RowCommand" runat="server" CssClass="table table-hover table-sm" AutoGenerateColumns="false" ItemType="eRaceSystem.ViewModels.Purchasing.OrderDetailInfo" DataKeyNames="ProductID" >
                    <EmptyDataTemplate>No New Order Details have been added</EmptyDataTemplate>
                    <Columns>
                        <asp:ButtonField Text="Remove" CommandName="Remove" ControlStyle-CssClass="btn btn-danger btn-xs" />
                        <asp:TemplateField HeaderText="Product">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="ProductID" Value="<%# Item.ProductID %>" />
                                <asp:HiddenField runat="server" ID="ReorderQty" Value="<%# Item.ReorderQty %>" />
                                <asp:HiddenField runat="server" ID="QuantityOnHand" Value="<%# Item.QuantityOnHand %>" />
                                <asp:HiddenField runat="server" ID="QuantityOnOrder" Value="<%# Item.QuantityOnOrder %>" />
                                <asp:HiddenField runat="server" ID="CategoryName" Value="<%# Item.CategoryName %>" />
                                <asp:HiddenField runat="server" ID="OrderDetailID" Value="<%# Item.OrderDetailID %>" />
                                <asp:Label runat="server" ID="ProductNameLabel" Text="<%# Item.ProductName %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Order Qty">
                            <ItemTemplate>
                                <asp:TextBox ID="OrderQtyLabel" runat="server" Text="<%# Item.OrderQty %>"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Size">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="UnitSize" Value="<%# Item.UnitSize %>" />
                                <asp:Label runat="server" ID="UnitSizeLabel"> Case(<%# Item.UnitSize %>)</asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Cost (in $)">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="UnitCostLabel" Text="<%# Item.UnitCost %>"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Per-item Cost (in $)">
                            <ItemTemplate>
                                <asp:ImageButton OnClick="refreshbutton_Click" CommandName="Refresh" runat="server" ID="refreshbutton" ImageUrl="~/images/Refresh.png" AlternateText="Refresh-button" ImageAlign="Left" />
                                <asp:HiddenField runat="server" ID="ItemPriceLabel" Value="<%# Item.ItemPrice %>"></asp:HiddenField>
                                
                                <asp:Label runat="server" ID="PerItemCost" Text="<%# Item.ItemCost %>"></asp:Label>
                                <asp:Label runat="server" ID="warning" Font-Bold="true" ForeColor="Red" Text="<%# Item.warning %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Extended Cost (in $)">
                            <ItemTemplate>
                                <asp:label ID="ExtendedCostLabel" runat="server" Text="<%# Item.ExtendedCost %>"></asp:label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="col-md-6">
            <h2>Inventory</h2>
            
                <asp:Repeater runat="server" ID="VendorRepeater" ItemType="eRaceSystem.ViewModels.Purchasing.VendorProducts">
                    <ItemTemplate>
                        <asp:Label Font-Bold="true" runat="server" ID="CategoryName" CssClass="label label-default" Text="<%# Item.CategoryName %>"></asp:Label>
                    
                        <asp:GridView OnRowCommand="VendorProductsGridview_RowCommand" runat="server" ID="VendorProductsGridview" AutoGenerateColumns="false" ItemType="eRaceSystem.ViewModels.Purchasing.VendorCatalogInfo" DataKeyNames="ProductID">
                            <EmptyDataTemplate>Vendor does not offer any other product in this category.</EmptyDataTemplate>
                            <Columns>
                                <asp:ButtonField Text="Add" CommandName="Add" ControlStyle-CssClass="btn btn-success btn-xs" />
                                <asp:TemplateField HeaderText="Product Name">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="ProductID" Value="<%# Item.ProductID %>" />
                                        <asp:HiddenField runat="server" ID="ItemCost" Value="<%# Item.ItemCost %>" />
                                        <asp:HiddenField runat="server" ID="CategoryName" Value="<%# Item.Category %>" />
                                       
                                        <asp:Label runat="server" ID="VendorProductName" Text="<%# Item.ProductName %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reorder">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="ReOrderLabel" Text="<%# Item.ReorderQty %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="In Stock">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="InStockLabel" Text="<%# Item.QuantityOnHand %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="On Order">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="OnOrderLabel" Text="<%# Item.QuantityOnOrder %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Size">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="UnitSize" Value="<%# Item.UnitSize %>" />
                                        <asp:Label runat="server" ID="SizeLabel">Case(<%# Item.UnitSize %>)</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        
                    </ItemTemplate>
        
                    
                </asp:Repeater>
            

        </div>
    </div>


</asp:Content>
