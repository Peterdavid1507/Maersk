<%@ Page Title="View Shipping Information" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewShipping.aspx.cs" Inherits="Maersk.ViewShipping" Async="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--<h2><%: Title %>.</h2>--%>
    <h3>Welcome <asp:Label runat="server" Font-Bold="true" ForeColor="#ae0000" ID="welcome"></asp:Label></h3>
    <h2><%: Title %></h2>
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
            <asp:BoundField DataField="ContainerName" HeaderText="Container" SortExpression="ContainerName" />
            <asp:BoundField DataField="ContainerSize" HeaderText="Size (TEU)" SortExpression="ContainerSize" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="hlEdit" runat="server" Text="Edit"
                        NavigateUrl='<%# "EditShipping.aspx?id=" + Eval("shipping_id")%>' /> 
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" 
                    Text="Delete" OnClientClick="return confirm('Are you sure to delete this shipment?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="dsCheckShipping" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
        SelectCommand="SELECT shipping_id, p1.port_name AS departure, p2.port_name AS arrival, shipping_date, shipping_status, shipping_weight, shipping_cost, shipping_remarks, c.container_name AS ContainerName, c.container_size AS ContainerSize
        FROM shipping s
        JOIN container c on s.container_id = c.container_id
        JOIN port p1 on shipping_departure_port = p1.port_id
        JOIN port p2 on shipping_arrival_port = p2.port_id
        WHERE (s.shipping_customer = @id)" 
        DeleteCommand="DELETE FROM shipping WHERE shipping_id = @shipping_id">
        <DeleteParameters>
            <asp:Parameter Name="shipping_id" />
        </DeleteParameters>
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="id" SessionField="id" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

