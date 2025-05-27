using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
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
                GetTenderDetails(ProjectWork_Id);
            }
        }
    }

    private void GetTenderDetails(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).Get_TenderDetails(ProjectWork_Id);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdTenderDetails.DataSource = ds.Tables[0];
            grdTenderDetails.DataBind();
            ViewState["dtTender"] = ds.Tables[0];
        }
        else
        {
            DataTable dt = CreateEmptyTenderDataTable();
            ViewState["dtTender"] = dt;
            grdTenderDetails.DataSource = dt;
            grdTenderDetails.DataBind();
        }
    }

    private DataTable CreateEmptyTenderDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Tender_Id", typeof(int));
        dt.Columns.Add("NIT_Date", typeof(DateTime));
        dt.Columns.Add("Tender_IssueDate", typeof(DateTime));
        dt.Columns.Add("Tender_EndDate", typeof(DateTime));
        dt.Columns.Add("EMD_Amount", typeof(decimal));
        dt.Columns.Add("Tender_Status", typeof(string));
        dt.Columns.Add("Failure_Reason", typeof(string));
        dt.Columns.Add("Remarks", typeof(string));
        dt.Columns.Add("Tender_DocumentPath", typeof(string));

        // Add one empty row
        DataRow dr = dt.NewRow();
        dr["Tender_Id"] = 0;
        dt.Rows.Add(dr);

        return dt;
    }

    protected void btnAddNewTender_Click(object sender, EventArgs e)
    {
        DataTable dtTender;
        if (ViewState["dtTender"] != null)
        {
            dtTender = (DataTable)ViewState["dtTender"];
        }
        else
        {
            dtTender = CreateEmptyTenderDataTable();
        }

        // Add a new empty row
        DataRow dr = dtTender.NewRow();
        dr["Tender_Id"] = 0;
        dtTender.Rows.Add(dr);

        ViewState["dtTender"] = dtTender;
        grdTenderDetails.DataSource = dtTender;
        grdTenderDetails.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        List<TenderDetail> tenderDetails = new List<TenderDetail>();
        bool isValid = true;
        string validationMessage = "";

        // First check if we have any rows in the grid
        if (grdTenderDetails.Rows.Count == 0)
        {
            MessageBox.Show("No tender details to save.");
            return;
        }

        foreach (GridViewRow row in grdTenderDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                TenderDetail tender = new TenderDetail();

                // Safely get Tender_Id - handle both new and existing records
                if (grdTenderDetails.DataKeys.Count > 0 && row.RowIndex < grdTenderDetails.DataKeys.Count)
                {
                    tender.Tender_Id = Convert.ToInt32(grdTenderDetails.DataKeys[row.RowIndex].Value);
                }
                else
                {
                    // For new rows that haven't been saved yet
                    tender.Tender_Id = 0;
                }

                // Get controls from the row
                TextBox txtNITDate = row.FindControl("txtNITDate") as TextBox;
                TextBox txtTenderIssueDate = row.FindControl("txtTenderIssueDate") as TextBox;
                TextBox txtTenderEndDate = row.FindControl("txtTenderEndDate") as TextBox;
                TextBox txtEMD = row.FindControl("txtEMD") as TextBox;
                DropDownList ddlTenderStatus = row.FindControl("ddlTenderStatus") as DropDownList;
                DropDownList ddlFailureReason = row.FindControl("ddlFailureReason") as DropDownList;
                TextBox txtRemarks = row.FindControl("txtRemarks") as TextBox;
                FileUpload fuTenderDocument = row.FindControl("fuTenderDocument") as FileUpload;

                tender.ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);

                // Parse dates with validation
                if (!string.IsNullOrEmpty(txtNITDate.Text))
                {
                    DateTime tempDate;
                    if (DateTime.TryParse(txtNITDate.Text, out tempDate))
                        tender.NIT_Date = tempDate;
                }

                // Validate required fields
                if (string.IsNullOrEmpty(txtTenderIssueDate.Text))
                {
                    isValid = false;
                    validationMessage = "Please enter Tender Issue Date for all tenders";
                    break;
                }
                else
                {
                    DateTime tempDate;
                    if (DateTime.TryParse(txtTenderIssueDate.Text, out tempDate))
                        tender.Tender_IssueDate = tempDate;
                    else
                    {
                        isValid = false;
                        validationMessage = "Please enter valid Tender Issue Date";
                        break;
                    }
                }

                if (string.IsNullOrEmpty(txtTenderEndDate.Text))
                {
                    isValid = false;
                    validationMessage = "Please enter Tender End Date for all tenders";
                    break;
                }
                else
                {
                    DateTime tempDate;
                    if (DateTime.TryParse(txtTenderEndDate.Text, out tempDate))
                        tender.Tender_EndDate = tempDate;
                    else
                    {
                        isValid = false;
                        validationMessage = "Please enter valid Tender End Date";
                        break;
                    }
                }

                // Validate EMD amount
                if (string.IsNullOrEmpty(txtEMD.Text))
                {
                    isValid = false;
                    validationMessage = "Please enter EMD Amount for all tenders";
                    break;
                }
                else
                {
                    decimal emdAmount;
                    if (decimal.TryParse(txtEMD.Text, out emdAmount))
                        tender.EMD_Amount = emdAmount;
                    else
                    {
                        isValid = false;
                        validationMessage = "Please enter valid EMD Amount";
                        break;
                    }
                }

                tender.Tender_Status = ddlTenderStatus.SelectedValue;
                tender.Failure_Reason = ddlFailureReason.SelectedValue;
                tender.Remarks = txtRemarks.Text;
                tender.Added_By = Convert.ToInt32(Session["Person_Id"].ToString());

                // Handle file upload - only for new records or when changing file
                if (fuTenderDocument.HasFile)
                {
                    string fileName = Path.GetFileName(fuTenderDocument.FileName);
                    string fileExtension = Path.GetExtension(fileName).ToLower();

                    if (fileExtension == ".pdf" || fileExtension == ".doc" || fileExtension == ".docx" || fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        string uploadFolder = "~/Uploads/TenderDocuments/";
                        string filePath = Path.Combine(Server.MapPath(uploadFolder), fileName);

                        // Ensure directory exists
                        if (!Directory.Exists(Server.MapPath(uploadFolder)))
                        {
                            Directory.CreateDirectory(Server.MapPath(uploadFolder));
                        }

                        // Save file to server
                        fuTenderDocument.SaveAs(filePath);
                        tender.Tender_DocumentPath = uploadFolder + fileName;
                    }
                    else
                    {
                        isValid = false;
                        validationMessage = "Only PDF, Word and Excel files are allowed";
                        break;
                    }
                }
                else if (tender.Tender_Id == 0)
                {
                    // For new records, file is mandatory
                    isValid = false;
                    validationMessage = "Please upload Tender Document for new records";
                    break;
                }
                else
                {
                    // Keep existing file path if no new file is uploaded
                    HiddenField hfDocumentPath = row.FindControl("hfDocumentPath") as HiddenField;
                    if (hfDocumentPath != null)
                    {
                        tender.Tender_DocumentPath = hfDocumentPath.Value;
                    }
                }

                tenderDetails.Add(tender);
            }
        }

        if (!isValid)
        {
            MessageBox.Show(validationMessage);
            return;
        }

        // Save to database
        bool result = (new DataLayer()).SaveTenderDetails(tenderDetails);

        if (result)
        {
            MessageBox.Show("Tender details saved successfully!");
            GetTenderDetails(Convert.ToInt32(hf_ProjectWork_Id.Value));
        }
        else
        {
            MessageBox.Show("Error saving tender details. Please try again.");
        }
    }

    protected void btnDeleteTender_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int tenderId = Convert.ToInt32(grdTenderDetails.DataKeys[row.RowIndex].Value);

        if (tenderId > 0)
        {
            bool result = (new DataLayer()).DeleteTender(tenderId, Convert.ToInt32(Session["Person_Id"].ToString()));

            if (result)
            {
                MessageBox.Show("Tender deleted successfully!");
                GetTenderDetails(Convert.ToInt32(hf_ProjectWork_Id.Value));
            }
            else
            {
                MessageBox.Show("Error deleting tender. Please try again.");
            }
        }
        else
        {
            // For new rows that weren't saved yet, just remove from grid
            DataTable dt = (DataTable)ViewState["dtTender"];
            dt.Rows.RemoveAt(row.RowIndex);
            grdTenderDetails.DataSource = dt;
            grdTenderDetails.DataBind();
        }
    }

    protected void grdTenderDetails_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        if (gv.Rows.Count > 0)
        {
            gv.UseAccessibleHeader = true;
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void grdTenderDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string documentPath = e.Row.Cells[1].Text.Trim().Replace("&nbsp;", "");
            LinkButton lnkBtn = (LinkButton)e.Row.FindControl("lnkTenderDoc");

            if (string.IsNullOrEmpty(documentPath))
            {
                lnkBtn.Visible = false;
            }
            else
            {
                lnkBtn.Visible = true;
                e.Row.Cells[2].BackColor = Color.LightGreen;
            }

            // Set the selected value for Tender Status dropdown
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            if (rowView != null)
            {
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlTenderStatus");
                if (ddlStatus != null && !string.IsNullOrEmpty(rowView["Tender_Status"].ToString()))
                {
                    ddlStatus.SelectedValue = rowView["Tender_Status"].ToString();
                }

                DropDownList ddlFailureReason = (DropDownList)e.Row.FindControl("ddlFailureReason");
                if (ddlFailureReason != null && !string.IsNullOrEmpty(rowView["Failure_Reason"].ToString()))
                {
                    ddlFailureReason.SelectedValue = rowView["Failure_Reason"].ToString();
                }
            }
        }
    }
}