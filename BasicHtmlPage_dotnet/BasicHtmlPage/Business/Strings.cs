using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicHtmlPage.Business
{
    public class Strings
    {
        public const string BasicHtmlPage = "<!DOCTYPE html>" +
            "<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
            "<head>" +
                "<title>{TITLE}</title>" +
                "<meta http-equiv=\"Content-Type\" content=\"text/html; charset={CHARSET}\">" +
                "{OTHER_HEAD}" +
            "</head>" +
            "<body>" +
            "{BODY}" +
            "</body>" +
            "</html>";

        public const string OptionsClickScript = "$('#{ID}').click(function(){optionsClick();});";

        public const string ScriptFrame = "<script type=\"text/javascript\" src=\"{0}\"></script>";
        public const string CssFrame = "<link rel=\"stylesheet\" href=\"{0}\" />";

        public const string JqueryChangeScript = "$('#{ID}').change(function(){if($(this).val() == \"\"){$('#uirow').hide();$('#mobilerow').hide();}else{$('#uirow').show();$('#mobilerow').show();}});";
        public const string JqueryUrl = "//ajax.googleapis.com/ajax/libs/jquery/{0}/jquery.min.js";
        public const string JqueryVersions = "2.1.1|1.11.1|2.1.0|2.0.3|2.0.2|2.0.1|2.0.0|1.11.0|1.10.2|1.10.1|1.10.0|1.9.1|1.9.0|1.8.3|1.8.2|1.8.1|1.8.0|1.7.2|1.7.1|1.7.0|1.6.4|1.6.3|1.6.2|1.6.1|1.6.0|1.5.2|1.5.1|1.5.0|1.4.4|1.4.3|1.4.2|1.4.1|1.4.0|1.3.2|1.3.1|1.3.0|1.2.6|1.2.3";
        public const string JqueryWebsiteUrl = "https://jquery.org/";

        public const string JqueryMobileJsUrl = "//ajax.googleapis.com/ajax/libs/jquerymobile/{0}/jquery.mobile.min.js";
        public const string JqueryMobileCssUrl = "//ajax.googleapis.com/ajax/libs/jquerymobile/{0}/jquery.mobile.min.css";
        public const string JqueryMobileVersions = "1.4.3|1.4.2|1.4.1|1.4.0";
        public const string JqueryMobileWebsiteUrl = "http://jquerymobile.com/";

        public const string JqueryUIJsUrl = "//ajax.googleapis.com/ajax/libs/jqueryui/{0}/jquery-ui.min.js";
        public const string JqueryUICssUrl = "//ajax.googleapis.com/ajax/libs/jqueryui/{0}/themes/smoothness/jquery-ui.css";
        public const string JqueryUIVersions = "1.11.0|1.10.4|1.10.3|1.10.2|1.10.1|1.10.0|1.9.2|1.9.1|1.9.0|1.8.24|1.8.23|1.8.22|1.8.21|1.8.20|1.8.19|1.8.18|1.8.17|1.8.16|1.8.15|1.8.14|1.8.13|1.8.12|1.8.11|1.8.10|1.8.9|1.8.8|1.8.7|1.8.6|1.8.5|1.8.4|1.8.2|1.8.1|1.8.0|1.7.3|1.7.2|1.7.1|1.7.0|1.6.0|1.5.3|1.5.2";
        public const string JqueryUIWebsiteUrl = "http://jqueryui.com/";

        public const string AngularUrl = "//ajax.googleapis.com/ajax/libs/angularjs/{0}/angular.min.js";
        public const string AngularVersions = "1.2.21|1.2.20|1.2.19|1.2.18|1.2.17|1.2.16|1.2.15|1.2.14|1.2.13|1.2.12|1.2.11|1.2.10|1.2.9|1.2.8|1.2.7|1.2.6|1.2.5|1.2.4|1.2.3|1.2.2|1.2.1|1.2.0|1.0.8|1.0.7|1.0.6|1.0.5|1.0.4|1.0.3|1.0.2|1.0.1";
        public const string AngularWebsiteUrl = "https://angularjs.org/";

        public const string DojoUrl = "//ajax.googleapis.com/ajax/libs/dojo/{0}/dojo/dojo.js";
        public const string DojoVersions = "1.10.0|1.9.3|1.9.2|1.9.1|1.9.0|1.8.6|1.8.5|1.8.4|1.8.3|1.8.2|1.8.1|1.8.0|1.7.5|1.7.4|1.7.3|1.7.2|1.7.1|1.7.0|1.6.2|1.6.1|1.6.0|1.5.3|1.5.2|1.5.1|1.5.0|1.4.5|1.4.4|1.4.3|1.4.1|1.4.0|1.3.2|1.3.1|1.3.0|1.2.3|1.2.0|1.1.1";
        public const string DojoWebsiteUrl = "http://dojotoolkit.org/";

        public const string ExtJsUrl = "//ajax.googleapis.com/ajax/libs/ext-core/{0}/ext-core.js";
        public const string ExtJsVersions = "3.1.0|3.0.0";
        public const string ExtJsWebsiteUrl = "http://www.sencha.com/products/extjs/";

        public const string MooToolsUrl = "//ajax.googleapis.com/ajax/libs/mootools/{0}/mootools-yui-compressed.js";
        public const string MooToolsVersions = "1.5.0|1.4.5|1.4.4|1.4.3|1.4.2|1.4.1|1.4.0|1.3.2|1.3.1|1.3.0|1.2.5|1.2.4|1.2.3|1.2.2|1.2.1|1.1.2|1.1.1";
        public const string MooToolsWebsiteUrl = "http://mootools.net/";

        public const string PrototypeChangeScript = "$('#{ID}').change(function(){if($(this).val() == \"\"){$('#scriptaculousrow').hide();}else{$('#scriptaculousrow').show();}});";
        public const string PrototypeUrl = "//ajax.googleapis.com/ajax/libs/prototype/{0}/prototype.js";
        public const string PrototypeVersions = "1.7.2.0|1.7.1.0|1.7.0.0|1.6.1.0|1.6.0.3|1.6.0.2";
        public const string PrototypeWebsiteUrl = "http://prototypejs.org/";

        public const string ScriptaculousUrl = "//ajax.googleapis.com/ajax/libs/scriptaculous/{0}/scriptaculous.js";
        public const string ScriptaculousVersions = "1.9.0|1.8.3|1.8.2|1.8.1";
        public const string ScriptaculousWebsiteUrl = "http://script.aculo.us/";

        public const string SWFObjectUrl = "//ajax.googleapis.com/ajax/libs/swfobject/{0}/swfobject.js";
        public const string SWFObjectVersions = "2.2|2.1";
        public const string SWFObjectWebsiteUrl = "https://code.google.com/p/swfobject/";

        public const string ThreeJsUrl = "//ajax.googleapis.com/ajax/libs/threejs/{0}/three.min.js";
        public const string ThreeJsVersions = "r67";
        public const string ThreeJsWebsiteUrl = "http://threejs.org/";

        public const string WebFontLoaderUrl = "//ajax.googleapis.com/ajax/libs/webfont/{0}/webfont.js";
        public const string WebFontLoaderVersions = "1.5.3|1.5.2|1.5.0|1.4.10|1.4.8|1.4.7|1.4.6|1.4.2|1.3.0|1.1.2|1.1.1|1.1.0|1.0.31|1.0.30|1.0.29|1.0.28|1.0.27|1.0.26|1.0.25|1.0.24|1.0.23|1.0.22|1.0.21|1.0.19|1.0.18|1.0.17|1.0.16|1.0.15|1.0.14|1.0.13|1.0.12|1.0.11|1.0.10|1.0.9|1.0.6|1.0.5|1.0.4|1.0.3|1.0.2|1.0.1|1.0.0";
        public const string WebFontLoaderWebsiteUrl = "https://developers.google.com/fonts/docs/webfont_loader";
    }
}