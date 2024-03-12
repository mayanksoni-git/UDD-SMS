using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Web;

public partial class IndexUser : System.Web.UI.Page
{
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
            else
            {
                //Response.Redirect("Index.aspx");
            }
        }
        if (!IsPostBack)
        {
            txtMobileNo.Focus();
        }
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if (txtMobileReg.Text == "")
        {
            MessageBox.Show("Please Provide Registred Mobile No");
            return;
        }
        if (txtFirmName.Text == "")
        {
            MessageBox.Show("Please Provide Vendor Firm Name");
            return;
        }
        if (txtMobileNoAltername1.Text == "")
        {
            MessageBox.Show("Please Provide Alternate Mobile No");
            return;
        }
        if (ddlFirmType.SelectedValue == "J")
        {
            if (txtFirmNameJV.Text == "")
            {
                MessageBox.Show("Please Provide JV Firm Name");
                return;
            }
            if (txtMobileNoAltername2.Text == "")
            {
                MessageBox.Show("Please Provide Alternate Mobile No");
                return;
            }
        }
        if (txtContactPersonName.Text == "")
        {
            MessageBox.Show("Please Provide Contact Person Name");
            return;
        }
        if (txtPasswordReg.Text == "")
        {
            MessageBox.Show("Please Provide Password");
            return;
        }
        if (txtPasswordRegRe.Text == "")
        {
            MessageBox.Show("Please Provide Re-Password");
            return;
        }
        if (txtPasswordReg.Text != txtPasswordRegRe.Text)
        {
            MessageBox.Show("Please Provide Valid Matching Password");
            return;
        }
        int Person_Id = 0;
        if (new DataLayer().get_Vendor_Details(txtMobileReg.Text.Trim(), ref Person_Id))
        {
            tbl_Vendor obj_tbl_Vendor = new tbl_Vendor();
            obj_tbl_Vendor.Vendor_AddedBy = 0;
            obj_tbl_Vendor.Vendor_FirmName = txtFirmName.Text.Trim();
            obj_tbl_Vendor.Vendor_FirmNameJV = txtFirmNameJV.Text.Trim();
            obj_tbl_Vendor.Vendor_Name = txtContactPersonName.Text.Trim();
            obj_tbl_Vendor.Vendor_Status = 1;
            obj_tbl_Vendor.Vendor_MobileNo1 = txtMobileNoAltername1.Text.Trim();
            obj_tbl_Vendor.Vendor_MobileNo = txtMobileReg.Text.Trim();
            obj_tbl_Vendor.Vendor_MobileNo2 = txtMobileNoAltername2.Text.Trim();
            obj_tbl_Vendor.Vendor_Password = txtPasswordReg.Text.Trim();
            obj_tbl_Vendor.Vendor_Person_Id = Person_Id;
            if (new DataLayer().Insert_tbl_Vendor(obj_tbl_Vendor))
            {
                MessageBox.Show("Vendor Registration Successfully");
                return;
            }
            else
            {
                MessageBox.Show("Error In Vendor Registration");
                return;
            }
        }
        else
        {
            MessageBox.Show("The Mobile No. Entred is Not Registred On ePayment Portal Please Contact Concerning Executive Engineer.");
            return;
        }

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (txtMobileNo.Text == "")
        {
            MessageBox.Show("Please Provide Registred Mobile No");
            return;
        }
        if (txtPassword.Text == "")
        {
            MessageBox.Show("Please Provide Password");
            return;
        }
        DataSet ds = new DataSet();
        ds = new DataLayer().get_Vendor_Details_Login(txtMobileNo.Text.Trim(), txtPassword.Text.Trim());
        if (AllClasses.CheckDataSet(ds))
        {
            Session["Vendor_Id"] = ds.Tables[0].Rows[0]["Vendor_Id"].ToString();
            Session["Vendor_Person_Id"] = ds.Tables[0].Rows[0]["Vendor_Person_Id"].ToString();
            Session["Vendor_Name"] = ds.Tables[0].Rows[0]["Vendor_Name"].ToString();
            Session["ServerDate"] = ds.Tables[0].Rows[0]["ServerDate"].ToString();
            Session["UserType"] = "5";
            Response.Redirect("MasterEMBVendor.aspx");
        }
        else
        {
            MessageBox.Show("The Mobile No. Entred is Not Registred On ePayment Portal Please Contact Concerning Executive Engineer or Procees To Registration.");
            return;
        }
    }
}