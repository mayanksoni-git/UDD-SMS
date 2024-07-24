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

public partial class AnnualActionPlan : System.Web.UI.Page
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
    private void get_tbl_WorkType(int ProjectId)
    {
        DataSet ds = (new DataLayer()).get_tbl_ProjectType(ProjectId, 0);
        FillDropDown(ds, ddlWorkType, "ProjectType_Name", "ProjectType_Id");
    }
    private void get_tbl_MPMLA(string MpMla)
    {
        DataSet ds = (new DataLayer()).get_tbl_MPMLA(MpMla);
        FillDropDown(ds, ddlMPMLA, "MPMLAName", "MPMLAId");
    }
    private void get_MPPLADataById(int MPMLAid,string MpMla)
    {
        DataSet ds = (new DataLayer()).get_MPPLADataById(MPMLAid, MpMla);
        if(ds.Tables.Count>0)
        {
            lblParyOfMPMLA.Text = ds.Tables[0].Rows[0]["PartyName"].ToString();
            lblConstituencyName.Text = ds.Tables[0].Rows[0]["ConstituencyName"].ToString();
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
    protected void rblRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(rblRoles.SelectedValue!= "Others")
        {
            string selectedValue = rblRoles.SelectedValue;
            divMPMLA.Visible = true;
            divParty.Visible = true;
            divConstituency.Visible = true;
            divOthers.Visible = false;

            // Call a method to retrieve data based on the selected value
            get_tbl_MPMLA(selectedValue);
        }
        else
        {
            divMPMLA.Visible = false;
            divParty.Visible = false;
            divConstituency.Visible = false;
            divOthers.Visible = true;
            ddlMPMLA.Items.Clear();
        }
    }
    protected void ddlMPMLA_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMPMLA.SelectedValue != "0" && rblRoles.SelectedValue!="Other")
        {
            int MPMLAid = 0;
            try
            {
                MPMLAid = Convert.ToInt32(ddlMPMLA.SelectedValue);
            }
            catch
            {
                MPMLAid = 0;
            }
            get_MPPLADataById(MPMLAid, rblRoles.SelectedValue.ToString());
        }
        else
        {
            lblConstituencyName.Text = "";
            lblParyOfMPMLA.Text = "";
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!ValidateFields())
        {
            return;
        }

        string pdfLocation = UploadPDF();
        if (string.IsNullOrEmpty(pdfLocation))
        {
            pdfLocation = "";
        }

        tbl_WorkProposal WorkProposal = CreateWorkProposalObject(pdfLocation);
        int result = objLoan.InsertWorkProposal(WorkProposal);

        if (result > 0)
        {
            MessageBox.Show("Record saved successfully.");
            reset();
        }
        else
        {
            reset();
            bool IsPDFDelete = PDFUploader.DeletePDF(pdfLocation);
            if (!IsPDFDelete)
            {
                MessageBox.Show("Something went wrong please try again or contact administrator!. While processing this data, recomendation letter was uploaded but failed to be deleted.");
            }
            else
            {
                MessageBox.Show("Something went wrong please try again or contact administrator!");
            }
            return;
        }
    }
    public bool ValidateFields()
    {
        double result;
        bool IsValid = true;
        if (ddlFY.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Financial Year. ");
            ddlFY.Focus();
            IsValid = false;
        }

        if (ddlZone.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a State. ");
            ddlZone.Focus();
            IsValid = false;
        }
        if (ddlCircle.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a District. ");
            ddlCircle.Focus();
            IsValid = false;
        }
        if (ddlDivision.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a ULB. ");
            ddlDivision.Focus();
            IsValid = false;
        }
        if (txtZoneOfULB.Text.Trim() == "")
        {
            MessageBox.Show("Please enter the names of the zones where the project will be built or implemented.");
            txtZoneOfULB.Focus();
            IsValid = false;
        }
        if (txtWard.Text.Trim() == "")
        {
            MessageBox.Show("Please enter the names of the wards where the project will be built or implemented.");
            txtWard.Focus();
            IsValid = false;
        }
        if (ddlProjectMaster.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Scheme. ");
            ddlProjectMaster.Focus();
            IsValid = false;
        }
        if (ddlWorkType.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a type of Work. ");
            ddlWorkType.Focus();
            IsValid = false;
        }
        if (txtExpectedAmount.Text.Trim() == "" || !double.TryParse(txtExpectedAmount.Text.Trim(), out result))
        {
            MessageBox.Show("Please enter valid expected amount.");
            txtExpectedAmount.Focus();
            IsValid = false;
        }
        if (rblRoles.SelectedValue == "MP" || rblRoles.SelectedValue == "MLA")
        {
            if (ddlMPMLA.SelectedValue == "0")
            {
                MessageBox.Show("Please Select a MPMLA. ");
                ddlWorkType.Focus();
                IsValid = false;
            }
        }
        if (rblRoles.SelectedValue == "Other")
        {
            if (txtMobileNo.Text.Trim() == "")
            {
                MessageBox.Show("Please enter mobile no of proposer.");
                ddlWorkType.Focus();
                IsValid = false;
            }
        }

        return IsValid;
    }
    private string UploadPDF()
    {
        if (fileUploadRecommendationLetter.HasFile)
        {
            string errorMessage;
            string pdfLocation = PDFUploader.UploadPDF(fileUploadRecommendationLetter.PostedFile, out errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show("Error: " + errorMessage);
                return null;
            }

            return pdfLocation;
        }

        return null;
    }
    private tbl_WorkProposal CreateWorkProposalObject( string pdfLocation)
    {
        int personId;
        int FY, zone, circle, division;
        string ZoneOfULB = "", Ward = "";

        int SchemeId, WorkType;
        double ExpectedAmount = 0.00;

        string ProposerType = "", ProposerName = "", MobileNo = "", Designation = "";
        int MPMLAid;


        if (!int.TryParse(Session["Person_Id"].ToString(), out personId))
        {
            throw new FormatException("Invalid Person_Id in session.");
        }

        if (!int.TryParse(ddlFY.SelectedValue, out FY))
        {
            throw new FormatException("Invalid Financial Year selected value.");
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
        if (txtZoneOfULB.Text != "")
        {
            ZoneOfULB = txtZoneOfULB.Text.ToString();
        }
        if (txtWard.Text != "")
        {
            Ward = txtWard.Text.ToString();
        }

        if (!int.TryParse(ddlProjectMaster.SelectedValue, out SchemeId))
        {
            throw new FormatException("Invalid Scheme selected value.");
        }
        if (!int.TryParse(ddlWorkType.SelectedValue, out WorkType))
        {
            throw new FormatException("Invalid Work Type selected value.");
        }
        if (!double.TryParse(txtExpectedAmount.Text, out ExpectedAmount))
        {
            throw new FormatException("Invalid Expected Amount value.");
        }

        if (string.IsNullOrEmpty(rblRoles.SelectedValue))
        {
            throw new FormatException("Invalid proposer type value.");
        }
        else
        {
            ProposerType = rblRoles.SelectedValue.ToString();
        }

        if (!int.TryParse(ddlMPMLA.SelectedValue, out MPMLAid))
        {
            MPMLAid = 0;
            //throw new FormatException("Invalid MPMLA selected value.");
        }
        if (txtOthers.Text != "")
        {
            ProposerName = txtOthers.Text.ToString();
        }

        if (txtMobileNo.Text != "")
        {
            MobileNo = txtMobileNo.Text.ToString();
        }

        if (txtDesignation.Text != "")
        {
            Designation = txtDesignation.Text.ToString();
        }


        return new tbl_WorkProposal
        {
            FY = FY,
            Zone = zone,
            Circle = circle,
            Division = division,
            ZoneOfULB = ZoneOfULB,
            Ward = Ward,
            Scheme = SchemeId,
            WorkType = WorkType,
            ExpectedAmount = ExpectedAmount,
            ProposerType = ProposerType,
            MPMLAid = MPMLAid,
            ProposerName = ProposerName,
            Mobile = MobileNo,
            Designation = Designation,
            RecomendationLetter = pdfLocation,
            AddedBy = personId,

        };
    }
    private void reset()
    {
        ddlFY.SelectedValue = "0";
        //ddlZone.SelectedValue = "0";
        ddlCircle.SelectedValue = "0";
        ddlDivision.Items.Clear();
        txtZoneOfULB.Text = "";
        txtWard.Text = "";
        ddlProjectMaster.SelectedValue = "0";
        ddlWorkType.Items.Clear();
        txtExpectedAmount.Text = "";
        rblRoles.SelectedIndex = -1;
        ddlMPMLA.Items.Clear();
        lblConstituencyName.Text = "";
        lblParyOfMPMLA.Text = "";
        txtOthers.Text = "";
        txtMobileNo.Text = "";
        txtDesignation.Text = "";
        hypRecommendationLetterEdit.Visible = false;
        hypRecommendationLetterEdit.NavigateUrl = "";
        btnSave.Visible = true;
        btnUpdate.Visible = false;
        gvRecords.DataSource = null;
        gvRecords.DataBind();
    }

    //work form here
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        tbl_WorkProposal obj = BindWorkProposalGridBySearch();
        LoadWorkProposalGrid(obj);
    }

    protected tbl_WorkProposal BindWorkProposalGridBySearch()
    {
        int Fy=0, Zone_Id = 0, Circle_Id = 0, Division_Id = 0, Scheme=0;

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

        tbl_WorkProposal obj = new tbl_WorkProposal();
        obj.FY = Fy;
        obj.Zone = Zone_Id;
        obj.Circle = Circle_Id;
        obj.Division = Division_Id;
        obj.Scheme = Scheme;
        obj.ProposalStatus = -1;

        return obj;
    }
    private void LoadWorkProposalGrid(tbl_WorkProposal obj)
    {
        DataTable dt = new DataTable();
        dt = objLoan.getWorkProposalBySearch(obj);

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
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }

    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // To export all pages
            gvRecords.AllowPaging = false;
            // Rebind your data here if needed
            tbl_WorkProposal obj = BindWorkProposalGridBySearch();
            LoadWorkProposalGrid(obj);

            gvRecords.HeaderRow.Cells[0].Visible = false; // Work Proposal Id
            gvRecords.HeaderRow.Cells[2].Visible = false; // Edit
            gvRecords.HeaderRow.Cells[18].Visible = false; // Recommendation Letter

            foreach (GridViewRow row in gvRecords.Rows)
            {
                row.Cells[0].Visible = false; // Work Proposal Id
                row.Cells[2].Visible = false; // Edit
                row.Cells[18].Visible = false; // Recommendation Letter
            }

            gvRecords.HeaderRow.BackColor = System.Drawing.Color.White;
            foreach (TableCell cell in gvRecords.HeaderRow.Cells)
            {
                cell.BackColor = gvRecords.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in gvRecords.Rows)
            {
                row.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = gvRecords.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = gvRecords.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            gvRecords.RenderControl(hw);

            // Style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for exporting to work
    }
    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRecords.PageIndex = e.NewPageIndex;
        tbl_WorkProposal obj = BindWorkProposalGridBySearch();
        LoadWorkProposalGrid(obj);        
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int WorkProposalId = Convert.ToInt32(gr.Cells[0].Text.Trim());
        if (WorkProposalId > 0)
        {
            reset();
            Load_WorkProposal(WorkProposalId);
            ToggleFormMode(true); // switch to update mode
        }
        else
        {
            return;
        }
    }
    protected void Load_WorkProposal(int WorkProposalId)
    {
        DataTable dt = objLoan.getWorkProposalById(WorkProposalId);
        if (dt != null && dt.Rows.Count > 0)
        {
            hfWorkProposalId.Value = WorkProposalId.ToString();

            try
            {
                ddlFY.SelectedValue = dt.Rows[0]["FY"].ToString();
            }
            catch
            {
                ddlFY.SelectedValue = "0";
            }

            try
            {
                ddlZone.SelectedValue = dt.Rows[0]["Zone"].ToString();
            }
            catch
            {
                ddlZone.SelectedValue = "0";
            }

            ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());
            try
            {
                ddlCircle.SelectedValue = dt.Rows[0]["Circle"].ToString();
            }
            catch
            {
                ddlCircle.SelectedValue = "0";
            }

            ddlCircle_SelectedIndexChanged(ddlCircle, new EventArgs());
            try
            {
                ddlDivision.SelectedValue = dt.Rows[0]["Division"].ToString();
            }
            catch
            {
                ddlDivision.SelectedValue = "0";
            }

            txtZoneOfULB.Text = dt.Rows[0]["ZoneOfULB"].ToString(); ;
            txtWard.Text = dt.Rows[0]["Ward"].ToString(); ;

            try
            {
                ddlProjectMaster.SelectedValue = dt.Rows[0]["Scheme"].ToString();
            }
            catch
            {
                ddlProjectMaster.SelectedValue = "0";
            }

            ddlProjectMaster_SelectedIndexChanged(ddlProjectMaster, new EventArgs());
            try
            {
                ddlWorkType.SelectedValue = dt.Rows[0]["WorkType"].ToString();
            }
            catch
            {
                ddlWorkType.SelectedValue = "0";
            }

            txtExpectedAmount.Text = dt.Rows[0]["ExpectedAmount"].ToString();

            rblRoles.SelectedValue= dt.Rows[0]["ProposerType"].ToString();
            rblRoles_SelectedIndexChanged(rblRoles, new EventArgs());
            if (rblRoles.SelectedValue.ToString()=="MP" || rblRoles.SelectedValue.ToString()=="MLA")
            {
                txtOthers.Text = "";
                try
                {
                    ddlMPMLA.SelectedValue = dt.Rows[0]["MPMLAid"].ToString();
                    ddlMPMLA_SelectedIndexChanged(ddlCircle, new EventArgs());
                }
                catch
                {
                    ddlMPMLA.SelectedValue = "0";
                }
            }
            else
            {
                ddlMPMLA.Items.Clear();
                lblConstituencyName.Text = "";
                lblParyOfMPMLA.Text = "";
                txtOthers.Text= dt.Rows[0]["ProposerName"].ToString();
            }
            txtMobileNo.Text = dt.Rows[0]["Mobile"].ToString();
            txtDesignation.Text = dt.Rows[0]["Designation"].ToString();
            hfPDFUrl.Value = dt.Rows[0]["RecomendationLetter"].ToString();
            if(!string.IsNullOrEmpty(hfPDFUrl.Value.ToString()))
            {
                hypRecommendationLetterEdit.Visible = true;
                hypRecommendationLetterEdit.NavigateUrl= dt.Rows[0]["RecomendationLetter"].ToString();
            }
            btnSave.Visible = false;
            btnUpdate.Visible = true;
        }
        else
        {
            btnSave.Visible = true;
            btnUpdate.Visible = false;

            MessageBox.Show("Record with Work Proposal id = " + WorkProposalId.ToString() + " does not found please contact administrator.");
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!ValidateFields())
        {
            return;
        }
        string pdfLocation = UploadPDF();
        if (string.IsNullOrEmpty(pdfLocation))
        {
            pdfLocation = hfPDFUrl.Value;
        }
        else
        {
            if (!PDFUploader.DeletePDF(hfPDFUrl.Value))
            {
                MessageBox.Show("While updating this data, an new recomendation letter was uploaded but failed to be deleted existing recomendation letter.");
            }
        }
        int WorkProposalId = Convert.ToInt32(hfWorkProposalId.Value.ToString());
        tbl_WorkProposal obj = CreateWorkProposalObject(pdfLocation);
        int result = objLoan.UpdateWorkProposal(obj, WorkProposalId);
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        reset();
    }
    
    private void ToggleFormMode(bool isUpdateMode)
    {
        // Toggle between save and update modes
        btnSave.Visible = !isUpdateMode;
        btnUpdate.Visible = isUpdateMode;
        btnCancel.Visible = isUpdateMode;
    }
    protected void cvFileSize_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (fileUploadRecommendationLetter.HasFile)
        {
            // Check the file size (5MB = 5 * 1024 * 1024 bytes)
            if (fileUploadRecommendationLetter.PostedFile.ContentLength > 5 * 1024 * 1024)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
        else
        {
            // If no file is uploaded, validation passes (depending on your requirements)
            args.IsValid = true;
        }
    }
}