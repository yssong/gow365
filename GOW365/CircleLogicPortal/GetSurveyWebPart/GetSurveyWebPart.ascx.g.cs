﻿//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.18033
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CircleLogicPortal.GetSurveyWebPart {
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
    
    
    public partial class GetSurveyWebPart {
        
        public static implicit operator global::System.Web.UI.TemplateControl(GetSurveyWebPart target) 
        {
            return target == null ? null : target.TemplateControl;
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__BuildControlTree(global::CircleLogicPortal.GetSurveyWebPart.GetSurveyWebPart @__ctrl) {
            @__ctrl.SetRenderMethodDelegate(new System.Web.UI.RenderMethod(this.@__Render__control1));
        }
        
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void @__Render__control1(System.Web.UI.HtmlTextWriter @__w, System.Web.UI.Control parameterContainer) {
            @__w.Write("\r\n\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
                                     @__w.Write(ImgUrl);

            @__w.Write("SurveyStyle.css\" />\r\n<script type=\"text/javascript\">\r\n   \r\n    ExecuteOrDelayUnti" +
                    "lScriptLoaded(");
                            @__w.Write(this.ClientID);

            @__w.Write("Initialize, \"sp.js\");\r\n       \r\n    //Retrieve  the Tab items\r\nfunction ");
 @__w.Write(this.ClientID);

            @__w.Write("Initialize() {\r\n    //Get the current SP context\r\n\r\n\tvar camlQuery = new SP.CamlQ" +
                    "uery();\r\n\t   \r\n    camlQuery.set_viewXml(q);\r\n\r\n    \r\n    ");
       if (SiteUrl.Trim() != "")
       { 
            @__w.Write("\r\n    clientContext = new SP.ClientContext(\"");
                                  @__w.Write(SiteUrl.Trim());

            @__w.Write("\");\r\n    ");
       }else { 
            @__w.Write("\r\n    clientContext = new SP.ClientContext.get_current();\r\n    ");
       } 
            @__w.Write("\r\n\r\n    web = clientContext.get_web();\r\n    this.");
 @__w.Write(this.ClientID );

            @__w.Write("lists = clientContext.web.get_lists();\r\n    \r\n    clientContext.load(");
               @__w.Write(this.ClientID );

            @__w.Write("lists,\"DefaultDisplayFormUrl\",\"BaseTemplate\",\"RootFolder\");\r\n    \r\n    clientCont" +
                    "ext.executeQueryAsync(Function.createDelegate(this, this.");
                                                               @__w.Write(this.ClientID);

            @__w.Write("onListItemsLoadSuccess), Function.createDelegate(this, this.onListItemsLoadFailed" +
                    "));\r\n    \r\n\r\n}\r\n\r\nfunction onListItemsLoadFailed(sender, args) {\r\n\tSP.UI.Notify." +
                    "addNotification(\"List items load failed: \" + args.get_message(), false);\r\n}\r\nfun" +
                    "ction ");
 @__w.Write(this.ClientID);

            @__w.Write("onListItemsLoadSuccess(sender, args) {\r\n\t\r\n    try{\r\n        \r\n       \r\n\r\n    }\r\n" +
                    "    catch(err)\r\n    {\r\n    }\r\n   \r\n\r\n}\r\n</script>\r\n\r\n<table width=\'100%\' border=" +
                    "\'0\' cellspacing=\'0\' cellpadding=\'0\'>\r\n    <tr>\r\n        <td width=\'8\' height=\'8\'" +
                    " background=\'");
                                     @__w.Write(ImgUrl );

            @__w.Write("survey_box_top_l.gif\' style=\'background-repeat: no-repeat;\'></td>\r\n        <td he" +
                    "ight=\'8\' background=\'");
                           @__w.Write(ImgUrl );

            @__w.Write("survey_box_top.gif\' style=\'background-repeat: repeat-x;\'></td>\r\n        <td width" +
                    "=\'8\' height=\'8\' background=\'");
                                     @__w.Write(ImgUrl );

            @__w.Write("survey_box_top_r.gif\' style=\'background-repeat: no-repeat;\'></td>\r\n    </tr>\r\n   " +
                    " <tr>\r\n        <td width=\'8\' valign=\'top\' background=\'");
                                       @__w.Write(ImgUrl );

            @__w.Write("survey_box_left.gif\' style=\'background-repeat: repeat-y;\'></td>\r\n        <td back" +
                    "ground=\'");
                @__w.Write(ImgUrl );

            @__w.Write("survey_box_bg.gif\' style=\'background-repeat: repeat-x;\'>\r\n            <table widt" +
                    "h=\'100%\' border=\'0\' cellspacing=\'0\' cellpadding=\'0\'>\r\n                <tr>\r\n    " +
                    "                <td colspan=\'2\' align=\'right\' height=\'18\'>\r\n                    " +
                    "    <img src=\'");
                          @__w.Write(ImgUrl );

            @__w.Write(@"more_button.gif' width='36' height='18' style='cursor:pointer' onclick='"" + string.Format(""javascript:document.location.href=\"""" + SiteUrl + ""\"""") + @""' />
                    </td>
                </tr>
                <tr>
                    <td width='116' valign='top' align='center'>&nbsp;""); 
                        <img src='");
                          @__w.Write(ImgUrl );

            @__w.Write("survey_image.gif\' />\r\n                    </td>\r\n                    <td valign=\'" +
                    "top\'>\r\n                        <div id=\"");
                         @__w.Write(this.ClientID );

            @__w.Write(@"_survey"">

                        </div>
                    </td>                                    
                </tr>
            </table>
        </td>
        <td width='8' style='background-position: top;' align='right' valign='top' background='");
                                                                                       @__w.Write(ImgUrl );

            @__w.Write("survey_box_right.gif\' style=\'background-repeat: repeat-y;\'></td>\r\n    </tr>\r\n    " +
                    "<tr>\r\n        <td width=\'8\' height=\'8\' background=\'");
                                     @__w.Write(ImgUrl );

            @__w.Write("survey_bottom_l.gif\' style=\'background-repeat: no-repeat;\'></td>\r\n        <td hei" +
                    "ght=\'8\' background=\'");
                           @__w.Write(ImgUrl );

            @__w.Write("survey_bottom.gif\' style=\'background-repeat: repeat-x;\'></td>\r\n        <td width=" +
                    "\'8\' height=\'8\' background=\'");
                                     @__w.Write(ImgUrl );

            @__w.Write("survey_bottom_r.gif\' style=\'background-repeat: no-repeat;\'></td>\r\n    </tr>\r\n</ta" +
                    "ble>");
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
