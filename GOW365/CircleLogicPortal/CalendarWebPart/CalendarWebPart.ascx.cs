﻿using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;

namespace CircleLogicPortal.CalendarWebPart
{
    [ToolboxItemAttribute(false)]
    public partial class CalendarWebPart : WebPart
    {
        protected string ImgUrl = "GOW365/Calendar/";
       
        private string webName = string.Empty;
        private string listName = string.Empty;


        [WebBrowsable(true),
         Personalizable(PersonalizationScope.Shared),
         DefaultValue("false"),
         Category("List"),
         WebDisplayName("Site Address"),
         WebDescription("Site Address")]
        public string WebName
        {
            get
            {
                return webName;
            }
            set
            {
                webName = value;
            }
        }

        [WebBrowsable(true),
         Personalizable(PersonalizationScope.Shared),
         DefaultValue("false"),
         Category("List"),
         WebDisplayName("List Name"),
         WebDescription("List Name")]
        public string ListName
        {
            get
            {
                return listName;
            }
            set
            {
                listName = value;
            }
        }

        // Instrumentation 메서드를 사용하여 팜 솔루션에서 성능 프로파일링을 수행할 때만 다음 SecurityPermission
        // 특성의 주석 처리를 제거하고 코드가 프로덕션을 위해 준비되었을 때 SecurityPermission
        // 특성을 제거합니다. SecurityPermission 특성은 생성자의 호출자를 위해 보안 검사를 건너뛰므로
        // 프로덕션 용도로 권장되지 않습니다.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public CalendarWebPart()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ImgUrl = (SPContext.Current.Site.ServerRelativeUrl.EndsWith("/") ? SPContext.Current.Site.ServerRelativeUrl + ImgUrl : SPContext.Current.Site.ServerRelativeUrl + "/" + ImgUrl);
        }
    }
}
