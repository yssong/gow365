using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace GOW365.SlideShowWebpart
{
    [ToolboxItemAttribute(false)]
    public class SlideShowWebpart : WebPart
    {
        protected override void CreateChildControls()
        {
        }
        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write("HellowWorld");
        }
    }
}
