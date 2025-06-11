using CrystalDecisions.CrystalReports.Engine;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
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
        txtUserName.Focus();
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

            ViewState["IsValid"] = "false";
            Session.Add("rno", 0);
            Random randomclass = new Random();
            Session["rno"] = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(randomclass.Next().ToString(), "MD5");
        }
        obj_tbl_ePaymentModules = new DataLayer().get_Modules();
        Session["tbl_ePaymentModules"] = obj_tbl_ePaymentModules;
        btnLogin.Attributes.Add("onclick", "javascript:return md5auth('" + Convert.ToString(Session["rno"]) + "');");
    }
    public string ReturnHash(string strPassword, string token)
    {
        string randomNo = token;
#pragma warning disable 618
        return FormsAuthentication.HashPasswordForStoringInConfigFile((randomNo + strPassword), "MD5");
#pragma warning restore 618
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (txtUserName.Text.Trim().Replace("'", "") == "")
        {
            MessageBox.Show("Please Provide Valid User Name..!!");
            txtUserName.Focus();
            return;
        }
        if (txtPassowrd.Text.Trim().Replace("'", "") == "")
        {
            MessageBox.Show("Please Provide Password!!");
            txtPassowrd.Focus();
            return;
        }
        DataSet ds = new DataSet();
        ds = new DataLayer().getLoginDetails(txtUserName.Text.Trim().Replace("'", ""));

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string pass_MD5 = AllClasses.CreateMD5(ds.Tables[0].Rows[0]["Login_password"].ToString().Trim());
            var orignalpassword = ReturnHash(pass_MD5.ToUpper(), Convert.ToString(Session["rno"]));
            string EnteredPassword = txtPassowrd.Text.Trim().Replace("'", "").Replace("<script>", "");

            if (txtPassowrd.Text.Trim().Replace("'", "").Replace("<script>", "") == orignalpassword)
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
                MessageBox.Show("Invalid Password!!");
                return;
            }
        }
        else
        {
            MessageBox.Show("Invalid Login Credentials or User Name / Mobile Number Not Registred!!");
            return;
        }
    }
    private void init_Session(DataSet ds, tbl_PersonAdditionalArea obj_tbl_PersonAdditionalArea)
    {
        string Client = ConfigurationManager.AppSettings.Get("Client");
        Session["LoginHistory_Id"] = (new DataLayer()).Insert_tbl_LoginHistory(ds.Tables[0].Rows[0]["Person_Id"].ToString());
        Session["User_Permission"] = (new DataLayer()).get_User_Permission(ds.Tables[0].Rows[0]["Person_BranchOffice_Id"].ToString().Trim(), ds.Tables[0].Rows[0]["PersonJuridiction_DesignationId"].ToString());
        
        if((ds.Tables[0].Rows[0]["Person_Id"].ToString())!=null)
        {
            Session["Profile_Pic"] = ds.Tables[0].Rows[0]["Person_ProfilePIC"].ToString();
        }
        else
        {
           
                Session["Profile_Pic"] = "assets/images/users/avatar-1.jpg";
           
        }
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
        Session["Division_Type"] = ds.Tables[0].Rows[0]["Division_Type"].ToString();
        string ULBTypeName = Session["Division_Type"].ToString().Trim();
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
            Session["MandalId"] = ds.Tables[0].Rows[0]["DivisionID"].ToString();
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
            else if (Session["UserType"].ToString() == "14")//Inspection
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
            else if (Session["UserType"].ToString() == "14")//Operator
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
    protected void btnStitchOTPLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("IndexUserOTP.aspx");
    }
}
