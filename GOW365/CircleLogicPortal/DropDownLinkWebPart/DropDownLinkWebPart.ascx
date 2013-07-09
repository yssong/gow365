<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DropDownLinkWebPart.ascx.cs" Inherits="CircleLogicPortal.DropDownLinkWebPart.DropDownLinkWebPart" %>


<script type="text/javascript">
   
    ExecuteOrDelayUntilScriptLoaded(<%=this.ClientID%>Initialize, "sp.js");
       
    //Retrieve  the Tab items
function <%=this.ClientID%>Initialize() {
    //Get the current SP context

	var camlQuery = new SP.CamlQuery();
	
    var q = "<View><Query><Where><And><Neq><FieldRef Name='ContentType' /><Value Type='String'>Folder</Value></Neq><Neq><FieldRef Name='ContentType' /><Value Type='String'>폴더</Value></Neq>"+
"</And></Where><OrderBy><FieldRef Name='Title' Ascending='TRUE' /></OrderBy></Query></View>";

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
    clientContext.load(<%=this.ClientID %>listItems);
    
    clientContext.executeQueryAsync(Function.createDelegate(this, this.<%=this.ClientID%>onListItemsLoadSuccess), Function.createDelegate(this, this.onListItemsLoadFailed));
    <% } %>

    
}

function onListItemsLoadFailed(sender, args) {
	SP.UI.Notify.addNotification("List items load failed: " + args.get_message(), false);
}
function <%=this.ClientID%>onListItemsLoadSuccess(sender, args) {
	<% if (ListName!="")    { %>
     try{
        jQuery('#<%=this.ClientID %>_tab1 ul li').remove();
        var listtype1 = <%=this.ClientID %>list.get_baseTemplate();
         
        var listEnumerator = this.<%=this.ClientID %>listItems.getEnumerator();
        var listurl = <%=this.ClientID %>list.get_rootFolder().get_serverRelativeUrl();

        while (listEnumerator.moveNext()) {
            //Retrieve the current list item
            var oListItem = listEnumerator.get_current();
            itemHtml = "";
		   
            if (listtype1 == SP.ListTemplateType.links)
            {
                itemHtml += "<option id='<%=this.ClientID%>"+oListItem.get_item("ID")+"' value='"+oListItem.get_item("URL").get_url()+"'>";
                itemHtml += oListItem.get_item("URL").get_description();
                itemHtml += "</option>";
                
            }
            else if (listtype1 == 170)
            {
                itemHtml += "<option id='<%=this.ClientID%>"+oListItem.get_item("ID")+"' value='"+oListItem.get_item("LinkLocation").get_url()+"'>";
                itemHtml += oListItem.get_item("Title");
                itemHtml += "</option>";
            }
            jQuery('#<%=this.ClientID %>_select').append(itemHtml);
        }
        
    }
    catch(err)
    {
    }
    <% } %>

}
</script>
<div id='<%=this.ClientID %>_tab'>

</div>
<select id="<%=this.ClientID %>_select" style="width:<%=ControlWidth%>px; vertical-align:middle; border:#DEDEDE 2px solid;" onchange="if(this.options[this.selectedIndex].value != ''){var pop = window.open(this.options[this.selectedIndex].value,'',''); if(pop)window.focus();else{    var timer = window.setTimeout( function(){ if(pop)win.focus(); }, 100 );};this.selectedIndex = 0; return false;}">
    <option id="<%=this.ClientID %>" value="">:::Site:::</option>
</select>

