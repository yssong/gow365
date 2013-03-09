using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using System.Data;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Utilities;

namespace GOW365.CalendarWebpart
{
    public class CalendarControl : System.Web.UI.WebControls.Calendar
    {
        private string webURL;
        private string listName;
        private string imgUrl;

        SPWeb web;

        public CalendarControl()
        {
            CalendarWebpart calendarWebPart = new CalendarWebpart();
            imgUrl = calendarWebPart.ImgUrl;
        }

        public string ListName
        {
            get { return listName; }
            set { listName = value; }
        }

        public string WebURL
        {
            get { return webURL; }
            set { webURL = value; }
        }

        protected override void OnDayRender(TableCell cell, CalendarDay day)
        {
            cell.Controls.Clear();

            //해당일에 이벤트가 있을경우 스크립트 처리
            if (HasEvent(day) || day.IsToday)
            {
                CreateCellControls(cell, day);
            }
            else
            {
                cell.Controls.Add(new LiteralControl(day.DayNumberText));
            }

            if (day.IsToday)
            {
                cell.Attributes.Add("style", "background:#cee7ff;");
            }
            else if (day.IsOtherMonth)
            {
                cell.Attributes.Add("style", "font-color:LightGray;background:#ffffff;");
            }
            else if (day.IsWeekend)
            {
                cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ff9932");
                cell.Attributes.Add("style", "color:#ff0200;background:#ffffff;");
            }
        }

        private bool HasEvent(CalendarDay day)
        {
            bool check = false;

            try
            {
                web = SPContext.Current.Site.OpenWeb(webURL);
                SPList calendarList = web.Lists[listName];

                SPQuery query = new SPQuery();
                query.RecurrenceOrderBy = true;
                query.ExpandRecurrence = true;
                query.DatesInUtc = true;
                query.CalendarDate = new DateTime(day.Date.Year, day.Date.Month, day.Date.Day, 00, 00, 00, DateTimeKind.Utc);

                query.Query = "<Where>" + "<DateRangesOverlap>"
                                        + "<FieldRef Name=\"EventDate\" />"
                                        + "<FieldRef Name=\"EndDate\" />"
                                        + "<FieldRef Name=\"RecurrenceID\" />"
                                        + "<Value Type=\"DateTime\">"
                                        + "<Today/>"
                                        + "</Value>"
                                        + "</DateRangesOverlap>"
                                        + "</Where>";
                SPListItemCollection calendarItems = calendarList.GetItems(query);
                if (calendarItems.Count > 0)
                {
                    DataTable dt = calendarItems.GetDataTable();
                    DataRow[] dr = dt.Select("EndDate >= '" + day.Date.ToString("yyyy-MM-dd") + "'");
                    if (dr.Length > 0)
                    {
                        check = true;
                    }
                }
            }
            catch (Exception ex)
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

        private void CreateCellControls(TableCell cell, CalendarDay day)
        {
            try
            {
                web = SPContext.Current.Site.OpenWeb(webURL);
                SPList calendarList = web.Lists[listName];
                string MenuDivinnerHtml = "";

                SPQuery query = new SPQuery();
                query.RecurrenceOrderBy = true;
                query.ExpandRecurrence = true;

                query.CalendarDate = new DateTime(day.Date.Year, day.Date.Month, day.Date.Day);

                query.Query =
                    "<Where>"
                        + "<DateRangesOverlap>"
                        + "<FieldRef Name=\"EventDate\" />"
                        + "<FieldRef Name=\"EndDate\" />"
                        + "<FieldRef Name=\"RecurrenceID\" />"
                        + "<Value Type=\"DateTime\">"
                        + "<Today/>"
                        + "</Value>"
                        + "</DateRangesOverlap>"
                    + "</Where>";

                SPListItemCollection calendarItems = calendarList.GetItems(query);
                MenuDivinnerHtml = @"
                <div id='" + day.Date.ToString("yyyy-MM-dd") + @"' curDivDate='" + day.Date.ToString("yyyy-MM-dd") + @"' width='100%' border='0' cellpadding='0' cellspacing='0' style='display:none;'>
                    <table width='100%' border='0' cellpadding='0' cellspacing='0'>
                        <tr>
                            <td>
                                <table width='100%' border='0' cellpadding='0' cellspacing='0'>
                                    <tr> 
                                        <td style='font-size:0' height='24' width='23'><img src='" + imgUrl + @"date_m_9.jpg' width='23' height='24'></td>
                                        <td bgcolor='e5e5e5' style='font-size:12px;text-align:center; color:#3a3a3a; font-weight:bold;'>" + day.Date.ToString("yyyy-MM-dd") + @"<div style='cursor:pointer; float:right; display:none;' onclick='javascript:" + this.Parent.ClientID + @"_defaultCalendar();'>×</div></td>
                                        <td style='font-size:0' height='24' width='23'><img src='" + imgUrl + @"date_m_10.jpg' width='23' height='24'></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width='100%' border='0' cellpadding='0' cellspacing='0' style='margin-left: 4%'>
                                    <tr style='display: none'> 
                                        <td colspan='2' style='text-align:left; font-size:12px; color:#d0101a; font-weight:bold; top:0; left:0; padding-top:10px; padding-bottom:10px;padding-left:5px;'><img src='" + imgUrl + @"date_m_icon1.jpg' width='8' height='11'>전사</td>
                                    </tr>
            ";

                MenuDivinnerHtml = ScheduleTR(day, calendarList, MenuDivinnerHtml, calendarItems);// <tr>태그 메서드

                MenuDivinnerHtml += @"
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            ";

                string strDate;
                string senderDate = day.Date.ToString("yyyy-MM-dd");

                if (calendarItems.Count < 1)
                    strDate = "<table width='100%' height='100%' style='font-size:12px; cursor:pointer;' curDivDate='" + senderDate + "' onclick=\"javascript:" + this.Parent.ClientID + "_scheduleBar('" + senderDate + "');\"><tr><td  align='center' valign='middle' curDivDate='" + senderDate + "' style='padding:0 0 0 0;'>" + day.DayNumberText + "</td></tr></table>";
                else
                    strDate = "<table width='100%' height='100%' style='font-size:12px; cursor:pointer;font-weight:bold;' curDivDate='" + senderDate + "' onclick=\"javascript:" + this.Parent.ClientID + "_scheduleBar('" + senderDate + "');\"><tr><td  align='center' valign='middle' curDivDate='" + senderDate + "' style='padding:0 0 0 0;'>" + day.DayNumberText + "</td></tr></table>";

                cell.Controls.Add(new LiteralControl(MenuDivinnerHtml));
                cell.Controls.Add(new LiteralControl(strDate));
            }
            catch (Exception ex)
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
        }

        private string ScheduleTR(CalendarDay day, SPList calendarList, string MenuDivinnerHtml, SPListItemCollection calendarItems)
        {
            foreach (SPListItem item in calendarItems)
            {
                DateTime startDate = Convert.ToDateTime(item["EventDate"]);
                DateTime endDate = Convert.ToDateTime(item["EndDate"]);
                string periodEvent = string.Empty;

                MenuDivinnerHtml += "<tr style='text-align:left; font-size:12px; color:#6a6969; top:0; left:0; padding-top:0px; padding-left:5px;  padding-bottom:10px;letter-spacing:1pt;line-height:150%;'>";

                if (Convert.ToBoolean(item["fAllDayEvent"])) //하루 종일 일정
                {
                    if (Convert.ToDateTime(item["EndDate"]) >= day.Date)
                    {
                        if (startDate.Date == endDate.Date)
                            periodEvent = "AllDay";// BizUtility.GetLocalizedString("AllDay");
                        else
                        {
                            if (startDate.Date.Subtract(day.Date).Days == 0)
                                periodEvent = "AllDay"/*BizUtility.GetLocalizedString("AllDay")*/ + " →";
                            else if (startDate.Date.Subtract(day.Date).Days < 0 && endDate.Date.Subtract(day.Date).Days > 0)
                                periodEvent = "← " + "AllDay"/*BizUtility.GetLocalizedString("AllDay")*/ + " →";
                            else
                                periodEvent = "← " + "AllDay"/*BizUtility.GetLocalizedString("AllDay")*/;
                        }
                        MenuDivinnerHtml += "<td>" + periodEvent + "</td>";
                        MenuDivinnerHtml += "</tr><tr><td align='right'><a href=\"javascript:" + this.Parent.ClientID + "_openDialog('" + SPEncode.HtmlEncode(calendarList.RootFolder.ServerRelativeUrl.ToString()) + "/DispForm.aspx?ID=" + item["ID"].ToString() + "')\">" + item["Title"].ToString() + "</a>&nbsp;&nbsp;&nbsp;</td>";
                    }
                }
                else //시간 일정
                {
                    if (startDate.Date == endDate.Date)
                        periodEvent = startDate.ToString("hh:mm") + " ~ " + endDate.ToString("hh:mm");
                    else
                    {
                        if (startDate.Date.Subtract(day.Date).Days == 0)
                            periodEvent = startDate.ToString("hh:mm") + " ~ " + "→";
                        else if (startDate.Date.Subtract(day.Date).Days < 0 && endDate.Date.Subtract(day.Date).Days > 0)
                            periodEvent = "←" + " ~ " + "→";
                        else
                            periodEvent = "←" + " ~ " + endDate.ToString("hh:mm");
                    }
                    MenuDivinnerHtml += "<td>" + periodEvent + "</td>";
                    MenuDivinnerHtml += "</tr><tr><td align='right'><a href=\"javascript:" + this.Parent.ClientID + "_openDialog('" + SPEncode.HtmlEncode(calendarList.RootFolder.ServerRelativeUrl.ToString()) + "/DispForm.aspx?ID=" + item["ID"].ToString() + "')\">" + item["Title"].ToString() + "</a>&nbsp;&nbsp;&nbsp;</td>";
                }

                MenuDivinnerHtml += "</tr>";
            }

            if (calendarItems.Count < 1)
                MenuDivinnerHtml += "";//"<tr><td colspan='2' rowspan='2'> - " + "NoEvents"/*BizUtility.GetLocalizedString("NoEvents")*/ + " - </td></tr>";
            return MenuDivinnerHtml;
        }

    }
}
