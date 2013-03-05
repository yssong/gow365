using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data;

namespace GOW365.TabListWebpart
{
    [ToolboxItemAttribute(false)]
    public class TabListWebpart : WebPart
    {
        private HtmlGenericControl TabWebPartCSS = null;

        //배포 전에 ImgUrl을 수정해주세요. 
        private string ImgUrl = "/GOW365/TabList/";
        private HtmlInputHidden hhdList;
        private HtmlInputHidden hdtxt;
        private HtmlInputHidden hhdSelectedSite;
        private Label lblWrongSiteUrl;
        private string moreInfo;

        private static SPWeb web = null;
        private static SPList list = null;

        #region Properties

        private string webName1 = string.Empty;
        private string listName1 = string.Empty;
        private string webName2 = string.Empty;
        private string listName2 = string.Empty;
        private string webName3 = string.Empty;
        private string listName3 = string.Empty;
        private int listItemCount = 5;

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

        public TabListWebpart()
        {
            //this.ExportMode = WebPartExportMode.All;
            //ImgUrl = SPContext.Current.Site.Url + ImgUrl;
        }

        protected override void OnPreRender(EventArgs e)
        {
            try
            {
                if (ListName1 != string.Empty)
                {
                    web = SPContext.Current.Site.OpenWeb(WebName1);
                    list = web.Lists[ListName1];

                    moreInfo = list.RootFolder.ServerRelativeUrl;
                    hhdSelectedSite.Value += list.RootFolder.ServerRelativeUrl + ";";
                    if (web != null) { web.Close(); web.Dispose(); }
                }
                if (ListName2 != string.Empty)
                {
                    web = SPContext.Current.Site.OpenWeb(WebName2);
                    list = web.Lists[ListName2];

                    hhdSelectedSite.Value += list.RootFolder.ServerRelativeUrl + ";";
                    if (web != null) { web.Close(); web.Dispose(); }
                }
                if (ListName3 != string.Empty)
                {
                    web = SPContext.Current.Site.OpenWeb(WebName3);
                    list = web.Lists[ListName3];

                    hhdSelectedSite.Value += list.RootFolder.ServerRelativeUrl + ";";
                    if (web != null) { web.Close(); web.Dispose(); }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected override void CreateChildControls()
        {
            this.hhdSelectedSite = new HtmlInputHidden();
            this.hhdSelectedSite.ID = this.hhdSelectedSite.ClientID;
            this.hhdSelectedSite.Value = string.Empty;
            this.Controls.Add(hhdSelectedSite);

            hhdList = new HtmlInputHidden();
            hhdList.Attributes["id"] = "hhdList_" + this.ClientID;
            this.Controls.Add(this.hhdList);

            hdtxt = new HtmlInputHidden();
            hdtxt.Attributes["id"] = "hdtxt_" + this.ClientID;
            this.Controls.Add(this.hdtxt);

            lblWrongSiteUrl = new Label();
            lblWrongSiteUrl.Text = "Check WebName or ListName.";
            this.Controls.Add(lblWrongSiteUrl);

            base.CreateChildControls();
        }

        public bool webCheck(string wName, string lName)
        {
            bool check = false;
            try
            {
                web = SPContext.Current.Site.OpenWeb(wName);
                if (lName == string.Empty)
                { check = true; }
                else
                {
                    SPList list = web.Lists[lName];
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

        protected override void Render(HtmlTextWriter writer)
        {

            hhdSelectedSite.RenderControl(writer);
            hhdList.RenderControl(writer);
            hdtxt.RenderControl(writer);

            string _hd_ID = hdtxt.Attributes["id"].ToString();
            string _hhdList_ID = hhdList.Attributes["id"].ToString();

            if (!webCheck(WebName1, ListName1) || !webCheck(WebName2, ListName2) || !webCheck(WebName3, ListName3))
            {
                this.RenderCSS(writer);
                this.RenderMainTable(writer);
                this.RenderScript(writer);
            }
            else
            {
                this.lblWrongSiteUrl.RenderControl(writer);
            }
        }

        private void RenderMainTable(HtmlTextWriter writer)
        {
            writer.WriteLine(@"
                            <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                <tr>");
            List<ListItem> tabs = new List<ListItem>();
            if (ListName1 != "")
            {
                tabs.Add(new ListItem(WebName1, ListName1));
            }
            if (ListName2 != "")
            {
                tabs.Add(new ListItem(WebName2, ListName2));
            }
            if (ListName3 != "")
            {
                tabs.Add(new ListItem(WebName3, ListName3));
            }

            for (int i = 0; i < tabs.Count; i++)
            {
                string textStyle;
                string imgTapOver;
                if (i == 0)
                {
                    textStyle = "selectedNewTab";
                    imgTapOver = "Selected_tab.gif";
                }
                else
                {
                    textStyle = "unselectedNewTab";
                    imgTapOver = "Unselected_tab.gif";
                }

                writer.WriteLine(@"<td width='100' height='31'>
                                        <table width='100%' onclick='" + string.Format("javascript:{0}_table({1})", this.ClientID, i) + @"' cellspacing='0' cellpadding='0' style='cursor:pointer'>
                                            <tr>
                                                <td id='" + this.ClientID + "_iconTD" + i + "' width='100%' height='31' align='center' background='" + ImgUrl + imgTapOver + "' class='" + textStyle + @"' style='padding-top: 8px;background-repeat: no-repeat; background-position:bottom'>
                                                    " + tabs[i].Value + @"
                                                    <img id='" + this.ClientID + "_iconIMG" + i + "' name='" + this.UniqueID + "$Image" + i + @"' src='" + ImgUrl + imgTapOver + @"' style='display: none' />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>");
            }
            writer.WriteLine("      <td height='31' background='" + ImgUrl + @"section_center.gif' style='background-repeat: repeat-x; background-position:bottom'>");
            writer.WriteLine("          <p align='right'><img id='" + this.ClientID + "_morebutton' src='" + ImgUrl + "more_button.gif' width='31' height='14' style='cursor:pointer' linkinfo='" + moreInfo + "' onclick='javascript:" + this.ClientID + "_GoMoreList(this);' /></p>");
            writer.WriteLine("      </td>");
            writer.WriteLine("  </tr>");
            writer.WriteLine("</table>");

            writer.WriteLine(@"
                <table width='100%' border='0' cellspacing='0' cellpadding='0'>
            	    <tr>
                        <td>
            			    <div id='" + this.ClientID + "_table' style='width:100%;height:100%'>");
            SPWeb web = null;
            int divcnt = 0;

            foreach (ListItem li in tabs)
            {
                try
                {
                    web = SPContext.Current.Site.OpenWeb(li.Text);
                    if (divcnt == 0)
                    {
                        writer.WriteLine("<div id='" + this.ClientID + "_content_" + divcnt.ToString() + @"'>");
                        divcnt++;
                    }
                    else
                    {
                        writer.WriteLine("<div id='" + this.ClientID + "_content_" + divcnt.ToString() + @"' style='display:none'>");
                        divcnt++;
                    }

                    writer.WriteLine(@"
                        <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                            <tr>
                                <td width='4' background='" + ImgUrl + @"section_centerleft.gif' style='background-repeat: repeat-y;'></td>
                                <td>");
                    if (li.Value != string.Empty)
                    {
                        writer.WriteLine(GetListHtml(li.Text, li.Value, ListItemCount).ToString());
                    }
                    writer.WriteLine(@"
                                </td>
                                <td width='4' background='" + ImgUrl + @"section_centerright.gif' style='background-repeat: repeat-y;'></td>
                            </tr>

                            <tr>
                                <td width='4' height='4' background='" + ImgUrl + @"section_bottomleft.gif' style='background-repeat: no-repeat; background-position:bottom'></td>
                                <td height='4' background='" + ImgUrl + @"section_bottomcenter.gif' style='background-repeat: repeat-x; background-position:bottom'></td>
                                <td width='4' height='4' background='" + ImgUrl + @"section_bottomright.gif' style='background-repeat: no-repeat; background-position:bottom'></td>
                            </tr>
                        </table>");
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
                    writer.WriteLine(@"
                                </div>");
                }
            }

            writer.WriteLine(@"
                            </div>
                        </td>
                    </tr>
                </table>");
        }

        public string GetListHtml(string wName, string lName, int iListCount)
        {
            string returnValue = string.Empty;

            try
            {
                web = SPContext.Current.Site.OpenWeb(wName);
                list = web.Lists[lName];

                SPQuery query = new SPQuery();
                DataTable dt = new DataTable();

                query.ViewAttributes = "Scope=\"Recursive\"";
                query.IncludeAllUserPermissions = true;
                string sQuery = "<Where><Neq><FieldRef Name='ContentType' /><Value Type='Text'>Folder</Value> </Neq></Where>";
                query.RowLimit = (uint)iListCount;
                query.Query = sQuery;

                if (list.BaseTemplate.ToString() != SPListTemplateType.DiscussionBoard.ToString())
                {
                    SPListItemCollection itemColl = list.GetItems(query);
                    dt = itemColl.GetDataTable();
                }
                else
                {
                    SPListItemCollection folderColl = list.Folders;
                    dt = folderColl.GetDataTable();
                }

                returnValue = "";
                returnValue += "<table width='100%' border='0' cellspacing='0' cellpadding='0'>";
                returnValue += "  <tr>";
                returnValue += "    <td  width='10'>&nbsp;</td>";
                returnValue += "    <td  width='70%' class='title_text'>" + "Title"/*BizUtility.GetLocalizedString("_Title")*/ + "</td>";
                returnValue += "    <td  width='15%' class='title_text'>" + "Createdby"/*BizUtility.GetLocalizedString("_Createdby")*/ + "</td>";
                returnValue += "    <td  width='13%' class='title_text'>" + "Created"/*BizUtility.GetLocalizedString("_Created")*/ + "</td>";
                returnValue += "  </tr>";

                DataRow[] foundRows = (dt == null ? null : dt.Select("", "Created DESC"));

                for (int i = 0; i < iListCount; i++)
                {
                    if (foundRows != null && foundRows.Length > i)
                    {
                        if (list.BaseType == SPBaseType.DocumentLibrary)
                        {
                            returnValue += "<tr>";

                            returnValue += "<td  width='10' height='20' background='" + ImgUrl + "section_dotline.gif' style='background-repeat: repeat-x;background-position: bottom'>";
                            returnValue += "<img src='" + ImgUrl + "section_icon.gif' width='3' height='5' />";
                            returnValue += "</td>";
                            returnValue += "<td  width='70%' background='" + ImgUrl + "section_dotline.gif' class='basic_text' style='background-repeat: repeat-x;background-position: bottom'>";
                            returnValue += "<a href=\"#\" onclick=\"javascript:openDialog('" + list.DefaultDisplayFormUrl + "?ID=" + foundRows[i]["ID"] + "'); return false;\">";
                            if (foundRows[i]["Title"].ToString() != string.Empty)
                            {
                                returnValue += foundRows[i]["Title"].ToString();
                            }
                            else
                            {
                                returnValue += foundRows[i]["FileLeafRef"].ToString().Split('.')[0];
                            }
                            returnValue += "</a>";
                            returnValue += "</td>";
                            returnValue += "<td  width='15%' background='" + ImgUrl + "section_dotline.gif' class='basic_text' style='background-repeat: repeat-x;background-position: bottom'>";
                            returnValue += foundRows[i]["Author"].ToString();
                            returnValue += "</td>";
                            returnValue += "<td  width='13%' background='" + ImgUrl + "section_dotline.gif' class='basic_text' style='background-repeat: repeat-x;background-position: bottom'>";
                            returnValue += Convert.ToDateTime(foundRows[i]["Created"].ToString()).ToShortDateString();
                            returnValue += "</td>";

                            returnValue += "</tr>";
                        }
                        else if (list.BaseTemplate.ToString() == SPListTemplateType.Links.ToString())
                        {
                            returnValue += "<tr>";

                            returnValue += "<td  width='10' height='20' background='" + ImgUrl + "section_dotline.gif' style='background-repeat: repeat-x;background-position: bottom'>";
                            returnValue += "<img src='" + ImgUrl + "section_icon.gif' width='3' height='5' />";
                            returnValue += "</td>";
                            returnValue += "<td  width='70%' background='" + ImgUrl + "section_dotline.gif' class='basic_text' style='background-repeat: repeat-x;background-position: bottom'>";
                            returnValue += "<a href=\"#\" onclick=\"javascript:openDialog('" + list.DefaultDisplayFormUrl + "?ID=" + foundRows[i]["ID"] + "'); return false;\">";
                            returnValue += foundRows[i]["URL"].ToString().Split(',')[1];
                            returnValue += "</a>";
                            returnValue += "</td>";
                            returnValue += "<td  width='15%' background='" + ImgUrl + "section_dotline.gif' class='basic_text' style='background-repeat: repeat-x;background-position: bottom'>";
                            returnValue += foundRows[i]["Author"].ToString();
                            returnValue += "</td>";
                            returnValue += "<td  width='13%' background='" + ImgUrl + "section_dotline.gif' class='basic_text' style='background-repeat: repeat-x;background-position: bottom'>";
                            returnValue += Convert.ToDateTime(foundRows[i]["Created"].ToString()).ToShortDateString();
                            returnValue += "</td>";

                            returnValue += "</tr>";
                        }
                        else if (list.BaseTemplate.ToString() == SPListTemplateType.DiscussionBoard.ToString())
                        {
                            returnValue += "<tr>";

                            returnValue += "<td  width='10' height='20' background='" + ImgUrl + "section_dotline.gif' style='background-repeat: repeat-x;background-position: bottom'>";
                            returnValue += "<img src='" + ImgUrl + "section_icon.gif' width='3' height='5' />";
                            returnValue += "</td>";
                            returnValue += "<td  width='70%' background='" + ImgUrl + "section_dotline.gif' class='basic_text' style='background-repeat: repeat-x;background-position: bottom'>";
                            returnValue += "<a href=\"#\" onclick=\"javascript:openDialog('" + list.DefaultDisplayFormUrl + "?ID=" + foundRows[i]["ID"] + "'); return false;\">";
                            returnValue += foundRows[i]["Title"].ToString() + "(" + foundRows[i]["ItemChildCount"] + ")";
                            returnValue += "</a>";
                            returnValue += "</td>";
                            returnValue += "<td  width='15%' background='" + ImgUrl + "section_dotline.gif' class='basic_text' style='background-repeat: repeat-x;background-position: bottom'>";
                            returnValue += foundRows[i]["Author"].ToString();
                            returnValue += "</td>";
                            returnValue += "<td  width='13%' background='" + ImgUrl + "section_dotline.gif' class='basic_text' style='background-repeat: repeat-x;background-position: bottom'>";
                            returnValue += Convert.ToDateTime(foundRows[i]["Created"].ToString()).ToShortDateString();
                            returnValue += "</td>";

                            returnValue += "</tr>";
                        }
                        else
                        {
                            returnValue += "<tr>";

                            returnValue += "<td  width='10' height='20' background='" + ImgUrl + "section_dotline.gif' style='background-repeat: repeat-x;background-position: bottom'>";
                            returnValue += "<img src='" + ImgUrl + "section_icon.gif' width='3' height='5' />";
                            returnValue += "</td>";
                            returnValue += "<td  width='70%' background='" + ImgUrl + "section_dotline.gif' class='basic_text' style='background-repeat: repeat-x;background-position: bottom'>";
                            returnValue += "<a href=\"#\" onclick=\"javascript:openDialog('" + list.DefaultDisplayFormUrl + "?ID=" + foundRows[i]["ID"] + "&IsDlg=1'); return false;\">";
                            returnValue += foundRows[i]["Title"].ToString();
                            returnValue += "</a>";
                            returnValue += "</td>";
                            returnValue += "<td  width='15%' background='" + ImgUrl + "section_dotline.gif' class='basic_text' style='background-repeat: repeat-x;background-position: bottom'>";
                            returnValue += foundRows[i]["Author"].ToString();
                            returnValue += "</td>";
                            returnValue += "<td  width='13%' background='" + ImgUrl + "section_dotline.gif' class='basic_text' style='background-repeat: repeat-x;background-position: bottom'>";
                            returnValue += Convert.ToDateTime(foundRows[i]["Created"].ToString()).ToShortDateString();
                            returnValue += "</td>";

                            returnValue += "</tr>";
                        }
                    }
                    else
                    {
                        returnValue += "<tr>";
                        returnValue += "<td colspan='4' height='20'  background='" + ImgUrl + "section_dotline.gif' class='basic_text' style='background-repeat: repeat-x;background-position: bottom'>&nbsp;</td>";
                        returnValue += "</tr>";
                    }
                }

                returnValue += "</table>";
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

            return returnValue;
        }

        private void RenderScript(HtmlTextWriter writer)
        {
            writer.Write(@"
                <script language='JavaScript'>
                                var " + this.ClientID + @"_selected_iconTD  = document.getElementById('" + this.ClientID + @"_iconTD0'); 
                                
                                var " + this.ClientID + @"_selectedSite = document.getElementById('" + this.hhdSelectedSite.ClientID + @"').value;
                                var " + this.ClientID + @"_moreButton = document.getElementById('" + this.ClientID + @"_morebutton');
                                " + this.ClientID + @"_moreButton.linkinfo = " + this.ClientID + @"_selectedSite.split(';')[0];

                                function openDialog(_url) {  
                                    var options = {  url: _url ,  width: 800, height: 600, };  
                                    SP.UI.ModalDialog.showModalDialog(options);  
                                }

                                function " + this.ClientID + @"_table(nSelectedTab) {
                                    var table = document.getElementById('" + this.ClientID + @"_table');

                                    " + this.ClientID + @"_moreButton.linkinfo = " + this.ClientID + @"_selectedSite.split(';')[nSelectedTab];
                                    
                                    
                                    try
                                    {
                                        for(var i=0;i<3;i++) {
                                            var fimg    = document.getElementById('" + this.ClientID + @"_fimg'+i);
                                            var iconTD  = document.getElementById('" + this.ClientID + @"_iconTD'+i); 
                                            var iconIMG = document.getElementById('" + this.ClientID + @"_iconIMG'+i); 
                                            var bimg    = document.getElementById('" + this.ClientID + @"_bimg'+i);
                                            var Content = document.getElementById('" + this.ClientID + @"_content_'+i);
                            
                                            if(i !=  nSelectedTab) {
                                                iconTD.className = 'unselectedNewTab';
                                                iconTD.background = '" + ImgUrl + @"Unselected_tab.gif';
                                                iconIMG.src = '" + ImgUrl + @"@Unselected_tab.gif';
                                                Content.style.display='none';
                                            } else {
                                                " + this.ClientID + @"_selected_iconTD = iconTD;
                                                iconTD.className = 'selectedNewTab';
                                                iconTD.background = '" + ImgUrl + @"Selected_tab.gif';
                                                iconIMG.src = '" + ImgUrl + @"Selected_tab.gif';
                                                Content.style.display='';
                                            }
                                        }                            
                                    }
                                    catch(e)
                                    {
                                    }                          
                                }

                                function " + this.ClientID + @"_GoMoreList(sender) {
                                    if (sender.linkinfo != '') { 
                                        if (sender.linkinfo != undefined) {
                                            window.open(sender.linkinfo);
                                        }
                                    }
                                }

                                function " + this.ClientID + @"_GetInfo(url) { 
                                    var xmlRequest = DoCallback(url); 
                                    return xmlRequest.responseText; 
                                } 

                                function " + this.ClientID + @"_DoCallback(pageUrl) { 
                                    var xmlRequest = new ActiveXObject('Microsoft.XMLHTTP'); 
                                    xmlRequest.Open('POST', pageUrl, false); 
                                    xmlRequest.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
                                    xmlRequest.Send(null); 
                                    return xmlRequest; 
                                }     

                            </script>");
        }

        private void RenderCSS(HtmlTextWriter writer)
        {
            writer.WriteLine("<style type='text/css'>");
            writer.WriteLine(".title_text {");
            writer.WriteLine("  padding-top:4px;");
            writer.WriteLine("  padding-bottom:4px;");
            writer.WriteLine("	font-family: '돋움';");
            writer.WriteLine("	font-size: 12px;");
            writer.WriteLine("  color: #0a56a3; ");
            writer.WriteLine("}");
            writer.WriteLine(".basic_text {");
            writer.WriteLine("	font-family: '돋움';");
            writer.WriteLine("	font-size: 12px;");
            writer.WriteLine("	color: #333333;");
            writer.WriteLine("}");

            writer.WriteLine(".unselectedNewTab {");
            writer.WriteLine("	background-image: url('" + ImgUrl + "Unselected_tab.gif');");
            writer.WriteLine("}");
            writer.WriteLine(".selectedNewTab {");
            writer.WriteLine("	background-image: url('" + ImgUrl + "Selected_tab.gif');");
            writer.WriteLine("}");

            writer.WriteLine(".basic_text A:link { text-decoration:none; color:333333}");
            writer.WriteLine(".basic_text A:visited { text-decoration:none; color:333333}");
            writer.WriteLine(".basic_text A:active { text-decoration:none; color:333333}");
            writer.WriteLine(".basic_text A:hover {text-decoration:none; color:#333333}");

            writer.WriteLine("</style>");
        }

    }
}
