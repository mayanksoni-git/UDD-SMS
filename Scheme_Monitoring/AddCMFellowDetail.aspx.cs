using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Data;

public partial class AddCMFellowDetail : BasePage
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
            GetTblZone(ddlZone, ddlCircle, ddlDivision);
            SetDropdownsBasedOnUserType(ddlZone, ddlCircle, ddlDivision);
            if (Request.QueryString.Count > 0)
            {
                int id = Convert.ToInt32(Request.QueryString["ID"]);
                hfCMFellowDetailId.Value = id.ToString();
                GetCMFellowDetailById(id);

            }
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }
    protected void GetCMFellowDetailById(int Id)
    {
        DataTable dt = new DataTable();
        dt = obj.getGetCMFellowDetailById(Id);

        if (dt != null && dt.Rows.Count > 0)
        {
            ddlZone.SelectedValue = dt.Rows[0]["Zone_Id"].ToString();
            ddlCircle.SelectedValue = dt.Rows[0]["Circle_Id"].ToString();
            GetTblDivision(Convert.ToInt32(dt.Rows[0]["Circle_Id"].ToString()), ddlDivision);
            ddlDivision.SelectedValue = dt.Rows[0]["Division_Id"].ToString();

            txtCMFellowName.Text = dt.Rows[0]["CMFellowName"].ToString();
            txtEducationalDetail.Text = dt.Rows[0]["EducationalDetail"].ToString();
            txtProfessionalDetail.Text = dt.Rows[0]["ProfessionalDetail"].ToString();
            txtExperience.Text = dt.Rows[0]["Experience"].ToString();
            hfCMFellowDetailId.Value = dt.Rows[0]["SrNo"].ToString();

            hfImageUrl.Value = dt.Rows[0]["CMFellowImagePath"].ToString();
            if (!string.IsNullOrEmpty(hfImageUrl.Value.ToString()))
            {
                hypCMFellowImage.Visible = true;
                hypCMFellowImage.NavigateUrl = dt.Rows[0]["CMFellowImagePath"].ToString();
                imgCMFellow.ImageUrl = dt.Rows[0]["CMFellowImagePath"].ToString();
            }

            btnUpdate.Visible = true;
            btnSave.Visible = false;
        }
        else
        {
            // exportToExcel.Visible = false;

        }
    }

    protected void btnsave_Click2(object sender, EventArgs e)
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
                else if (result == -1)
                {
                    // Division already exists case
                    bool IsPDFDelete = ImageUploader.DeleteImage(imageLocation);
                    if (!IsPDFDelete)
                    {
                        lblMessage.Text = "You can insert only one record for every ULB. Additionally, the uploaded image couldn't be deleted.";
                    }
                    else
                    {
                        lblMessage.Text = "You can insert only one record for every Division.";
                    }
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    bool IsPDFDelete = ImageUploader.DeleteImage(imageLocation);
                    if (!IsPDFDelete)
                    {
                        lblMessage.Text = "Something went wrong please try again or contact administrator!. While processing this data, CM Fellow Image was uploaded but failed to be deleted.";
                    }
                    else
                    {
                        lblMessage.Text = "Something went wrong please try again or contact administrator!";
                    }
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Red;
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
            EducationalDetail=txtEducationalDetail.Text.Trim().ToString(),
            ProfessionalDetail = txtProfessionalDetail.Text.Trim().ToString(),
            Experience = txtExperience.Text.Trim().ToString(),
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
        if (!ValidateFields())
        {
            return;
        }
        tbl_CMFellowDetail obj_CMFellowDetail = CreateCMFellowDetailObject();

        string imageLocation = UploadImage();
        if (string.IsNullOrEmpty(imageLocation))
        {
            imageLocation = hfImageUrl.Value;
        }
        else
        {
            if (!ImageUploader.DeleteImage(hfImageUrl.Value))
            {
                MessageBox.Show("While updating this data, a new CM Fellow image was uploaded but failed to delete the existing image.");
            }
        }
        obj_CMFellowDetail.CMFellowImagePath = imageLocation;

        try
        {
            int result = obj.UpdateCMFellowDetail(obj_CMFellowDetail, Convert.ToInt32(hfCMFellowDetailId.Value));

            if (result > 0)
            {
                lblMessage.Text = "Record updated successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                reset();
            }
            else
            {
                bool IsImageDelete = ImageUploader.DeleteImage(imageLocation);
                if (!IsImageDelete)
                {
                    MessageBox.Show("Something went wrong please try again or contact administrator!. While processing this data, new CM Fellow image was uploaded but failed to be New CM Fellow image deleted.");
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
        if(txtCMFellowName.Text=="")
        {
            MessageBox.Show("Please enter CM Fellow Name. ");
            txtCMFellowName.Focus();
            return false;
        } 
        if(txtEducationalDetail.Text=="")
        {
            MessageBox.Show("Please enter educational detail. ");
            txtEducationalDetail.Focus();
            return false;
        } 
        if(txtProfessionalDetail.Text=="")
        {
            MessageBox.Show("Please enter professional detail. ");
            txtProfessionalDetail.Focus();
            return false;
        } 
        if(txtExperience.Text=="")
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
        
        //get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
        //GetTblCircle(ddlZone);
        GetTblCircle(Convert.ToInt32(ddlZone.SelectedValue), ddlCircle, ddlDivision);
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

        //UpladedDoc.InnerText = "";

        btnSave.Visible = true;
        btnUpdate.Visible = false;

        SetDropdownsBasedOnUserType(ddlZone, ddlCircle, ddlDivision);

        hfCMFellowDetailId.Value = "";
    }

    #region Drop Down Lists

        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            DdlZone_SelectedIndexChanged(ddlZone, ddlCircle, ddlDivision);
        }

        protected void ddlCircle_SelectedIndexChanged(object sender, EventArgs e)
        {
            DdlCircle_SelectedIndexChanged(ddlCircle, ddlDivision);
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDivision.SelectedValue == "0")
            {
                ddlDivision.Focus();
            }
            else
            {
                // Perform additional actions if needed
            }
        }
    #endregion
}