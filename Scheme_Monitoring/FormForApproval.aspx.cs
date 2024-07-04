using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

public partial class FormForApproval : System.Web.UI.Page
{
    Loan objLoan = new Loan();

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
            lblZoneH.Text = Session["Default_Zone"].ToString() + "*";
            lblCircleH.Text = Session["Default_Circle"].ToString() + "*";
            lblDivisionH.Text = Session["Default_Division"].ToString() + "*";

            get_tbl_Zone();
            get_tbl_Project();
            //get_tbl_WorkType();
            get_tbl_FinancialYear();

            SetDropdownsBasedOnUserType();
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }
    private void SetDropdownsBasedOnUserType()
    {
        int userType = Convert.ToInt32(Session["UserType"]);
        int zoneId = Convert.ToInt32(Session["PersonJuridiction_ZoneId"]);
        int circleId = Convert.ToInt32(Session["PersonJuridiction_CircleId"]);
        int divisionId = Convert.ToInt32(Session["PersonJuridiction_DivisionId"]);

        if (userType == 4 && zoneId > 0)
        {
            SetDropdownValueAndDisable(ddlZone, zoneId);
        }
        else if (userType == 6 && zoneId > 0)
        {
            SetDropdownValueAndDisable(ddlZone, zoneId);
            if (circleId > 0)
            {
                SetDropdownValueAndDisable(ddlCircle, circleId);
            }
        }
        else if (userType == 7 && zoneId > 0)
        {
            SetDropdownValueAndDisable(ddlZone, zoneId);
            if (circleId > 0)
            {
                SetDropdownValueAndDisable(ddlCircle, circleId);
                if (divisionId > 0)
                {
                    SetDropdownValueAndDisable(ddlDivision, divisionId);
                }
            }
        }
    }
    private void SetDropdownValueAndDisable(DropDownList ddl, int value)
    {
        try
        {
            ddl.SelectedValue = value.ToString();
            ddl.Enabled = false;
            if (ddl.ID.ToString() == "ddlZone")
            {
                ddlZone_SelectedIndexChanged(ddl, EventArgs.Empty);
            }
            else if (ddl.ID.ToString() == "ddlCircle")
            {
                ddlCircle_SelectedIndexChanged(ddl, EventArgs.Empty);
            }
        }
        catch
        {
            // Handle exception if needed
        }
    }
    

    private void get_tbl_Zone()
    {
        DataSet ds = (new DataLayer()).get_tbl_Zone();
        FillDropDown(ds, ddlZone, "Zone_Name", "Zone_Id");
        if (ddlZone.SelectedItem.Value != "0")
        {
            get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
        }
    }

    private void get_tbl_Project()
    {
        DataSet ds = (new DataLayer()).get_tbl_Project(0);
        FillDropDown(ds, ddlProjectMaster, "Project_Name", "Project_Id");
    }

    private void get_tbl_WorkType(int ProjectId)
    {
        DataSet ds = (new DataLayer()).get_tbl_ProjectType(ProjectId, 0);
        FillDropDown(ds, ddlWorkType, "ProjectType_Name", "ProjectType_Id");
    }

    private void get_tbl_FinancialYear()
    {
        DataSet ds = (new DataLayer()).get_tbl_FinancialYear();
        FillDropDown(ds, ddlFY, "FinancialYear_Comments", "FinancialYear_Id");
    }

    private void get_tbl_Circle(int zoneId)
    {
        DataSet ds = (new DataLayer()).get_tbl_Circle(zoneId);
        FillDropDown(ds, ddlCircle, "Circle_Name", "Circle_Id");
    }

    private void get_tbl_Division(int circleId)
    {
        DataSet ds = (new DataLayer()).get_tbl_Division(circleId);
        FillDropDown(ds, ddlDivision, "Division_Name", "Division_Id");
    }

    private void FillDropDown(DataSet ds, DropDownList ddl, string textField, string valueField)
    {
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddl, textField, valueField);
        }
        else
        {
            ddl.Items.Clear();
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

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue == "0")
        {
            lblMessage.Text = "Please Select a ULB.";
            ddlDivision.Focus();
        }
        else
        {
            //BindLoanReleaseGridByULB();
        }
    }

    protected void ddlProjectMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProjectMaster.SelectedValue == "0")
        {
            ddlWorkType.Items.Clear();
        }
        else
        {
            int ProjectId = 0;
            try
            {
                ProjectId = Convert.ToInt32(ddlProjectMaster.SelectedValue);
            }
            catch
            {
                ProjectId = 0;
            }
            get_tbl_WorkType(ProjectId);
        }
    }



    protected void BindLoanReleaseGridByULB()
    {
        int Zone_Id = 0, Circle_Id = 0, Division_Id = 0;

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

        tbl_LoanRelease obj_Loan = new tbl_LoanRelease();
        obj_Loan.Zone = Zone_Id;
        obj_Loan.Circle = Circle_Id;
        obj_Loan.Division = Division_Id;

        LoadLoanReleaseGrid(obj_Loan);
    }

    private void LoadLoanReleaseGrid(tbl_LoanRelease obj_Loan)
    {
        DataTable dt = new DataTable();
        dt = objLoan.getLoanReleaseBySearch(obj_Loan);

        if (dt != null && dt.Rows.Count > 0)
        {
            //Session["GridViewData"] = dt;
            gvRecords.DataSource = dt;
            gvRecords.DataBind();
            divData.Visible = true;

        }
        else
        {
            divData.Visible = true;
            gvRecords.DataSource = null;
            gvRecords.DataBind();
            MessageBox.Show("No Records Found");
        }
    }




    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!ValidateFields())
        {
            return;
        }

        tbl_LoanRelease LoanRelease = CreateLoanReleaseObject();
        int result = objLoan.InsertLoanRelease(LoanRelease);

        if (result > 0)
        {
            MessageBox.Show("Record saved successfully.");
            reset();
        }
        else
        {
            MessageBox.Show("Something went wrong please try again or contact administrator!");
            return;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!ValidateFields())
        {
            return;
        }
        int FormApproval_Id = Convert.ToInt32(hfFormApproval_Id.Value.ToString());
        tbl_LoanRelease obj_LoanRelease = CreateLoanReleaseObject();
        int result = objLoan.UpdateLoanRelease(obj_LoanRelease, FormApproval_Id);
        if (result > 0)
        {
            MessageBox.Show("Record updated successfully!");
            reset();
            divData.Visible = false;
        }
        else
        {
            MessageBox.Show("Something went wrong please try again or contact administrator!");
            return;
        }
    }

    private tbl_LoanRelease CreateLoanReleaseObject()
    {
        int personId;
        int zone;
        int circle;
        int division;
        double releaseAmount;
        int instNo;


        if (!int.TryParse(Session["Person_Id"].ToString(), out personId))
        {
            throw new FormatException("Invalid Person_Id in session.");
        }

        if (!int.TryParse(ddlZone.SelectedValue, out zone))
        {
            throw new FormatException("Invalid Zone selected value.");
        }

        if (!int.TryParse(ddlCircle.SelectedValue, out circle))
        {
            throw new FormatException("Invalid Circle selected value.");
        }

        if (!int.TryParse(ddlDivision.SelectedValue, out division))
        {
            throw new FormatException("Invalid Division selected value.");
        }





        return new tbl_LoanRelease
        {
            AddedBy = personId,
            Zone = zone,
            Circle = circle,
            Division = division
        };
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        reset();
    }

    public bool ValidateFields()
    {
        
        if (ddlZone.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a State. ");
            ddlZone.Focus();
            return false;
        }
        if (ddlCircle.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a District. ");
            ddlCircle.Focus();
            return false;
        }
        if (ddlDivision.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a ULB. ");
            ddlDivision.Focus();
            return false;
        }

        else
        {
            return true;
        }
    }

    private void reset()
    {
        
        ddlZone.SelectedValue = "0";
        ddlCircle.SelectedValue = "0";
        ddlDivision.SelectedValue = "0";
       


       

        btnSave.Visible = true;
        btnUpdate.Visible = false;
    }
    

    protected void Load_LoanRelease(int LoanRelease_Id)
    {
        DataTable dt = objLoan.getLoanReleaseById(LoanRelease_Id);
        if (dt != null && dt.Rows.Count > 0)
        {
            hfFormApproval_Id.Value = LoanRelease_Id.ToString();
            
            try
            {
                ddlZone.SelectedValue = dt.Rows[0]["Zone"].ToString();
            }
            catch
            {
                ddlZone.SelectedValue = "0";
            }
            //ddlZone.Enabled = false;

            ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());
            try
            {
                ddlCircle.SelectedValue = dt.Rows[0]["Circle"].ToString();
            }
            catch
            {
                ddlCircle.SelectedValue = "0";
            }
            //ddlCircle.Enabled = false;

            ddlCircle_SelectedIndexChanged(ddlCircle, new EventArgs());
            try
            {
                ddlDivision.SelectedValue = dt.Rows[0]["Division"].ToString();
            }
            catch
            {
                ddlDivision.SelectedValue = "0";
            }
            //ddlDivision.Enabled = false;



            btnSave.Visible = false;
            btnUpdate.Visible = true;
        }
        else
        {
            btnSave.Visible = true;
            btnUpdate.Visible = false;
           
            MessageBox.Show("Record with Loan Release id = " + LoanRelease_Id.ToString() + " does not found please contact administrator.");
        }
    }

  

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int LoanRelease_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        if (LoanRelease_Id > 0)
        {
            Load_LoanRelease(LoanRelease_Id);
            ToggleFormMode(true); // switch to update mode
        }
        else
        {
            return;
        }
    }

    private void ToggleFormMode(bool isUpdateMode)
    {
        // Toggle between save and update modes
        btnSave.Visible = !isUpdateMode;
        btnUpdate.Visible = isUpdateMode;
        btnCancel.Visible = isUpdateMode;
    }
}