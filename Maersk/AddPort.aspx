<%@ Page Title="Register New Port" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddPort.aspx.cs" Inherits="Maersk.AddPort" Async="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Create a new port</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Name" CssClass="col-md-2 control-label">Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Name" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Name"
                    CssClass="text-danger"  Display="Dynamic" ErrorMessage="The name field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Country" CssClass="col-md-2 control-label">Country</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Country" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Country"
                    CssClass="text-danger"  Display="Dynamic" ErrorMessage="The country field is required." />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" Text="Register" ID="btnRegister" CssClass="btn btn-default" OnClick="btnRegister_Click" />
            </div>
        </div>
    </div>
</asp:Content>


