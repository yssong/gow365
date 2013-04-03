<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteMap.ascx.cs" Inherits="CircleLogicPortal.SiteMap.SiteMap" %>

<link rel="stylesheet" type="text/css" href="<%=ImgUrl%>SiteMap.css"/>


<script type="text/javascript">

    ExecuteOrDelayUntilScriptLoaded(<%=this.ClientID%>Initialize, "sp.js");

    //Retrieve  the Tab items
    function <%=this.ClientID%>Initialize() {
        //Get the current SP context
        clientContext = new SP.ClientContext.get_current();
        web = clientContext.get_web();
        this.<%=this.ClientID %>webcols = web.getSubwebsForCurrentUser(null)
        clientContext.load(<%=this.ClientID %>webcols);
        try {
            clientContext.executeQueryAsync(Function.createDelegate(this, this.<%=this.ClientID%>_SitesLoadSuccess), Function.createDelegate(this, this.onListItemsLoadFailed));
        }
        catch (err)
        {
        }
    }

    function onListItemsLoadFailed(sender, args) {
        SP.UI.Notify.addNotification("List items load failed: " + args.get_message(), false);
    }

    function <%=this.ClientID%>_SitesLoadSuccess(sender, args) {
        try {
            var webEnumerator = this.<%=this.ClientID %>webcols.getEnumerator();
            <%=this.ClientID %>navs = {};
            var i = 0;
            while (webEnumerator.moveNext()) {
                
                var oWeb = webEnumerator.get_current();
                
                //<div class="sitebox"><h3>팀사이트</h3><ul class="sitemapbox"><li><a>사이트 콘텐츠</a></li></ul></div>
                var itemHtml = "<div class=\"sitebox\" id=\"<%=this.ClientID%>web" + i.toString() + "\"><h3><a href='" + oWeb.get_url() + "'>" + oWeb.get_title() + "</h3><ul class=\"sitemapbox\" id=\"<%=this.ClientID%>sitemapbox" + i.toString() + "\"></ul></div>";
                
                <%=this.ClientID %>navs[i] = oWeb.get_navigation().get_quickLaunch();
                clientContext.load(<%=this.ClientID %>navs[i]);

                jQuery('#<%=this.ClientID %>_sitemap').append(itemHtml);
                i++;
            }
        }
        catch (err) {
        }
        
        
        try {
            clientContext.executeQueryAsync(Function.createDelegate(this, this.<%=this.ClientID%>_NavsLoadSuccess), Function.createDelegate(this, this.onListItemsLoadFailed));
        }
        catch (err) {
        }
    }
    function <%=this.ClientID%>_NavsLoadSuccess(sender, args) {
        try {
            <%=this.ClientID %>childnavs = new Array();
            for (var nav in <%=this.ClientID %>navs)
            {
                var qEnumerator = <%=this.ClientID %>navs[nav].getEnumerator();
                var cno = 0;
                <%=this.ClientID %>childnavs[nav] = new Array();
                while (qEnumerator.moveNext()) {
                    var quickLaunch = qEnumerator.get_current();
                    var itemHtml = "<li id='<%=this.ClientID %>li"+nav.toString()+cno.toString()+"'><a>" + quickLaunch.get_title() + "</a></li>";
                    jQuery('#<%=this.ClientID %>sitemapbox' + nav.toString()).append(itemHtml);
                    <%=this.ClientID %>childnavs[nav][cno] = quickLaunch.get_children();
                    clientContext.load(<%=this.ClientID %>childnavs[nav][cno]);
                    cno++;
                }
            }
        }
        catch (err) {
        }
        try {
            clientContext.executeQueryAsync(Function.createDelegate(this, this.<%=this.ClientID%>_CNavsLoadSuccess), Function.createDelegate(this, this.onListItemsLoadFailed));
        }
        catch (err) {
        }

    }
    function <%=this.ClientID%>_CNavsLoadSuccess(sender, args) {
        try {
            for (var i = 0; i < <%=this.ClientID %>childnavs.length; i++) {
                for (var j = 0; j < <%=this.ClientID %>childnavs[i].length; j++) {
                    var qEnumerator = <%=this.ClientID %>childnavs[i][j].getEnumerator();
                    var cno = 0;
                    var itemHtml ="<ul class='sitemapnormal'>";
                    while (qEnumerator.moveNext()) {
                        var quickLaunch = qEnumerator.get_current();
                        itemHtml += "<li><a>" + quickLaunch.get_title() + "</a></li>";
                        cno++;
                    }
                    itemHtml += "</ul>"
                    jQuery('#<%=this.ClientID %>li' + i.toString() + j.toString()).append(itemHtml);
                }
            }
                
        }
        catch (err) {
        }
      

    }

</script>

<div class="sitemap_head">
	<img src="<%=ImgUrl%>sitemap_head.gif" />
</div>
<div class="sitemap" id="<%=this.ClientID %>_sitemap">
	
</div>

