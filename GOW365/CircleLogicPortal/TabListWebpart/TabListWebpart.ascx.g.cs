﻿//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.18033
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CircleLogicPortal.TabListWebpart {
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
    
    
    public partial class TabListWebpart {
        
        public static implicit operator global::System.Web.UI.TemplateControl(TabListWebpart target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControlTree(global::CircleLogicPortal.TabListWebpart.TabListWebpart @__ctrl) {
            @__ctrl.SetRenderMethodDelegate(new System.Web.UI.RenderMethod(this.@__Render__control1));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__Render__control1(System.Web.UI.HtmlTextWriter @__w, System.Web.UI.Control parameterContainer) {
            @__w.Write("\r\n\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
                                     @__w.Write(ImgUrl);

            @__w.Write("TabStyle.css\"/>\r\n<script type=\"text/javascript\">\r\n   \r\n    ExecuteOrDelayUntilScr" +
                    "iptLoaded(");
                            @__w.Write(this.ClientID);

            @__w.Write("Initialize, \"sp.js\");\r\n\r\n    function openDialog(_url) {  \r\n        var options =" +
                    " {  url: _url ,  width: 800, height: 600, };  \r\n        SP.UI.ModalDialog.showMo" +
                    "dalDialog(options);  \r\n    }\r\n    function ");
     @__w.Write(this.ClientID);

            @__w.Write("_changeClass(obj,tabname){\r\n\t\tjQuery(\'#");
   @__w.Write(this.ClientID);

            @__w.Write("_container #nav li > a\').removeClass(\'active\');\r\n\t\tjQuery(obj).addClass(\'active\')" +
                    ";\r\n\t\tjQuery(\'#");
   @__w.Write(this.ClientID);

            @__w.Write("_container .tab\').hide();\r\n\t\tjQuery(\'#\'+tabname).show();\r\n    }  \r\n\r\n\r\n    //Retr" +
                    "ieve  the Tab items\r\nfunction ");
 @__w.Write(this.ClientID);

            @__w.Write(@"Initialize() {
    //Get the current SP context

	var camlQuery = new SP.CamlQuery();
	
    var q = ""<View><Query><Where><Neq><FieldRef Name='ContentType' /><Value Type='Computed'>Folder</Value> </Neq></Where><OrderBy><FieldRef Name='Modified' Ascending='FALSE' /></OrderBy><QueryOptions><RowLimit>");
                                                                                                                                                                                                          @__w.Write(ListItemCount);

            @__w.Write("</RowLimit></QueryOptions></Query></View>\";\r\n    camlQuery.set_viewXml(q);\r\n\r\n   " +
                    " ");
       if (ListName1.Trim()!="")    { 
       if (WebName1.Trim() != "")
       { 
            @__w.Write("\r\n    clientContext1 = new SP.ClientContext(\"");
                                   @__w.Write(WebName1.Trim());

            @__w.Write("\");\r\n    ");
       }else { 
            @__w.Write("\r\n    clientContext1 = new SP.ClientContext.get_current();\r\n    ");
       } 
            @__w.Write("\r\n    web = clientContext1.get_web();\r\n    this.");
 @__w.Write(this.ClientID );

            @__w.Write("list1 = web.get_lists().getByTitle(\'");
                                                        @__w.Write(ListName1.Trim());

            @__w.Write("\');\r\n    this.");
 @__w.Write(this.ClientID );

            @__w.Write("listContentTypes1 = this.");
                                             @__w.Write(this.ClientID );

            @__w.Write("list1.get_contentTypes();\r\n    \r\n    this.");
 @__w.Write(this.ClientID );

            @__w.Write("listItems1 = ");
                                 @__w.Write(this.ClientID );

            @__w.Write("list1.getItems(camlQuery);\r\n    \r\n    clientContext1.load(");
                @__w.Write(this.ClientID );

            @__w.Write("list1,\"DefaultDisplayFormUrl\",\"BaseTemplate\",\"RootFolder\");\r\n    clientContext1.l" +
                    "oad(");
                @__w.Write(this.ClientID );

            @__w.Write("listContentTypes1);\r\n    clientContext1.load(");
                @__w.Write(this.ClientID );

            @__w.Write("listItems1);\r\n    \r\n    clientContext1.executeQueryAsync(Function.createDelegate(" +
                    "this, this.");
                                                                @__w.Write(this.ClientID);

            @__w.Write("onListItemsLoadSuccess1), Function.createDelegate(this, this.onListItemsLoadFaile" +
                    "d));\r\n    ");
       } 
       if (ListName2.Trim()!="")    { 
       if (WebName2.Trim()!="")    { 
            @__w.Write("\r\n    clientContext2 = new SP.ClientContext(\"");
                                   @__w.Write(WebName2.Trim());

            @__w.Write("\");\r\n    ");
       }else { 
            @__w.Write("\r\n    clientContext2 = new SP.ClientContext.get_current();\r\n    ");
       } 
            @__w.Write("\r\n    web = clientContext2.get_web();\r\n    this.");
 @__w.Write(this.ClientID );

            @__w.Write("list2 = web.get_lists().getByTitle(\'");
                                                        @__w.Write(ListName2.Trim());

            @__w.Write("\');\r\n    this.");
 @__w.Write(this.ClientID );

            @__w.Write("listContentTypes2 = this.");
                                             @__w.Write(this.ClientID );

            @__w.Write("list2.get_contentTypes();\r\n    \r\n    this.");
 @__w.Write(this.ClientID );

            @__w.Write("listItems2 = ");
                                 @__w.Write(this.ClientID );

            @__w.Write("list2.getItems(camlQuery);\r\n    \r\n    clientContext2.load(");
                @__w.Write(this.ClientID );

            @__w.Write("list2,\"DefaultDisplayFormUrl\",\"BaseTemplate\",\"RootFolder\");\r\n    clientContext2.l" +
                    "oad(");
                @__w.Write(this.ClientID );

            @__w.Write("listContentTypes2);\r\n    clientContext2.load(");
                @__w.Write(this.ClientID );

            @__w.Write("listItems2);\r\n\r\n    clientContext2.executeQueryAsync(Function.createDelegate(this" +
                    ", this.");
                                                                @__w.Write(this.ClientID);

            @__w.Write("onListItemsLoadSuccess2), Function.createDelegate(this, this.onListItemsLoadFaile" +
                    "d));\r\n    ");
       } 
       if (ListName3.Trim()!="")    { 
       if (WebName3.Trim()!="")    { 
            @__w.Write("\r\n    clientContext3 = new SP.ClientContext(\"");
                                   @__w.Write(WebName3.Trim());

            @__w.Write("\");\r\n    \r\n    ");
       }else { 
            @__w.Write("\r\n    clientContext3 = new SP.ClientContext.get_current();\r\n    \r\n    ");
       } 
            @__w.Write("\r\n    web = clientContext3.get_web();\r\n    this.");
 @__w.Write(this.ClientID );

            @__w.Write("list3 = web.get_lists().getByTitle(\'");
                                                        @__w.Write(ListName3.Trim());

            @__w.Write("\');\r\n    this.");
 @__w.Write(this.ClientID );

            @__w.Write("listContentTypes3 = this.");
                                             @__w.Write(this.ClientID );

            @__w.Write("list3.get_contentTypes();\r\n    \r\n    this.");
 @__w.Write(this.ClientID );

            @__w.Write("listItems3 = ");
                                 @__w.Write(this.ClientID );

            @__w.Write("list3.getItems(camlQuery);\r\n    \r\n    clientContext3.load(");
                @__w.Write(this.ClientID );

            @__w.Write("list3,\"DefaultDisplayFormUrl\",\"BaseTemplate\",\"RootFolder\");\r\n    clientContext3.l" +
                    "oad(");
                @__w.Write(this.ClientID );

            @__w.Write("listContentTypes3);\r\n    clientContext3.load(");
                @__w.Write(this.ClientID );

            @__w.Write("listItems3);\r\n\r\n    clientContext3.executeQueryAsync(Function.createDelegate(this" +
                    ", this.");
                                                                @__w.Write(this.ClientID);

            @__w.Write("onListItemsLoadSuccess3), Function.createDelegate(this, this.onListItemsLoadFaile" +
                    "d));\r\n    ");
       } 
            @__w.Write("\r\n\r\n    \r\n}\r\n\r\n\r\nfunction onListItemsLoadFailed(sender, args) {\r\n\tSP.UI.Notify.ad" +
                    "dNotification(\"List items load failed: \" + args.get_message(), false);\r\n}\r\nfunct" +
                    "ion ");
 @__w.Write(this.ClientID);

            @__w.Write("onListItemsLoadSuccess1(sender, args) {\r\n\t");
    if (ListName1!="")    { 
            @__w.Write("\r\n    try{\r\n        jQuery(\'#");
         @__w.Write(this.ClientID );

            @__w.Write("_tab1 ul li\').remove();\r\n        var listtype1 = ");
                @__w.Write(this.ClientID );

            @__w.Write("list1.get_baseTemplate();\r\n        //var DisplayURL1 = ");
                    @__w.Write(this.ClientID );

            @__w.Write("list1.get_defaultDisplayFormUrl();\r\n\r\n        var contenttype1 = ");
                   @__w.Write(this.ClientID );

            @__w.Write("listContentTypes1.itemAt(0).get_id();\r\n    \r\n        var listEnumerator = this.");
                          @__w.Write(this.ClientID );

            @__w.Write("listItems1.getEnumerator();\r\n        var listurl = ");
              @__w.Write(this.ClientID );

            @__w.Write(@"list1.get_rootFolder().get_serverRelativeUrl();

        while (listEnumerator.moveNext()) {
            //Retrieve the current list item
            var oListItem = listEnumerator.get_current();
            itemHtml = """";
		    //Add the items to the list
            if (listtype1 == SP.BaseType.DocumentLibrary)
            {
                itemHtml += ""<li><span class='tabTitle'><a href=\""#\"" onclick=\""javascript:openDialog('"" + ");
                                                                                                   @__w.Write(this.ClientID );

            @__w.Write(@"list1.get_defaultDisplayFormUrl()
     + ""?ID="" + oListItem.get_item('ID') + ""'); return false;\"">"";
                itemHtml += oListItem.get_item('DisplayName');
                itemHtml += ""</a>"";
                itemHtml += ""</span><span class='tabName'>"" + oListItem.get_item(""Editor"").get_lookupValue() + ""</span><span  class='tabDate'>"" + oListItem.get_item(""Modified"").format(""yyyy-MM-dd"") + ""</span></li>"";

                                
            }
            else if (listtype1 == SP.ListTemplateType.links)
            {
                itemHtml += ""<li><span class='tabTitle'><a href=\""#\"" onclick=\""javascript:openDialog('"" + ");
                                                                                                   @__w.Write(this.ClientID );

            @__w.Write("list1.DefaultDisplayFormUrl + \"?ID=\" +  oListItem.get_item(\'ID\') + \"\'); return fa" +
                    "lse;\\\">\";\r\n                itemHtml += oListItem.get_item(\"URL\");\r\n             " +
                    "   itemHtml += \"</a>\";\r\n                itemHtml += \"</span><span class=\'tabName" +
                    "\'>\" + oListItem.get_item(\"Editor\").get_lookupValue() + \"</span><span  class=\'tab" +
                    "Date\'>\" + oListItem.get_item(\"Modified\").format(\"yyyy-MM-dd\") + \"</span></li>\";\r" +
                    "\n            }\r\n            else if (listtype1 == SP.ListTemplateType.discussion" +
                    "Board)\r\n            {\r\n                //타이틀의 특수문자 제거 ? # $ % * &\r\n             " +
                    "   var title = oListItem.get_item(\"Title\").replace(/\\?/gi, \"\").replace(/\\#/gi, \"" +
                    "\").replace(/\\%/gi, \"\").replace(/\\*/gi, \"\").replace(/\\&/gi, \"\");\r\n               " +
                    " //가운데 연속된 \".\",\" \" 는 한개로 마지막은 잘라냄\r\n                title = title.replace(/\\.{1,}" +
                    "/gi, \".\").replace(/\\.$/gi, \"\").replace(/\\s$/gi, \"\");\r\n                var rootur" +
                    "l = encodeURIComponent(listurl + \"/\" + title).replace(/\\!/gi, \"%21\").replace(/\\(" +
                    "/gi, \"%28\").replace(/\\)/gi, \"%29\").replace(/\\_/gi, \"%5F\").replace(/\\-/gi, \"%2D\")" +
                    ".replace(/\\./gi, \"%2E\");\r\n                itemHtml += \"<li><span class=\'tabTitle" +
                    "\'><a href=\'#\' onclick=\\\"javascript:openDialog(\'\" + listurl + \"/Flat.aspx?rootfol" +
                    "der=\" + rooturl + \"&FolderCTID=\" + contenttype1 + \"\'); return false;\\\">\";\r\n     " +
                    "           itemHtml += oListItem.get_item(\"Title\") + \"(\" + oListItem.get_item(\"I" +
                    "temChildCount\") + \")\";\r\n                itemHtml += \"</a>\";\r\n                ite" +
                    "mHtml += \"</span><span class=\'tabName\'>\" + oListItem.get_item(\"Editor\").get_look" +
                    "upValue() + \"</span><span  class=\'tabDate\'>\" + oListItem.get_item(\"Modified\").fo" +
                    "rmat(\"yyyy-MM-dd\") + \"</span></li>\";\r\n            }\r\n            else\r\n         " +
                    "   {\r\n                itemHtml += \"<li><span class=\'tabTitle\'><a href=\\\"#\\\" oncl" +
                    "ick=\\\"javascript:openDialog(\'\" + ");
                                                                                                   @__w.Write(this.ClientID );

            @__w.Write(@"list1.get_defaultDisplayFormUrl() + ""?ID="" +  oListItem.get_item('ID') + ""'); return false;\"">"";
                itemHtml += oListItem.get_item(""Title"");
                itemHtml += ""</a>"";
                itemHtml += ""</span><span class='tabName'>"" + oListItem.get_item(""Editor"").get_lookupValue() + ""</span><span  class='tabDate'>"" + oListItem.get_item(""Modified"").format(""yyyy-MM-dd"") + ""</span></li>"";
            }
		
		    jQuery('#");
       @__w.Write(this.ClientID );

            @__w.Write("_tab1 ul\').append(itemHtml);\r\n        }\r\n        jQuery(\'#");
         @__w.Write(this.ClientID );

            @__w.Write("_link1\').attr(\"href\",");
                                                 @__w.Write(this.ClientID );

            @__w.Write("list1.get_rootFolder().get_serverRelativeUrl());\r\n    }\r\n    catch(err)\r\n    {\r\n " +
                    "   }\r\n    ");
       } 
            @__w.Write("\r\n\r\n}\r\nfunction ");
 @__w.Write(this.ClientID);

            @__w.Write("onListItemsLoadSuccess2(sender, args) {\r\n    ");
       if (ListName2!="")    { 
            @__w.Write("\r\n    try\r\n    {\r\n        jQuery(\'#");
         @__w.Write(this.ClientID );

            @__w.Write("_tab2 ul li\').remove();\r\n        var listtype1 = ");
                @__w.Write(this.ClientID );

            @__w.Write("list2.get_baseTemplate();\r\n        var contenttype2 = ");
                   @__w.Write(this.ClientID );

            @__w.Write("listContentTypes2.itemAt(0).get_id();\r\n    \r\n        var listEnumerator = this.");
                          @__w.Write(this.ClientID );

            @__w.Write("listItems2.getEnumerator();\r\n        var listurl = ");
              @__w.Write(this.ClientID );

            @__w.Write(@"list2.get_rootFolder().get_serverRelativeUrl();

        while (listEnumerator.moveNext()) {
            //Retrieve the current list item
            var oListItem = listEnumerator.get_current();
            itemHtml = """";
		    //Add the items to the list
            if (listtype1 == SP.BaseType.DocumentLibrary)
            {
                itemHtml += ""<li><span class='tabTitle'><a href=\""#\"" onclick=\""javascript:openDialog('"" + ");
                                                                                                   @__w.Write(this.ClientID );

            @__w.Write(@"list2.get_defaultDisplayFormUrl()
     + ""?ID="" + oListItem.get_item('ID') + ""'); return false;\"">"";
                itemHtml += oListItem.get_item('DisplayName');
                itemHtml += ""</a>"";
                itemHtml += ""</span><span class='tabName'>"" + oListItem.get_item(""Editor"").get_lookupValue() + ""</span><span  class='tabDate'>"" + oListItem.get_item(""Modified"").format(""yyyy-MM-dd"") + ""</span></li>"";

            }
            else if (listtype1 == SP.ListTemplateType.links)
            {
                itemHtml += ""<li><span class='tabTitle'><a href=\""#\"" onclick=\""javascript:openDialog('"" + ");
                                                                                                   @__w.Write(this.ClientID );

            @__w.Write("list2.DefaultDisplayFormUrl + \"?ID=\" +  oListItem.get_item(\'ID\') + \"\'); return fa" +
                    "lse;\\\">\";\r\n                itemHtml += oListItem.get_item(\"URL\");\r\n             " +
                    "   itemHtml += \"</a>\";\r\n                itemHtml += \"</span><span class=\'tabName" +
                    "\'>\" + oListItem.get_item(\"Editor\").get_lookupValue() + \"</span><span  class=\'tab" +
                    "Date\'>\" + oListItem.get_item(\"Modified\").format(\"yyyy-MM-dd\") + \"</span></li>\";\r" +
                    "\n            }\r\n            else if (listtype1 == SP.ListTemplateType.discussion" +
                    "Board)\r\n            {\r\n                //타이틀의 특수문자 제거 ? # $ % * &\r\n             " +
                    "   var title = oListItem.get_item(\"Title\").replace(/\\?/gi, \"\").replace(/\\#/gi, \"" +
                    "\").replace(/\\%/gi, \"\").replace(/\\*/gi, \"\").replace(/\\&/gi, \"\");\r\n               " +
                    " //가운데 연속된 \".\",\" \" 는 한개로 마지막은 잘라냄\r\n                title = title.replace(/\\.{1,}" +
                    "/gi, \".\").replace(/\\.$/gi, \"\").replace(/\\s$/gi, \"\");\r\n                var rootur" +
                    "l = encodeURIComponent(listurl + \"/\" + title).replace(/\\!/gi, \"%21\").replace(/\\(" +
                    "/gi, \"%28\").replace(/\\)/gi, \"%29\").replace(/\\_/gi, \"%5F\").replace(/\\-/gi, \"%2D\")" +
                    ".replace(/\\./gi, \"%2E\");\r\n                itemHtml += \"<li><span class=\'tabTitle" +
                    "\'><a href=\'#\' onclick=\\\"javascript:openDialog(\'\" + listurl + \"/Flat.aspx?rootfol" +
                    "der=\" + rooturl + \"&FolderCTID=\" + contenttype2 + \"\'); return false;\\\">\";\r\n     " +
                    "           itemHtml += oListItem.get_item(\"Title\") + \"(\" + oListItem.get_item(\"I" +
                    "temChildCount\") + \")\";\r\n                itemHtml += \"</a>\";\r\n                ite" +
                    "mHtml += \"</span><span class=\'tabName\'>\" + oListItem.get_item(\"Editor\").get_look" +
                    "upValue() + \"</span><span  class=\'tabDate\'>\" + oListItem.get_item(\"Modified\").fo" +
                    "rmat(\"yyyy-MM-dd\") + \"</span></li>\";\r\n            }\r\n            else\r\n         " +
                    "   {\r\n                itemHtml += \"<li><span class=\'tabTitle\'><a href=\\\"#\\\" oncl" +
                    "ick=\\\"javascript:openDialog(\'\" + ");
                                                                                                   @__w.Write(this.ClientID );

            @__w.Write(@"list2.get_defaultDisplayFormUrl() + ""?ID="" +  oListItem.get_item('ID') + ""'); return false;\"">"";
                itemHtml += oListItem.get_item(""Title"");
                itemHtml += ""</a>"";
                itemHtml += ""</span><span class='tabName'>"" + oListItem.get_item(""Editor"").get_lookupValue() + ""</span><span  class='tabDate'>"" + oListItem.get_item(""Modified"").format(""yyyy-MM-dd"") + ""</span></li>"";
            }
		
		    jQuery('#");
       @__w.Write(this.ClientID );

            @__w.Write("_tab2 ul\').append(itemHtml);\r\n        }\r\n        jQuery(\'#");
         @__w.Write(this.ClientID );

            @__w.Write("_link2\').attr(\"href\",");
                                                 @__w.Write(this.ClientID );

            @__w.Write("list2.get_rootFolder().get_serverRelativeUrl());\r\n     }\r\n    catch(err)\r\n    {\r\n" +
                    "    }\r\n    ");
       } 
            @__w.Write("\r\n\r\n}\r\nfunction ");
 @__w.Write(this.ClientID);

            @__w.Write("onListItemsLoadSuccess3(sender, args) {\r\n\t\r\n    ");
       if (ListName3!="")    { 
            @__w.Write("\r\n    try\r\n    {\r\n        jQuery(\'#");
         @__w.Write(this.ClientID );

            @__w.Write("_tab3 ul li\').remove();\r\n        var listtype1 = ");
                @__w.Write(this.ClientID );

            @__w.Write("list3.get_baseTemplate();\r\n    \r\n        var contenttype3 = ");
                   @__w.Write(this.ClientID );

            @__w.Write("listContentTypes3.itemAt(0).get_id();\r\n    \r\n        var listEnumerator = this.");
                          @__w.Write(this.ClientID );

            @__w.Write("listItems3.getEnumerator();\r\n        var listurl = ");
              @__w.Write(this.ClientID );

            @__w.Write(@"list3.get_rootFolder().get_serverRelativeUrl();

        while (listEnumerator.moveNext()) {
            //Retrieve the current list item
            var oListItem = listEnumerator.get_current();
            itemHtml = """";
		    //Add the items to the list
            if (listtype1 == SP.BaseType.DocumentLibrary)
            {
                itemHtml += ""<li><span class='tabTitle'><a href=\""#\"" onclick=\""javascript:openDialog('"" + ");
                                                                                                   @__w.Write(this.ClientID );

            @__w.Write(@"list3.get_defaultDisplayFormUrl()
     + ""?ID="" + oListItem.get_item('ID') + ""'); return false;\"">"";
                itemHtml += oListItem.get_item('DisplayName');
                itemHtml += ""</a>"";
                itemHtml += ""</span><span class='tabName'>"" + oListItem.get_item(""Editor"").get_lookupValue() + ""</span><span  class='tabDate'>"" + oListItem.get_item(""Modified"").format(""yyyy-MM-dd"") + ""</span></li>"";
                    
            }
            else if (listtype1 == SP.ListTemplateType.links)
            {
                itemHtml += ""<li><span class='tabTitle'><a href=\""#\"" onclick=\""javascript:openDialog('"" + ");
                                                                                                   @__w.Write(this.ClientID );

            @__w.Write("list3.DefaultDisplayFormUrl + \"?ID=\" +  oListItem.get_item(\'ID\') + \"\'); return fa" +
                    "lse;\\\">\";\r\n                itemHtml += oListItem.get_item(\"URL\");\r\n             " +
                    "   itemHtml += \"</a>\";\r\n                itemHtml += \"</span><span class=\'tabName" +
                    "\'>\" + oListItem.get_item(\"Editor\").get_lookupValue() + \"</span><span  class=\'tab" +
                    "Date\'>\" + oListItem.get_item(\"Modified\").format(\"yyyy-MM-dd\") + \"</span></li>\";\r" +
                    "\n            }\r\n            else if (listtype1 == SP.ListTemplateType.discussion" +
                    "Board)\r\n            {\r\n                //타이틀의 특수문자 제거 ? # $ % * &\r\n             " +
                    "   var title = oListItem.get_item(\"Title\").replace(/\\?/gi, \"\").replace(/\\#/gi, \"" +
                    "\").replace(/\\%/gi, \"\").replace(/\\*/gi, \"\").replace(/\\&/gi, \"\");\r\n               " +
                    " //가운데 연속된 \".\",\" \" 는 한개로 마지막은 잘라냄\r\n                title = title.replace(/\\.{1,}" +
                    "/gi, \".\").replace(/\\.$/gi, \"\").replace(/\\s$/gi, \"\");\r\n                var rootur" +
                    "l = encodeURIComponent(listurl + \"/\" + title).replace(/\\!/gi, \"%21\").replace(/\\(" +
                    "/gi, \"%28\").replace(/\\)/gi, \"%29\").replace(/\\_/gi, \"%5F\").replace(/\\-/gi, \"%2D\")" +
                    ".replace(/\\./gi, \"%2E\");\r\n                itemHtml += \"<li><span class=\'tabTitle" +
                    "\'><a href=\'#\' onclick=\\\"javascript:openDialog(\'\" + listurl + \"/Flat.aspx?rootfol" +
                    "der=\" + rooturl + \"&FolderCTID=\" + contenttype3 + \"\'); return false;\\\">\";\r\n     " +
                    "           itemHtml += oListItem.get_item(\"Title\") + \"(\" + oListItem.get_item(\"I" +
                    "temChildCount\") + \")\";\r\n                itemHtml += \"</a>\";\r\n                ite" +
                    "mHtml += \"</span><span class=\'tabName\'>\" + oListItem.get_item(\"Editor\").get_look" +
                    "upValue() + \"</span><span  class=\'tabDate\'>\" + oListItem.get_item(\"Modified\").fo" +
                    "rmat(\"yyyy-MM-dd\") + \"</span></li>\";\r\n            }\r\n            else\r\n         " +
                    "   {\r\n                itemHtml += \"<li><span class=\'tabTitle\'><a href=\\\"#\\\" oncl" +
                    "ick=\\\"javascript:openDialog(\'\" + ");
                                                                                                   @__w.Write(this.ClientID );

            @__w.Write(@"list3.get_defaultDisplayFormUrl() + ""?ID="" +  oListItem.get_item('ID') + ""'); return false;\"">"";
                itemHtml += oListItem.get_item(""Title"");
                itemHtml += ""</a>"";
                itemHtml += ""</span><span class='tabName'>"" + oListItem.get_item(""Editor"").get_lookupValue() + ""</span><span  class='tabDate'>"" + oListItem.get_item(""Modified"").format(""yyyy-MM-dd"") + ""</span></li>"";
            }
		
		    jQuery('#");
       @__w.Write(this.ClientID );

            @__w.Write("_tab3 ul\').append(itemHtml);\r\n        }\r\n        jQuery(\'#");
         @__w.Write(this.ClientID );

            @__w.Write("_link3\').attr(\"href\",");
                                                 @__w.Write(this.ClientID );

            @__w.Write("list3.get_rootFolder().get_serverRelativeUrl());\r\n    }\r\n    catch(err)\r\n    {\r\n " +
                    "   }\r\n\t");
    } 
            @__w.Write("\r\n}\r\n\r\n\r\n</script>\r\n<div class=\"container\" id=\"");
                   @__w.Write(this.ClientID );

            @__w.Write("_container\">\r\n <div id=\"tabhead\">\r\n  <ul id=\"nav\">\r\n");
   if (ListName1!="")    { 
            @__w.Write("\r\n   <li>\r\n    <a class=\"active\" onclick=\'");
                       @__w.Write(this.ClientID );

            @__w.Write("_changeClass(this,\"");
                                                             @__w.Write(this.ClientID );

            @__w.Write("_tab1\")\' href=\"#\">");
                                                                                                  @__w.Write(ListName1);

            @__w.Write("</a>\r\n   </li>\r\n");
   } 
   if (ListName2!="")    { 
            @__w.Write("\r\n   <li>\r\n    <a onclick=\'");
        @__w.Write(this.ClientID );

            @__w.Write("_changeClass(this,\"");
                                              @__w.Write(this.ClientID );

            @__w.Write("_tab2\")\' href=\"#\">");
                                                                                   @__w.Write(ListName2);

            @__w.Write("</a>\r\n   </li>\r\n");
   } 
   if (ListName3!="")    { 
            @__w.Write("\r\n   <li>\r\n    <a onclick=\'");
        @__w.Write(this.ClientID );

            @__w.Write("_changeClass(this,\"");
                                              @__w.Write(this.ClientID );

            @__w.Write("_tab3\")\' href=\"#\">");
                                                                                   @__w.Write(ListName3);

            @__w.Write("</a>\r\n   </li>\r\n");
   } 
            @__w.Write("\r\n  </ul>\r\n </div>\r\n");
   if (ListName1!="")    { 
            @__w.Write("\r\n <div class=\"tab\" id=\"");
              @__w.Write(this.ClientID );

            @__w.Write("_tab1\">\r\n  <ul id=\"");
  @__w.Write(this.ClientID );

            @__w.Write("_ul1\">\r\n  \r\n  </ul>  \r\n      \r\n  <div style=\"width: 100%;\">\r\n   <span style=\"floa" +
                    "t: right;\"><a id=\"");
                              @__w.Write(this.ClientID );

            @__w.Write("_link1\" href=\"\">more</a></span>\r\n  </div>\r\n </div>\r\n");
   } 
   if (ListName2!="")    { 
            @__w.Write("\r\n <div class=\"tab\" id=\"");
              @__w.Write(this.ClientID );

            @__w.Write("_tab2\" style=\"display: none;\">\r\n  <ul id=\"");
  @__w.Write(this.ClientID );

            @__w.Write("_ul2\">\r\n  \r\n  </ul>\r\n      \r\n  <div style=\"width: 100%;\">\r\n   <span style=\"float:" +
                    " right;\"><a id=\"");
                              @__w.Write(this.ClientID );

            @__w.Write("_link2\" href=\"\">more</a></span>\r\n  </div>\r\n  \r\n </div>\r\n");
   } 
   if (ListName3!="")    { 
            @__w.Write("\r\n <div class=\"tab\" id=\"");
              @__w.Write(this.ClientID );

            @__w.Write("_tab3\" style=\"display: none;\">\r\n  <ul id=\"");
  @__w.Write(this.ClientID );

            @__w.Write("_ul3\">\r\n  \r\n  </ul>\r\n  \r\n  <div style=\"width: 100%;\">\r\n   <span style=\"float: rig" +
                    "ht;\"><a id=\"");
                              @__w.Write(this.ClientID );

            @__w.Write("_link3\"  href=\"\">more</a></span>\r\n  </div>\r\n  \r\n </div>\r\n\r\n");
   } 
            @__w.Write("\r\n</div>");
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
