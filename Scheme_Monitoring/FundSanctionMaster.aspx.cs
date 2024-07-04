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

public partial class FundSanctionMaster : System.Web.UI.Page
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
            BindFinancialYear();
            BindDistrict();
            BindULBType();
            BindScheme();
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    private void BindFinancialYear()
    {
        DataSet ds = (new DataLayer()).get_FinancialYear();
        FillDropDown(ds, ddlFY, "SessionYear", "YearID");
    }
    private void BindDistrict()
    {
        DataSet ds = (new DataLayer()).get_District();
        FillDropDown(ds, ddlDistrict, "DistName", "DistID");
    }
    private void BindULBType()
    {
        DataSet ds = (new DataLayer()).get_ULBType();
        FillDropDown(ds, ddlULBType, "ULBType", "ULBTypeID");
    }
    private void BindULB(int DisID, int ULBType)
    {
        DataSet ds = (new DataLayer()).get_ULB(DisID, ULBType);
        FillDropDown(ds, ddlULB, "ULBName", "ULBID");
    }
    private void BindScheme()
    {
        DataSet ds = (new DataLayer()).get_Scheme(0);
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_Scheme(0);
        }
        else
        {
            ds = (new DataLayer()).get_Scheme(Convert.ToInt32(Session["Person_Id"].ToString()));
        }
        FillDropDown(ds, ddlScheme, "SchemeName", "SchemeID");
    }

    private void SetDropdownValueAndDisable(DropDownList ddl, int value)
    {
        try
        {
            ddl.SelectedValue = value.ToString();
            ddl.Enabled = false;
            if (ddl.ID.ToString() == "ddlDistrict")
            {
                ddlDistrict_SelectedIndexChanged(ddl, EventArgs.Empty);
            }
        }
        catch
        {
            // Handle exception if needed
        }
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

    protected void ddlFY_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFY.SelectedValue == "0")
        {
            
        }
        else
        {
            //BindLoanReleaseGridByULB();
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedValue == "0")
        {
            ddlULB.Items.Clear();
            ddlULBType.SelectedValue = "0";
        }
        else
        {
            BindULB(Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlULBType.SelectedValue));
            BindFundSactionGrid();
        }
    }
    protected void ddlULBType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedValue!="0")
        {
            BindULB(Convert.ToInt32(ddlDistrict.SelectedValue), Convert.ToInt32(ddlULBType.SelectedValue));
            BindFundSactionGrid();
        }
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedValue!="0")
        {
            BindFundSactionGrid();
        }
    }

    protected void BindFundSactionGrid()
    {
        int? YearID = null, DistID = null, ULBID = null, SchemeID= null, PersonId=null, ULBTypeID= null;

        try
        {
            if(Convert.ToInt32(ddlFY.SelectedValue)!=0)
            YearID = Convert.ToInt32(ddlFY.SelectedValue);
        }
        catch
        {
            YearID = null;
        }

        try
        {
            if (Convert.ToInt32(ddlDistrict.SelectedValue) != 0)
                DistID = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            DistID = null;
        }

        try
        {
            if (Convert.ToInt32(ddlULB.SelectedValue) != 0)
                ULBID = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULBID = null;
        }

        try
        {
            if (Convert.ToInt32(ddlScheme.SelectedValue) != 0)
                SchemeID = Convert.ToInt32(ddlScheme.SelectedValue);
        }
        catch
        {
            SchemeID = null;
        }
        try
        {
            if (Convert.ToInt32(ddlULBType.SelectedValue) != 0)
                ULBTypeID = Convert.ToInt32(ddlULBType.SelectedValue);
        }
        catch
        {
            ULBTypeID = null;
        }
        if (Session["UserType"].ToString() == "1")
        {
            PersonId = null;
        }
        else
        {
            PersonId=Convert.ToInt32(Session["Person_Id"].ToString());
        }

        LoadGrid(YearID,  DistID,  ULBID,  SchemeID, PersonId, ULBTypeID);
    }
    private void LoadGrid(int? YearID, int? DistID, int? ULBID, int? SchemeID, int? PersonId, int? ULBTypeID)
    {
        DataTable dt = new DataTable();
        dt = objLoan.getFundSanctionBySearch(YearID, DistID, ULBID, SchemeID, PersonId, ULBTypeID);

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


    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        if (Id > 0)
        {
            Load_FundSanctioned(Id);
            ToggleFormMode(true); // switch to update mode
        }
        else
        {
            return;
        }
    }

    protected void Load_FundSanctioned(int Id)
    {
        DataTable dt = objLoan.getFundSanctionedById(Id);
        if (dt != null && dt.Rows.Count > 0)
        {
            hfId.Value = Id.ToString();

            try
            {
                ddlFY.SelectedValue = dt.Rows[0]["SessionID"].ToString();
            }
            catch
            {
                ddlFY.SelectedValue = "0";
            }

            try
            {
                ddlDistrict.SelectedValue = dt.Rows[0]["DistID"].ToString();
            }
            catch
            {
                ddlDistrict.SelectedValue = "0";
            }

            try
            {
                ddlULBType.SelectedValue = dt.Rows[0]["ULBTypeID"].ToString();
            }
            catch
            {
                ddlULBType.SelectedValue = "0";
            }

            ddlDistrict_SelectedIndexChanged(ddlDistrict, new EventArgs());
            try
            {
                ddlULB.SelectedValue = dt.Rows[0]["ULBID"].ToString();
            }
            catch
            {
                ddlULB.SelectedValue = "0";
            }

            BindScheme();
            try
            {
                ddlScheme.SelectedValue = dt.Rows[0]["SchemeID"].ToString();
            }
            catch
            {
                ddlScheme.SelectedValue = "0";
            }

            txtSactionAmt.Text= dt.Rows[0]["AmtInLac"].ToString();

            ToggleFormMode(true);
            
        }
        else
        {
            btnSave.Visible = true;

            MessageBox.Show("Record with ID = " + Id.ToString() + " does not found please contact administrator.");
        }
    }

    

    

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!ValidateFields())
        {
            return;
        }
        int Id = Convert.ToInt32(hfId.Value.ToString());
        FundSanctionedMaster obj = CreateObject();
        int result = objLoan.UpdateFundSanctioned(obj, Id);
        if (result > 0)
        {
            MessageBox.Show("Record updated successfully!");
            BindFundSactionGrid();
            reset();
            //.Visible = false;
        }
        else
        {
            MessageBox.Show("Something went wrong please try again or contact administrator!");
            return;
        }
    }

    public bool ValidateFields()
    {

        if (ddlDistrict.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a District. ");
            ddlDistrict.Focus();
            return false;
        }
        if (ddlULB.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a ULB. ");
            ddlULB.Focus();
            return false;
        }
        if (ddlFY.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Financial. ");
            ddlFY.Focus();
            return false;
        }

        else
        {
            return true;
        }
    }
    private void reset()
    {
        ddlFY.SelectedValue = "0";
        ddlDistrict.SelectedValue = "0";
        ddlULBType.SelectedValue = "0";
        ddlULB.Items.Clear();
        ddlScheme.SelectedValue = "0";
        txtSactionAmt.Text = "";
        hfId.Value = "";
        ToggleFormMode(false);
    }
    private void ToggleFormMode(bool isUpdateMode)
    {
        // Toggle between save and update modes
        btnSave.Visible = !isUpdateMode;
        btnUpdate.Visible = isUpdateMode;
        ddlFY.Enabled = !isUpdateMode;
        ddlDistrict.Enabled = !isUpdateMode;
        ddlULBType.Enabled = !isUpdateMode;
        ddlULB.Enabled = !isUpdateMode;
        ddlScheme.Enabled = !isUpdateMode;
    }

    private FundSanctionedMaster CreateObject()
    {
        int YearID, DistID, ULBID, SchemeID, PersonId;
        double SanAmount = 0.00;


        if (!int.TryParse(ddlFY.SelectedValue, out YearID))
        {
            throw new FormatException("Invalid Financial Year value.");
        }

        if (!int.TryParse(ddlDistrict.SelectedValue, out DistID))
        {
            throw new FormatException("Invalid District value.");
        }

        if (!int.TryParse(ddlULB.SelectedValue, out ULBID))
        {
            throw new FormatException("Invalid ULB selected value.");
        }

        if (!int.TryParse(ddlScheme.SelectedValue, out SchemeID))
        {
            throw new FormatException("Invalid ULB selected value.");
        }

        if (!int.TryParse(Session["Person_Id"].ToString(), out PersonId))
        {
            throw new FormatException("Invalid Person_Id in session.");
        }

        if (!double.TryParse(txtSactionAmt.Text, out SanAmount))
        {
            throw new FormatException("Invalid Amount!");
        }

        return new FundSanctionedMaster
        {
            AddedBy = PersonId,
            SessionID = YearID,
            DistID = DistID,
            ULBID = ULBID,
            SchemeID = SchemeID,
            AmtInLac = SanAmount
        };
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!ValidateFields())
        {
            return;
        }
        FundSanctionedMaster obj = CreateObject();
        int result = objLoan.InsertFundSanction(obj);
        if (result > 0)
        {
            MessageBox.Show("Record Created successfully!");
            BindFundSactionGrid();
            reset();
            //.Visible = false;
        }
        else
        {
            MessageBox.Show("Something went wrong please try again or contact administrator!");
            return;
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        reset();
    }
}