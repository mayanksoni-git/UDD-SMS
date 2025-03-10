using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Data;

public partial class AddCMFellowDetail : System.Web.UI.Page
{
    ULBFund objLoan = new ULBFund();
    AkanshiYojna obj = new AkanshiYojna();
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
            SetDropdownsBasedOnUserType();
            if (Request.QueryString.Count > 0)
            {
                var id = Convert.ToInt32(Request.QueryString["id"]);
            }
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateFields())
            {
                return;
            }
            string imageLocation = UploadImage();
            if (string.IsNullOrEmpty(imageLocation))
            {
                imageLocation = "";
            }
            tbl_CMFellowDetail objCMFellowDetail = CreateCMFellowDetailObject();
            objCMFellowDetail.CMFellowImagePath = imageLocation;
            try
            {
                int result = obj.InsertCMFellowDetail(objCMFellowDetail);

                if (result > 0)
                {
                    lblMessage.Text = "Record saved successfully!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    reset();
                }
                else
                {
                    bool IsPDFDelete = ImageUploader.DeleteImage(imageLocation);
                    if (!IsPDFDelete)
                    {
                        MessageBox.Show("Something went wrong please try again or contact administrator!. While processing this data, CM Fellow Image was uploaded but failed to be deleted.");
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
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private tbl_CMFellowDetail CreateCMFellowDetailObject()
    {
        int personId;
        int division;

        if (!int.TryParse(Session["Person_Id"].ToString(), out personId))
        {
            throw new FormatException("Invalid Person_Id in session.");
        }
        if (!int.TryParse(ddlDivision.SelectedValue, out division))
        {
            throw new FormatException("Invalid Division selected value.");
        }

        return new tbl_CMFellowDetail
        {
            Division = division,
            CMFellowName=txtCMFellowName.Text.ToString(),
            EducationalDetail=txtEducationalDetail.Text.ToString(),
            ProfessionalDetail = txtProfessionalDetail.Text.ToString(),
            Experience = txtExperience.Text.ToString(),
            AddedBy = personId,
            UpdatedBy = personId
        };
    }

    private string UploadImage()
    {
        int MaxImageSize = 10 * 1024 * 1024;
        string ImageDirectory = "~/Images/CMFellowProfile/";
        if (fileupload.HasFile)
        {
            string errorMessage;
            string pdfLocation = ImageUploader.UploadImageWithSizeAndPath(fileupload.PostedFile, ImageDirectory, MaxImageSize, out errorMessage);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show("Error: " + errorMessage);
                return null;
            }

            return pdfLocation;
        }

        return null;
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {


            var doc = "";
            var pathProfile = "";
            if (fileupload.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(fileupload.FileName).ToLower();
                if (fileExtension != ".pdf")
                {
                    MessageBox.Show("Only PDF files are allowed.");
                    return;
                }
                int fileSize = fileupload.PostedFile.ContentLength;

                // path = FileUpload1.FileName;
                //FileUpload1.SaveAs(Server.MapPath(("Images"+path)));
                string sFilename = Path.GetFileName(fileupload.PostedFile.FileName);
                int fileappent = 0;
                while (File.Exists(Server.MapPath("/PDFs/VisionPlanPDF/" + sFilename)))
                {
                    fileappent++;
                    sFilename = Path.GetFileNameWithoutExtension(fileupload.PostedFile.FileName)
                    + fileappent.ToString() + Path.GetExtension(fileupload.PostedFile.FileName).ToLower();
                }
                doc = Server.MapPath("/PDFs/VisionPlanPDF/" + sFilename);

                //string pathProfiles = Path.Combine(pathProfileRoot, sFilename);nn

                fileupload.SaveAs(doc);

                pathProfile = "/PDFs/VisionPlanPDF/" + sFilename;
            }
            else
            {
                MessageBox.Show("Please choose the Vision Plan document to upload. It should be in PDF format.");
                return;
            }

            int zone = Convert.ToInt32(ddlZone.SelectedValue);
            int circle = Convert.ToInt32(ddlCircle.SelectedValue);
            int division = Convert.ToInt32(ddlDivision.SelectedValue);

            


            var Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
            var taskid = Convert.ToInt32(hdnplanId.Value);

            //var sfc = Convert.ToDecimal();
            Button clickedButton = sender as Button;
            string text = clickedButton.Text;
            DataTable dt = new DataTable();
            dt = objLoan.GetDocOfAnnualActionPlan("Update", division, taskid, zone, circle, 0, Person_Id, pathProfile, "VisionPlan", "-1", 0, "1900-01-01", "9999-12-31",-1);

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlCircle.Enabled = true;
                ddlZone.Enabled = true;
                ddlDivision.Enabled = true;
                
                UpladedDoc.InnerText = "";
                if (dt.Rows[0]["remark"].ToString() != "Record already exists for this ULBID and FYID")
                {
                    reset();
                }
                MessageBox.Show(dt.Rows[0]["remark"].ToString());
            }
           
            //GetULBFundAction
            reset();
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
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
        if(txtCMFellowName.Text!="")
        {
            MessageBox.Show("Please enter CM Fellow Name. ");
            txtCMFellowName.Focus();
            return false;
        } 
        if(txtEducationalDetail.Text!="")
        {
            MessageBox.Show("Please enter educational detail. ");
            txtEducationalDetail.Focus();
            return false;
        } 
        if(txtProfessionalDetail.Text!="")
        {
            MessageBox.Show("Please enter professional detail. ");
            txtProfessionalDetail.Focus();
            return false;
        } 
        if(txtExperience.Text!="")
        {
            MessageBox.Show("Please enter your experience with akanshi nagar yojna. ");
            txtExperience.Focus();
            return false;
        }
        if(!fileupload.HasFile)
        {
            MessageBox.Show("Please choose a photo of CM Fellow. ");
            fileupload.Focus();
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
        get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
        ddlCircle_SelectedIndexChanged(ddlCircle, new EventArgs());
        try
        {
            ddlDivision.SelectedValue = "0";
        }
        catch
        {
            ddlDivision.SelectedValue = "0";
        }
        ddlZone.Enabled = true;
        ddlCircle.Enabled = true;
        ddlDivision.Enabled = true;

        UpladedDoc.InnerText = "";

        btnSave.Visible = true;
        BtnUpdate.Visible = false;

        SetDropdownsBasedOnUserType();

        hdnplanId.Value = "";
    }

    #region Drop Down Lists
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

                ddlDivision.Focus();
            }
            else
            {
                // GetAllData(Convert.ToInt32(ddlDivision.SelectedValue));
                //BindLoanReleaseGridByULB();

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
                AllClasses.FillDropDown2(ds.Tables[0], ddl, textField, valueField);
            }
            else
            {
                ddl.Items.Clear();
            }
        }
    #endregion
}