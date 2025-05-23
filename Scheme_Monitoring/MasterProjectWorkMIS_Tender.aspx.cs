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
                Get_Tender_Details(ProjectWork_Id);
            }
        }
    }

    private void Get_Tender_Details(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).Get_tbl_ProjectTender(ProjectWork_Id);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdTenderDetails.DataSource = ds.Tables[0];
            grdTenderDetails.DataBind();
            ViewState["dtTender"] = ds.Tables[0];
        }
        else
        {
            DataTable dt = new DataTable();
            DataColumn dc_1 = new DataColumn("ProjectTender_Id", typeof(int));
            DataColumn dc_2 = new DataColumn("ProjectTender_NITDate", typeof(string));
            DataColumn dc_3 = new DataColumn("ProjectTender_IssueDate", typeof(string));
            DataColumn dc_4 = new DataColumn("ProjectTender_EndDate", typeof(string));
            DataColumn dc_5 = new DataColumn("ProjectTender_EMD", typeof(decimal));
            DataColumn dc_6 = new DataColumn("ProjectTender_Remarks", typeof(string));
            DataColumn dc_7 = new DataColumn("ProjectTender_Status", typeof(string));
            DataColumn dc_8 = new DataColumn("ProjectTender_FailureReason", typeof(string));
            DataColumn dc_9 = new DataColumn("ProjectTender_Document", typeof(string));

            dt.Columns.AddRange(new DataColumn[] { dc_1, dc_2, dc_3, dc_4, dc_5, dc_6, dc_7, dc_8, dc_9 });

            DataRow dr = dt.NewRow();
            dr["ProjectTender_Id"] = 0;
            dt.Rows.Add(dr);

            ViewState["dtTender"] = dt;
            grdTenderDetails.DataSource = dt;
            grdTenderDetails.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string FilePath = "";
        List<tbl_ProjectTender> obj_tbl_ProjectTender_Li = new List<tbl_ProjectTender>();

        for (int i = 0; i < grdTenderDetails.Rows.Count; i++)
        {
            TextBox txtNITDate = grdTenderDetails.Rows[i].FindControl("txtNITDate") as TextBox;
            TextBox txtTenderIssueDate = grdTenderDetails.Rows[i].FindControl("txtTenderIssueDate") as TextBox;
            TextBox txtTenderEndDate = grdTenderDetails.Rows[i].FindControl("txtTenderEndDate") as TextBox;
            TextBox txtEMD = grdTenderDetails.Rows[i].FindControl("txtEMD") as TextBox;
            TextBox txtRemarks = grdTenderDetails.Rows[i].FindControl("txtRemarks") as TextBox;
            DropDownList ddlTenderStatus = grdTenderDetails.Rows[i].FindControl("ddlTenderStatus") as DropDownList;
            DropDownList ddlFailureReason = grdTenderDetails.Rows[i].FindControl("ddlFailureReason") as DropDownList;
            FileUpload fuTenderFile = grdTenderDetails.Rows[i].FindControl("fuTenderFile") as FileUpload;

            FilePath = grdTenderDetails.Rows[i].Cells[1].Text.Trim();

            tbl_ProjectTender obj_tbl_ProjectTender = new tbl_ProjectTender();
            obj_tbl_ProjectTender.ProjectTender_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectTender.ProjectTender_NITDate = txtNITDate.Text.Trim();
            obj_tbl_ProjectTender.ProjectTender_IssueDate = txtTenderIssueDate.Text.Trim();
            obj_tbl_ProjectTender.ProjectTender_EndDate = txtTenderEndDate.Text.Trim();
            obj_tbl_ProjectTender.ProjectTender_Remarks = txtRemarks.Text.Trim();
            obj_tbl_ProjectTender.ProjectTender_Status = ddlTenderStatus.SelectedValue;
            obj_tbl_ProjectTender.ProjectTender_FailureReason = ddlFailureReason.SelectedValue;
            obj_tbl_ProjectTender.ProjectTender_ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);

            try
            {
                obj_tbl_ProjectTender.ProjectTender_Id = Convert.ToInt32(grdTenderDetails.Rows[i].Cells[0].Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectTender.ProjectTender_Id = 0;
            }

            try
            {
                obj_tbl_ProjectTender.ProjectTender_EMD = Convert.ToDecimal(txtEMD.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectTender.ProjectTender_EMD = 0;
            }

            // Validate required fields
            if (txtTenderIssueDate.Text.Trim() == "")
            {
                MessageBox.Show("Please enter Tender Issue Date");
                return;
            }

            if (txtTenderEndDate.Text.Trim() == "")
            {
                MessageBox.Show("Please enter Tender End Date");
                return;
            }

            if (txtEMD.Text.Trim() == "")
            {
                MessageBox.Show("Please enter EMD amount");
                return;
            }

            if (FilePath.Replace("&nbsp;", "") == "")
            {
                if (!fuTenderFile.HasFile)
                {
                    MessageBox.Show("Please upload Tender Document");
                    return;
                }
            }

            try
            {
                if (fuTenderFile.HasFile)
                {
                    obj_tbl_ProjectTender.ProjectTender_Document_Bytes = fuTenderFile.FileBytes;
                }
                else
                {
                    obj_tbl_ProjectTender.ProjectTender_Document_Bytes = null;
                }
            }
            catch { }

            obj_tbl_ProjectTender_Li.Add(obj_tbl_ProjectTender);
        }

        bool flag = false;
        try
        {
            DataLayer dataLayer = new DataLayer();
            flag = dataLayer.Update_tbl_ProjectTender(obj_tbl_ProjectTender_Li, null, Convert.ToInt32(hf_ProjectWork_Id.Value));
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }

        if (flag)
        {
            MessageBox.Show("Tender details saved successfully!");
            Get_Tender_Details(Convert.ToInt32(hf_ProjectWork_Id.Value));
        }
        else
        {
            MessageBox.Show("Error in saving tender details!");
        }
    }

    protected void btnAddTender_Click(object sender, ImageClickEventArgs e)
    {
        Add_Tender_Record("U");
    }

    private void Add_Tender_Record(string Entry_Type)
    {
        DataTable dtTender;
        if (ViewState["dtTender"] != null)
        {
            dtTender = (DataTable)(ViewState["dtTender"]);
            DataRow dr = dtTender.NewRow();
            dtTender.Rows.Add(dr);
            ViewState["dtTender"] = dtTender;

            grdTenderDetails.DataSource = dtTender;
            grdTenderDetails.DataBind();
        }
        else
        {
            dtTender = new DataTable();

            DataColumn dc_Sr_No = new DataColumn("Sr_No", typeof(int));
            dtTender.Columns.AddRange(new DataColumn[] { dc_Sr_No });

            DataRow dr = dtTender.NewRow();
            dtTender.Rows.Add(dr);
            ViewState["dtTender"] = dtTender;

            grdTenderDetails.DataSource = dtTender;
            grdTenderDetails.DataBind();
        }
    }

    protected void imgDeleteTender_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["dtTender"] != null)
        {
            DataTable dt = (DataTable)ViewState["dtTender"];
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
            grdTenderDetails.DataSource = dt;
            grdTenderDetails.DataBind();
            ViewState["dtTender"] = dt;
        }
    }

    protected void btnDeleteTender_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int ProjectTender_Id = 0;
        try
        {
            ProjectTender_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectTender_Id = 0;
        }

        if (ProjectTender_Id == 0)
        {
            MessageBox.Show("Nothing to delete");
            return;
        }

        if (new DataLayer().Delete_tbl_ProjectTender(ProjectTender_Id, Convert.ToInt32(Session["Person_Id"].ToString())))
        {
            int ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
            Get_Tender_Details(ProjectWork_Id);
            MessageBox.Show("Tender deleted successfully");
        }
        else
        {
            MessageBox.Show("Error in deleting tender");
        }
    }

    protected void grdTenderDetails_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        if (gv.Rows.Count > 0)
        {
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

    protected void grdTenderDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string ProjectTender_Document = e.Row.Cells[1].Text.Trim().Replace("&nbsp;", "");
            if (ProjectTender_Document != "")
            {
                e.Row.Cells[2].BackColor = Color.LightGreen;
            }
            else
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("lnkTenderDoc");
                lnkBtn.Visible = false;
            }

            // Set the selected value for Tender Status dropdown
            DropDownList ddlTenderStatus = (DropDownList)e.Row.FindControl("ddlTenderStatus");
            string status = DataBinder.Eval(e.Row.DataItem, "ProjectTender_Status").ToString();
            if (!string.IsNullOrEmpty(status))
            {
                ddlTenderStatus.SelectedValue = status;
            }

            // Set the selected value for Failure Reason dropdown
            DropDownList ddlFailureReason = (DropDownList)e.Row.FindControl("ddlFailureReason");
            string failureReason = DataBinder.Eval(e.Row.DataItem, "ProjectTender_FailureReason").ToString();
            if (!string.IsNullOrEmpty(failureReason))
            {
                ddlFailureReason.SelectedValue = failureReason;
            }
        }
    }

    protected void btnAction_Click(object sender, ImageClickEventArgs e)
    {
        mp1.Show();
    }

    protected void btnUpdateAction_Click(object sender, EventArgs e)
    {
        // Implementation for updating tender action
        mp1.Hide();
    }
}