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

public partial class FormForApproval2 : System.Web.UI.Page
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
        if (Session["UserTypeName"].ToString() != "Administrator" /* Session["UserType"]*/)
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
        FillDropDown(ds, ddlFY1, "FinancialYear_Comments", "FinancialYear_Id");
        FillDropDown(ds, ddlFY2, "FinancialYear_Comments", "FinancialYear_Id");
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
            AllClasses.FillDropDown2(ds.Tables[0], ddl, textField, valueField);
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindWorkProposalGridBySearch();
    }

    protected void BindWorkProposalGridBySearch()
    {
        int Fy = 0, Zone_Id = 0, Circle_Id = 0, Division_Id = 0, Scheme = 0, ProposalStatus=0;

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
            ProposalStatus = Convert.ToInt32(ddlProposalStatus.SelectedValue);
        }
        catch
        {
            ProposalStatus = 0;
        }

        tbl_WorkProposal obj = new tbl_WorkProposal();
        obj.FY = Fy;
        obj.Zone = Zone_Id;
        obj.Circle = Circle_Id;
        obj.Division = Division_Id;
        obj.Scheme = Scheme;
        obj.ProposalStatus = ProposalStatus;

        int AkanshiULB = chkIsAkanshiULB.Checked ? 1 : -1;

        LoadWorkProposalGrid(obj);
        btnHideModal.Visible = false;
        btnShowModal.Visible = false;

    }
    private void LoadWorkProposalGrid(tbl_WorkProposal obj)
    {
        DataTable dt = new DataTable();
        int AkanshiULB = chkIsAkanshiULB.Checked ? 1 : -1;
        dt = objLoan.getWorkProposalBySearch(obj, 0, "-1", AkanshiULB);

        if (dt != null && dt.Rows.Count > 0)
        {
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
    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRecords.PageIndex = e.NewPageIndex;
        BindWorkProposalGridBySearch();
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
            //.Visible = false;
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
        btnSearch.Visible = true;
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



            btnSearch.Visible = false;
        }
        else
        {
            btnSearch.Visible = true;
           
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
        btnSearch.Visible = !isUpdateMode;
        btnCancel.Visible = isUpdateMode;
    }

    protected void btnShowModal_Click(object sender, EventArgs e)
    {
        // Show the modal
        //fundStatusModal.Style["display"] = "block";
        divFundStatus.Visible = true;
        btnShowModal.Visible = false;
        btnHideModal.Visible = true;
    }

    protected void btnHideModal_Click(object sender, EventArgs e)
    {
        // Hide the modal
        //fundStatusModal.Style["display"] = "none";
        divFundStatus.Visible = false;
        btnShowModal.Visible = true;
        btnHideModal.Visible = false;
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        // Hide the modal when close button is clicked
        //fundStatusModal.Style["display"] = "none";
        divFundStatus.Visible = false;
    }

    protected void btnSubmitAction_Click(object sender, EventArgs e)
    {
        //string Remarks = txtRemarks.Text;
        //int status = Convert.ToInt16(ddlAction.SelectedValue);
        //int id = Convert.ToInt32(hfFormApproval_Id.Value);
        //int personId;
        //if (!int.TryParse(Session["Person_Id"].ToString(), out personId))
        //{
        //    throw new FormatException("Invalid Person_Id in session.");
        //}
        //int result = objLoan.ActionOnWorkProposal(Remarks, status, id, personId);

        //if (result > 0)
        //{
        //    MessageBox.Show("Record Updated successfully.");
        //    mpActionProposal.Visible = false;
        //    BindWorkProposalGridBySearch();
        //}
        //else
        //{
        //    MessageBox.Show("Something went wrong please try again or contact administrator!");
        //    return;
        //}
        
    }

    protected void btnCloseActionProposal_Click(object sender, EventArgs e)
    {
        mpActionProposal.Visible = false;
    }


    protected void btnAction_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Action")
        {
            // Retrieve the command argument (WorkProposalId)
            int workProposalId;
            if (int.TryParse(e.CommandArgument.ToString(), out workProposalId))
            {
                // Call a method to handle the action based on WorkProposalId
                int id = Convert.ToInt32(e.CommandArgument);
                hfFormApproval_Id.Value = id.ToString();
                workProposalId = id;
                mpActionProposal.Visible = true;
                string WorkProposalCode = objLoan.GetWorkProposalCode(workProposalId);
                if (!string.IsNullOrEmpty(WorkProposalCode))
                {
                    lblWrokProposalId.Text = "Work Proposal Code= " + WorkProposalCode;
                }
                else
                {
                    lblWrokProposalId.Text = "Work Proposal Id= " + workProposalId;
                }
                //HandleAction(workProposalId);
            }
            else
            {
                // Handle error if parsing fails
            }
            Response.Redirect("FormForApproval3.aspx?WorkProposalId=" + workProposalId.ToString());
        }
    }
        //protected void btnAction_Click(object sender, EventArgs e)
        //{
        //    ExportHtmlTableToExcel(gvRecords, "ExportedData");

        //}


    protected void ExportButton_Click(object sender, EventArgs e)
    {
        ExportGridViewToExcel(gvRecords, "ExportedData");
    }

    private DataTable GetData()
    {
        // Sample data; replace with your actual data fetching logic
        DataTable table = new DataTable();
        table.Columns.Add("Column1", typeof(string));
        table.Columns.Add("Column2", typeof(int));
        table.Columns.Add("Column3", typeof(string));
        table.Columns.Add("Column4", typeof(DateTime));
        table.Columns.Add("Column5", typeof(decimal));
        table.Columns.Add("Column6", typeof(decimal));
        table.Columns.Add("Column7", typeof(decimal));
        table.Columns.Add("Column8", typeof(decimal));
        table.Columns.Add("Column9", typeof(decimal));
        table.Columns.Add("Column10", typeof(decimal));
        table.Columns.Add("Column11", typeof(decimal));
        table.Columns.Add("Column12", typeof(decimal));
        table.Columns.Add("Column13", typeof(decimal));
        table.Columns.Add("Column14", typeof(decimal));
        table.Columns.Add("Column15", typeof(decimal));
        table.Columns.Add("Column16", typeof(decimal));
        table.Columns.Add("Column5", typeof(decimal));

        table.Rows.Add("Row1Data1", 1, "Row1Data3", DateTime.Now, 100.50m);
        table.Rows.Add("Row2Data1", 2, "Row2Data3", DateTime.Now.AddDays(1), 200.75m);
        return table;
    }

    private void ExportGridViewToExcel(GridView gridView, string fileName)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            // Hide the controls we don't want to export
            gridView.AllowPaging = false;
            gridView.DataBind();

            // Render the GridView to the HtmlTextWriter
            gridView.RenderControl(htw);

            // Write the rendered content to the response
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the specified ASP.NET
        // server control at run time. Required for exporting to work.
    }

}

   

   
