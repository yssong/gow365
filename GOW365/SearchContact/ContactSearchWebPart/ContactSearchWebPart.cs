using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Xml.Serialization;
using System.Data;


namespace GOW365.ContactSearchWebPart
{
    [ToolboxItemAttribute(false)]
    
    public class ContactSearchWebPart : WebPart
    {
        private string imgUrl = "GOW365/SubSitesTab/";

        private string webName = string.Empty;

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        Category("사이트"),
        WebDisplayName("Site Url"),
        WebDescription("사이트의 상대경로를 입력하세요.")]

        public string WebName
        {
            get { return webName; }
            set { webName = value; }
        }

        //Controls
        UpdatePanel mainUpdatePanel;
        UpdateProgress progressControl;
        Button checkTimeButton;
        Label timeDisplayLabel;
        TextBox txtSearch;      

        protected override void OnInit(EventArgs e)
        {
            if (Page != null && ScriptManager.GetCurrent(Page) == null)
            {
                Page.Form.Controls.AddAt(0, new ScriptManager());
            }

            base.OnInit(e);
        }

        protected override void CreateChildControls()
        {
            //sm = new ScriptManager();

            imgUrl = (SPContext.Current.Site.ServerRelativeUrl.EndsWith("/") ? SPContext.Current.Site.ServerRelativeUrl + imgUrl : SPContext.Current.Site.ServerRelativeUrl + "/" + imgUrl);

            //Create the update panel
            mainUpdatePanel = new UpdatePanel();
            mainUpdatePanel.ID = this.ClientID+"updateAjaxDemoWebPart";
            //Use conditional mode so that only controls within this panel cause an update
            mainUpdatePanel.UpdateMode = UpdatePanelUpdateMode.Conditional;
            
            //Create the update progress control
            progressControl = new UpdateProgress();
            progressControl.AssociatedUpdatePanelID = this.ClientID + "updateAjaxDemoWebPart";
            //The class used for the progrss template is defined below in this code file
            progressControl.ProgressTemplate = new ProgressTemplate(imgUrl+"", "Loading");

            //search
            txtSearch = new TextBox();


            //Create the Check Time button
            checkTimeButton = new Button();
            checkTimeButton.ID = "searchButton";
            checkTimeButton.Text = "Search";
            checkTimeButton.Click += new EventHandler(searchContact_Click);

            //Create the label that displays the time
            timeDisplayLabel = new Label();
            timeDisplayLabel.ID = "timeDisplayLabel";
            timeDisplayLabel.Text = "";

            //Add the button and label to the Update Panel
            mainUpdatePanel.ContentTemplateContainer.Controls.Add(txtSearch);
            mainUpdatePanel.ContentTemplateContainer.Controls.Add(checkTimeButton);
            mainUpdatePanel.ContentTemplateContainer.Controls.Add(timeDisplayLabel);

            //Add the Update Panel and the progress control to the Web Part controls
            //this.Controls.Add(sm);

            this.Controls.Add(mainUpdatePanel);
            this.Controls.Add(progressControl);

        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            if (!ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
            {
                string script = "";
                script = @"
                        var ITEMLISTINGBUTTONID;
                        with(Sys.WebForms.PageRequestManager.getInstance()){
                            add_beginRequest(onBeginRequest);
                            add_endRequest(onEndRequest);
                        }
                        function onBeginRequest(sender, args){
                            ITEMLISTINGBUTTONID = args.get_postBackElement().id;
                            $get(ITEMLISTINGBUTTONID).parentElement.style.display = 'none';
                        }
                        function onEndRequest(sender, args){                           
                           $get(ITEMLISTINGBUTTONID).parentElement.style.display = '';
                        }
                        ";
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "HideSimpleAJAXWebPartUDP", script, true);
            }
        }

        private void searchContact_Click(object sender, EventArgs e)
        {
            //This calls a server-side method, but because the button is in 
            //an update panel, only the update panel reloads.
            //this.timeDisplayLabel.Text = string.Format("<br/>The time is: {0}", DateTime.Now.ToLongTimeString());
            this.timeDisplayLabel.Text =getSearchResult();
        }

        private string getSearchResult()
        {
            string strContact = @"<div id='" + this.ClientID + "_contact' class='contactContainer'>";

            SPWeb web = null;
            
            string weburl = "";
            
            try
            {
               
                if (webName == "")
                {
                    weburl = SPContext.Current.Web.ServerRelativeUrl;
                }
                else
                {
                    weburl = WebName;
                }
               
                web = SPContext.Current.Site.OpenWeb(weburl);

                try
                {
                    SPSiteDataQuery qry = new SPSiteDataQuery();

                    qry.Query = @"<Where><Or><Contains><FieldRef Name='FullName' /><Value Type='Text'>" + txtSearch.Text + "</Value></Contains><Contains><FieldRef Name='Company' /><Value Type='Text'>" + txtSearch.Text + "</Value></Contains></Or></Where><OrderBy><FieldRef Name='FullName' /></OrderBy>";
                    qry.Lists = "<Lists ServerTemplate='105'/>";
                    qry.ViewFields = "<FieldRef Name='FileRef' /><FieldRef Name='FullName' /><FieldRef Name='Company' /><FieldRef Name='WorkPhone' /><FieldRef Name='CellPhone' /><FieldRef Name='Email' />";
                    qry.Webs = "<Webs Scope='Recursive'/>";

                    DataTable resultTable = web.GetSiteData(qry);

                    foreach (DataRow item in resultTable.Rows)
                    {
                        if (item != null)
                        {
                            strContact += "<div class='contact'><span><a href=\"" + "/" + item["FileRef"].ToString().Split('#')[1] + "\">";
                            strContact += item["FullName"].ToString();
                            strContact += "</a></span>";
                            strContact += "<span>" + item["Company"].ToString() + "</span>";
                            strContact += "<span>" + item["WorkPhone"].ToString() + "</span>";
                            strContact += "<span>" + item["CellPhone"].ToString() +"</span>";
                            strContact += "</div>";
                        }
                    }

                    
                }
                catch (Exception ex)
                {
                }
                finally
                {
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
            strContact += "</div>";
            return strContact;
            //this.timeDisplayLabel.Text = strContact;
            

        }

    }

    //This template defines the contents of the Update Progress control
    public class ProgressTemplate : ITemplate
    {
        public string ImagePath { get; set; }
        public string DisplayText { get; set; }

        public ProgressTemplate(string imagePath, string displayText)
        {
            ImagePath = imagePath;
            DisplayText = displayText;
        }

        public void InstantiateIn(Control container)
        {
            Image img = new Image();
            img.ImageUrl = SPContext.Current.Site.Url + "/" + ImagePath;

            Label displayTextLabel = new Label();
            displayTextLabel.Text = DisplayText;

            container.Controls.Add(img);
            container.Controls.Add(displayTextLabel);
        }
    }
}
