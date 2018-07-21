<%@ Page Title="View Port Shipping" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewPortShipping.aspx.cs" Inherits="Maersk.ViewPortShipping" Async="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--<h2><%: Title %>.</h2>--%>
    <h3>Welcome <asp:Label runat="server" Font-Bold="true" ForeColor="#ae0000" ID="welcome"></asp:Label></h3>
    <asp:GridView ID="gvViewShipping" CssClass="table table-striped table-bordered table-condensed" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="shipping_id" DataSourceID="dsCheckShipping" >
        <Columns>
            <asp:BoundField DataField="shipping_id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="shipping_id" />
            <asp:BoundField DataField="departure" HeaderText="Departure" SortExpression="departure" />
            <asp:BoundField DataField="arrival" HeaderText="Arrival" SortExpression="arrival" />
            <asp:BoundField DataField="shipping_date" HeaderText="Date" SortExpression="shipping_date" />
            <asp:BoundField DataField="shipping_status" HeaderText="Status" SortExpression="shipping_status" />
            <asp:BoundField DataField="shipping_weight" HeaderText="Weight (TEU)" SortExpression="shipping_weight" />
            <asp:BoundField DataField="shipping_cost" HeaderText="Cost (RM)" SortExpression="shipping_cost" />
            <asp:BoundField DataField="shipping_remarks" HeaderText="Remarks" SortExpression="shipping_remarks" />
            <asp:BoundField DataField="container_name" HeaderText="Container" SortExpression="container_name" />
            <asp:BoundField DataField="container_size" HeaderText="Size (TEU)" SortExpression="container_size" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="hlEdit" runat="server" Text="Edit"
                        NavigateUrl='<%# "EditPortShipping.aspx?id=" + Eval("shipping_id") + "&port=" + Eval("port") + "&status=" + Eval("shipping_status") %>' /> 
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="dsCheckShipping" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
        SelectCommand="SELECT *, 'port' = 
                        CASE
	                        WHEN shipping_departure_port = (select port_id from users where user_id = @id) THEN 1
	                        WHEN shipping_arrival_port = (select port_id from users where user_id = @id) THEN 2
	                        ELSE 0
                        END
                        FROM Shipping_Details
                        WHERE shipping_departure_port = (SELECT port_id FROM users WHERE user_id = @id)
                        OR shipping_arrival_port = (SELECT port_id FROM users WHERE user_id = @id)" >
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="id" SessionField="id" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>


