using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace GOW365.SubSitesTabWebpart
{
    [ToolboxItemAttribute(false)]
    public class SubSitesTabWebpart : WebPart
    {
        private string imgUrl = "GOW365/SubSitesTab/";

        private string webName1 = string.Empty;

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("Tab1"),
        WebDisplayName("Tab 1's Web URL"),
        WebDescription("1")]
        public string WebName1
        {
            get { return webName1; }
            set { webName1 = value; }
        }

        private string tabTitle1 = string.Empty;

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("Tab1"),
        WebDisplayName("Tab 1's Title"),
        WebDescription("2")]
        public string TabTitle1
        {
            get { return tabTitle1; }
            set { tabTitle1 = value; }
        }

        private string webName2 = string.Empty;

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("Tab2"),
        WebDisplayName("Tab 2's Web URL"),
        WebDescription("3")]
        public string WebName2
        {
            get { return webName2; }
            set { webName2 = value; }
        }

        private string tabTitle2 = string.Empty;

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("Tab2"),
        WebDisplayName("Tab 2's Title"),
        WebDescription("4")]
        public string TabTitle2
        {
            get { return tabTitle2; }
            set { tabTitle2 = value; }
        }

        private string webName3 = string.Empty;

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("Tab3"),
        WebDisplayName("Tab 3's Web URL"),
        WebDescription("5")]
        public string WebName3
        {
            get { return webName3; }
            set { webName3 = value; }
        }

        private string tabTitle3 = string.Empty;

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("Tab3"),
        WebDisplayName("Tab 3's Title"),
        WebDescription("6")]
        public string TabTitle3
        {
            get { return tabTitle3; }
            set { tabTitle3 = value; }
        }

        protected override void CreateChildControls()
        {
            imgUrl = SPContext.Current.Site.ServerRelativeUrl + imgUrl;
            base.CreateChildControls();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            RenderJS(writer);
            RenderHtml(writer);
            
        }
        protected void RenderHtml(HtmlTextWriter writer)
        {
            //전체 DIV
            writer.Write(@"<div id='"+this.ClientID+"_tab' >");
            //Tab 
            writer.Write(@"<ul>");
            if(webName1.Trim()!="")
                writer.Write(@"<li><a href='#" + this.ClientID + @"_Tab1'>" + tabTitle1 + @"</a></li>");
            if (webName2.Trim() != "")
                writer.Write(@"<li><a href='#" + this.ClientID + @"_Tab2'>" + tabTitle2 + @"</a></li>");
            if (webName3.Trim() != "")
                writer.Write(@"<li><a href='#" + this.ClientID + @"_Tab3'>" + tabTitle3 + @"</a></li>");
            writer.Write(@"</ul>");
            if (webName1.Trim() != "")
                writer.Write(RenderTabBody("1"));
            if (webName2.Trim() != "")
                writer.Write(RenderTabBody("2"));
            if (webName3.Trim() != "")
                writer.Write(RenderTabBody("3"));
            writer.Write(@"</div>");
            //Body

        }

        private string RenderTabBody(string tabNo)
        {
            string retHtml = "<div id='"+this.ClientID+"_Tab"+tabNo+"'><ul>";
            string siteUrl = "";
            switch (tabNo)
            {
                case "1" :
                    siteUrl = this.webName1;
                    break;
                case "2":
                    siteUrl = this.webName2;
                    break;
                case "3":
                    siteUrl = this.webName3;
                    break;
                default:
                    break;
            }
            if (siteUrl.Trim() != "")
            {
                if (!siteUrl.StartsWith("http"))
                {
                    if (SPContext.Current.Site.ServerRelativeUrl == "/")
                    {
                        siteUrl = SPContext.Current.Site.Url+siteUrl;
                    }
                    else
                    {
                        siteUrl = SPContext.Current.Site.Url.Replace(SPContext.Current.Site.ServerRelativeUrl, "") + siteUrl;
                    }
                    
                }
                using (SPSite oSPsite = new SPSite(siteUrl))
                {
                    using (SPWeb oSPWeb = oSPsite.OpenWeb())
                    {
                        SPWebCollection webs = null;
                        webs = oSPWeb.GetSubwebsForCurrentUser();
                        foreach (SPWeb web in webs)
                        {
                            retHtml += @"<li><a href='" + web.Url + "' target='_new'>" + web.Title + "</a></li>";
                        }

                    }
                }
            }
            retHtml += "</ul></div>";
            return retHtml;

        }
        protected void RenderJS(HtmlTextWriter writer)
        {
           writer.WriteLine(@" <script type='text/javascript'>
// Only do anything if jQuery isn't defined
if (typeof jQuery == 'undefined') {
 if (typeof $ == 'function') {
  // warning, global var
  thisPageUsingOtherJSLibrary = true;
 }
if (typeof jQuery.ui == 'undefined') {
  // UI loaded
}

  function getScript(url, success) {
    var script     = document.createElement('script');
    script.src = url;

    var head = document.getElementsByTagName('head')[0],
    done = false;

    // Attach handlers for all browsers
    script.onload = script.onreadystatechange = function() {
     if (!done && (!this.readyState || this.readyState == 'loaded' || this.readyState == 'complete')) {
      done = true;
        // callback function provided as param
    success();

        script.onload = script.onreadystatechange = null;
    head.removeChild(script);
       };
    };
    head.appendChild(script);
  };

  getScript('" + imgUrl + @"jquery-1.9.1.min.js', function() {
   if (typeof jQuery=='undefined') {
     // Super failsafe - still somehow failed...
    } else {
     // jQuery loaded! Make sure to use .noConflict just in case
   fancyCode();
      if (thisPageUsingOtherJSLibrary) {
    // Run your jQuery Code
   } else {
    // Use .noConflict(), then run your jQuery Code
   }
    }
  });
 }else { // jQuery was already loaded
  // Run your jQuery Code
}
</script>

<link href='" + imgUrl + @"jquery-ui-1.10.0.custom.min.css' rel='stylesheet'>
<script src='" + imgUrl + @"jquery-ui-1.10.0.min.js'></script>
<script  type='text/javascript'>
$(function() {
    $('#" + this.ClientID + @"_tab').tabs();
});
</script>
");
        }

    }
}
