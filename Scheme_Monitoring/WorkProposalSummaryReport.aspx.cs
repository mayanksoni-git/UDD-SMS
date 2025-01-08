using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            get_tbl_Mandal();
            get_tbl_Section();

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
        DataSet ds = (new DataLayer()).get_tbl_DivisionByULBType(circleId, ULBType);
        FillDropDown(ds, ddlDivision, "Division_Name", "Division_Id");
    }
    private void get_tbl_Project(int SectionId)
    {
        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_ProjectBySection(0, SectionId);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_ProjectBySection(Convert.ToInt32(Session["Person_Id"].ToString()), SectionId);
        }
        FillDropDown(ds, ddlProjectMaster, "Project_Name", "Project_Id");
        ListItem itemToRemove = ddlProjectMaster.Items.FindByValue("1019");
        ddlProjectMaster.Items.Remove(itemToRemove);
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
        else
        {
            get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue), ddlULBType.SelectedValue.ToString());
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
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSection.SelectedValue == "-1")
        {
            lblMessage.Text = "Please Select a Section.";
            ddlSection.Focus();
        }
        else
        {
            get_tbl_Project(Convert.ToInt32(ddlSection.SelectedValue));
        }
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
        int Mandal = 0;
        string ULBType = "";
        try
        {
            Mandal = Convert.ToInt32(ddlMandal.SelectedValue);
        }
        catch
        {
            Mandal = 0;
        }

        try
        {
            ULBType = ddlULBType.SelectedValue.ToString();
        }
        catch
        {
            ULBType = "";
        }
        int AkanshiULB = chkIsAkanshiULB.Checked ? 1 : -1;


        LoadWorkProposalGrid(obj, Mandal, ULBType, AkanshiULB);
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

    private void LoadWorkProposalGrid(tbl_WorkProposal obj, int Mandal, string ULBType, int AkanshiULB)
    {
        DataTable dt = new DataTable();
        dt = objLoan.getWorkProposalSummary(obj, Mandal, ULBType, AkanshiULB);

        if (dt != null && dt.Rows.Count > 0)
        {
            grdPost.DataSource = dt;
            grdPost.DataBind();
            //divData.Visible = true;
            Label lblTotalText = (Label)grdPost.FooterRow.FindControl("lblTotalText") as Label;
            grdPost.FooterRow.Font.Bold = true;
            lblTotalText.Text = "Total";
            decimal TotalAmount = dt.Compute("Sum(TotalAmount)", "").ToString().ToDecimal();
            Label lblTotalAmount = (Label)grdPost.FooterRow.FindControl("lblTotalAmount") as Label;
            lblTotalAmount.Text = TotalAmount.ToString("0.00");
            int TotalNoOfProposal = Convert.ToInt32(dt.Compute("Sum(NoOfProposals)", "").ToString());
            Label lblTotalNoOfProposal = (Label)grdPost.FooterRow.FindControl("lblTotalNoOfProposal") as Label;
            lblTotalNoOfProposal.Text = TotalNoOfProposal.ToString();
            int lblApprovedProposals = Convert.ToInt32(dt.Compute("Sum(ApprovedProposals)", "").ToString());
            Label lblApprovedProposal = (Label)grdPost.FooterRow.FindControl("lblApprovedProposal") as Label;
            lblApprovedProposal.Text = lblApprovedProposals.ToString();
            int lblHoldProposals = Convert.ToInt32(dt.Compute("Sum(HoldProposals)", "").ToString());
            Label lblHoldProposal = (Label)grdPost.FooterRow.FindControl("lblHoldProposal") as Label;
            lblHoldProposal.Text = lblHoldProposals.ToString();
            int PendingProposals = Convert.ToInt32(dt.Compute("Sum(PendingProposals)", "").ToString());
            Label lblPendingProposal = (Label)grdPost.FooterRow.FindControl("lblPendingProposal") as Label;
            lblPendingProposal.Text = PendingProposals.ToString();


            //grdPost.DataSource = dt;
            //grdPost.DataBind();
            divData.Visible = true;
           //ToggleDiv(divData);


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

    private void ToggleDiv(System.Web.UI.HtmlControls.HtmlGenericControl div)
    {
        //divData.Visible = false;
        //divMPWise.Visible = false;
        //divMLAWise.Visible = false;
        //divDivisionWise.Visible = false;
        //divWorkPlanWise.Visible = false;
        //divDistrictWise.Visible = false;
        //divRecommendationWise.Visible = false;
        div.Visible = true;
        divData.Visible = true;
        div.Focus();
    }
}