using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BasicHtmlPage
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region StringInit
            this.Title.InnerText = Resources.Resources.aboutTitle;
            this.AboutContent.Text = Resources.Resources.aboutContent;
            #endregion
        }
    }
}