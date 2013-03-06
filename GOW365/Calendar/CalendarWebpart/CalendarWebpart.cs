using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace GOW365.CalendarWebpart
{
    [ToolboxItemAttribute(false)]
    public class CalendarWebpart : WebPart
    {
        CalendarControl cal;
        Label lblCalendarHidden;
        Label lblWrongUrl;
        SPWeb web;

        //배포 전에 ImgUrl을 수정해주세요. (따로 업로드 해야 함)
        public string ImgUrl = "/GOW365/Calendar/";
        
        private string webName = string.Empty;
        private string listName = string.Empty;
        private bool isViewCalendar = true;
        private bool check = false;
        private string linkurl = string.Empty;

        #region Properties

        [WebBrowsable(true),
        Personalizable(PersonalizationScope.Shared),
        DefaultValue("false"),
        Category("List"),
        WebDisplayName("Site Address"),
        WebDescription("Site Address")]
        public string WebName
        {
            get { return webName; }
            set { webName = value; }
        }

        [WebBrowsable(true),
        Personalizable(PersonalizationScope.Shared),
        DefaultValue("false"),
        Category("List"),
        WebDisplayName("Calendar List Name"),
        WebDescription("Calendar List Name")]
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

        public CalendarWebpart()
        {
            //this.ExportMode = WebPartExportMode.All;
            //this.ImgUrl = SPContext.Current.Site.Url + ImgUrl;
        }

        protected override void CreateChildControls()
        {
            if (WebName != string.Empty && WebName.StartsWith("https"))
            {
                //배포 전에 WebName을 수정해주세요.
                string url=SPContext.Current.Site.Url;
                WebName = WebName.Replace(url, "");
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

            if (ListCheck())
            {
                cal = new CalendarControl();
                this.cal.ID = "Calendar";
                this.cal.WebURL = this.webName;
                this.cal.ListName = this.listName;
                this.cal.CellPadding = 3;
                this.cal.CellSpacing = 1;
                this.cal.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                this.cal.BorderWidth = Unit.Pixel(0);
                this.cal.BorderColor = System.Drawing.Color.LightGray;
                this.cal.TitleStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#f2f2f2");
                this.cal.TitleStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#6c6b6b");
                this.cal.DayHeaderStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#818080");
                this.cal.DayHeaderStyle.Font.Size = FontUnit.Point(8);
                this.cal.DayStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#9f9e9e");
                this.cal.OtherMonthDayStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#CCCCCC");
                this.cal.OtherMonthDayStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#f2f2f2");
                this.cal.SelectedDayStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffdf88");
                this.cal.SelectedDayStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#9f9e9e");
                this.cal.Width = Unit.Percentage(100);

                if (isViewCalendar)
                    cal.Style.Add("display", "block");
                else
                    cal.Style.Add("display", "none");

                this.Controls.Add(cal);

                lblCalendarHidden = new Label();
                this.lblCalendarHidden.ID = this.lblCalendarHidden.ClientID;
                this.lblCalendarHidden.Attributes.Add("style", "cursor:pointer;");
                this.lblCalendarHidden.Attributes.Add("onclick", "javascript:" + this.ClientID + "_calendarHidden();");
                this.Controls.Add(lblCalendarHidden);
            }
            else
            {
                lblWrongUrl = new Label();
                this.lblWrongUrl.Text = "Check Site Address or ListName";
                this.Controls.Add(lblWrongUrl);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.ImgUrl = SPContext.Current.Site.Url + this.ImgUrl;
            RenderClientScript(writer);
            if (check)
            {
                //테두리 Design
                writer.WriteLine(@"
                        <table width='100%' border='0' cellpadding='0' cellspacing='0'>
                            <tr valign='top'>
                                <td height='35' background='" + this.ImgUrl + @"date_m_3.jpg'>
                                    <table width='100%' border='0' cellpadding='0' cellspacing='0'>
                                        <tr>
                                            <td style='font-size:0' width='63'><a href='" + linkurl + "' target='_top' onFocus='this.blur()'><img src='" + this.ImgUrl + "date_m_1.jpg" + @"' width='63' height='35' border='0'></a></td>
                                            <td>&nbsp; </td>
                                            <td style='font-size:0' width='8'><img src='" + this.ImgUrl + @"date_m_2.jpg' width='8' height='35'> </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>    
                                    <table width='100%' border='0' cellpadding='0' cellspacing='0'>
                                        <tr>
                                            <td style='font-size:0' width='8' valign='top' background='" + this.ImgUrl + @"date_m_4.jpg'></td>
                                            <td>"); cal.RenderControl(writer); writer.WriteLine(@"</td>
                                            <td style='font-size:0' width='8' valign='top' background='" + this.ImgUrl + @"date_m_5.jpg'></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width='100%' border='0' cellpadding='0' cellspacing='0'>
                                        <tr>
                                            <td style='font-size:0' width='8' height='9'><img src='" + this.ImgUrl + @"date_m_6.jpg' width='8' height='9'> </td>
                                            <td style='font-size:0' height='9' background='" + this.ImgUrl + @"date_m_8.jpg'><img src='" + this.ImgUrl + @"date_m_8.jpg' width='2' height='9'> </td>
                                            <td style='font-size:0' height='9' width='8'><img src='" + this.ImgUrl + @"date_m_7.jpg' width='8' height='9'> </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                ");

                lblCalendarHidden.RenderControl(writer);
                writer.WriteLine(@"
                        <script type='text/javascript'>
                            " + this.ClientID + @"_scheduleBar(new Date());
                            //" + this.ClientID + @"_calendarHidden();
                        </script>
                ");
            }
            else
            {
                writer.Write("Check Site Address or ListName");
            }
        }

        private void RenderClientScript(HtmlTextWriter output)
        {
            output.WriteLine(@"<script language='JavaScript'>
                                var " + this.ClientID + @"_activeMenu = null;
                                var " + this.ClientID + @"_activePos = null;
                                var " + this.ClientID + @"_WebPartWPQ = null;
                                var save" + this.ClientID + @"_WebPartWPQ_innerHTML = '';");

            if (check)
            {
                output.WriteLine("var " + this.ClientID + @"_calendarID = '_" + this.cal.ID + "';");
                output.WriteLine("var " + this.ClientID + @"_calendarHiddenID = '_" + this.lblCalendarHidden.ID + "';");
                output.WriteLine("var " + this.ClientID + @"_varCalendar = null;");
            }

            output.WriteLine(@"
            function " + this.ClientID + @"_scheduleBar(sender) {
                if (typeof(sender) == 'object')
                    sender = " + this.ClientID + @"_dateSubstition(sender);
                " + this.ClientID + @"_showmenu(sender);
            }

            function " + this.ClientID + @"_dateSubstition(date) {
                var month = date.getMonth() < 9 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1;
                var day = date.getDate() < 10 ? '0' + date.getDate() : date.getDate();
	            date = date.getYear() + '-' + month + '-' + day;
                return date;
            }

            var view = null;
            function " + this.ClientID + @"_calendarHidden() {
                " + this.ClientID + @"_varCalendar =  document.getElementById('" + this.ClientID + @"' + " + this.ClientID + @"_calendarID);
		        if (" + this.ClientID + @"_varCalendar.style.display == 'none') {
		            " + this.ClientID + @"_varCalendar.style.display = 'block';
		        } else {
		            " + this.ClientID + @"_varCalendar.style.display = 'none';
                }
            }
            
            function " + this.ClientID + @"_showmenu(sender) {
                var menuobj;
                if(menuobj=document.getElementById(sender))
                    menuobj.style.position = ""absolute"";
                else 
                    return;

                " + this.ClientID + @"_WebPartWPQ = document.getElementById('" + this.ClientID + @"' + " + this.ClientID + @"_calendarID).parentElement;
            
                if (" + this.ClientID + @"_activeMenu != null) {
                    if (" + this.ClientID + @"_activeMenu.id == menuobj.id) { 
                        return;
                    }
                } else {
                    save" + this.ClientID + @"_WebPartWPQ_innerHTML = " + this.ClientID + @"_WebPartWPQ.innerHTML;
                }
                " + this.ClientID + @"_activeMenu = menuobj;

                " + this.ClientID + @"_WebPartWPQ.innerHTML = save" + this.ClientID + @"_WebPartWPQ_innerHTML + menuobj.innerHTML;              

                return;
            }

            function " + this.ClientID + @"_defaultCalendar() {
                " + this.ClientID + @"_WebPartWPQ.innerHTML = save" + this.ClientID + @"_WebPartWPQ_innerHTML;
                " + this.ClientID + @"_activeMenu = null;
            }

            function " + this.ClientID + @"_openDialog(urlstr) {
                var options = {
                    url: urlstr,
                    width: 800,
                    height: 600
                };
            SP.UI.ModalDialog.showModalDialog(options);    
            }
            ");
            output.WriteLine("</script>");
        }

        private bool ListCheck()
        {
            try
            {
                web = SPContext.Current.Site.OpenWeb(WebName);
                SPList calendarList = web.Lists[listName];
                linkurl = web.Lists[listName].DefaultViewUrl.ToString();
                check = true;
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
    }
}
