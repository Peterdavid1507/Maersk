<%@ Page Title="Edit Shipping Information" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditPortShipping.aspx.cs" Inherits="Maersk.EditPortShipping" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <h3>Shipping ID: <asp:Label runat="server" Font-Bold="true" ForeColor="#ae0000" ID="lblShippingID"></asp:Label></h3>
    <h3>Cuurent Status: <asp:Label runat="server" Font-Bold="true" ForeColor="#ae0000" ID="lblStatus"></asp:Label></h3>
    <hr />

    <div class="form-horizontal" id ="CUS">
        <div class="form-group">
            <asp:ValidationSummary runat="server" CssClass="text-danger" />
            <asp:Label runat="server" AssociatedControlID="Status" CssClass="col-md-2 control-label">Status</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="Status" AppendDataBoundItems="true" runat="server" CssClass="form-control">
                    <asp:ListItem Value="" Selected="True" Text="Select Status..."></asp:ListItem>
                    <asp:ListItem Value="Pending Approval" Selected="false" Text="Pending Approval"></asp:ListItem>
                    <asp:ListItem Value="Approved" Selected="false" Text="Approved"></asp:ListItem>
                    <asp:ListItem Value="Shipping" Selected="false" Text="Shipping"></asp:ListItem>
                    <asp:ListItem Value="Delivered" Selected="false" Text="Delivered"></asp:ListItem>
                    <asp:ListItem Value="Rejected" Selected="false" Text="Rejected"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Status" 
                    CssClass="text-danger"  Display="Dynamic" ErrorMessage="The status field is required." />
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
            <asp:Button id="btnUpdate" runat="server" Text="Update" CssClass="btn btn-default" OnClick="btnUpdate_Click"  />
        </div>
    </div>
</asp:Content>
