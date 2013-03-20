<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<%@ Page Language="C#" %>
<%@ Register tagprefix="SharePoint" namespace="Microsoft.SharePoint.WebControls" assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<html dir="ltr" xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>결재선 선택</title>
<meta http-equiv="X-UA-Compatible" content="IE=10" />

<!-- 결재 관련 모듈 loading-->
	

<script type="text/javascript" src="./MicrosoftAjax.js" />
<SharePoint:ScriptLink runat="server" id="ScriptLink2" Name="sp.init.js" Localizable="False"/>

<SharePoint:ScriptLink runat="server" id="ScriptLink3" Name="sp.js" LoadAfterUI="True" Localizable="False"/>

<SharePoint:ScriptLink runat="server" id="ScriptLink4" Name="sp.runtime.js" LoadAfterUI="True" Localizable="False"/>

<SharePoint:ScriptLink runat="server" id="ScriptLink7" Name="sp.core.js"  Localizable="False"/>

		
	<script type="text/javascript" src="OrgTree/OrgTree.js"></script>
	<link id="OrgTreeCss" href="OrgTree/Orgtree.css" rel="stylesheet" type="text/css"/>
	<script type="text/javascript" src="jquery-1.9.0.min.js"></script>
	<link rel="stylesheet" href="http://code.jquery.com/ui/1.10.0/themes/base/jquery-ui.css" />
	<script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.10.0/jquery-ui.min.js"></script>
	<script src="dynatree/jquery.dynatree-1.2.2.js" type="text/javascript"></script>
	<script src="jquery-ui/jquery-ui-1.10.0.custom.js" type="text/javascript"></script>
  <script src="jquery-ui/jquery.cookie.js" type="text/javascript"></script>
  <link href="dynatree/skin/ui.dynatree.css" rel="stylesheet" type="text/css"/>

<!-- 결재 관련 모듈 loading 끝-->



<SharePoint:CssRegistration Name="default" runat="server"/>

</head>
<body>
<script type="text/javascript">
$(document).ready(function() {
	// Handler for .ready() called.
	setInit();
});

</script>

<form id="form1" runat="server">
<div></div>
<div id="strDept" style="width:200px;float:left">
</div>
<div id="strPeople" style="float:right">
<table >
</table>
</div>

</form>

</body>

</html>
