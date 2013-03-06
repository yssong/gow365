using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace GOW365.DropDownLinkWebpart
{
    [ToolboxItemAttribute(false)]
    public class DropDownLinkWebpart : WebPart
    {
        private string layoutUrl = string.Empty;
        private string webName = string.Empty;
        private string listName = string.Empty;
        private string url = string.Empty;

        DropDownList ddlLink;
        SPList ConnectedList;
        bool checkvalue = false;
        private string controlWidth = "210";
        //배포 전에 ImgUrl을 수정해주세요.
        private string ImgUrl = "/GOW365/DropDownLink/";

        #region Properties

        [WebBrowsable(true),
         Personalizable(PersonalizationScope.Shared),
         DefaultValue("false"),
         Category("Control Width"),
         WebDisplayName("DropDown Width"),
         WebDescription("DropDown Control Width")]
        public string ControlWidth
        {
            get
            {
                return controlWidth;
            }
            set
            {
                controlWidth = value;
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

        protected override void OnPreRender(EventArgs e)
        {
            DropDownListDataBind();
        }

        protected override void CreateChildControls()
        {
            ImgUrl = SPContext.Current.Site.ServerRelativeUrl + ImgUrl;

            this.ddlLink = new DropDownList();
            this.ddlLink.ID = this.ddlLink.ClientID;
            this.ddlLink.AutoPostBack = false;
            this.ddlLink.Attributes["onchange"] = "if(this.options[this.selectedIndex].value != ''){var pop = window.open(this.options[this.selectedIndex].value,'',''); if(pop)window.focus();else{    var timer = window.setTimeout( function(){ if(pop)win.focus(); }, 100 );};this.selectedIndex = 0; return false;}";
            this.ddlLink.Attributes["style"] = "width:" + Convert.ToInt32(ControlWidth) + "px; vertical-align:middle; border:#DEDEDE 2px solid;";
            this.Controls.Add(this.ddlLink);
        }

        private void DropDownListDataBind()
        {
            ddlLink.Items.Clear();

            if (ListName != string.Empty)
            {
                try
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

                    SPWeb web = SPContext.Current.Site.OpenWeb(WebName);
                    ConnectedList = web.Lists[ListName];

                    if (this.ConnectedList != null)
                    {
                        string btype = ConnectedList.BaseTemplate.ToString();
                        if ( btype== SPListTemplateType.Links.ToString())
                        {
                            this.ListName = ConnectedList.Title;

                            checkvalue = true;
                            SPListItemCollection listitemcoll = ConnectedList.Items;
                            ddlLink.Items.Add(new ListItem("  :: " + "SiteList", ""));

                            foreach (SPListItem li in listitemcoll)
                            {
                                //배포 전에 해당 항목이 있는지 확인해주세요. Spirax의 커스텀 링크 리스트에 쓰기 위해 수정한 부분입니다.
                                ddlLink.Items.Add(new ListItem(li["Title"].ToString(), li["URL"].ToString().Split(',')[0]));
                                //ddlLink.Items.Add(new ListItem(li["URL"].ToString().Split(',')[1], li["URL"].ToString().Split(',')[0]));
                            }
                        }
                        else if(btype=="170")
                        {
                            this.ListName = ConnectedList.Title;

                            checkvalue = true;
                            SPListItemCollection listitemcoll = ConnectedList.Items;
                            ddlLink.Items.Add(new ListItem("  :: " + "SiteList", ""));

                            foreach (SPListItem li in listitemcoll)
                            {
                                //배포 전에 해당 항목이 있는지 확인해주세요. Spirax의 커스텀 링크 리스트에 쓰기 위해 수정한 부분입니다.
                                ddlLink.Items.Add(new ListItem(li["Title"].ToString(), li["LinkLocation"].ToString().Split(',')[0]));
                                //ddlLink.Items.Add(new ListItem(li["URL"].ToString().Split(',')[1], li["URL"].ToString().Split(',')[0]));
                            }
                            
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {

            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (checkvalue)
            {
                writer.WriteLine("<table cellspacing='0' cellpadding='0' style='padding:0 0 0 0;'>");
                writer.WriteLine("<tr>");
                writer.WriteLine("<td align='left' style='padding:0 0 0 0;'>");
                ddlLink.RenderControl(writer);
                writer.WriteLine("</td>");
                writer.WriteLine("</tr>");
                writer.WriteLine("</table>");

            }
            else
            {
                writer.WriteLine("<table style='padding:0 0 0 0;'>");
                writer.WriteLine("<tr>");
                writer.WriteLine("<td valign='middle' align='center' style='padding:0 0 0 0;'>");
                if (ConnectedList == null)
                {
                    writer.WriteLine("NoConfiguration");
                }
                else
                {
                    writer.WriteLine("NolinkTypeList");
                }

                writer.WriteLine("</td>");
                writer.WriteLine("</tr>");
                writer.WriteLine("</table>");

            }
        }
    }
}
