﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SiteOnWheels.Extension.Sitemap {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SitemapResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SitemapResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SiteOnWheels.Extension.Sitemap.SitemapResources", typeof(SitemapResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
        ///&lt;?xml-stylesheet type=&quot;text/xsl&quot; href=&quot;[root-url]/sitemap.xsl&quot;?&gt;
        ///&lt;!-- generated-on=&quot;[current-sitemap-datetime]&quot; --&gt;
        ///&lt;!-- generator=&quot;Site On Wheels Sitemap Plugin&quot; --&gt;
        ///&lt;!-- generator-url=&quot;http://duco.cc&quot; --&gt;
        ///&lt;urlset xmlns=&quot;http://www.sitemaps.org/schemas/sitemap/0.9&quot; 
        ///xmlns:image=&quot;http://www.google.com/schemas/sitemap-image/1.1&quot; 
        ///xmlns:xsi=&quot;http://www.w3.org/2001/XMLSchema-instance&quot; 
        ///xsi:schemaLocation=&quot;http://www.sitemaps.org/schemas/sitemap/0.9 
        ///http://www.si [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string frame {
            get {
                return ResourceManager.GetString("frame", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;url&gt;
        ///	&lt;loc&gt;[sitemap:url]&lt;/loc&gt;
        ///	&lt;lastmod&gt;[sitemap:lastmod]&lt;/lastmod&gt; 
        ///	&lt;changefreq&gt;[sitemap:freq]&lt;/changefreq&gt;
        ///	&lt;priority&gt;[sitemap:priority]&lt;/priority&gt;
        ///&lt;/url&gt;.
        /// </summary>
        internal static string item {
            get {
                return ResourceManager.GetString("item", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;&lt;xsl:stylesheet version=&quot;2.0&quot; 
        ///	xmlns:html=&quot;http://www.w3.org/TR/REC-html40&quot; 
        ///	xmlns:sitemap=&quot;http://www.sitemaps.org/schemas/sitemap/0.9&quot; 
        ///		xmlns:image=&quot;http://www.google.com/schemas/sitemap-image/1.1&quot;
        ///	xmlns:xsl=&quot;http://www.w3.org/1999/XSL/Transform&quot;&gt;
        ///&lt;xsl:output method=&quot;html&quot; version=&quot;1.0&quot; encoding=&quot;UTF-8&quot; indent=&quot;yes&quot;/&gt;
        ///&lt;xsl:template match=&quot;/&quot;&gt;
        ///&lt;html xmlns=&quot;http://www.w3.org/1999/xhtml&quot;&gt;
        ///&lt;head&gt;
        ///	&lt;title&gt;XML Sitemap Feed&lt;/title&gt;
        ///	&lt;meta http-equiv=&quot;Content-T [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string xsl {
            get {
                return ResourceManager.GetString("xsl", resourceCulture);
            }
        }
    }
}
