﻿//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.18033
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CircleLogicPortal.ImageWheelWebpart {
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
    
    
    public partial class ImageWheelWebpart {
        
        public static implicit operator global::System.Web.UI.TemplateControl(ImageWheelWebpart target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControlTree(global::CircleLogicPortal.ImageWheelWebpart.ImageWheelWebpart @__ctrl) {
            @__ctrl.SetRenderMethodDelegate(new System.Web.UI.RenderMethod(this.@__Render__control1));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__Render__control1(System.Web.UI.HtmlTextWriter @__w, System.Web.UI.Control parameterContainer) {
            @__w.Write("\r\n\r\n\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
                                     @__w.Write(ImgUrl);

            @__w.Write("WaterWheel.css\"/>\r\n<script type=\"text/javascript\" src=\"");
                            @__w.Write(ImgUrl);

            @__w.Write("jquery.waterwheelCarousel.js\" ></script>\r\n<script type=\"text/javascript\">\r\n   \r\n " +
                    "   ExecuteOrDelayUntilScriptLoaded(");
                            @__w.Write(this.ClientID);

            @__w.Write("Initialize, \"sp.js\");\r\n       \r\n    //Retrieve  the Tab items\r\nfunction ");
 @__w.Write(this.ClientID);

            @__w.Write(@"Initialize() {
    //Get the current SP context

	var camlQuery = new SP.CamlQuery();
	
	var q = ""<View  Scope='RecursiveAll'><Query><Where><And><Neq><FieldRef Name='ContentType' /><Value Type='String'>Folder</Value></Neq><Neq><FieldRef Name='ContentType' /><Value Type='String'>폴더</Value></Neq>"" +
""</And></Where><OrderBy><FieldRef Name='Modified' Ascending='FALSE' /></OrderBy></Query><RowLimit>");
                                                                                          @__w.Write(ItemCount);

            @__w.Write("</RowLimit></View>\";\r\n\r\n    camlQuery.set_viewXml(q);\r\n\r\n    ");
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

            @__w.Write("listItems,\'Include(ID,FileLeafRef,FileDirRef)\');\r\n    \r\n    clientContext.execute" +
                    "QueryAsync(Function.createDelegate(this, this.");
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
            @__w.Write("\r\n    try{\r\n        var listtype = ");
               @__w.Write(this.ClientID );

            @__w.Write("list.get_baseTemplate();\r\n    \r\n        var listEnumerator = this.");
                          @__w.Write(this.ClientID );

            @__w.Write("listItems.getEnumerator();\r\n        var listurl = ");
              @__w.Write(this.ClientID );

            @__w.Write(@"list.get_rootFolder().get_serverRelativeUrl();
        
        while (listEnumerator.moveNext()) {
            itemHtml = """";

            var oListItem = listEnumerator.get_current();
            var filename = oListItem.get_item('FileLeafRef');
            filename = filename.replace('.', '_');
            filename += '.jpg';
            var dir = oListItem.get_item('FileDirRef');
            filename = dir + '/_t/' + filename;

            itemHtml += ""<img src='"" + filename + ""' width='");
                                                    @__w.Write(ImgWidth);

            @__w.Write("px\' height=\'");
                                                                             @__w.Write(ImgHeight);

            @__w.Write("px\'   id=\'\" + oListItem.get_item(\"ID\") + \"\'/>\";\r\n\t\t    jQuery(\'#");
       @__w.Write(this.ClientID );

            @__w.Write("_WaterWheel\').append(itemHtml);\r\n        }\r\n        //jQuery(\'#");
           @__w.Write(this.ClientID );

            @__w.Write("_link1\').attr(\"href\",");
                                                   @__w.Write(this.ClientID );

            @__w.Write("list1.get_rootFolder().get_serverRelativeUrl());\r\n        \r\n        var wwcarouse" +
                    "l = jQuery(\'#");
                          @__w.Write(this.ClientID );

            @__w.Write(@"_WaterWheel').waterwheelCarousel
		            ({
		                separation :170,flankingItems: 3, autoPlay:3000, 
		                movingToCenter: function ($item) {
		                jQuery('#callback-output').prepend('movingToCenter: ' + $item.attr('id') + '<br/>');
		                },
		                movedToCenter: function ($item) {
		                jQuery('#callback-output').prepend('movedToCenter: ' + $item.attr('id') + '<br/>');
		                },
		                movingFromCenter: function ($item) {
		                jQuery('#callback-output').prepend('movingFromCenter: ' + $item.attr('id') + '<br/>');
		                },
		                movedFromCenter: function ($item) {
		                jQuery('#callback-output').prepend('movedFromCenter: ' + $item.attr('id') + '<br/>');
		                },
		                clickedCenter: function ($item) {
		                jQuery('#callback-output').prepend('clickedCenter: ' + $item.attr('id') + '<br/>');
		                }
		            });

    }
    catch(err)
    {
    }
    ");
       } 
            @__w.Write("\r\n\r\n}\r\n</script>\r\n<div id=\'");
 @__w.Write(this.ClientID );

            @__w.Write("_WaterWheel\' class=\'WaterWheel\'>\r\n\r\n</div>\r\n\r\n");
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
