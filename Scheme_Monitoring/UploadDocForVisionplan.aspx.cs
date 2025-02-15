using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Generic;

public partial class UploadDocForVisionplan : System.Web.UI.Page
{
    ULBFund objLoan = new ULBFund();
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
           
            message.InnerText = "";
            get_tbl_Zone();
            get_tbl_FinancialYear();
            SetDropdownsBasedOnUserType();
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
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
    private void get_tbl_Division(int circleId, string ULBType)
    {
        DataSet ds = (new DataLayer3()).get_tbl_DivisionByULBTypeForVisionPlan(circleId, ULBType);
        FillDropDown(ds, ddlDivision, "Division_Name", "Division_Id");
    }
    private void get_tbl_FinancialYear()
    {
        DataSet ds = (new DataLayer()).get_tbl_FinancialYear();
        FillDropDown(ds, ddlFY, "FinancialYear_Comments", "FinancialYear_Id");
        //FillDropDown(ds, ddlFY1, "FinancialYear_Comments", "FinancialYear_Id");
        //FillDropDown(ds, ddlFY2, "FinancialYear_Comments", "FinancialYear_Id");
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
            get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue), ddlULBType.SelectedValue.ToString());
        }
    }
    protected void ddlULBType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlULBType.SelectedValue == "-1")
        {
            lblMessage.Text = "Please Select a ULB Type.";
            ddlULBType.Focus();
        }
        else if (ddlCircle.SelectedValue == "")
        {
            lblMessage.Text = "Please Select District before ULB Type.";
            ddlULBType.Focus();
        }
        else
        {
            get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue), ddlULBType.SelectedValue.ToString());
        }
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue == "0")
        {
           // lblMessage.Text = "Please Select a ULB.";
            ddlDivision.Focus();
        }
        else
        {
           // GetAllData();
            //BindLoanReleaseGridByULB();
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        List<string> PersonIdNotAllowd = new List<string> { "3291", "3294" };
        string searchString = Session["Person_Id"].ToString();

        if (!PersonIdNotAllowd.Contains(searchString))
        {
            GetAllData();
        }        
    }
    protected void GetAllData()
    {
        var dist = 0;
        var ULB = 0;
        var FY = 0;

        var state = Convert.ToInt32(ddlZone.SelectedValue);
        var mandal = Convert.ToInt32(ddlMandal.SelectedValue);
        if (ddlCircle.SelectedValue == "0" || ddlCircle.SelectedValue == "")
        {
            dist = 0;
        }
        else
        {
            dist = Convert.ToInt32(ddlCircle.SelectedValue);// == "0"
        }

        string UlbType = "-1";
        try
        {
            UlbType = ddlULBType.SelectedValue.ToString();
        }
        catch
        {
            UlbType = "-1";
        }

        if (ddlDivision.SelectedValue == "0" || ddlDivision.SelectedValue == "")
        {
            ULB = 0;
        }
        else
        {
            ULB = Convert.ToInt32(ddlDivision.SelectedValue);// == "0"
        }
        if (ddlFY.SelectedValue == "0" || ddlFY.SelectedValue == "")
        {
            FY = 0;
        }
        else
        {
            FY = Convert.ToInt32(ddlFY.SelectedValue);// == "0"
        }

        string FromDate = "", ToDate = "";

        if (txtFromDate.Text == "")
        {
            FromDate = "1900-01-01";
        }
        else
        {
            FromDate = txtFromDate.Text;
        }

        if (txtToDate.Text == "")
        {
            ToDate = "9999-12-31";
        }
        else
        {
            ToDate = txtToDate.Text;
        }

        int AkanshiULB = chkIsAkanshiULB.Checked ? 1 : -1;

        // ULBID = 0;
        DataTable dt = new DataTable();
        dt = objLoan.GetDocOfAnnualActionPlan("select", ULB, 0, 0, dist, FY, Convert.ToInt32(Session["Person_Id"].ToString()), "", "VisionPlan", UlbType, mandal, FromDate, ToDate, AkanshiULB);
        grdPost.DataSource = dt;
        grdPost.DataBind();

    }
    protected void Edit_Command(object sender, CommandEventArgs e)
    {
        var id = Convert.ToInt32(e.CommandArgument.ToString());
        Response.Redirect("CreateDocForVisionPlan.aspx?id=" + id);
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        var id = Convert.ToInt32(e.CommandArgument.ToString());

        DataTable dt = new DataTable();
        dt = objLoan.GetDocOfAnnualActionPlan("Delete", 0, id, 0, 0, 0, Convert.ToInt32(Session["Person_Id"].ToString()), "", "VisionPlan", "-1", 0, "1900-01-01", "9999-12-31", -1);
        if (dt != null && dt.Rows.Count > 0)
        {
            MessageBox.Show(dt.Rows[0]["Remark"].ToString());
        }

        GetAllData();
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

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string personId = Session["Person_Id"].ToString();
        string Designation = Session["PersonJuridiction_DesignationId"].ToString();

        // Check conditions and hide buttons
        if (Designation == "1056" || personId == "3297" || personId == "2288")
        {
            // Find the columns by CssClass
            foreach (DataControlField column in grdPost.Columns)
            {
                if (column.HeaderText == "Edit")
                {
                    column.Visible = true; // Hide the Edit column
                }
                if (column.HeaderText == "Delete")
                {
                    column.Visible = true; // Hide the Delete column
                }
            }
        }
        else
        {
            foreach (DataControlField column in grdPost.Columns)
            {
                if (column.HeaderText == "Edit")
                {
                    column.Visible = false; // Hide the Edit column
                }
                if (column.HeaderText == "Delete")
                {
                    column.Visible = false; // Hide the Delete column
                }
            }
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
                        ddlULBType.Enabled = false;
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
}