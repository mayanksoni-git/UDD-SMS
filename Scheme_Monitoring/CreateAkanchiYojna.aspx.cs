using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateAkanchiYojna : System.Web.UI.Page
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
            if (Request.QueryString.Count > 0)
            {
                //hfAkanshiYoujnaId.Value = Request.QueryString["AkanshiID"].ToString();
                string encryptedId = Request.QueryString["AkanshiID"];
                hfAkanshiYoujnaId.Value = CryptoHelper.Decrypt(Server.UrlDecode(encryptedId));
                GetGetAkanshiDataById(Convert.ToInt32(hfAkanshiYoujnaId.Value));
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

    protected void GetGetAkanshiDataById(int AkanshiYojnaId)
    {
        DataTable dt = new DataTable();
        dt = objLoan.getGetAkanshiDataById(AkanshiYojnaId);

        if (dt != null && dt.Rows.Count > 0)
        {
            ddlZone.SelectedValue = dt.Rows[0]["Zone_Id"].ToString();
            ddlCircle.SelectedValue = dt.Rows[0]["Circle_Id"].ToString();
            get_tbl_Division(Convert.ToInt32(dt.Rows[0]["Circle_Id"].ToString()));
            ddlDivision.SelectedValue = dt.Rows[0]["Division_Id"].ToString();
            ddlFY.SelectedValue = dt.Rows[0]["FinancialYear_Id"].ToString();

            txtCMFellowName.Text = dt.Rows[0]["CMFellowName"].ToString();

            txtCMAbhyudaySchool.Text = dt.Rows[0]["CMAbhyudaySchool"].ToString();
            txtCMAbhyudaySchoolWP.Text = dt.Rows[0]["CMAbhyudaySchoolWP"].ToString();

            txtAnganwadiConstructionOnRent.Text = dt.Rows[0]["AnganwadiConstructionOnRent"].ToString();
            txtAnganwadiConstructionOnOtherPlace.Text = dt.Rows[0]["AnganwadiConstructionOnOtherPlace"].ToString();
            txtAnganwadiConstructionWP.Text = dt.Rows[0]["AnganwadiConstructionWP"].ToString();

            txtSmartClassFurniture.Text = dt.Rows[0]["SmartClassFurniture"].ToString();
            txtSmartClassFurnitureWP.Text = dt.Rows[0]["SmartClassFurnitureWP"].ToString(); 

            txtAdditionalClassRoom.Text = dt.Rows[0]["AdditionalClassRoom"].ToString();
            txtAdditionalClassRoomWP.Text = dt.Rows[0]["AdditionalClassRoomWP"].ToString();

            //txtTotalCMAbhyudaySchoolCost.Text = dt.Rows[0]["TotalCMAbhyudayCost"].ToString();
            //txtTotalAnganwadiCost.Text = dt.Rows[0]["TotalAnganwadiCost"].ToString();
            //txtTotalSmartClassCost.Text = dt.Rows[0]["TotalSmartClassCost"].ToString();
            //txtAdditionalClassRoomCost.Text = dt.Rows[0]["TotalAdditionalClassRoomCost"].ToString();
            //txtTotalAmount.Text= dt.Rows[0]["CMFellowName"].ToString();
            
            txtTotalAmountTransferred.Text= dt.Rows[0]["TotalAmountTransferred"].ToString();

            hfPDFUrl.Value = dt.Rows[0]["UCofAnganwadiCentrePath"].ToString();
            if (!string.IsNullOrEmpty(hfPDFUrl.Value.ToString()))
            {
                hypUCofAnganwadiCentre.Visible = true;
                hypUCofAnganwadiCentre.NavigateUrl = dt.Rows[0]["UCofAnganwadiCentrePath"].ToString();
            }

            hfPDFUrl2.Value = dt.Rows[0]["UCofAdditionalClassroomPath"].ToString();
            if (!string.IsNullOrEmpty(hfPDFUrl2.Value.ToString()))
            {
                hypUCofAdditionalClassroom.Visible = true;
                hypUCofAdditionalClassroom.NavigateUrl = dt.Rows[0]["UCofAdditionalClassroomPath"].ToString();
            }

            hfPDFUrl3.Value = dt.Rows[0]["UCofSmartClassroomPath"].ToString();
            if (!string.IsNullOrEmpty(hfPDFUrl3.Value.ToString()))
            {
                hypUCofSmartClassroom.Visible = true;
                hypUCofSmartClassroom.NavigateUrl = dt.Rows[0]["UCofSmartClassroomPath"].ToString();
            }

            CalculateTotalAmount(null, EventArgs.Empty);

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
        DataSet ds = (new DataLayer()).get_tbl_AkanshiDivision(circleId);
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
                    txtCMFellowName.Enabled = false;
                    txtCMAbhyudaySchool.Enabled = false;
                    txtAnganwadiConstructionOnRent.Enabled = false;
                    txtAnganwadiConstructionOnOtherPlace.Enabled = false;
                    txtSmartClassFurniture.Enabled = false;
                    txtAdditionalClassRoom.Enabled = false;
                    txtTotalAmountTransferred.Enabled = false;
                    ddlFY.Enabled = false;
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

    protected void CalculateTotalAmount(object sender, EventArgs e)
    {

        int CMAbhyudaySchool = 0;
        decimal CMAbhyudaySchoolCost = 142;
        decimal totalCMAbhyudayCost = 0;

        int AnganwadiConstructionOnRent = 0;
        int AnganwadiConstructionOnOtherPlace = 0;
        decimal AnganwadiCost = 11.84M;
        decimal totalAnganwadiCost = 0;

        int SmartClassFurniture = 0;
        decimal SmartClassCost = 2.505M+ 0.7195M;
        decimal totalSmartClassCost = 0;

        int AdditionalClassRoom = 0;
        decimal AdditionalClassRoomCost = 9.27M;
        decimal totalAdditionalClassRoomCost = 0;
        decimal TotalAmount = 0;

        //decimal totalAmountTransferred = 0;

        CMAbhyudaySchool = string.IsNullOrWhiteSpace(txtCMAbhyudaySchool.Text) ? 0 : Convert.ToInt32(txtCMAbhyudaySchool.Text);
        AnganwadiConstructionOnRent = string.IsNullOrWhiteSpace(txtAnganwadiConstructionOnRent.Text) ? 0 : Convert.ToInt32(txtAnganwadiConstructionOnRent.Text);
        AnganwadiConstructionOnOtherPlace = string.IsNullOrWhiteSpace(txtAnganwadiConstructionOnOtherPlace.Text) ? 0 : Convert.ToInt32(txtAnganwadiConstructionOnOtherPlace.Text);
        SmartClassFurniture = string.IsNullOrWhiteSpace(txtSmartClassFurniture.Text) ? 0 : Convert.ToInt32(txtSmartClassFurniture.Text);
        AdditionalClassRoom = string.IsNullOrWhiteSpace(txtAdditionalClassRoom.Text) ? 0 : Convert.ToInt32(txtAdditionalClassRoom.Text);

        totalCMAbhyudayCost = (CMAbhyudaySchool * CMAbhyudaySchoolCost);
        totalAnganwadiCost = ((AnganwadiConstructionOnRent+ AnganwadiConstructionOnOtherPlace) * AnganwadiCost);
        totalSmartClassCost = (SmartClassFurniture * SmartClassCost);
        totalAdditionalClassRoomCost = (AdditionalClassRoom * AdditionalClassRoomCost);
        TotalAmount = totalCMAbhyudayCost + totalAnganwadiCost + totalSmartClassCost + totalAdditionalClassRoomCost;

        txtTotalCMAbhyudaySchoolCost.Text = totalCMAbhyudayCost.ToString("N2");
        txtTotalAnganwadiCost.Text = totalAnganwadiCost.ToString("N2");
        txtTotalSmartClassCost.Text = totalSmartClassCost.ToString("N2");
        txtAdditionalClassRoomCost.Text = totalAdditionalClassRoomCost.ToString("N2");
        txtTotalAmount.Text= TotalAmount.ToString("N2");
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

    private void ToggleFormMode(bool isUpdateMode)
    {
        // Toggle between save and update modes
        btnSave.Visible = !isUpdateMode;
        btnUpdate.Visible = isUpdateMode;
        btnCancel.Visible = isUpdateMode;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!ValidateFields())
        {
            return;
        }

        tbl_AkanshiYojna obj_AkanshiYojna = CreateAkanshiYojnaObject();

        try
        {
            int result = objLoan.InsertAkanshiYojnaData(obj_AkanshiYojna);

            if (result > 0)
            {
                lblMessage.Text = "Record saved successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                reset();
            }
            else
            {
                MessageBox.Show("Something went wrong. Please try again or contact administration!");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred: " + ex.Message);
        }
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
        if (txtTotalAmount.Text == "" || double.Parse(txtTotalAmount.Text)<=0)
        {
            lblMessage.Text = "You cannot enter record with 0 amount!";
            txtTotalAmount.Focus();
            IsValid = false;
        }
        
        return IsValid;
    }


    private string UploadPDFUCofAnganwadiCentrePath()
    {
        int MaxPdfSize = 10 * 1024 * 1024;
        string PdfDirectory = "~/PDFs/UCAnganwadiCentre/";
        if (fileUCofAnganwadiCentre.HasFile)
        {
            string errorMessage;
            string pdfLocation = PDFUploader.UploadPDFWithSizeAndPath(fileUCofAnganwadiCentre.PostedFile, PdfDirectory, MaxPdfSize, out errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show("Error: " + errorMessage);
                return null;
            }

            return pdfLocation;
        }

        return null;
    }

    private string UploadPDFUCofAdditionalClassroomPath()
    {
        int MaxPdfSize = 10 * 1024 * 1024;
        string PdfDirectory = "~/PDFs/UCAdditionalClassroom/";
        if (fileUCofAdditionalClassroom.HasFile)
        {
            string errorMessage;
            string pdfLocation = PDFUploader.UploadPDFWithSizeAndPath(fileUCofAdditionalClassroom.PostedFile, PdfDirectory, MaxPdfSize, out errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show("Error: " + errorMessage);
                return null;
            }

            return pdfLocation;
        }

        return null;
    }

    private string UploadPDFUCofSmartClassroomPath()
    {
        int MaxPdfSize = 10 * 1024 * 1024;
        string PdfDirectory = "~/PDFs/UCSmartClassroom/";
        if (fileUCofSmartClassroom.HasFile)
        {
            string errorMessage;
            string pdfLocation = PDFUploader.UploadPDFWithSizeAndPath(fileUCofSmartClassroom.PostedFile, PdfDirectory, MaxPdfSize, out errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show("Error: " + errorMessage);
                return null;
            }

            return pdfLocation;
        }

        return null;
    }



    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!ValidateFields())
        {
            return;
        }

        tbl_AkanshiYojna obj_AkanshiYojna = CreateAkanshiYojnaObject();

        string pdfLocation = UploadPDFUCofAnganwadiCentrePath();
        if (string.IsNullOrEmpty(pdfLocation))
        {
            pdfLocation = hfPDFUrl.Value;
        }
        else
        {
            if (!PDFUploader.DeletePDF(hfPDFUrl.Value))
            {
                MessageBox.Show("While updating this data, a new UC of Anganwadi Centre was uploaded but failed to delete the existing UC.");
            }
        }
        obj_AkanshiYojna.UCofAnganwadiCentrePath = pdfLocation; 
        
        pdfLocation = UploadPDFUCofAdditionalClassroomPath();
        if (string.IsNullOrEmpty(pdfLocation))
        {
            pdfLocation = hfPDFUrl2.Value;
        }
        else
        {
            if (!PDFUploader.DeletePDF(hfPDFUrl2.Value))
            {
                MessageBox.Show("While updating this data, a new UC of Additional Classroom was uploaded but failed to delete the existing UC.");
            }
        }
        obj_AkanshiYojna.UCofAdditionalClassroomPath = pdfLocation;   
        
        pdfLocation = UploadPDFUCofSmartClassroomPath();
        if (string.IsNullOrEmpty(pdfLocation))
        {
            pdfLocation = hfPDFUrl3.Value;
        }
        else
        {
            if (!PDFUploader.DeletePDF(hfPDFUrl3.Value))
            {
                MessageBox.Show("While updating this data, a new UC of Smart Classroom was uploaded but failed to delete the existing UC.");
            }
        }
        obj_AkanshiYojna.UCofSmartClassroomPath = pdfLocation;

        try
        {
            int result = objLoan.UpdateAkanshiYojnaData(obj_AkanshiYojna, Convert.ToInt32(hfAkanshiYoujnaId.Value));

            if (result > 0)
            {
                lblMessage.Text = "Record updated successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                reset();
            }
            else
            {
                MessageBox.Show("Something went wrong. Please try again or contact administration!");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred: " + ex.Message);
        }
    }

    private tbl_AkanshiYojna CreateAkanshiYojnaObject()
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


        return new tbl_AkanshiYojna
        {
            FY = FY,
            Zone = zone,
            Circle = circle,
            Division = division,

            CMFellowName = txtCMFellowName.Text.ToString(),

            CMAbhyudaySchool = Int32.Parse(string.IsNullOrEmpty(txtCMAbhyudaySchool.Text) ? "0" : txtCMAbhyudaySchool.Text),
            CMAbhyudaySchoolWP = Int32.Parse(string.IsNullOrEmpty(txtCMAbhyudaySchoolWP.Text) ? "0" : txtCMAbhyudaySchoolWP.Text),
            TotalCMAbhyudaySchoolCost = double.Parse(string.IsNullOrEmpty(txtTotalCMAbhyudaySchoolCost.Text) ? "0" : txtTotalCMAbhyudaySchoolCost.Text),

            AnganwadiConstructionOnRent = Int32.Parse(string.IsNullOrEmpty(txtAnganwadiConstructionOnRent.Text) ? "0" : txtAnganwadiConstructionOnRent.Text),
            AnganwadiConstructionOnOtherPlace = Int32.Parse(string.IsNullOrEmpty(txtAnganwadiConstructionOnOtherPlace.Text) ? "0" : txtAnganwadiConstructionOnOtherPlace.Text),
            AnganwadiConstructionWP = Int32.Parse(string.IsNullOrEmpty(txtAnganwadiConstructionWP.Text) ? "0" : txtAnganwadiConstructionWP.Text),
            TotalAnganwadiCost = double.Parse(string.IsNullOrEmpty(txtTotalAnganwadiCost.Text) ? "0" : txtTotalAnganwadiCost.Text),

            SmartClassFurniture = Int32.Parse(string.IsNullOrEmpty(txtSmartClassFurniture.Text) ? "0" : txtSmartClassFurniture.Text),
            SmartClassFurnitureWP = Int32.Parse(string.IsNullOrEmpty(txtSmartClassFurnitureWP.Text) ? "0" : txtSmartClassFurnitureWP.Text),
            TotalSmartClassCost = double.Parse(string.IsNullOrEmpty(txtTotalSmartClassCost.Text) ? "0" : txtTotalSmartClassCost.Text),

            AdditionalClassRoom = Int32.Parse(string.IsNullOrEmpty(txtAdditionalClassRoom.Text) ? "0" : txtAdditionalClassRoom.Text),
            AdditionalClassRoomWP = Int32.Parse(string.IsNullOrEmpty(txtAdditionalClassRoomWP.Text) ? "0" : txtAdditionalClassRoomWP.Text),
            AdditionalClassRoomCost = double.Parse(string.IsNullOrEmpty(txtAdditionalClassRoomCost.Text) ? "0" : txtAdditionalClassRoomCost.Text),

            TotalAmount = double.Parse(string.IsNullOrEmpty(txtTotalAmount.Text) ? "0" : txtTotalAmount.Text),
            TotalAmountTransferred = double.Parse(string.IsNullOrEmpty(txtTotalAmountTransferred.Text) ? "0" : txtTotalAmountTransferred.Text),
            AddedBy = personId,
            UpdatedBy = personId
        };
    }

    protected void cvFileSize_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (fileUCofAnganwadiCentre.HasFile)
        {
            // Check the file size (5MB = 5 * 1024 * 1024 bytes)
            if (fileUCofAnganwadiCentre.PostedFile.ContentLength > 5 * 1024 * 1024)
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
    
    protected void cvFileSize_ServerValidate2(object source, ServerValidateEventArgs args)
    {
        if (fileUCofAdditionalClassroom.HasFile)
        {
            // Check the file size (5MB = 5 * 1024 * 1024 bytes)
            if (fileUCofAdditionalClassroom.PostedFile.ContentLength > 5 * 1024 * 1024)
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


    protected void cvFileSize_ServerValidate3(object source, ServerValidateEventArgs args)
    {
        if (fileUCofSmartClassroom.HasFile)
        {
            // Check the file size (5MB = 5 * 1024 * 1024 bytes)
            if (fileUCofSmartClassroom.PostedFile.ContentLength > 5 * 1024 * 1024)
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