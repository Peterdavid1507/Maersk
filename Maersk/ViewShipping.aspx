<%@ Page Title="View Shipping" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewShipping.aspx.cs" Inherits="Maersk.ViewShipping" Async="true" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your application description page.</h3>
    <p>Use this area to provide additional information.</p>
    <asp:TextBox runat="server" ID="txt_b" CssClass="form-control" TextMode="SingleLine" />
</asp:Content>

