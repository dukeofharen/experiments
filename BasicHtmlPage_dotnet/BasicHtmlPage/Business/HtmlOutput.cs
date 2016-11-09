using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using TidyNet;

namespace BasicHtmlPage.Business
{
    public class HtmlOutput
    {
        public bool HtmlTidy { get; set; }

        public string Title { get; set; }
        public string Charset { get; set; }

        public string Jquery { get; set; }
        public string JqueryMobile { get; set; }
        public string JqueryUI { get; set; }
        public string Angular { get; set; }
        public string Dojo { get; set; }
        public string ExtJS { get; set; }
        public string MooTools { get; set; }
        public string Protoptype { get; set; }
        public string Scriptaculous { get; set; }
        public string SWFObject { get; set; }
        public string ThreeJS { get; set; }
        public string WebFontLoader { get; set; }

        public string Generate()
        {
            string result = Strings.BasicHtmlPage;
            string head = string.Empty;

            if (this.Charset == string.Empty)
            {
                this.Charset = "UTF-8";
            }

            #region HeadInit
            if (this.Jquery != string.Empty)
            {
                head += string.Format(Strings.ScriptFrame, string.Format(Strings.JqueryUrl, this.Jquery));
            }
            if (this.JqueryMobile != string.Empty)
            {
                head += string.Format(Strings.CssFrame, string.Format(Strings.JqueryMobileCssUrl, this.JqueryMobile));
                head += string.Format(Strings.ScriptFrame, string.Format(Strings.JqueryMobileJsUrl, this.JqueryMobile));
            }
            if (this.JqueryUI != string.Empty)
            {
                head += string.Format(Strings.CssFrame, string.Format(Strings.JqueryUICssUrl, this.JqueryUI));
                head += string.Format(Strings.ScriptFrame, string.Format(Strings.JqueryUIJsUrl, this.JqueryUI));
            }
            if (this.Angular != string.Empty)
            {
                head += string.Format(Strings.ScriptFrame, string.Format(Strings.AngularUrl, this.Angular));
            }
            if (this.Dojo != string.Empty)
            {
                head += string.Format(Strings.ScriptFrame, string.Format(Strings.DojoUrl, this.Dojo));
            }
            if (this.ExtJS != string.Empty)
            {
                head += string.Format(Strings.ScriptFrame, string.Format(Strings.ExtJsUrl, this.ExtJS));
            }
            if (this.MooTools != string.Empty)
            {
                head += string.Format(Strings.ScriptFrame, string.Format(Strings.MooToolsUrl, this.MooTools));
            }
            if (this.Protoptype != string.Empty)
            {
                head += string.Format(Strings.ScriptFrame, string.Format(Strings.PrototypeUrl, this.Protoptype));
            }
            if (this.Scriptaculous != string.Empty)
            {
                head += string.Format(Strings.ScriptFrame, string.Format(Strings.ScriptaculousUrl, this.Scriptaculous));
            }
            if (this.SWFObject != string.Empty)
            {
                head += string.Format(Strings.ScriptFrame, string.Format(Strings.SWFObjectUrl, this.SWFObject));
            }
            if (this.ThreeJS != string.Empty)
            {
                head += string.Format(Strings.ScriptFrame, string.Format(Strings.ThreeJsUrl
, this.ThreeJS));
            }
            if (this.WebFontLoader != string.Empty)
            {
                head += string.Format(Strings.ScriptFrame, string.Format(Strings.WebFontLoaderUrl
, this.WebFontLoader));
            }
            #endregion

            result = result.Replace("{TITLE}", this.Title);
            result = result.Replace("{CHARSET}", this.Charset);
            result = result.Replace("{OTHER_HEAD}", head);
            result = result.Replace("{BODY}", "");

            #region HtmlTidy
            if (this.HtmlTidy)
            {
                Tidy tidy = new Tidy();
                tidy.Options.DocType = DocType.Strict;
                tidy.Options.DropFontTags = true;
                tidy.Options.LogicalEmphasis = true;
                tidy.Options.Xhtml = true;
                tidy.Options.XmlOut = true;
                tidy.Options.MakeClean = true;
                tidy.Options.TidyMark = false;
                TidyMessageCollection tmc = new TidyMessageCollection();
                MemoryStream input = new MemoryStream();
                MemoryStream output = new MemoryStream();
                byte[] byteArray = Encoding.UTF8.GetBytes(result);
                input.Write(byteArray, 0, byteArray.Length);
                input.Position = 0;
                tidy.Parse(input, output, tmc);
                result = Encoding.UTF8.GetString(output.ToArray());
            }
            #endregion

            return result;
        }
    }
}