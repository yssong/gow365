<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 

<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabListWebpart.ascx.cs" Inherits="CircleLogicPortal.TabListWebpart.TabListWebpart" %>


<script type="text/javascript">
   
    ExecuteOrDelayUntilScriptLoaded(Initialize, "sp.js");
    function openDialog(_url) {  
        var options = {  url: _url ,  width: 800, height: 600, };  
        SP.UI.ModalDialog.showModalDialog(options);  
    }

    //Retrieve  the Tab items
function Initialize() {
    //Get the current SP context
    clientContext = new SP.ClientContext.get_current();
    web = clientContext.get_web();
    
	var camlQuery = new SP.CamlQuery();
	
    var q = "<View><Query><Where><Neq><FieldRef Name='ContentType' /><Value Type='Computed'>Folder</Value> </Neq></Where><OrderBy><FieldRef Name='Modified' Ascending='FALSE' /></OrderBy><QueryOptions><RowLimit><%=ListItemCount%></RowLimit></QueryOptions></Query></View>";
    camlQuery.set_viewXml(q);

    <% if (ListName1!="")    { %>
    this.list1 = web.get_lists().getByTitle('<%=ListName1%>');
    this.listItems1 = list1.getItems(camlQuery);
    clientContext.load(list1);
    clientContext.load(listItems1);
    <% } %>

    <% if (ListName2!="")    { %>
    this.list2 = web.get_lists().getByTitle('<%=ListName2%>');
    this.listItems2 = list2.getItems(camlQuery);
    clientContext.load(list2);
    clientContext.load(listItems2);
    <% } %>

    <% if (ListName3!="")    { %>
    this.list3 = web.get_lists().getByTitle('<%=ListName3%>');
    this.listItems3 = list3.getItems(camlQuery);
    clientContext.load(list3);
    clientContext.load(listItems3);
    <% } %>

    clientContext.executeQueryAsync(Function.createDelegate(this, this.onListItemsLoadSuccess), Function.createDelegate(this, this.onListItemsLoadFailed));
}

/*
*
* Initialize delegate function: this function adds the returned items to the HTML lists.
*
*/

function onListItemsLoadSuccess(sender, args) {
	<% if (ListName1!="")    { %>
    $('#<%=this.ClientID %>_tab1 ul li').remove();
    var listtype1 = list1.get_baseTemplate()
    var listEnumerator = this.listItems1.getEnumerator();
    while (listEnumerator.moveNext()) {
        //Retrieve the current list item
        var oListItem = listEnumerator.get_current();
		//Add the items to the list
		var itemHtml = "<li ref='" + oListItem.get_item('ID') + "'>";
			itemHtml += "<a href='#' title='Mark as completed' onClick='javascript:MarkAsComplete(" + oListItem.get_item('ID') + "); return false;'><img src='/_layouts/images/CHECK.GIF' /></a>";
			itemHtml += "<a href='#' title='Delete to-do item' onClick='javascript:DeleteItem(" + oListItem.get_item('ID') + "); return false;'><img src='/_layouts/images/delete.GIF' /></a>";
			itemHtml += oListItem.get_item('Title') + "</li>";
		$('#<%=this.ClientID %>_tab1 ul').append(itemHtml);
    }
    <% } %>
    <% if (ListName2!="")    { %>
    $('#<%=this.ClientID %>_tab2 ul li').remove();
    var listtype2 = list2.get_baseTemplate()
	listEnumerator = this.listItems2.getEnumerator();
    while (listEnumerator.moveNext()) {
        //Retrieve the current list item
        var oListItem = listEnumerator.get_current();
		//Add the items to the list
		var itemHtml = "<li ref='" + oListItem.get_item('ID') + "'>";
			itemHtml += "<a href='#' onClick='javascript:MarkAsComplete(" + oListItem.get_item('ID') + "); return false;'><img src='/_layouts/images/CHECK.GIF' /></a>";
			itemHtml += "<a href='#' onClick='javascript:DeleteItem(" + oListItem.get_item('ID') + "); return false;'><img src='/_layouts/images/delete.GIF' /></a>";
			itemHtml += oListItem.get_item('Title') + "</li>";
		$('#<%=this.ClientID %>_tab2 ul').append(itemHtml);
    }
    <% } %>
    <% if (ListName3!="")    { %>
    $('#<%=this.ClientID %>_tab3 ul li').remove();
    var listtype3 = list3.get_baseTemplate()
    listEnumerator3 = this.listItems3.getEnumerator();
    while (listEnumerator.moveNext()) {
        //Retrieve the current list item
        var oListItem = listEnumerator.get_current();
		//Add the items to the list
		var itemHtml = "<li ref='" + oListItem.get_item('ID') + "'>";
			itemHtml += "<a href='#' title='Mark as completed' onClick='javascript:MarkAsComplete(" + oListItem.get_item('ID') + "); return false;'><img src='/_layouts/images/CHECK.GIF' /></a>";
			itemHtml += "<a href='#' title='Delete to-do item' onClick='javascript:DeleteItem(" + oListItem.get_item('ID') + "); return false;'><img src='/_layouts/images/delete.GIF' /></a>";
			itemHtml += oListItem.get_item('Title') + "</li>";
		$('#<%=this.ClientID %>_tab3 ul').append(itemHtml);
    }
	<% } %>
}
function onListItemsLoadFailed(sender, args) {
	SP.UI.Notify.addNotification("List items load failed: " + args.get_message(), false);
}
    
</script>


<%=ImgUrl%><br />
<%=ListItemCount %><br />
<%=WebName1%><br />
<%=WebName2%><br />
<%=WebName3%><br />
<%=ListName1%><br />
<%=ListName2%><br />
<%=ListName3%><br />
<div class="container" id="<%=this.ClientID %>"_container">
 <div id="tabhead">
  <ul id="nav">
<% if (ListName1!="")    { %>
   <li>
    <a class="active" onclick='<%=this.ClientID %>_changeClass(this,"<%=this.ClientID %>_tab1")' href="#"><%=ListName1%></a>
   </li>
<% } %>
<% if (ListName2!="")    { %>
   <li>
    <a onclick='<%=this.ClientID %>_changeClass(this,"<%=this.ClientID %>_tab2")' href="#"><%=ListName2%></a>
   </li>
<% } %>
<% if (ListName3!="")    { %>
   <li>
    <a onclick='<%=this.ClientID %>_changeClass(this,"<%=this.ClientID %>_tab3")' href="#"><%=ListName3%></a>
   </li>
<% } %>
  </ul>
 </div>
<% if (ListName1!="")    { %>
 <div class="tab" id="<%=this.ClientID %>_tab1">
  <ul id="<%=this.ClientID %>_ul1">
  
  </ul>  
      
  <div style="width: 100%;">
   <span style="float: right;"><a id="<%=this.ClientID %>_link1" href="">more</a></span>
  </div>
 </div>
<% } %>
<% if (ListName2!="")    { %>
 <div class="tab" id="<%=this.ClientID %>_tab2" style="display: none;">
  <ul id="<%=this.ClientID %>_ul2">
  
  </ul>
      
  <div style="width: 100%;">
   <span style="float: right;"><a id="<%=this.ClientID %>_link2" href="">more</a></span>
  </div>
  
 </div>
<% } %>
<% if (ListName3!="")    { %>
 <div class="tab" id="<%=this.ClientID %>_tab3" style="display: none;">
  <ul id="<%=this.ClientID %>_ul3">
  
  </ul>
  
  <div style="width: 100%;">
   <span style="float: right;"><a id="<%=this.ClientID %>_link3"  href="">more</a></span>
  </div>
  
 </div>

<% } %>
</div>