<%@ Page Title="Register New Staff for Port" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="Maersk.Admin.AddUser" Async="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Create a new port staff</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Port" CssClass="col-md-2 control-label">Assigned Port</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="Port" AppendDataBoundItems="true" runat="server" CssClass="form-control" DataSourceID="dsPort" DataTextField="port_name" DataValueField="port_id">
                    <asp:ListItem Value="" Selected="True" Text="Select Departure Port..."></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="dsPort" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT port_id, port_name FROM Port"></asp:SqlDataSource>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Port"
                    CssClass="text-danger"  Display="Dynamic" ErrorMessage="The port field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Name" CssClass="col-md-2 control-label">Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Name" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Name"
                    CssClass="text-danger"  Display="Dynamic" ErrorMessage="The name field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Contact" CssClass="col-md-2 control-label">Contact No.</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Contact" CssClass="form-control" TextMode="Phone" />
                <asp:RegularExpressionValidator ID="revPhone" runat="server" ErrorMessage="Not a valid phone format" 
                    ControlToValidate="Contact" CssClass="text-danger" Display="Dynamic" 
                    ValidationExpression="0[0-9]{9,10}" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Contact"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The contact no. field is required."/>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The email field is required." />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" Text="Register" ID="btnRegister" CssClass="btn btn-default" OnClick="btnRegister_Click" />
            </div>
        </div>
    </div>
    <h4>Default Password is <asp:Label runat="server" Font-Bold="true" ForeColor="#ae0000" ID="welcome">P@ssw0rd</asp:Label></h4>
</asp:Content>

