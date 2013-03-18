//최초 데이터 
var approverCount = 4;
var listItemInfo ="";
//사용자 정보를 미리 가져놓고 사용자 로그인 ID 추적하기
var _users ;
var orgUsers ="orgUsers";

function setInit()
{
	//조직도 트리 컨트롤 
	$("#divRealGroupTree").dynatree({
  		onActivate: function(node) {
		    // A DynaTreeNode object is passed to the activation handler
		    // Note: we also get this event, if persistence is on, and the page is reloaded.
		    //alert("You activated " + node.data.key);
		    //클릭시 부서 사용자 조회
		    getOrgUsers(node.data.key);
 		 },
 		 //최초 펼쳐질 레벨
 		 minExpandLevel: 2
 	});
 	//사용자 정보를 미리 가져놓고 사용자 로그인 ID 추적하기
 	getWebUsers(
 		function(error, _webusers) {
			if (error) {
				console.error(error);
				return;
			}
			_users=_webusers;
			//결재문서에 설정되어있는 결재선 정보 조회
			fnGetApprovalLines();
		}
	);
	
	getDeptItem();
    
    //결재선 초기화
    $("#ClearApprovalLine").click(function(){
    		$('tbody#ApprovalUserLists').children("tr").remove();
    		$('tbody#ReviewUserLists').children("tr").remove();
    		$('tbody#ReferUserLists').children("tr").remove();
    	}
    );
    //선택된 결재자 삭제
    $("#DeleteApprovalLine").click(function(){
    		$('tbody#ApprovalUserLists').children("tr [Choice='true']").remove();
    		$('tbody#ReviewUserLists').children("tr [Choice='true']").remove();
    		$('tbody#ReferUserLists').children("tr [Choice='true']").remove();
    	}
    );
	//결재선 반영
	$("#SetApprovalLine").click(function(){
				fnSetApprovalLines();
			    self.close();
			}
	);
	//사용자 검색
	$("#btnSearch").click(function(){
		
		if($("#searchbox").val()=="")
		{
			alert("검색어를 입력하세요.");
			return;
		}
		searchOrgUsers($("#searchbox").val());
	});

}

//조직도 정보 조회  --> 상위부서의 Seq는 반드시 하위부서의 Seq보다 작야야 한다.ex. 고우아이티 Seq: 10 , SS : 110 , SD : 120 이런식. 
function getDeptItem()
{
    var clientContext = new SP.ClientContext.get_current();
    var oList = clientContext.get_web().get_lists().getByTitle('DeptInfo');
	
    var camlQuery = new SP.CamlQuery();
    var camlstr = "<View><Query><OrderBy><FieldRef Name='seq' /></OrderBy></Query></View>";

    camlQuery.set_viewXml(camlstr);
    
    this.collListItem = oList.getItems(camlQuery);

    clientContext.load(collListItem);
    
    clientContext.executeQueryAsync(
	    Function.createDelegate(this, this.onGetDeptItemsSucceeded),
	    Function.createDelegate(this, this.onGetDeptItemsFailed)
   );

}

function onGetDeptItemsSucceeded(sender, args) {
    //부서 정보를 정상적으로 가져 오면 트리를 그림. 
    var listItemEnumerator = collListItem.getEnumerator();
    //JSON 형태로 변환
    while (listItemEnumerator.moveNext()) {
        var oListItem = listItemEnumerator.get_current();
        
        if(this.listItemInfo=="")
        {
        	this.listItemInfo = '{"Title": ' +'"'+ oListItem.get_item('Title')+'"'
            +',"ID":'+'"'+ oListItem.get_id()+'"'
            +',"parentDept":'+'"'+ (oListItem.get_item('parentDept')==null?"":oListItem.get_item('parentDept').get_lookupValue())+'"'
            +',"parentDept_x003a_ID":'+'"'+ (oListItem.get_item('parentDept_x003a_ID')==null?"":oListItem.get_item('parentDept_x003a_ID').get_lookupValue())+'"'
            +'}';
        }
        else
        {
        	this.listItemInfo = this.listItemInfo+','+'{"Title": ' +'"'+ oListItem.get_item('Title')+'"'
            +',"ID":'+'"'+ oListItem.get_id()+'"'
            +',"parentDept":'+'"'+ (oListItem.get_item('parentDept')==null?'""':oListItem.get_item('parentDept').get_lookupValue())+'"'
            +',"parentDept_x003a_ID":'+'"'+ (oListItem.get_item('parentDept_x003a_ID')==null?'""':oListItem.get_item('parentDept_x003a_ID').get_lookupValue())+'"'
            +'}';
        }
    }
    
    this.listItemInfo = jQuery.parseJSON("["+this.listItemInfo.toString()+"]");
    
    // Get the DynaTree object instance:
	var tree = $("#divRealGroupTree").dynatree("getTree");
	
	var rootNode = tree.getRoot();
	
	// Use it's class methods:
	// Get a DynaTreeNode object instance:
	
	//var node = tree.getNodeByKey("key7654");
	
	//var rootNode = $("#tree").dynatree("getRoot");
	
	// and use it
	//rootNode .toggleExpand();
    
    for(i in listItemInfo )
    {
    	
    	if(listItemInfo[i].parentDept=="")
    	{
    		var childNode = rootNode.addChild({
		        title: listItemInfo[i].Title,
		        key:listItemInfo[i].ID, 
		        isFolder: false,
		        icon : 'OrgRoot.gif'
		 	});
		 	//childNode.toggleExpand(true);

        }
        else
        {
        	var parentNode = tree.getNodeByKey(listItemInfo[i].parentDept_x003a_ID);
			var childNode = parentNode.addChild({
		        title: listItemInfo[i].Title,
		        key:listItemInfo[i].ID, 
		        isFolder: false,
		        icon : 'Deptfolder.gif'
		 	});
		 	//childNode.expand(true);

        }

    	 
    }
    
	////Expand All Node
	//$("#divRealGroupTree").dynatree("getRoot").visit(function(node){
	//    node.expand(true);
	//});
}

function onGetDeptItemsFailed(sender, args) {
    alert('Request failed. ' + args.get_message() + 
        '\n' + args.get_stackTrace());
}
//부서 Node가 클릭되면 부서별 사용자 정보를  나타냄. 
function getOrgUsers(keystr)
{
	var clientContext = new SP.ClientContext.get_current(); 
    var oList = clientContext.get_web().get_lists().getByTitle(orgUsers);
    this.collListItem =null;
    
    var camlQuery = new SP.CamlQuery();
    
    var camlstr = '<View><Query><Where><Eq><FieldRef Name=\'dept_x003a_ID\'/>' + 
        '<Value Type=\'Lookup\'>'+keystr+'</Value></Eq></Where></Query></View>';
    
    //var camlstr = "<Where><Eq><FieldRef Name='dept_x003a_ID' /><Value Type='Lookup'>"+keystr+"</Value></Eq></Where>";
            
    camlQuery.set_viewXml(camlstr);
    
    this.collListItem = oList.getItems(camlQuery);
    
    clientContext.load(collListItem);
    
    clientContext.executeQueryAsync(
	    Function.createDelegate(this, this.onGetOrgUsersSucceeded), 
	    Function.createDelegate(this, this.onGetOrgUsersFailed)
   );

}

function onGetOrgUsersSucceeded(sender, args) {
    //var listItemInfo = '';
    var listItemEnumerator = collListItem.getEnumerator();
    var userStr ="";
    
    while (listItemEnumerator.moveNext()) {
        var oListItem = listItemEnumerator.get_current();
        
    	userStr +='<tr class="trDeptuser" style="cursor: pointer;" groupId="'+oListItem.get_item('dept').get_lookupValue()
    			+'" userId="'+oListItem.get_item('user').get_lookupId()+'" username="'+oListItem.get_item('user').get_lookupValue()
    			+'" grade="'+oListItem.get_item('rolename')
    			+'" deptName="'+oListItem.get_item('dept').get_lookupValue()+' " Choice="false">'
				+'<td class="f_left" style="padding-top: 0px; padding-right: 0px; padding-bottom: 0px; padding-left: 0px;">'
				+'<span class="ellipsis">'+oListItem.get_item('user').get_lookupValue()+'</span>'
				+'</td>'
				+'<td class="f_left">'
				+'<span title="'+oListItem.get_item('rolename')+'" class="ellipsis">'+oListItem.get_item('rolename')+'</span>'
				+'</td>'
				+'<td class="f_left" style="border-right-color: currentColor; border-right-width: 0px; border-right-style: none;">	'
				+'<span title="'+oListItem.get_item('dept').get_lookupValue()+'" class="ellipsis">'+oListItem.get_item('dept').get_lookupValue()+'</span>'
				+'</td>'
				+'</tr>';
    }
    $("tbody#deptUsers").html(userStr);

    //부서 사용자 클릭, 더블클릭시 결재자에 추가함. 
    $(".trDeptuser").dblclick(function () {
	    $(this).attr("Choice","true");	
		$(this ).children('td').css('background-color', '#dfdfdf');
		AddAprUser("approver");
    });
	//사용자 선택시 선택만 활성화 함.     
    $(".trDeptuser").click(function () {
		//alert($(this).attr("Choice"));
		if($(this).attr("Choice")=="true")
		{
			$(this).attr("Choice","false");
			$(this).children('td').css('background-color', '#FFFFFF');
		}
		else
		{
			$(this).attr("Choice","true");	
			$(this ).children('td').css('background-color', '#dfdfdf');
		
		}

    });

}

function onGetOrgUsersFailed(sender, args) {
    alert('Request failed. ' + args.get_message() + 
        '\n' + args.get_stackTrace());
}
//사용자 검색 결과  Action은 같으므로 onGetOrgUsersSucceeded로 Delegation.
function searchOrgUsers(keystr)
{
	var clientContext = new SP.ClientContext.get_current(); 
    var oList = clientContext.get_web().get_lists().getByTitle(orgUsers);
    this.collListItem =null;
    
    var camlQuery = new SP.CamlQuery();
    
    var camlstr = "<View><Query><Where><BeginsWith><FieldRef Name='user' /><Value Type='User'>"
    +keystr+"</Value></BeginsWith></Where></Query></View>"
    
    camlQuery.set_viewXml(camlstr);
    
    this.collListItem = oList.getItems(camlQuery);
    
    clientContext.load(collListItem);
    
    clientContext.executeQueryAsync(
	    Function.createDelegate(this, this.onGetOrgUsersSucceeded), 
	    Function.createDelegate(this, this.onGetOrgUsersFailed)
   );

}



//사용자 더블클릭시 사용자 결재선에 추가
function AddAprUser(category)
{
	obj=$('#deptUserList tr[Choice=true]');
	
	if(obj.length==0)
	{
		alert("사용자가 선택되지 않았습니다. 사용자를 선택 후 추가 버튼을 클릭하시기 바랍니다. ");
		return;
	}
	
	//var selectedUser = obj.getAttribute('userId') + '|' + obj.getAttribute('groupId');
	if((category=="approver")&&((eval($('#ApprovalUserLists tr').length)+eval(obj.length)) > approverCount) )
	{
		alert("더이상 결재자를 추가 할 수 없습니다");
		return;
	}
	
	var target = getTarget(category);
	
	var selectedUser = target.html();
	
	for(i=0;i<obj.length;i++)
	{

		selectedUser += '<tr style="cursor: pointer;" date="" signimage="" groupId="'+obj[i].getAttribute('groupId')
		+'" userId="'+obj[i].getAttribute('userId')+'" username="'+obj[i].getAttribute('username')+'" loginName="'+getUserLoginID(obj[i].getAttribute('userId'))
		+'" groupname="'+obj[i].getAttribute('deptName')+'" class=".ApprovalLine">'
		+'<td id="rowNo" class="f_left" style="text-align: center; font-size: 12px;">'
		+'</td>'
		+'<td class="f_left" style="font-size: 12px;">'
		+obj[i].getAttribute('username')
		+'</td>'
		+'<td class="f_left" style="font-size: 12px;" >'
		+'<span title="grade" class="ellipsis">'+obj[i].getAttribute('grade')+'</span>'
		+'</td>'
		+'<td class="f_left" style="font-size: 12px;">'
		+'<span title="dept" class="ellipsis">'+obj[i].getAttribute('deptName')+'</span>'
		+'</td>'
		+'<td class="f_left" style="width: 0%; border-right-color: currentColor; border-right-width: medium; border-right-style: none; display: none;" >&nbsp;</td>'
		+'</tr>';
		
		//obj[i].setAttribute('Choice','false'); 
	}
	
	obj.attr("Choice","false"); 
	obj.children("td").css('background-color', '#FFFFFF');
	
	//for (var i=0; i<obj.cells.length; i++)
	//{ 
	//obj.cells[i].style.backgroundColor = '#ffffff'; 
	//} 
	
	target.html(selectedUser);
	
	setTargetEvent(target);
    
    setRowNo(category);

}

function getTarget(category)
{
	if(category =="approver")
	{
		return $('tbody#ApprovalUserLists');
	}
	else if(category =="reviewer")
	{
		return $('tbody#ReviewUserLists');
	}
	else if(category =="referrer")
	{
		return $('tbody#ReferUserLists');
	}
}

function setTargetEvent(target)
{
	target.children("tr").dblclick(function () {
			$(this).remove();
		}
	);
	target.children("tr").click(function () {
		//alert($(this).attr("Choice"));
		if($(this).attr("Choice")=="true")
		{
			$(this).attr("Choice","false");
			$(this).children('td').css('background-color', '#FFFFFF');
		}
		else
		{
			$(this).attr("Choice","true");	
			$(this ).children('td').css('background-color', '#dfdfdf');
		
		}

    });
}

//사용자 부서정보 
function setUserInfo(curTarget)
{
		curTarget.children("tr").each(function()
		{
			var uInfo = new getUserInfo();
			uInfo.userid =$(this).attr("userid");
			uInfo.trObj = $(this);
			uInfo.UserDeptRole();

				
		}
	);
}

//순서 지정 
function setRowNo(category)
{

	var target = null;
	
	if(category =="approver")
	{
		target=$('tbody#ApprovalUserLists tr');
		
		target.each(function() { 
	    	//alert($(this).index());
	    	var startpos = 4-$('tbody#ApprovalUserLists tr').length;
	    	var ordstr = startpos + $(this).index();
	    	switch(ordstr)
			 {
				 case 0:
				  ordstr ="검토";
				  break;
				 case 1:
				  ordstr ="확인";
				   break;
				 case 2:
				  ordstr ="심사";
				   break;
   				 case 3:
				  ordstr ="승인";
				   break;
				 default:
				  ordstr =eval($(this).index())+1;
			 }
		    $(this).children("td#rowNo").html(ordstr);
		});
		return;
	}
	else if(category =="reviewer")
	{
		target=$('tbody#ReviewUserLists tr');
	}
	else if(category =="referrer")
	{
		target=$('tbody#ReferUserLists tr');
	}

	target.each(function() { 
    	//alert($(this).index());
	    $(this).children("td#rowNo").html(eval($(this).index())+1);
	});

}


function MoveApprovalLine(direction)
{
	//var row = $("tr [Choice='true']").parents("tr:first");
	var approverrow = $("#ApprovalUserLists tr[Choice='true']");
	var reviewerrow = $("#ReviewUserLists tr[Choice='true']");
	var referrerrow = $("#ReferUserLists tr[Choice='true']");
	
	if((approverrow.length>1)||(reviewerrow.length>1)||(referrerrow.length>1)){
		alert("한명의 사용자만 선택하시기 바랍니다.");
		return;
	}
	//alert(approverrow.index());
    if (direction=="UP") {
        approverrow.insertBefore(approverrow.prev());
        reviewerrow.insertBefore(reviewerrow.prev());
		referrerrow.insertBefore(referrerrow.prev());
    } else {
        approverrow.insertAfter(approverrow.next());
        reviewerrow.insertAfter(reviewerrow.next());
		referrerrow.insertAfter(referrerrow.next());
    }
    
    setRowNo("approver");
    setRowNo("reviewer");
    setRowNo("referrer");

}

function fnGetApprovalLines()
{
	var target ;
	var curTarget ;
	var selectedUser = "";
	var _cate;

	_cate="approver";
	curTarget = getTarget(_cate);
	selectedUser = curTarget.html();

	for(var i=0;i < 4;i++)
	{
		target =$("#tdApprover"+i.toString()+" Div[title='사용자 선택'] span.ms-entity-resolved",opener.document);

		for(var j =0;j<target.length;j++)
		{
			// title=> ID , textContent => 이름.
			//alert(getUserID(target[j].title.split("\\")[1])+target[j].textContent);
			//setOpenerApprover(getUserID(target[j].title.split("\\")[1]),"approver");
			
			selectedUser += getTRstring(target[j]);

			
		}
	}
	
	curTarget.html(selectedUser);
	setTargetEvent(curTarget);
	setRowNo(_cate);
	setUserInfo(curTarget);
	

	
	_cate="reviewer";
	curTarget = getTarget(_cate);
	selectedUser = curTarget.html();

	
	target =$("#tdReviewer Div[title='사용자 선택'] span.ms-entity-resolved",opener.document);
	
	for(var i =0;i<target.length;i++)
	{
		//alert(getUserID(target[i].title.split("\\")[1])+target[i].textContent);
		//setOpenerApprover(getUserID(target[i].title.split("\\")[1]),"reviewer");
		selectedUser += getTRstring(target[i]);

	}
	
	curTarget.html(selectedUser);
	setTargetEvent(curTarget);
	setRowNo(_cate);
	setUserInfo(curTarget);

	
	_cate="referrer";
	curTarget = getTarget(_cate);
	selectedUser = curTarget.html();
	
	target =$("#tdReferrer Div[title='사용자 선택'] span.ms-entity-resolved",opener.document);
	
	for(var i =0;i<target.length;i++)
	{
		//alert(getUserID(target[i].title.split("\\")[1])+target[i].textContent);
		//setOpenerApprover(getUserID(target[i].title.split("\\")[1]),"referrer");
		selectedUser += getTRstring(target[i]);

	}
	
	curTarget.html(selectedUser);
	setTargetEvent(curTarget);
	setRowNo(_cate);
	setUserInfo(curTarget);

	
		
	
}

function getTRstring(target)
{
	return '<tr style="cursor: pointer;" date="" signimage="" groupId="'+''
	+'" userId="'+ getUserID(target.title.split("\\")[1]) +'" username="'+target.textContent
	+'" loginName="'+target.title.split("\\")[1]
	+'" groupname="'+''+'" class=".ApprovalLine">'
	+'<td id="rowNo" class="f_left" style="text-align: center; font-size: 12px;">'
	+'</td>'
	+'<td class="f_left" style="font-size: 12px;" >'
	+ target.textContent
	+'</td>'
	+'<td class="f_left" style="font-size: 12px;" >'
	+'<span title="grade" class="ellipsis">'+''+'</span>'
	+'</td>'
	+'<td class="f_left" style="font-size: 12px;">'
	+'<span title="dept"  class="ellipsis">'+''+'</span>'
	+'</td>'
	+'<td class="f_left" style="width: 0%; border-right-color: currentColor; border-right-width: medium; border-right-style: none; display: none;" >&nbsp;</td>'
	+'</tr>'
}


function fnSetApprovalLines()
{
	var approvers = $("#ApprovalUserLists tr");
	var reviewers = $("#ReviewUserLists tr");
	var strReviewers="";
	var referrers = $("#ReferUserLists tr");
	var strReferrers="";
	var startpos = 4-approvers.length;
	//PeoplePicker 핸들러.
	var pplPicker = new PeoplePicker();
	//for(i=0;i< approvers.length;i++)
	for(i=0;i< 4;i++)
	{
		pplPicker.SetParentTagId('tdApprover'+i.toString());
		if(i>=startpos){
			var order = i-startpos;
		    //pplPicker.setDefaultValue(approvers[i].getAttribute('username'));
		    pplPicker.setDefaultValue(approvers[order].getAttribute('loginName'));
	    }
	    else
	    {
	    	pplPicker.setDefaultValue("");
	    }	
	}
	
	for(var i=0;i<reviewers.length;i++)
	{
		if(i==0)
		{
			//strReviewers += reviewers[i].getAttribute('username');
			strReviewers += reviewers[i].getAttribute('loginName');
		}
		else
		{
			//strReviewers = strReviewers+";"+reviewers[i].getAttribute('username');
			strReviewers = strReviewers+";"+reviewers[i].getAttribute('loginName');
		}
	}
	
	for(var i=0;i<referrers.length;i++)
	{
		if(i==0)
		{
			//strReferrers+= referrers[i].getAttribute('username');
			strReferrers+= referrers[i].getAttribute('loginName');
		}
		else
		{
			//strReferrers= strReferrers+";"+referrers[i].getAttribute('username');
			strReferrers= strReferrers+";"+referrers[i].getAttribute('loginName');
		}
	}

	
	//TD의 ID 
    // Set the parent tag id of the people the picker.
    pplPicker.SetParentTagId('tdReviewer');
    pplPicker.setDefaultValue(strReviewers);
    
    pplPicker.SetParentTagId('tdReferrer');
    pplPicker.setDefaultValue(strReferrers);
    $("a [title='이름 확인']",opener.document).click();
}


function showUserData(user){
    $('#display-user-info').html('<tr><td>Name: '+user.get_title()+'</td><td>&nbsp;Email: '+user.get_email()+'</td></tr>'
    );
    $('#user-name').val(user.get_title());
}


var getWebUsers= function(callback) {
	var clientContext = new SP.ClientContext.get_current();
	var web = clientContext.get_web();
	
	// Get the user information list for the current site
	var userInfoList = web.get_siteUserInfoList();
	
	// Define the query to get the user by the display name of the user.
	var camlQuery = new SP.CamlQuery();
	camlQuery.set_viewXml("<Query></Query>");
	
	// Load the items into a variable
	var _webusers= userInfoList.getItems(camlQuery);
	clientContext.load(_webusers);
	
	// Execute the query and set up the profile information
	clientContext.executeQueryAsync(
		function() {
		    callback(null, _webusers);
		}, 
		function(a, b) {
		    callback(new Error(b.get_message()));
		}
	);

};



function getAllUsers(){
	// Use the client context to get the user.
	var clientContext = new SP.ClientContext.get_current();
	var web = clientContext.get_web();
	
	// Get the user information list for the current site
	var userInfoList = web.get_siteUserInfoList();
	
	// Define the query to get the user by the display name of the user.
	var camlQuery = new SP.CamlQuery();
	camlQuery.set_viewXml("<Query></Query>");
	
	// Load the items into a variable
	var _webusers= userInfoList.getItems(camlQuery);
	clientContext.load(_webusers);
	
	// Execute the query and set up the profile information
	clientContext.executeQueryAsync(
		function (sender, args) {
			//var listItemEnumerator = _webusers.getEnumerator();
        	//while (listItemEnumerator.moveNext()) {
			//	var listItem = listItemEnumerator.get_current();
			//	//로그인 ID  전체이름
			//	var loginID = listItem.get_item('Name');
			//	//표시이름
			//	var DisplayName = listItem.get_item('Title');
			//	//ID 만 
			//	var loginName= listItem.get_item('UserName');
			//	//사용자 WebuserID
			//	var userId= listItem.get_id();

			//}
			_users=_webusers;
			fnGetApprovalLines();
			return null;

		},
		function (sender, args) {
			return null;
		});
}
//userID 로 로그인ID 찾기
function getUserLoginID(userid)
{
	var listItemEnumerator = this._users.getEnumerator();
	var loginName="";
	while (listItemEnumerator.moveNext()) {
		var listItem = listItemEnumerator.get_current();
		
		if(userid== listItem.get_id())
		{
			loginName=listItem.get_item('UserName');
			break;
		}
		
	}
	return loginName;

}
//로그인ID 로 UserID 찾기
function getUserID(userloginName)
{
	var listItemEnumerator = this._users.getEnumerator();
	var userid="";
	while (listItemEnumerator.moveNext()) {
		var listItem = listItemEnumerator.get_current();

		if(listItem.get_item('UserName')!=null && userloginName.toLowerCase()== listItem.get_item('UserName').toLowerCase())
		{
			userid=listItem.get_id();
			break;
		}
		
	}
	return userid;

}

function getUserInfo()
{
	this.userid="";	
	this.clientContext = null;
    this.web = null;
	//this.collListItem =null;
	this.userDept = "";
	this.userRole = "";
	this.trObj = null;
	
	this.UserDeptRole = function(){
        this.clientContext = new SP.ClientContext.get_current(); 
		var oList = this.clientContext.get_web().get_lists().getByTitle(orgUsers);
						
	    this.collListItem =null;
	    var camlQuery = new SP.CamlQuery();	    
	    var camlstr = "<View><Query><Where><Eq><FieldRef Name='user' LookupId='True' /><Value Type='Integer'>"
	    +this.userid+"</Value></Eq></Where></Query></View>";
	    
	    camlQuery.set_viewXml(camlstr);
	    
	    this.collListItem = oList.getItems(camlQuery);
	    
	    this.clientContext.load(this.collListItem);
        this.clientContext.executeQueryAsync(Function.createDelegate(this, this.onSuccessMethod), 
                                       Function.createDelegate(this, this.onFailureMethod));
    }    
    
    this.onSuccessMethod = function(){ 
        var listItemEnumerator = this.collListItem.getEnumerator();	        
	    while (listItemEnumerator.moveNext()) {
	        var oListItem = listItemEnumerator.get_current();
	       	//alert($(this).attr("userid"));
	       	this.userDept =oListItem.get_item('dept').get_lookupValue();
	       	this.userRole=oListItem.get_item('rolename');
	       	this.trObj.attr("groupname",this.userDept);
	       	this.trObj.attr("grade",this.userRole);     	
	       	this.trObj.find("td span[title='dept']").text(this.userDept);
	       	this.trObj.find("td span[title='grade']").text(this.userRole);
	    }
	    
    }

    this.onFailureMethod = function(){
        alert('request failed ' + args.get_message() + '\n' + args.get_stackTrace());
    }    

}


//People Picker 
function PeoplePicker(){

    this.context = null;
    this.web = null;
    this.currentUser = null;
    this.parentTagId = null;
    
	this.doc = opener.document;    
    
    this.SetParentTagId = function(id){
        this.parentTagId = id;
    }
    
    this.SetLoggedInUser = function(){
        if(this.parentTagId != null){
            this.getWebUserData();            
        }    
    }
    
    this.getWebUserData = function(){
        this.context = new SP.ClientContext.get_current();
        this.web = this.context.get_web();
        this.currentUser = this.web.get_currentUser();
        this.currentUser.retrieve();
        this.context.load(this.web);
        this.context.executeQueryAsync(Function.createDelegate(this, this.onSuccessMethod), 
                                       Function.createDelegate(this, this.onFailureMethod));
    }    
    
    this.onSuccessMethod = function(){ 
        this.setDefaultValue(this.currentUser.get_title());        
    }

    this.onFailureMethod = function(){
        alert('request failed ' + args.get_message() + '\n' + args.get_stackTrace());
    }    
        
    this.setDefaultValue = function(value){         
        var parentTag = this.doc.getElementById(this.parentTagId);
        if (parentTag != null) {
            var peoplePickerTag = this.getTagFromIdentifierAndTitle(parentTag, 'div', 
                                                    'UserField_upLevelDiv', '사용자 선택');
            if (peoplePickerTag) {
                peoplePickerTag.innerHTML = value;
            }        
        }
    }
    this.getValue = function(){         
        var parentTag = this.doc.getElementById(this.parentTagId);
        if (parentTag != null) {
            var peoplePickerTag = this.getTagFromIdentifierAndTitle(parentTag, 'div', 'UserField_upLevelDiv', '사용자 선택');
            if (peoplePickerTag)
            {
				var innerSpans = peoplePickerTag.getElementsByTagName("SPAN"); 
				for(var j=0; j < innerSpans.length; j++) { 
					if(innerSpans[j].id == 'content') { 
						alert('HIT for  id=' + innerSpans[j].id + ' innerHTML=' + innerSpans[j].innerHTML);
						//innerSpans[j].innerHTML; 
					}
				}

 				//return peoplePickerTag.innerHTML ;
            }
        }
        
    } 
    
    this.getTagFromIdentifierAndTitle = function(parentElement, tagName, identifier, title){     
        var len = identifier.length;
        var tags = parentElement.getElementsByTagName(tagName);
        for (var i = 0; i < tags.length; i++) {
            var tempString = tags[i].id;
            if (tags[i].title == title && (identifier == "" || 
                tempString.indexOf(identifier) == tempString.length - len)) {
                return tags[i];
            }
        }
        return null;
    }
}
