using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;

namespace GOW365.SurveyList
{
    [ToolboxItemAttribute(false)]
    public class SurveyList : WebPart
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
        

        protected override void CreateChildControls()
        {
            ImgUrl = (SPContext.Current.Site.ServerRelativeUrl.EndsWith("/") ? SPContext.Current.Site.ServerRelativeUrl + ImgUrl : SPContext.Current.Site.ServerRelativeUrl + "/" + ImgUrl);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            RenderStyle(writer);
            RenderBody(writer);
            RenderScript(writer);
        }

        private void RenderBody(HtmlTextWriter writer)
        {
            writer.WriteLine(@"
                <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                    <tr>
                        <td width='8' height='8' background='" + ImgUrl + @"survey_box_top_l.gif' style='background-repeat: no-repeat;'></td>
                        <td height='8' background='" + ImgUrl + @"survey_box_top.gif' style='background-repeat: repeat-x;'></td>
                        <td width='8' height='8' background='" + ImgUrl + @"survey_box_top_r.gif' style='background-repeat: no-repeat;'></td>
                    </tr>
                    <tr>
                        <td width='8' valign='top' background='" + ImgUrl + @"survey_box_left.gif' style='background-repeat: repeat-y;'></td>
                        <td background='" + ImgUrl + @"survey_box_bg.gif' style='background-repeat: repeat-x;'>
                            <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                <tr>
                                    <td colspan='2' align='right' height='18'>
                                        <img src='" + ImgUrl + @"more_button.gif' width='36' height='18' style='cursor:pointer' onclick='" + string.Format("javascript:document.location.href=\"" + SiteUrl + "\"") + @"' />
                                    </td>
                                </tr>
                                <tr>
                                    <td width='116' valign='top' align='center'>&nbsp;"); writer.WriteLine("<img src='" + ImgUrl + @"survey_image.gif' >");/* 좌측 이미지 삽입 부분 (현재 이미지 없음) */ writer.WriteLine(@"</td>
                                    <td valign='top'>"); this.RenderMainTable(writer); writer.WriteLine(@"</td>                                    
                                </tr>
                            </table>
                        </td>
                        <td width='8' style='background-position: top;' align='right' valign='top' background='" + ImgUrl + @"survey_box_right.gif' style='background-repeat: repeat-y;'></td>
                    </tr>
                    <tr>
                        <td width='8' height='8' background='" + ImgUrl + @"survey_bottom_l.gif' style='background-repeat: no-repeat;'></td>
                        <td height='8' background='" + ImgUrl + @"survey_bottom.gif' style='background-repeat: repeat-x;'></td>
                        <td width='8' height='8' background='" + ImgUrl + @"survey_bottom_r.gif' style='background-repeat: no-repeat;'></td>
                    </tr>
                </table>");
        }

        private static void RenderStyle(HtmlTextWriter writer)
        {
            writer.WriteLine(@"
                <style type='text/css'>
                    .selectedBestTab {
                        font-family: '맑은 고딕','돋움';
                        font-size: 12px;
                        font-weight: bold;
                        color: #595959;
                    }
                    .unselectedBestTab {
                        font-family: '맑은 고딕','돋움';
                        font-size: 12px;
                        color: #838383;
                    }
                    .title_text {
                        padding-top:4px;
                        padding-bottom:4px;
                        font-family: '맑은 고딕','돋움';
                        font-size: 12px;
                        color: #0a56a3;
                    }
                    .basic_text {
                        font-family: '맑은 고딕','돋움', '굴림', 'seoul', 'arial', 'helvetica';
                        font-size: 12px;
                        color: #4c4c4c;
                    }
                    .basic_text A:link { text-decoration:none; color:333333}
                    .basic_text A:visited { text-decoration:none; color:333333}
                    .basic_text A:active { text-decoration:none; color:333333}
                    .basic_text A:hover {text-decoration:none; color:#333333}
                </style>
                ");
        }

        private void RenderMainTable(HtmlTextWriter writer)
        {
            writer.WriteLine(@"
                    <table width='95%' border='0' cellspacing='0' cellpadding='0'>
                        <tr>
                            <td>
                                <div id='" + this.ClientID + "_table' style='width:100%; height:100%'>");

            SPSite site = null;
            SPWeb web = null;

            if (!SiteUrl.StartsWith("http"))
            {
                if (SPContext.Current.Site.ServerRelativeUrl == "/")
                {
                    SiteUrl = SPContext.Current.Site.Url + SiteUrl;
                }
                else
                {
                    SiteUrl = SPContext.Current.Site.Url.Replace(SPContext.Current.Site.ServerRelativeUrl, "") + SiteUrl;
                }


            }

            if (SiteUrl != "")
            {
                site = new SPSite(SiteUrl);
            }

            try
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("Title", typeof(string));
                dt.Columns.Add("Url", typeof(string));
                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("DateS", typeof(string));

                if (site != null)
                {
                    web = site.OpenWeb();

                    foreach (SPList li in web.Lists)
                    {
                        if (li.BaseType == SPBaseType.Survey)
                        {
                            dt.Rows.Add(li.Title, li.DefaultViewUrl, li.Created.ToString(), li.Created.ToShortDateString());
                        }
                        else { }
                    }

                    dt.DefaultView.Sort = "Date desc";
                    int count = 0;
                    foreach (DataRowView dv in dt.DefaultView)
                    {
                        count++;
                        if (count <= RowLimit)
                        {
                            string title = string.Format("* <a href='' onclick='javascript:window.open(\"{0}\"); return false;'>{1}</a>", dv["Url"], dv["Title"]);
                            writer.WriteLine(@"
                                            <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                	            <tr>
                                                    <td style='height: 14pt; font-family: 돋움; vertical-align: middle; padding-left:14px'>" + title + @"</td>
                                                    <td align='right' style='height: 14pt; width: 70px; font-family: 돋움; vertical-align: middle; padding-left:14px'>" + dv["DateS"] + @"</td>
                                                </tr>
                                            </table>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                writer.WriteLine(ex);
            }
            finally
            {
                if (web != null)
                {
                    web.Dispose();
                }
                if (site != null)
                {
                    site.Close();
                    site.Dispose();
                }
                writer.WriteLine(@" </div></td></tr></table>");
            }
            //}
        }

        //// <summary>
        /// 일정 길이를 넘어서면 텍스트를 제한하고 뒤에 ...표시
        /// 한 글자가 (소문자 or 아라비아 숫자 or 공백)일 때 길이 0.5씩 확장
        /// </summary>
        /// <param name="strTitle">입력 값</param>
        /// <param name="nLength">제한 길이</param>
        /// <returns>0부터 nLength까지 Substring</returns>
        private string GetLimitString(string strTitle, int nLength)
        {
            if (strTitle.Length > nLength)
            {
                double addLength = 0.0;
                foreach (char c in strTitle.Remove(nLength))
                {
                    if (Char.IsLower(c) || Char.IsDigit(c) || Char.IsWhiteSpace(c))
                        addLength += 0.5;
                }

                if (strTitle.Length > nLength + addLength)
                    strTitle = strTitle.Remove(nLength + (int)addLength) + "...";
            }

            return strTitle;
        }



        private void RenderScript(HtmlTextWriter writer)
        {
            writer.Write(@"
                <script language='JavaScript'>
                    function openDialog(_url) {  
                        var options = {  url: _url ,  width: 800, height: 600, };  
                        SP.UI.ModalDialog.showModalDialog(options);  
                    }
                </script>
                ");
        }
    }
}
