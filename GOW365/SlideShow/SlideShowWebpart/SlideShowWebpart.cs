using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace GOW365.SlideShowWebpart
{
    [ToolboxItemAttribute(false)]
    public class SlideShowWebpart : WebPart
    {
        private string imgWidth = "100";
        private string imgHeight = "200";
        private string webName = string.Empty;
        private string listName = string.Empty;
        private int itemCount = 50;
        private int viewCount = 3;
        private int displayTime = 5000;
        private bool autoPlay = true;
        private bool showbutton = true;
        //배포 전에 JsUrl을 수정해주세요. (따로 업로드 해야 함)
        private string JsUrl = "/GOW365/SlideShow/";
        bool checkvalue = false;

        SPWeb web;
        SPList List;
        Label noUrl;
        Label wrongViewCount;

        #region Properties

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

        [WebBrowsable(true),
         Personalizable(PersonalizationScope.Shared),
         DefaultValue("false"),
         Category("Picture"),
         WebDisplayName("한 번에 보여질 이미지 개수"),
         WebDescription("한 화면에 보여지는 이미지의 개수")]

        public int ViewCount
        {
            get
            {
                return viewCount;
            }
            set
            {
                viewCount = value;
            }
        }

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

        #endregion

        protected override void CreateChildControls()
        {
            noUrl = new Label();
            this.noUrl.Text = "Check Site Address or List Name.";
            this.Controls.Add(noUrl);

            wrongViewCount = new Label();
            this.wrongViewCount.Text = "ViewCount number is bigger than items count number.";
            this.Controls.Add(wrongViewCount);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (ListName != string.Empty)
            {
                if (WebName != string.Empty && WebName.StartsWith("https"))
                {
                    //배포 전에 WebName을 수정해주세요.
                    WebName = WebName.Replace("https://ishare.spiraxsarco.com/companies/asia-pac/kr", "");
                    WebName = WebName.StartsWith("/") ? WebName.Remove(0, 1) : WebName;
                    WebName = WebName.EndsWith("/") ? WebName.Remove(WebName.Length - 1, 1) : WebName;
                }
                else if (WebName == string.Empty)
                {
                    SPWeb w = SPContext.Current.Web;
                    WebName = w.ServerRelativeUrl.ToString();
                    w.Close();
                    w.Dispose();
                }

                if (ListCheck(WebName, ListName))
                {
                    this.RenderHtml(writer);
                }
            }
            else
            {
                this.noUrl.RenderControl(writer);
            }
        }

        private bool ListCheck(string url, string name)
        {
            bool check = false;
            try
            {
                web = SPContext.Current.Site.OpenWeb(url);
                if (name == string.Empty)
                { check = true; }
                else
                {
                    SPList list = web.Lists[name];
                    if (list.BaseTemplate.ToString() == SPListTemplateType.PictureLibrary.ToString())
                    {
                        check = true;
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (web != null)
                {
                    web.Close();
                    web.Dispose();
                }
            }
            return check;
        }

        protected void RenderHtml(HtmlTextWriter writer)
        {
            writer.WriteLine(@"
            <div id='" + this.ClientID + @"_carousel' style='display:none;'>
                <br>
                <ul>" + GetData(writer) + @"
                </ul>
            </div>");

            writer.WriteLine(@"<script type='text/javascript'>
// Only do anything if jQuery isn't defined
if (typeof jQuery == 'undefined') {
 if (typeof $ == 'function') {
  // warning, global var
  thisPageUsingOtherJSLibrary = true;
 }

  function getScript(url, success) {
    var script     = document.createElement('script');
       script.src = url;

    var head = document.getElementsByTagName('head')[0],
    done = false;

    // Attach handlers for all browsers
    script.onload = script.onreadystatechange = function() {
     if (!done && (!this.readyState || this.readyState == 'loaded' || this.readyState == 'complete')) {
      done = true;
        // callback function provided as param
    success();

        script.onload = script.onreadystatechange = null;
    head.removeChild(script);
       };
    };
    head.appendChild(script);
  };

  getScript('" +JsUrl+@"jquery-1.9.1.min.js', function() {
   if (typeof jQuery=='undefined') {
     // Super failsafe - still somehow failed...
    } else {
     // jQuery loaded! Make sure to use .noConflict just in case
   fancyCode();
      if (thisPageUsingOtherJSLibrary) {
    // Run your jQuery Code
   } else {
    // Use .noConflict(), then run your jQuery Code
   }
    }
  });
 } else { // jQuery was already loaded
  // Run your jQuery Code
</script>
                                <script type='text/javascript' src='" + JsUrl + @"jquery.infinitecarousel2.js'></script>
                                <script type='text/javascript'>
                                $(function(){
	                                $('#" + this.ClientID + @"_carousel').infiniteCarousel(
                                    {
                                        imgWidth:" + (Convert.ToInt32(ImgWidth) + 10) + @",
                                        imgHeight:" + (Convert.ToInt32(ImgHeight) + 50) + @",
                                        imagePath:'" + JsUrl + @"',
                                        advance:" + ViewCount + @",
                                        autoStart:" + AutoPlay.ToString().ToLower() + @",
                                        displayTime:" + DisplayTime + @",
                                        showControls: " + showbutton.ToString().ToLower() + @"
                                    });
                                    $('#" + this.ClientID + @"_carousel').css('display','block');
                                });
                                </script>");
        }

        private string GetData(HtmlTextWriter writer)
        {
            string returnValue = string.Empty;
            try
            {
                web = SPContext.Current.Site.OpenWeb(WebName);
                List = web.Lists[ListName];
                string linkurl = string.Empty;
                if (List.BaseTemplate.ToString() == SPListTemplateType.PictureLibrary.ToString())
                {
                    this.ListName = List.Title;

                    checkvalue = true;
                    SPQuery query = GetCAMLQuery();
                    SPListItemCollection listitemcoll = List.GetItems(query);

                    foreach (SPListItem spl in listitemcoll)
                    {
                        string strTitle = spl["NameOrTitle"].ToString();
                        strTitle = strTitle.Contains(".") ? strTitle.Split('.')[0] : strTitle;
                        strTitle = strTitle.Contains("__") ? strTitle.Replace("__", "<br>") : strTitle;

                        if (List.Fields.ContainsField("URL"))
                        {
                            if (spl["URL"] == null)
                            {
                                linkurl = List.DefaultDisplayFormUrl.ToString() + "?ID=" + spl["ID"].ToString();
                            }
                            else if (spl["URL"].ToString().Contains(","))
                            {
                                linkurl = spl["URL"].ToString().Split(',')[0];
                            }
                            else
                            {
                                linkurl = spl["URL"].ToString();
                            }
                        }
                        else
                        {
                            linkurl = List.DefaultDisplayFormUrl.ToString() + "?ID=" + spl["ID"].ToString();
                        }

                        returnValue += @"   
                                    <li>
                                        <table width='" + (Convert.ToInt32(ImgWidth) + 10).ToString() + @"px' padding='0' margin='0' border='0'>
                                            <tr>
                                                <td width='" + ImgWidth + @"px' align='center' valign='top'>
                                                    <a href='" + linkurl + @"' onfocus='this.blur()'>
                                                        <img src='" + spl["FileRef"].ToString() + @"' width='" + ImgWidth + @"px' style='max-height:" + ImgHeight + @"px; border:#000000 1px solid;'/>
                                                    </a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width='" + ImgWidth + @"px' style='word-break:break-all;' align='center' valign='middle'>
                                                    <a href='" + linkurl + @"' onfocus='this.blur()'>
                                                        <span>" + strTitle + @"</span>
                                                    </a>
                                                </td>
                                            </tr>
                                        </table>
                                    </li>";

                    }

                    if (listitemcoll.Count <= ViewCount)
                    {
                        ViewCount = listitemcoll.Count - 1;
                        if (ViewCount == 0)
                        {
                            returnValue = string.Empty;
                            ViewCount = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.wrongViewCount.RenderControl(writer);
            }
            finally
            {
                if (web != null)
                {
                    web.Close();
                    web.Dispose();
                }
            }

            return returnValue;
        }

        private SPQuery GetCAMLQuery()
        {
            SPQuery query = new SPQuery();
            query.ViewAttributes = "Scope='Recursive'";

            string strQuery = string.Format(
                @"<Where>
                    <And>
                        <Neq>
                            <FieldRef Name='ContentType' />
                            <Value Type='String'>Folder</Value>
                        </Neq>
                        <Neq>
                            <FieldRef Name='ContentType' />
                            <Value Type='String'>폴더</Value>
                        </Neq>
                    </And>
                </Where>");

            strQuery +=
                @"<OrderBy>
                    <FieldRef Name='Modified' Ascending='False' />
                </OrderBy>";

            query.Query = strQuery;
            query.RowLimit = (uint)ItemCount;
            return query;
        }
    }
}
