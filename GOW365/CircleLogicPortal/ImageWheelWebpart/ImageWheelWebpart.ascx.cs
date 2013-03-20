using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;

namespace CircleLogicPortal.ImageWheelWebpart
{
    [ToolboxItemAttribute(false)]
    public partial class ImageWheelWebpart : WebPart
    {
        // Instrumentation 메서드를 사용하여 팜 솔루션에서 성능 프로파일링을 수행할 때만 다음 SecurityPermission
        // 특성의 주석 처리를 제거하고 코드가 프로덕션을 위해 준비되었을 때 SecurityPermission
        // 특성을 제거합니다. SecurityPermission 특성은 생성자의 호출자를 위해 보안 검사를 건너뛰므로
        // 프로덕션 용도로 권장되지 않습니다.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        protected string ImgUrl = "GOW365/ImageWaterWheel/";
        private int itemCount = 15;

        [WebBrowsable(true),
         Personalizable(PersonalizationScope.Shared),
         DefaultValue("false"),
         Category("Picture"),
         WebDisplayName("불러올 아이템 개수"),
         WebDescription("해당 아이템을 전부 불러오므로 로딩 시간을 줄이려면 개수를 적절히 조절해야 합니다.")]
        public int ItemCount
        {
            get
            {
                return itemCount;
            }
            set
            {
                itemCount = value;
            }
        }

        private string imgWidth = "210";
        private string imgHeight = "140";


        [WebBrowsable(true),
         Personalizable(PersonalizationScope.Shared),
         DefaultValue("false"),
         Category("Picture"),
         WebDisplayName("이미지 가로 (각 이미지마다 여백이 10씩 붙습니다.) "),
         WebDescription("웹 파트 가로 크기에 여백 참고하시기 바랍니다.")]

        public string ImgWidth
        {
            get
            {
                return imgWidth;
            }
            set
            {
                imgWidth = value;
            }
        }

        [WebBrowsable(true),
         Personalizable(PersonalizationScope.Shared),
         DefaultValue("false"),
         Category("Picture"),
         WebDisplayName("이미지 세로 (+ 50 = 웹 파트 세로)"),
         WebDescription("제목을 불러오기 위해 세로 50px이 추가됩니다.")]
        public string ImgHeight
        {
            get
            {
                return imgHeight;
            }
            set
            {
                imgHeight = value;
            }
        }


        private string webName = string.Empty;
        private string listName = string.Empty;
        
        private int displayTime = 5000;
        private bool autoPlay = true;


        [WebBrowsable(true),
        Personalizable(PersonalizationScope.Shared),
        DefaultValue("false"),
        Category("Picture"),
        WebDisplayName("AutoPlay"),
        WebDescription("AutoPlay")]
        public bool AutoPlay
        {
            get
            {
                return autoPlay;
            }
            set
            {
                autoPlay = value;
            }
        }

        [WebBrowsable(true),
         Personalizable(PersonalizationScope.Shared),
         DefaultValue("false"),
         Category("Picture"),
         WebDisplayName("DisplayTime (1 sec = 1000 )"),
         WebDescription("DisplayTime (1 sec = 1000 )")]
        public int DisplayTime
        {
            get
            {
                return displayTime;
            }
            set
            {
                displayTime = value;
            }
        }

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

        

        public ImageWheelWebpart()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            ImgUrl = (SPContext.Current.Site.ServerRelativeUrl.EndsWith("/") ? SPContext.Current.Site.ServerRelativeUrl + ImgUrl : SPContext.Current.Site.ServerRelativeUrl + "/" + ImgUrl);
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}
