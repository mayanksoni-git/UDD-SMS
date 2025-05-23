using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterProjectWorkMIS_Tender : System.Web.UI.Page
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
                int ProjectWork_Id = Convert.ToInt32(Request.QueryString["ProjectWork_Id"].Trim());
                hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
                hf_Scheme_Id.Value = Request.QueryString["Id"].ToString().Trim();
                BindTenderDetails(ProjectWork_Id);
            }
        }
    }

    private void BindTenderDetails(int ProjectWork_Id)
    {
        DataSet ds = new DataLayer().Get_tbl_ProjectTender(ProjectWork_Id);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdTenderDetails.DataSource = ds.Tables[0];
            grdTenderDetails.DataBind();
        }
        else
        {
            // Add empty row if no data exists
            DataTable dt = CreateTenderDataTable();
            grdTenderDetails.DataSource = dt;
            grdTenderDetails.DataBind();
        }
    }

    private DataTable CreateTenderDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ProjectTender_Id", typeof(int));
        dt.Columns.Add("ProjectTender_NITDate", typeof(string));
        dt.Columns.Add("ProjectTender_IssueDate", typeof(string));
        dt.Columns.Add("ProjectTender_EndDate", typeof(string));
        dt.Columns.Add("ProjectTender_EMD", typeof(decimal));
        dt.Columns.Add("ProjectTender_Remarks", typeof(string));
        dt.Columns.Add("ProjectTender_Status", typeof(string));
        dt.Columns.Add("ProjectTender_FailureReason", typeof(string));
        dt.Columns.Add("ProjectTender_FileName", typeof(string));
        dt.Columns.Add("ProjectTender_FilePath", typeof(string));

        // Add empty row
        dt.Rows.Add(dt.NewRow());
        return dt;
    }

    protected void grdTenderDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;

            // Set Tender Status dropdown
            DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlTenderStatus");
            if (rowView["ProjectTender_Status"] != DBNull.Value)
            {
                ddlStatus.SelectedValue = rowView["ProjectTender_Status"].ToString();
            }

            // Set Failure Reason dropdown
            DropDownList ddlReason = (DropDownList)e.Row.FindControl("ddlFailureReason");
            if (rowView["ProjectTender_FailureReason"] != DBNull.Value)
            {
                ddlReason.SelectedValue = rowView["ProjectTender_FailureReason"].ToString();
            }
        }
    }

    protected void grdTenderDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddNew")
        {
            AddNewTenderRow();
        }
        else if (e.CommandName == "DeleteTender")
        {
            int tenderId = Convert.ToInt32(e.CommandArgument);
            DeleteTender(tenderId);
        }
        else if (e.CommandName == "EditTender")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            hf_SelectedTenderId.Value = grdTenderDetails.DataKeys[rowIndex]["ProjectTender_Id"].ToString();
            mp1.Show();
        }
    }

    private void AddNewTenderRow()
    {
        DataTable dt;
        if (ViewState["CurrentTenders"] != null)
        {
            dt = (DataTable)ViewState["CurrentTenders"];
        }
        else
        {
            dt = CreateTenderDataTable();
        }

        DataRow newRow = dt.NewRow();
        dt.Rows.Add(newRow);

        ViewState["CurrentTenders"] = dt;
        grdTenderDetails.DataSource = dt;
        grdTenderDetails.DataBind();
    }

    private void DeleteTender(int tenderId)
    {
        if (tenderId > 0)
        {
            bool success = new DataLayer().Delete_tbl_ProjectTender(tenderId, Convert.ToInt32(Session["Person_Id"]));
            if (success)
            {
                ShowMessage("Tender deleted successfully", true);
                BindTenderDetails(Convert.ToInt32(hf_ProjectWork_Id.Value));
            }
            else
            {
                ShowMessage("Error deleting tender", false);
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            List<tbl_ProjectTender> tenderList = new List<tbl_ProjectTender>();
            string uploadFolder = "~/Uploads/TenderDocuments/";
            string physicalPath = Server.MapPath(uploadFolder);

            // Create directory if it doesn't exist
            if (!Directory.Exists(physicalPath))
            {
                Directory.CreateDirectory(physicalPath);
            }

            foreach (GridViewRow row in grdTenderDetails.Rows)
            {
                tbl_ProjectTender tender = new tbl_ProjectTender();

                // Get controls from the row
                TextBox txtNITDate = (TextBox)row.FindControl("txtNITDate");
                TextBox txtTenderIssueDate = (TextBox)row.FindControl("txtTenderIssueDate");
                TextBox txtTenderEndDate = (TextBox)row.FindControl("txtTenderEndDate");
                TextBox txtEMD = (TextBox)row.FindControl("txtEMD");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                DropDownList ddlTenderStatus = (DropDownList)row.FindControl("ddlTenderStatus");
                DropDownList ddlFailureReason = (DropDownList)row.FindControl("ddlFailureReason");
                FileUpload fuTenderFile = (FileUpload)row.FindControl("fuTenderFile");
                HiddenField hfFilePath = (HiddenField)row.FindControl("hfFilePath");
                HiddenField hfFileName = (HiddenField)row.FindControl("hfFileName");

                // Set tender properties
                tender.ProjectTender_NITDate = txtNITDate.Text.Trim();
                tender.ProjectTender_IssueDate = txtTenderIssueDate.Text.Trim();
                tender.ProjectTender_EndDate = txtTenderEndDate.Text.Trim();
                tender.ProjectTender_Remarks = txtRemarks.Text.Trim();
                tender.ProjectTender_Status = ddlTenderStatus.SelectedValue;
                tender.ProjectTender_FailureReason = ddlFailureReason.SelectedValue;
                tender.ProjectTender_AddedBy = Convert.ToInt32(Session["Person_Id"]);
                tender.ProjectTender_ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);

                // Handle EMD value
                decimal emdValue;
                if (decimal.TryParse(txtEMD.Text.Trim(), out emdValue))
                {
                    tender.ProjectTender_EMD = emdValue;
                }

                // Handle file upload
                if (fuTenderFile.HasFile)
                {
                    string fileName = Path.GetFileName(fuTenderFile.FileName);
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                    string filePath = Path.Combine(physicalPath, uniqueFileName);

                    fuTenderFile.SaveAs(filePath);

                    tender.ProjectTender_FileName = fileName;
                    tender.ProjectTender_FilePath = uploadFolder + uniqueFileName;
                }
                else if (!string.IsNullOrEmpty(hfFilePath.Value))
                {
                    // Keep existing file if not uploading new one
                    tender.ProjectTender_FileName = hfFileName.Value;
                    tender.ProjectTender_FilePath = hfFilePath.Value;
                }

                tenderList.Add(tender);
            }

            bool result = new DataLayer().Update_tbl_ProjectTender(tenderList, null, Convert.ToInt32(hf_ProjectWork_Id.Value));

            if (result)
            {
                ShowMessage("Tender details saved successfully!", true);
                BindTenderDetails(Convert.ToInt32(hf_ProjectWork_Id.Value));
            }
            else
            {
                ShowMessage("Error saving tender details.", false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Error: " + ex.Message, false);
        }
    }

    protected void btnUpdateAction_Click(object sender, EventArgs e)
    {
        // Implementation for updating tender action
        mp1.Hide();
    }

    private void ShowMessage(string message, bool isSuccess)
    {
        string script = "alert('" + message + "');";
        if (isSuccess)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", script, true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
        }
    }
}