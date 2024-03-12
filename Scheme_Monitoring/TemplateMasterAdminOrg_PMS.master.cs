using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;

public partial class TemplateMasterAdminOrg_PMS : System.Web.UI.MasterPage
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
            lblUserHeader.Text = Session["Person_Name"].ToString() + "     " + lastLogin;
            lblUser.Text = Session["Person_Name"].ToString();
            lblUserNameDesig.Text = Session["Person_Name"].ToString() + Environment.NewLine + Session["Designation_DesignationName"].ToString();

            Render_Notification();
        }
        obj_tbl_ePaymentModules = (tbl_ePaymentModules)Session["tbl_ePaymentModules"];
        set_Permission();
    }
    private void Render_Notification()
    {
        string Scheme_Id = Session["Default_Scheme"].ToString();
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

        //if (Session["UserType"].ToString() != "1")
        //{
        //    try
        //    {
        //        if (Session["PersonJuridiction_Project_Id"].ToString() != "" && Session["PersonJuridiction_Project_Id"].ToString() != "0")
        //        {
        //            ddlScheme.SelectedValue = Session["PersonJuridiction_Project_Id"].ToString();
        //            ddlScheme.Enabled = false;
        //        }
        //    }
        //    catch
        //    {

        //    }

        //}
        if (Session["UserType"].ToString() == "4" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {//Zone
            try
            {
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {
            try
            {
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
                if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                {//Circle
                    try
                    {
                        Circle_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                }
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
        {
            try
            {
                Zone_Id = Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString());
                if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                {//Circle
                    try
                    {
                        Circle_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                        if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                        {//Circle
                            try
                            {
                                Division_Id = Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString());
                            }
                            catch
                            {
                                Division_Id = 0;
                            }
                        }
                    }
                    catch
                    {
                        Circle_Id = 0;
                    }
                }
            }
            catch
            {
                Zone_Id = 0;
            }
        }
        int Total_Notification = 0;

        int Document_NA = 0;

        int Payment_CanBeDone = 0;

        int RequireExtention = 0;
        int ExtentionOver = 0;

        int StagnantPhysical = 0;
        int StagnantFinancial = 0;

        string li_Inner = "";

        string navigateURL = "";
        DataSet ds = new DataSet();
        ds = (DataSet)Session["ds_Notification"];
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            try
            {
                RequireExtention = Convert.ToInt32(ds.Tables[0].Rows[0]["LD_Likly"].ToString());
            }
            catch
            {
                RequireExtention = 0;
            }

            if (RequireExtention > 0)
            {
                navigateURL = "MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&LD=0";
                li_Inner += @"<li>
	                            <a href='" + navigateURL + "'>";
                li_Inner += @"      <div class='clearfix'>
			                            <span class='pull-left'>
				                            <i class='btn btn-xs no-hover btn-success fa fa-shopping-cart'></i>
				                            Packages Where Contract Has already ended but extention has not being given
                                        </span>
			                            <span class='pull-right badge badge-success'>" + RequireExtention + "</span>";
                li_Inner += @"      </div>
	                            </a>
                            </li>
                            ";
            }

            try
            {
                ExtentionOver = Convert.ToInt32(ds.Tables[0].Rows[0]["Delay_Likly2"].ToString());
            }
            catch
            {
                ExtentionOver = 0;
            }

            if (ExtentionOver > 0)
            {
                navigateURL = "MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&LD=2";
                li_Inner += @"<li>
	                            <a href='" + navigateURL + "'>";
                li_Inner += @"      <div class='clearfix'>
			                            <span class='pull-left'>
				                            <i class='btn btn-xs no-hover btn-success fa fa-shopping-cart'></i>
				                            List of packages where Extension of time line is over and further extension is required
                                        </span>
			                            <span class='pull-right badge badge-success'>" + ExtentionOver + "</span>";
                li_Inner += @"      </div>
	                            </a>
                            </li>
                            ";
            }
        }
        else
        {
            RequireExtention = 0;
            ExtentionOver = 0;
        }

        if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            try
            {
                StagnantPhysical = Convert.ToInt32(ds.Tables[1].Rows[0]["Total_Stagnent_Physical"].ToString());
            }
            catch
            {
                StagnantPhysical = 0;
            }

            if (StagnantPhysical > 0)
            {
                navigateURL = "Report_ProjectWork_Physical_Progress_NoChange.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id;
                li_Inner += @"<li>
	                            <a href='" + navigateURL + "'>";
                li_Inner += @"      <div class='clearfix'>
			                            <span class='pull-left'>
				                            <i class='btn btn-xs no-hover btn-success fa fa-shopping-cart'></i>
				                            List of Projects where No Physical Progress is updated since last 60 days
                                        </span>
			                            <span class='pull-right badge badge-success'>" + StagnantPhysical + "</span>";
                li_Inner += @"      </div>
	                            </a>
                            </li>
                            ";
            }
        }
        else
        {
            StagnantPhysical = 0;
        }

        if (ds != null && ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
        {
            try
            {
                StagnantFinancial = Convert.ToInt32(ds.Tables[2].Rows[0]["Total_Stagnent_Financial"].ToString());
            }
            catch
            {
                StagnantFinancial = 0;
            }

            if (StagnantFinancial > 0)
            {
                navigateURL = "Report_Project_With_No_Invoice.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id;
                li_Inner += @"<li>
	                            <a href='" + navigateURL + "'>";
                li_Inner += @"      <div class='clearfix'>
			                            <span class='pull-left'>
				                            <i class='btn btn-xs no-hover btn-success fa fa-shopping-cart'></i>
				                            List of Projects where No Financial Progress is updated since last 60 days
                                        </span>
			                            <span class='pull-right badge badge-success'>" + StagnantFinancial + "</span>";
                li_Inner += @"      </div>
	                            </a>
                            </li>
                            ";
            }
        }
        else
        {
            StagnantFinancial = 0;
        }

        if (ds != null && ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
        {
            try
            {
                Document_NA = Convert.ToInt32(ds.Tables[3].Rows[0]["Total_Project_Doc_NA"].ToString());
            }
            catch
            {
                Document_NA = 0;
            }

            if (Document_NA > 0)
            {
                navigateURL = "Report_ProjectDocumentNotAvailable.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id;
                li_Inner += @"<li>
	                            <a href='" + navigateURL + "'>";
                li_Inner += @"      <div class='clearfix'>
			                            <span class='pull-left'>
				                            <i class='btn btn-xs no-hover btn-success fa fa-shopping-cart'></i>
				                            List of Projects where Any Document is Missing
                                        </span>
			                            <span class='pull-right badge badge-success'>" + Document_NA + "</span>";
                li_Inner += @"      </div>
	                            </a>
                            </li>
                            ";
            }
        }
        else
        {
            Document_NA = 0;
        }

        if (ds != null && ds.Tables.Count > 4 && ds.Tables[4].Rows.Count > 0)
        {
            try
            {
                Payment_CanBeDone = Convert.ToInt32(ds.Tables[4].Rows[0]["Payment_Can_Be_Done"].ToString());
            }
            catch
            {
                Payment_CanBeDone = 0;
            }

            if (Payment_CanBeDone > 0)
            {
                navigateURL = "MasterSNAChildAccount.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Display=1";
                li_Inner += @"<li>
	                            <a href='" + navigateURL + "'>";
                li_Inner += @"      <div class='clearfix'>
			                            <span class='pull-left'>
				                            <i class='btn btn-xs no-hover btn-success fa fa-shopping-cart'></i>
				                            List of Projects where SNA Limit is Available and Payment Can Be Done
                                        </span>
			                            <span class='pull-right badge badge-success'>" + Payment_CanBeDone + "</span>";
                li_Inner += @"      </div>
	                            </a>
                            </li>
                            ";
            }
        }
        else
        {
            Payment_CanBeDone = 0;
        }
        string NotFor = "";
        string Client = ConfigurationManager.AppSettings.Get("Client");
        if (Client == "JNUP")
        {
            NotFor = "AMRUT";
        }
        else if (Client == "ULB")
        {
            NotFor = "D-ULB";
        }
        else
        {
            NotFor = "C&DS";
        }
        Total_Notification = Document_NA + RequireExtention + ExtentionOver + StagnantPhysical + StagnantFinancial + Payment_CanBeDone;
        spNotificationCount.InnerHtml = Total_Notification.ToString();
        liNotificationCount.InnerHtml = "<i class='ace-icon fa fa-exclamation-triangle'></i>" + Total_Notification.ToString() + " Notifications (" + NotFor + ")";
        ulNotificationList.InnerHtml = li_Inner;
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
