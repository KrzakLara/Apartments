using RwaLib.Models;
using RwaLib.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace RWA_Projekt
{
    public partial class ApartmentDetails : System.Web.UI.Page
    {
        private IList<Tag> _ListOfAllApartmentDetailTags;
        private int _apartmentId;

        private const string img = @"\Images\";
        private Apartment _apartment;
        private int apartmentId;
        private IList<ApartmentPicture> pictureList;
        private string _picPath = img;

        protected void Page_Load(object sender, EventArgs e)
        {
            _apartmentId = (int)Session["apartmentId"];
            _ListOfAllApartmentDetailTags = ((DBRepo)Application["database"]).LoadApartmentTags(_apartmentId);
            if (!IsPostBack)
            {
                //kad odaberemo grad mora se spremiti po slected value                
                LoadApartmentTags(_ListOfAllApartmentDetailTags);
                LoadApartment(_apartmentId);
            }
        }

        public void LoadApartment(int apartmentId)
        {
            Apartment apartment = new Apartment();

            apartment = ((DBRepo)Application["database"]).GetApartmentById(apartmentId);

            //dohvacanje podataka o odabranom apartmanu
            txtName.Text = apartment.Name;
            txtBeachDistance.Text = apartment.BeachDistance.ToString();
            txtMaxAdults.Text = apartment.MaxAdults.ToString();
            txtMaxChildren.Text = apartment.MaxChildren.ToString();
            txtPrice.Text = apartment.Price.ToString();
            txtTotalRooms.Text = apartment.TotalRooms.ToString();
            txtOwnerName.Text= ((DBRepo)Application["database"]).GetOwnerName(apartment.OwnerId);           
            ddltype.SelectedIndex = ((DBRepo)Application["database"]).GetApartmentStatus(apartmentId);           
        }

        private void LoadApartmentTags(IList<Tag> tags )
        {
            //dohvacanje liste tagova
            rptApartmentDetailsTags.DataSource = tags;
            rptApartmentDetailsTags.DataBind();
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
        

        protected void btnReturnBack_Click(object sender, EventArgs e)
        {           
            Response.Redirect("ApartmentsList.aspx");            
        }

        protected void btnSaveApartment_Click(object sender, EventArgs e)
        {           
            Apartment apartment = new Apartment()
            {
                Id = _apartmentId,
                Name =  txtName.Text,
                MaxAdults = int.Parse(txtMaxAdults.Text),
                MaxChildren = int.Parse(txtMaxChildren.Text),
                Price = decimal.Parse(txtPrice.Text),
                StatusId = ddltype.SelectedIndex,
                BeachDistance = int.Parse(txtBeachDistance.Text),
                TotalRooms = int.Parse(txtTotalRooms.Text)
            };

            ((DBRepo)Application["database"]).EditApartment(apartment);
            Response.Redirect("ApartmentsList.aspx");
        }

        protected void btnShowApartmetDetaisDeleteTagModal_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int tagId = int.Parse(button.CommandArgument);//dohvacanje tagid 
            pnlShowApartmentDetailDeleteTagModal.Visible = true; //postavljanje na visible panel za brisanje taga
            ViewState["tagId"] = tagId;
        }


        //brisanje taga
        protected void btnDeleteApartmentDetailTag_Click(object sender, EventArgs e)
        {
            int tagId = (int)ViewState["tagId"]; //dohvacanje tagid            
            ((IRepo)Application["database"]).DeleteApartmentTag(tagId, _apartmentId);
            Response.Redirect("ApartmentDetails.aspx");
            //rptApartmentDetailsTags.DataSource = ((DBRepo)Application["database"]).LoadApartmentTags(_apartmentId);
            //rptApartmentDetailsTags.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ((DBRepo)Application["database"]).DeleteApartment(_apartmentId);
            Response.Redirect("ApartmentsList.aspx");
        }

        protected void btnAddPicture_Click(object sender, EventArgs e)
        {
            string picPath = Request.PhysicalApplicationPath + _picPath;
            string getFileName = Path.GetFileName(uplImages.FileName);
            string fullPath = picPath + getFileName;
            string dbPath = _picPath + getFileName;

            if (File.Exists(fullPath)) return;

            if (String.IsNullOrEmpty(getFileName)) return;

            uplImages.SaveAs(fullPath);
            ((DBRepo)Application["database"]).AddPictures(apartmentId, dbPath);

            GetPictures();
        }

        private void GetPictures()
        {
            IList<ApartmentPicture> apartmentPictures = new List<ApartmentPicture>();
            apartmentPictures = ((DBRepo)Application["database"]).GetApartmentPicturesByID(apartmentId);
            rptApartmentPictures.DataSource = apartmentPictures;
            rptApartmentPictures.DataBind();
        }

        protected void btnRepresentative_Click(object sender, EventArgs e)
        {
            Button representative = (Button)sender;
            int newRepresentative = int.Parse(representative.CommandArgument);
            ((DBRepo)Application["database"]).SetRepresentativePicture(apartmentId, newRepresentative);
        }


        protected void btnShowDeleteImageModal_Click(object sender, EventArgs e)
        {
            Button delete = (Button)sender;
            int imageId = int.Parse(delete.CommandArgument);
            string path = Request.PhysicalApplicationPath + ((DBRepo)Application["database"]).GetImagePath(imageId);
            ViewState["path"] = path;
            ViewState["imageId"] = imageId;
            pnlShowDeleteImgModal.Visible = true;
        }

        protected void btnDeleteImage_Click(object sender, EventArgs e)
        {
            int imageId = (int)ViewState["imageId"];
            ((DBRepo)Application["database"]).DeleteApartmentPictureByID(imageId);
            pnlShowDeleteImgModal.Visible = false;
            GetPictures();
            string path = (string)ViewState["path"];
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
