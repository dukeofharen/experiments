<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BasicHtmlPage.Default" MasterPageFile="~/basichtmlpage.Master" %>
<asp:Content ContentPlaceHolderId="ContentPlaceHolder" runat="server">
    <div class="article">
		<div>
			<h1 runat="server" id="title"></h1>
            <p>
                <asp:Literal runat="server" ID="Explanation"></asp:Literal>
            </p>
			<div id="options-pane">
				<p>
					<table border="0" class="table">
                        <tr>
                            <td runat="server" id="titleTagName"></td>
                            <td><asp:TextBox runat="server" ID="PageTitle"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:HyperLink runat="server" ID="EncodingTooltip" CssClass="tooltip" NavigateUrl="http://www.w3schools.com/tags/ref_charactersets.asp" Target="_blank"></asp:HyperLink></td>
                            <td><asp:TextBox runat="server" ID="CharacterEncoding"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:HyperLink runat="server" ID="JqueryTooltip" CssClass="tooltip" Target="_blank">jQuery</asp:HyperLink></td>
                            <td><asp:DropDownList runat="server" ID="JqueryDropdown"></asp:DropDownList></td>
                        </tr>
                        <tr id="mobilerow" style="display:none;">
                            <td><asp:HyperLink runat="server" ID="JqueryMobileTooltip" CssClass="tooltip" Target="_blank">jQuery Mobile</asp:HyperLink></td>
                            <td><asp:DropDownList runat="server" ID="JqueryMobileDropdown"></asp:DropDownList></td>
                        </tr>
                        <tr id="uirow" style="display:none;">
                            <td><asp:HyperLink runat="server" ID="JqueryUITooltip" CssClass="tooltip" Target="_blank">jQuery UI</asp:HyperLink></td>
                            <td><asp:DropDownList runat="server" ID="JqueryUIDropdown"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:HyperLink runat="server" ID="AngularJSTooltip" CssClass="tooltip" Target="_blank">AngularJS</asp:HyperLink></td>
                            <td><asp:DropDownList runat="server" ID="AngularJSDropdown"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:HyperLink runat="server" ID="DojoTooltip" CssClass="tooltip" Target="_blank">Dojo</asp:HyperLink></td>
                            <td><asp:DropDownList runat="server" ID="DojoDropdown"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:HyperLink runat="server" ID="ExtJSTooltip" CssClass="tooltip" Target="_blank">Ext JS</asp:HyperLink></td>
                            <td><asp:DropDownList runat="server" ID="ExtJsDropdown"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:HyperLink runat="server" ID="MooToolsTooltip" CssClass="tooltip" Target="_blank">MooTools</asp:HyperLink></td>
                            <td><asp:DropDownList runat="server" ID="MooToolsDropdown"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:HyperLink runat="server" ID="PrototypeTooltip" CssClass="tooltip" Target="_blank">Prototype</asp:HyperLink></td>
                            <td><asp:DropDownList runat="server" ID="PrototypeDropdown"></asp:DropDownList></td>
                        </tr>
                        <tr style="display:none;" id="scriptaculousrow">
                            <td><asp:HyperLink runat="server" ID="ScriptaculousTooltip" CssClass="tooltip" Target="_blank">script.aculo.us</asp:HyperLink></td>
                            <td><asp:DropDownList runat="server" ID="ScriptaculousDropdown"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:HyperLink runat="server" ID="SWFObjectTooltip" CssClass="tooltip" Target="_blank">SWFObject</asp:HyperLink></td>
                            <td><asp:DropDownList runat="server" ID="SWFObjectDropdown"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:HyperLink runat="server" ID="ThreeJSTooltip" CssClass="tooltip" Target="_blank">Three.js</asp:HyperLink></td>
                            <td><asp:DropDownList runat="server" ID="ThreeJsDropdown"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:HyperLink runat="server" ID="WebFontLoaderTooltip" CssClass="tooltip" Target="_blank">Web Font Loader</asp:HyperLink></td>
                            <td><asp:DropDownList runat="server" ID="WebFontLoaderDropdown"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:HyperLink runat="server" ID="TidyHtml" CssClass="tooltip" NavigateUrl="#" onclick="return false;">Tidy HTML</asp:HyperLink></td>
                            <td><asp:CheckBox runat="server" ID="TidyHtmlCheck" Checked="true" /></td>
                        </tr>
					</table>
				</p>
			</div>
			<a href="#" id="options" class="button" runat="server"></a>
            <asp:LinkButton runat="server" ID="Generate" CssClass="button" OnClick="Generate_Click"></asp:LinkButton>
			<div id="output">
				<textarea runat="server" id="BhtmlOutput"></textarea>
			</div>
		</div>
	</div>
</asp:Content>