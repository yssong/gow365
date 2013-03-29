<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubSitesTabWepart.ascx.cs" Inherits="CircleLogicPortal.SubSitesTabWepart.SubSitesTabWepart" %>

<link rel="stylesheet" type="text/css" href="<%=ImgUrl%>jquery-ui-1.10.0.custom.min.css"/>
<script type="text/javascript" src="<%=ImgUrl%>jquery-ui-1.10.0.min.js"></script>
<script type="text/javascript">
   
    ExecuteOrDelayUntilScriptLoaded(<%=this.ClientID%>Initialize, "sp.js");
       
    //Retrieve  the Tab items
function <%=this.ClientID%>Initialize() {
    //Get the current SP context

    <% if (TabTitle1.Trim() != "")
       { %>
    <% if (WebName1.Trim() != "")
       { %>
    clientContext1 = new SP.ClientContext("<%=WebName1.Trim()%>");
    <% }else { %>
    clientContext1 = new SP.ClientContext.get_current();
    <% } %>
    web = clientContext1.get_web();
    this.<%=this.ClientID %>webcols1 = web.getSubwebsForCurrentUser(null)
    clientContext1.load(<%=this.ClientID %>webcols1);
    try{
    clientContext1.executeQueryAsync(Function.createDelegate(this, this.<%=this.ClientID%>subSitesLoadSuccess1), Function.createDelegate(this, this.onListItemsLoadFailed));
    }catch(err)
    {
    }
    jQuery('#<%=this.ClientID%>_tab').tabs();
    <% } %>

    <% if (TabTitle2.Trim() != "")
       { %>
    <% if (WebName2.Trim()!="")    { %>
    clientContext2 = new SP.ClientContext("<%=WebName2.Trim()%>");
    <% }else { %>
    clientContext2 = new SP.ClientContext.get_current();
    <% } %>
    web = clientContext2.get_web();
    this.<%=this.ClientID %>webcols2 = web.getSubwebsForCurrentUser(null)
    clientContext2.load(<%=this.ClientID %>webcols2);
    try{
    clientContext2.executeQueryAsync(Function.createDelegate(this, this.<%=this.ClientID%>subSitesLoadSuccess2), Function.createDelegate(this, this.onListItemsLoadFailed));
    }catch(err)
    {
    }
    <% } %>

    <% if (TabTitle3.Trim() != "")
       { %>
    <% if (WebName3.Trim()!="")    { %>
    clientContext3 = new SP.ClientContext("<%=WebName3.Trim()%>");
    
    <% }else { %>
    clientContext3 = new SP.ClientContext.get_current();
    
    <% } %>
    web = clientContext3.get_web();
    this.<%=this.ClientID %>webcols3 = web.getSubwebsForCurrentUser(null)
    clientContext3.load(<%=this.ClientID %>webcols3);
    try{
    clientContext3.executeQueryAsync(Function.createDelegate(this, this.<%=this.ClientID%>subSitesLoadSuccess3), Function.createDelegate(this, this.onListItemsLoadFailed));
    }catch(err)
    {
    }
    <% } %>

    
}

function onListItemsLoadFailed(sender, args) {
	SP.UI.Notify.addNotification("List items load failed: " + args.get_message(), false);
}
function <%=this.ClientID%>subSitesLoadSuccess1(sender, args) {
	<% if (TabTitle1!="")    { %>
    try{
        var webEnumerator = this.<%=this.ClientID %>webcols1.getEnumerator();
        
        while (webEnumerator.moveNext())
        {
            var oWeb = webEnumerator.get_current();
		    var itemHtml ="<li><span class='weblist'><a href='"+oWeb.get_url()+"' target='_new'>"+oWeb.get_title()+"</a></span></li>";
		    jQuery('#<%=this.ClientID %>_tab1 ul').append(itemHtml);
        }
    }
    catch(err)
    {
    }
    <% } %>

}

function <%=this.ClientID%>subSitesLoadSuccess2(sender, args) {
	<% if (TabTitle2!="")    { %>
    try{
        var webEnumerator = this.<%=this.ClientID %>webcols2.getEnumerator();
        
        while (webEnumerator.moveNext())
        {
            var oWeb = webEnumerator.get_current();
		    var itemHtml ="<li><span class='weblist'><a href='"+oWeb.get_url()+"' target='_new'>"+oWeb.get_title()+"</a></span></li>";
		    		
		    jQuery('#<%=this.ClientID %>_tab2 ul').append(itemHtml);
        }
    }
    catch(err)
    {
    }
    <% } %>

}
function <%=this.ClientID%>subSitesLoadSuccess3(sender, args) {
	<% if (TabTitle3!="")    { %>
    try{
        var webEnumerator = this.<%=this.ClientID %>webcols3.getEnumerator();
        
        while (webEnumerator.moveNext())
        {
            var oWeb = webEnumerator.get_current();
		    var itemHtml ="<li><span class='weblist'><a href='"+oWeb.get_url()+"' target='_new'>"+oWeb.get_title()+"</a></span></li>";
		    		
		    jQuery('#<%=this.ClientID %>_tab3 ul').append(itemHtml);
        }
    }
    catch(err)
    {
    }
    <% } %>

}

</script>





<div id='<%=this.ClientID %>_tab' >
    <ul>
        <%if(TabTitle1.Trim()!=""){ %>
        <li>
            <a href='#<%=this.ClientID %>_tab1'><%=TabTitle1%></a>
        </li>
        <%} %>
        <%if(TabTitle2.Trim()!=""){ %>
        <li>
            <a href='#<%=this.ClientID %>_tab2'><%=TabTitle2%></a>
        </li>
        <%} %>
        <%if(TabTitle3.Trim()!=""){ %>
        <li>
            <a href='#<%=this.ClientID %>_tab3'><%=TabTitle3%></a>
        </li>
        <%} %>
    </ul>
    <%if(TabTitle1.Trim()!=""){ %>
    <div id='<%=this.ClientID %>_tab1'>
        <ul>
            

        </ul>

    </div>
    <%} %>
    <%if(TabTitle2.Trim()!=""){ %>
    <div id='<%=this.ClientID %>_tab2'>
        <ul>
        </ul>
    </div>
    <%} %>
    <%if(TabTitle3.Trim()!=""){ %>
    <div id='<%=this.ClientID %>_tab3'>
        <ul>
        </ul>
    </div>
    <%} %>
</div>