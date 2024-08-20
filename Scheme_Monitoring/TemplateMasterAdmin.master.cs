using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;

public partial class TemplateMasterAdmin : System.Web.UI.MasterPage
{
    public tbl_ePaymentModules obj_tbl_ePaymentModules = new tbl_ePaymentModules();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Person_Id"] == null || Session["Login_Id"] == null)
        {
            Response.Redirect("Index.aspx");
        }
        if (!IsPostBack)
        {
            string lastLogin = new DataLayer().get_tbl_LoginHistory_Last_Login(Session["Person_Id"].ToString());
            //lblUserHeader.Text = Session["Person_Name"].ToString() + "     " + lastLogin;
            //lblUser.Text = Session["Person_Name"].ToString();
            //lblUserNameDesig.Text = Session["Person_Name"].ToString() + Environment.NewLine + Session["Designation_DesignationName"].ToString();
            ProflePic.Src = Session["Profile_Pic"].ToString();
           
        }
        obj_tbl_ePaymentModules = (tbl_ePaymentModules)Session["tbl_ePaymentModules"];
    }

    protected void lnkLogOut_Click(object sender, EventArgs e)
    {
        new DataLayer().Update_tbl_LoginHistory(Session["LoginHistory_Id"].ToString());
        Session.Abandon();
        Session.Clear();
        Response.Redirect("Index.aspx");
    }

    protected void lnkChangePassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChangePassword.aspx");
    }
}
