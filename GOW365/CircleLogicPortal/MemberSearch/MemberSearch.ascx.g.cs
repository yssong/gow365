﻿//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.18033
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CircleLogicPortal.MemberSearch {
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
    
    
    public partial class MemberSearch {
        
        public static implicit operator global::System.Web.UI.TemplateControl(MemberSearch target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControlTree(global::CircleLogicPortal.MemberSearch.MemberSearch @__ctrl) {
            @__ctrl.SetRenderMethodDelegate(new System.Web.UI.RenderMethod(this.@__Render__control1));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__Render__control1(System.Web.UI.HtmlTextWriter @__w, System.Web.UI.Control parameterContainer) {
            @__w.Write("\r\n\r\n<link rel=\"stylesheet\" href=\"");
                     @__w.Write(ImgUrl);

            @__w.Write("membersearch.css\" />\r\n<script type=\"text/javascript\">\r\nfunction ");
 @__w.Write(this.ClientID);

            @__w.Write("memberSearch() {\r\n\r\n    var userName=document.getElementById(\"");
                                  @__w.Write(this.ClientID);

            @__w.Write(@"name"").value;
	var clientContext = new SP.ClientContext.get_current();
	var web = clientContext.get_web();
	var userInfoList = web.get_siteUserInfoList();
	var camlQuery = new SP.CamlQuery();
	camlQuery.set_viewXml(""<View><Query><Where><And><Contains><FieldRef Name='Title'/><Value Type='Text'>"" + userName + 
    ""</Value></Contains><IsNotNull><FieldRef Name='UserName' /></IsNotNull></And></Where></Query></View>"");
	var ");
@__w.Write(this.ClientID );

            @__w.Write("_webusers= userInfoList.getItems(camlQuery);\r\n\tclientContext.load(");
            @__w.Write(this.ClientID );

            @__w.Write("_webusers);\r\n\t\r\n\tclientContext.executeQueryAsync(\r\n\t\tfunction (sender, args) {\r\n " +
                    "           jQuery(\'#");
             @__w.Write(this.ClientID );

            @__w.Write("items ul li\').remove();\r\n\t\t\tvar ");
@__w.Write(this.ClientID );

            @__w.Write("listItemEnumerator = ");
                                       @__w.Write(this.ClientID );

            @__w.Write("_webusers.getEnumerator();\r\n        \twhile (");
        @__w.Write(this.ClientID );

            @__w.Write("listItemEnumerator.moveNext()) {\r\n                var ");
            @__w.Write(this.ClientID );

            @__w.Write("itemStr = \"\";\r\n\t\t\t\tvar listItem = ");
           @__w.Write(this.ClientID );

            @__w.Write(@"listItemEnumerator.get_current();
				var loginID = listItem.get_item('Name');
				var DisplayName = listItem.get_item('Title');
				var loginName= listItem.get_item('UserName');
				var userId= listItem.get_id();
                var MobilePhone = (listItem.get_item('MobilePhone')==null?"""":listItem.get_item('MobilePhone'));
                var WorkPhone = (listItem.get_item('WorkPhone')==null?"""":listItem.get_item('WorkPhone'));
                var JobTitle = (listItem.get_item('JobTitle')==null?"""":listItem.get_item('JobTitle'));
                var Department = (listItem.get_item('Department')==null?"""":listItem.get_item('Department'));
                var Email = (listItem.get_item('EMail')==null?"""":listItem.get_item('EMail'));
                var picture = (listItem.get_item('Picture')==null?""");
                                                           @__w.Write(ImgUrl);

            @__w.Write("nopicture.gif\":listItem.get_item(\'Picture\').get_url());\r\n                \r\n      " +
                    "          ");
        @__w.Write(this.ClientID );

            @__w.Write("itemstr=\"<li class=\'searchresult\'>\"+\"<div class=\'photo\'><img width=40 height=40 s" +
                    "rc=\'\"+picture +\"\'/></div>\";\r\n                ");
        @__w.Write(this.ClientID );

            @__w.Write(@"itemstr+=""<div class='userInfo'><div class='uName'>""+DisplayName+
                ""</div><div class='uDept'>""+Department+"" / ""+JobTitle+
                ""</div><div class='uPhone'>""+MobilePhone+""</div><div class='uPhone'>""+WorkPhone+
                ""</div><div class='uMail'>""+Email+""</div></div></li>"";
                jQuery('#");
                 @__w.Write(this.ClientID );

            @__w.Write("items ul\').append(");
                                                      @__w.Write(this.ClientID );

            @__w.Write(@"itemstr);
			}
			
		},
		function (sender, args) {
			return null;
		});
}
</script>
<div class=""membersearch"">
 <div class=""search_head"">
   <span class=""search_title"">직원 검색</span>
 </div>
 <div class=""search_content"">
  <table border=""0"" cellSpacing=""0"" cellPadding=""0"">
   <tbody>
    <tr>
     <td height=""28"" style=""padding-bottom: 3px;"">
       <label for=""textfield""></label><input name=""textfield"" id=""");
                                                          @__w.Write(this.ClientID);

            @__w.Write("name\" style=\"height: 25px;\" type=\"text\" size=\"15\" onkeydown=\"if(event.keyCode == " +
                    "13) {event.returnValue=false;");
                                                                                                                                                                                          @__w.Write(this.ClientID);

            @__w.Write("memberSearch()}\" /> \r\n     </td>\r\n     <td align=\"left\" class=\"search_btn\" vAlign" +
                    "=\"top\" rowSpan=\"2\">\r\n      <img src=\"");
        @__w.Write(ImgUrl);

            @__w.Write("search_btn_32.gif\"  onclick=\"");
                                                @__w.Write(this.ClientID);

            @__w.Write("memberSearch()\"/>\r\n     </td>\r\n    </tr>\r\n   </tbody>\r\n  </table>\r\n </div>\r\n <div" +
                    " id=\"");
  @__w.Write(this.ClientID );

            @__w.Write("items\">\r\n     <ul class=\"searchItems\">\r\n\r\n     </ul>\r\n </div>\r\n</div>\r\n");
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