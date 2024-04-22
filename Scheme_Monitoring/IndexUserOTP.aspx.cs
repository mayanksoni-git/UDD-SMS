using Aspose.Pdf.Operators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Web;

public partial class IndexUserOTP : System.Web.UI.Page
{
    public tbl_ePaymentModules obj_tbl_ePaymentModules = new tbl_ePaymentModules();
    protected void Page_Load(object sender, EventArgs e)
    {
        string Client = ConfigurationManager.AppSettings.Get("Client");
        if (Session["Module_Id"] != null && Session["Module_Id"].ToString() == "1")
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "1")//Administrator
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                {
                    if (Client == "CNDS")
                    {
                        Response.Redirect("Dashboard_PMIS_CNDS.aspx");
                    }
                    else if (Client == "SAR")
                    {
                        Response.Redirect("Dashboard_Sarovar.aspx");
                    }
                    else if (Client == "JNUP")
                    {
                        Response.Redirect("Dashboard_PMIS.aspx");
                    }
                    else
                    {
                        Response.Redirect("DashboardNew.aspx");
                    }
                }
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "2")//District
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                {
                    if (Client == "CNDS")
                    {
                        Response.Redirect("Dashboard_PMIS_CNDS.aspx");
                    }
                    else if (Client == "SAR")
                    {
                        Response.Redirect("Dashboard_Sarovar.aspx");
                    }
                    else if (Client == "JNUP")
                    {
                        Response.Redirect("Dashboard_PMIS.aspx");
                    }
                    else
                    {
                        Response.Redirect("DashboardNew.aspx");
                    }
                }
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "3")//ULB
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                {
                    if (Client == "CNDS")
                    {
                        Response.Redirect("Dashboard_PMIS_CNDS.aspx");
                    }
                    else if (Client == "SAR")
                    {
                        Response.Redirect("Dashboard_Sarovar.aspx");
                    }
                    else if (Client == "JNUP")
                    {
                        Response.Redirect("Dashboard_PMIS.aspx");
                    }
                    else
                    {
                        Response.Redirect("DashboardNew.aspx");
                    }
                }
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "4")//Zone Officer
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                {
                    if (Client == "CNDS")
                    {
                        Response.Redirect("Dashboard_PMIS_CNDS.aspx");
                    }
                    else if (Client == "SAR")
                    {
                        Response.Redirect("Dashboard_Sarovar.aspx");
                    }
                    else if (Client == "JNUP")
                    {
                        Response.Redirect("Dashboard_PMIS.aspx");
                    }
                    else
                    {
                        Response.Redirect("DashboardNew.aspx");
                    }
                }
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "5")//Contractor Officer
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                {
                    if (Client == "CNDS")
                    {
                        Response.Redirect("Dashboard_PMIS_CNDS.aspx");
                    }
                    else if (Client == "SAR")
                    {
                        Response.Redirect("Dashboard_Sarovar.aspx");
                    }
                    else if (Client == "JNUP")
                    {
                        Response.Redirect("Dashboard_PMIS.aspx");
                    }
                    else
                    {
                        Response.Redirect("DashboardNew.aspx");
                    }
                }
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "6")//Circle Officer
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                {
                    if (Client == "CNDS")
                    {
                        Response.Redirect("Dashboard_PMIS_CNDS.aspx");
                    }
                    else if (Client == "SAR")
                    {
                        Response.Redirect("Dashboard_Sarovar.aspx");
                    }
                    else if (Client == "JNUP")
                    {
                        Response.Redirect("Dashboard_PMIS.aspx");
                    }
                    else
                    {
                        Response.Redirect("DashboardNew.aspx");
                    }
                }
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "7")//Division Officer
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                {
                    if (Client == "CNDS")
                    {
                        Response.Redirect("Dashboard_PMIS_CNDS.aspx");
                    }
                    else if (Client == "SAR")
                    {
                        Response.Redirect("Dashboard_Sarovar.aspx");
                    }
                    else if (Client == "JNUP")
                    {
                        Response.Redirect("Dashboard_PMIS.aspx");
                    }
                    else
                    {
                        Response.Redirect("DashboardNew.aspx");
                    }
                }
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "8")//Organizational Admin
            {
                {
                    if (Client == "CNDS")
                    {
                        Response.Redirect("Dashboard_PMIS_CNDS.aspx");
                    }
                    else if (Client == "SAR")
                    {
                        Response.Redirect("Dashboard_Sarovar.aspx");
                    }
                    else if (Client == "JNUP")
                    {
                        Response.Redirect("Dashboard_PMIS.aspx");
                    }
                    else
                    {
                        Response.Redirect("DashboardNew.aspx");
                    }
                }
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "9")//Operator
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                {
                    if (Client == "CNDS")
                    {
                        Response.Redirect("Dashboard_PMIS_CNDS.aspx");
                    }
                    else if (Client == "SAR")
                    {
                        Response.Redirect("Dashboard_Sarovar.aspx");
                    }
                    else if (Client == "JNUP")
                    {
                        Response.Redirect("Dashboard_PMIS.aspx");
                    }
                    else
                    {
                        Response.Redirect("DashboardNew.aspx");
                    }
                }
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "14")//Operator
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                {
                    if (Client == "CNDS")
                    {
                        Response.Redirect("Dashboard_PMIS_CNDS.aspx");
                    }
                    else if (Client == "SAR")
                    {
                        Response.Redirect("Dashboard_Sarovar.aspx");
                    }
                    else if (Client == "JNUP")
                    {
                        Response.Redirect("Dashboard_PMIS.aspx");
                    }
                    else
                    {
                        Response.Redirect("DashboardNew.aspx");
                    }
                }
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else
            {
                //Response.Redirect("Index.aspx");
            }
        }
        else
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "1")//Administrator
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                    Response.Redirect("DashboardFinance.aspx");
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "4")//Zone Officer
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                    Response.Redirect("DashboardFinance.aspx");
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "6")//Circle Officer
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                    Response.Redirect("DashboardFinance.aspx");
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "7")//Division Officer
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                    Response.Redirect("DashboardFinance.aspx");
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "8")//Organizational Admin
            {
                if (Session["Person_BranchOffice_Id"].ToString() == "1" && Session["PersonJuridiction_DesignationId"].ToString() == "33")
                {
                    if (Session["Login_IsDefault"].ToString() == "1")
                        Response.Redirect("DashboardFinance.aspx");
                    else
                        Response.Redirect("ChangePassword.aspx");
                }
                else if (Session["Person_BranchOffice_Id"].ToString() == "1" && Session["PersonJuridiction_DesignationId"].ToString() == "1039")
                {
                    if (Session["Login_IsDefault"].ToString() == "1")
                        Response.Redirect("DashboardFinance.aspx");
                    else
                        Response.Redirect("ChangePassword.aspx");
                }
                else
                {
                    if (Session["Login_IsDefault"].ToString() == "1")
                        Response.Redirect("DashboardFinance.aspx");
                    else
                        Response.Redirect("ChangePassword.aspx");
                }
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "14")//Division Officer
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                    Response.Redirect("DashboardFinance.aspx");
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else
            {
                //Response.Redirect("Index.aspx");
            }
        }
        if (!IsPostBack)
        {
            string Default_Zone = ConfigurationManager.AppSettings.Get("Default_Zone");
            string Default_Circle = ConfigurationManager.AppSettings.Get("Default_Circle");
            string Default_Division = ConfigurationManager.AppSettings.Get("Default_Division");

            string Default_Scheme = ConfigurationManager.AppSettings.Get("Default_Scheme");
            string Base_Web_URL = ConfigurationManager.AppSettings.Get("Base_Web_URL");
            Session["Default_Zone"] = Default_Zone;
            Session["Default_Circle"] = Default_Circle;
            Session["Default_Division"] = Default_Division;
            Session["Client"] = Client;
            Session["Default_Scheme"] = Default_Scheme;
            Session["Base_Web_URL"] = Base_Web_URL;

            obj_tbl_ePaymentModules = new DataLayer().get_Modules();
            Session["tbl_ePaymentModules"] = obj_tbl_ePaymentModules;

            ViewState["OTP"] = "";
            ViewState["IsValid"] = "false";
            get_tbl_Zone();
        }
    }
    private void get_tbl_Zone()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlZone, "Zone_Name", "Zone_Id");
        }
        else
        {
            ddlZone.Items.Clear();
        }
    }

    private void get_tbl_Circle(int Zone_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Circle(Zone_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlCircle, "Circle_Name", "Circle_Id");
        }
        else
        {
            ddlCircle.Items.Clear();
        }
    }
    private void get_tbl_Division(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division(Circle_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDivision, "Division_Name", "Division_Id");
        }
        else
        {
            ddlDivision.Items.Clear();
        }
    }

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlZone.SelectedValue == "0")
        {
            ddlCircle.Items.Clear();
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
        }
    }

    protected void ddlCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCircle.SelectedValue == "0")
        {
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue));
        }
    }

    protected void btnGetEmployee_Click(object sender, EventArgs e)
    {
        ViewState["OTP"] = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).getLoginDetails_OTP(Zone_Id, Circle_Id, Division_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlPerson, "Person_Name_Full", "Person_Id");
        }
        else
        {
            ddlPerson.Items.Clear();
        }
    }


    protected void ddlPerson_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPerson.SelectedValue == "0")
        {
            ViewState["OTP"] = "";
        }
        else
        {
            string[] _data = ddlPerson.SelectedItem.Text.Split(new string[] { ", Mob: " }, StringSplitOptions.RemoveEmptyEntries);
            string Mobile_No = _data[1].Trim();
            string Name = _data[0].Trim();
            ViewState["OTP"] = "";
            Random rnd = new Random();
            string OTP = rnd.Next(10000, 99999).ToString();
            //string OTP = "1234";
            List<SMS_Objects> obj_SMS_Objects_Li = new List<SMS_Objects>();
            if (Mobile_No.Trim().Length == 10)
            {
                SMS_Objects obj_SMS_Objects = new SMS_Objects();
                obj_SMS_Objects.MobileNum = Mobile_No.Trim();
                obj_SMS_Objects.Sid = "EPMSUP";
                obj_SMS_Objects.Template_Id = "1207167203138495076";
                obj_SMS_Objects.SMS_Content = "Dear " + Name + ", OTP for login on PMS Web Application is " + OTP + ". From EPMSUP.";
                obj_SMS_Objects_Li.Add(obj_SMS_Objects);
            }
            if (obj_SMS_Objects_Li.Count == 0)
            {
                MessageBox.Show("Invalid Mobile No");
                ViewState["OTP"] = "";
            }
            else
            {
                ViewState["OTP"] = OTP;
                MessageBox.Show("OTP has been send to mobile No: " + Mobile_No.Trim());
                new Thread(() =>
                {
                    AllClasses.SendSMS(obj_SMS_Objects_Li);
                }).Start();
            }
        }   
    }

    protected void btnLoginULB_Click(object sender, EventArgs e)
    {
        if (ddlPerson.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Registred Employee..!!");
            ddlPerson.Focus();
            return;
        }
        if (ViewState["OTP"] == null)
        {
            MessageBox.Show("OTP Not Generated!!");
            return;
        }
        if (txtOTP.Text.Trim().Replace("'", "") == "")
        {
            MessageBox.Show("Please Provide OTP!!");
            txtOTP.Focus();
            return;
        }
        if (txtOTP.Text.Trim().Replace("'", "") == ViewState["OTP"].ToString())
        {
            DataSet ds = new DataSet();
            ds = new DataLayer().getLoginDetails(Convert.ToInt32(ddlPerson.SelectedValue));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataSet dsAdditional = (new DataLayer().get_PersonAdditionalArea_Login(Convert.ToInt32(ds.Tables[0].Rows[0]["Person_Id"].ToString())));
                if (dsAdditional != null && dsAdditional.Tables.Count > 0 && dsAdditional.Tables[0].Rows.Count > 1)
                {
                    ViewState["Login_Info"] = ds;
                    List<tbl_PersonAdditionalArea> obj_tbl_PersonAdditionalArea_Li = new List<tbl_PersonAdditionalArea>();
                    for (int i = 0; i < dsAdditional.Tables[0].Rows.Count; i++)
                    {
                        tbl_PersonAdditionalArea obj_tbl_PersonAdditionalArea = new tbl_PersonAdditionalArea();
                        obj_tbl_PersonAdditionalArea.Display_Text = dsAdditional.Tables[0].Rows[i]["DisplayText"].ToString().Trim();
                        obj_tbl_PersonAdditionalArea.PersonAdditionalArea_Id = Convert.ToInt32(dsAdditional.Tables[0].Rows[i]["PersonAdditionalArea_Id"].ToString().Trim());
                        obj_tbl_PersonAdditionalArea.PersonAdditionalArea_CircleId = Convert.ToInt32(dsAdditional.Tables[0].Rows[i]["PersonAdditionalArea_CircleId"].ToString().Trim());
                        obj_tbl_PersonAdditionalArea.PersonAdditionalArea_Designation_Id = Convert.ToInt32(dsAdditional.Tables[0].Rows[i]["PersonAdditionalArea_Designation_Id"].ToString().Trim());
                        obj_tbl_PersonAdditionalArea.PersonAdditionalArea_DivisionId = Convert.ToInt32(dsAdditional.Tables[0].Rows[i]["PersonAdditionalArea_DivisionId"].ToString().Trim());
                        obj_tbl_PersonAdditionalArea.PersonAdditionalArea_ZoneId = Convert.ToInt32(dsAdditional.Tables[0].Rows[i]["PersonAdditionalArea_ZoneId"].ToString().Trim());
                        obj_tbl_PersonAdditionalArea.Zone_Name = dsAdditional.Tables[0].Rows[i]["Zone_Name"].ToString().Trim();
                        obj_tbl_PersonAdditionalArea.Circle_Name = dsAdditional.Tables[0].Rows[i]["Circle_Name"].ToString().Trim();
                        obj_tbl_PersonAdditionalArea.Division_Name = dsAdditional.Tables[0].Rows[i]["Division_Name"].ToString().Trim();
                        obj_tbl_PersonAdditionalArea.Designation_Name = dsAdditional.Tables[0].Rows[i]["Designation_Name"].ToString().Trim();
                        obj_tbl_PersonAdditionalArea.Mode = dsAdditional.Tables[0].Rows[i]["Mode"].ToString().Trim();
                        obj_tbl_PersonAdditionalArea_Li.Add(obj_tbl_PersonAdditionalArea);
                    }
                    ddlAdditionalDivisionAndRole.DataTextField = "Display_Text";
                    ddlAdditionalDivisionAndRole.DataValueField = "PersonAdditionalArea_Id";
                    ddlAdditionalDivisionAndRole.DataSource = obj_tbl_PersonAdditionalArea_Li;
                    ddlAdditionalDivisionAndRole.DataBind();
                    ViewState["PersonAdditionalArea"] = obj_tbl_PersonAdditionalArea_Li;
                    mp1.Show();
                }
                else
                {
                    ViewState["PersonAdditionalArea"] = null;
                    ViewState["Login_Info"] = null;
                    init_Session(ds, null);
                }
            }
            else
            {
                MessageBox.Show("Invalid Login Credentials!!");
                return;
            }
        }
        else
        {
            MessageBox.Show("Please Provide Valid OTP!!");
            txtOTP.Focus();
            return;
        }
    }

    private void init_Session(DataSet ds, tbl_PersonAdditionalArea obj_tbl_PersonAdditionalArea)
    {
        string Client = ConfigurationManager.AppSettings.Get("Client");
        Session["LoginHistory_Id"] = (new DataLayer()).Insert_tbl_LoginHistory(ds.Tables[0].Rows[0]["Person_Id"].ToString());
        Session["User_Permission"] = (new DataLayer()).get_User_Permission(ds.Tables[0].Rows[0]["Person_BranchOffice_Id"].ToString().Trim(), ds.Tables[0].Rows[0]["PersonJuridiction_DesignationId"].ToString());
        Session["Module_Id"] = "1";
        Session["Login_Id"] = ds.Tables[0].Rows[0]["Login_Id"].ToString().Trim();
        Session["Person_BranchOffice_Id"] = ds.Tables[0].Rows[0]["Person_BranchOffice_Id"].ToString().Trim();
        Session["UserName"] = ds.Tables[0].Rows[0]["Login_UserName"].ToString().Trim();
        Session["UserType"] = ds.Tables[0].Rows[0]["PersonJuridiction_UserTypeId"].ToString();
        Session["UserTypeName"] = ds.Tables[0].Rows[0]["UserType_Desc_E"].ToString();
        Session["Person_Id"] = ds.Tables[0].Rows[0]["Person_Id"].ToString();
        Session["Person_Name"] = ds.Tables[0].Rows[0]["Person_Name"].ToString();
        Session["Person_Mobile1"] = ds.Tables[0].Rows[0]["Person_Mobile1"].ToString();
        Session["ServerDate"] = ds.Tables[0].Rows[0]["ServerDate"].ToString();
        Session["M_Level_Id"] = ds.Tables[0].Rows[0]["M_Level_Id"].ToString();
        Session["M_Jurisdiction_Id"] = ds.Tables[0].Rows[0]["M_Jurisdiction_Id"].ToString();

        Session["PersonJuridiction_DepartmentId"] = ds.Tables[0].Rows[0]["PersonJuridiction_DepartmentId"].ToString();
        Session["PersonJuridiction_Project_Id"] = ds.Tables[0].Rows[0]["PersonJuridiction_Project_Id"].ToString();
        Session["Department_Name"] = ds.Tables[0].Rows[0]["Department_Name"].ToString();
        if (obj_tbl_PersonAdditionalArea == null)
        {
            Session["PersonJuridiction_DesignationId"] = ds.Tables[0].Rows[0]["PersonJuridiction_DesignationId"].ToString();
            Session["Designation_DesignationName"] = ds.Tables[0].Rows[0]["Designation_DesignationName"].ToString();
            Session["Zone_Name"] = ds.Tables[0].Rows[0]["Zone_Name"].ToString();
            Session["Circle_Name"] = ds.Tables[0].Rows[0]["Circle_Name"].ToString();
            Session["Division_Name"] = ds.Tables[0].Rows[0]["Division_Name"].ToString();
            Session["PersonJuridiction_ZoneId"] = ds.Tables[0].Rows[0]["PersonJuridiction_ZoneId"].ToString();
            Session["PersonJuridiction_CircleId"] = ds.Tables[0].Rows[0]["PersonJuridiction_CircleId"].ToString();
            Session["PersonJuridiction_DivisionId"] = ds.Tables[0].Rows[0]["PersonJuridiction_DivisionId"].ToString();
        }
        else
        {
            Session["PersonJuridiction_DesignationId"] = obj_tbl_PersonAdditionalArea.PersonAdditionalArea_Designation_Id;
            Session["Designation_DesignationName"] = obj_tbl_PersonAdditionalArea.Designation_Name;
            Session["Zone_Name"] = obj_tbl_PersonAdditionalArea.Zone_Name;
            Session["Circle_Name"] = obj_tbl_PersonAdditionalArea.Circle_Name;
            Session["Division_Name"] = obj_tbl_PersonAdditionalArea.Division_Name;
            Session["PersonJuridiction_ZoneId"] = obj_tbl_PersonAdditionalArea.PersonAdditionalArea_ZoneId;
            Session["PersonJuridiction_CircleId"] = obj_tbl_PersonAdditionalArea.PersonAdditionalArea_CircleId;
            Session["PersonJuridiction_DivisionId"] = obj_tbl_PersonAdditionalArea.PersonAdditionalArea_DivisionId;
        }
        Session["TypingMode"] = "G";
        Session["District_Id"] = ds.Tables[0].Rows[0]["District_Id"].ToString();
        Session["District_Name"] = ds.Tables[0].Rows[0]["District_Name"].ToString();
        Session["ULB_Id"] = ds.Tables[0].Rows[0]["ULB_Id"].ToString();
        Session["ULB_Name"] = ds.Tables[0].Rows[0]["ULB_Name"].ToString();

        Session["Login_IsDefault"] = ds.Tables[0].Rows[0]["Login_IsDefault"].ToString();

        if (Session["Module_Id"].ToString() == "1")
        {
            if (Session["UserType"].ToString() == "1")//Administrator
            {
                Render_Notification();
                if (Session["Login_IsDefault"].ToString() == "1")
                {
                    if (Client == "CNDS")
                    {
                        Response.Redirect("Dashboard_PMIS_CNDS.aspx");
                    }
                    else if (Client == "SAR")
                    {
                        Response.Redirect("Dashboard_Sarovar.aspx");
                    }
                    else if (Client == "JNUP")
                    {
                        Response.Redirect("Dashboard_PMIS.aspx");
                    }
                    else
                    {
                        Response.Redirect("DashboardNew.aspx");
                    }
                }
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"].ToString() == "2")//District
            {
                Render_Notification();
                if (Session["Login_IsDefault"].ToString() == "1")
                {
                    if (Client == "CNDS")
                    {
                        Response.Redirect("Dashboard_PMIS_CNDS.aspx");
                    }
                    else if (Client == "SAR")
                    {
                        Response.Redirect("Dashboard_Sarovar.aspx");
                    }
                    else if (Client == "JNUP")
                    {
                        Response.Redirect("Dashboard_PMIS.aspx");
                    }
                    else
                    {
                        Response.Redirect("DashboardNew.aspx");
                    }
                }
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"].ToString() == "3")//ULB
            {
                Render_Notification();
                if (Session["Login_IsDefault"].ToString() == "1")
                {
                    if (Client == "CNDS")
                    {
                        Response.Redirect("Dashboard_PMIS_CNDS.aspx");
                    }
                    else if (Client == "SAR")
                    {
                        Response.Redirect("Dashboard_Sarovar.aspx");
                    }
                    else if (Client == "JNUP")
                    {
                        Response.Redirect("Dashboard_PMIS.aspx");
                    }
                    else
                    {
                        Response.Redirect("DashboardNew.aspx");
                    }
                }
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"].ToString() == "4")//Zone Officer
            {
                Render_Notification();
                if (Session["Login_IsDefault"].ToString() == "1")
                {
                    if (Client == "CNDS")
                    {
                        Response.Redirect("Dashboard_PMIS_CNDS.aspx");
                    }
                    else if (Client == "SAR")
                    {
                        Response.Redirect("Dashboard_Sarovar.aspx");
                    }
                    else if (Client == "JNUP")
                    {
                        Response.Redirect("Dashboard_PMIS.aspx");
                    }
                    else
                    {
                        Response.Redirect("DashboardNew.aspx");
                    }
                }
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"].ToString() == "5")//Contractor Officer
            {
                Render_Notification();
                if (Session["Login_IsDefault"].ToString() == "1")
                {
                    if (Client == "CNDS")
                    {
                        Response.Redirect("Dashboard_PMIS_CNDS.aspx");
                    }
                    else if (Client == "SAR")
                    {
                        Response.Redirect("Dashboard_Sarovar.aspx");
                    }
                    else if (Client == "JNUP")
                    {
                        Response.Redirect("Dashboard_PMIS.aspx");
                    }
                    else
                    {
                        Response.Redirect("DashboardNew.aspx");
                    }
                }
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"].ToString() == "6")//Circle Officer
            {
                Render_Notification();
                if (Session["Login_IsDefault"].ToString() == "1")
                {
                    if (Client == "CNDS")
                    {
                        Response.Redirect("Dashboard_PMIS_CNDS.aspx");
                    }
                    else if (Client == "SAR")
                    {
                        Response.Redirect("Dashboard_Sarovar.aspx");
                    }
                    else if (Client == "JNUP")
                    {
                        Response.Redirect("Dashboard_PMIS.aspx");
                    }
                    else
                    {
                        Response.Redirect("DashboardNew.aspx");
                    }
                }
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"].ToString() == "7")//Division Officer
            {
                Render_Notification();
                if (Session["Login_IsDefault"].ToString() == "1")
                {
                    if (Client == "CNDS")
                    {
                        Response.Redirect("Dashboard_PMIS_CNDS.aspx");
                    }
                    else if (Client == "SAR")
                    {
                        Response.Redirect("Dashboard_Sarovar.aspx");
                    }
                    else if (Client == "JNUP")
                    {
                        Response.Redirect("Dashboard_PMIS.aspx");
                    }
                    else
                    {
                        Response.Redirect("DashboardNew.aspx");
                    }
                }
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"].ToString() == "8")//Organizational Admin
            {
                Render_Notification();
                if (Client == "CNDS")
                {
                    Response.Redirect("Dashboard_PMIS_CNDS.aspx");
                }
                else if (Client == "SAR")
                {
                    Response.Redirect("Dashboard_Master.aspx");
                }
                else if (Client == "JNUP")
                {
                    Response.Redirect("Dashboard_PMIS.aspx");
                }
                else
                {
                    Response.Redirect("DashboardNew.aspx");
                }
            }
            else if (Session["UserType"].ToString() == "9")//Operator
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                {
                    if (Client == "CNDS")
                    {
                        Response.Redirect("Dashboard_PMIS_CNDS.aspx");
                    }
                    else if (Client == "SAR")
                    {
                        Response.Redirect("Dashboard_Sarovar.aspx");
                    }
                    else if (Client == "JNUP")
                    {
                        Response.Redirect("Dashboard_PMIS.aspx");
                    }
                    else
                    {
                        Response.Redirect("DashboardNew.aspx");
                    }
                }
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"].ToString() == "13")//PS
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                {
                    if (Client == "CNDS")
                    {
                        Response.Redirect("Dashboard_PMIS_CNDS.aspx");
                    }
                    else if (Client == "SAR")
                    {
                        Response.Redirect("Dashboard_Sarovar.aspx");
                    }
                    else if (Client == "JNUP")
                    {
                        Response.Redirect("Dashboard_PMIS.aspx");
                    }
                    else
                    {
                        Response.Redirect("DashboardNew.aspx");
                    }
                }
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"].ToString() == "14")//Operator
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                {
                    if (Client == "CNDS")
                    {
                        Response.Redirect("Dashboard_PMIS_CNDS.aspx");
                    }
                    else if (Client == "SAR")
                    {
                        Response.Redirect("Dashboard_Sarovar.aspx");
                    }
                    else if (Client == "JNUP")
                    {
                        Response.Redirect("Dashboard_PMIS.aspx");
                    }
                    else
                    {
                        Response.Redirect("DashboardNew.aspx");
                    }
                }
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else
            {
                MessageBox.Show("Not Authorized To Login...!!");
                return;
            }
        }
        else
        {
            if (Session["UserType"].ToString() == "1")//Administrator
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                    Response.Redirect("DashboardFinance.aspx");
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"].ToString() == "4")//Zone Officer
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                    Response.Redirect("DashboardFinance.aspx");
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"].ToString() == "6")//Circle Officer
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                    Response.Redirect("DashboardFinance.aspx");
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"].ToString() == "7")//Division Officer
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                    Response.Redirect("DashboardFinance.aspx");
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"].ToString() == "8")//Organizational Admin
            {
                if (Session["Person_BranchOffice_Id"].ToString() == "1" && Session["PersonJuridiction_DesignationId"].ToString() == "33")
                {
                    if (Session["Login_IsDefault"].ToString() == "1")
                        Response.Redirect("DashboardFinance.aspx");
                    else
                        Response.Redirect("ChangePassword.aspx");
                }
                else if (Session["Person_BranchOffice_Id"].ToString() == "1" && Session["PersonJuridiction_DesignationId"].ToString() == "1039")
                {
                    if (Session["Login_IsDefault"].ToString() == "1")
                        Response.Redirect("DashboardFinance.aspx");
                    else
                        Response.Redirect("ChangePassword.aspx");
                }
                else
                {
                    if (Session["Login_IsDefault"].ToString() == "1")
                        Response.Redirect("DashboardFinance.aspx");
                    else
                        Response.Redirect("ChangePassword.aspx");
                }
            }
            else if (Session["UserType"].ToString() == "9")//Operator
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                    Response.Redirect("DashboardFinance.aspx");
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else if (Session["UserType"].ToString() == "14")//Zone Officer
            {
                if (Session["Login_IsDefault"].ToString() == "1")
                    Response.Redirect("DashboardFinance.aspx");
                else
                    Response.Redirect("ChangePassword.aspx");
            }
            else
            {
                MessageBox.Show("Not Authorized To Login...!!");
                return;
            }
        }
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

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Notification_Dashboard(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, true);
        Session["ds_Notification"] = ds;
    }
    protected void btnLogin2_Click(object sender, EventArgs e)
    {
        DataSet ds = (DataSet)ViewState["Login_Info"];
        int PersonAdditionalArea_Id = Convert.ToInt32(ddlAdditionalDivisionAndRole.SelectedValue);

        List<tbl_PersonAdditionalArea> obj_tbl_PersonAdditionalArea_Li = new List<tbl_PersonAdditionalArea>();
        obj_tbl_PersonAdditionalArea_Li = (List<tbl_PersonAdditionalArea>)ViewState["PersonAdditionalArea"];
        int _indx = -1;
        for (int i = 0; i < obj_tbl_PersonAdditionalArea_Li.Count; i++)
        {
            if (obj_tbl_PersonAdditionalArea_Li[i].PersonAdditionalArea_Id == PersonAdditionalArea_Id && obj_tbl_PersonAdditionalArea_Li[i].Mode == "Additional")
            {
                _indx = i;
                break;
            }
        }
        if (_indx == -1)
        {
            init_Session(ds, null);
        }
        else
        {
            init_Session(ds, obj_tbl_PersonAdditionalArea_Li[_indx]);
        }
    }
}