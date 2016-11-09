using BasicHtmlPage.Business;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TidyNet;

namespace BasicHtmlPage
{
    public partial class Default : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "JqueryScript", Strings.JqueryChangeScript.Replace("{ID}", this.JqueryDropdown.ClientID), true);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "PrototypeScript", Strings.PrototypeChangeScript.Replace("{ID}", this.PrototypeDropdown.ClientID), true);

            #region StringInit
            this.title.InnerText = Resources.Resources.generateHtmlPage;
            this.Explanation.Text = Resources.Resources.defaultExplanation;
            this.EncodingTooltip.Text = Resources.Resources.encoding;
            this.EncodingTooltip.ToolTip = Resources.Resources.encodingTooltip;
            this.Generate.Text = Resources.Resources.generate;
            this.options.InnerText = Resources.Resources.options;
            this.titleTagName.InnerText = Resources.Resources.titleTagName;
            this.TidyHtml.Text = Resources.Resources.beautifyHTML;
            #endregion

            #region DropDownInit
            string[] jqueryVersions = Strings.JqueryVersions.Split('|');
            this.JqueryDropdown.Items.Add(new ListItem("", ""));
            foreach (string version in jqueryVersions)
            {
                this.JqueryDropdown.Items.Add(new ListItem("Version " + version, version));
            }

            string[] jqueryMobileVersions = Strings.JqueryMobileVersions.Split('|');
            this.JqueryMobileDropdown.Items.Add(new ListItem("", ""));
            foreach (string version in jqueryMobileVersions)
            {
                this.JqueryMobileDropdown.Items.Add(new ListItem("Version " + version, version));
            }

            string[] jqueryUIVersions = Strings.JqueryUIVersions.Split('|');
            this.JqueryUIDropdown.Items.Add(new ListItem("", ""));
            foreach (string version in jqueryUIVersions)
            {
                this.JqueryUIDropdown.Items.Add(new ListItem("Version " + version, version));
            }

            string[] angularJSVersions = Strings.AngularVersions.Split('|');
            this.AngularJSDropdown.Items.Add(new ListItem("", ""));
            foreach (string version in angularJSVersions)
            {
                this.AngularJSDropdown.Items.Add(new ListItem("Version " + version, version));
            }

            string[] dojoVersions = Strings.DojoVersions.Split('|');
            this.DojoDropdown.Items.Add(new ListItem("", ""));
            foreach (string version in dojoVersions)
            {
                this.DojoDropdown.Items.Add(new ListItem("Version " + version, version));
            }

            string[] extJsVersions = Strings.ExtJsVersions.Split('|');
            this.ExtJsDropdown.Items.Add(new ListItem("", ""));
            foreach (string version in extJsVersions)
            {
                this.ExtJsDropdown.Items.Add(new ListItem("Version " + version, version));
            }

            string[] mooToolsVersions = Strings.MooToolsVersions.Split('|');
            this.MooToolsDropdown.Items.Add(new ListItem("", ""));
            foreach (string version in mooToolsVersions)
            {
                this.MooToolsDropdown.Items.Add(new ListItem("Version " + version, version));
            }

            string[] prototypeVersions = Strings.PrototypeVersions.Split('|');
            this.PrototypeDropdown.Items.Add(new ListItem("", ""));
            foreach (string version in prototypeVersions)
            {
                this.PrototypeDropdown.Items.Add(new ListItem("Version " + version, version));
            }

            string[] scriptaculousVersions = Strings.ScriptaculousVersions.Split('|');
            this.ScriptaculousDropdown.Items.Add(new ListItem("", ""));
            foreach (string version in scriptaculousVersions)
            {
                this.ScriptaculousDropdown.Items.Add(new ListItem("Version " + version, version));
            }

            string[] swfobjectVersions = Strings.SWFObjectVersions.Split('|');
            this.SWFObjectDropdown.Items.Add(new ListItem("", ""));
            foreach (string version in swfobjectVersions)
            {
                this.SWFObjectDropdown.Items.Add(new ListItem("Version " + version, version));
            }

            string[] threeJsVersions = Strings.ThreeJsVersions.Split('|');
            this.ThreeJsDropdown.Items.Add(new ListItem("", ""));
            foreach (string version in threeJsVersions)
            {
                this.ThreeJsDropdown.Items.Add(new ListItem("Version " + version, version));
            }

            string[] webFontLoaderVersions = Strings.WebFontLoaderVersions.Split('|');
            this.WebFontLoaderDropdown.Items.Add(new ListItem("", ""));
            foreach (string version in webFontLoaderVersions)
            {
                this.WebFontLoaderDropdown.Items.Add(new ListItem("Version " + version, version));
            }
            #endregion

            #region TooltipInit
            this.JqueryTooltip.ToolTip = Resources.Resources.jqueryTooltip;
            this.JqueryTooltip.NavigateUrl = Strings.JqueryWebsiteUrl;
            this.JqueryMobileTooltip.ToolTip = Resources.Resources.jqueryMobileTooltip;
            this.JqueryMobileTooltip.NavigateUrl = Strings.JqueryMobileWebsiteUrl;
            this.JqueryUITooltip.ToolTip = Resources.Resources.jqueryUITooltip;
            this.JqueryUITooltip.NavigateUrl = Strings.JqueryUIWebsiteUrl;
            this.AngularJSTooltip.ToolTip = Resources.Resources.angularTooltip;
            this.AngularJSTooltip.NavigateUrl = Strings.AngularWebsiteUrl;
            this.DojoTooltip.ToolTip = Resources.Resources.dojoTooltip;
            this.DojoTooltip.NavigateUrl = Strings.DojoWebsiteUrl;
            this.ExtJSTooltip.ToolTip = Resources.Resources.extJSTooltip;
            this.ExtJSTooltip.NavigateUrl = Strings.ExtJsWebsiteUrl;
            this.MooToolsTooltip.ToolTip = Resources.Resources.mooToolsTooltip;
            this.MooToolsTooltip.NavigateUrl = Strings.MooToolsWebsiteUrl;
            this.PrototypeTooltip.ToolTip = Resources.Resources.prototypeJsTooltip;
            this.PrototypeTooltip.NavigateUrl = Strings.PrototypeWebsiteUrl;
            this.ScriptaculousTooltip.ToolTip = Resources.Resources.scriptaculousTooltip;
            this.ScriptaculousTooltip.NavigateUrl = Strings.ScriptaculousWebsiteUrl;
            this.SWFObjectTooltip.ToolTip = Resources.Resources.swfObjectTooltip;
            this.SWFObjectTooltip.NavigateUrl = Strings.SWFObjectWebsiteUrl;
            this.ThreeJSTooltip.ToolTip = Resources.Resources.threeJsTooltip;
            this.ThreeJSTooltip.NavigateUrl = Strings.ThreeJsWebsiteUrl;
            this.WebFontLoaderTooltip.ToolTip = Resources.Resources.webFontLoaderTooltip;
            this.WebFontLoaderTooltip.NavigateUrl = Strings.WebFontLoaderWebsiteUrl;
            this.TidyHtml.ToolTip = Resources.Resources.beautifyHTMLToolTip;
            #endregion

            #region LanguageInit
            HttpCookie cookie = Request.Cookies["Language"];
            if (Request.QueryString["lang"] != null)
            {
                cookie.Value = (string)Request.QueryString["lang"];
                Response.SetCookie(cookie);
                Response.Redirect("/");
            }
            else if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            {
                cookie = new HttpCookie("Language");
                cookie.Value = "en";
                Response.SetCookie(cookie);
            }
            #endregion
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "OptionsClick", Strings.OptionsClickScript.Replace("{ID}", this.options.ClientID), true);
        }

        protected void Generate_Click(object sender, EventArgs e)
        {
            HtmlOutput output = new HtmlOutput();
            if (this.TidyHtmlCheck.Checked)
            {
                output.HtmlTidy = true;
            }

            output.Angular = this.AngularJSDropdown.SelectedValue;
            output.Charset = this.CharacterEncoding.Text;
            output.Dojo = this.DojoDropdown.SelectedValue;
            output.ExtJS = this.ExtJsDropdown.SelectedValue;
            output.Jquery = this.JqueryDropdown.SelectedValue;
            output.JqueryMobile = this.JqueryMobileDropdown.SelectedValue;
            output.JqueryUI = this.JqueryUIDropdown.SelectedValue;
            output.MooTools = this.MooToolsDropdown.SelectedValue;
            output.Protoptype = this.PrototypeDropdown.SelectedValue;
            output.Scriptaculous = this.ScriptaculousDropdown.SelectedValue;
            output.SWFObject = this.SWFObjectDropdown.SelectedValue;
            output.ThreeJS = this.ThreeJsDropdown.SelectedValue;
            output.Title = this.PageTitle.Text;
            output.WebFontLoader = this.WebFontLoaderDropdown.SelectedValue;

            this.BhtmlOutput.InnerText = output.Generate();
        }
    }
}