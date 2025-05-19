using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;

public partial class QualifiedULBManagement : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.MasterPageFile = SetMasterPage.ReturnPage();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string personId = Session["Person_Id"].ToString();
        if (Session["Person_Id"] == null || Session["Login_Id"] == null)
        {
            Response.Redirect("Index.aspx");
        }
        if (personId == "3297" || personId == "2288")
        {
            btnSave.Visible = true;
        }
        else
        {
            btnSave.Visible = false;
        }
        if (!IsPostBack)
        {
            BindFinancialYears();
            BindDistricts();
            BindQualifiedULBs();
        }
    }

    private void BindFinancialYearsOld()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_FinancialYear();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlFinancialYear.DataSource = ds.Tables[0];
            ddlFinancialYear.DataTextField = "FinancialYear_Comments";
            ddlFinancialYear.DataValueField = "FinancialYear_Id";
            ddlFinancialYear.DataBind();
            ddlFinancialYear.Items.Insert(0, new ListItem("-- Select Financial Year --", "0"));

            // Set default selected item to FinancialYear_Id = 21 if it exists
            if (ddlFinancialYear.Items.FindByValue("21") != null)
            {
                ddlFinancialYear.SelectedValue = "21";
            }
        }
    }

    private void BindFinancialYears()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_FinancialYear();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            // Filter only the record with FinancialYear_Id = 21
            DataRow[] rows = ds.Tables[0].Select("FinancialYear_Id = 21");

            if (rows.Length > 0)
            {
                DataTable filteredTable = rows.CopyToDataTable();
                ddlFinancialYear.DataSource = filteredTable;
                ddlFinancialYear.DataTextField = "FinancialYear_Comments";
                ddlFinancialYear.DataValueField = "FinancialYear_Id";
                ddlFinancialYear.DataBind();

                // Optionally add a default selection item at the top
                ddlFinancialYear.Items.Insert(0, new ListItem("-- Select Financial Year --", "0"));

                // Set the default selection to 21
                ddlFinancialYear.SelectedValue = "21";
            }
            else
            {
                // No matching FY found; clear and show only default option
                ddlFinancialYear.Items.Clear();
                ddlFinancialYear.Items.Insert(0, new ListItem("-- Select Financial Year --", "0"));
            }
        }
    }

    private void BindDistricts()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Circle(0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlDistrict.DataSource = ds.Tables[0];
            ddlDistrict.DataTextField = "Circle_Name";
            ddlDistrict.DataValueField = "Circle_Id";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("-- Select District --", "0"));
        }
    }

    private void BindULBsByDistrict(int districtId)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division(districtId);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlULB.DataSource = ds.Tables[0];
            ddlULB.DataTextField = "Division_Name";
            ddlULB.DataValueField = "Division_Id";
            ddlULB.DataBind();
            ddlULB.Items.Insert(0, new ListItem("-- Select ULB --", "0"));
        }
        else
        {
            ddlULB.Items.Clear();
            ddlULB.Items.Insert(0, new ListItem("-- No ULB Found --", "0"));
        }
    }

    private void BindQualifiedULBs()
    {
        int financialYearId = 0, DistrictId=0;
        int.TryParse(ddlFinancialYear.SelectedValue, out financialYearId);
        int.TryParse(ddlDistrict.SelectedValue, out DistrictId);

        DataSet ds = new DataSet();
        ds = (new DataLayerVisionPlan()).get_QualifiedULB(financialYearId, 0, DistrictId);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            rptQualifiedULBs.DataSource = ds.Tables[0];
            rptQualifiedULBs.DataBind();
        }
        else
        {
            rptQualifiedULBs.DataSource = null;
            rptQualifiedULBs.DataBind();
        }
    }

    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindQualifiedULBs();
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        int districtId = 0;
        int.TryParse(ddlDistrict.SelectedValue, out districtId);

        if (districtId > 0)
        {
            BindULBsByDistrict(districtId);
        }
        else
        {
            ddlULB.Items.Clear();
            ddlULB.Items.Insert(0, new ListItem("-- Select ULB --", "0"));
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlFinancialYear.SelectedValue == "0")
        {
            MessageBox.Show("Please select Financial Year");
            return;
        }

        if (ddlDistrict.SelectedValue == "0")
        {
            MessageBox.Show("Please select District");
            return;
        }

        if (ddlULB.SelectedValue == "0")
        {
            MessageBox.Show("Please select ULB");
            return;
        }

        int financialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);
        int ulbId = Convert.ToInt32(ddlULB.SelectedValue);
        int personId = Convert.ToInt32(Session["Person_Id"]);

        // Check if this ULB is already qualified for the selected financial year
        DataSet dsCheck = new DataSet();
        dsCheck = (new DataLayerVisionPlan()).get_QualifiedULB(financialYearId, ulbId, 0);

        if (dsCheck != null && dsCheck.Tables.Count > 0 && dsCheck.Tables[0].Rows.Count > 0)
        {
            MessageBox.Show("This ULB is already qualified for the selected financial year");
            return;
        }

        // Save the record
        if ((new DataLayerVisionPlan()).Insert_QualifiedULB(financialYearId, ulbId, personId))
        {
            MessageBox.Show("ULB qualified successfully!");
            BindQualifiedULBs();
            ResetForm();
        }
        else
        {
            MessageBox.Show("Error in qualifying ULB!");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindQualifiedULBs();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetForm();
    }

    private void ResetForm()
    {
        //ddlFinancialYear.SelectedIndex = 0;
        ddlDistrict.SelectedIndex = 0;
        ddlULB.Items.Clear();
        ddlULB.Items.Insert(0, new ListItem("-- Select ULB --", "0"));
        hfQualifiedULB_Id.Value = "0";
    }

    protected void rptQualifiedULBs_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int qualifiedULBId = Convert.ToInt32(e.CommandArgument);
            int personId = Convert.ToInt32(Session["Person_Id"]);

            if ((new DataLayerVisionPlan()).Delete_QualifiedULB(qualifiedULBId, personId))
            {
                MessageBox.Show("Record deleted successfully!");
                BindQualifiedULBs();
            }
            else
            {
                MessageBox.Show("Error in deleting record!");
            }
        }
    }
}