<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Receiving.aspx.cs" Inherits="eRaceProject.Subsystem.Receiving.Receiving" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="container-fluid">
            <h1>Receiving</h1>
        <div>
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        </div>
    
        <div class="row">
            <div class="col">
                <asp:DropDownList ID="OrderListDropDown" runat="server"  AppendDataBoundItems="true" DataSourceID="OrderListODS" DataTextField="DisplayText" DataValueField="IDValueField" >
                    <asp:ListItem Value="0">Select a PO</asp:ListItem>
                </asp:DropDownList> 
                <asp:Button ID="Open" runat="server" Text="Open" OnClick="Open_Click" CssClass="btn btn-secondary"/>
                <asp:ObjectDataSource ID="OrderListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListOrders" TypeName="eRaceSystem.BLL.Receiving.ReceivingController"></asp:ObjectDataSource> 
            </div>
        </div>
        <div class="row">
            <div class="col-2">
                <asp:Label ID="CompanyName" runat="server" Font-Bold="true" CssClass="float-right"></asp:Label>
            </div>
            <div class="col-2">
                <asp:Label ID="CompanyAddress" runat="server"></asp:Label>
            </div>
            <div class ="col-6">
                <asp:Button ID="ReceiveShimpment" runat="server" Text="Receive Shipment" OnClick="ReceiveShimpment_Click" CssClass="float-right btn btn-primary" Visible="False"/>
            </div>          
         </ div>
         <div class="row">
             <div class="col-2">
                 <asp:Label ID="CompanyContact" runat="server" Font-Italic="true" CssClass="float-right"></asp:Label>
             </div>
             <div class="col-2">
                 <asp:Label ID="CompanyPhone" runat="server" TextMode="Phone"></asp:Label>
             </div>        
         </div>
    </div>
    <div class="col-md-12">
        <asp:GridView ID="OrderGridView" runat="server" CssClass="table table-striped" ItemType="eRaceSystem.ViewModels.Receiving.OrderDetailInfo" DataKeyNames="OrderDetailID" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField Visible ="false">
                        <ItemTemplate><asp:Label runat="server" ID="OrderDetailID" Text="<%#Item.OrderDetailID %>"/></ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ProductName" HeaderText="Item"/>
                    <asp:BoundField DataField="BulkQuantityOrdered" HeaderText="Quantity Ordered" />                   
                    <asp:TemplateField HeaderText="Ordered Units">
                        <ItemTemplate><%# Item.BulkQuantityOrdered/Item.ItemUnitSize + " x case of " +Item.ItemUnitSize %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity Outstanding">
                        <ItemTemplate><asp:Label runat="server" ID="ItemQuantityOutstanding" Text="<%#Item.ItemQuantityOutstanding %>"></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                                      
                    <asp:TemplateField HeaderText="Received Units">
                        <ItemTemplate><asp:TextBox runat="server" ID="ReceivedUnits" Width="30px"></asp:TextBox> x case of <asp:Label runat="server" ID="UnitSize" Text="<%# Item.ItemUnitSize %>"></asp:Label> </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rejected Units / Reason">
                        <ItemTemplate>
                            <asp:TextBox runat="server" Width="30px" ID="RejectedUnits"></asp:TextBox>
                            <asp:TextBox runat="server" ID="RejectedReason"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Salvaged Items">
                        <ItemTemplate><asp:TextBox runat="server" Width="30px" ID="SalvagedItems"></asp:TextBox></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
        </asp:GridView>
        <div class="row">
            <div class="col-6">              
                <asp:Label ID="UnorderedLabel" runat="server" Text="Unorder Items" Visible="false" Font-Bold="true" Font-Size="20"></asp:Label>
                <asp:ListView ID="UnorderedListView" runat="server" Visible="false" InsertItemPosition="LastItem"  DataKeyNames="ItemID">
                    <EditItemTemplate>
                        <tr style="">
                            <td>
                                <asp:TextBox Text='<%# Bind("ItemID") %>' runat="server" ID="ItemIDTextBox" Visible="false"/></td>
                            <td>
                                <asp:TextBox Text='<%# Bind("ItemName") %>' runat="server" ID="ItemNameTextBox" /></td>
                            <td>
                                <asp:TextBox Text='<%# Bind("VendorID") %>' runat="server" ID="VendorIDTextBox" /></td>
                            <td>
                                <asp:TextBox Text='<%# Bind("Quantity") %>' runat="server" ID="QuantityTextBox" />
                            </td>
                            <td>
                                <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" />
                            </td>
                        </tr>
                    </EditItemTemplate>
                    <EmptyDataTemplate>
                        <table runat="server" style="">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <InsertItemTemplate>
                        <tr style="">
                            <td>
                                <asp:TextBox Text='<%# Bind("ItemID") %>' runat="server" ID="ItemIDTextBox" Visible="false"/></td>
                            <td>
                                <asp:TextBox Text='<%# Bind("ItemName") %>' runat="server" ID="ItemNameTextBox" /></td>
                            <td>
                                <asp:TextBox Text='<%# Bind("VendorID") %>' runat="server" ID="VendorIDTextBox" />
                            </td>
                            <td>
                                <asp:TextBox Text='<%# Bind("Quantity") %>' runat="server" ID="QuantityTextBox" />
                            </td>
                            <td>
                                <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" />
                            </td>
                        </tr>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <tr style="">
                            <td>
                                <asp:Label Text='<%# Eval("ItemID") %>' runat="server" ID="ItemIDLabel" Visible="false"/></td>
                            <td>
                                <asp:Label Text='<%# Eval("ItemName") %>' runat="server" ID="ItemNameLabel" /></td>
                            <td>
                                <asp:Label Text='<%# Eval("VendorID") %>' runat="server" ID="VendorIDLabel" />
                            </td>
                            <td>
                                <asp:Label Text='<%# Eval("Quantity") %>' runat="server" ID="QuantityLabel" />
                            </td>
                            <td>
                                <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <table runat="server" id="itemPlaceholderContainer" style="" border="0">
                                        <tr runat="server" style="">
                                            <th runat="server"></th>
                                            <th runat="server" visible="false">ItemID</th>
                                            <th runat="server">ItemName</th>
                                            <th runat="server">VendorID</th>
                                            <th runat="server">Quantity</th>
                                        </tr>
                                        <tr runat="server" id="itemPlaceholder"></tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" style=""></td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <SelectedItemTemplate>
                        <tr style="">
                            <td>
                                <asp:Label Text='<%# Eval("ItemID") %>' runat="server" ID="ItemIDLabel" Visible="false"/></td>
                            <td>
                                <asp:Label Text='<%# Eval("ItemName") %>' runat="server" ID="ItemNameLabel" /></td>
                            <td>
                                <asp:Label Text='<%# Eval("VendorID") %>' runat="server" ID="VendorIDLabel" /></td>
                            <td>
                                <asp:Label Text='<%# Eval("Quantity") %>' runat="server" ID="QuantityLabel" />
                            </td>
                            <td>
                                <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                            </td>
                        </tr>
                    </SelectedItemTemplate>
                </asp:ListView>
                <asp:ObjectDataSource ID="UnorderedODS" runat="server" DataObjectTypeName="eRaceSystem.ViewModels.Receiving.UnorderedInfo" DeleteMethod="DeleteUnorderedItem" InsertMethod="AddUnorderedItem" OldValuesParameterFormatString="original_{0}" SelectMethod="ListUnorderedItems" TypeName="eRaceSystem.BLL.Receiving.ReceivingController">
                </asp:ObjectDataSource>
            </div>
            <div class="col-1">
                <asp:Button ID="ForceClose" runat="server" Text="Force Close" OnClick="ForceClose_Click" Visible="false" CssClass="btn btn-secondary"/>
            </div>
            <div class="col-5">
                <asp:TextBox ID="Comments" runat="server" TextMode="MultiLine" Visible="false" Height="150px" Width="750"></asp:TextBox>
            </div>
        </div>
    </div>                  
    
</asp:Content>
