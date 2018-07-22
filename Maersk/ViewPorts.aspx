<%@ Page Title="View Ports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewPorts.aspx.cs" Inherits="Maersk.ViewPorts" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Welcome <asp:Label runat="server" Font-Bold="true" ForeColor="#ae0000" ID="welcome"></asp:Label></h3>
    <h2><%: Title %></h2>
    <asp:GridView ID="gvPort" CssClass="table table-striped table-bordered table-condensed" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="port_id" DataSourceID="dsPort" >
        <Columns>
            <asp:BoundField DataField="port_id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="port_id" />
            <asp:BoundField DataField="port_name" HeaderText="Name" SortExpression="port_name" />
            <asp:BoundField DataField="port_country" HeaderText="Country" SortExpression="port_country" />
            <asp:CommandField ShowEditButton="true"/>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="dsPort" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
        SelectCommand="SELECT * FROM port;"
        UpdateCommand="UPDATE port SET port_name = @port_name, port_country = @port_country WHERE port_id = @port_id">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="id" SessionField="id" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="port_name" />
            <asp:Parameter Name="port_country" />
            <asp:Parameter Name="port_id" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

