<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApprovalStatusWebPart.ascx.cs" Inherits="CircleLogicPortal.ApprovalStatusWebPart.ApprovalStatusWebPart" %>


<link rel="stylesheet" type="text/css" href="<%=ImgUrl%>ApprovalStatus.css"/>
<script type="text/javascript">

    ExecuteOrDelayUntilScriptLoaded(<%=this.ClientID%>Initialize, "sp.js");
    function <%=this.ClientID%>Initialize() {
        <% if (ListName1.Trim()!="")    { %>

        <% if (WebName1.Trim() != "")
           { %>
            <%=this.ClientID%>clientContext = new SP.ClientContext("<%=WebName1.Trim()%>");
        <% }else { %>
            <%=this.ClientID%>clientContext = new SP.ClientContext.get_current();
        <% } %>
        
        var web = <%=this.ClientID%>clientContext.get_web();
        var list = web.get_lists().getByTitle("<%=ListName1.Trim()%>");
        var camlQuery = new SP.CamlQuery();

        var q1 = '<View><Query><Where><And><And><And><Neq><FieldRef Name="aprStatus" /><Value Type="Text">임시저장</Value></Neq><Neq><FieldRef Name="aprStatus" /><Value Type="Text">반려</Value></Neq></And><Neq><FieldRef Name="aprStatus" /><Value Type="Text">승인완료</Value></Neq></And><Contains><FieldRef Name=\'curApprover\' /><Value Type=\'Lookup\'><UserID/></Value></Contains></And></Where></Query></View>';
        camlQuery.set_viewXml(q1);
        <%=this.ClientID%>listItems1 = list.getItems(camlQuery);
        <%=this.ClientID%>clientContext.load(<%=this.ClientID%>listItems1);

        //var q2 = '<View><Query><Where><And><And><And><Neq><FieldRef Name="aprStatus" /><Value Type="Text">임시저장</Value></Neq><Neq><FieldRef Name="aprStatus" /><Value Type="Text">반려</Value></Neq></And><Neq><FieldRef Name="aprStatus" /><Value Type="Text">승인완료</Value></Neq></And><Eq><FieldRef Name="curApprover" /><Value Type="Integer">' + _spPageContextInfo.userId + '</Value></Eq></And></Where></Query></View>';
        var q2 = '<OrderBy><FieldRef Name="submitted" Ascending="FALSE" /></OrderBy><Where><And><And><And><Or><Or><Or><Or><Eq><FieldRef Name="approver1" /><Value Type="Integer"><UserID Type="Integer" /></Value></Eq><Eq><FieldRef Name="approver2" /><Value Type="Integer"><UserID Type="Integer" /></Value></Eq></Or><Eq><FieldRef Name="approver3" /><Value Type="Integer"><UserID Type="Integer" /></Value></Eq></Or><Eq><FieldRef Name="approver4" /><Value Type="Integer"><UserID Type="Integer" /></Value></Eq></Or><Eq><FieldRef Name="reviewer" /><Value Type="Integer"><UserID Type="Integer" /></Value></Eq></Or><Neq><FieldRef Name="aprStatus" /><Value Type="Text">임시저장</Value></Neq></And><Neq><FieldRef Name="aprStatus" /><Value Type="Text">승인완료</Value></Neq></And><Neq><FieldRef Name="aprStatus" /><Value Type="Text">반려</Value></Neq></And></Where>';
        camlQuery.set_viewXml(q2);
        <%=this.ClientID%>listItems2 = list.getItems(camlQuery);
        <%=this.ClientID%>clientContext.load(<%=this.ClientID%>listItems2);

        //var q3 = '<View><Query><Where><And><And><And><Neq><FieldRef Name="aprStatus" /><Value Type="Text">임시저장</Value></Neq><Neq><FieldRef Name="aprStatus" /><Value Type="Text">반려</Value></Neq></And><Neq><FieldRef Name="aprStatus" /><Value Type="Text">승인완료</Value></Neq></And><Eq><FieldRef Name="curApprover" /><Value Type="Integer">' + _spPageContextInfo.userId + '</Value></Eq></And></Where></Query></View>';
        //var q3 = '<View><Query><Where><And><And><And><Neq><FieldRef Name="aprStatus" /><Value Type="Text">임시저장</Value></Neq><Neq><FieldRef Name="aprStatus" /><Value Type="Text">반려</Value></Neq></And><Neq><FieldRef Name="aprStatus" /><Value Type="Text">승인완료</Value></Neq></And><Contains><FieldRef Name="curApprover" /><Value Type="UserMulti">UserID</Value></Contains></And></Where></Query></View>';
        //camlQuery.set_viewXml(q3);
        //<%=this.ClientID%>listItems3 = list.getItems(camlQuery);
        //<%=this.ClientID%>clientContext.load(<%=this.ClientID%>listItems2);
        
        <%=this.ClientID%>clientContext.executeQueryAsync(Function.createDelegate(this, this.<%=this.ClientID%>onListItemsLoadSuccess),
        Function.createDelegate(this, this.<%=this.ClientID%>onQueryFailed));

        <%} %>
    }
    function <%=this.ClientID%>onListItemsLoadSuccess(sender, args) {        
        alert(this.<%=this.ClientID%>listItems1.get_count());
        alert(this.<%=this.ClientID%>listItems2.get_count());
        //alert(this.<%=this.ClientID%>listItems3.get_count());
    }

    function <%=this.ClientID%>onQueryFailed(sender, args) {
        alert('request failed ' + args.get_message() + '\n' + args.get_stackTrace());
    }
</script>

