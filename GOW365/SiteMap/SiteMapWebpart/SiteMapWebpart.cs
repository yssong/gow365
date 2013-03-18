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
            writer.Write("<link rel='stylesheet' type='text/css' href='" + imgUrl + "sitemapStyle.css'/>");
            using (SPWeb oSPWeb = SPContext.Current.Web)
            {
                writer.Write("<div class='SiteMap' >");

                writer.Write("<ul class='map' >" );
                writer.Write("<li class='mapHead'>" + oSPWeb.Title + "</li>");
                foreach (SPNavigationNode node in oSPWeb.Navigation.QuickLaunch)
                {
                    writer.Write("<li class='maplink'>");
                    writer.Write("<a href='"+node.Url+"'>"+node.Title+"</a>");
                    //writer.Write(node.Title + ": " + node.Url + " isVisible : " + node.IsVisible.ToString() + " isExternal : " + node.IsExternal.ToString() + "</br>");
                    writer.Write("</li>");

                    foreach (SPNavigationNode child in node.Children)
                    {
                        writer.Write("<li class='submaplink'>");
                        writer.Write("<a href='" + child.Url + "'>" + child.Title + "</a>");
                        //writer.Write("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + child.Title + ": " + child.Url + " isVisible : " + child.IsVisible.ToString() + " isExternal : " + child.IsExternal.ToString() + "</br>");
                        writer.Write("</li>");
                    }

                    writer.Write("</li>");
                }

                writer.Write("</ul>");

                SPWebCollection webs= oSPWeb.GetSubwebsForCurrentUser();
                foreach (SPWeb web in webs)
                {
                    writer.Write("<ul class='map' >");
                    writer.Write("<li class='mapHead'>" + web.Title + "</li>");
                    foreach (SPNavigationNode node in web.Navigation.QuickLaunch)
                    {
                        writer.Write("<li class='maplink'>");
                        writer.Write("<a href='" + node.Url + "'>" + node.Title + "</a>");
                        //writer.Write(node.Title + ": " + node.Url + " isVisible : " + node.IsVisible.ToString() + " isExternal : " + node.IsExternal.ToString() + "");
                        writer.Write("</li>");

                        foreach (SPNavigationNode child in node.Children)
                        {
                            writer.Write("<li class='submaplink'>");
                            writer.Write("<a href='" + child.Url + "'>" + child.Title + "</a>");
                            //writer.Write("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + child.Title + ": " + child.Url + " isVisible : " + child.IsVisible.ToString() + " isExternal : " + child.IsExternal.ToString() + "");
                            writer.Write("</li>");
                        }
                        
                    }
                    writer.Write("</ul>");

                    
                }
                writer.Write("</div>");
            }
        }

       

    }
}
