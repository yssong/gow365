﻿//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.18033
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CircleLogicPortal.DropDownLinkWebPart {
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
    
    
    public partial class DropDownLinkWebPart {
        
        public static implicit operator global::System.Web.UI.TemplateControl(DropDownLinkWebPart target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControlTree(global::CircleLogicPortal.DropDownLinkWebPart.DropDownLinkWebPart @__ctrl) {
            @__ctrl.SetRenderMethodDelegate(new System.Web.UI.RenderMethod(this.@__Render__control1));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__Render__control1(System.Web.UI.HtmlTextWriter @__w, System.Web.UI.Control parameterContainer) {
            @__w.Write("\r\n\r\n\r\n<script type=\"text/javascript\">\r\n   \r\n    ExecuteOrDelayUntilScriptLoaded(");
                            @__w.Write(this.ClientID);

            @__w.Write("Initialize, \"sp.js\");\r\n       \r\n    //Retrieve  the Tab items\r\nfunction ");
 @__w.Write(this.ClientID);

            @__w.Write(@"Initialize() {
    //Get the current SP context

	var camlQuery = new SP.CamlQuery();
	
    var q = ""<View><Query><Where><And><Neq><FieldRef Name='ContentType' /><Value Type='String'>Folder</Value></Neq><Neq><FieldRef Name='ContentType' /><Value Type='String'>폴더</Value></Neq>""+
""</And></Where><OrderBy><FieldRef Name='Title' Ascending='TRUE' /></OrderBy></Query></View>"";

    camlQuery.set_viewXml(q);

    ");
       if (ListName.Trim()!="")    { 
       if (WebName.Trim() != "")
       { 
            @__w.Write("\r\n    clientContext = new SP.ClientContext(\"");
                                  @__w.Write(WebName.Trim());

            @__w.Write("\");\r\n    ");
       }else { 
            @__w.Write("\r\n    clientContext = new SP.ClientContext.get_current();\r\n    ");
       } 
            @__w.Write("\r\n    web = clientContext.get_web();\r\n    this.");
 @__w.Write(this.ClientID );

            @__w.Write("list = web.get_lists().getByTitle(\'");
                                                       @__w.Write(ListName.Trim());

            @__w.Write("\');\r\n    \r\n    this.");
 @__w.Write(this.ClientID );

            @__w.Write("listItems = ");
                                @__w.Write(this.ClientID );

            @__w.Write("list.getItems(camlQuery);\r\n    \r\n    clientContext.load(");
               @__w.Write(this.ClientID );

            @__w.Write("list,\"DefaultDisplayFormUrl\",\"BaseTemplate\",\"RootFolder\");\r\n    clientContext.loa" +
                    "d(");
               @__w.Write(this.ClientID );

            @__w.Write("listItems);\r\n    \r\n    clientContext.executeQueryAsync(Function.createDelegate(th" +
                    "is, this.");
                                                               @__w.Write(this.ClientID);

            @__w.Write("onListItemsLoadSuccess), Function.createDelegate(this, this.onListItemsLoadFailed" +
                    "));\r\n    ");
       } 
            @__w.Write("\r\n\r\n    \r\n}\r\n\r\nfunction onListItemsLoadFailed(sender, args) {\r\n\tSP.UI.Notify.addN" +
                    "otification(\"List items load failed: \" + args.get_message(), false);\r\n}\r\nfunctio" +
                    "n ");
 @__w.Write(this.ClientID);

            @__w.Write("onListItemsLoadSuccess(sender, args) {\r\n\t");
    if (ListName!="")    { 
            @__w.Write("\r\n     try{\r\n        jQuery(\'#");
         @__w.Write(this.ClientID );

            @__w.Write("_tab1 ul li\').remove();\r\n        var listtype1 = ");
                @__w.Write(this.ClientID );

            @__w.Write("list.get_baseTemplate();\r\n         \r\n        var listEnumerator = this.");
                          @__w.Write(this.ClientID );

            @__w.Write("listItems.getEnumerator();\r\n        var listurl = ");
              @__w.Write(this.ClientID );

            @__w.Write(@"list.get_rootFolder().get_serverRelativeUrl();

        while (listEnumerator.moveNext()) {
            //Retrieve the current list item
            var oListItem = listEnumerator.get_current();
            itemHtml = """";
		   
            if (listtype1 == SP.ListTemplateType.links)
            {
                itemHtml += ""<option id='");
                                 @__w.Write(this.ClientID);

            @__w.Write(@"""+oListItem.get_item(""ID"")+""' value='""+oListItem.get_item(""URL"").get_url()+""'>"";
                itemHtml += oListItem.get_item(""URL"").get_description();
                itemHtml += ""</option>"";
                
            }
            else if (listtype1 == 170)
            {
                itemHtml += ""<option id='");
                                 @__w.Write(this.ClientID);

            @__w.Write("\"+oListItem.get_item(\"ID\")+\"\' value=\'\"+oListItem.get_item(\"LinkLocation\").get_url" +
                    "()+\"\'>\";\r\n                itemHtml += oListItem.get_item(\"Title\");\r\n            " +
                    "    itemHtml += \"</option>\";\r\n            }\r\n            jQuery(\'#");
             @__w.Write(this.ClientID );

            @__w.Write("_select\').append(itemHtml);\r\n        }\r\n        \r\n    }\r\n    catch(err)\r\n    {\r\n " +
                    "   }\r\n    ");
       } 
            @__w.Write("\r\n\r\n}\r\n</script>\r\n<div id=\'");
 @__w.Write(this.ClientID );

            @__w.Write("_tab\'>\r\n\r\n</div>\r\n<select id=\"");
    @__w.Write(this.ClientID );

            @__w.Write("_select\" style=\"width:");
                                             @__w.Write(ControlWidth);

            @__w.Write(@"px; vertical-align:middle; border:#DEDEDE 2px solid;"" onchange=""if(this.options[this.selectedIndex].value != ''){var pop = window.open(this.options[this.selectedIndex].value,'',''); if(pop)window.focus();else{    var timer = window.setTimeout( function(){ if(pop)win.focus(); }, 100 );};this.selectedIndex = 0; return false;}"">
    <option id=""");
        @__w.Write(this.ClientID );

            @__w.Write("\" value=\"\">:::Site:::</option>\r\n</select>\r\n\r\n");
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
