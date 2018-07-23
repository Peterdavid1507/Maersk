<%@ Page Title="View Shipping Information" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewShipping.aspx.cs" Inherits="Maersk.ViewShipping" Async="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--<h2><%: Title %>.</h2>--%>
    <h3>Welcome <asp:Label runat="server" Font-Bold="true" ForeColor="#ae0000" ID="welcome"></asp:Label></h3>
    <h2><%: Title %></h2>
    <h6><asp:Label runat="server" Font-Bold="true" ForeColor="#ae0000">User cannot edit the Shipping Information if status is "Shipping" or "Delivered"</asp:Label></h6>
    <div class="form-group">
        <asp:Label runat="server" AssociatedControlID="Search" >Search by ID (click Enter button after ID is entered):</asp:Label>
        <asp:TextBox runat="server" ID="Search" AutoPostBack="true" CssClass="form-inline" TextMode="Number" OnTextChanged="Search_TextChanged" />
        <asp:Button runat="server" Text="Reset" ID="btnReset" CssClass="btn btn-default" OnClick="btnReset_Click" />
    </div>

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
                        NavigateUrl='<%# "EditShipping.aspx?id=" + Eval("shipping_id") + "&status=" + Eval("shipping_status")%>' /> 
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
        SelectCommand="SELECT * FROM shipping_details
        WHERE (shipping_customer = @id)
        ORDER BY shipping_id DESC" 
        DeleteCommand="DELETE FROM shipping WHERE shipping_id = @shipping_id">
        <DeleteParameters>
            <asp:Parameter Name="shipping_id" />
        </DeleteParameters>
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="id" SessionField="id" />
            <asp:SessionParameter DefaultValue="" Name="shipping" SessionField="shipping" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

