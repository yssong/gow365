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
using Microsoft.SharePoint.Utilities;

namespace GOW365.TabListWebpart
{
    [ToolboxItemAttribute(false)]
    public class TabListWebpart : WebPart
    {
        //배포 전에 ImgUrl을 수정해주세요. 
        private string ImgUrl = "GOW365/TabList/";

        #region Properties

        private string webName1 = string.Empty;
        private string listName1 = string.Empty;
        private string webName2 = string.Empty;
        private string listName2 = string.Empty;
        private string webName3 = string.Empty;
        private string listName3 = string.Empty;
        private int listItemCount = 7;

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
        Category("LIST 1"),
        WebDisplayName("LIST 1's WebName"),
        WebDescription("1")]
        public string WebName1
        {
            get { return webName1; }
            set { webName1 = value; }
        }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("LIST 1"),
        WebDisplayName("LIST 1's ListName"),
        WebDescription("1")]
        public string ListName1
        {
            get { return listName1; }
            set { listName1 = value; }
        }

        //

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("LIST 2"),
        WebDisplayName("LIST 2's WebName"),
        WebDescription("2")]
        public string WebName2
        {
            get { return webName2; }
            set { webName2 = value; }
        }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("LIST 2"),
        WebDisplayName("LIST 2's ListName"),
        WebDescription("2")]
        public string ListName2
        {
            get { return listName2; }
            set { listName2 = value; }
        }

        //

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("LIST 3"),
        WebDisplayName("LIST 3's WebName"),
        WebDescription("3")]
        public string WebName3
        {
            get { return webName3; }
            set { webName3 = value; }
        }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("LIST 3"),
        WebDisplayName("LIST 3's ListName"),
        WebDescription("3")]
        public string ListName3
        {
            get { return listName3; }
            set { listName3 = value; }
        }

        #endregion

        protected override void CreateChildControls()
        {
            ImgUrl = (SPContext.Current.Site.ServerRelativeUrl.EndsWith("/")?SPContext.Current.Site.ServerRelativeUrl+ImgUrl:SPContext.Current.Site.ServerRelativeUrl+"/"+ImgUrl);

        }
        
        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write("<link rel='stylesheet' type='text/css' href='"+ImgUrl+"TabStyle.css'/>");
            
            renderTabScript(writer);
            renderTab(writer);
            

        }
        private void renderTab(HtmlTextWriter writer)
        {
            string tabhead = @"<div id='"+this.ClientID+"_container' class='container'>";

            tabhead += @"<div id='tabhead'><ul id='nav'>";
            if(ListName1!="")
            {
                tabhead += "<li><a class='active' href='#' onclick='"+this.ClientID+"_changeClass(this,\""+this.ClientID+"_tab1\")'>"+ListName1+"</a></li>";
            }
            if(ListName2!="")
            {
                tabhead += "<li><a  href='#' onclick='" + this.ClientID + "_changeClass(this,\"" + this.ClientID + "_tab2\")'>" + ListName2 + "</a></li>";
            }
            if(ListName3!="")
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
                        SPList list = web.Lists[listName];

                        SPQuery query = new SPQuery();
                        DataTable dt = new DataTable();

                        if (list.BaseTemplate.ToString() != SPListTemplateType.DiscussionBoard.ToString()) query.ViewAttributes = "Scope=\"Recursive\"";
                        query.IncludeAllUserPermissions = true;
                        string sQuery = "<Where><Neq><FieldRef Name='ContentType' /><Value Type='Computed'>Folder</Value> </Neq></Where><OrderBy><FieldRef Name='Modified' Ascending='FALSE' /></OrderBy>";
                        query.RowLimit = (uint)ListItemCount;
                        query.Query = sQuery;

                        
                        SPListItemCollection itemColl = list.GetItems(query);
                        tabhead += "<div id='" + this.ClientID + "_tab" + (i+1).ToString() + "' class='tab' "+(i==0?"":"style='display:none;'")+"><ul>";
                        foreach (SPListItem item in itemColl)
                        {                            
                            if (list.BaseType == SPBaseType.DocumentLibrary)
                            {
                                tabhead += "<li><span class='tabTitle'><a href=\"#\" onclick=\"javascript:openDialog('" + list.DefaultDisplayFormUrl + "?ID=" + item["ID"] + "'); return false;\">";
                                tabhead += item.DisplayName;
                                tabhead += "</a>";
                                tabhead += "</span><span class='tabName'>" + item["Author"].ToString().Split('#')[1] + "</span><span  class='tabDate'>" + Convert.ToDateTime(item["Created"].ToString()).ToShortDateString() + "</span></li>";

                                
                            }
                            else if (list.BaseTemplate.ToString() == SPListTemplateType.Links.ToString())
                            {
                                tabhead += "<li><span class='tabTitle'><a href=\"#\" onclick=\"javascript:openDialog('" + list.DefaultDisplayFormUrl + "?ID=" + item["ID"] + "'); return false;\">";
                                tabhead += item["URL"].ToString().Split(',')[1];
                                tabhead += "</a>";
                                tabhead += "</a>";
                                tabhead += "</span><span class='tabName'>" + item["Author"].ToString().Split('#')[1] + "</span><span  class='tabDate'>" + Convert.ToDateTime(item["Created"].ToString()).ToShortDateString() + "</span></li>";
                            }
                            else if (list.BaseTemplate.ToString() == SPListTemplateType.DiscussionBoard.ToString())
                            {
                                //tabhead += "<li><span class='tabTitle'><a href=\"#\" onclick=\"javascript:openDialog('" + "/" + item["ReplyNoGif"].ToString() + "/flat.aspx?rootfolder=" + HttpUtility.UrlEncode("/" +item["ReplyNoGif"].ToString() + "/" + item["Title"].ToString()) + "&FolderCTID=" + list.ContentTypes[0].Id.ToString() + "'); return false;\">";
                                //tabhead += "<li><span class='tabTitle'><a href='#' onclick=\"javascript:openDialog('" +list.DefaultDisplayFormUrl.Substring(0,list.DefaultDisplayFormUrl.LastIndexOf('/')) +"/flat.aspx?rootfolder="+ HttpUtility.UrlEncode((weburl=="/"?weburl:weburl+"/")+item.Url)+"&FolderCTID=" + list.ContentTypes[0].Id.ToString() + "'); return false;\">";
                                tabhead += "<li><span class='tabTitle'><a href='#' onclick=\"javascript:openDialog('" + list.DefaultDisplayFormUrl.Substring(0, list.DefaultDisplayFormUrl.LastIndexOf('/')) + "/Flat.aspx?rootfolder=" + SPEncode.UrlEncode((weburl == "/" ? weburl : weburl + "/") + item.Url) + "&FolderCTID=" + list.ContentTypes[0].Id.ToString() + "'); return false;\">";
                                tabhead += item.DisplayName + "(" + item["ItemChildCount"].ToString() + ")";
                                tabhead += "</a>";
                                tabhead += "</span><span class='tabName'>" + item["Author"].ToString().Split('#')[1] + "</span><span  class='tabDate'>" + Convert.ToDateTime(item["Created"].ToString()).ToShortDateString() + "</span></li>";
                            }
                            else
                            {
                                tabhead += "<li><span class='tabTitle'><a href=\"#\" onclick=\"javascript:openDialog('" + list.DefaultDisplayFormUrl + "?ID=" + item["ID"] + "'); return false;\">";
                                tabhead += item.DisplayName;
                                tabhead += "</a>";
                                tabhead += "</span><span class='tabName'>" + item["Author"].ToString().Split('#')[1] + "</span><span  class='tabDate'>" + Convert.ToDateTime(item["Created"].ToString()).ToShortDateString() + "</span></li>";
                            }
                        }
                        tabhead += @"</ul>
    <div style='width:100%'><span style='float:right'><a href='"+list.DefaultViewUrl+@"'>more</a></span></div>
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
function "+this.ClientID+@"_changeClass(obj,tabname){
		$('#" + this.ClientID + @"_container #nav li > a').removeClass('active');
		$(obj).addClass('active');
		$('#" + this.ClientID+@"_container .tab').hide();
		$('#'+tabname).show();
}
</script>";

            writer.Write(script);
        }

    }
}
