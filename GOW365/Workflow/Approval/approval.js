//  Title – SP.ListItem.get_item(‘Title‘);
//  ID – SP.ListItem.get_id();
//  Url - SP.ListItem.get_item(‘urlfieldname‘).get_url()
//  Description – SP.ListItem.get_item(‘descriptionfieldname‘).get_description();
//  Current Version – SP.ListItem.get_item(“_UIVersionString“);
//  Lookup field – SP.ListItem.get_item(‘LookupFieldName’).get_lookupValue();
//  Choice Field – SP.ListItem.get_item(‘ChoiceFieldName‘);
//  Created Date – SP.ListItem.get_item(“Created“);
//  Modified Date – SP.ListItem.get_item(“Modified“); -> case sensitive does not work with ‘modified’
//  Created By – SP.ListItem.get_item(“Author“).get_lookupValue());
//  Modified by – SP.ListItem.get_item(“Editor“).get_lookupValue());
//  File  – SP.ListItem.get_file();
//  File Versions -  File.get_versions();
//  Content Type – SP.ListItem.get_contentType();
// Parent List – SP.ListItem.get_parentList();

var curUser = null;

//초기 데이터 세팅
function setInitialValue(formType)
{

	if(formType=="new")
	{
		setNewInitial();
	}
	else if(formType=="view")
	{
		setViewInitial();
	}
	else if(formType=="edit")
	{
		setEditInitial();
	}




}

function setNewInitial()
{
	//인시저장 필드 확인
	//alert($("input[title='aprStatus']").val());
	//저장 버튼 확인
	$("input[value='저장']").hide();
	//임시저장 이벤트 할당 
	$(".btnTmpSave").click(function(){
		//$("input[title='aprStatus']").val("임시저장");
		$("input[title='상태']").val("임시저장");
		$("input[value='저장']").click();
	});
	//결재요청 이벤트 할당
	$(".btnRequestApprove").click(function(){
		$("input[title='상태']").val("검토");
		$("input[value='저장']").click();
	});
	getUser('curUserName');
	//결재선 지정 
	$(".btnAprLine").click( function() {
		window.open("../../approval/orgtree/orgtree.aspx","결재선 지정","width=700,height=700");
	});
	//기본 People Picker 숨기기
    $("div[Title='사용자 선택']").attr("contentEditable",false);
    $("div[Title='사용자 선택']").css("border-width",0);
   	$("a [title='이름 확인']").hide();
	$("a[title='찾아보기']").hide();
	$("#tdReviewer table.ms-usereditor tr:last").hide();
	$("#tdReferrer table.ms-usereditor tr:last").hide();
	
	selectFormType();
	setBodyContent("");
}

function setViewInitial()
{
	//var uid = _spUserId;
	//alert(uid);   -- 오류 
	
	
	//alert(GetUrlKeyValue('ID', true)); //--성공
	
	
	//JSRequest.EnsureSetup(); 
	//alert(JSRequest.QueryString["ID"]); // q = 15   성공
	//alert(JSRequest.FileName); //파일명 성공
	//alert(JSRequest.PathName); // server relative url 성공
	
	//alert(_spPageContextInfo.webServerRelativeUrl) //성공
	//alert(_spPageContextInfo.siteServerRelativeUrl); //성공
	//alert(_spPageContextInfo.webLanguage); //성공
	//alert(_spPageContextInfo.currentLanguage); //성공
	//alert(_spPageContextInfo.webUIVersion); //성공
	//alert(_spPageContextInfo.userId ); //성공
	//alert(_spPageContextInfo.alertsEnabled); //성공
	//alert(_spPageContextInfo.allowSilverlightPrompt); //성공
	//alert(_spPageContextInfo.pageItemId);  //undefined
	//alert(_spPageContextInfo.pageListId); //성공
	
	getUser("");
		
	//편집, 결재요청 숨김
	$('#_areaRequestApproval').hide();
	//편집 이벤트 할당
	$('#_btnEdit').click(function(){
		document.location.href="eApprovalEdit.aspx?id="+GetUrlKeyValue('ID', true)+"&source="+GetUrlKeyValue('Source', true);
	});
	//결재요청 이벤트 할당
	$('#_btnRequestApprove').click(function(){
    	requestForApprovalAtView();
	});
	//결재 버튼 영역
	//결재 관련 버튼 숨김
	$('#_areaApproval').hide();
	//승인 반려 이벤트 할당. 
	//승인
	$('#_btnApprove').click(function(){
    	approveDocumentAtView("Approve");
	});
	//반려
	$('#_btnReject').click(function(){
    	approveDocumentAtView("Reject");
	});
	
	//합의 버튼 영역 
	//결재 관련 버튼 숨김
	$('#_areaReview').hide();
	//승인 반려 이벤트 할당.
	$('#_btnAgreeReview').click(function(){
    	approveDocumentAtView("Agree");
	});
	$('#_btnDisagreeReview').click(function(){
    	approveDocumentAtView("DisAgree");
	});

	$('#_btnRejectReview').click(function(){
    	approveDocumentAtView("Reject");
	});
	//합의 버튼 끝
	$( "#dialog-form" ).dialog({

      autoOpen: false,
      height: 300,
      width: 350,
      modal: true,
      buttons: {
        "결재": function() {
        						
				result = $("#_opinionType").val();
				//$("p.invalidateTips").hide();
				
				if(((result== "Reject")||(result== "DisAgree"))&&($("#_opinion").val()=="" ))
				{
					//$("p.invalidateTips").text( "반려의견은 반드시 입력하여야 합니다." );
					//$("p.invalidateTips").show();
					alert("반려의견이나 불합의 의견은 반드시 입력하여야 합니다." );
				}
				else
				{
					if(confirm("결재를 진행하시겠습니까?"))
					{
						processApproveDocument(result)				
						$( this ).dialog( "close" );	
					}
					else
					{	
						$( this ).dialog( "close" );
						$("#_opinion").val("");
					}
		  	
		  		}
        },

        "취소": function() {
          $( this ).dialog( "close" );
          $("#_opinion").val("");
        }

      },

      close: function() {
        $("#_opinion").val("");
      }

    });

	
	//인쇄 
	$('#_btnPrint').click(function(){
    	//인쇄
    	window.print();
	});
	
	
	

	//결재관련 데이터 비교 후 설정 하고 결재 버튼 보이기 설정. 
	retrieveCurrentListItems();
	
	
	

}


function setEditInitial()
{
	//인시저장 필드 확인
	//alert($("input[title='상태']").val());

	//저장 버튼 확인
	
	if($("input[title='상태']").val()=="임시저장")
	{
		$("input[value='저장']").hide();
		//임시저장 이벤트 할당 
		$(".btnTmpSave").click(function(){
			//$("input[title='aprStatus']").val("임시저장");
			$("input[title='상태']").val("임시저장");
			$("input[value='저장']").click();
		});
		//결재요청 이벤트 할당
		$(".btnRequestApprove").click(function(){
			$("input[title='상태']").val("검토");
			$("input[value='저장']").click();
		});
	}
	else if($("input[title='상태']").val()=="승인완료")
	{
		$(".btnTmpSave").hide();		
		$(".btnRequestApprove").hide();
		$(".btnAprLine").hide();
		$("input[value='저장']").hide();	
	}
	else
	{
		$(".btnTmpSave").hide();		
		$(".btnRequestApprove").hide();
	}
	
	//결재선 지정
	$(".btnAprLine").click( function() {
		window.open("../../approval/orgtree/orgtree.aspx","결재선 지정","width=700,height=700");
	});
	//기본 People Picker 숨기기
    $("div[Title='사용자 선택']").attr("contentEditable",false);
    $("div[Title='사용자 선택']").css("border-width",0);
   	$("a [title='이름 확인']").hide();
	$("a[title='찾아보기']").hide();
	$("#tdReviewer table.ms-usereditor tr:last").hide();
	$("#tdReferrer table.ms-usereditor tr:last").hide();
	
	selectFormType();
	
}

//Retreive current Item 
//ListItem Retreive
function retrieveCurrentListItems() {
    var clientContext = new SP.ClientContext.get_current(); 
    //var oList = clientContext.get_web().get_lists().getByTitle('eApproval');
    var oList = clientContext.get_web().get_lists().getById(_spPageContextInfo.pageListId);
    this._currentUser = clientContext.get_web().get_currentUser(); 
        
    this.oListItem = oList.getItemById(GetUrlKeyValue('ID', true));

    clientContext.load(this._currentUser);
    clientContext.load(oListItem);
    
    clientContext.executeQueryAsync(
        Function.createDelegate(this, this.onRetrieveCurrentListItemsSucceeded), 
        Function.createDelegate(this, this.onRetrieveCurrentListItemsFailed)
    );
}

function onRetrieveCurrentListItemsSucceeded(sender, args) {
    //alert(oListItem.get_item('Title'));
    //alert(oListItem.get_item('Author').get_lookupValue());
    //alert(oListItem.get_item('Created'));
    //alert(oListItem.get_item('aprStatus'));
    //alert(this._currentUser.get_title());
	//alert(oListItem.get_fieldValuesAsText("reviewer"));
	//alert(this._currentUser.get_loginName());
	//alert(this._currentUser.get_title());
	//alert(this._currentUser.get_userId());
	
	var reviewers = this.oListItem.get_fieldValues().reviewer;
	var reviewStatus = null;
	if(oListItem.get_item('reviewStatus')!=null)
	{
		reviewStatus=oListItem.get_item('reviewStatus').split('$$$');
	}
    	
	switch (oListItem.get_item('aprStatus')) {
    	//임시저장
    	
		case "임시저장"    :
			//작성자만 편집, 삭제가능
			//if(this._currentUser.get_title()==oListItem.get_item('Author').get_lookupValue())
			if(this._currentUser.get_id()==oListItem.get_item('Author').get_lookupId())
			{
				$('#_areaRequestApproval').show();
			}
		   	break;
		case "검토"   : 
			//approver1 만 승인,반려가능, 삭제,편집 불가
			//alert(this._currentUser.get_title()+"#####"+oListItem.get_item('approver1').get_lookupValue());
			//alert(this._currentUser.get_id()+"#####"+oListItem.get_item('approver1').get_lookupId());			
			if(this._currentUser.get_id()==oListItem.get_item('approver1').get_lookupId())
			{
				$('#_areaApproval').show();
			}
		   	break;
		case "확인"  : 

			//approver2 만 승인,반려가능, 삭제,편집 불가
			//if(this._currentUser.get_title()==oListItem.get_item('approver2').get_lookupValue())
			if(this._currentUser.get_id()==oListItem.get_item('approver2').get_lookupId())
			{
				$('#_areaApproval').show();
			}
		   	break;
		case "심사"  : 
			//approver3 만 승인,반려가능, 삭제,편집 불가
			//if(this._currentUser.get_title()==oListItem.get_item('approver3').get_lookupValue())
			if(this._currentUser.get_id()==oListItem.get_item('approver3').get_lookupId())
			{
				$('#_areaApproval').show();
			}
		   	break;
		case "합의"  : 
			//reviewer 만 승인,반려가능, 삭제,편집 불가
			var isReviewer = false;
			var isApproved = false;
			
			for(r in reviewers)
			{
				//alert(reviewers[r].get_lookupId());
				//if(reviewers[r].get_lookupValue()==this._currentUser.get_title())
				if(reviewers[r].get_lookupId()==this._currentUser.get_id())
				{
					isReviewer = true;
					break;
				}
			}
			
			//합의자가 맞고 합의를 진행했는지 판단함. 
			if(isReviewer == true) 
			{
				//reviewStatus 값에 Review 정보 저장  reriewerID###reriewerName###revierResult###revierResultDate###revierOpinion$$$
				if(oListItem.get_item('reviewStatus')!=null)
				{
					
					for(i in reviewStatus)
					{
						//alert(reviewStatus[i]);
						var reviewResults = reviewStatus[i].split('###')
						//0 : reriewerID  1: reriewerName   2: revierResult 3: revierResultDate 4: revierOpinion 5: revierLoginName

						//if(reviewResults[1]==this._currentUser.get_title())
						if(reviewResults[0]==this._currentUser.get_id())
						{
							isApproved =true;
							break;
						}
					}
				}
			}
			
			
			if(isReviewer&&!isApproved)
			{
				$('#_areaReview').show();
			}
		   	break;		   		   		   
		case "승인"  : 
			//approver2 만 승인,반려가능, 삭제,편집 불가
			//if(this._currentUser.get_title()==oListItem.get_item('approver4').get_lookupValue())
			if(this._currentUser.get_id()==oListItem.get_item('approver4').get_lookupId())
			{
				$('#_areaApproval').show();
			}
		   	break;
		case "승인완료"  :
			//approver1 만 승인,반려가능, 삭제,편집 불가
		   break;		   		   		   
		default    : 
			//approver1 만 승인,반려가능, 삭제,편집 불가
	}
	//결재라인 결재결과 그리디
	if(oListItem.get_item('approver1')!=null)
	{
		if(oListItem.get_item('aprStatus1')!=null){
			set_AprLine(oListItem.get_item('approver1').get_lookupId(), 'approver1',oListItem.get_item('aprStatus1'));
		}
		//차례가 안된거라서 blank.gif 그대로 놔둠
	}
	else 
	{
		//결재자가 없으니 blankline.gif을 그리다.
		$(".approver1").attr('src','../../Approval/BlankLine.gif');

	}
	//Approver2
	if(oListItem.get_item('approver2')!=null)
	{
		if(oListItem.get_item('aprStatus2')!=null){
			set_AprLine(oListItem.get_item('approver2').get_lookupId(), 'approver2',oListItem.get_item('aprStatus2'));
		}
		//차례가 안된거라서 blank.gif 그대로 놔둠
	}
	else 
	{
		//결재자가 없으니 blankline.gif을 그리다.
		$(".approver2").attr('src','../../Approval/BlankLine.gif');

	}
	//Approver3
	if(oListItem.get_item('approver3')!=null)
	{
		if(oListItem.get_item('aprStatus3')!=null){
			set_AprLine(oListItem.get_item('approver3').get_lookupId(), 'approver3',oListItem.get_item('aprStatus3'));
		}
		//차례가 안된거라서 blank.gif 그대로 놔둠
	}
	else 
	{
		//결재자가 없으니 blankline.gif을 그리다.
		$(".approver3").attr('src','../../Approval/BlankLine.gif');

	}
	//Approver4
	if(oListItem.get_item('approver4')!=null)
	{
		if(oListItem.get_item('aprStatus4')!=null){
			set_AprLine(oListItem.get_item('approver4').get_lookupId(), 'approver4',oListItem.get_item('aprStatus4'));
		}
		//차례가 안된거라서 blank.gif 그대로 놔둠
	}
	else 
	{
		//결재자가 없으니 blankline.gif을 그리다.
		$(".approver4").attr('src','../../Approval/BlankLine.gif');

	}

	
	//합의결재선 그리기
	var reviewLine ="";
	var reviewOpinion ="";
	if(reviewers==null||reviewers.length==0)
	{
		$("#rowreviewer").hide();
	}
	for(i in reviewers)
	{
		reviewLine = reviewLine + "<p>" + reviewers[i].get_lookupValue();
		reviewOpinion = reviewOpinion +"<tr><td style='width:92px;'>"+reviewers[i].get_lookupValue()+"</td>"
		//reviewOpinion = reviewOpinion +"<div  class='opinionBody' style='text-align:center;float:left'>"+reviewers[i].get_lookupValue()+"</div>"
		for(j in reviewStatus)
		{
			var reviewResult = reviewStatus[j].split("###");
			if(reviewResult[1]==reviewers[i].get_lookupValue())
			{
				//reriewerID###reriewerName###revierResult###revierResultDate###revierOpinion
				//reviewLine = reviewLine +"    "+reviewResult[2]+"    "+ utcToLocal(reviewResult[3])+"    "+ reviewResult[4];
				reviewLine = reviewLine +"    "+reviewResult[2]+"    "+ utcToLocal(reviewResult[3]);
				reviewOpinion = reviewOpinion +"<td  style='width:50px;' >"+reviewResult[2]+"</td>"
				+"<td style='width:345px;'>"+reviewResult[4]+"</td>"
				+"<td  >"+utcToLocal(reviewResult[3])+"</td>"; 
				break;
			}
		}
		reviewLine += "</p>";
		reviewOpinion +="</td>"
	}
	$("#reviewer").html(reviewLine);
	$("#opinionReviewer").html(reviewOpinion);
	
    
}

function onRetrieveCurrentListItemsFailed(sender, args) {
    alert('Request failed. ' + args.get_message() + 
        '\n' + args.get_stackTrace());
}


//결재 요청 
function requestForApprovalAtView()
{
	var clientContext = new SP.ClientContext.get_current(); 
    //var oList = clientContext.get_web().get_lists().getByTitle('eApproval');
    var oList = clientContext.get_web().get_lists().getById(_spPageContextInfo.pageListId);
    this._currentUser = clientContext.get_web().get_currentUser(); 
      
    this.oListItem = oList.getItemById(GetUrlKeyValue('ID', true));
	
	oListItem.set_item('aprStatus', '검토');
	//oListItem.set_item('submitted', new Date());
    oListItem.update();
    
    clientContext.executeQueryAsync(
        Function.createDelegate(this, this.onRequestForApprovalSucceeded), 
        Function.createDelegate(this, this.onRequestForApprovalFailed)
    );
}

function onRequestForApprovalSucceeded() {
    alert('결재요청을 완료하였습니다.');
    document.location.href=unescape(GetUrlKeyValue('Source', true));
}

function onRequestForApprovalFailed(sender, args) {
    alert('결재요청을 실패하였습니다. 관리자에게 문의 바랍니다. '+'\n' + args.get_message() + 
        '\n' + args.get_stackTrace());
}




//결재 승인,반려 
function approveDocumentAtView(result)
{
	switch (result) {
   	//승인    	
		case "Approve"    :
			$( "p.validateTips" ).text( "승인의견을 입력하세요" );
		   	break;
		case "Reject"    :
			$( "p.validateTips" ).text( "반려의견을 입력하세요" );
		   	break;
		case "Agree"    :
			$( "p.validateTips" ).text( "합의의견을 입력하세요" );
		   	break;
		case "DisAgree"    :
			$( "p.validateTips" ).text( "불합의의견을 입력하세요" );
		   	break;
		default : 
			break;
	}
	
	$("#_opinionType").val(result);
	$( "#dialog-form" ).dialog( "open" );
	//$( "#dialog-form" ).show( );
	//processApproveDocument(result);
}

function processApproveDocument(result)
{
	var clientContext = new SP.ClientContext.get_current();
    //var oList = clientContext.get_web().get_lists().getByTitle('eApproval');
    var oList = clientContext.get_web().get_lists().getById(_spPageContextInfo.pageListId);
    //결재 완료 여부 확인 , 검토,확인,심사,승인은 한명만 결재 , 합의는 병렬로 여러명 결재 가능 
   	var isAprCompleted = true;	
    //결재 결과 업데이트용
    var strResult = "";	
    this._currentUser = clientContext.get_web().get_currentUser();
    
    //현재 문서 상태 확인 
    this.oListItem = oList.getItemById(GetUrlKeyValue('ID', true));
    
    switch (result) {
   	//승인    	
		case "Approve"    :
			strResult = "승인";
		   	break;
		case "Reject"    :
			strResult = "반려";
		   	break;
		case "Agree"    :
			strResult = "합의";
		   	break;
		case "DisAgree"  :
			strResult = "불합의";
		   	break;
		default : 
			break;
	}	
	
	switch (oListItem.get_item('aprStatus')) {
    	//임시저장
   	
		case "임시저장"    :
			//oListItem.set_item('submitted', new Date());
		   	break;
		case "검토"   : 
			//aprStatus1, aprOpinion1, aprDate1
			oListItem.set_item('aprStatus1', strResult);
			oListItem.set_item('aprOpinion1', $('#_opinion').val());
			oListItem.set_item('aprDate1', new Date());
		   	break;
		case "확인"  : 
			oListItem.set_item('aprStatus2', strResult);
			oListItem.set_item('aprOpinion2', $('#_opinion').val());
			oListItem.set_item('aprDate2', new Date());
			
		   	break;
		case "심사"  : 
			oListItem.set_item('aprStatus3', strResult);
			oListItem.set_item('aprOpinion3', $('#_opinion').val());
			oListItem.set_item('aprDate3', new Date());
			
		   	break;
		case "합의"  : 
			//합의 결과 반영 
			//reriewerID###reriewerName###revierResult###revierResultDate###revierOpinion
			var reviewers = this.oListItem.get_fieldValues().reviewer;
			var curApprover= this.oListItem.get_fieldValues().curApprover;
			var curApproverArray = [];
			var reviewersCount = 0;
			
			var reviewResult = this._currentUser.get_id()+"###"+this._currentUser.get_title()+"###"+strResult +"###"+new Date()+"###"+$('#_opinion').val()+"###"+this._currentUser.get_loginName();
			var reviewResultCount=0; 
			
			//이미 합의한 사람이 합의 하는지 확인해서 Block 
			if(oListItem.get_item('reviewStatus')!=null)
			{

				var preResult = oListItem.get_item('reviewStatus').split("$$$");
				reviewResultCount=preResult.length;
				 
				for(i in preResult)
				{
					if(preResult[i].split("###")[0]==this._currentUser.get_id())
					{
						//이미 결재한 사용자 이므로 return 
						alert("이미 결재한 사용자 입니다.");
						return;
					}
				}
			}
			else
			{
				reviewResultCount =0;
			}
					
			if(oListItem.get_item('reviewStatus')==null|| oListItem.get_item('reviewStatus') =="")
			{
				reviewResult =reviewResult ;
			}
			else
			{
				reviewResult = oListItem.get_item('reviewStatus')+"$$$"+reviewResult ;
			}
			//reviewStatus 값 업데이트 
			oListItem.set_item('reviewStatus',reviewResult );
			
			var j=0;
			for(var i =0;i<curApprover.length;i++)
			{
				if(curApprover[i].get_lookupId()!=this._currentUser.get_id())
				{
					curApproverArray[j] = curApprover[i];
					j++;
				}
				
			}
			
			oListItem.set_item('curApprover',curApproverArray );	
			
			//합의자중 한사람이라도 반려하면 반려 
			if(reviewers.length ==reviewResult.split("$$$").length||result =="Reject")
			{
				isAprCompleted =true;
			}
			else
			{
				//합의, 불합의일 경우 다음 결재자가 더있는지 확인한 후 결과반영 
				isAprCompleted =false;
			}
						
		   	break;		   		   		   
		case "승인"  : 
			oListItem.set_item('aprStatus4', strResult);
			oListItem.set_item('aprOpinion4', $('#_opinion').val());
			oListItem.set_item('aprDate4', new Date());
			
		   	break;
		case "승인완료"  :
			//approver1 만 승인,반려가능, 삭제,편집 불가
		   break;		   		   		   
		default    : 
			//approver1 만 승인,반려가능, 삭제,편집 불가
	}
	
	if(isAprCompleted)
	{
		oListItem.set_item('aprStatus', oListItem.get_item('aprStatus')+"완료");
		oListItem.set_item('aprResult', strResult);

    }
    oListItem.update();
    
    clientContext.executeQueryAsync(
        Function.createDelegate(this, this.onProcessApproveDocumentSucceeded), 
        Function.createDelegate(this, this.onProcessApproveDocumentFailed)
    );

}


function onProcessApproveDocumentSucceeded() {
    alert('결재요청을 완료하였습니다.');
    document.location.href=unescape(GetUrlKeyValue('Source', true));
}


function onProcessApproveDocumentFailed(sender, args) {
    alert('결재요청을 실패하였습니다. 관리자에게 문의 바랍니다. '+'\n' + args.get_message() + 
        '\n' + args.get_stackTrace());
}

function selectFormType()
{
	var clientContext = new SP.ClientContext.get_current();
    var oList = clientContext.get_web().get_lists().getByTitle('eApprovalTemplate');
	
    var camlQuery = new SP.CamlQuery();
    var camlstr = "<View><OrderBy><FieldRef Name='Title' /></OrderBy></View>";

    camlQuery.set_viewXml(camlstr);
    
    collListItem = oList.getItems(camlQuery);

    clientContext.load(this.collListItem);
    
    clientContext.executeQueryAsync(
		function (sender, args) {
			//var listItemInfo = '';
		    var listItemEnumerator = collListItem.getEnumerator();

		    while (listItemEnumerator.moveNext())
		    {
		        var oListItem = listItemEnumerator.get_current();
		        $("#selFormType").append("<option value='"+oListItem.get_id()+"' >"+oListItem.get_item("Title")+"</opneion>");
			};

			$("#selFormType").change(function(){
				var pplPicker = new PeoplePicker();
	        	pplPicker.SetParentTagId('tdApprover0');
	        	pplPicker.setDefaultValue("");
	        	pplPicker.SetParentTagId('tdApprover1');
	        	pplPicker.setDefaultValue("");
	        	pplPicker.SetParentTagId('tdApprover2');
				pplPicker.setDefaultValue("");
	        	pplPicker.SetParentTagId('tdApprover3');
	        	pplPicker.setDefaultValue("");
		       	pplPicker.SetParentTagId('tdApprover4');
				pplPicker.setDefaultValue("");
		      	pplPicker.SetParentTagId('tdReviewer');
				pplPicker.setDefaultValue("");
		      	pplPicker.SetParentTagId('tdReferrer');
				pplPicker.setDefaultValue("");

			    if($("#selFormType option:selected").val()!="품의서")
		        {
		        	setApprovalTemplate($("#selFormType option:selected").val());
		        }
		        else
		        {
		        	setBodyContent("")
		        }
		        

		        $("input[title='양식명']").val($("#selFormType option:selected").text());
		        $("span.FormTypeTitle").text($("#selFormType option:selected").text());
		    });
		},
		function (sender, args) {
			return null;
		}
	);

}

function setApprovalTemplate(templateId)
{
	var clientContext = new SP.ClientContext.get_current();
    var oList = clientContext.get_web().get_lists().getByTitle('eApprovalTemplate');
	
    var camlQuery = new SP.CamlQuery();
    var camlstr = "<View><Query><Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>"+templateId+"</Value></Eq></Where></Query></View>";

    camlQuery.set_viewXml(camlstr);
    
    collListItem = oList.getItems(camlQuery);

    clientContext.load(collListItem);
    
    clientContext.executeQueryAsync(
		function (sender, args) {
		    var listItemEnumerator = collListItem.getEnumerator();
			
		    while (listItemEnumerator.moveNext()) {
		        var oListItem = listItemEnumerator.get_current();
		        setBodyContent(oListItem.get_item("templare"));
		        
		        if(oListItem.get_item("securityLevel")!=null)
		        {
		        	$("select[title='보안구분']").val(oListItem.get_item("securityLevel")).attr("selected", "selected");

		        }
		        
		        if(oListItem.get_item("retention")!=null)
		        {
		        	$("select[title='보존연한']").val(oListItem.get_item("retention")).attr("selected", "selected");
		        }
		        
		        if(oListItem.get_item("approver1")!=null)
		        {
		        	//getLoginNameByUserID(oListItem.get_item("approver1").get_lookupId(),"approver1");
		        	var pplPicker = new PeoplePicker();
		        	pplPicker.SetParentTagId('tdApprover0');
		        	pplPicker.setDefaultValue(oListItem.get_item("approver1").get_lookupValue());

		        }
		        if(oListItem.get_item("approver2")!=null)
		        {
		        	//getLoginNameByUserID(oListItem.get_item("approver2").get_lookupId(),"approver2");
		        	var pplPicker = new PeoplePicker();
		        	pplPicker.SetParentTagId('tdApprover1');
		        	pplPicker.setDefaultValue(oListItem.get_item("approver1").get_lookupValue());

		        }
		        if(oListItem.get_item("approver3")!=null)
		        {
		        	//getLoginNameByUserID(oListItem.get_item("approver3").get_lookupId(),"approver3");
		        	var pplPicker = new PeoplePicker();
		        	pplPicker.SetParentTagId('tdApprover2');
		        	pplPicker.setDefaultValue(oListItem.get_item("approver1").get_lookupValue());

		        }
		        if(oListItem.get_item("approver4")!=null)
		        {
		        	//getLoginNameByUserID(oListItem.get_item("approver4").get_lookupId(),"approver4");
		        	var pplPicker = new PeoplePicker();
		        	pplPicker.SetParentTagId('tdApprover3');
		        	pplPicker.setDefaultValue(oListItem.get_item("approver4").get_lookupValue());
		        }
		        if(oListItem.get_item("reviewer")!=null)
		        {
		        	var reviers ="";
		        	for(i=0;i<oListItem.get_item("reviewer").length;i++)
		        	{
		        		//getLoginNameByUserID(oListItem.get_item("reviewer")[i].get_lookupId(),"reviewer")
		        		if(reviers =="")
		        		{
		        			reviers = oListItem.get_item("reviewer")[i].get_lookupValue();
		        		}
		        		else
		        		{
		        			reviers = reviers+";"+oListItem.get_item("reviewer")[i].get_lookupValue();
		        		}
		        		
		        	}
		        	
		        	var pplPicker = new PeoplePicker();
		        	pplPicker.SetParentTagId('tdReviewer');
		        	pplPicker.setDefaultValue(reviers);
		        }
		        if(oListItem.get_item("referrer")!=null)
		        {
		        	var referrer="";	        
		        	for(i=0;i<oListItem.get_item("referrer").length;i++)
		        	{
		        		//getLoginNameByUserID(oListItem.get_item("referrer")[i].get_lookupId(),"referrer")
		        		if(referrer == "")
		        		{
		        			referrer= oListItem.get_item("referrer")[i].get_lookupValue();
		        		}
		        		else
		        		{
		        			referrer= referrer+";"+ oListItem.get_item("referrer")[i].get_lookupValue();
		        		}
		        	}
		        	var pplPicker = new PeoplePicker();
		        	pplPicker.SetParentTagId('tdReferrer');
		        	pplPicker.setDefaultValue(referrer);
		        }
		        $("a [title='이름 확인']").click();
			};
		},
		function (sender, args) {
			return null;
		}
	);
}

function setBodyContent(strHtml)
{
	//$("textarea[Title=’aprBody’]").closest("span").find("iframe[Title='Rich Text Editor']").contents().text("<img src='http://static.naver.net/www/u/2010/0611/nmms_215646753.gif' alt='네이버' width='210' height='78' usemap='#logo_ss' />");
	//$("TD.FormTableMainContentTD Div[role='textbox']").html("<img src='http://static.naver.net/www/u/2010/0611/nmms_215646753.gif' alt='네이버' width='210' height='78' usemap='#logo_ss' />");
	//$("TD.FormTableMainContentTD Div[role='textbox']").html(strHtml);
	$("#approvalContent Div[role='textbox']").html(strHtml);
}


function getUser(fieldName)
{
	context = new SP.ClientContext.get_current(); 
	field = fieldName;
	web = context.get_web(); 
	this._currentUser = web.get_currentUser(); 
	context.load(this._currentUser); 
	
	context.executeQueryAsync(Function.createDelegate(this, this.onGetUserSuccessMethod),Function.createDelegate(this, this.onGetUserFailureMethod)); 
} 

function getLoginNameByUserID(userid,fieldName)
{
	var clientContext = SP.ClientContext.get_current(); 
    var web = clientContext.get_web(); 
    var collListItem = web.get_siteUserInfoList().getItemById(userid);

    //Load the objects async
    clientContext.load(collListItem);
    clientContext.executeQueryAsync( 
    Function.createDelegate(this, function(){
	      //Read all properties from the Item
	     var userName = collListItem.get_item('Name');
	     //var website = collListItem.get_item('WebSite').get_url();
	     alert(fieldName+":"+userName);
	     
     }), Function.createDelegate(this, function(){
         //On Fail
    }));
}

function onGetUserSuccessMethod(sender, args) 
{
	if(this.field!="")
	{
		document.getElementById(this.field).innerHTML=this._currentUser.get_title(); 
	}
	curUser = this._currentUser;
	
} 

function onGetUserFaiureMethod(sender, args) 
{ 
	alert('request failed' + args.get_message() + '\n' + args.get_stackTrace()); 
}


function utcToLocal(utc){
    // Create a local date from the UTC string
    var t = new Date(utc);
    
    // Get the offset in ms
    var offset = t.getTimezoneOffset()*60000;
	
    // Subtract from the UTC time to get local
    //t.setTime(t.getTime() - offset);

    // do whatever
    var d = [t.getFullYear(), t.getMonth(), t.getDate()].join('-');
    d += ' ' + t.toLocaleTimeString();
    return d;
}

function set_AprLine(userId, fieldId,result)
{
	var urlstr="../../SignImage/"+userId+".gif";
	$.ajax({
	
		url: urlstr  ,
		
		statusCode: {
			404: function() {
				if(result=="승인")
				{
					$("."+fieldId).attr('src','../../Approval/approved.gif');
				}
				else if(result=="반려")
				{
					$("."+fieldId).attr('src','../../Approval/rejected.gif');
				}
			},
			200: function() {
				if(result=="승인")
				{
					$("."+fieldId).attr('src',urlstr  );
				}
				else if(result=="반려")
				{
					$("."+fieldId).attr('src','../../Approval/rejected.gif');
				}

				
			}		
		}
		
	}).done(function ( data ){
	
		if( console && console.log ) {
		
			console.log("Sample of data:", data.slice(0, 100));		
			
		}	
	});
}


//http://msdn.microsoft.com/en-us/library/jj163201.aspx 참조

function retrieveWebSite(siteUrl) {
    var clientContext = new SP.ClientContext(siteUrl);
    this.oWebsite = clientContext.get_web();

    clientContext.load(this.oWebsite);

    clientContext.executeQueryAsync(
        Function.createDelegate(this, this.onRetrieveWebSiteQuerySucceeded), 
        Function.createDelegate(this, this.onrRtrieveWebSiteQueryFailed)
    );
}

function onRetrieveWebSiteQuerySucceeded(sender, args) {
    alert('Title: ' + this.oWebsite.get_title() + 
        ' Description: ' + this.oWebsite.get_description());
}
    
function onRetrieveWebSiteQueryFailed(sender, args) {
    alert('Request failed. ' + args.get_message() + 
        '\n' + args.get_stackTrace());
}

//ListItem Retreive
function retrieveListItems(siteUrl) {
    var clientContext = new SP.ClientContext(siteUrl);
    //var oList = clientContext.get_web().get_lists().getByTitle('eApproval');
    var oList = clientContext.get_web().get_lists().getById(_spPageContextInfo.pageListId);
        
    var camlQuery = new SP.CamlQuery();
    camlQuery.set_viewXml(
        '<View><Query><Where><Geq><FieldRef Name=\'ID\'/>' + 
        '<Value Type=\'Number\'>1</Value></Geq></Where></Query>' + 
        '<RowLimit>10</RowLimit></View>'
    );
    this.collListItem = oList.getItems(camlQuery);
        
    clientContext.load(collListItem);
    clientContext.executeQueryAsync(
        Function.createDelegate(this, this.onRetrieveListItemsQuerySucceeded), 
        Function.createDelegate(this, this.onRetrieveListItemsQueryFailed)
    ); 
}

function onRetrieveListItemsQuerySucceeded(sender, args) {
    var listItemInfo = '';
    var listItemEnumerator = collListItem.getEnumerator();
        
    while (listItemEnumerator.moveNext()) {
        var oListItem = listItemEnumerator.get_current();
        listItemInfo += '\nID: ' + oListItem.get_id() + 
            '\nTitle: ' + oListItem.get_item('Title') + 
            '\nBody: ' + oListItem.get_item('Body');
    }

    alert(listItemInfo.toString());
}

function onRetrieveListItemsQueryFailed(sender, args) {
    alert('Request failed. ' + args.get_message() + 
        '\n' + args.get_stackTrace());
}




//CreateListItem 
function createListItem(siteUrl) {
    var clientContext = new SP.ClientContext(siteUrl);
    var oList = clientContext.get_web().get_lists().getByTitle('Announcements');
        
    var itemCreateInfo = new SP.ListItemCreationInformation();
    this.oListItem = oList.addItem(itemCreateInfo);
    oListItem.set_item('Title', 'My New Item!');
    oListItem.set_item('Body', 'Hello World!');
    oListItem.update();

    clientContext.load(oListItem);
    clientContext.executeQueryAsync(
        Function.createDelegate(this, this.onCreateListItemQuerySucceeded), 
        Function.createDelegate(this, this.onCreateListItemQueryFailed)
    );
}

function onCreateListItemQuerySucceeded() {
    alert('Item created: ' + oListItem.get_id());
}

function onCreateListItemQueryFailed(sender, args) {
    alert('Request failed. ' + args.get_message() + 
        '\n' + args.get_stackTrace());
}

//updateListItem
function updateListItem(siteUrl) {
    var clientContext = new SP.ClientContext(siteUrl);
    var oList = clientContext.get_web().get_lists().getByTitle('Announcements');

    this.oListItem = oList.getItemById(3);
    oListItem.set_item('Title', 'My Updated Title');
    oListItem.update();

    clientContext.executeQueryAsync(
        Function.createDelegate(this, this.onUpdateListItemQuerySucceeded), 
        Function.createDelegate(this, this.onUpdateListItemQueryFailed)
    );
}

function onUpdateListItemQuerySucceeded() {
    alert('Item updated!');
}

function onUpdateListItemQueryFailed(sender, args) {
    alert('Request failed. ' + args.get_message() + 
        '\n' + args.get_stackTrace());
}

//deleteListItem 
function deleteListItem(siteUrl) {
    this.itemId = 2;
    var clientContext = new SP.ClientContext(siteUrl);
    var oList = clientContext.get_web().get_lists().getByTitle('Announcements');
    this.oListItem = oList.getItemById(itemId);
    oListItem.deleteObject();

    clientContext.executeQueryAsync(
        Function.createDelegate(this, this.onDeleteListItemQuerySucceeded), 
        Function.createDelegate(this, this.onDeleteListItemQueryFailed)
    );
}

function onDeleteListItemQuerySucceeded() {
    alert('Item deleted: ' + itemId);
}

function onDeleteListItemQueryFailed(sender, args) {
    alert('Request failed. ' + args.get_message() + 
        '\n' + args.get_stackTrace());
}

function deleteListItemDisplayCount(siteUrl) {
    this.clientContext = new SP.ClientContext(siteUrl);
    this.oList = clientContext.get_web().get_lists().getByTitle('Announcements');
    clientContext.load(oList);

    clientContext.executeQueryAsync(
        Function.createDelegate(this, this.deleteItem), 
        Function.createDelegate(this, this.onDeleteListItemDisplayCountQueryFailed)
    );
}

function deleteItem() {
    this.itemId = 58;
    this.startCount = oList.get_itemCount();
    this.oListItem = oList.getItemById(itemId);
    oListItem.deleteObject();

    oList.update();
    clientContext.load(oList);
        
    clientContext.executeQueryAsync(
        Function.createDelegate(this, this.displayCount), 
        Function.createDelegate(this, this.onQueryFailed)
    );
}

function displayCount() {
    var endCount = oList.get_itemCount();
    var listItemInfo = 'Item deleted: ' + itemId + 
        '\nStart Count: ' +  startCount + 
        ' End Count: ' + endCount;
        
    alert(listItemInfo)
}

function onDeleteListItemDisplayCountQueryFailed(sender, args) {
    alert('Request failed. ' + args.get_message() + 
        '\n' + args.get_stackTrace());
}


//People Picker 
function PeoplePicker(){

    this.context = null;
    this.web = null;
    this.currentUser = null;
    this.parentTagId = null;
    
	this.doc = document;    
    
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
