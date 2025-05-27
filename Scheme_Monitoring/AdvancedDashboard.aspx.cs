using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.UI.HtmlControls;

public partial class AdvancedDashboard : System.Web.UI.Page
{
    ULBFund objLoan = new ULBFund();
    string ConStr = ConfigurationManager.AppSettings.Get("conn");
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
            if (Request.QueryString.Count > 0)
            {
                ULBID.Value = Request.QueryString["ULBID"].ToString();
                FYID.Value = Request.QueryString["FYID"].ToString();
            }
            get_tbl_Project();
            get_tbl_Zone();
            get_tbl_FinancialYear();
            SetDropdownsBasedOnUserType();
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }
    public bool ValidateFields()
    {
        if (ddlScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Scheme. ");
            ddlScheme.Focus();
            return false;
        }
        if (ddlFY.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Financial Year. ");
            ddlFY.Focus();
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        if (!ValidateFields())
        {
            return;
        }
    }

    #region Bind Drop Down List
    private void get_tbl_Project()
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
        FillDropDown(ds, ddlScheme, "Project_Name", "Project_Id");
    }
    private void get_tbl_FinancialYear()
    {
        DataSet ds = (new ULBFund()).getFYDetail();
        FillDropDown(ds, ddlFY, "FinancialYear_Comments", "FinancialYear_Id");
    }
    private void get_tbl_Zone()
    {
        DataSet ds = (new DataLayer()).get_tbl_Zone();
        FillDropDown(ds, ddlZone, "Zone_Name", "Zone_Id");
        if (ddlZone.SelectedItem.Value != "0")
        {
            get_tbl_Mandal();
        }
    }
    private void get_tbl_Mandal()
    {
        DataSet ds = (new DataLayer()).get_tbl_Mandal();
        FillDropDown(ds, ddlMandal, "DivName", "DivisionID");
        if (ddlMandal.SelectedItem.Value != "0")
        {
            get_tbl_Circle(Convert.ToInt32(ddlMandal.SelectedValue));
        }
    }
    private void get_tbl_Circle(int MandalId)
    {
        DataSet ds = (new DataLayer()).get_tbl_CircleByDivisionId(MandalId);
        FillDropDown(ds, ddlCircle, "Circle_Name", "Circle_Id");
    }
    private void get_tbl_Division(int circleId)
    {
        DataSet ds = (new DataLayer()).get_tbl_Division(circleId);
        FillDropDown(ds, ddlDivision, "Division_Name", "Division_Id");
    }
    private void get_tbl_ProjectType(int SchemeId)
    {
        DataSet ds = (new DataLayer()).get_tbl_ProjectType(SchemeId, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlProjectType.DataTextField = "ProjectType_Name";
            ddlProjectType.DataValueField = "ProjectType_Id";
            ddlProjectType.DataSource = ds.Tables[0];
            ddlProjectType.DataBind();

            // Add default item
            ddlProjectType.Items.Insert(0, new ListItem("-Select Project Type-", "0"));
        }
        else
        {
            ddlProjectType.Items.Clear();
            ddlProjectType.Items.Insert(0, new ListItem("-Select Project Type-", "0"));
        }
    }

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlZone.SelectedValue == "0")
        {
            ddlMandal.Items.Clear();
            ddlCircle.Items.Clear();
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Mandal();
        }
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue == "0")
        {
            ddlProjectType.Items.Clear();
        }
        else
        {
            int SchemeId = 0;
            try
            {
                SchemeId = Convert.ToInt32(ddlScheme.SelectedValue);
            }
            catch
            {
                SchemeId = 0;
            }

            get_tbl_ProjectType(SchemeId);
        }
    }
    protected void ddlMandal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMandal.SelectedValue == "0")
        {
            ddlCircle.Items.Clear();
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Circle(Convert.ToInt32(ddlMandal.SelectedValue));
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

    private void SetDropdownsBasedOnUserType()
    {
        int userType = Convert.ToInt32(Session["UserType"]);
        int zoneId = Convert.ToInt32(Session["PersonJuridiction_ZoneId"]);
        //int MandalId = Convert.ToInt32(Session["MandalId"]);
        //int MandalId = Session["MandalId"] is int mandalId ? mandalId : Convert.ToInt32(Session["MandalId"]);
        int MandalId = Session["MandalId"] != null && !string.IsNullOrEmpty(Session["MandalId"].ToString())
               ? Convert.ToInt32(Session["MandalId"])
               : 0;
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
            if (MandalId > 0)
            {
                SetDropdownValueAndDisable(ddlMandal, MandalId);
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

            else if (ddl.ID.ToString() == "ddlMandal")
            {
                ddlMandal_SelectedIndexChanged(ddl, EventArgs.Empty);
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
    private void FillDropDown(DataSet ds, DropDownList ddl, string textField, string valueField)
    {
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown2(ds.Tables[0], ddl, textField, valueField);
        }
        else
        {
            ddl.Items.Clear();
        }
    }
    #endregion
}