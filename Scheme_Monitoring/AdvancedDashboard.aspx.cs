using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

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
            //LoadDashboardData();
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

        LoadDashboardData();
    }
    private void LoadDashboardData()
    {
        int schemeId = 0;
        int fyId = 0;

        // Safely parse scheme ID
        if (!int.TryParse(ddlScheme.SelectedValue, out schemeId))
        {
            lblMessage.Text = "Please select a valid Scheme.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }

        // Safely parse financial year ID
        if (!int.TryParse(ddlFY.SelectedValue, out fyId))
        {
            lblMessage.Text = "Please select a valid Financial Year.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }

        // Safely parse division ID
        int divisionId = 0;
        if (!string.IsNullOrEmpty(ddlDivision.SelectedValue) && ddlDivision.SelectedValue != "0")
        {
            if (!int.TryParse(ddlDivision.SelectedValue, out divisionId))
            {
                lblMessage.Text = "Invalid Division selected.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        // Safely parse circle ID
        int circleId = 0;
        if (!string.IsNullOrEmpty(ddlCircle.SelectedValue) && ddlCircle.SelectedValue != "0")
        {
            if (!int.TryParse(ddlCircle.SelectedValue, out circleId))
            {
                lblMessage.Text = "Invalid District selected.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        // Safely parse zone ID
        int zoneId = 0;
        if (!string.IsNullOrEmpty(ddlZone.SelectedValue) && ddlZone.SelectedValue != "0")
        {
            if (!int.TryParse(ddlZone.SelectedValue, out zoneId))
            {
                lblMessage.Text = "Invalid State selected.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        string implAgency = ddlImplAgency.SelectedValue;

        // Load tile data
        DataSet dsTiles = (new DataLayer()).GetDashboardTileData(schemeId, fyId, zoneId, circleId, divisionId, implAgency);
        if (dsTiles != null && dsTiles.Tables.Count > 0 && dsTiles.Tables[0].Rows.Count > 0)
        {
            DataTable dtTiles = dsTiles.Tables[0];
            divDivisionCount.InnerText = dtTiles.Rows[0]["DivisionCount"].ToString();
            divDistrictCount.InnerText = dtTiles.Rows[0]["DistrictCount"].ToString();
            divULBCount.InnerText = dtTiles.Rows[0]["ULBCount"].ToString();
            divProjectCount.InnerText = dtTiles.Rows[0]["ProjectCount"].ToString();
            divSanctionedCost.InnerText = "₹" + Convert.ToDecimal(dtTiles.Rows[0]["SanctionedCost"]).ToString("N2");
            divReleaseAmount.InnerText = "₹" + Convert.ToDecimal(dtTiles.Rows[0]["ReleaseAmount"]).ToString("N2");
        }

        // Load chart data
        DataSet dsCharts = (new DataLayer()).GetDashboardChartData(schemeId, fyId, zoneId, circleId, divisionId, implAgency);
        if (dsCharts != null && dsCharts.Tables.Count > 0)
        {
            // Prepare chart data object
            var chartData = new
            {
                DivisionProjects = GetChartData(dsCharts.Tables[0], "DivisionName", "ProjectCount"),
                DivisionCost = GetChartData(dsCharts.Tables[0], "DivisionName", "ProjectCost"),
                DivisionULBs = GetChartData(dsCharts.Tables[1], "DivisionName", "ULBCount"),
                ULBType = GetChartData(dsCharts.Tables[2], "ULBType", "ULBCount"),
                ULBTypeCost = GetChartData(dsCharts.Tables[2], "ULBType", "ProjectCost"),
                ULBTypeCount = GetChartData(dsCharts.Tables[2], "ULBType", "ProjectCount"),
                ImplAgencyProjects = new
                {
                    labels = GetColumnValues(dsCharts.Tables[3], "ImplAgency"),
                    data = GetColumnValues(dsCharts.Tables[3], "ProjectCount", true)
                },
                ImplAgencyCost = new
                {
                    labels = GetColumnValues(dsCharts.Tables[3], "ImplAgency"),
                    data = GetColumnValues(dsCharts.Tables[3], "ProjectCost", true)
                },

                DistrictProjectCost = new
                {
                    labels = GetColumnValues(dsCharts.Tables[4], "DistrictName"),
                    data = GetColumnValues(dsCharts.Tables[4], "ProjectCost", true)
                },

                DistrictProject= new
                {
                    labels = GetColumnValues(dsCharts.Tables[4], "DistrictName"),
                    data = GetColumnValues(dsCharts.Tables[4], "ProjectCount", true)
                },

                DistrictSummary = new
                {
                    labels = GetColumnValues(dsCharts.Tables[4], "DistrictName"),
                    datasets = new[] {
                        new {
                            label = "Number of Projects",
                            data = GetColumnValues(dsCharts.Tables[4], "ProjectCount", true),
                            backgroundColor = "rgba(54, 162, 235, 0.7)"
                        },
                        new {
                            label = "Project Cost",
                            data = GetColumnValues(dsCharts.Tables[4], "ProjectCost", true),
                            backgroundColor = "rgba(255, 99, 132, 0.7)"
                        }
                    }
                },

                TenderStatusProjects = new
                {
                    labels = GetColumnValues(dsCharts.Tables[5], "TenderStatus"),
                    data = GetColumnValues(dsCharts.Tables[5], "ProjectCount", true),
                    centerText = dsCharts.Tables[5].AsEnumerable().Sum(row => row.Field<int>("ProjectCount")) + " Projects"
                },
                WorkOrderStatusProjects = new
                {
                    labels = GetColumnValues(dsCharts.Tables[6], "WorkOrderStatus"),
                    data = GetColumnValues(dsCharts.Tables[6], "ProjectCount", true),
                    centerText = dsCharts.Tables[6].AsEnumerable().Sum(row => row.Field<int>("ProjectCount")) + " Projects"
                }
            };

            // Register startup script to initialize charts
            string script = "initializeCharts(" + JsonConvert.SerializeObject(chartData, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }) + ");";
            ScriptManager.RegisterStartupScript(this, GetType(), "InitializeCharts", script, true);
        }
    }

    private void LoadDashboardDataOLD()
    {
        int schemeId = Convert.ToInt32(ddlScheme.SelectedValue);
        int fyId = Convert.ToInt32(ddlFY.SelectedValue);
        //int divisionId = ddlDivision.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlDivision.SelectedValue);
        int divisionId = 0;

        if (!string.IsNullOrWhiteSpace(ddlDivision.SelectedValue) && ddlDivision.SelectedValue != "0")
        {
            int.TryParse(ddlDivision.SelectedValue, out divisionId);
        }
        int circleId = ddlCircle.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlCircle.SelectedValue);
        int zoneId = ddlZone.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlZone.SelectedValue);
        string implAgency = ddlImplAgency.SelectedValue;

        // Load tile data
        DataSet dsTiles = (new DataLayer()).GetDashboardTileData(schemeId, fyId, zoneId, circleId, divisionId, implAgency);
        if (dsTiles != null && dsTiles.Tables.Count > 0)
        {
            DataTable dtTiles = dsTiles.Tables[0];
            if (dtTiles.Rows.Count > 0)
            {
                divDivisionCount.InnerText = dtTiles.Rows[0]["DivisionCount"].ToString();
                divDistrictCount.InnerText = dtTiles.Rows[0]["DistrictCount"].ToString();
                divULBCount.InnerText = dtTiles.Rows[0]["ULBCount"].ToString();
                divProjectCount.InnerText = dtTiles.Rows[0]["ProjectCount"].ToString();
                divSanctionedCost.InnerText = "₹" + Convert.ToDecimal(dtTiles.Rows[0]["SanctionedCost"]).ToString("N2");
                divReleaseAmount.InnerText = "₹" + Convert.ToDecimal(dtTiles.Rows[0]["ReleaseAmount"]).ToString("N2");
            }
        }

        // Load chart data
        DataSet dsCharts = (new DataLayer()).GetDashboardChartData(schemeId, fyId, zoneId, circleId, divisionId, implAgency);
        if (dsCharts != null && dsCharts.Tables.Count > 0)
        {
            // Prepare chart data object
            var chartData = new
            {
                DivisionProjects = GetChartData(dsCharts.Tables[0], "DivisionName", "ProjectCount"),
                DivisionCost = GetChartData(dsCharts.Tables[0], "DivisionName", "ProjectCost"),
                DivisionULBs = GetChartData(dsCharts.Tables[1], "DivisionName", "ULBCount"),
                ULBType = GetChartData(dsCharts.Tables[2], "ULBType", "ULBCount"),
                ULBTypeCost = GetChartData(dsCharts.Tables[2], "ULBType", "ProjectCost"),
                ImplAgencyProjects = GetChartData(dsCharts.Tables[3], "ImplAgency", "ProjectCount"),
                ImplAgencyCost = GetChartData(dsCharts.Tables[3], "ImplAgency", "ProjectCost"),
                DistrictSummary = new
                {
                    labels = GetColumnValues(dsCharts.Tables[4], "DistrictName"),
                    projectCounts = GetColumnValues(dsCharts.Tables[4], "ProjectCount", true),
                    projectCosts = GetColumnValues(dsCharts.Tables[4], "ProjectCost", true)
                }
            };

            // Register startup script to initialize charts
           
            string script = "initializeCharts(" + JsonConvert.SerializeObject(chartData) + ");";
            ScriptManager.RegisterStartupScript(this, GetType(), "InitializeCharts", script, true);
        }
    }

    private object GetChartData(DataTable dt, string labelColumn, string dataColumn)
    {
        return new
        {
            labels = GetColumnValues(dt, labelColumn),
            data = GetColumnValues(dt, dataColumn, true)
        };
    }

    private string[] GetColumnValues(DataTable dt, string columnName, bool isNumeric = false)
    {
        List<string> values = new List<string>();
        foreach (DataRow row in dt.Rows)
        {
            if (isNumeric)
            {
                values.Add(row[columnName].ToString());
            }
            else
            {
                values.Add(row[columnName].ToString());
            }
        }
        return values.ToArray();
    }


    protected void btnKnowMoreDivisions_Click(object sender, EventArgs e)
    {
        int schemeId = Convert.ToInt32(ddlScheme.SelectedValue);
        int fyId = Convert.ToInt32(ddlFY.SelectedValue);

        DataTable dt = (new DataLayer()).GetDivisionDrillDownData(schemeId, fyId);
        gvDrillDown.DataSource = dt;
        gvDrillDown.DataBind();

        knowMoreModalLabel.InnerText = "Division-wise Project Details";
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", "showModal();", true);
    }

    protected void btnKnowMoreDistricts_Click(object sender, EventArgs e)
    {
        int schemeId = Convert.ToInt32(ddlScheme.SelectedValue);
        int fyId = Convert.ToInt32(ddlFY.SelectedValue);

        DataTable dt = (new DataLayer()).GetDistrictDrillDownData(schemeId, fyId);
        gvDrillDown.DataSource = dt;
        gvDrillDown.DataBind();

        knowMoreModalLabel.InnerText = "District-wise Project Details";
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", "showModal();", true);
    }

    protected void btnKnowMoreULBs_Click(object sender, EventArgs e)
    {
        int schemeId = Convert.ToInt32(ddlScheme.SelectedValue);
        int fyId = Convert.ToInt32(ddlFY.SelectedValue);

        DataTable dt = (new DataLayer()).GetULBDrillDownData(schemeId, fyId);
        gvDrillDown.DataSource = dt;
        gvDrillDown.DataBind();

        knowMoreModalLabel.InnerText = "ULB-wise Project Details";
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", "showModal();", true);
    }

    protected void btnKnowMoreProjects_Click(object sender, EventArgs e)
    {
        int schemeId = Convert.ToInt32(ddlScheme.SelectedValue);
        int fyId = Convert.ToInt32(ddlFY.SelectedValue);

        DataTable dt = (new DataLayer()).GetProjectDrillDownData(schemeId, fyId);
        gvDrillDown.DataSource = dt;
        gvDrillDown.DataBind();

        knowMoreModalLabel.InnerText = "Project Details";
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", "showModal();", true);
    }

    protected void btnKnowMoreSanctionedCost_Click(object sender, EventArgs e)
    {
        int schemeId = Convert.ToInt32(ddlScheme.SelectedValue);
        int fyId = Convert.ToInt32(ddlFY.SelectedValue);

        DataTable dt = (new DataLayer()).GetSanctionedCostDrillDownData(schemeId, fyId);
        gvDrillDown.DataSource = dt;
        gvDrillDown.DataBind();

        knowMoreModalLabel.InnerText = "Sanctioned Cost Details";
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", "showModal();", true);
    }

    protected void btnKnowMoreRelease_Click(object sender, EventArgs e)
    {
        int schemeId = Convert.ToInt32(ddlScheme.SelectedValue);
        int fyId = Convert.ToInt32(ddlFY.SelectedValue);

        DataTable dt = (new DataLayer()).GetReleaseAmountDrillDownData(schemeId, fyId);
        gvDrillDown.DataSource = dt;
        gvDrillDown.DataBind();

        knowMoreModalLabel.InnerText = "Release Amount Details";
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", "showModal();", true);
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