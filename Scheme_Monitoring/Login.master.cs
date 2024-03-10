using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TemplateMasterAdmin : System.Web.UI.MasterPage
{
    public tbl_ePaymentModules obj_tbl_ePaymentModules = new tbl_ePaymentModules();

    protected void Page_Load(object sender, EventArgs e)
    {
        string Base_Web_URL = ConfigurationManager.AppSettings.Get("Base_Web_URL");
        string Client = ConfigurationManager.AppSettings.Get("Client");
        Session["Client"] = Client;
        obj_tbl_ePaymentModules = new DataLayer().get_Modules();
        Session["tbl_ePaymentModules"] = obj_tbl_ePaymentModules;
        Session["Base_Web_URL"] = Base_Web_URL;
    }

    protected void btnSiteVisit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("ProjectWorkFeildVisitUpload.aspx");
    }
}
