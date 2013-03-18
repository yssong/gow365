using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data;

namespace SiteNewDocsTab.NewDocsTabWebPart
{
    [ToolboxItemAttribute(false)]
    public class NewDocsTabWebPart : WebPart
    {
        private HtmlGenericControl TabWebPartCSS = null;

        //배포 전에 ImgUrl을 수정해주세요. 
        private string ImgUrl = "GOW365/SiteNewDocsTab/";
        private HtmlInputHidden hhdList;
        private HtmlInputHidden hdtxt;
        private HtmlInputHidden hhdSelectedSite;
        private Label lblWrongSiteUrl;
        private string moreInfo;

        private static SPWeb web = null;
        private static SPList list = null;

        #region Properties

        private string webName1 = string.Empty;
        private string listName1 = string.Empty;
        private string webName2 = string.Empty;
        private string listName2 = string.Empty;
        private string webName3 = string.Empty;
        private string listName3 = string.Empty;
        private int listItemCount = 5;

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("List Item Count"),
        WebDisplayName("List Item Count"),
        WebDescription("List Item Count")]
        public int ListItemCount
        {
            get { return listItemCount; }
            set { listItemCount = value; }
        }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("Tasb 1"),
        WebDisplayName("Tab 1's WebURL"),
        WebDescription("첫번째탭 사이트 URL 을 입력하세요")]
        public string WebName1
        {
            get { return webName1; }
            set { webName1 = value; }
        }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("Tasb 1"),
        WebDisplayName("Tasb 1'Name"),
        WebDescription("1")]
        public string ListName1
        {
            get { return listName1; }
            set { listName1 = value; }
        }

        //

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("Tasb 2"),
        WebDisplayName("Tasb 2's WebURL"),
        WebDescription("2")]
        public string WebName2
        {
            get { return webName2; }
            set { webName2 = value; }
        }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("Tasb 2"),
        WebDisplayName("Tasb 2's Name"),
        WebDescription("2")]
        public string ListName2
        {
            get { return listName2; }
            set { listName2 = value; }
        }

        //

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("Tasb 3"),
        WebDisplayName("Tasb 3's WebURL"),
        WebDescription("3")]
        public string WebName3
        {
            get { return webName3; }
            set { webName3 = value; }
        }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("Tasb 3"),
        WebDisplayName("Tasb 3's Name"),
        WebDescription("3")]
        public string ListName3
        {
            get { return listName3; }
            set { listName3 = value; }
        }

        #endregion

        protected override void CreateChildControls()
        {
            ImgUrl = (SPContext.Current.Site.ServerRelativeUrl.EndsWith("/") ? SPContext.Current.Site.ServerRelativeUrl + ImgUrl : SPContext.Current.Site.ServerRelativeUrl + "/" + ImgUrl);

            base.CreateChildControls();
        }


        

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write("<link rel='stylesheet' type='text/css' href='" + ImgUrl + "TabStyle.css'/>");
            renderTabScript(writer);
            renderTab(writer);
        }


        private void renderTab(HtmlTextWriter writer)
        {
            string tabhead = @"<div id='" + this.ClientID + "_container' class='container'>";

            tabhead += @"<div id='tabhead'><ul id='nav'>";
            if (ListName1 != "")
            {
                tabhead += "<li><a class='active' href='#' onclick='" + this.ClientID + "_changeClass(this,\"" + this.ClientID + "_tab1\")'>" + ListName1 + "</a></li>";
            }
            if (ListName2 != "")
            {
                tabhead += "<li><a  href='#' onclick='" + this.ClientID + "_changeClass(this,\"" + this.ClientID + "_tab2\")'>" + ListName2 + "</a></li>";
            }
            if (ListName3 != "")
            {
                tabhead += "<li><a  href='#' onclick='" + this.ClientID + "_changeClass(this,\"" + this.ClientID + "_tab3\")'>" + ListName3 + "</a></li>";
            }

            SPWeb web = null;
            tabhead += @"</ul></div>";
            string weburl = "";
            string listName = "";
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (webName1 == "")
                            {
                                weburl = SPContext.Current.Web.ServerRelativeUrl;
                            }
                            else
                            {
                                weburl = WebName1;
                            }
                            listName = ListName1;
                            break;
                        case 1:
                            if (webName2 == "")
                            {
                                weburl = SPContext.Current.Web.ServerRelativeUrl;
                            }
                            else
                            {
                                weburl = WebName2;
                            }
                            listName = ListName2;
                            break;
                        case 2:
                            if (webName3 == "")
                            {
                                weburl = SPContext.Current.Web.ServerRelativeUrl;
                            }
                            else
                            {
                                weburl = WebName3;
                            }
                            listName = ListName3;
                            break;
                        default:
                            break;
                    }

                    web = SPContext.Current.Site.OpenWeb(weburl);
                    
                    try
                    {
                        SPSiteDataQuery qry = new SPSiteDataQuery();

                        qry.Query = @"<Where>" +
                            "<Or><Or><Or><Or><Or><Or><Or><Or>" +
                            "<Contains><FieldRef Name='FileLeafRef' /><Value Type='File'>.jpg</Value></Contains>" +
                            "<Contains><FieldRef Name='FileLeafRef' /><Value Type='File'>.gif</Value></Contains>" +
                            "</Or>" +
                            "<Contains><FieldRef Name='FileLeafRef' /><Value Type='File'>.doc</Value></Contains>" +
                            "</Or>" +
                            "<Contains><FieldRef Name='FileLeafRef' /><Value Type='File'>.xls</Value></Contains>" +
                            "</Or>" +
                            "<Contains><FieldRef Name='FileLeafRef' /><Value Type='File'>.ppt</Value></Contains>" +
                            "</Or>" +
                            "<Contains><FieldRef Name='FileLeafRef' /><Value Type='File'>.vsd</Value></Contains>" +
                            "</Or>" +
                            "<Contains><FieldRef Name='FileLeafRef' /><Value Type='File'>.vsd</Value></Contains>" +
                            "</Or>" +
                            "<Contains><FieldRef Name='FileLeafRef' /><Value Type='File'>.pdf</Value></Contains>" +
                            "</Or>" +
                            "<Contains><FieldRef Name='FileLeafRef' /><Value Type='File'>.hwp</Value></Contains>" +
                            "</Or>" +
                            "</Where>" +
                            "<OrderBy><FieldRef Name='Modified' Ascending='FALSE' /></OrderBy>";
                        qry.Lists = "<Lists ServerTemplate='101'/>";
                        qry.RowLimit = (uint)listItemCount;
                        qry.ViewFields = "<FieldRef Name='FileLeafRef' /><FieldRef Name='Title' /><FieldRef Name='Created' /><FieldRef Name='Author' /><FieldRef Name='Modified' /><FieldRef Name='Editor' /><FieldRef Name='FileRef' /><FieldRef Name='LinkFilenameNoMenu' />";
                        qry.Webs = "<Webs Scope='Recursive'/>";

                        DataTable resultTable = web.GetSiteData(qry);

                        tabhead += "<div id='" + this.ClientID + "_tab" + (i + 1).ToString() + "' class='tab' " + (i == 0 ? "" : "style='display:none;'") + "><ul>";
                        foreach (DataRow item in resultTable.Rows)
                        {

                            if (item != null)
                            {
                                tabhead += "<li><span class='tabTitle'><a href=\""+ "/" + item["FileRef"].ToString().Split('#')[1] + "\">";
                                tabhead += item["LinkFilenameNoMenu"].ToString();
                                tabhead += "</a>";
                                tabhead += "</span><span class='tabName'>" + item["Editor"].ToString().Split('#')[1] + "</span><span  class='tabDate'>" + Convert.ToDateTime(item["Modified"].ToString()).ToShortDateString() + "</span></li>";
                            }

                        }
                        tabhead += @"</ul>
    <div style='width:100%'><span style='float:right'><a href='" + weburl + @"'>more</a></span></div>
</div>";

                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                    }

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (web != null)
                {
                    web.Close();
                    web.Dispose();
                }
            }

            tabhead += @"<!--container-->";
            tabhead += @"</div>";
            writer.Write(tabhead);

        }

        private void renderTabScript(HtmlTextWriter writer)
        {
            string script = @"
<script type='text/javascript'>
// Only do anything if jQuery isn't defined
if (typeof jQuery == 'undefined') {
    if (typeof $ == 'function') {
        // warning, global var
    }
	function  " + this.ClientID + @"getScript(url, success)
	{
	    var script     = document.createElement('script');
	    script.src = url;
	
	    var head = document.getElementsByTagName('head')[0];
	    " + this.ClientID + @"done = false;
	
	    // Attach handlers for all browsers
	    script.onload = script.onreadystatechange = function()
	    {
	        if (!" + this.ClientID + @"done && (!this.readyState || this.readyState == 'loaded' || this.readyState == 'complete'))
	        {
		        " + this.ClientID + @"done = true;
		        // callback function provided as param
		        success();
		
		        script.onload = script.onreadystatechange = null;
		        head.removeChild(script);
	        };
	    };
	    head.appendChild(script);
	};

	 " + this.ClientID + @"getScript('" + ImgUrl + @"jquery-1.9.1.min.js', function()
	{
		if (typeof jQuery=='undefined') {
		 // Super failsafe - still somehow failed...
		}
		else
		{
		}
	});
}
else
{
}

function openDialog(_url) {  
    var options = {  url: _url ,  width: 800, height: 600, };  
    SP.UI.ModalDialog.showModalDialog(options);  
}
function " + this.ClientID + @"_changeClass(obj,tabname){
		$('#" + this.ClientID + @"_container #nav li > a').removeClass('active');
		$(obj).addClass('active');
		$('#" + this.ClientID + @"_container .tab').hide();
		$('#'+tabname).show();
}
</script>";

            writer.Write(script);
        }

       

    }
}
