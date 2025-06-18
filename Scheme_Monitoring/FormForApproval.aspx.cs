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
using System.Data.SqlClient;

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

            get_tbl_FinancialYear();
            get_tbl_Zone();
            get_tbl_Project();
            
            SetDropdownsBasedOnUserType();
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    private void get_tbl_FinancialYear()
    {
        DataSet ds = (new DataLayer()).get_tbl_FinancialYearForWorkProposal();
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

        ListItem itemToRemove = ddlProjectMaster.Items.FindByValue("13");
        if (itemToRemove != null)
        {
            ddlProjectMaster.Items.Remove(itemToRemove);
        }

        ListItem itemToRemove2 = ddlProjectMaster.Items.FindByValue("1019");
        if (itemToRemove2 != null)
        {
            ddlProjectMaster.Items.Remove(itemToRemove2);
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
    private void get_tbl_WorkType(int ProjectId)
    {
        DataSet ds = (new DataLayer()).get_tbl_ProjectType(ProjectId, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ddlWorkType.DataTextField = "ProjectType_Name";
            ddlWorkType.DataValueField = "ProjectType_Id";
            ddlWorkType.DataSource = ds.Tables[0];
            ddlWorkType.DataBind();
        }
        else
        {
            ddlWorkType.Items.Clear();
        }
    }
    private void get_tbl_MPMLA(string ProposerRole)
    {
        DataSet ds = (new DataLayer()).get_tbl_MPMLA(ProposerRole);
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
            if(ddlProjectMaster.SelectedValue=="16")
            {
                divSubScheme.Visible = true;
            }
            else
            {
                divSubScheme.Visible = false;
            }
            get_tbl_WorkType(ProjectId);
        }
    }
    protected void rblRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(rblRoles.SelectedValue!= "Others" && rblRoles.SelectedValue != "C&DS (नगरीय)" && rblRoles.SelectedValue != "उत्तर प्रदेश जल निगम (नगरीय)" 
            && rblRoles.SelectedValue != "Ex-MLA" && rblRoles.SelectedValue != "Ex-MP" && rblRoles.SelectedValue != "प्रदेश अध्यक्ष")
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

        if (!fileUploadRecommendationLetter.HasFile)
        {
            MessageBox.Show("Please choose recomendation letter.");
            fileUploadRecommendationLetter.Focus();
            return;
        }

        //Create List of Selected Project Work Type
        List<WorkProposal_ProjectType> objList = new List<WorkProposal_ProjectType>();
        foreach (ListItem listItem in ddlWorkType.Items)
        {
            if (listItem.Selected)
            {
                WorkProposal_ProjectType obj = new WorkProposal_ProjectType();
                obj.AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj.ProjectType_Id = Convert.ToInt32(listItem.Value);
                obj.Status = 1;
                objList.Add(obj);
            }
        }
        //Check Project Work Type is empty or not and alert
        if (objList == null || objList.Count == 0)
        {
            MessageBox.Show("Please Provide Work Type!");
            ddlWorkType.Focus();
            return;
        }

        string pdfLocation = UploadPDF();
        if (string.IsNullOrEmpty(pdfLocation))
        {
            pdfLocation = "";
        }

        tbl_WorkProposal WorkProposal = CreateWorkProposalObject(pdfLocation);
        string ConStr = ConfigurationManager.AppSettings.Get("conn").ToString();

        using (SqlConnection connection = new SqlConnection(ConStr))
        {
            connection.Open();

            SqlTransaction trans = null;

            try
            {
                // Begin transaction
                trans = connection.BeginTransaction();

                // Set transaction for commands
                SqlCommand command = connection.CreateCommand();
                command.Transaction = trans;

                DataTable dt = objLoan.InsertWorkProposal(WorkProposal);
                
                if (dt.Rows.Count>0)
                {
                    int result = Convert.ToInt32(dt.Rows[0]["WorkProposalId"].ToString());
                    string ProposalCode = dt.Rows[0]["ProposalCode"].ToString();
                    if(result!=0 && ProposalCode !="0")
                    {
                        if (objList != null && objList.Count > 0)
                        {
                            objList[0].Proposal_Id = result;
                            for (int i = 0; i < objList.Count; i++)
                            {
                                objList[i].Proposal_Id = result;
                                objLoan.Insert_WorkProposal_ProjectType(objList[i], trans, connection);
                            }
                        }
                        trans.Commit();
                        MessageBox.Show("Record with Work Proposal Code \"" + dt.Rows[0]["ProposalCode"].ToString() + "\" saved successfully.");
                        reset();
                        btnSearch_Click(null, EventArgs.Empty);
                    }
                    else
                    {
                        MessageBox.Show("We found duplicate record with the same Proposal Name, Financial Year, ULB and Scheme!");
                    }
                    
                }
                else
                {
                    trans.Rollback();
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
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show("Error: " + ex.Message);

                // Rollback transaction on error
                if (trans != null)
                {
                    try
                    {
                        trans.Rollback();
                    }
                    catch (Exception rollbackEx)
                    {
                        MessageBox.Show("Rollback error: " + rollbackEx.Message);
                    }
                }
            }
            finally
            {
                // Ensure the connection is closed and resources are released
                if (trans != null)
                {
                    //trans.Dispose();
                }
                connection.Close();
            }
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
        if (rblApprovedVP.SelectedValue != "1")
        {
            MessageBox.Show("Please Upload only committee-approved Proposal Letter.");
            rblApprovedVP.Focus();
            return false;
        }
        //if (txtZoneOfULB.Text.Trim() == "")
        //{
        //    MessageBox.Show("Please enter the names of the zones where the project will be built or implemented.");
        //    txtZoneOfULB.Focus();
        //    IsValid = false;
        //}
        //if (txtWard.Text.Trim() == "")
        //{
        //    MessageBox.Show("Please enter the names of the wards where the project will be built or implemented.");
        //    txtWard.Focus();
        //    IsValid = false;
        //}
        if (ddlProjectMaster.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Scheme. ");
            ddlProjectMaster.Focus();
            IsValid = false;
        }
        if(ddlProjectMaster.SelectedValue=="16")
        {
            if (rblSubScheme.SelectedValue == "")
            {
                MessageBox.Show("Please Select Sub Scheme. ");
                rblSubScheme.Focus();
                IsValid = false;
            }
        }
        if (ddlWorkType.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a type of Work. ");
            ddlWorkType.Focus();
            IsValid = false;
        }
        //if (txtProposalName.Text.Trim() == "")
        //{
        //    MessageBox.Show("Please enter Proposal Name.");
        //    txtProposalName.Focus();
        //    IsValid = false;
        //}
        if (txtExpectedAmount.Text.Trim() == "" || !double.TryParse(txtExpectedAmount.Text.Trim(), out result))
        {
            MessageBox.Show("Please enter valid expected amount.");
            txtExpectedAmount.Focus();
            IsValid = false;
        }
        if (rblRoles.SelectedValue != "-1" && rblRoles.SelectedValue != "Other")
        {
            if (ddlMPMLA.SelectedValue == "0")
            {
                MessageBox.Show("Please Select a Proposer. ");
                ddlMPMLA.Focus();
                IsValid = false;
            }
        }
        if (rblRoles.SelectedValue == "Other" || rblRoles.SelectedValue == "C&DS (नगरीय)" || rblRoles.SelectedValue == "उत्तर प्रदेश जल निगम (नगरीय)"
             || rblRoles.SelectedValue == "Ex-MLA" || rblRoles.SelectedValue == "Ex-MP" || rblRoles.SelectedValue == "प्रदेश अध्यक्ष")
        {
            //if (txtMobileNo.Text.Trim() == "")
            //{
            //    MessageBox.Show("Please enter mobile no of proposer.");
            //    txtMobileNo.Focus();
            //    IsValid = false;
            //}

            if (txtOthers.Text.Trim() == "")
            {
                MessageBox.Show("Please enter Name of proposer.");
                txtOthers.Focus();
                IsValid = false;
            }
        }

        if (txtProposalName.Text.Trim() == "")
        {
            MessageBox.Show("Please enter Proposal Name (Letter subject).");
            txtProposalName.Focus();
            IsValid = false;
        }

        if (txtProposalDetail.Text.Trim() == "")
        {
            MessageBox.Show("Please enter Proposal Detail (Work details).");
            txtProposalDetail.Focus();
            IsValid = false;
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
        string ProposalName = "", ProposalDetail = "";

        int SchemeId, SubSchemeId=0;
        double ExpectedAmount = 0.00;

        string ProposerType = "", ProposerName = "",ProposalDate="", MobileNo = "", Designation = "";
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
        if (ddlProjectMaster.SelectedValue == "16")
        {
            if (!int.TryParse(rblSubScheme.SelectedValue, out SubSchemeId))
            {
                throw new FormatException("Invalid Sub Scheme selected value.");
            }
        }
        else
        {
            SubSchemeId = 0;
        }
        //if (!int.TryParse(ddlWorkType.SelectedValue, out WorkType))
        //{
        //    throw new FormatException("Invalid Work Type selected value.");
        //}
        if (!double.TryParse(txtExpectedAmount.Text, out ExpectedAmount))
        {
            throw new FormatException("Invalid Expected Amount value.");
        }


        if (txtProposalName.Text != "")
        {
            ProposalName = txtProposalName.Text.ToString();
        }
        if (txtProposalDetail.Text != "")
        {
            ProposalDetail = txtProposalDetail.Text.ToString();
        }
        if (txtProposalDate.Text != "")
        {
            ProposalDate = txtProposalDate.Text.ToString();
        }
        else
        {
            ProposalDate = null;
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
            WorkType = 0,
            ExpectedAmount = ExpectedAmount,
            ProposerType = ProposerType,
            MPMLAid = MPMLAid,
            ProposerName = ProposerName,
            Mobile = MobileNo,
            Designation = Designation,
            RecomendationLetter = pdfLocation,
            AddedBy = personId,
            ProposalName = ProposalName,
            ProposalDetail = ProposalDetail,
            ProposalDate=ProposalDate,
            SubSchemeId=SubSchemeId

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
        //ddlProjectMaster.SelectedValue = "0";
        //ddlWorkType.Items.Clear();
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
        grdPost.DataSource = null;
        grdPost.DataBind();
    }

    //work form here
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
        dt = objLoan.getWorkProposalBySearch(obj, 0, "-1", -1);

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
            grdPost.AllowPaging = false;
            // Rebind your data here if needed
            tbl_WorkProposal obj = BindWorkProposalGridBySearch();
            LoadWorkProposalGrid(obj);

            grdPost.HeaderRow.Cells[0].Visible = false; // Work Proposal Id
            grdPost.HeaderRow.Cells[2].Visible = false; // Edit
            grdPost.HeaderRow.Cells[12].Visible = false; // Recommendation Letter

            foreach (GridViewRow row in grdPost.Rows)
            {
                row.Cells[0].Visible = false; // Work Proposal Id
                row.Cells[2].Visible = false; // Edit
                row.Cells[12].Visible = false; // Recommendation Letter
            }

            grdPost.HeaderRow.BackColor = System.Drawing.Color.White;
            foreach (TableCell cell in grdPost.HeaderRow.Cells)
            {
                cell.BackColor = grdPost.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grdPost.Rows)
            {
                row.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdPost.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdPost.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grdPost.RenderControl(hw);

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
        grdPost.PageIndex = e.NewPageIndex;
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
            GetWorkTypeByProposalId(WorkProposalId);
            
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

            if (ddlProjectMaster.SelectedValue == "16")
            {
                rblSubScheme.SelectedValue= dt.Rows[0]["SubSchemeId"].ToString();
            }
            else
            {
                rblSubScheme.SelectedIndex = -1;
            }

            txtProposalName.Text = dt.Rows[0]["ProposalName"].ToString();
            txtProposalDetail.Text = dt.Rows[0]["ProposalDetail"].ToString();
            string date = dt.Rows[0]["ProposalDate"].ToString();

            if(date!="")
            {
                txtProposalDate.Text = DateTime.Parse(dt.Rows[0]["ProposalDate"].ToString()).ToString("yyyy-MM-dd");
            }
            else
            {
                txtProposalDate.Text = "";
            }

;
            //if (dt.Rows[0]["ProposalDate"]!=null || dt.Rows[0]["ProposalDate"].ToString()!=string.Empty)
            //{
            //    txtProposalDate.Text=DateTime.Parse(dt.Rows[0]["ProposalDate"].ToString()).ToString("yyyy-MM-dd");
            //}
            //else
            //{
            //    txtProposalDate.Text = "";
            //}
            txtExpectedAmount.Text = dt.Rows[0]["ExpectedAmount"].ToString();

            rblRoles.SelectedValue= dt.Rows[0]["ProposerType"].ToString();
            rblRoles_SelectedIndexChanged(rblRoles, new EventArgs());
            if (rblRoles.SelectedValue.ToString()== "Others" || rblRoles.SelectedValue == "C&DS (नगरीय)" || rblRoles.SelectedValue == "उत्तर प्रदेश जल निगम (नगरीय)"
                 || rblRoles.SelectedValue == "Ex-MLA" || rblRoles.SelectedValue == "Ex-MP" || rblRoles.SelectedValue == "प्रदेश अध्यक्ष")
            {
                ddlMPMLA.Items.Clear();
                lblConstituencyName.Text = "";
                lblParyOfMPMLA.Text = "";
                txtOthers.Text = dt.Rows[0]["ProposerName"].ToString();
            }
            else
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
    protected void GetWorkTypeByProposalId(int WorkProposalId)
    {
        DataTable dt = new DataTable();
        dt = objLoan.getWorkTypeByProposal(WorkProposalId);

        if (dt != null && dt.Rows.Count > 0)
        {
            // Select all items in the ListBox based on DataTable values
            foreach (DataRow row in dt.Rows)
            {
                string valueToSelect = row["ProjectType_Id"].ToString();
                ListItem item = ddlWorkType.Items.FindByValue(valueToSelect);
                if (item != null)
                {
                    item.Selected = true;
                }
            }
        }
        else
        {
            foreach (ListItem item in ddlWorkType.Items)
            {
                item.Selected = false;
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!ValidateFields())
        {
            return;
        }

        // Create List of Selected Project Work Type
        List<WorkProposal_ProjectType> objList = new List<WorkProposal_ProjectType>();
        foreach (ListItem listItem in ddlWorkType.Items)
        {
            if (listItem.Selected)
            {
                WorkProposal_ProjectType objPT = new WorkProposal_ProjectType();
                objPT.AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                objPT.ProjectType_Id = Convert.ToInt32(listItem.Value);
                objPT.Status = 1;
                objList.Add(objPT);
            }
        }

        // Check if Project Work Type is empty or not and alert
        if (objList == null || objList.Count == 0)
        {
            MessageBox.Show("Please Provide Work Type!");
            ddlWorkType.Focus();
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
                MessageBox.Show("While updating this data, a new recommendation letter was uploaded but failed to delete the existing recommendation letter.");
            }
        }

        int WorkProposalId = Convert.ToInt32(hfWorkProposalId.Value.ToString());
        tbl_WorkProposal obj = CreateWorkProposalObject(pdfLocation);

        string ConStr = ConfigurationManager.AppSettings.Get("conn").ToString();
        using (SqlConnection connection = new SqlConnection(ConStr))
        {
            connection.Open();
            SqlTransaction trans = null;

            try
            {
                // Begin transaction
                trans = connection.BeginTransaction();

                // Set transaction for commands
                SqlCommand command = connection.CreateCommand();
                command.Transaction = trans;

                // Update WorkProposal
                int result = objLoan.UpdateWorkProposal(obj, WorkProposalId);
                if (result > 0)
                {
                    // Delete existing Project Types
                    objLoan.DeleteWorkProposalProjectTypes(WorkProposalId, trans, connection);

                    // Insert new Project Types
                    foreach (var projectType in objList)
                    {
                        projectType.Proposal_Id = WorkProposalId;
                        objLoan.Insert_WorkProposal_ProjectType(projectType, trans, connection);
                    }

                    // Commit transaction
                    trans.Commit();
                    MessageBox.Show("Record updated successfully!");
                    reset();
                    divData.Visible = false;
                }
                else
                {
                    // Rollback transaction
                    trans.Rollback();
                    MessageBox.Show("Something went wrong, please try again or contact the administrator!");
                    return;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show("Error: " + ex.Message);

                // Rollback transaction on error
                if (trans != null)
                {
                    try
                    {
                        trans.Rollback();
                    }
                    catch (Exception rollbackEx)
                    {
                        MessageBox.Show("Rollback error: " + rollbackEx.Message);
                    }
                }
            }
            finally
            {
                // Ensure the connection is closed and resources are released
                if (trans != null)
                {
                    //trans.Dispose();
                }
                connection.Close();
            }
        }
    }


    //protected void btnUpdate_Click(object sender, EventArgs e)
    //{
    //    if (!ValidateFields())
    //    {
    //        return;
    //    }
    //    string pdfLocation = UploadPDF();
    //    if (string.IsNullOrEmpty(pdfLocation))
    //    {
    //        pdfLocation = hfPDFUrl.Value;
    //    }
    //    else
    //    {
    //        if (!PDFUploader.DeletePDF(hfPDFUrl.Value))
    //        {
    //            MessageBox.Show("While updating this data, an new recomendation letter was uploaded but failed to be deleted existing recomendation letter.");
    //        }
    //    }
    //    int WorkProposalId = Convert.ToInt32(hfWorkProposalId.Value.ToString());
    //    tbl_WorkProposal obj = CreateWorkProposalObject(pdfLocation);
    //    int result = objLoan.UpdateWorkProposal(obj, WorkProposalId);
    //    if (result > 0)
    //    {
    //        MessageBox.Show("Record updated successfully!");
    //        reset();
    //        divData.Visible = false;
    //    }
    //    else
    //    {
    //        MessageBox.Show("Something went wrong please try again or contact administrator!");
    //        return;
    //    }
    //}
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
}