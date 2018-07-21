<%@ Page Title="View Users" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewUsers.aspx.cs" Inherits="Maersk.Admin.ViewUsers" Async="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Welcome <asp:Label runat="server" Font-Bold="true" ForeColor="#ae0000" ID="welcome"></asp:Label></h3>
    <asp:GridView ID="gvUser" CssClass="table table-striped table-bordered table-condensed" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="user_id" DataSourceID="dsUser" >
        <Columns>
            <asp:BoundField DataField="user_id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="user_id" />
            <asp:BoundField DataField="user_name" HeaderText="Name" SortExpression="user_name" />
            <asp:BoundField DataField="user_contact" HeaderText="Contact No." SortExpression="user_contact" />
            <asp:BoundField DataField="user_email" HeaderText="Email" SortExpression="user_email" />
            <asp:BoundField DataField="role_name" HeaderText="Role" SortExpression="role_name" />
            <asp:BoundField DataField="port_name" HeaderText="Port Name" SortExpression="port_name" />
            <asp:BoundField DataField="port_country" HeaderText="Port Country" SortExpression="port_country" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" 
                    Text="Delete" OnClientClick="return confirm('Are you sure to remove this user?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="dsUser" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
        SelectCommand="SELECT user_id, user_name, user_contact, user_email, role_name, port_name, port_country FROM users u
            JOIN role r ON u.role_id = r.role_id
            FULL OUTER JOIN port p ON u.port_id = p.port_id
            WHERE user_id IS NOT NULL;" 
        DeleteCommand="DELETE FROM users WHERE user_id = @user_id">
        <DeleteParameters>
            <asp:Parameter Name="user_id" />
        </DeleteParameters>
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="id" SessionField="id" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
