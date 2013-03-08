using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Navigation;

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
        protected override void Render(HtmlTextWriter writer)
        {
            using (SPWeb oSPWeb = SPContext.Current.Web)
            {

                writer.Write("</br>" + oSPWeb.Title + "'s QuickLaunch: </br></br>");
                foreach (SPNavigationNode node in oSPWeb.Navigation.QuickLaunch)
                {
                    writer.Write(node.Title + ": " + node.Url + " isVisible : " + node.IsVisible.ToString() + " isExternal : " + node.IsExternal.ToString() + "</br>");
                    foreach (SPNavigationNode child in node.Children)
                    {
                        writer.Write("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + child.Title + ": " + child.Url + " isVisible : " + child.IsVisible.ToString() + " isExternal : " + child.IsExternal.ToString() + "</br>");
                    }
                }

                SPWebCollection webs= oSPWeb.GetSubwebsForCurrentUser();
                foreach (SPWeb web in webs)
                {
                    writer.Write("</br>"+web.Title+"'s QuickLaunch: </br></br>");
                    foreach (SPNavigationNode node in web.Navigation.QuickLaunch)
                    {
                        writer.Write(node.Title + ": " + node.Url + " isVisible : " + node.IsVisible.ToString() + " isExternal : " + node.IsExternal.ToString() + "</br>");
                        foreach (SPNavigationNode child in node.Children)
                        {
                            writer.Write("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + child.Title + ": " + child.Url + " isVisible : " + child.IsVisible.ToString() + " isExternal : " + child.IsExternal.ToString() + "</br>");
                        }
                    }
                }

            }
        }

       

    }
}
