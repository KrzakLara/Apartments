using RwaLib.Dal;
using RwaLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RWA_Projekt
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        

            if (!IsPostBack)
            {
                PanelForma.Visible = true;
                PanelIspis.Visible = false;
            }
                     
           

        }
      

        private bool ValidateUser(string userName, string passWord)
        {
            if (userName == "123" && passWord == "123")
                return true;

            return false;
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args.Value == "admin")
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //provjeravamo jel postoji username i password u users

                var username = txtUsername.Text;
                var password = Cryptography.HashPassword(txtPassword.Text);

                User user = ((IRepo)Application["database"]).AuthUser(username, password);

                if (user == null)
                {
                    PanelIspis.Visible = true;
                    PanelForma.Visible = true;

                    txtUsername.Text = "";
                    txtPassword.Text = "";
                }
                else
                {
                    Session["user"] = user;
                    Response.Redirect("ApartmentsList.aspx");
                }

            }
        }
        
    }
}
