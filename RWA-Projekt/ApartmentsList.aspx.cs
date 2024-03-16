using RwaLib.Dal;
using RwaLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWA_Projekt
{
    public partial class ApartmentsList : System.Web.UI.Page
    {
        private IList<Apartment> _ListOfAllApartments;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["user"]==null) Response.Redirect("Default.aspx");

            IList<String> apartmentTypes = new List<String>();
            apartmentTypes = ((DBRepo)Application["database"]).LoadTagType();
            
            if (!IsPostBack)
            {                
                LoadData();
                LoadCities();
            }           
        }

        private void LoadCities()
        {
            //dohvacanje gradova iz baze i spremanje u ddl
            ddlCities.DataSource = ((DBRepo)Application["database"]).LoadCities();
            ddlCities.DataBind();
        }

        private void LoadData()
        {
            //dohvacanje liste statusa
            ddlStatus.DataSource = ((DBRepo)Application["database"]).GetStatuses();
            ddlStatus.DataBind();
            // dohvacanje liste apartmana iz baze
            _ListOfAllApartments = ((DBRepo)Application["database"]).LoadApartments();
            rptApartment.DataSource = _ListOfAllApartments;
            rptApartment.DataBind();            
        }        

        protected void btnAddNewApartment_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddApartments.aspx");
        }

        protected void btnDetails_Click(object sender, EventArgs e)
        {
            Button btnDetails = (Button)sender;
            Session["apartmentId"] = int.Parse(btnDetails.CommandArgument); //dohvacanje id odabaranog apartmana
            Response.Redirect("ApartmentDetails.aspx");
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            int statusId = ddlStatus.SelectedIndex;
            if (statusId == 0)
            {
                rptApartment.DataSource = ((DBRepo)Application["database"]).LoadApartments();
                rptApartment.DataBind();
            }
            else
            {
                rptApartment.DataSource = ((DBRepo)Application["database"]).GetApartmentsByStatusId(statusId);
                rptApartment.DataBind();
            }
        }

       
    }

}
