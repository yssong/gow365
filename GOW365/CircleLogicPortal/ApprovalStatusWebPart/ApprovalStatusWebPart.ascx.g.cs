﻿//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.18033
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CircleLogicPortal.ApprovalStatusWebPart {
    using System.Web.UI.WebControls.Expressions;
    using System.Web.UI.HtmlControls;
    using System.Collections;
    using System.Text;
    using System.Web.UI;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Microsoft.SharePoint.WebPartPages;
    using System.Web.SessionState;
    using System.Configuration;
    using Microsoft.SharePoint;
    using System.Web;
    using System.Web.DynamicData;
    using System.Web.Caching;
    using System.Web.Profile;
    using System.ComponentModel.DataAnnotations;
    using System.Web.UI.WebControls;
    using System.Web.Security;
    using System;
    using Microsoft.SharePoint.Utilities;
    using System.Text.RegularExpressions;
    using System.Collections.Specialized;
    using System.Web.UI.WebControls.WebParts;
    using Microsoft.SharePoint.WebControls;
    
    
    public partial class ApprovalStatusWebPart {
        
        public static implicit operator global::System.Web.UI.TemplateControl(ApprovalStatusWebPart target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControlTree(global::CircleLogicPortal.ApprovalStatusWebPart.ApprovalStatusWebPart @__ctrl) {
            @__ctrl.SetRenderMethodDelegate(new System.Web.UI.RenderMethod(this.@__Render__control1));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__Render__control1(System.Web.UI.HtmlTextWriter @__w, System.Web.UI.Control parameterContainer) {
            @__w.Write("\r\n\r\n\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
                                     @__w.Write(ImgUrl);

            @__w.Write("ApprovalStatus.css\"/>\r\n<script type=\"text/javascript\">\r\n\r\n    ExecuteOrDelayUntil" +
                    "ScriptLoaded(");
                            @__w.Write(this.ClientID);

            @__w.Write("Initialize, \"sp.js\");\r\n    function ");
     @__w.Write(this.ClientID);

            @__w.Write("Initialize() {\r\n        ");
           if (ListName1.Trim()!="")    { 
           if (WebName1.Trim() != "")
           { 
    @__w.Write(this.ClientID);

            @__w.Write("clientContext = new SP.ClientContext(\"");
                                                            @__w.Write(WebName1.Trim());

            @__w.Write("\");\r\n        ");
           }else { 
    @__w.Write(this.ClientID);

            @__w.Write("clientContext = new SP.ClientContext.get_current();\r\n        ");
           } 
            @__w.Write("\r\n        \r\n        var web = ");
          @__w.Write(this.ClientID);

            @__w.Write("clientContext.get_web();\r\n        var list = web.get_lists().getByTitle(\"");
                                       @__w.Write(ListName1.Trim());

            @__w.Write(@""");
        var camlQuery = new SP.CamlQuery();

        var q1 = '<View><Query><Where><And><And><And><Neq><FieldRef Name=""aprStatus"" /><Value Type=""Text"">임시저장</Value></Neq><Neq><FieldRef Name=""aprStatus"" /><Value Type=""Text"">반려</Value></Neq></And><Neq><FieldRef Name=""aprStatus"" /><Value Type=""Text"">승인완료</Value></Neq></And><Contains><FieldRef Name=\'curApprover\' /><Value Type=\'Lookup\'><UserID/></Value></Contains></And></Where></Query></View>';
        camlQuery.set_viewXml(q1);
        ");
@__w.Write(this.ClientID);

            @__w.Write("listItems1 = list.getItems(camlQuery);\r\n        ");
@__w.Write(this.ClientID);

            @__w.Write("clientContext.load(");
                                     @__w.Write(this.ClientID);

            @__w.Write(@"listItems1);

        //var q2 = '<View><Query><Where><And><And><And><Neq><FieldRef Name=""aprStatus"" /><Value Type=""Text"">임시저장</Value></Neq><Neq><FieldRef Name=""aprStatus"" /><Value Type=""Text"">반려</Value></Neq></And><Neq><FieldRef Name=""aprStatus"" /><Value Type=""Text"">승인완료</Value></Neq></And><Eq><FieldRef Name=""curApprover"" /><Value Type=""Integer"">' + _spPageContextInfo.userId + '</Value></Eq></And></Where></Query></View>';
        var q2 = '<OrderBy><FieldRef Name=""submitted"" Ascending=""FALSE"" /></OrderBy><Where><And><And><And><Or><Or><Or><Or><Eq><FieldRef Name=""approver1"" /><Value Type=""Integer""><UserID Type=""Integer"" /></Value></Eq><Eq><FieldRef Name=""approver2"" /><Value Type=""Integer""><UserID Type=""Integer"" /></Value></Eq></Or><Eq><FieldRef Name=""approver3"" /><Value Type=""Integer""><UserID Type=""Integer"" /></Value></Eq></Or><Eq><FieldRef Name=""approver4"" /><Value Type=""Integer""><UserID Type=""Integer"" /></Value></Eq></Or><Eq><FieldRef Name=""reviewer"" /><Value Type=""Integer""><UserID Type=""Integer"" /></Value></Eq></Or><Neq><FieldRef Name=""aprStatus"" /><Value Type=""Text"">임시저장</Value></Neq></And><Neq><FieldRef Name=""aprStatus"" /><Value Type=""Text"">승인완료</Value></Neq></And><Neq><FieldRef Name=""aprStatus"" /><Value Type=""Text"">반려</Value></Neq></And></Where>';
        camlQuery.set_viewXml(q2);
        ");
@__w.Write(this.ClientID);

            @__w.Write("listItems2 = list.getItems(camlQuery);\r\n        ");
@__w.Write(this.ClientID);

            @__w.Write("clientContext.load(");
                                     @__w.Write(this.ClientID);

            @__w.Write(@"listItems2);

        //var q3 = '<View><Query><Where><And><And><And><Neq><FieldRef Name=""aprStatus"" /><Value Type=""Text"">임시저장</Value></Neq><Neq><FieldRef Name=""aprStatus"" /><Value Type=""Text"">반려</Value></Neq></And><Neq><FieldRef Name=""aprStatus"" /><Value Type=""Text"">승인완료</Value></Neq></And><Eq><FieldRef Name=""curApprover"" /><Value Type=""Integer"">' + _spPageContextInfo.userId + '</Value></Eq></And></Where></Query></View>';
        //var q3 = '<View><Query><Where><And><And><And><Neq><FieldRef Name=""aprStatus"" /><Value Type=""Text"">임시저장</Value></Neq><Neq><FieldRef Name=""aprStatus"" /><Value Type=""Text"">반려</Value></Neq></And><Neq><FieldRef Name=""aprStatus"" /><Value Type=""Text"">승인완료</Value></Neq></And><Contains><FieldRef Name=""curApprover"" /><Value Type=""UserMulti"">UserID</Value></Contains></And></Where></Query></View>';
        //camlQuery.set_viewXml(q3);
        //");
  @__w.Write(this.ClientID);

            @__w.Write("listItems3 = list.getItems(camlQuery);\r\n        //");
  @__w.Write(this.ClientID);

            @__w.Write("clientContext.load(");
                                       @__w.Write(this.ClientID);

            @__w.Write("listItems2);\r\n        \r\n        ");
@__w.Write(this.ClientID);

            @__w.Write("clientContext.executeQueryAsync(Function.createDelegate(this, this.");
                                                                                     @__w.Write(this.ClientID);

            @__w.Write("onListItemsLoadSuccess),\r\n        Function.createDelegate(this, this.");
                                   @__w.Write(this.ClientID);

            @__w.Write("onQueryFailed));\r\n\r\n        ");
          } 
            @__w.Write("\r\n    }\r\n    function ");
     @__w.Write(this.ClientID);

            @__w.Write("onListItemsLoadSuccess(sender, args) {        \r\n        alert(this.");
           @__w.Write(this.ClientID);

            @__w.Write("listItems1.get_count());\r\n        alert(this.");
           @__w.Write(this.ClientID);

            @__w.Write("listItems2.get_count());\r\n        //alert(this.");
             @__w.Write(this.ClientID);

            @__w.Write("listItems3.get_count());\r\n    }\r\n\r\n    function ");
     @__w.Write(this.ClientID);

            @__w.Write("onQueryFailed(sender, args) {\r\n        alert(\'request failed \' + args.get_message" +
                    "() + \'\\n\' + args.get_stackTrace());\r\n    }\r\n</script>\r\n\r\n");
        }
        
        private void InitializeControl() {
            this.@__BuildControlTree(this);
            this.Load += new global::System.EventHandler(this.Page_Load);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected virtual object Eval(string expression) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression);
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected virtual string Eval(string expression, string format) {
            return global::System.Web.UI.DataBinder.Eval(this.Page.GetDataItem(), expression, format);
        }
    }
}
