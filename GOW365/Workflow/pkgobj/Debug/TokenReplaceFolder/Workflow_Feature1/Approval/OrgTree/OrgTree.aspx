<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<%@ Page Language="C#" %>
<%@ Register tagprefix="SharePoint" namespace="Microsoft.SharePoint.WebControls" assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<html dir="ltr" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>결재선 선택</title>
<meta http-equiv="X-UA-Compatible" content="IE=10" />

<!-- 결재 관련 모듈 loading-->

<script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/4.0/1/MicrosoftAjax.js"/>
<SharePoint:ScriptLink runat="server" id="ScriptLink1" Name="sp.init.js" Localizable="False"/>

<SharePoint:ScriptLink runat="server" id="ScriptLink6" Name="sp.js" LoadAfterUI="True" Localizable="False"/>

<SharePoint:ScriptLink runat="server" id="ScriptLink5" Name="sp.runtime.js" LoadAfterUI="True" Localizable="False"/>

<SharePoint:ScriptLink runat="server" id="ScriptLink8" Name="sp.core.js"  Localizable="False"/>
	
<script type="text/javascript" src="OrgTree.js"></script>
<link id="OrgTreeCss" href="Orgtree.css" rel="stylesheet" type="text/css"/>
<script type="text/javascript" src="../jquery-1.9.0.min.js"></script>
<link rel="stylesheet" href="http://code.jquery.com/ui/1.10.0/themes/base/jquery-ui.css" />
<script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.10.0/jquery-ui.min.js"></script>
<script src="../dynatree/jquery.dynatree-1.2.2.js" type="text/javascript"></script>
<script src="../jquery-ui/jquery-ui-1.10.0.custom.js" type="text/javascript"></script>
<script src="../jquery-ui/jquery.cookie.js" type="text/javascript"></script>

<link href="../dynatree/skin-vista/ui.dynatree.css" rel="stylesheet" type="text/css"/>

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
<div id="OrgInfo">
<div id="box">
    <!-- Title 라인-->                     
	<div id="top">                  
		<div class="l_bar">
			<img alt="top_left" src="img/top_left.jpg" />
		</div>              
		<div class="r_bar">
			<img alt="top_right" src="img/top_right.jpg" />
		</div>
		<h3 style="font-size: 14px;">
		결재선 지정
		</h3>
		                          
		<div class="btn" id="divPersonalApprovalLineButtons" style="display: inline;">
			<span><!--img style="cursor: pointer;" onclick="javascript:return approvalLineManager_AddUserApprovalLine();" src="img/btn_payment_save.jpg" /--></span>
			<span><!--img style="cursor: pointer;" onclick="javascript:return approvalLineManager_GetUserApprovalLine();" src="img/btn_payment_load.jpg" /--></span>
		</div>          
	</div>
	<!-- Outline -->             
	<div id="outline">
		<div class="out_top" style='background-image: url("img/c_line.jpg");'>
			<p class="l">
				<img alt="line_l" src="img/c_line_l.jpg" />
			</p>
			<p class="r">
				<img alt="line_r" src="img/c_line_r.jpg" />
			</p>
		</div>
                
	<div class="out_mid">          
		<table class="l_table" cellspacing="0" cellpadding="0">	                                    
			<tbody>                                  
				<tr>                                      
					<td class="left" style="height: 100%; padding-right: 3px; vertical-align: top;width:250px">
					<div style="text-align: center;">
						<table border="0" cellspacing="0" cellpadding="0">              
						 <thead>                      
						  <tr>                           
						   <td>
                            <img id="imgRealGroupTree" style="float: left; cursor: pointer; " onclick="" src="img/btn_member_on.jpg" />
                            <img id="imgVirtualGroupTree" style="left: -7px; float: left; display: none; position: relative; cursor: pointer;" onclick="" src="img/btn_group_off.jpg" />
						   </td>
						  </tr>                  
						 </thead>
						 						                         
						 <tbody>
						  <tr>                           
						   <td>                                  
						    <div style="width: 250px; margin-top: 0px; margin-right: 0px; margin-bottom: 5px; margin-left: 0px; border-top-color: #cccccc; border-right-color: #cccccc; border-bottom-color: #cccccc; border-left-color: #cccccc; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; border-top-style: solid; border-right-style: solid; border-bottom-style: solid; border-left-style: solid;">

						     <div id="divRealGroupTree" style="margin-top: 4px;display: block;width: 230px;height:255px;text-align:left">
						     
						     </div>
						                                     
						    </div>
						   </td>
						                                        
						                               
						  </tr>
						                                 
						                          
						 </tbody>
						 
						                     
						</table>
					
					                
					</div>
					
					<div>
				
				                                                    
				                                                
				<table style="margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px;" cellspacing="0" cellpadding="0">
				 
				                                                     
				 <tbody>
				  <tr>
				   
				                                                           
				   <th align="left" style="padding-top: 2px; padding-right: 0px; padding-bottom: 2px; padding-left: 0px;">
				    <input name="searchbox" class="approvaltextbox" id="searchbox" style="width: 150px; vertical-align: middle; ime-mode: auto;" onkeydown="if (event.keyCode == 13) {$('#btnSearch').click(); return false; }" type="text" />&nbsp;<img id="btnSearch" style="vertical-align: middle; border-top-width: 0px; border-right-width: 0px; border-bottom-width: 0px; border-left-width: 0px; cursor: pointer;"  alt="검색" src="img/btn_combination_search.jpg" />
				   </th>
				                                                       
				  </tr>
				  
				                                                  
				 </tbody>
				</table>
				
				                                                
				<table class="t_list" style="margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; border-bottom-color: currentColor; border-bottom-width: medium; border-bottom-style: none;" cellspacing="0" cellpadding="0">
				 
				                                                     
				 <thead>
				  
				                                                          
				  <tr>
				   
				                                                               
				   <th colspan="3">
				    <img alt="bullet" src="img/icon1.jpg" />구성원
				   </th>
				   
				                                                           
				  </tr>
				  
				                                                      
				 </thead>
				   
				                                                 
				</table>
				 
				                                                
				<div style="height: 204px; border-top-color: currentColor; border-right-color: #bbb1a7; border-bottom-color: #bbb1a7; border-left-color: #bbb1a7; border-top-width: 0px; border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; border-top-style: none; border-right-style: solid; border-bottom-style: solid; border-left-style: solid; overflow-y: auto;">
				  
				                                                     
				 <table width="100%" class="t_list" style="border-style: none; border-color: currentColor; border-width: 0px; margin: 0px;" cellspacing="0" cellpadding="0">
				   <tr>
				    <th class="list_title" style="width: 30%;">
				     이름
				    </th>
				    <th class="list_title" style="width: 30%;">
				     직책
				    </th>
				    <th class="list_title" style="width: 40%; border-right-color: currentColor; border-right-width: medium; border-right-style: none;">
				     부서
				    </th>
				   </tr>
				   <tbody>
				   <tr>
				    <td colspan="5">				                                                                         
				     <div id="divDeptUsers">
				      <table id="deptUserList" style="width: 100%; table-layout: fixed;" border="0" cellspacing="0" cellpadding="0" >
                         <colgroup>
                             <col style="width: 30%;" />
                             <col style="width: 30%;" />
                             <col style="width: 40%;" />
                         </colgroup>
                         
						<tbody id="deptUsers">
						  
						 </tbody>
						</table>
				      
				     </div>
				    </td>
				    
				                                                                
				   </tr>
				   
				                                                           
				  </tbody>
				                                                                           
				                                                      
				 </table>
				 
				                                                 
				</div>
				                                                
				</div>
					                                           
				</td>
				
				<td width="25px;">
				<!--결재선 추가 버튼 -->
					<div class="point">
		                <span  align="AbsMiddle" ><img align="AbsMiddle" id="addApprover" style="cursor: pointer;margin-right:15px;" onclick="return AddAprUser('approver');" src="img/btnAddApprover.gif" /></span>
		                <span  align="AbsMiddle"  ><img align="AbsMiddle" id="addReviwer" style="cursor: pointer;margin-top:180px;margin-right:15px;" onclick="return AddAprUser('reviewer');" src="img/btnAddReviewer.gif" /></span>
		                <span  align="AbsMiddle"  ><img align="AbsMiddle" id="adReferrer" style="cursor: pointer;margin-top:180px;margin-right:15px;" onclick="return AddAprUser('referrer');" src="img/btnAddReferrer.gif" /></span>
					</div>
				<!--결재선 추가 버튼 끝-->					
				</td>
				                                          
				<td class="right">
				                                               
				<div>
				
				                                                    
				<!--결재자  목록 -->
				<table class="t_list" style="margin-top: 10px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; border-bottom-color: currentColor; border-bottom-width: medium; border-bottom-style: none;" cellspacing="0" cellpadding="0">
				 <thead>                                                  
				  <tr>                                                         
				   <th colspan="3">
				    <img alt="bullet" src="img/icon1.jpg" />결재자
				   </th>			                                                        
				  </tr>				                                                     
				 </thead>				                                                
				</table>				 
				
				<div style="height: 150px; border-top-color: currentColor; border-right-color: #bbb1a7; border-bottom-color: #bbb1a7; border-left-color: #bbb1a7; border-top-width: 0px; border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; border-top-style: none; border-right-style: solid; border-bottom-style: solid; border-left-style: solid; overflow-y: auto;">
				 <table width="100%" class="t_list" style="border-style: none; border-color: currentColor; border-width: 0px; margin: 0px;" cellspacing="0" cellpadding="0">
				  <tbody>
				  <tr>
				  <th class="list_title" style="width: 13%;">
				     순번
				  </th>
				  <th class="list_title" style="width: 30%;">
				     성명
				  </th>
				  <th class="list_title" style="width: 22%;">
				     직책
				  </th>
				  <th class="list_title" style="width: 35%;">
				     부서
				  </th>
				  </tr>
				  <tr>
				  <td colspan="5">
				  	<table id="tblApproverLists" style="width: 100%; table-layout: fixed;" border="0" cellspacing="0" cellpadding="0"> 
                         <colgroup>
                             <col style="width: 13%;" />
                             <col style="width: 30%;" />
                             <col style="width: 22%;" />
                             <col style="width: 35%;" />
                         </colgroup>
						 <tbody id="ApprovalUserLists" >
						 </tbody>
					  </table>			                                                                         
				    
				  </td>
				  </tr>
				  </tbody>
				  </table>
				 </div>
				<!--결재자  목록  끝-->	
				
				<!--합의자 목록 -->
				<table class="t_list" style="margin-top: 10px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; border-bottom-color: currentColor; border-bottom-width: medium; border-bottom-style: none;" cellspacing="0" cellpadding="0">
				 <thead>                                                  
				  <tr>                                                         
				   <th colspan="3">
				    <img alt="bullet" src="img/icon1.jpg" />합의자
				   </th>	                                                        
				  </tr>				                                                     
				 </thead>				                                                
				</table>				 
				
				<div style="height: 150px; border-top-color: currentColor; border-right-color: #bbb1a7; border-bottom-color: #bbb1a7; border-left-color: #bbb1a7; border-top-width: 0px; border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; border-top-style: none; border-right-style: solid; border-bottom-style: solid; border-left-style: solid; overflow-y: auto;">
				 <table width="100%" class="t_list" style="border-style: none; border-color: currentColor; border-width: 0px; margin: 0px;" cellspacing="0" cellpadding="0">
				  <tbody>
				  <tr>
				  <th class="list_title" style="width: 13%;">
				     순번
				  </th>
				  <th class="list_title" style="width: 30%;">
				     성명
				  </th>
				  <th class="list_title" style="width: 22%;">
				     직책
				  </th>
				  <th class="list_title" style="width: 35%;">
				     부서
				  </th>
				  </tr>
				  <tr>
				  <td colspan="5">
				  	<table id="tblReviewerLists" style="width: 100%; table-layout: fixed;" border="0" cellspacing="0" cellpadding="0">
                         <colgroup>
                             <col style="width: 13%;" />
                             <col style="width: 30%;" />
                             <col style="width: 22%;" />
                             <col style="width: 35%;" />
                         </colgroup>
						 <tbody id="ReviewUserLists" >
						 </tbody>
					  </table>				    
				  </td>
				  </tr>
				  </tbody>
				  </table>
				 </div>
				<!--합의자  목록  끝-->
				
				<!--참조자  목록 -->
				<table class="t_list" style="margin-top: 10px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; border-bottom-color: currentColor; border-bottom-width: medium; border-bottom-style: none;" cellspacing="0" cellpadding="0">
				 <thead>
				  <tr>
				   <th colspan="3">
				    <img alt="bullet" src="img/icon1.jpg" />참조자
				   </th>
				  </tr>                  
				 </thead>
				</table>
				
				<div style="height: 150px; border-top-color: currentColor; border-right-color: #bbb1a7; border-bottom-color: #bbb1a7; border-left-color: #bbb1a7; border-top-width: 0px; border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; border-top-style: none; border-right-style: solid; border-bottom-style: solid; border-left-style: solid; overflow-y: auto;">
				 <table width="100%" class="t_list" style="border-style: none; border-color: currentColor; border-width: 0px; margin: 0px;" cellspacing="0" cellpadding="0">
				  <tbody>
				  <tr>
				  <th class="list_title" style="width: 13%;">
				     순번
				  </th>
				  <th class="list_title" style="width: 30%;">
				     성명
				  </th>
				  <th class="list_title" style="width: 22%;">
				     직책
				  </th>
				  <th class="list_title" style="width: 35%;">
				     부서
				  </th>
				  </tr>
				  <tr>
				  <td colspan="5">
				  	<table id="tblReferrerLists" style="width: 100%; table-layout: fixed;" border="0" cellspacing="0" cellpadding="0"> 
                         <colgroup>
                             <col style="width: 13%;" />
                             <col style="width: 30%;" />
                             <col style="width: 22%;" />
                             <col style="width: 35%;" />
                         </colgroup>
						 <tbody id="ReferUserLists">
						 </tbody>
					  </table>			                                                                         
				    
				  </td>
				  </tr>
				  </tbody>
				  </table>
				 </div>
				<!--참조자  목록  끝-->	
				
				
				
				                                         
				</div>
				
				                                           
				</td>
				
				                                      
				</tr>
			 
			                                     
			</tbody>
		
		                                
		</table>
	
	                           
	</div>
	
	                          
	<div class="out_bot" style='background-image: url("img/b_line.jpg");'>
	
	                               
	<p class="l">
	<img alt="line_l" src="img/b_line_l.jpg" />
	</p>
	
	                               
	<p class="r">
	<img alt="line_r" src="img/b_line_r.jpg" />
	</p>
	
	                           
	</div>
	
	                          
	<p class="bot_btn">
	
	                               <a href="#"><img onclick="javascript:return MoveApprovalLine('UP');" alt="올리기" src="img/btn_upload.jpg" /></a>
	                               <a href="#"><img onclick="javascript:return MoveApprovalLine('DOWN');" alt="내리기" src="img/btn_down2.jpg" /></a>
	                               <span><a href="#"><img id="DeleteApprovalLine" alt="삭제" src="img/btn_del2.jpg" /></a></span>          
	                               <span><a href="#"><img id="ClearApprovalLine" alt="결재선초기화" src="img/btn_reset.jpg" /></a></span>
	                               <span><a href="#"><img id="SetApprovalLine" alt="결재선반영" src="img/btn_apply.jpg" /></a></span>              
	                           
	</p>
	
	                      
	</div>
 
                     
</div>

</div>

</form>

</body>

</html>
