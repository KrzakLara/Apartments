
using RwaLib.Dal;
using RwaLib.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWA_Projekt
{
    public partial class AddImages : System.Web.UI.Page
    {
        //povezivanje sa bazom
        //string connectionString = @"Data Source=(LocalDB)\LaraLocalDB;Initial Catalog=RwaApartmani;Integrated Security=True";
        private const string img = @"\Images\";
        private Apartment _apartment;
        private int apartmentId;
        private IList<ApartmentPicture> pictureList;
        private string _picPath = img;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null) Response.Redirect("Default.aspx");
            if (!IsPostBack)
            {
                UcitajGradove();
                UcitajVlasnike();
                UcitajStatuse();
            }
        }

        private void UcitajStatuse()
        {
            ddlStatus.DataSource = ((DBRepo)Application["database"]).GetStatuses();
            ddlStatus.DataBind();
        }

        private void UcitajVlasnike()
        {
            ddlOwner.DataSource = ((DBRepo)Application["database"]).GetOwners();
            ddlOwner.DataBind();
        }

        private void UcitajGradove()
        {
            ddlCities.DataSource = ((DBRepo)Application["database"]).GetCities();
            ddlCities.DataBind();
        }

        //dodavanje apartmana 
        protected void btnAddApartment_Click(object sender, EventArgs e)
        {
            Apartment apartment = new Apartment();

            //dohvacanje podataka o odabranom apartmanu
            apartment.Name= txtApartmentName.Text;
            apartment.BeachDistance=int.Parse(txtBeachDistance.Text);
            apartment.MaxAdults = int.Parse(txtMaxAdults.Text);
            apartment.MaxChildren = int.Parse(txtMaxChildren.Text);
            apartment.Price = int.Parse(txtApartmentPrice.Text);
            apartment.TotalRooms = int.Parse(txtTotalRooms.Text);
            apartment.Address = txtApartmentAdress.Text;
            apartment.CityId = ddlCities.SelectedIndex;
            apartment.OwnerId = int.Parse(ddlOwner.SelectedValue);
            apartment.StatusId = ddlStatus.SelectedIndex;
            apartment.NameEng = txtApartmentName.Text;
            apartment.TypeId = 999;

            ((DBRepo)Application["database"]).CreateApartment(apartment); //Kreiranje apartmana

            Response.Redirect("ApartmentsList.aspx");
            //txtOwnerName.Text = ((DBRepo)Application["database"]).GetOwnerName(apartment.OwnerId);
            //ddltype.SelectedIndex = ((DBRepo)Application["database"]).GetApartmentStatus(apartmentId);

            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(connectionString))
            //    {
            //        if (FileUpload1.HasFile)
            //        {
            //            conn.Open();
            //            string query = "Insert into Apartmetns (Id,Address,Name,NameEng,Price,MaxAdults,MaxChildren,TotalRooms,BeachDistance,Img) values(@Id,@Address,@Name,@NameEng,@Price,@MaxAdults,@MaxChildren,@TotalRooms,@BeachDistance,@Img";
            //            SqlCommand  sqlcommand = new SqlCommand(query, conn);
            //            FileUpload1.SaveAs(Server.MapPath("~/Images/") + System.IO.Path.GetFileName(FileUpload1.FileName));
            //            string linkPath = "Images/" +System.IO.Path.GetFileName(FileUpload1.FileName);

            //            sqlcommand.Parameters.AddWithValue("@Id", txtIdApartment.Text);
            //            sqlcommand.Parameters.AddWithValue("@Address", txtApartmentAdress.Text);
            //            sqlcommand.Parameters.AddWithValue("@Name", txtApartmentName.Text);
            //            sqlcommand.Parameters.AddWithValue("@NameEng", txtApartmentNameEng.Text);
            //            sqlcommand.Parameters.AddWithValue("@Price", txtApartmentPrice.Text);
            //            sqlcommand.Parameters.AddWithValue("@MaxAdults", txtMaxAdults.Text);
            //            sqlcommand.Parameters.AddWithValue("@MaxChildren", txtMaxChildren.Text);
            //            sqlcommand.Parameters.AddWithValue("@TotalRooms", txtTotalRooms.Text);
            //            sqlcommand.Parameters.AddWithValue("@BeachDistance", txtBeachDistance.Text);
            //            sqlcommand.Parameters.AddWithValue("@Img", linkPath);

            //            sqlcommand.ExecuteNonQuery();
            //            conn.Close();
            //        }

            //    }
            //    Response.Write("<script>alert('Image added.');</script>");
            //}
            //catch (Exception)
            //{

            //    Response.Write("<script>alert('There was a problem adding the file.);</script>");}
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListOfAddedApartments.aspx");
        }

        protected void btnReturnToApt_Click(object sender, EventArgs e)
        {
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