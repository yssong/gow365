using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace GOW365.SiteNewDocsWebPart
{
    [ToolboxItemAttribute(false)]
    public class SiteNewDocsWebPart : WebPart
    {
        private DataGrid grid;

        private string webName = string.Empty;
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
        protected override void CreateChildControls()
        {
            grid = new DataGrid();
            grid.Width = Unit.Percentage(100);
            grid.GridLines = GridLines.Horizontal;
            grid.HeaderStyle.CssClass = "ms-vh2";
            grid.CellPadding = 2;
            grid.BorderWidth = Unit.Pixel(5);
            grid.HeaderStyle.Font.Bold = true;
            grid.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

            if (webName == "") webName = SPContext.Current.Web.Url;

            if (!webName.StartsWith("http"))
            {
                if (SPContext.Current.Site.ServerRelativeUrl == "/")
                {
                    webName = SPContext.Current.Site.Url + webName;
                }
                else
                {
                    webName = SPContext.Current.Site.Url.Replace(SPContext.Current.Site.ServerRelativeUrl, "") + webName;
                }

                
            }


            using (SPSite site = new SPSite(webName))
            {
                try
                {
                    string weburl = webName.Replace(site.RootWeb.Url,"");
                    weburl = (weburl.StartsWith("") ? weburl : "/" + weburl);
                    using (SPWeb web = site.OpenWeb(weburl))
                    {
                        SPSiteDataQuery qry = new SPSiteDataQuery();

                        qry.Query = @"<Where>"+
                            "<Or><Or><Or><Or><Or><Or><Or><Or>"+
                            "<Contains><FieldRef Name='FileLeafRef' /><Value Type='File'>.jpg</Value></Contains>"+
                            "<Contains><FieldRef Name='FileLeafRef' /><Value Type='File'>.gif</Value></Contains>" +
                            "</Or>" +
                            "<Contains><FieldRef Name='FileLeafRef' /><Value Type='File'>.doc</Value></Contains>" +
                            "</Or>" +
                            "<Contains><FieldRef Name='FileLeafRef' /><Value Type='File'>.xls</Value></Contains>" +
                            "</Or>" +
                            "<Contains><FieldRef Name='FileLeafRef' /><Value Type='File'>.ppt</Value></Contains>" +
                            "</Or>" +
                            "<Contains><FieldRef Name='FileLeafRef' /><Value Type='File'>.vsd</Value></Contains>" +
                            "</Or>" +
                            "<Contains><FieldRef Name='FileLeafRef' /><Value Type='File'>.vsd</Value></Contains>" +
                            "</Or>" +
                            "<Contains><FieldRef Name='FileLeafRef' /><Value Type='File'>.pdf</Value></Contains>" +
                            "</Or>" +
                            "<Contains><FieldRef Name='FileLeafRef' /><Value Type='File'>.hwp</Value></Contains>" +
                            "</Or>" +
                            "</Where>"+
                            "<OrderBy><FieldRef Name='Modified' Ascending='FALSE' /></OrderBy>";

                        qry.Lists = "<Lists ServerTemplate='101'/>";

                        qry.ViewFields = "<FieldRef Name='Created' /><FieldRef Name='Created' /><FieldRef Name='Modified' /><FieldRef Name='Author' /><FieldRef Name='Editor' /><FieldRef Name='LinkFilenameNoMenu' /><FieldRef Name='FileRef' />";
                        qry.Webs = "<Webs Scope='Recursive'/>";
                        
                        DataTable resultTable = web.GetSiteData(qry);

                        grid.DataSource = resultTable;
                        grid.DataBind();
                        //writer.Write("");
                    }
                }
                catch (Exception ex)
                {
                    //writer.Write(ex.Message);
                }
            }


            Controls.Add(grid);
            base.CreateChildControls();


        }
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            
        }
    }
}
