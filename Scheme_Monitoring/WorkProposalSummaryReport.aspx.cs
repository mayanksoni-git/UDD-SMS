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
using System.Web.UI.DataVisualization.Charting;
using System.Web.Script.Serialization;
using System.Web.Services;

public partial class WorkProposalSummaryReport : System.Web.UI.Page
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

            get_tbl_FinancialYear();
            get_tbl_Zone();
            get_tbl_Section();
            get_tbl_Project();

            SetDropdownsBasedOnUserType();
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    private void get_tbl_FinancialYear()
    {
        DataSet ds = (new DataLayer()).get_tbl_FinancialYear();
        FillDropDown(ds, ddlFY, "FinancialYear_Comments", "FinancialYear_Id");
    }
    private void get_tbl_Section()
    {
        DataSet ds = (new DataLayer()).get_tbl_Section();
        FillDropDown(ds, ddlSection, "Section_Name", "Section_Id");
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
        FillDropDown(ds, ddlProjectMaster, "Project_Name", "Project_Id");
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

    private void reset()
    {
        ddlFY.SelectedValue = "0";
        ddlCircle.SelectedValue = "0";
        ddlDivision.Items.Clear();
        ddlProjectMaster.SelectedValue = "0";
        grdPost.DataSource = null;
        grdPost.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Session["UserType"].ToString() != "1")
        {
            if (ddlProjectMaster.SelectedValue == "0")
            {
                MessageBox.Show("Please Select A Scheme");
                ddlProjectMaster.Focus();
                return;
            }
        }
        
        tbl_WorkProposal obj = BindWorkProposalGridBySearch();
        LoadWorkProposalGrid(obj);
    }

    


    protected tbl_WorkProposal BindWorkProposalGridBySearch()
    {
        int Fy=0, Zone_Id = 0, Circle_Id = 0, Division_Id = 0, Scheme=0, Section=0;
        string Role;

        try
        {
            Fy = Convert.ToInt32(ddlFY.SelectedValue);
        }
        catch
        {
            Fy = 0;
        }

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

        try
        {
            Scheme = Convert.ToInt32(ddlProjectMaster.SelectedValue);
        }
        catch
        {
            Scheme = 0;
        }

        try
        {
            Section = Convert.ToInt32(ddlSection.SelectedValue);
        }
        catch
        {
            Section = 0;
        }

        try
        {
            Role = rblRoles.SelectedValue.ToString();
        }
        catch
        {
            Role = "";
        }

        tbl_WorkProposal obj = new tbl_WorkProposal();
        obj.FY = Fy;
        obj.Zone = Zone_Id;
        obj.Circle = Circle_Id;
        obj.Division = Division_Id;
        obj.Scheme = Scheme;
        obj.ProposerType = Role;
        obj.Section = Section;
        obj.ProposalStatus = -1;

        return obj;
    }
    private void LoadWorkProposalGrid(tbl_WorkProposal obj)
    {
        DataTable dt = new DataTable();
        dt = objLoan.getWorkProposalSummary(obj);

        if (dt != null && dt.Rows.Count > 0)
        {
            grdPost.DataSource = dt;
            grdPost.DataBind();
            divData.Visible = true;
        }
        else
        {
            divData.Visible = true;
            grdPost.DataSource = null;
            grdPost.DataBind();
            MessageBox.Show("No Records Found");
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        reset();
    }
}