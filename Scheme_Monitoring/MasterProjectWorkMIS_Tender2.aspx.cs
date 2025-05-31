using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterProjectWorkMIS_Tender2 : System.Web.UI.Page
{
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
                int projectWorkId = Convert.ToInt32(Request.QueryString["ProjectWork_Id"].Trim());
                int schemeId = Convert.ToInt32(Request.QueryString["Id"].Trim());

                hfProjectWorkId.Value = projectWorkId.ToString();
                hfSchemeId.Value = schemeId.ToString();

                LoadProjectDetails(projectWorkId);
                LoadTenderDetails(projectWorkId);
                ResetForm();
            }
            ddlTenderStatus_SelectedIndexChanged(null, null);
        }
    }

    private void LoadProjectDetails(int projectWorkId)
    {
        DataSet ds = new DataLayer().GetProjectDetails(projectWorkId);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];

            lblProjectName.Text = dr["ProjectName"].ToString();
            lblProjectType.Text = dr["ProjectType"].ToString();
            lblSchemeName.Text = dr["SchemeName"].ToString();
            lblULBName.Text = dr["ULBName"].ToString();
            lblDistrictName.Text = dr["DistrictName"].ToString();
            lblSanctionedCost.Text = string.Format("{0:N2}", dr["SanctionedCost"]);
        }
    }

    private void LoadTenderDetails(int projectWorkId)
    {
        DataSet ds = new DataLayer().GetTenderDetails(projectWorkId);

        if (ds != null && ds.Tables.Count > 0)
        {
            gvTenders.DataSource = ds.Tables[0];
            gvTenders.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!ValidateForm())
        {
            return;
        }

        // Check if there's already an active tender
        if (ddlTenderStatus.SelectedValue == "A" && new DataLayer().HasActiveTender(Convert.ToInt32(hfProjectWorkId.Value)))
        {
            MessageBox.Show("There is already an active tender for this project. Please update the previous tender to 'Failed' or 'Completed' before adding a new active tender.");
            return;
        }

        string filePath = UploadTenderFile();
        if (filePath == "error")
        {
            return;
        }

        tbl_ProjectTender tender = new tbl_ProjectTender
        {
            ProjectTender_Id = 0, // New record
            ProjectTender_ProjectWork_Id = Convert.ToInt32(hfProjectWorkId.Value),
            ProjectTender_NITDate = txtNITDate.Text.Trim(),
            ProjectTender_IssueDate = txtTenderIssueDate.Text.Trim(),
            ProjectTender_EndDate = txtTenderEndDate.Text.Trim(),
            ProjectTender_EMD = Convert.ToDecimal(txtEMD.Text.Trim()),
            ProjectTender_Status = ddlTenderStatus.SelectedValue,
            ProjectTender_FailureReason = ddlTenderStatus.SelectedValue == "F" ? ddlFailureReason.SelectedValue : null,
            ProjectTender_FailureDetail = ddlTenderStatus.SelectedValue == "F" ? txtFailureDetail.Text.Trim() : null,
            ProjectTender_Remarks = txtTenderRemark.Text.Trim(),
            ProjectTender_FilePath = filePath,
            ProjectTender_AddedBy = Convert.ToInt32(Session["Person_Id"])
        };

        bool result = new DataLayer().SaveTenderDetails(tender);

        if (result)
        {
            ShowMessage("Tender details saved successfully!", true);
            LoadTenderDetails(Convert.ToInt32(hfProjectWorkId.Value));
            ResetForm();
        }
        else
        {
            ShowMessage("Error saving tender details. Please try again.", false);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!ValidateForm())
        {
            return;
        }

        string filePath = hfTenderId.Value == "0" ? UploadTenderFile() : null;
        if (filePath == "error" && hfTenderId.Value == "0")
        {
            return;
        }

        tbl_ProjectTender tender = new tbl_ProjectTender
        {
            ProjectTender_Id = Convert.ToInt32(hfTenderId.Value),
            ProjectTender_ProjectWork_Id = Convert.ToInt32(hfProjectWorkId.Value),
            ProjectTender_NITDate = txtNITDate.Text.Trim(),
            ProjectTender_IssueDate = txtTenderIssueDate.Text.Trim(),
            ProjectTender_EndDate = txtTenderEndDate.Text.Trim(),
            ProjectTender_EMD = Convert.ToDecimal(txtEMD.Text.Trim()),
            ProjectTender_Status = ddlTenderStatus.SelectedValue,
            ProjectTender_FailureReason = ddlTenderStatus.SelectedValue == "F" ? ddlFailureReason.SelectedValue : null,
            ProjectTender_FailureDetail = ddlTenderStatus.SelectedValue == "F" ? txtFailureDetail.Text.Trim() : null,
            ProjectTender_Remarks = txtTenderRemark.Text.Trim(),
            ProjectTender_FilePath = filePath,
            ProjectTender_AddedBy = Convert.ToInt32(Session["Person_Id"])
        };

        bool result = new DataLayer().UpdateTenderDetails(tender);

        if (result)
        {
            ShowMessage("Tender details updated successfully!", true);
            LoadTenderDetails(Convert.ToInt32(hfProjectWorkId.Value));
            ResetForm();
        }
        else
        {
            ShowMessage("Error updating tender details. Please try again.", false);
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetForm();
    }

    protected void gvTenders_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditTender")
        {
            int tenderId = Convert.ToInt32(e.CommandArgument);
            hfTenderId.Value = tenderId.ToString();

            DataSet ds = new DataLayer().GetTenderById(tenderId);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                txtNITDate.Text = Convert.ToDateTime(dr["ProjectTender_NITDate"]).ToString("dd/MM/yyyy");
                txtTenderIssueDate.Text = Convert.ToDateTime(dr["ProjectTender_IssueDate"]).ToString("dd/MM/yyyy");
                txtTenderEndDate.Text = Convert.ToDateTime(dr["ProjectTender_EndDate"]).ToString("dd/MM/yyyy");
                txtEMD.Text = dr["ProjectTender_EMD"].ToString();
                ddlTenderStatus.SelectedValue = dr["ProjectTender_Status"].ToString();

                if (ddlTenderStatus.SelectedValue == "F")
                {
                    ddlFailureReason.SelectedValue = dr["ProjectTender_FailureReason"].ToString();
                    txtFailureDetail.Text = dr["ProjectTender_FailureDetail"].ToString();
                }

                txtTenderRemark.Text = dr["ProjectTender_Remarks"].ToString();

                // Show existing file
                if (!string.IsNullOrEmpty(dr["ProjectTender_FilePath"].ToString()))
                {
                    divExistingFile.Visible = true;
                    lnkExistingFile.NavigateUrl = dr["ProjectTender_FilePath"].ToString();
                    lnkExistingFile.Text = "View Existing Document (" + Path.GetFileName(dr["ProjectTender_FilePath"].ToString()) + ")";
                }

                btnSave.Visible = false;
                btnUpdate.Visible = true;
            }
        }
    }

    protected void ddlTenderStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTenderStatus.SelectedValue == "F")
        {
            divFailureReason.Visible = true;
            divFailureDetail.Visible = true;
        }
        else
        {
            divFailureReason.Visible = false;
            divFailureDetail.Visible = false;
        }
    }


    private string UploadTenderFile()
    {
        if (!fuTenderFile.HasFile)
        {
            ShowMessage("Please upload tender document.", false);
            return "error";
        }

        string fileName = fuTenderFile.FileName;
        string extension = Path.GetExtension(fileName).ToLower();

        if (extension != ".pdf")
        {
            lblFileError.Text = "Only PDF files are allowed.";
            lblFileError.Visible = true;
            return "error";
        }

        string uploadFolder = "~/Uploads/TenderDocuments/";
        string physicalPath = Server.MapPath(uploadFolder);

        if (!Directory.Exists(physicalPath))
        {
            Directory.CreateDirectory(physicalPath);
        }

        string newFileName = Guid.NewGuid().ToString() + extension;
        string filePath = Path.Combine(physicalPath, newFileName);

        try
        {
            fuTenderFile.SaveAs(filePath);
            return uploadFolder + newFileName;
        }
        catch (Exception ex)
        {
            ShowMessage("Error uploading file: " + ex.Message, false);
            return "error";
        }
    }

    private bool ValidateForm()
    {
        if (string.IsNullOrEmpty(txtNITDate.Text))
        {
            ShowMessage("Please enter NIT Date.", false);
            return false;
        }

        if (string.IsNullOrEmpty(txtTenderIssueDate.Text))
        {
            ShowMessage("Please enter Tender Issue Date.", false);
            return false;
        }

        if (string.IsNullOrEmpty(txtTenderEndDate.Text))
        {
            ShowMessage("Please enter Tender End Date.", false);
            return false;
        }

        if (string.IsNullOrEmpty(txtEMD.Text))
        {
            ShowMessage("Please enter EMD amount.", false);
            return false;
        }

        if (ddlTenderStatus.SelectedValue == "F" && string.IsNullOrEmpty(ddlFailureReason.SelectedValue))
        {
            ShowMessage("Please select Failure Reason.", false);
            return false;
        }

        if (ddlTenderStatus.SelectedValue == "F" && string.IsNullOrEmpty(txtFailureDetail.Text))
        {
            ShowMessage("Please enter Failure Detail.", false);
            return false;
        }

        if (hfTenderId.Value == "0" && !fuTenderFile.HasFile)
        {
            ShowMessage("Please upload tender document.", false);
            return false;
        }

        return true;
    }

    private void ResetForm()
    {
        txtNITDate.Text = "";
        txtTenderIssueDate.Text = "";
        txtTenderEndDate.Text = "";
        txtEMD.Text = "";
        ddlTenderStatus.SelectedIndex = 0;
        ddlFailureReason.SelectedIndex = 0;
        txtFailureDetail.Text = "";
        txtTenderRemark.Text = "";
        fuTenderFile.Dispose();
        lblFileError.Visible = false;
        divExistingFile.Visible = false;

        btnSave.Visible = true;
        btnUpdate.Visible = false;
        hfTenderId.Value = "0";
    }

    private void ShowMessage(string message, bool isSuccess)
    {
        string script = string.Format("alert('{0}');", message);
        if (isSuccess)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", script, true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
        }
    }

    protected string GetStatusText(string status)
    {
        switch (status)
        {
            case "A": return "Active";
            case "F": return "Failed";
            case "C": return "Completed";
            default: return status;
        }
    }
}