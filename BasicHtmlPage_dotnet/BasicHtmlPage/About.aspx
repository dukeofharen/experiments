<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="BasicHtmlPage.About" MasterPageFile="~/basichtmlpage.Master" %>
<asp:Content ID="ContentPlaceHolder" ContentPlaceHolderId="ContentPlaceHolder" runat="server">
    <div class="article">
		<div>
            <h1 runat="server" ID="Title"></h1>
            <asp:Literal runat="server" ID="AboutContent"></asp:Literal>
        </div>
    </div>
</asp:Content>