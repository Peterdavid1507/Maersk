<%@ Page Title="Edit Shipping Information" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditContainer.aspx.cs" Inherits="Maersk.EditContainer" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <h3>Container ID: <asp:Label runat="server" Font-Bold="true" ForeColor="#ae0000" ID="lblContainerID"></asp:Label></h3>
    <h6>
        (<asp:Label runat="server" Font-Bold="true" ForeColor="#ae0000">Click "View Containers" and click "Edit" on the desired container information</asp:Label>, if field is not populated with respective information)
    </h6>
    <hr />

    <div class="form-horizontal" id ="CUS">
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Name" CssClass="col-md-2 control-label">Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Name" CssClass="form-control" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Name"
                    CssClass="text-danger"  Display="Dynamic" ErrorMessage="The name field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Size" CssClass="col-md-2 control-label">Size (in TEU)</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Size" CssClass="form-control" TextMode="Number" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Size"
                    CssClass="text-danger"  Display="Dynamic" ErrorMessage="The size field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Price" CssClass="col-md-2 control-label">Price (in RM)</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Price" CssClass="form-control" TextMode="Number" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Price"
                    CssClass="text-danger"  Display="Dynamic" ErrorMessage="The price field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" CssClass="col-md-2 control-label">Stop Using?</asp:Label>
            <div class="col-md-10">
                <asp:RadioButton id="rbYes" Text="Yes" CssClass="radio-inline" GroupName="rgStop" runat="server" /><br />
                <asp:RadioButton id="rbNo" Text="No" CssClass="radio-inline" GroupName="rgStop" runat="server" />
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
            <asp:Button id="btnUpdate" runat="server" Text="Update" CssClass="btn btn-default" OnClick="btnUpdate_Click"   />
        </div>
    </div>
</asp:Content>

