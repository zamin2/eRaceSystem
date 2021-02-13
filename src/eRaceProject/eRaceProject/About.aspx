<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="eRaceProject.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    
    <div>
        <p>Username of Administrator: Webmaster</p>
        <p>Password of Administrator: Pa$$word1</p>
        <p>All passwords for users will be "Pa$$word1" for development purposes.</p>
    </div>
    <div>
    <h3>Usernames and Passwords</h3>
    <asp:GridView CssClass="table" runat="server" ID="UserGridView" AutoGenerateColumns="False" DataSourceID="UsersDataSource">
        <Columns>
            <asp:BoundField DataField="UserName" HeaderText="Username" SortExpression="UserName"></asp:BoundField>
            <asp:BoundField DataField="EmployeeRole" HeaderText="Employee Role" SortExpression="EmployeeRole"></asp:BoundField>
            <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password"></asp:BoundField>
        </Columns>
    </asp:GridView>
    </div>
    <asp:ObjectDataSource ID="UsersDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListEmployeebyPosition" TypeName="eRaceSystem.BLL.eRaceController"></asp:ObjectDataSource>


    <br />
    <h3>Database Connection String</h3>
    <p>The database connection name is "eRaceDB". Make sure that the connection string is correct for you.</p>
</asp:Content>
