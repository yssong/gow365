using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;

namespace CircleLogicPortal.TabListWebpart
{
    [ToolboxItemAttribute(false)]
    public partial class TabListWebpart : WebPart
    {
        //배포 전에 ImgUrl을 수정해주세요. 
        private string imgUrl = "GOW365/TabList/";
        public string ImgUrl
        {
            get { return imgUrl; }
            set { imgUrl = value; }
        }

        #region Properties

        private string webName1 = string.Empty;
        private string listName1 = string.Empty;
        private string webName2 = string.Empty;
        private string listName2 = string.Empty;
        private string webName3 = string.Empty;
        private string listName3 = string.Empty;
        private int listItemCount = 7;

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("List Item Count"),
        WebDisplayName("List Item Count"),
        WebDescription("List Item Count")]
        public int ListItemCount
        {
            get { return listItemCount; }
            set { listItemCount = value; }
        }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("LIST 1"),
        WebDisplayName("LIST 1's WebName"),
        WebDescription("1")]
        public string WebName1
        {
            get { return webName1; }
            set { webName1 = value; }
        }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("LIST 1"),
        WebDisplayName("LIST 1's ListName"),
        WebDescription("1")]
        public string ListName1
        {
            get { return listName1; }
            set { listName1 = value; }
        }

        //

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("LIST 2"),
        WebDisplayName("LIST 2's WebName"),
        WebDescription("2")]
        public string WebName2
        {
            get { return webName2; }
            set { webName2 = value; }
        }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("LIST 2"),
        WebDisplayName("LIST 2's ListName"),
        WebDescription("2")]
        public string ListName2
        {
            get { return listName2; }
            set { listName2 = value; }
        }

        //

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("LIST 3"),
        WebDisplayName("LIST 3's WebName"),
        WebDescription("3")]
        public string WebName3
        {
            get { return webName3; }
            set { webName3 = value; }
        }

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("LIST 3"),
        WebDisplayName("LIST 3's ListName"),
        WebDescription("3")]
        public string ListName3
        {
            get { return listName3; }
            set { listName3 = value; }
        }

        #endregion
        // Instrumentation 메서드를 사용하여 팜 솔루션에서 성능 프로파일링을 수행할 때만 다음 SecurityPermission
        // 특성의 주석 처리를 제거하고 코드가 프로덕션을 위해 준비되었을 때 SecurityPermission
        // 특성을 제거합니다. SecurityPermission 특성은 생성자의 호출자를 위해 보안 검사를 건너뛰므로
        // 프로덕션 용도로 권장되지 않습니다.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public TabListWebpart()
        {

        }
        protected override void CreateChildControls()
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
