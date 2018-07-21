<%@ Page Title="Register New Shipping Information" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddShipping.aspx.cs" Inherits="Maersk.AddShipping" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>New Shipment</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Departure" CssClass="col-md-2 control-label">Departure Port</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="Departure" AppendDataBoundItems="true" runat="server" CssClass="form-control" DataSourceID="dsPort" DataTextField="port_name" DataValueField="port_id">
                    <asp:ListItem Value="" Selected="True" Text="Select Departure Port..."></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="dsPort" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT port_id, port_name FROM Port"></asp:SqlDataSource>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Departure"
                    CssClass="text-danger"  Display="Dynamic" ErrorMessage="The departure port field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Arrival" CssClass="col-md-2 control-label">End Port</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="Arrival" AppendDataBoundItems="true" runat="server" CssClass="form-control" DataSourceID="dsPort" DataTextField="port_name" DataValueField="port_id">
                    <asp:ListItem Value="" Selected="True" Text="Select Arrival Port..."></asp:ListItem>
                </asp:DropDownList>
                <asp:CompareValidator runat="server" ControlToCompare="Departure" ControlToValidate="Arrival"
                    CssClass="text-danger" Display="Dynamic" Operator="NotEqual" ErrorMessage="The Departure and Arrival Ports should not be the same." />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Arrival"
                CssClass="text-danger" Display="Dynamic" ErrorMessage="The arrival port field is required!" InitialValue=""></asp:RequiredFieldValidator>
            </div>
        </div>
        <%--<div class="form-group">
            <asp:Label runat="server" AssociatedControlID="shippingDate" CssClass="col-md-2 control-label">Shipping Date</asp:Label>
            <div class="col-md-10">
                <asp:Calendar runat="server" ID ="shippingDate" >
                </asp:Calendar>
            </div>
        </div>--%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Weight" CssClass="col-md-2 control-label">Shipping Weight (in TEU)</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Weight" AutoPostBack="true" CssClass="form-control" TextMode="Number" OnTextChanged="Weight_TextChanged" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Weight"
                    CssClass="text-danger"  Display="Dynamic" ErrorMessage="The weight field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Remarks" CssClass="col-md-2 control-label">Remarks</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Remarks"  CssClass="form-control" TextMode="MultiLine" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Container" CssClass="col-md-2 control-label">Container</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="Container" AppendDataBoundItems="true" AutoPostBack="True" runat="server" CssClass="form-control" DataSourceID="dsContainer" DataTextField="list" DataValueField="container_id" OnSelectedIndexChanged="Container_SelectedIndexChanged">
                    <asp:ListItem Value="" Selected="True" Text="Select Conatiner..."></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="dsContainer" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT *, CONCAT(container_name, ' - ', container_size, ' TEU', ' - RM', container_price) AS list, CONCAT('RM',container_price) AS price  FROM container;"></asp:SqlDataSource>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Container"
                    CssClass="text-danger"  Display="Dynamic" ErrorMessage="The container field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtPrice" CssClass="col-md-2 control-label">Price(RM)</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtPrice" Text="" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button id="btnAdd" runat="server" Text="Add" CssClass="btn btn-default" OnClick="btnAdd_Click" />
            </div>
        </div>
    </div>
</asp:Content>

