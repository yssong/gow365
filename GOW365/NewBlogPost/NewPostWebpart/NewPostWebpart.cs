using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;

namespace GOW365.NewPostWebpart
{
    [ToolboxItemAttribute(false)]
    public class NewPostWebpart : WebPart
    {
        private string ImgUrl = "GOW365/SiteNewDocsTab/";

        private int itemCount = 5;
        [Personalizable(PersonalizationScope.Shared),
       WebBrowsable(true),
       Category("List Item Count"),
       WebDisplayName("List Item Count"),
       WebDescription("List Item Count")]
        public int ItemCount
        {
            get { return itemCount; }
            set { itemCount = value; }
        }

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
            ImgUrl = (SPContext.Current.Site.ServerRelativeUrl.EndsWith("/") ? SPContext.Current.Site.ServerRelativeUrl + ImgUrl : SPContext.Current.Site.ServerRelativeUrl + "/" + ImgUrl);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            /**
               <QueryOptions>
      <Lists ServerTemplate='301' />
      <Webs Scope='Recursive' />
   </QueryOptions>
   <ViewFields>
      <FieldRef Name='Title' />
      <FieldRef Name='ID' />
      <FieldRef Name='PublishedDate' />
      <FieldRef Name='Author' />
      <FieldRef Name='FileRef' />
   </ViewFields>
             **/
            
            
            try
            {
                string weburl = webName.Replace(SPContext.Current.Site.RootWeb.Url, "");
                weburl = (weburl.StartsWith("") ? weburl : "/" + weburl);
                using (SPWeb web = SPContext.Current.Site.OpenWeb(weburl))
                {
                    SPSiteDataQuery qry = new SPSiteDataQuery();

                    qry.Query = @"<OrderBy><FieldRef Name='PublishedDate' Ascending='FALSE' /></OrderBy>";

                    qry.Lists = "<Lists ServerTemplate='301'/>";
                    qry.RowLimit = (uint)itemCount;
                    qry.ViewFields = "<FieldRef Name='Title' /><FieldRef Name='ID' /><FieldRef Name='PublishedDate' /><FieldRef Name='Author' /><FieldRef Name='FileRef' />";
                    qry.Webs = "<Webs Scope='Recursive'/>";

                    DataTable resultTable = web.GetSiteData(qry);

                    string strPost = "";
                    strPost += "<div id='" + this.ClientID + "_post'><ul>";
                    foreach (DataRow item in resultTable.Rows)
                    {

                        if (item != null)
                        {
                            strPost += "<li><span class='postTitle'><a href=\"" + "/" + item["FileRef"].ToString().Split('#')[1].Substring(0, item["FileRef"].ToString().Split('#')[1].LastIndexOf('/'))+"/post.aspx?ID="+item["ID"].ToString() + "\" target='_new'>";
                            strPost += item["Title"].ToString();
                            strPost += "</a>";
                            strPost += "</span><span class='postName'>" + item["Author"].ToString().Split('#')[1] + "</span><span  class='postDate'>" + Convert.ToDateTime(item["PublishedDate"].ToString()).ToShortDateString() + "</span></li>";
                        }

                    }
                    strPost += @"</ul>
    <div style='width:100%'><span style='float:right'><a href='" + weburl + @"'>more</a></span></div>
</div>";
                    writer.Write(strPost);
                }
            }
            catch (Exception ex)
            {
                //writer.Write(ex.Message);
            }
            
        }


        
    }
}
