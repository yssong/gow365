<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 

<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TabListWebpart.ascx.cs" Inherits="CircleLogicPortal.TabListWebpart.TabListWebpart" %>

<link rel="stylesheet" type="text/css" href="<%=ImgUrl%>TabStyle.css"/>
<script type="text/javascript">
   
    ExecuteOrDelayUntilScriptLoaded(<%=this.ClientID%>Initialize, "sp.js");

    function openDialog(_url) {  
        var options = {  url: _url ,  width: 800, height: 600, };  
        SP.UI.ModalDialog.showModalDialog(options);  
    }
    function <%=this.ClientID%>_changeClass(obj,tabname){
		jQuery('#<%=this.ClientID%>_container #nav li > a').removeClass('active');
		jQuery(obj).addClass('active');
		jQuery('#<%=this.ClientID%>_container .tab').hide();
		jQuery('#'+tabname).show();
    }  


    //Retrieve  the Tab items
function <%=this.ClientID%>Initialize() {
    //Get the current SP context

	var camlQuery = new SP.CamlQuery();
	
    var q = "<View><Query><Where><Neq><FieldRef Name='ContentType' /><Value Type='Computed'>Folder</Value> </Neq></Where><OrderBy><FieldRef Name='Modified' Ascending='FALSE' /></OrderBy><QueryOptions><RowLimit><%=ListItemCount%></RowLimit></QueryOptions></Query></View>";
    camlQuery.set_viewXml(q);

    <% if (ListName1.Trim()!="")    { %>
    <% if (WebName1.Trim() != "")
       { %>
    clientContext1 = new SP.ClientContext("<%=WebName1.Trim()%>");
    <% }else { %>
    clientContext1 = new SP.ClientContext.get_current();
    <% } %>
    web = clientContext1.get_web();
    this.<%=this.ClientID %>list1 = web.get_lists().getByTitle('<%=ListName1.Trim()%>');
    this.<%=this.ClientID %>listContentTypes1 = this.<%=this.ClientID %>list1.get_contentTypes();
    
    this.<%=this.ClientID %>listItems1 = <%=this.ClientID %>list1.getItems(camlQuery);
    
    clientContext1.load(<%=this.ClientID %>list1,"DefaultDisplayFormUrl","BaseTemplate","RootFolder");
    clientContext1.load(<%=this.ClientID %>listContentTypes1);
    clientContext1.load(<%=this.ClientID %>listItems1);
    
    clientContext1.executeQueryAsync(Function.createDelegate(this, this.<%=this.ClientID%>onListItemsLoadSuccess1), Function.createDelegate(this, this.onListItemsLoadFailed));
    <% } %>

    <% if (ListName2.Trim()!="")    { %>
    <% if (WebName2.Trim()!="")    { %>
    clientContext2 = new SP.ClientContext("<%=WebName2.Trim()%>");
    <% }else { %>
    clientContext2 = new SP.ClientContext.get_current();
    <% } %>
    web = clientContext2.get_web();
    this.<%=this.ClientID %>list2 = web.get_lists().getByTitle('<%=ListName2.Trim()%>');
    this.<%=this.ClientID %>listContentTypes2 = this.<%=this.ClientID %>list2.get_contentTypes();
    
    this.<%=this.ClientID %>listItems2 = <%=this.ClientID %>list2.getItems(camlQuery);
    
    clientContext2.load(<%=this.ClientID %>list2,"DefaultDisplayFormUrl","BaseTemplate","RootFolder");
    clientContext2.load(<%=this.ClientID %>listContentTypes2);
    clientContext2.load(<%=this.ClientID %>listItems2);

    clientContext2.executeQueryAsync(Function.createDelegate(this, this.<%=this.ClientID%>onListItemsLoadSuccess2), Function.createDelegate(this, this.onListItemsLoadFailed));
    <% } %>

    <% if (ListName3.Trim()!="")    { %>
    <% if (WebName3.Trim()!="")    { %>
    clientContext3 = new SP.ClientContext("<%=WebName3.Trim()%>");
    
    <% }else { %>
    clientContext3 = new SP.ClientContext.get_current();
    
    <% } %>
    web = clientContext3.get_web();
    this.<%=this.ClientID %>list3 = web.get_lists().getByTitle('<%=ListName3.Trim()%>');
    this.<%=this.ClientID %>listContentTypes3 = this.<%=this.ClientID %>list3.get_contentTypes();
    
    this.<%=this.ClientID %>listItems3 = <%=this.ClientID %>list3.getItems(camlQuery);
    
    clientContext3.load(<%=this.ClientID %>list3,"DefaultDisplayFormUrl","BaseTemplate","RootFolder");
    clientContext3.load(<%=this.ClientID %>listContentTypes3);
    clientContext3.load(<%=this.ClientID %>listItems3);

    clientContext3.executeQueryAsync(Function.createDelegate(this, this.<%=this.ClientID%>onListItemsLoadSuccess3), Function.createDelegate(this, this.onListItemsLoadFailed));
    <% } %>

    
}

function onListItemsLoadFailed(sender, args) {
	SP.UI.Notify.addNotification("List items load failed: " + args.get_message(), false);
}
function <%=this.ClientID%>onListItemsLoadSuccess1(sender, args) {
	<% if (ListName1!="")    { %>
    try{
        jQuery('#<%=this.ClientID %>_tab1 ul li').remove();
        var listtype1 = <%=this.ClientID %>list1.get_baseTemplate();
        //var DisplayURL1 = <%=this.ClientID %>list1.get_defaultDisplayFormUrl();

        var contenttype1 = <%=this.ClientID %>listContentTypes1.itemAt(0).get_id();
    
        var listEnumerator = this.<%=this.ClientID %>listItems1.getEnumerator();
        var listurl = <%=this.ClientID %>list1.get_rootFolder().get_serverRelativeUrl();

        while (listEnumerator.moveNext()) {
            //Retrieve the current list item
            var oListItem = listEnumerator.get_current();
            itemHtml = "";
		    //Add the items to the list
            if (listtype1 == SP.BaseType.DocumentLibrary)
            {
                itemHtml += "<li><span class='tabTitle'><a href=\"#\" onclick=\"javascript:openDialog('" + <%=this.ClientID %>list1.get_defaultDisplayFormUrl()
     + "?ID=" + oListItem.get_item('ID') + "'); return false;\">";
                itemHtml += oListItem.get_item('DisplayName');
                itemHtml += "</a>";
                itemHtml += "</span><span class='tabName'>" + oListItem.get_item("Editor").get_lookupValue() + "</span><span  class='tabDate'>" + oListItem.get_item("Modified").format("yyyy-MM-dd") + "</span></li>";

                                
            }
            else if (listtype1 == SP.ListTemplateType.links)
            {
                itemHtml += "<li><span class='tabTitle'><a href=\"#\" onclick=\"javascript:openDialog('" + <%=this.ClientID %>list1.DefaultDisplayFormUrl + "?ID=" +  oListItem.get_item('ID') + "'); return false;\">";
                itemHtml += oListItem.get_item("URL");
                itemHtml += "</a>";
                itemHtml += "</span><span class='tabName'>" + oListItem.get_item("Editor").get_lookupValue() + "</span><span  class='tabDate'>" + oListItem.get_item("Modified").format("yyyy-MM-dd") + "</span></li>";
            }
            else if (listtype1 == SP.ListTemplateType.discussionBoard)
            {
                var rooturl = escape(listurl+"/"+oListItem.get_item("Title"));
                itemHtml += "<li><span class='tabTitle'><a href='#' onclick=\"javascript:openDialog('" + listurl + "/Flat.aspx?rootfolder=" + rooturl + "&FolderCTID=" + contenttype1 + "'); return false;\">";
                itemHtml += oListItem.get_item("Title") + "(" + oListItem.get_item("ItemChildCount") + ")";
                itemHtml += "</a>";
                itemHtml += "</span><span class='tabName'>" + oListItem.get_item("Editor").get_lookupValue() + "</span><span  class='tabDate'>" + oListItem.get_item("Modified").format("yyyy-MM-dd") + "</span></li>";
            }
            else
            {
                itemHtml += "<li><span class='tabTitle'><a href=\"#\" onclick=\"javascript:openDialog('" + <%=this.ClientID %>list1.get_defaultDisplayFormUrl() + "?ID=" +  oListItem.get_item('ID') + "'); return false;\">";
                itemHtml += oListItem.get_item("Title");
                itemHtml += "</a>";
                itemHtml += "</span><span class='tabName'>" + oListItem.get_item("Editor").get_lookupValue() + "</span><span  class='tabDate'>" + oListItem.get_item("Modified").format("yyyy-MM-dd") + "</span></li>";
            }
		
		    jQuery('#<%=this.ClientID %>_tab1 ul').append(itemHtml);
        }
        jQuery('#<%=this.ClientID %>_link1').attr("href",<%=this.ClientID %>list1.get_rootFolder().get_serverRelativeUrl());
    }
    catch(err)
    {
    }
    <% } %>

}
function <%=this.ClientID%>onListItemsLoadSuccess2(sender, args) {
    <% if (ListName2!="")    { %>
    try
    {
        jQuery('#<%=this.ClientID %>_tab2 ul li').remove();
        var listtype1 = <%=this.ClientID %>list2.get_baseTemplate();
        var contenttype2 = <%=this.ClientID %>listContentTypes2.itemAt(0).get_id();
    
        var listEnumerator = this.<%=this.ClientID %>listItems2.getEnumerator();
        var listurl = <%=this.ClientID %>list2.get_rootFolder().get_serverRelativeUrl();

        while (listEnumerator.moveNext()) {
            //Retrieve the current list item
            var oListItem = listEnumerator.get_current();
            itemHtml = "";
		    //Add the items to the list
            if (listtype1 == SP.BaseType.DocumentLibrary)
            {
                itemHtml += "<li><span class='tabTitle'><a href=\"#\" onclick=\"javascript:openDialog('" + <%=this.ClientID %>list2.get_defaultDisplayFormUrl()
     + "?ID=" + oListItem.get_item('ID') + "'); return false;\">";
                itemHtml += oListItem.get_item('DisplayName');
                itemHtml += "</a>";
                itemHtml += "</span><span class='tabName'>" + oListItem.get_item("Editor").get_lookupValue() + "</span><span  class='tabDate'>" + oListItem.get_item("Modified").format("yyyy-MM-dd") + "</span></li>";

            }
            else if (listtype1 == SP.ListTemplateType.links)
            {
                itemHtml += "<li><span class='tabTitle'><a href=\"#\" onclick=\"javascript:openDialog('" + <%=this.ClientID %>list2.DefaultDisplayFormUrl + "?ID=" +  oListItem.get_item('ID') + "'); return false;\">";
                itemHtml += oListItem.get_item("URL");
                itemHtml += "</a>";
                itemHtml += "</span><span class='tabName'>" + oListItem.get_item("Editor").get_lookupValue() + "</span><span  class='tabDate'>" + oListItem.get_item("Modified").format("yyyy-MM-dd") + "</span></li>";
            }
            else if (listtype1 == SP.ListTemplateType.discussionBoard)
            {
                var rooturl = escape(listurl+"/"+oListItem.get_item("Title"));
                itemHtml += "<li><span class='tabTitle'><a href='#' onclick=\"javascript:openDialog('" + listurl + "/Flat.aspx?rootfolder=" + rooturl + "&FolderCTID=" + contenttype2 + "'); return false;\">";
                itemHtml += oListItem.get_item("Title") + "(" + oListItem.get_item("ItemChildCount") + ")";
                itemHtml += "</a>";
                itemHtml += "</span><span class='tabName'>" + oListItem.get_item("Editor").get_lookupValue() + "</span><span  class='tabDate'>" + oListItem.get_item("Modified").format("yyyy-MM-dd") + "</span></li>";
            }
            else
            {
                itemHtml += "<li><span class='tabTitle'><a href=\"#\" onclick=\"javascript:openDialog('" + <%=this.ClientID %>list2.get_defaultDisplayFormUrl() + "?ID=" +  oListItem.get_item('ID') + "'); return false;\">";
                itemHtml += oListItem.get_item("Title");
                itemHtml += "</a>";
                itemHtml += "</span><span class='tabName'>" + oListItem.get_item("Editor").get_lookupValue() + "</span><span  class='tabDate'>" + oListItem.get_item("Modified").format("yyyy-MM-dd") + "</span></li>";
            }
		
		    jQuery('#<%=this.ClientID %>_tab2 ul').append(itemHtml);
        }
        jQuery('#<%=this.ClientID %>_link2').attr("href",<%=this.ClientID %>list2.get_rootFolder().get_serverRelativeUrl());
     }
    catch(err)
    {
    }
    <% } %>

}
function <%=this.ClientID%>onListItemsLoadSuccess3(sender, args) {
	
    <% if (ListName3!="")    { %>
    try
    {
        jQuery('#<%=this.ClientID %>_tab3 ul li').remove();
        var listtype1 = <%=this.ClientID %>list3.get_baseTemplate();
    
        var contenttype3 = <%=this.ClientID %>listContentTypes3.itemAt(0).get_id();
    
        var listEnumerator = this.<%=this.ClientID %>listItems3.getEnumerator();
        var listurl = <%=this.ClientID %>list3.get_rootFolder().get_serverRelativeUrl();

        while (listEnumerator.moveNext()) {
            //Retrieve the current list item
            var oListItem = listEnumerator.get_current();
            itemHtml = "";
		    //Add the items to the list
            if (listtype1 == SP.BaseType.DocumentLibrary)
            {
                itemHtml += "<li><span class='tabTitle'><a href=\"#\" onclick=\"javascript:openDialog('" + <%=this.ClientID %>list3.get_defaultDisplayFormUrl()
     + "?ID=" + oListItem.get_item('ID') + "'); return false;\">";
                itemHtml += oListItem.get_item('DisplayName');
                itemHtml += "</a>";
                itemHtml += "</span><span class='tabName'>" + oListItem.get_item("Editor").get_lookupValue() + "</span><span  class='tabDate'>" + oListItem.get_item("Modified").format("yyyy-MM-dd") + "</span></li>";

                                
            }
            else if (listtype1 == SP.ListTemplateType.links)
            {
                itemHtml += "<li><span class='tabTitle'><a href=\"#\" onclick=\"javascript:openDialog('" + <%=this.ClientID %>list3.DefaultDisplayFormUrl + "?ID=" +  oListItem.get_item('ID') + "'); return false;\">";
                itemHtml += oListItem.get_item("URL");
                itemHtml += "</a>";
                itemHtml += "</span><span class='tabName'>" + oListItem.get_item("Editor").get_lookupValue() + "</span><span  class='tabDate'>" + oListItem.get_item("Modified").format("yyyy-MM-dd") + "</span></li>";
            }
            else if (listtype1 == SP.ListTemplateType.discussionBoard)
            {
                var rooturl = escape(listurl+"/"+oListItem.get_item("Title"));
                itemHtml += "<li><span class='tabTitle'><a href='#' onclick=\"javascript:openDialog('" + listurl + "/Flat.aspx?rootfolder=" + rooturl + "&FolderCTID=" + contenttype3 + "'); return false;\">";
                itemHtml += oListItem.get_item("Title") + "(" + oListItem.get_item("ItemChildCount") + ")";
                itemHtml += "</a>";
                itemHtml += "</span><span class='tabName'>" + oListItem.get_item("Editor").get_lookupValue() + "</span><span  class='tabDate'>" + oListItem.get_item("Modified").format("yyyy-MM-dd") + "</span></li>";
            }
            else
            {
                itemHtml += "<li><span class='tabTitle'><a href=\"#\" onclick=\"javascript:openDialog('" + <%=this.ClientID %>list3.get_defaultDisplayFormUrl() + "?ID=" +  oListItem.get_item('ID') + "'); return false;\">";
                itemHtml += oListItem.get_item("Title");
                itemHtml += "</a>";
                itemHtml += "</span><span class='tabName'>" + oListItem.get_item("Editor").get_lookupValue() + "</span><span  class='tabDate'>" + oListItem.get_item("Modified").format("yyyy-MM-dd") + "</span></li>";
            }
		
		    jQuery('#<%=this.ClientID %>_tab3 ul').append(itemHtml);
        }
        jQuery('#<%=this.ClientID %>_link3').attr("href",<%=this.ClientID %>list3.get_rootFolder().get_serverRelativeUrl());
    }
    catch(err)
    {
    }
	<% } %>
}


</script>
<div class="container" id="<%=this.ClientID %>_container">
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