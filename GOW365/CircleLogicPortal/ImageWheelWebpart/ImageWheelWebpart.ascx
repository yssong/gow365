<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageWheelWebpart.ascx.cs" Inherits="CircleLogicPortal.ImageWheelWebpart.ImageWheelWebpart" %>


<link rel="stylesheet" type="text/css" href="<%=ImgUrl%>WaterWheel.css"/>
<script type="text/javascript" src="<%=ImgUrl%>jquery.waterwheelCarousel.js" ></script>
<script type="text/javascript">
   
    ExecuteOrDelayUntilScriptLoaded(<%=this.ClientID%>Initialize, "sp.js");
       
    //Retrieve  the Tab items
function <%=this.ClientID%>Initialize() {
    //Get the current SP context

	var camlQuery = new SP.CamlQuery();
	
    var q = "<View><Query><Where><And><Neq><FieldRef Name='ContentType' /><Value Type='String'>Folder</Value></Neq><Neq><FieldRef Name='ContentType' /><Value Type='String'>폴더</Value></Neq>"+
"</And></Where><OrderBy><FieldRef Name='Modified' Ascending='FALSE' /></OrderBy><QueryOptions><RowLimit><%=ItemCount%></RowLimit></QueryOptions></Query></View>";

    camlQuery.set_viewXml(q);

    <% if (ListName.Trim()!="")    { %>
    <% if (WebName.Trim() != "")
       { %>
    clientContext = new SP.ClientContext("<%=WebName.Trim()%>");
    <% }else { %>
    clientContext = new SP.ClientContext.get_current();
    <% } %>
    web = clientContext.get_web();
    this.<%=this.ClientID %>list = web.get_lists().getByTitle('<%=ListName.Trim()%>');
    
    this.<%=this.ClientID %>listItems = <%=this.ClientID %>list.getItems(camlQuery);
    
    clientContext.load(<%=this.ClientID %>list,"DefaultDisplayFormUrl","BaseTemplate","RootFolder");
    clientContext.load(<%=this.ClientID %>listItems,'Include(ID,FileLeafRef,FileDirRef)');
    
    clientContext.executeQueryAsync(Function.createDelegate(this, this.<%=this.ClientID%>onListItemsLoadSuccess), Function.createDelegate(this, this.onListItemsLoadFailed));
    <% } %>

    
}

function onListItemsLoadFailed(sender, args) {
	SP.UI.Notify.addNotification("List items load failed: " + args.get_message(), false);
}
function <%=this.ClientID%>onListItemsLoadSuccess(sender, args) {
	<% if (ListName!="")    { %>
    try{
        var listtype = <%=this.ClientID %>list.get_baseTemplate();
    
        var listEnumerator = this.<%=this.ClientID %>listItems.getEnumerator();
        var listurl = <%=this.ClientID %>list.get_rootFolder().get_serverRelativeUrl();
        
        while (listEnumerator.moveNext()) {
            itemHtml = "";

            var oListItem = listEnumerator.get_current();
            var filename = oListItem.get_item('FileLeafRef');
            filename = filename.replace('.', '_');
            filename += '.jpg';
            var dir = oListItem.get_item('FileDirRef');
            filename = dir + '/_t/' + filename;

            itemHtml += "<img src='" + filename + "' width='<%=ImgWidth%>px' height='<%=ImgHeight%>px'   id='" + oListItem.get_item("ID") + "'/>";
		    jQuery('#<%=this.ClientID %>_WaterWheel').append(itemHtml);
        }
        //jQuery('#<%=this.ClientID %>_link1').attr("href",<%=this.ClientID %>list1.get_rootFolder().get_serverRelativeUrl());
        
        var wwcarousel = jQuery('#<%=this.ClientID %>_WaterWheel').waterwheelCarousel
		            ({
		                separation :170,flankingItems: 3, autoPlay:3000, 
		                movingToCenter: function ($item) {
		                jQuery('#callback-output').prepend('movingToCenter: ' + $item.attr('id') + '<br/>');
		                },
		                movedToCenter: function ($item) {
		                jQuery('#callback-output').prepend('movedToCenter: ' + $item.attr('id') + '<br/>');
		                },
		                movingFromCenter: function ($item) {
		                jQuery('#callback-output').prepend('movingFromCenter: ' + $item.attr('id') + '<br/>');
		                },
		                movedFromCenter: function ($item) {
		                jQuery('#callback-output').prepend('movedFromCenter: ' + $item.attr('id') + '<br/>');
		                },
		                clickedCenter: function ($item) {
		                jQuery('#callback-output').prepend('clickedCenter: ' + $item.attr('id') + '<br/>');
		                }
		            });

    }
    catch(err)
    {
    }
    <% } %>

}
</script>
<div id='<%=this.ClientID %>_WaterWheel' class='WaterWheel'>

</div>

