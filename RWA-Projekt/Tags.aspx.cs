using RwaLib.Dal;
using RwaLib.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApartmentTags = RwaLib.Models.Tag;

namespace RWA_Projekt
{
    public partial class Tags : System.Web.UI.Page
    {
        private IList<ApartmentTags> _ListOfAllTags;

        public object Name { get; internal set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null) Response.Redirect("Default.aspx");
            _ListOfAllTags = ((DBRepo)Application["database"]).LoadTags();
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            rptTags.DataSource = _ListOfAllTags;
            rptTags.DataBind();


            //ucitavanje vrstu taga
            IList<String> tagTypes = new List<String>();
            tagTypes = ((DBRepo)Application["database"]).LoadTagType();
            int numerator = 1;
            foreach (string tag in tagTypes)
            {
                ddlType.Items.Add(new ListItem { Text = tag, Value = numerator.ToString() });
                numerator++;
            }
            ViewState["tags"] = _ListOfAllTags;
        }

        protected void btnDeleteConfirmation(object sender, EventArgs e)
        {         
            try
            {
                Response.Redirect(Request.Url.LocalPath);
            }
            catch (Exception)
            {
                throw;
            };
        }
      
        //Prikazujemo panel za brisanja taga
      
        protected void btnShowDeleteModal_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender; //
            int tagId = int.Parse(button.CommandArgument);//dohvacanje tagid 
            pnlShowDeleteModal.Visible = true; //postavljanje na visible panel za brisanje taga
            ViewState["tagId"] = tagId;
        }

        //brisanje taga
        protected void btnDeleteTag_Click(object sender, EventArgs e)
        {
            int tagId = (int)ViewState["tagId"]; //dohvacanje tagid
            ((IRepo)Application["database"]).DeleteTag(tagId);
            Response.Redirect("Tags.aspx"); 
        }

        //dodavanje novog taga
        protected void btnAddTag_Click(object sender, EventArgs e)
        {
            int typeId = int.Parse(ddlType.SelectedValue);
            string ime = tbName.Text;

            for (int i = 0; i < _ListOfAllTags.Count; i++)
            {
                if (ime.ToUpper() == _ListOfAllTags[i].Name.ToUpper())
                {
                    pnlTagPostoji.Visible = true;
                    return;
                }
            }

            ((IRepo)Application["database"]).AddTag(typeId, ime);
            Response.Redirect("Tags.aspx");
        }
    }
}