using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


public partial class MasterEmployee : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.MasterPageFile = SetMasterPage.ReturnPage();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Person_Id"] == null || Session["Login_Id"] == null)
        {
            Response.Redirect("Index.aspx");
        }
        if (!IsPostBack)
        {
            get_tbl_Department();
            get_Branch_Office_Details();
            get_tbl_Designation();
            get_tbl_Level();
            get_tbl_UserType();

        }
    }
    private void get_tbl_Department()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Department();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDepartment, "Department_Name", "Department_Id");
            if (Session["UserType"].ToString() != "1" && Session["UserType"].ToString() != "8")
            {
                ddlDepartment.SelectedValue = "1";
            }
            else
            {
                ddlDepartment.Items.Clear();
            }

        }
    }
    private void get_Branch_Office_Details()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Branch_Office_Details(0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlBranchOffice, "OfficeBranch_Name", "OfficeBranch_Id");
        }
        else
        {
            ddlBranchOffice.Items.Clear();
        }
    }
    private void get_tbl_Designation()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Designation();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDesignation, "Designation_DesignationName", "Designation_Id");
            if (ddlDesignation.Items.Count == 2)
            {
                ddlDesignation.SelectedIndex = 1;
            }


        }
        else
        {
            ddlDesignation.Items.Clear();

        }
    }
    private void get_tbl_Level()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_M_Level(false);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlLevel, "Level_Name", "M_Level_Id");
            ddlLevel.SelectedValue = "0";

            if (Session["UserType"].ToString() != "1" && Session["UserType"].ToString() != "8")
            {
                ddlLevel.SelectedValue = "8";
                ddlLevel.Enabled = false;
            }
        }
        else
        {
            ddlLevel.Items.Clear();
        }
    }
    private void get_tbl_UserType()
    {
        string UserTypeId = "";

        if (Session["UserType"].ToString() == "1")
        {
            UserTypeId = "1, 2, 4, 6, 7, 8, 11, 3";
        }
        else
        {
            UserTypeId = Session["UserType"].ToString();
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_UserType(UserTypeId);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlUserType, "UserType_Desc_E", "UserType_Id");
            if (Session["UserType"].ToString() != "1" && Session["UserType"].ToString() != "8")
            {
                ddlUserType.SelectedValue = "7";
                ddlUserType.Enabled = false;
            }
        }
        else
        {
            ddlUserType.Items.Clear();
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string Msg = "";
        tbl_MasterEmployee obj_tbl_MasterEmployee = new tbl_MasterEmployee();
        if (hf_MasterEmployee_Id.Value == "0" || hf_MasterEmployee_Id.Value == "")
        {
            obj_tbl_MasterEmployee.MasterEmployee_Id = 0;
        }
        else
        {
            try
            {
                obj_tbl_MasterEmployee.MasterEmployee_Id = Convert.ToInt32(hf_MasterEmployee_Id.Value);
            }
            catch
            {
                obj_tbl_MasterEmployee.MasterEmployee_Id = 0;
            }
        }
        obj_tbl_MasterEmployee.MasterEmployee_Name = txtPersonName.Text.Trim();
        obj_tbl_MasterEmployee.MasterEmployee_Father_Name = txtPersonFName.Text.Trim();
        obj_tbl_MasterEmployee.MasterEmployee_Mobile_Number = txtMobileNo1.Text.Trim();
        obj_tbl_MasterEmployee.MasterEmployee_Landline_Number = txtLandLine.Text.Trim();
        obj_tbl_MasterEmployee.MasterEmployee_Email_Id = txtEmailId.Text.Trim();
        obj_tbl_MasterEmployee.MasterEmployee_Address = txtAddress.Text.Trim();
        obj_tbl_MasterEmployee.MasterEmployee_Organization = ddlBranchOffice.SelectedValue;
        obj_tbl_MasterEmployee.MasterEmployee_Employee_Code = txtEmployeeCode.Text.Trim();
        obj_tbl_MasterEmployee.MasterEmployee_Deparement = ddlDepartment.SelectedValue;
        obj_tbl_MasterEmployee.MasterEmployee_Designation = ddlDesignation.SelectedValue;
        obj_tbl_MasterEmployee.MasterEmployee_Level = ddlLevel.SelectedValue;
        obj_tbl_MasterEmployee.MasterEmployee_Reporting_Manager = txtEmployeeManager.Text.Trim();
        obj_tbl_MasterEmployee.MasterEmployee_User_Type = ddlUserType.SelectedValue;
        obj_tbl_MasterEmployee.MasterEmployee_high_School = txtHigh.Text.Trim();
        obj_tbl_MasterEmployee.MasterEmployee_Intermediate = txtInter.Text.Trim();
        obj_tbl_MasterEmployee.MasterEmployee_Graduation = txtGraduation.Text.Trim();
        obj_tbl_MasterEmployee.MasterEmployee_Post_Graduation = txtPostGraduation.Text.Trim();
        obj_tbl_MasterEmployee.MasterEmployee_Other = txtOther.Text.Trim();

        obj_tbl_MasterEmployee.MasterEmployee_Status=1;

        if ((new DataLayer()).Insert_tbl_MasterEmployee(obj_tbl_MasterEmployee, ref Msg))
        {
            MessageBox.Show("Master Created Successfully!");
            return;
        }
        else
        {
            MessageBox.Show("Error In Creating Pension Master!");
            return;
        }

    }

}