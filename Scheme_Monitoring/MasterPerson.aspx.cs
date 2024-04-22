﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPerson : System.Web.UI.Page
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
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();

            lblZoneHS.Text = Session["Default_Zone"].ToString();
            lblCircleHS.Text = Session["Default_Circle"].ToString();
            lblDivisionHS.Text = Session["Default_Division"].ToString();

            lblZone.Text = Session["Default_Zone"].ToString();
            lblCircle.Text = Session["Default_Circle"].ToString();
            lblDivision.Text = Session["Default_Division"].ToString();

            get_tbl_Project();
            get_tbl_ProjectSearch();
            get_tbl_UserType();
            get_tbl_Zone();
            get_tbl_Zone_Addditional();
            //Branch Office
            get_Branch_Office_Details();
            //Department
            get_tbl_Department();
            //Designation
            get_tbl_Designation();
            
            get_tbl_Designation_Additional();
            //Level
            get_tbl_Level();
            txtUserName.Text = (new DataLayer()).get_tbl_Employee_User_Name(null, null);
            txtUserName.Enabled = false;

            if (Session["UserType"].ToString() == "4" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {//Zone
                try
                {
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZoneS.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZoneS_SelectedIndexChanged(ddlZoneS, e);
                    ddlZone.Enabled = false;
                    ddlZoneS.Enabled = false;
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {
                try
                {
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZoneS.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZoneS_SelectedIndexChanged(ddlZoneS, e);
                    ddlZone.Enabled = false;
                    ddlZoneS.Enabled = false;
                    if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircle_SelectedIndexChanged(ddlCircle, e);
                            ddlCircle.Enabled = false;

                            ddlCircleS.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircleS_SelectedIndexChanged(ddlCircleS, e);
                            ddlCircleS.Enabled = false;
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {
                try
                {
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZoneS.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZoneS_SelectedIndexChanged(ddlZoneS, e);
                    ddlZone.Enabled = false;
                    ddlZoneS.Enabled = false;
                    if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircle_SelectedIndexChanged(ddlCircle, e);
                            ddlCircle.Enabled = false;

                            ddlCircleS.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircleS_SelectedIndexChanged(ddlCircleS, e);
                            ddlCircleS.Enabled = false;

                            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                            {//Circle
                                try
                                {
                                    ddlDivision.SelectedValue = Session["PersonJuridiction_DivisionId"].ToString();
                                    if (Session["PersonJuridiction_DivisionId"].ToString() == "" || Session["PersonJuridiction_DivisionId"].ToString() == "0")
                                    {
                                        ddlDivision.Enabled = true;
                                    }
                                    else
                                    {
                                        ddlDivision.Enabled = false;
                                    }
                                    ddlDivisionS.SelectedValue = Session["PersonJuridiction_DivisionId"].ToString();
                                    if (Session["PersonJuridiction_DivisionId"].ToString() == "" || Session["PersonJuridiction_DivisionId"].ToString() == "0")
                                    {
                                        ddlDivisionS.Enabled = true;
                                    }
                                    else
                                    {
                                        ddlDivisionS.Enabled = false;
                                    }
                                    ddlDivisionS_SelectedIndexChanged(ddlDivisionS, e);
                                }
                                catch
                                { }
                            }
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "1")
            {
                btnAddNew.Visible = true;
                //get_Employee(-1, 0, 0);
            }
            else if (Session["UserType"].ToString() == "8")
            {
                btnAddNew.Visible = true;
                //get_Employee(-1, 0, 0);
            }
            else
            {
                //btnAddNew.Visible = false;
            }
        }
    }

    private void get_tbl_Project()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Project(0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlProjectMaster.DataTextField = "Project_Name";
            ddlProjectMaster.DataValueField = "Project_Id";
            ddlProjectMaster.DataSource = ds.Tables[0];
            ddlProjectMaster.DataBind();
        }
        else
        {
            ddlProjectMaster.Items.Clear();
        }
    }
    private void get_tbl_ProjectSearch()
    {
        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_Project(0);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Project(Convert.ToInt32(Session["Person_Id"].ToString()));
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlSearchScheme, "Project_Name", "Project_Id");
            try
            {
                ddlSearchScheme.SelectedValue = Session["Default_Scheme"].ToString();
            }
            catch
            {
                ddlSearchScheme.SelectedIndex = 1;
            }
        }
        else
        {
            ddlSearchScheme.Items.Clear();
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
    private void get_Employee(int Zone_Id, int Circle_Id, int Division_Id)
    {
        string UserTypeId = "1, 2, 4, 6, 7, 8, 9, 11, 3, 13, 14";
        int District_Id = 0;
        int Project_Id = 0;
        //try
        //{
        //    District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        //}
        //catch
        //{
        //    District_Id = 0;
        //}
        try
        {
            Project_Id = Convert.ToInt32(ddlSearchScheme.SelectedValue);
        }
        catch
        {
            Project_Id = 0;
        }
        string Designation_Ids = "";
        foreach (ListItem listItem in lbDesignationS.Items)
        {
            if (listItem.Selected)
            {
                Designation_Ids += listItem.Value + ", ";
            }
        }
        if (Designation_Ids != "")
            Designation_Ids = Designation_Ids.Trim().Substring(0, Designation_Ids.Trim().Length - 1);
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Employee(UserTypeId, 0, 0, District_Id, Zone_Id, Circle_Id, Division_Id, 0, Designation_Ids, txtSearchMobile.Text.Trim(), 0, 0, Project_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
        }
        else
        {
            grdPost.DataSource = null;
            grdPost.DataBind();
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
        }
        else
        {
            ddlDepartment.Items.Clear();
        }
    }

    private void get_Employee_Reporting_Manager(int Designation_Id, int Zone_Id, int Circle_Id, int Division_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Employee_Reporting_Manager(Designation_Id, Zone_Id, Circle_Id, Division_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlReportingManager, "Person_Name_Full", "Person_Id");
        }
        else
        {
            ddlReportingManager.Items.Clear();
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

            lbDesignationS.DataTextField = "Designation_DesignationName";
            lbDesignationS.DataValueField = "Designation_Id";
            lbDesignationS.DataSource = ds.Tables[0];
            lbDesignationS.DataBind();
        }
        else
        {
            ddlDesignation.Items.Clear();
            lbDesignationS.Items.Clear();
        }
    }

    private void get_tbl_Zone()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlZone, "Zone_Name", "Zone_Id");
            AllClasses.FillDropDown_WithOutSelect(ds.Tables[0], ddlZoneS, "Zone_Name", "Zone_Id");
        }
        else
        {
            ddlZone.Items.Clear();
            ddlZoneS.Items.Clear();
        }
    }

    private void get_tbl_UserType()
    {
        string UserTypeId = "";

        if (Session["UserType"].ToString() == "1")
        {
            UserTypeId = "1, 2, 4, 6, 7, 8, 9, 11, 3, 13, 14";
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
        
    private void get_tbl_Level()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_M_Level(false);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlLevel, "Level_Name", "M_Level_Id");
            ddlLevel.SelectedValue = "0";

            ddlDivision.Visible = false;
            lblDivision.Visible = false;

            ddlCircle.Visible = false;
            lblCircle.Visible = false;

            ddlZone.Visible = false;
            lblZone.Visible = false;
            if (Session["UserType"].ToString() != "1" && Session["UserType"].ToString() != "8")
            {
                ddlLevel.SelectedValue = "8";
                ddlLevel.Enabled = false;
                ddlLevel_SelectedIndexChanged(null, null);
            }
        }
        else
        {
            ddlLevel.Items.Clear();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        tbl_PersonDetail obj_PersonDetail = new tbl_PersonDetail();
        try
        {
            obj_PersonDetail.Person_Id = Convert.ToInt32(hf_Person_Id.Value);
        }
        catch
        {
            obj_PersonDetail.Person_Id = 0;
        }
        if (ddlProjectMaster.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlProjectMaster.Focus();
            return;
        }
        if (txtPersonName.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Employee Name");
            txtPersonName.Focus();
            return;
        }
        //if (txtPersonFName.Text.Trim() == "")
        //{
        //    MessageBox.Show("Please Provide Employee Father's Name");
        //    txtPersonFName.Focus();
        //    return;
        //}
        if (txtMobileNo1.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide Employee Mobile No");
            txtMobileNo1.Focus();
            return;
        }
        if (txtMobileNo1.Text.Trim().Length != 10)
        {
            MessageBox.Show("Please Provide Correct Mobile No");
            txtMobileNo1.Focus();
            return;
        }
        if (obj_PersonDetail.Person_Id == 0)
        {
            if (txtConfirmPassword.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Password Confirmation");
                txtConfirmPassword.Focus();
                return;
            }
            if (txtPassowrd.Text.Trim() == "")
            {
                MessageBox.Show("Please Provide Password");
                txtPassowrd.Focus();
                return;
            }
            if (txtPassowrd.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                MessageBox.Show("Password and Confirm Password Does Not Match");
                txtPassowrd.Focus();
                return;
            }
        }
        if (txtUserName.Text.Trim() == "")
        {
            MessageBox.Show("Please Provide User Name");
            txtUserName.Focus();
            return;
        }
        if (ddlDepartment.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Department");
            ddlDepartment.Focus();
            return;
        }
        if (ddlDesignation.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Designation");
            ddlDesignation.Focus();
            return;
        }
        if (ddlUserType.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Access Role");
            ddlUserType.Focus();
            return;
        }
        if (ddlLevel.SelectedValue == "6" && ddlZone.SelectedValue == "0")
        {
            MessageBox.Show("Please Select " + Session["Default_Zone"].ToString() + "");
            ddlZone.Focus();
            return;
        }
        if (ddlLevel.SelectedValue == "7" && ddlCircle.SelectedValue == "0")
        {
            MessageBox.Show("Please Select " + Session["Default_Circle"].ToString() + "");
            ddlCircle.Focus();
            return;
        }
        if (ddlLevel.SelectedValue == "8" && ddlDivision.SelectedValue == "0")
        {
            MessageBox.Show("Please Select " + Session["Default_Division"].ToString() + "");
            ddlDivision.Focus();
            return;
        }
        if (ddlBranchOffice.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Branch Office");
            ddlBranchOffice.Focus();
            return;
        }
        obj_PersonDetail.Person_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        try
        {
            obj_PersonDetail.Person_Id = Convert.ToInt32(hf_Person_Id.Value);
        }
        catch
        {
            obj_PersonDetail.Person_Id = 0;
        }
        obj_PersonDetail.Person_BranchOffice_Id = Convert.ToInt32(ddlBranchOffice.SelectedValue);
        obj_PersonDetail.Person_AddressLine1 = txtAddress.Text.Trim();
        obj_PersonDetail.Person_EmailId = txtEmailId.Text.Trim();
        obj_PersonDetail.PersonDetail_Code = txtEmployeeCode.Text.Trim();
        obj_PersonDetail.Person_Mobile1 = txtMobileNo1.Text.Trim();
        obj_PersonDetail.Person_Mobile2 = txtMobileNo2.Text.Trim();
        obj_PersonDetail.Person_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_PersonDetail.Person_Name = txtPersonName.Text.Trim();
        obj_PersonDetail.Person_FName = txtPersonFName.Text.Trim();
        obj_PersonDetail.Person_Status = 1;

        tbl_PersonJuridiction obj_PersonJuridiction = new tbl_PersonJuridiction();
        try
        {
            obj_PersonJuridiction.PersonJuridiction_Id = Convert.ToInt32(hf_PersonJuridiction_Id.Value);
        }
        catch
        {
            obj_PersonJuridiction.PersonJuridiction_Id = 0;
        }
        try
        {
            obj_PersonJuridiction.PersonJuridiction_PersonId = Convert.ToInt32(hf_Person_Id.Value);
        }
        catch
        {
            obj_PersonJuridiction.PersonJuridiction_PersonId = 0;
        }
        obj_PersonJuridiction.M_Level_Id = Convert.ToInt32(ddlLevel.SelectedValue);
        if (obj_PersonJuridiction.M_Level_Id == 6)
        {
            obj_PersonJuridiction.PersonJuridiction_ZoneId = Convert.ToInt32(ddlZone.SelectedValue);
        }
        else if (obj_PersonJuridiction.M_Level_Id == 7)
        {
            obj_PersonJuridiction.PersonJuridiction_ZoneId = Convert.ToInt32(ddlZone.SelectedValue);
            obj_PersonJuridiction.PersonJuridiction_CircleId = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        else if (obj_PersonJuridiction.M_Level_Id == 8)
        {
            obj_PersonJuridiction.PersonJuridiction_ZoneId = Convert.ToInt32(ddlZone.SelectedValue);
            obj_PersonJuridiction.PersonJuridiction_CircleId = Convert.ToInt32(ddlCircle.SelectedValue);
            obj_PersonJuridiction.PersonJuridiction_DivisionId = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        obj_PersonJuridiction.PersonJuridiction_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_PersonJuridiction.PersonJuridiction_DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
        obj_PersonJuridiction.PersonJuridiction_DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);
        obj_PersonJuridiction.PersonJuridiction_ModifiedBy = Convert.ToInt32(Session["Person_Id"].ToString());

        obj_PersonJuridiction.PersonJuridiction_Project_Id = 0;

        try
        {
            obj_PersonJuridiction.PersonJuridiction_ParentPerson_Id = Convert.ToInt32(ddlReportingManager.SelectedValue);
        }
        catch
        {
            obj_PersonJuridiction.PersonJuridiction_ParentPerson_Id = 0;
        }
        obj_PersonJuridiction.PersonJuridiction_Status = 1;

        obj_PersonJuridiction.PersonJuridiction_UserTypeId = Convert.ToInt32(ddlUserType.SelectedValue);

        List<tbl_PersonDetail_Scheme> obj_tbl_PersonDetail_Scheme_Li = new List<tbl_PersonDetail_Scheme>();

        foreach (ListItem listItem in ddlProjectMaster.Items)
        {
            if (listItem.Selected)
            {
                tbl_PersonDetail_Scheme obj_tbl_PersonDetail_Scheme = new tbl_PersonDetail_Scheme();
                obj_tbl_PersonDetail_Scheme.PersonDetail_Scheme_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_PersonDetail_Scheme.PersonDetail_Scheme_Project_Id = Convert.ToInt32(listItem.Value);
                obj_tbl_PersonDetail_Scheme.PersonDetail_Scheme_Status = 1;
                obj_tbl_PersonDetail_Scheme_Li.Add(obj_tbl_PersonDetail_Scheme);
            }
        }

        if (obj_tbl_PersonDetail_Scheme_Li == null || obj_tbl_PersonDetail_Scheme_Li.Count == 0)
        {
            MessageBox.Show("Please Provide Scheme!");
            ddlProjectMaster.Focus();
            return;
        }

        List<tbl_PersonAdditionalCharge> obj_tbl_PersonAdditionalCharge_Li = new List<tbl_PersonAdditionalCharge>();
        
        List<tbl_PersonAdditionalArea> obj_tbl_PersonAdditionalArea_Li = new List<tbl_PersonAdditionalArea>();
        for (int i = 0; i < dgvAdditionalDivision.Rows.Count; i++)
        {
            tbl_PersonAdditionalArea obj_tbl_PersonAdditionalArea = new tbl_PersonAdditionalArea();
            obj_tbl_PersonAdditionalArea.PersonAdditionalArea_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            try
            {
                obj_tbl_PersonAdditionalArea.PersonAdditionalArea_ZoneId = Convert.ToInt32(dgvAdditionalDivision.Rows[i].Cells[1].Text.Trim());
            }
            catch
            {
                obj_tbl_PersonAdditionalArea.PersonAdditionalArea_ZoneId = 0;
            }
            try
            {
                obj_tbl_PersonAdditionalArea.PersonAdditionalArea_CircleId = Convert.ToInt32(dgvAdditionalDivision.Rows[i].Cells[2].Text.Trim());
            }
            catch
            {
                obj_tbl_PersonAdditionalArea.PersonAdditionalArea_CircleId = 0;
            }
            try
            {
                obj_tbl_PersonAdditionalArea.PersonAdditionalArea_DivisionId = Convert.ToInt32(dgvAdditionalDivision.Rows[i].Cells[3].Text.Trim());
            }
            catch
            {
                obj_tbl_PersonAdditionalArea.PersonAdditionalArea_DivisionId = 0;
            }
            try
            {
                obj_tbl_PersonAdditionalArea.PersonAdditionalArea_Designation_Id = Convert.ToInt32(dgvAdditionalDivision.Rows[i].Cells[0].Text.Trim());
            }
            catch
            {
                obj_tbl_PersonAdditionalArea.PersonAdditionalArea_Designation_Id = 0;
            }
            obj_tbl_PersonAdditionalArea.PersonAdditionalArea_Status = 1;
            obj_tbl_PersonAdditionalArea_Li.Add(obj_tbl_PersonAdditionalArea);
        }

        tbl_UserLogin obj_UserLogin = new tbl_UserLogin();
        try
        {
            obj_UserLogin.Login_PersonId = Convert.ToInt32(hf_Person_Id.Value);
        }
        catch
        {
            obj_UserLogin.Login_PersonId = 0;
        }
        obj_UserLogin.Login_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_UserLogin.Login_password = txtPassowrd.Text.Trim();
        obj_UserLogin.Login_Status = 1;
        obj_UserLogin.Login_UserName = txtUserName.Text.Trim();
        string msg = "";
        if ((new DataLayer()).Insert_Employee(obj_PersonDetail, obj_PersonJuridiction, obj_UserLogin, Convert.ToInt32(hf_Person_Id.Value), null, null, obj_tbl_PersonAdditionalArea_Li, null, obj_tbl_PersonAdditionalCharge_Li, obj_tbl_PersonDetail_Scheme_Li, ref msg))
        {
            if (msg == "")
            {
                if (Convert.ToInt32(hf_Person_Id.Value) > 0)
                {
                    MessageBox.Show("Employee Updated Successfully!");
                }
                else
                {
                    MessageBox.Show("Employee Created Successfully!");
                }
                reset();
                return;
            }
            else
            {
                MessageBox.Show(msg);
                return;
            }
        }
        else
        {
            MessageBox.Show("Error In Creating Employee!");
            return;
        }
    }

    private void reset()
    {
        hf_PersonJuridiction_Id.Value = "0";
        hf_Person_Id.Value = "0";
        txtAddress.Text = "";
        ddlDepartment.SelectedValue = "0";
        if (Session["UserType"].ToString() != "1" && Session["UserType"].ToString() != "8")
        {
            ddlDepartment.SelectedValue = "1";
        }
        ddlDesignation.SelectedValue = "0";
        txtEmailId.Text = "";
        txtMobileNo1.Text = "";
        txtMobileNo2.Text = "";
        txtAddress.Text = "";
        txtEmailId.Text = "";
        txtPersonFName.Text = "";
        txtPersonName.Text = "";
        txtConfirmPassword.Text = "";
        txtPassowrd.Text = "";
        txtUserName.Text = "";
        divLoginDetails.Visible = true;

        if (ddlZoneS.SelectedValue == "0")
        {
            get_Employee(-1, 0, 0);
        }
        else
        {
            int Zone_Id = 0;
            int Circle_Id = 0;
            int Division_Id = 0;
            try
            {
                Zone_Id = Convert.ToInt32(ddlZoneS.SelectedValue);
            }
            catch
            {
                Zone_Id = 0;
            }
            try
            {
                Circle_Id = Convert.ToInt32(ddlCircleS.SelectedValue);
            }
            catch
            {
                Circle_Id = 0;
            }
            try
            {
                Division_Id = Convert.ToInt32(ddlDivisionS.SelectedValue);
            }
            catch
            {
                Division_Id = 0;
            }
            get_Employee(Zone_Id, Circle_Id, Division_Id);
        }
        txtUserName.Text = (new DataLayer()).get_tbl_Employee_User_Name(null, null);
        txtUserName.Enabled = false;
        divCreateNew.Visible = false;
        if (Session["UserType"].ToString() == "1" && Session["UserType"].ToString() == "8")
        {
            ddlUserType.SelectedValue = "0";
            ddlLevel.SelectedValue = "0";
            ddlLevel_SelectedIndexChanged(ddlLevel, new EventArgs());
        }
        foreach (ListItem listItem in ddlProjectMaster.Items)
        {
            listItem.Selected = false;
        }

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLevel.SelectedValue == "0")
        {
            ddlCircle.Items.Clear();
            ddlDivision.Items.Clear();
            lblZone.Visible = false;
            ddlZone.Visible = false;
            lblCircle.Visible = false;
            ddlCircle.Visible = false;
            lblDivision.Visible = false;
            ddlDivision.Visible = false;
        }
        else if (ddlLevel.SelectedValue == "6")
        {//Zone
            ddlZone.SelectedValue = "0";
            ddlCircle.Items.Clear();
            ddlDivision.Items.Clear();
            lblZone.Visible = true;
            ddlZone.Visible = true;
            lblCircle.Visible = false;
            ddlCircle.Visible = false;
            lblDivision.Visible = false;
            ddlDivision.Visible = false;
        }
        else if (ddlLevel.SelectedValue == "7")
        {//Circle
            ddlZone.SelectedValue = "0";
            ddlCircle.Items.Clear();
            ddlDivision.Items.Clear();
            lblZone.Visible = true;
            ddlZone.Visible = true;
            lblCircle.Visible = true;
            ddlCircle.Visible = true;
            lblDivision.Visible = false;
            ddlDivision.Visible = false;
        }
        else if (ddlLevel.SelectedValue == "8")
        {//Division
            ddlZone.SelectedValue = "0";
            ddlCircle.Items.Clear();
            ddlDivision.Items.Clear();
            lblZone.Visible = true;
            ddlZone.Visible = true;
            lblCircle.Visible = true;
            ddlCircle.Visible = true;
            lblDivision.Visible = true;
            ddlDivision.Visible = true;
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int Person_Id_Delete = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        if (new DataLayer().Delete_Person(Person_Id_Delete, Person_Id))
        {
            MessageBox.Show("Deleted Successfully!!");
            int Zone_Id = 0;
            int Circle_Id = 0;
            int Division_Id = 0;
            try
            {
                Zone_Id = Convert.ToInt32(ddlZoneS.SelectedValue);
            }
            catch
            {
                Zone_Id = 0;
            }
            try
            {
                Circle_Id = Convert.ToInt32(ddlCircleS.SelectedValue);
            }
            catch
            {
                Circle_Id = 0;
            }
            try
            {
                Division_Id = Convert.ToInt32(ddlDivisionS.SelectedValue);
            }
            catch
            {
                Division_Id = 0;
            }
            get_Employee(Zone_Id, Division_Id, Circle_Id);
            return;
        }
        else
        {
            MessageBox.Show("Error In Deletion!!");
            return;
        }
    }

    protected void grdPost_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        if (gv.Rows.Count > 0)
        {
            //This replaces <td> with <th> and adds the scope attribute
            gv.UseAccessibleHeader = true;
        }
        if ((gv.ShowHeader == true && gv.Rows.Count > 0) || (gv.ShowHeaderWhenEmpty == true))
        {
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gv.ShowFooter == true && gv.Rows.Count > 0)
        {
            gv.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }

    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        M_Jurisdiction_Detailed obj = new M_Jurisdiction_Detailed();
        ImageButton lnkUpdate = sender as ImageButton;
        for (int i = 0; i < grdPost.Rows.Count; i++)
        {
            grdPost.Rows[i].BackColor = Color.Transparent;
        }
        (lnkUpdate.Parent.Parent as GridViewRow).BackColor = Color.LightGreen;
        divCreateNew.Visible = true;
        divLoginDetails.Visible = false;
        hf_PersonJuridiction_Id.Value = (lnkUpdate.Parent.Parent as GridViewRow).Cells[1].Text.Trim();
        hf_Person_Id.Value = (lnkUpdate.Parent.Parent as GridViewRow).Cells[0].Text.Trim();
        GridViewRow grd = lnkUpdate.Parent.Parent as GridViewRow;
        try
        {
            ddlLevel.SelectedValue = grd.Cells[2].Text.Replace("&nbsp;", "").Trim();
            ddlLevel_SelectedIndexChanged(ddlLevel, e);
        }
        catch
        {
            ddlLevel.SelectedValue = "0";
        }
        string M_Jurisdiction_Id = "0";
        if (ddlLevel.SelectedValue == "11")
        {
            M_Jurisdiction_Id = grd.Cells[13].Text.Replace("&nbsp;", "").Trim();
        }
        else
        {
            M_Jurisdiction_Id = grd.Cells[3].Text.Replace("&nbsp;", "").Trim();
        }
        obj = (new DataLayer()).get_M_Jurisdiction_Detailed(ddlLevel.SelectedValue, M_Jurisdiction_Id);
        
        try
        {
            ddlZone.SelectedValue = grd.Cells[8].Text.Replace("&nbsp;", "").Trim();
            ddlZone_SelectedIndexChanged(ddlZone, e);
        }
        catch
        {
            ddlZone.SelectedValue = "0";
        }
        try
        {
            ddlCircle.SelectedValue = grd.Cells[9].Text.Replace("&nbsp;", "").Trim();
            ddlCircle_SelectedIndexChanged(ddlCircle, e);
        }
        catch
        {
            ddlCircle.SelectedValue = "0";
        }
        try
        {
            ddlDivision.SelectedValue = grd.Cells[10].Text.Replace("&nbsp;", "").Trim();
        }
        catch
        {
            ddlDivision.SelectedValue = "0";
        }
        try
        {
            ddlDesignation.SelectedValue = grd.Cells[4].Text.Replace("&nbsp;", "").Trim();
            ddlDesignation_SelectedIndexChanged(ddlDesignation, e);
        }
        catch
        {
            ddlDesignation.SelectedValue = "0";
        }
        try
        {
            ddlDepartment.SelectedValue = grd.Cells[5].Text.Replace("&nbsp;", "").Trim();
        }
        catch
        {
            ddlDepartment.SelectedValue = "0";
        }
        try
        {
            ddlUserType.SelectedValue = grd.Cells[6].Text.Replace("&nbsp;", "").Trim();
        }
        catch
        {
            ddlUserType.SelectedValue = "0";
        }
        try
        {
            ddlReportingManager.SelectedValue = grd.Cells[7].Text.Replace("&nbsp;", "").Trim();
        }
        catch
        {
            ddlReportingManager.SelectedValue = "0";
        }
        try
        {
            ddlBranchOffice.SelectedValue = grd.Cells[11].Text.Replace("&nbsp;", "").Trim();
        }
        catch
        { }
        txtPersonName.Text = grd.Cells[16].Text.Replace("&nbsp;", "").Trim();
        txtPersonFName.Text = grd.Cells[17].Text.Replace("&nbsp;", "").Trim();
        txtAddress.Text = grd.Cells[21].Text.Replace("&nbsp;", "").Trim();
        txtEmailId.Text = grd.Cells[22].Text.Replace("&nbsp;", "").Trim();
        txtLandLine.Text = grd.Cells[18].Text.Replace("&nbsp;", "").Trim();
        txtMobileNo1.Text = grd.Cells[19].Text.Replace("&nbsp;", "").Trim();
        txtMobileNo2.Text = grd.Cells[20].Text.Replace("&nbsp;", "").Trim();

        string[] List_ReportingStaffJEAPE = grd.Cells[32].Text.Replace("&nbsp;", "").Trim().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        if (List_ReportingStaffJEAPE.Length > 0)
        {
            for (int i = 0; i < List_ReportingStaffJEAPE.Length; i++)
            {
                foreach (ListItem listItem in ddlProjectMaster.Items)
                {
                    if (List_ReportingStaffJEAPE[i].Trim() == listItem.Value)
                    {
                        listItem.Selected = true;
                        break;
                    }
                }
            }
        }
        else
        {
            foreach (ListItem listItem in ddlProjectMaster.Items)
            {
                listItem.Selected = false;
            }
        }

        txtEmployeeCode.Text = grd.Cells[34].Text.Replace("&nbsp;", "").Trim();

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PersonAdditionalArea_Edit(Convert.ToInt32(hf_Person_Id.Value));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["AdditionalDivision"] = ds.Tables[0];
            dgvAdditionalDivision.DataSource = ds.Tables[0];
            dgvAdditionalDivision.DataBind();
        }
        else
        {
            ViewState["AdditionalDivision"] = null;
            dgvAdditionalDivision.DataSource = null;
            dgvAdditionalDivision.DataBind();
        }

        //txtUserName.Text = grd.Cells[27].Text.Replace("&nbsp;", "").Trim();
        divCreateNew.Focus();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        reset();
        divCreateNew.Visible = true;
    }

    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDesignation.SelectedValue == "0")
        {
            ddlReportingManager.Items.Clear();
        }
        else
        {
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
            get_Employee_Reporting_Manager(Convert.ToInt32(ddlDesignation.SelectedValue), Zone_Id, Circle_Id, Division_Id);
        }
    }

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlZone.SelectedValue == "0")
        {
            ddlCircle.Items.Clear();
        }
        else
        {
            get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue), ddlCircle);
        }
    }

    private void get_tbl_Circle(int Zone_Id, DropDownList ddlCircle)
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

    protected void ddlCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCircle.SelectedValue == "0")
        {
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue), ddlDivision);
        }
    }

    private void get_tbl_Division(int Circle_Id, DropDownList ddlDivision)
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

    protected void ddlZoneS_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlZoneS.SelectedValue == "0")
        {
            ddlCircleS.Items.Clear();
            MessageBox.Show("Please Select Zone");
            return;
        }
        else
        {
            get_tbl_Circle(Convert.ToInt32(ddlZoneS.SelectedValue), ddlCircleS);
            //int Zone_Id = 0;
            //int Circle_Id = 0;
            //int Division_Id = 0;
            //try
            //{
            //    Zone_Id = Convert.ToInt32(ddlZoneS.SelectedValue);
            //}
            //catch
            //{
            //    Zone_Id = 0;
            //}
            //try
            //{
            //    Circle_Id = Convert.ToInt32(ddlCircleS.SelectedValue);
            //}
            //catch
            //{
            //    Circle_Id = 0;
            //}
            //try
            //{
            //    Division_Id = Convert.ToInt32(ddlDivisionS.SelectedValue);
            //}
            //catch
            //{
            //    Division_Id = 0;
            //}
            //get_Employee(Zone_Id, Circle_Id, Division_Id);
        }
    }

    protected void ddlCircleS_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCircleS.SelectedValue == "0")
        {
            ddlDivisionS.Items.Clear();
            MessageBox.Show("Please Select "+ Session["Default_Circle"].ToString() + "");
            return;
        }
        else
        {
            get_tbl_Division(Convert.ToInt32(ddlCircleS.SelectedValue), ddlDivisionS);
            //int Zone_Id = 0;
            //int Circle_Id = 0;
            //int Division_Id = 0;
            //try
            //{
            //    Zone_Id = Convert.ToInt32(ddlZoneS.SelectedValue);
            //}
            //catch
            //{
            //    Zone_Id = 0;
            //}
            //try
            //{
            //    Circle_Id = Convert.ToInt32(ddlCircleS.SelectedValue);
            //}
            //catch
            //{
            //    Circle_Id = 0;
            //}
            //try
            //{
            //    Division_Id = Convert.ToInt32(ddlDivisionS.SelectedValue);
            //}
            //catch
            //{
            //    Division_Id = 0;
            //}
            //get_Employee(Zone_Id, Circle_Id, Division_Id);
        }
    }

    protected void ddlDivisionS_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        try
        {
            Zone_Id = Convert.ToInt32(ddlZoneS.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircleS.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivisionS.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        get_Employee(Zone_Id, Circle_Id, Division_Id);
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[27].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[28].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[29].Text = Session["Default_Division"].ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnDelete = e.Row.FindControl("btnDelete") as ImageButton;
            //if (Session["UserType"].ToString() == "1")
            //{
            //    btnDelete.Visible = true;
            //}
            //else
            //{
            //    btnDelete.Visible = false;
            //}
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //if (ddlSearchScheme.SelectedValue == "0")
        //{
        //    MessageBox.Show("Please Select A Scheme");
        //    return;
        //}
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        try
        {
            Zone_Id = Convert.ToInt32(ddlZoneS.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircleS.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivisionS.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        get_Employee(Zone_Id, Circle_Id, Division_Id);
    }
    private void get_tbl_Designation_Additional()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Designation();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlAdditionDesignation, "Designation_DesignationName", "Designation_Id");
            if (ddlDesignation.Items.Count == 2)
            {
                ddlDesignation.SelectedIndex = 1;
            }
        }
        else
        {
            ddlAdditionDesignation.Items.Clear();
        }
    }
    private void get_tbl_Zone_Addditional()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlAdditionZone, "Zone_Name", "Zone_Id");
        }
        else
        {
            ddlAdditionZone.Items.Clear();
        }
    }
    protected void ddlAdditionZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdditionZone.SelectedValue == "0")
        {
            ddlAdditionalCircle.Items.Clear();
            ddlAdditionalDivision.Items.Clear();
        }
        else
        {
            get_tbl_Circle_Additional(Convert.ToInt32(ddlAdditionZone.SelectedValue));
        }
    }

    protected void ddlAdditionalCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdditionalCircle.SelectedValue == "0")
        {
            ddlAdditionalDivision.Items.Clear();
        }
        else
        {
            get_tbl_Division_Additional(Convert.ToInt32(ddlAdditionalCircle.SelectedValue));
        }
    }
    private void get_tbl_Circle_Additional(int Zone_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Circle(Zone_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlAdditionalCircle, "Circle_Name", "Circle_Id");
        }
        else
        {
            ddlAdditionalCircle.Items.Clear();
        }
    }
    private void get_tbl_Division_Additional(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division(Circle_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlAdditionalDivision, "Division_Name", "Division_Id");
        }
        else
        {
            ddlAdditionalDivision.Items.Clear();
        }
    }

    protected void btnAddAdditionalDivision_Click(object sender, EventArgs e)
    {
        if (ddlAdditionDesignation.SelectedValue == "0" || ddlAdditionDesignation.SelectedValue == "")
        {
            MessageBox.Show("Please Select Designation");
            ddlAdditionalDivision.Focus();
            return;
        }
        if (ddlAdditionZone.SelectedValue == "0" || ddlAdditionZone.SelectedValue == "")
        {
            MessageBox.Show("Please Select Zone");
            ddlAdditionZone.Focus();
            return;
        }
        //if (ddlAdditionalCircle.SelectedValue == "0" || ddlAdditionalCircle.SelectedValue == "")
        //{
        //    MessageBox.Show("Please Select Circle");
        //    ddlAdditionalCircle.Focus();
        //    return;
        //}
        //if (ddlAdditionalDivision.SelectedValue == "0" || ddlAdditionalDivision.SelectedValue == "")
        //{
        //    MessageBox.Show("Please Select Divison");
        //    ddlAdditionalDivision.Focus();
        //    return;
        //}

        if (ViewState["AdditionalDivision"] != null)
        {
            DataTable dt = (DataTable)ViewState["AdditionalDivision"];

            DataRow dr = dt.NewRow();

            dr["PersonAdditionalArea_Designation_Id"] = ddlAdditionDesignation.SelectedValue;
            try
            {
                dr["PersonAdditionalArea_ZoneId"] = ddlAdditionZone.SelectedValue;
            }
            catch
            {
                dr["PersonAdditionalArea_ZoneId"] = "0";
            }
            try
            {
                dr["PersonAdditionalArea_CircleId"] = ddlAdditionalCircle.SelectedValue;
            }
            catch
            {
                dr["PersonAdditionalArea_CircleId"] = "0";
            }
            try
            {
                dr["PersonAdditionalArea_DivisionId"] = ddlAdditionalDivision.SelectedValue;
            }
            catch
            {
                dr["PersonAdditionalArea_DivisionId"] = "0";
            }
            dr["Designation_Name"] = ddlAdditionDesignation.SelectedItem.Text.Trim();
            try
            {
                dr["Zone_Name"] = ddlAdditionZone.SelectedItem.Text.Trim();
            }
            catch
            {
                dr["Zone_Name"] = "";
            }
            try
            {
                dr["Circle_Name"] = ddlAdditionalCircle.SelectedItem.Text.Trim();
            }
            catch
            {
                dr["Circle_Name"] = "";
            }
            try
            {
                dr["Division_Name"] = ddlAdditionalDivision.SelectedItem.Text.Trim();
            }
            catch
            {
                dr["Division_Name"] = "";
            }
            dt.Rows.Add(dr);

            ViewState["AdditionalDivision"] = dt;

            dgvAdditionalDivision.DataSource = dt;
            dgvAdditionalDivision.DataBind();
            ddlAdditionDesignation.SelectedValue = "0";
            ddlAdditionZone.SelectedValue = "0";
            ddlAdditionalCircle.Items.Clear();
            ddlAdditionalDivision.Items.Clear();
        }
        else
        {
            DataSet ds = (new DataLayer()).get_PersonAdditionalArea_Edit(0);
            try
            {
                ViewState["AdditionalDivision"] = ds.Tables[0];
            }
            catch
            {
                ViewState["AdditionalDivision"] = null;
            }
        }
    }

    protected void lnkDeleteAdditionalDivision_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["AdditionalDivision"] != null)
        {
            if (index >= 0)
            {
                DataTable dt = (DataTable)ViewState["AdditionalDivision"];
                dt.Rows.RemoveAt(index);
                dgvAdditionalDivision.DataSource = dt;
                dgvAdditionalDivision.DataBind();
                ViewState["AdditionalDivision"] = dt;
            }

        }
    }

    protected void dgvAdditionalDivision_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[6].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[7].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[8].Text = Session["Default_Division"].ToString();
        }
    }
}