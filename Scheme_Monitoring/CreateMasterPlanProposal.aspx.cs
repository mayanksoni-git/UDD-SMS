using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateMasterPlanProposal : System.Web.UI.Page
{
    StormWaterDrainage obj = new StormWaterDrainage();

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
                hfMasterPlanProposalId.Value = Request.QueryString["MasterPlanProposalID"].ToString();
                GetMasterPlanProposalById(Convert.ToInt32(hfMasterPlanProposalId.Value));
            }

            lblZoneH.Text = Session["Default_Zone"].ToString() + "*";
            lblCircleH.Text = Session["Default_Circle"].ToString() + "*";
            lblDivisionH.Text = Session["Default_Division"].ToString() + "*";

            get_tbl_FinancialYear();
            get_tbl_Zone();
            SetDropdownsBasedOnUserType();
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    protected void GetMasterPlanProposalById(int MasterPlanProposalId)
    {
        DataTable dt = new DataTable();
        dt = obj.getGetMasterPlanProposalById(MasterPlanProposalId);

        if (dt != null && dt.Rows.Count > 0)
        {
            ddlZone.SelectedValue = dt.Rows[0]["Zone_Id"].ToString();
            ddlCircle.SelectedValue = dt.Rows[0]["Circle_Id"].ToString();
            get_tbl_Division(Convert.ToInt32(dt.Rows[0]["Circle_Id"].ToString()));
            ddlDivision.SelectedValue = dt.Rows[0]["Division_Id"].ToString();
            ddlFY.SelectedValue = dt.Rows[0]["FinancialYear_Id"].ToString();
            txtProposalName.Text = dt.Rows[0]["ProposalName"].ToString();
            txtProposalDetail.Text = dt.Rows[0]["ProposalDetail"].ToString();
            txtMobileNo.Text = dt.Rows[0]["MobileNo"].ToString();
            txtExpAmt.Text = dt.Rows[0]["ExpAmt"].ToString();
            hfMasterPlanProposalId.Value = dt.Rows[0]["SrNo"].ToString();

            hfPDFUrl.Value = dt.Rows[0]["MasterPlanProposalFilePath"].ToString();
            if (!string.IsNullOrEmpty(hfPDFUrl.Value.ToString()))
            {
                hypMasterPlanProposalDocEdit.Visible = true;
                hypMasterPlanProposalDocEdit.NavigateUrl = dt.Rows[0]["MasterPlanProposalFilePath"].ToString();
            }

            btnUpdate.Visible = true;
            btnSave.Visible = false;
        }
        else
        {
            // exportToExcel.Visible = false;

        }
    }


    private void get_tbl_FinancialYear()
    {
        DataSet ds = (new DataLayer()).get_tbl_FinancialYearFromId(19);
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
        btnUpdate.Visible = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        reset();
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


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!ValidateFields())
        {
            return;
        }

        if (!fileUploadMasterPlanProposalDoc.HasFile)
        {
            MessageBox.Show("Please choose master plan document.");
            fileUploadMasterPlanProposalDoc.Focus();
            return;
        }

        tbl_MasterPlanProposal obj_MasterPlanProposal = CreateMasterPlanProposalObject();

        string pdfLocation = UploadPDF();
        if (string.IsNullOrEmpty(pdfLocation))
        {
            pdfLocation = "";
        }
        obj_MasterPlanProposal.MasterPlanProposalFilePath = pdfLocation;

        try
        {
            int result = obj.InsertMasterPlanProposal(obj_MasterPlanProposal);

            if (result > 0)
            {
                lblMessage.Text = "Record saved successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                reset();
            }
            else
            {
                bool IsPDFDelete = PDFUploader.DeletePDF(pdfLocation);
                if (!IsPDFDelete)
                {
                    MessageBox.Show("Something went wrong please try again or contact administrator!. While processing this data, master plan proposal document was uploaded but failed to be deleted.");
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
            MessageBox.Show("An error occurred: " + ex.Message);
        }
    }

    private string UploadPDF()
    {
        int MaxPdfSize = 10 * 1024 * 1024;
        string PdfDirectory = "~/PDFs/MasterPlanProposal/";
        if (fileUploadMasterPlanProposalDoc.HasFile)
        {
            string errorMessage;
            string pdfLocation = PDFUploader.UploadPDFWithSizeAndPath(fileUploadMasterPlanProposalDoc.PostedFile, PdfDirectory, MaxPdfSize,  out errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show("Error: " + errorMessage);
                return null;
            }

            return pdfLocation;
        }

        return null;
    }

    public bool ValidateFields()
    {
        bool IsValid = true;
        if (ddlFY.SelectedValue == "0")
        {
            MessageBox.Show("Please Select a Year. ");
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
        if (txtMobileNo.Text == "")
        {
            lblMessage.Text = "You cannot enter record without mobile no!";
            txtMobileNo.Focus();
            IsValid = false;
        }
        if (txtExpAmt.Text == "" || double.Parse(txtExpAmt.Text) <= 0)
        {
            lblMessage.Text = "You cannot enter record with 0 Expected Amount!";
            txtExpAmt.Focus();
            IsValid = false;
        }
        if (txtProposalName.Text == "")
        {
            lblMessage.Text = "Please Enter Proposal Amount!";
            txtProposalName.Focus();
            IsValid = false;
        }
        if (txtProposalDetail.Text == "")
        {
            lblMessage.Text = "Please Enter Proposal Detail!";
            txtProposalDetail.Focus();
            IsValid = false;
        }
        if (!fileUploadMasterPlanProposalDoc.HasFile)
        {
            lblMessage.Text = "Please choose master plan proposal document!";
            fileUploadMasterPlanProposalDoc.Focus();
            IsValid = false;
        }

        return IsValid;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!ValidateFields())
        {
            return;
        }


        tbl_MasterPlanProposal obj_MasterPlanProposal = CreateMasterPlanProposalObject();

        string pdfLocation = UploadPDF();
        if (string.IsNullOrEmpty(pdfLocation))
        {
            pdfLocation = hfPDFUrl.Value;
        }
        else
        {
            if (!PDFUploader.DeletePDF(hfPDFUrl.Value))
            {
                MessageBox.Show("While updating this data, a new master plan proposal doc was uploaded but failed to delete the existing master plan proposal doc.");
            }
        }
        obj_MasterPlanProposal.MasterPlanProposalFilePath = pdfLocation;

        try
        {
            int result = obj.UpdateMasterPlanProposal(obj_MasterPlanProposal, Convert.ToInt32(hfMasterPlanProposalId.Value));

            if (result > 0)
            {
                lblMessage.Text = "Record updated successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                reset();
            }
            else
            {
                bool IsPDFDelete = PDFUploader.DeletePDF(pdfLocation);
                if (!IsPDFDelete)
                {
                    MessageBox.Show("Something went wrong please try again or contact administrator!. While processing this data, new master plan proposal document was uploaded but failed to be new master plan proposal document deleted.");
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
            MessageBox.Show("An error occurred: " + ex.Message);
        }
    }

    private tbl_MasterPlanProposal CreateMasterPlanProposalObject()
    {
        int personId;
        int FY, zone, circle, division;

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

        return new tbl_MasterPlanProposal
        {
            FY = FY,
            Zone = zone,
            Circle = circle,
            Division = division,
            ProposalName = string.IsNullOrEmpty(txtProposalName.Text) ? "" : txtProposalName.Text,
            ProposalDetail = string.IsNullOrEmpty(txtProposalDetail.Text) ? "" : txtProposalDetail.Text,
            MobileNo = string.IsNullOrEmpty(txtMobileNo.Text) ? "" : txtMobileNo.Text,
            ExpAmt = double.Parse(string.IsNullOrEmpty(txtExpAmt.Text) ? "0" : txtExpAmt.Text),
            AddedBy = personId,
            UpdatedBy = personId
        };
    }

    protected void cvFileSize_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (fileUploadMasterPlanProposalDoc.HasFile)
        {
            // Check the file size (5MB = 5 * 1024 * 1024 bytes)
            if (fileUploadMasterPlanProposalDoc.PostedFile.ContentLength > 5 * 1024 * 1024)
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