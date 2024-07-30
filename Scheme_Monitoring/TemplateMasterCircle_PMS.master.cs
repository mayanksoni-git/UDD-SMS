using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;

public partial class TemplateMasterCircle_PMS : System.Web.UI.MasterPage
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
            lblUserHeader.InnerHtml = Session["Person_Name"].ToString();
            lblUserHeader2.InnerHtml = Session["Person_Name"].ToString();
            lblDesignation.InnerHtml = Session["Designation_DesignationName"].ToString();
            ProflePic.Src = Session["Profile_Pic"].ToString();
        }
        obj_tbl_ePaymentModules = (tbl_ePaymentModules)Session["tbl_ePaymentModules"];
        set_Permission();
    }
    private void set_Permission()
    {
        if (Session["User_Permission"] != null)
        {
            DataSet ds_User_Permission = (DataSet)(Session["User_Permission"]);
            if (ds_User_Permission != null)
            {
                if (ds_User_Permission.Tables.Count > 0 && ds_User_Permission.Tables[0].Rows.Count > 0)
                {
                    Session["BOQ_C"] = ds_User_Permission.Tables[0].Rows[0]["ProcessConfigMaster_Creation_Allowed"].ToString();
                    Session["BOQ_U"] = ds_User_Permission.Tables[0].Rows[0]["ProcessConfigMaster_Updation_Allowed"].ToString();
                }
                else
                {
                    Session["BOQ_C"] = "0";
                    Session["BOQ_U"] = "0";
                }

                if (ds_User_Permission.Tables.Count > 1 && ds_User_Permission.Tables[1].Rows.Count > 0)
                {
                    Session["EMB_C"] = ds_User_Permission.Tables[1].Rows[0]["ProcessConfigMaster_Creation_Allowed"].ToString();
                    Session["EMB_U"] = ds_User_Permission.Tables[1].Rows[0]["ProcessConfigMaster_Updation_Allowed"].ToString();
                }
                else
                {
                    Session["EMB_C"] = "0";
                    Session["EMB_U"] = "0";
                }

                if (ds_User_Permission.Tables.Count > 2 && ds_User_Permission.Tables[2].Rows.Count > 0)
                {
                    Session["Invoice_C"] = ds_User_Permission.Tables[2].Rows[0]["ProcessConfigMaster_Creation_Allowed"].ToString();
                    Session["Invoice_U"] = ds_User_Permission.Tables[2].Rows[0]["ProcessConfigMaster_Updation_Allowed"].ToString();
                }
                else
                {
                    Session["Invoice_C"] = "0";
                    Session["Invoice_U"] = "0";
                }

                if (ds_User_Permission.Tables.Count > 3 && ds_User_Permission.Tables[3].Rows.Count > 0)
                {
                    int Creation = 0;
                    int Updation = 0;
                    try
                    {
                        Creation = Convert.ToInt32(ds_User_Permission.Tables[3].Rows[0]["ProcessConfigMaster_Creation_Allowed"].ToString());
                    }
                    catch
                    {
                        Creation = 0;
                    }
                    try
                    {
                        Updation = Convert.ToInt32(ds_User_Permission.Tables[3].Rows[0]["ProcessConfigMaster_Updation_Allowed"].ToString());
                    }
                    catch
                    {
                        Updation = 0;
                    }
                    if (Creation + Updation > 0)
                    {
                        Session["DPR"] = "1";
                    }
                    else
                    {
                        Session["DPR"] = "0";
                    }
                }
                else
                {
                    Session["DPR"] = "0";
                }
            }
            else
            {
                Session["BOQ_C"] = "0";
                Session["BOQ_U"] = "0";

                Session["EMB_C"] = "0";
                Session["EMB_U"] = "0";

                Session["Invoice_C"] = "0";
                Session["Invoice_U"] = "0";

                Session["DPR"] = "0";
            }
        }
        else
        {
            Session["BOQ_C"] = "0";
            Session["BOQ_U"] = "0";

            Session["EMB_C"] = "0";
            Session["EMB_U"] = "0";

            Session["Invoice_C"] = "0";
            Session["Invoice_U"] = "0";
        }
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
