using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;

namespace CircleLogicPortal.SubSitesTabWepart
{
    [ToolboxItemAttribute(false)]
    public partial class SubSitesTabWepart : WebPart
    {
        // Instrumentation 메서드를 사용하여 팜 솔루션에서 성능 프로파일링을 수행할 때만 다음 SecurityPermission
        // 특성의 주석 처리를 제거하고 코드가 프로덕션을 위해 준비되었을 때 SecurityPermission
        // 특성을 제거합니다. SecurityPermission 특성은 생성자의 호출자를 위해 보안 검사를 건너뛰므로
        // 프로덕션 용도로 권장되지 않습니다.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]

        protected string ImgUrl = "GOW365/SubSitesTab/";

        private string webName1 = string.Empty;

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("Tab1"),
        WebDisplayName("Tab 1's Web URL"),
        WebDescription("1")]
        public string WebName1
        {
            get { return webName1; }
            set { webName1 = value; }
        }

        private string tabTitle1 = string.Empty;

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("Tab1"),
        WebDisplayName("Tab 1's Title"),
        WebDescription("2")]
        public string TabTitle1
        {
            get { return tabTitle1; }
            set { tabTitle1 = value; }
        }

        private string webName2 = string.Empty;

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("Tab2"),
        WebDisplayName("Tab 2's Web URL"),
        WebDescription("3")]
        public string WebName2
        {
            get { return webName2; }
            set { webName2 = value; }
        }

        private string tabTitle2 = string.Empty;

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("Tab2"),
        WebDisplayName("Tab 2's Title"),
        WebDescription("4")]
        public string TabTitle2
        {
            get { return tabTitle2; }
            set { tabTitle2 = value; }
        }

        private string webName3 = string.Empty;

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("Tab3"),
        WebDisplayName("Tab 3's Web URL"),
        WebDescription("5")]
        public string WebName3
        {
            get { return webName3; }
            set { webName3 = value; }
        }

        private string tabTitle3 = string.Empty;

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("Tab3"),
        WebDisplayName("Tab 3's Title"),
        WebDescription("6")]
        public string TabTitle3
        {
            get { return tabTitle3; }
            set { tabTitle3 = value; }
        }
        public SubSitesTabWepart()
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
