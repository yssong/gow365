<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberSearch.ascx.cs" Inherits="CircleLogicPortal.MemberSearch.MemberSearch" %>

<link rel="stylesheet" href="<%=ImgUrl%>membersearch.css" />
<script type="text/javascript">
function <%=this.ClientID%>memberSearch() {

    var userName=document.getElementById("<%=this.ClientID%>name").value;
	var clientContext = new SP.ClientContext.get_current();
	var web = clientContext.get_web();
	var userInfoList = web.get_siteUserInfoList();
	var camlQuery = new SP.CamlQuery();
	camlQuery.set_viewXml("<View><Query><Where><And><Contains><FieldRef Name='Title'/><Value Type='Text'>" + userName + 
    "</Value></Contains><IsNotNull><FieldRef Name='UserName' /></IsNotNull></And></Where></Query></View>");
	var <%=this.ClientID %>_webusers= userInfoList.getItems(camlQuery);
	clientContext.load(<%=this.ClientID %>_webusers);
	
	clientContext.executeQueryAsync(
		function (sender, args) {
            jQuery('#<%=this.ClientID %>items ul li').remove();
			var <%=this.ClientID %>listItemEnumerator = <%=this.ClientID %>_webusers.getEnumerator();
        	while (<%=this.ClientID %>listItemEnumerator.moveNext()) {
                var <%=this.ClientID %>itemStr = "";
				var listItem = <%=this.ClientID %>listItemEnumerator.get_current();
				var loginID = listItem.get_item('Name');
				var DisplayName = listItem.get_item('Title');
				var loginName= listItem.get_item('UserName');
				var userId= listItem.get_id();
                var MobilePhone = (listItem.get_item('MobilePhone')==null?"":listItem.get_item('MobilePhone'));
                var WorkPhone = (listItem.get_item('WorkPhone')==null?"":listItem.get_item('WorkPhone'));
                var JobTitle = (listItem.get_item('JobTitle')==null?"":listItem.get_item('JobTitle'));
                var Department = (listItem.get_item('Department')==null?"":listItem.get_item('Department'));
                var Email = (listItem.get_item('EMail')==null?"":listItem.get_item('EMail'));
                var picture = (listItem.get_item('Picture')==null?"<%=ImgUrl%>nopicture.gif":listItem.get_item('Picture').get_url());
                
                <%=this.ClientID %>itemstr="<li class='searchresult'>"+"<div class='photo'><img width=40 height=40 src='"+picture +"'/></div>";
                <%=this.ClientID %>itemstr+="<div class='userInfo'><div class='uName'>"+DisplayName+
                "</div><div class='uDept'>"+Department+" / "+JobTitle+
                "</div><div class='uPhone'>"+MobilePhone+"</div><div class='uPhone'>"+WorkPhone+
                "</div><div class='uMail'>"+Email+"</div></div></li>";
                jQuery('#<%=this.ClientID %>items ul').append(<%=this.ClientID %>itemstr);
			}
			
		},
		function (sender, args) {
			return null;
		});
}
</script>
<div class="membersearch">
 <div class="search_head">
   <span class="search_title">직원 검색</span>
 </div>
 <div class="search_content">
  <table border="0" cellSpacing="0" cellPadding="0">
   <tbody>
    <tr>
     <td height="28" style="padding-bottom: 3px;">
       <label for="textfield"></label><input name="textfield" id="<%=this.ClientID%>name" style="height: 25px;" type="text" size="15" onkeydown="if(event.keyCode == 13) {event.returnValue=false;<%=this.ClientID%>memberSearch()}" /> 
     </td>
     <td align="left" class="search_btn" vAlign="top" rowSpan="2">
      <img src="<%=ImgUrl%>search_btn_32.gif"  onclick="<%=this.ClientID%>memberSearch()"/>
     </td>
    </tr>
   </tbody>
  </table>
 </div>
 <div id="<%=this.ClientID %>items">
     <ul class="searchItems">

     </ul>
 </div>
</div>
