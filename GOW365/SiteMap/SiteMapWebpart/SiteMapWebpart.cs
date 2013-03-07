using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace GOW365.SiteMapWebpart
{
    [ToolboxItemAttribute(false)]
    public class SiteMapWebpart : WebPart
    {
        private string imgUrl = "GOW365/SiteMap/";



        protected override void CreateChildControls()
        {
            imgUrl = (SPContext.Current.Site.ServerRelativeUrl.EndsWith("/") ? SPContext.Current.Site.ServerRelativeUrl + imgUrl : SPContext.Current.Site.ServerRelativeUrl + "/" + imgUrl);
        }


    }
}
