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
    public partial class RegisteredUsers : System.Web.UI.Page
    {
        private IList<User> _listOfAllUsers;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null) Response.Redirect("Default.aspx");

            _listOfAllUsers = ((DBRepo)Application["database"]).LoadUsers();
            if (!IsPostBack)
            {
                LoadData();
            }
        }
            private void LoadData()
            {
                rptUsers.DataSource = _listOfAllUsers;
                rptUsers.DataBind();
            

        }
    }
}