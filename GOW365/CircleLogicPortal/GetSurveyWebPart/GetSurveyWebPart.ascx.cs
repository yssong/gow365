using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;

namespace CircleLogicPortal.GetSurveyWebPart
{
    [ToolboxItemAttribute(false)]
    public partial class GetSurveyWebPart : WebPart
    {
        private int rowLimit = 5;
        private string Url = string.Empty;

        // Survey를 불러올 Site Url
        [WebBrowsable(true),
        Personalizable(PersonalizationScope.Shared),
        DefaultValue("false"),
        Category("Survey"),
        WebDisplayName("Survey URL"),
        WebDescription("Survey Site url")]

        public string SiteUrl
        {
            get
            {
                return Url;
            }
            set
            {
                Url = value;
            }
        }

        // 리스트 개수
        [WebBrowsable(true),
        Personalizable(PersonalizationScope.Shared),
        DefaultValue("false"),
        Category("Survey"),
        WebDisplayName("RowLimit"),
        WebDescription("Display Survey Count")]

        public int RowLimit
        {
            get
            {
                return rowLimit;
            }
            set
            {
                if (value < 0)
                    rowLimit = 0;
                else
                    rowLimit = value;
            }
        }

        private string ImgUrl = "GOW365/GetSurveyList/";

        // Instrumentation 메서드를 사용하여 팜 솔루션에서 성능 프로파일링을 수행할 때만 다음 SecurityPermission
        // 특성의 주석 처리를 제거하고 코드가 프로덕션을 위해 준비되었을 때 SecurityPermission
        // 특성을 제거합니다. SecurityPermission 특성은 생성자의 호출자를 위해 보안 검사를 건너뛰므로
        // 프로덕션 용도로 권장되지 않습니다.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public GetSurveyWebPart()
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
