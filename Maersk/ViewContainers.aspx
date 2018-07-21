<%@ Page Title="View Shipping" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewContainers.aspx.cs" Inherits="Maersk.ViewContainers" Async="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--<h2><%: Title %>.</h2>--%>
    <h3>Welcome <asp:Label runat="server" Font-Bold="true" ForeColor="#ae0000" ID="welcome"></asp:Label></h3>
    <asp:GridView ID="gvViewContainer" CssClass="table table-striped table-bordered table-condensed" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="container_id" DataSourceID="dsContainer" >
        <Columns>
            <asp:BoundField DataField="container_id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="container_id" />
            <asp:BoundField DataField="container_name" HeaderText="Name" SortExpression="container_name" />
            <asp:BoundField DataField="container_size" HeaderText="Size (TEU)" SortExpression="container_size" />
            <asp:BoundField DataField="container_price" HeaderText="Price (RM)" SortExpression="container_price" />
            <asp:BoundField DataField="container_isStopped" HeaderText="Stopped Using" SortExpression="container_isStopped" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="hlEdit" runat="server" Text="Edit"
                        NavigateUrl='<%# "EditContainer.aspx?id=" + Eval("container_id")%>' /> 
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="dsContainer" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
        SelectCommand="SELECT * FROM container" >
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="id" SessionField="id" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>


