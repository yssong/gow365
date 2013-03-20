<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GetSurveyWebPart.ascx.cs" Inherits="CircleLogicPortal.GetSurveyWebPart.GetSurveyWebPart" %>

<link rel="stylesheet" type="text/css" href="<%=ImgUrl%>SurveyStyle.css" />
<script type="text/javascript">
   
    ExecuteOrDelayUntilScriptLoaded(<%=this.ClientID%>Initialize, "sp.js");
       
    //Retrieve  the Tab items
function <%=this.ClientID%>Initialize() {
    //Get the current SP context

	var camlQuery = new SP.CamlQuery();
	   
    camlQuery.set_viewXml(q);

    
    <% if (SiteUrl.Trim() != "")
       { %>
    clientContext = new SP.ClientContext("<%=SiteUrl.Trim()%>");
    <% }else { %>
    clientContext = new SP.ClientContext.get_current();
    <% } %>

    web = clientContext.get_web();
    this.<%=this.ClientID %>lists = clientContext.web.get_lists();
    
    clientContext.load(<%=this.ClientID %>lists,"DefaultDisplayFormUrl","BaseTemplate","RootFolder");
    
    clientContext.executeQueryAsync(Function.createDelegate(this, this.<%=this.ClientID%>onListItemsLoadSuccess), Function.createDelegate(this, this.onListItemsLoadFailed));
    

}

function onListItemsLoadFailed(sender, args) {
	SP.UI.Notify.addNotification("List items load failed: " + args.get_message(), false);
}
function <%=this.ClientID%>onListItemsLoadSuccess(sender, args) {
	
    try{
        
       

    }
    catch(err)
    {
    }
   

}
</script>

<table width='100%' border='0' cellspacing='0' cellpadding='0'>
    <tr>
        <td width='8' height='8' background='<%=ImgUrl %>survey_box_top_l.gif' style='background-repeat: no-repeat;'></td>
        <td height='8' background='<%=ImgUrl %>survey_box_top.gif' style='background-repeat: repeat-x;'></td>
        <td width='8' height='8' background='<%=ImgUrl %>survey_box_top_r.gif' style='background-repeat: no-repeat;'></td>
    </tr>
    <tr>
        <td width='8' valign='top' background='<%=ImgUrl %>survey_box_left.gif' style='background-repeat: repeat-y;'></td>
        <td background='<%=ImgUrl %>survey_box_bg.gif' style='background-repeat: repeat-x;'>
            <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                <tr>
                    <td colspan='2' align='right' height='18'>
                        <img src='<%=ImgUrl %>more_button.gif' width='36' height='18' style='cursor:pointer' onclick='" + string.Format("javascript:document.location.href=\"" + SiteUrl + "\"") + @"' />
                    </td>
                </tr>
                <tr>
                    <td width='116' valign='top' align='center'>&nbsp;"); 
                        <img src='<%=ImgUrl %>survey_image.gif' />
                    </td>
                    <td valign='top'>
                        <div id="<%=this.ClientID %>_survey">

                        </div>
                    </td>                                    
                </tr>
            </table>
        </td>
        <td width='8' style='background-position: top;' align='right' valign='top' background='<%=ImgUrl %>survey_box_right.gif' style='background-repeat: repeat-y;'></td>
    </tr>
    <tr>
        <td width='8' height='8' background='<%=ImgUrl %>survey_bottom_l.gif' style='background-repeat: no-repeat;'></td>
        <td height='8' background='<%=ImgUrl %>survey_bottom.gif' style='background-repeat: repeat-x;'></td>
        <td width='8' height='8' background='<%=ImgUrl %>survey_bottom_r.gif' style='background-repeat: no-repeat;'></td>
    </tr>
</table>