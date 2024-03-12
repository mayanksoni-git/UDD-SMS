using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterProjectWorkMIS_3 : System.Web.UI.Page
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
            divPreInvoiceDetails.Visible = false;
            if (Session["PersonJuridiction_DesignationId"].ToString() == "1" || Session["PersonJuridiction_DesignationId"].ToString() == "33")
            {
                btnSkip.Visible = true;
            }
            else if (Session["UserType"].ToString() == "1" || Session["UserType"].ToString() == "4" || Session["UserType"].ToString() == "6" || Session["UserType"].ToString() == "8")
            {
                btnSkip.Visible = true;
            }
            else
            {
                btnSkip.Visible = false;
            }
            if (Request.QueryString.Count > 0)
            {
                int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
                hf_Scheme_Id.Value = Request.QueryString["Id"].ToString().Trim();
                get_Project_Invoice_Details(ProjectWork_Id);
                get_tbl_ProjectClosure(ProjectWork_Id.ToString());
                if (hf_Scheme_Id.Value == "1015")
                {
                    get_Project_Centage_Received(ProjectWork_Id);
                    divCentage.Visible = true;
                }
                else
                {
                    divCentage.Visible = false;
                    grdCentageDetails.DataSource = null;
                    grdCentageDetails.DataBind();
                }

                if (hf_Scheme_Id.Value == "1013" || hf_Scheme_Id.Value == "1016")
                {
                    get_ProjectWorkFinancialDoc(ProjectWork_Id);
                    divFinancialDoc.Visible = true;
                }
                else
                {
                    divFinancialDoc.Visible = false;
                    grdFinancialDoc.DataSource = null;
                    grdFinancialDoc.DataBind();
                }
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        PostBackTrigger trg1 = new PostBackTrigger();
        trg1.ControlID = btnUploadDocs1.ID;
        up.Triggers.Add(trg1);

        PostBackTrigger trg2 = new PostBackTrigger();
        trg2.ControlID = btnSave.ID;
        up.Triggers.Add(trg2);
    }

    private void get_Project_Invoice_Details(int ProjectWork_Id)
    {
        hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Project_Invoice_Details(ProjectWork_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdProjectInvoiceDetails.DataSource = ds.Tables[0];
            grdProjectInvoiceDetails.DataBind();
            get_PreviousInvoiceDetails_ProjectWise(ProjectWork_Id);
        }
        else
        {
            grdProjectInvoiceDetails.DataSource = null;
            grdProjectInvoiceDetails.DataBind();
        }
    }
    protected void get_tbl_ProjectClosure(string ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectClosure(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["ProjectClosure_ClosureApplicable"].ToString().Trim().Replace("&nbsp;", "") != "")
            {
                rbtPhysicalClosureApplicable.SelectedValue = ds.Tables[0].Rows[0]["ProjectClosure_ClosureApplicable"].ToString().Trim().Replace("&nbsp;", "");
            }
            else
            {
                rbtPhysicalClosureApplicable.Items[0].Selected = false;
                rbtPhysicalClosureApplicable.Items[1].Selected = false;
            }
            rbtPhysicalClosureApplicable_SelectedIndexChanged(rbtPhysicalClosureApplicable, new EventArgs());

            if (ds.Tables[0].Rows[0]["ProjectClosure_HandoverNoteSend"].ToString().Trim().Replace("&nbsp;", "") != "")
            {
                rbtHandoverNote.SelectedValue = ds.Tables[0].Rows[0]["ProjectClosure_HandoverNoteSend"].ToString().Trim().Replace("&nbsp;", "");
            }
            else
            {
                rbtHandoverNote.Items[0].Selected = false;
                rbtHandoverNote.Items[1].Selected = false;
            }
            rbtHandoverNote_SelectedIndexChanged(rbtHandoverNote, new EventArgs());

            txtHandoverNoteYesRef_Number.Text = ds.Tables[0].Rows[0]["ProjectClosure_HandoverNote_Yes_LetterNo"].ToString().Trim().Replace("&nbsp;", "");
            txtHandoverNoteYesDate.Text = ds.Tables[0].Rows[0]["ProjectClosure_HandoverNote_Yes_LetterDate"].ToString().Trim().Replace("&nbsp;", "");
            if (ds.Tables[0].Rows[0]["ProjectClosure_HandoverNote_Yes_LetterPath"].ToString().Trim().Replace("&nbsp;", "") != "")
            {
                flHandoverNoteDoc.ToolTip = ds.Tables[0].Rows[0]["ProjectClosure_HandoverNote_Yes_LetterPath"].ToString().Trim().Replace("&nbsp;", "");
                hf_DownloadLetterHandover.Value = ds.Tables[0].Rows[0]["ProjectClosure_HandoverNote_Yes_LetterPath"].ToString().Trim().Replace("&nbsp;", "");
                btnDownloadLetterHandover.Visible = true;
            }
            else
            {
                flHandoverNoteDoc.ToolTip = "";
                hf_DownloadLetterHandover.Value = "";
                btnDownloadLetterHandover.Visible = false;
            }

            txtHandoverTentitiveDate.Text = ds.Tables[0].Rows[0]["ProjectClosure_HandoverNote_No_TentitiveDate"].ToString().Trim().Replace("&nbsp;", "");
            txtHandoverNoRemarks.Text = ds.Tables[0].Rows[0]["ProjectClosure_HandoverNote_No_Comments"].ToString().Trim().Replace("&nbsp;", "");

            if (ds.Tables[0].Rows[0]["ProjectClosure_HandoverDone"].ToString().Trim().Replace("&nbsp;", "") != "")
            {
                rbtHandOverDone.SelectedValue = ds.Tables[0].Rows[0]["ProjectClosure_HandoverDone"].ToString().Trim().Replace("&nbsp;", "");
            }
            else
            {
                rbtHandOverDone.Items[0].Selected = false;
                rbtHandOverDone.Items[1].Selected = false;
            }
            rbtHandOverDone_SelectedIndexChanged(rbtHandOverDone, new EventArgs());
            txtHandOverDoneLetterNo.Text = ds.Tables[0].Rows[0]["ProjectClosure_HandoverDone_Yes_LetterNo"].ToString().Trim().Replace("&nbsp;", "");
            txtHandOverDoneDate.Text = ds.Tables[0].Rows[0]["ProjectClosure_HandoverDone_Yes_LetterDate"].ToString().Trim().Replace("&nbsp;", "");
            txtDefectLiabilityDate.Text = ds.Tables[0].Rows[0]["ProjectClosure_HandoverDone_Yes_DefectLibelityPeriod"].ToString().Trim().Replace("&nbsp;", "");
            if (ds.Tables[0].Rows[0]["ProjectClosure_HandoverDone_Yes_LetterPath"].ToString().Trim().Replace("&nbsp;", "") != "")
            {
                flHandOverDoneDoc.ToolTip = ds.Tables[0].Rows[0]["ProjectClosure_HandoverDone_Yes_LetterPath"].ToString().Trim().Replace("&nbsp;", "");
                hf_HandOverDoneDoc.Value = ds.Tables[0].Rows[0]["ProjectClosure_HandoverDone_Yes_LetterPath"].ToString().Trim().Replace("&nbsp;", "");
                btnHandOverDoneDoc.Visible = true;
            }
            else
            {
                flHandOverDoneDoc.ToolTip = "";
                hf_HandOverDoneDoc.Value = "";
                btnHandOverDoneDoc.Visible = false;
            }

            txtHandOverDoneTentitiveDate.Text = ds.Tables[0].Rows[0]["ProjectClosure_HandoverDone_No_TentitiveDate"].ToString().Trim().Replace("&nbsp;", "");
            txtHandOverDoneRemarks.Text = ds.Tables[0].Rows[0]["ProjectClosure_HandoverDone_No_Comments"].ToString().Trim().Replace("&nbsp;", "");

            if (ds.Tables[0].Rows[0]["ProjectClosure_FinancialClosureApplicable"].ToString().Trim().Replace("&nbsp;", "") != "")
            {
                rbtFinancialClosureApplicable.SelectedValue = ds.Tables[0].Rows[0]["ProjectClosure_FinancialClosureApplicable"].ToString().Trim().Replace("&nbsp;", "");
            }
            else
            {
                rbtFinancialClosureApplicable.Items[0].Selected = false;
                rbtFinancialClosureApplicable.Items[1].Selected = false;
            }
            rbtFinancialClosureApplicable_SelectedIndexChanged(rbtFinancialClosureApplicable, new EventArgs());
            txtFinancialClosureLetterNo.Text = ds.Tables[0].Rows[0]["ProjectClosure_FinancialClosureApplicable_Yes_LetterNo"].ToString().Trim().Replace("&nbsp;", "");
            txtFinancialClosureLetterDate.Text = ds.Tables[0].Rows[0]["ProjectClosure_FinancialClosureApplicable_Yes_LetterDate"].ToString().Trim().Replace("&nbsp;", "");
            if (ds.Tables[0].Rows[0]["ProjectClosure_FinancialClosureApplicable_Yes_LetterPath"].ToString().Trim().Replace("&nbsp;", "") != "")
            {
                flFinancialClosureDoc.ToolTip = ds.Tables[0].Rows[0]["ProjectClosure_FinancialClosureApplicable_Yes_LetterPath"].ToString().Trim().Replace("&nbsp;", "");
                hf_FinancialClosureDoc.Value = ds.Tables[0].Rows[0]["ProjectClosure_FinancialClosureApplicable_Yes_LetterPath"].ToString().Trim().Replace("&nbsp;", "");
                btnFinancialClosureDoc.Visible = true;
            }
            else
            {
                flFinancialClosureDoc.ToolTip = "";
                hf_FinancialClosureDoc.Value = "";
                btnFinancialClosureDoc.Visible = false;
            }

            txtFinancialClosureTentitiveDate.Text = ds.Tables[0].Rows[0]["ProjectClosure_FinancialClosureApplicable_No_TentitiveDate"].ToString().Trim().Replace("&nbsp;", "");
            txtFinancialClosureRemarks.Text = ds.Tables[0].Rows[0]["ProjectClosure_FinancialClosureApplicable_No_Comments"].ToString().Trim().Replace("&nbsp;", "");
        }
        else
        {
            flHandoverNoteDoc.ToolTip = "";
            hf_DownloadLetterHandover.Value = "";
            btnDownloadLetterHandover.Visible = false;

            flHandOverDoneDoc.ToolTip = "";
            hf_HandOverDoneDoc.Value = "";
            btnHandOverDoneDoc.Visible = false;

            flFinancialClosureDoc.ToolTip = "";
            hf_FinancialClosureDoc.Value = "";
            btnFinancialClosureDoc.Visible = false;
        }
    }
        
    protected void btnUploadDocs1_Click(object sender, EventArgs e)
    {
        tbl_ProjectClosure obj_tbl_ProjectClosure = new tbl_ProjectClosure();
        obj_tbl_ProjectClosure.ProjectClosure_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_ProjectClosure.ProjectClosure_Status = 1;
        obj_tbl_ProjectClosure.ProjectClosure_Work_Id = Convert.ToInt32(Request.QueryString[0].Trim());
        obj_tbl_ProjectClosure.ProjectClosure_ClosureApplicable = rbtPhysicalClosureApplicable.SelectedValue;
        obj_tbl_ProjectClosure.ProjectClosure_HandoverNoteSend = rbtHandoverNote.SelectedValue;
        obj_tbl_ProjectClosure.ProjectClosure_HandoverNote_Yes_LetterNo = txtHandoverNoteYesRef_Number.Text.Trim();
        obj_tbl_ProjectClosure.ProjectClosure_HandoverNote_Yes_LetterDate = txtHandoverNoteYesDate.Text.Trim();
        if (flHandoverNoteDoc.HasFile)
        {
            obj_tbl_ProjectClosure.ProjectClosure_HandoverNote_Yes_LetterPath_Bytes = flHandoverNoteDoc.FileBytes;
        }
        else
        {
            obj_tbl_ProjectClosure.ProjectClosure_HandoverNote_Yes_LetterPath = flHandoverNoteDoc.ToolTip;
        }
        obj_tbl_ProjectClosure.ProjectClosure_HandoverNote_No_TentitiveDate = txtHandoverTentitiveDate.Text;
        obj_tbl_ProjectClosure.ProjectClosure_HandoverNote_No_Comments = txtHandoverNoRemarks.Text;
        obj_tbl_ProjectClosure.ProjectClosure_HandoverDone = rbtHandOverDone.SelectedValue;
        obj_tbl_ProjectClosure.ProjectClosure_HandoverDone_Yes_LetterNo = txtHandOverDoneLetterNo.Text;
        obj_tbl_ProjectClosure.ProjectClosure_HandoverDone_Yes_LetterDate = txtHandOverDoneDate.Text;
        obj_tbl_ProjectClosure.ProjectClosure_HandoverDone_Yes_DefectLibelityPeriod = txtDefectLiabilityDate.Text;
        if (flHandOverDoneDoc.HasFile)
        {
            obj_tbl_ProjectClosure.ProjectClosure_HandoverDone_Yes_LetterPath_Bytes = flHandOverDoneDoc.FileBytes;
        }
        else
        {
            obj_tbl_ProjectClosure.ProjectClosure_HandoverDone_Yes_LetterPath = flHandOverDoneDoc.ToolTip;
        }
        obj_tbl_ProjectClosure.ProjectClosure_HandoverDone_No_TentitiveDate = txtHandOverDoneTentitiveDate.Text;
        obj_tbl_ProjectClosure.ProjectClosure_HandoverDone_No_Comments = txtHandOverDoneRemarks.Text;
        obj_tbl_ProjectClosure.ProjectClosure_FinancialClosureApplicable = rbtFinancialClosureApplicable.SelectedValue;
        obj_tbl_ProjectClosure.ProjectClosure_FinancialClosureApplicable_Yes_LetterNo = txtFinancialClosureLetterNo.Text;
        obj_tbl_ProjectClosure.ProjectClosure_FinancialClosureApplicable_Yes_LetterDate = txtFinancialClosureLetterDate.Text;
        if (flFinancialClosureDoc.HasFile)
        {
            obj_tbl_ProjectClosure.ProjectClosure_FinancialClosureApplicable_Yes_LetterPath_Bytes = flFinancialClosureDoc.FileBytes;
        }
        else
        {
            obj_tbl_ProjectClosure.ProjectClosure_FinancialClosureApplicable_Yes_LetterPath = flFinancialClosureDoc.ToolTip;
        }
        obj_tbl_ProjectClosure.ProjectClosure_FinancialClosureApplicable_No_TentitiveDate = txtFinancialClosureTentitiveDate.Text;
        obj_tbl_ProjectClosure.ProjectClosure_FinancialClosureApplicable_No_Comments = txtFinancialClosureRemarks.Text;
        if (new DataLayer().Insert_tbl_ProjectClosure(obj_tbl_ProjectClosure))
        {
            MessageBox.Show("Project Closure Details Saved Successfully");
            string ProjectWork_Id = Request.QueryString[0].Trim();
            get_tbl_ProjectClosure(ProjectWork_Id);
            return;
        }
        else
        {
            MessageBox.Show("Error In Updating Project Closure Details");
            return;
        }
    }
    private decimal get_PreviousInvoiceDetailsADP_ProjectWise(int ProjectWork_Id)
    {
        decimal adp_total = 0;
        hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PreviousInvoiceDetailsADP_ProjectWise(ProjectWork_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            try
            {
                adp_total = decimal.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString());
            }
            catch
            {
                adp_total = 0;
            }
            grdInvoiceADP.DataSource = ds.Tables[0];
            grdInvoiceADP.DataBind();
            grdInvoiceADP.FooterRow.Cells[4].Text = ds.Tables[0].Compute("sum(Total_Amount)", "").ToString();
            grdInvoiceADP.FooterRow.Cells[5].Text = ds.Tables[0].Compute("sum(GST)", "").ToString();
            grdInvoiceADP.FooterRow.Cells[6].Text = ds.Tables[0].Compute("sum(Amount)", "").ToString();
        }
        else
        {
            adp_total = 0;
            divPreInvoiceDetails.Visible = false;
            grdInvoiceADP.DataSource = null;
            grdInvoiceADP.DataBind();
        }
        return adp_total;
    }
    private void get_PreviousInvoiceDetails_ProjectWise(int ProjectWork_Id)
    {
        decimal invprev_total = 0;
        hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PreviousInvoiceDetails_ProjectWise(ProjectWork_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            try
            {
                invprev_total = decimal.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString());
            }
            catch
            {
                invprev_total = 0;
            }
            grdInvoice.DataSource = ds.Tables[0];
            grdInvoice.DataBind();
            grdInvoice.FooterRow.Cells[6].Text = ds.Tables[0].Compute("sum(Total_Amount)", "").ToString();
            grdInvoice.FooterRow.Cells[7].Text = ds.Tables[0].Compute("sum(GST)", "").ToString();
            grdInvoice.FooterRow.Cells[8].Text = ds.Tables[0].Compute("sum(Amount)", "").ToString();
        }
        else
        {
            invprev_total = 0;
            //(grdProjectInvoiceDetails.Rows[0].FindControl("lblPrevInvoiceTotal") as LinkButton).Text = "";
            divPreInvoiceDetails.Visible = false;
            grdInvoice.DataSource = null;
            grdInvoice.DataBind();
        }

        decimal adp_total = get_PreviousInvoiceDetailsADP_ProjectWise(ProjectWork_Id);
        (grdProjectInvoiceDetails.Rows[0].FindControl("lblPrevInvoiceTotal") as LinkButton).Text = (invprev_total + adp_total).ToString();

        ds = new DataSet();
        ds = (new DataLayer()).get_Target_ProjectWise(ProjectWork_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            //(grdProjectInvoiceDetails.Rows[0].FindControl("txtTargetMonth") as TextBox).Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["ProjectWorkFinancialTarget_TargetMonth"].ToString();
            (grdProjectInvoiceDetails.Rows[0].FindControl("txtTargetMonth") as TextBox).Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["ProjectWorkFinancialTarget_Year"].ToString();
            (grdProjectInvoiceDetails.Rows[0].FindControl("ddlMonth") as DropDownList).SelectedValue = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["ProjectWorkFinancialTarget_Month"].ToString();
        }
        else
        {
            //(grdProjectInvoiceDetails.Rows[0].FindControl("txtTargetMonth") as TextBox).Text = "";
            (grdProjectInvoiceDetails.Rows[0].FindControl("txtTargetMonth") as TextBox).Text = DateTime.Now.Year.ToString();
            (grdProjectInvoiceDetails.Rows[0].FindControl("ddlMonth") as DropDownList).SelectedValue = DateTime.Now.Month.ToString();
        }

        if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            (grdProjectInvoiceDetails.Rows[0].FindControl("txtPhysicalTarget") as TextBox).Text = ds.Tables[1].Rows[ds.Tables[0].Rows.Count - 1]["ProjectWorkPhysicalTarget_Target"].ToString();
            hf_PrePhysicalProgress.Value = ds.Tables[1].Rows[ds.Tables[0].Rows.Count - 1]["ProjectWorkPhysicalTarget_Target"].ToString();
        }
        else
        {
            (grdProjectInvoiceDetails.Rows[0].FindControl("txtPhysicalTarget") as TextBox).Text = "0";
            hf_PrePhysicalProgress.Value = "0";
        }
        if (ds != null && ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
        {
            (grdProjectInvoiceDetails.Rows[0].FindControl("txtFinancialTarget") as TextBox).Text = ds.Tables[2].Rows[0]["Financial_Progress"].ToString();
        }
        else
        {
            (grdProjectInvoiceDetails.Rows[0].FindControl("txtFinancialTarget") as TextBox).Text = "0.00";
        }

        if (ds != null && ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
        {
            (grdProjectInvoiceDetails.Rows[0].FindControl("txtFinancialTargetA") as TextBox).Text = ds.Tables[3].Rows[0]["ProjectWorkFinancialTarget_TargetA"].ToString();
        }
        else
        {
            (grdProjectInvoiceDetails.Rows[0].FindControl("txtFinancialTargetA") as TextBox).Text = "0.00";
        }

        if (ds != null && ds.Tables.Count > 4 && ds.Tables[4].Rows.Count > 0)
        {
            if (ds.Tables[4].Rows[0]["ProjectWork_PhysicalCompleted"].ToString() == "1")
            {
                chkPhysicalCompleted.Checked = true;
            }
            else
            {
                chkPhysicalCompleted.Checked = false;
            }
            if (ds.Tables[4].Rows[0]["ProjectWork_FinancialCompletedAll"].ToString() == "1")
            {
                chkFinancialCompletedAll.Checked = true;
            }
            else
            {
                chkFinancialCompletedAll.Checked = false;
            }
            if (ds.Tables[4].Rows[0]["ProjectWork_FinancialCompletedPartial"].ToString() == "1")
            {
                chkFinancialCompletedPartial.Checked = true;
            }
            else
            {
                chkFinancialCompletedPartial.Checked = false;
            }
        }
        else
        {
            chkPhysicalCompleted.Checked = false;
            chkFinancialCompletedAll.Checked = false;
            chkFinancialCompletedPartial.Checked = false;
        }
        if (ds != null && ds.Tables.Count > 5 && ds.Tables[5].Rows.Count > 0)
        {
            (grdProjectInvoiceDetails.Rows[0].FindControl("txtTrialRunDate") as TextBox).Text = ds.Tables[5].Rows[0]["ProjectWorkFinancialTarget_TrialMonth"].ToString();
        }
        else
        {
            (grdProjectInvoiceDetails.Rows[0].FindControl("txtTrialRunDate") as TextBox).Text = "";
        }
    }

    private void get_Package_and_Month_Wise_Invoice_Details(int ProjectWork_Id)
    {
        hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Package_and_Month_Wise_Invoice_Details(ProjectWork_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
        }
        else
        {
            grdPost.DataSource = null;
            grdPost.DataBind();
        }
    }

    private void close_All(int rowIndex)
    {
        for (int i = 0; i < grdPost.Rows.Count; i++)
        {
            if (rowIndex == i)
            {
                continue;
            }
            else
            {
                ImageButton imgShowHide = grdPost.Rows[i].FindControl("imgShow") as ImageButton;
                grdPost.Rows[i].FindControl("pnlOrders").Visible = false;
                grdPost.Rows[i].FindControl("pnlOrdersDiv").Visible = false;
                imgShowHide.CommandArgument = "Show";
                imgShowHide.ImageUrl = "assets/images/plus.png";
            }
        }
    }
    protected void Show_Hide_ChildGrid(object sender, EventArgs e)
    {
        ImageButton imgShowHide = (sender as ImageButton);
        GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
        //close_All(row.RowIndex);
        if (imgShowHide.CommandArgument == "Show")
        {
            row.FindControl("pnlOrders").Visible = true;
            row.FindControl("pnlOrdersDiv").Visible = true;
            imgShowHide.CommandArgument = "Hide";
            imgShowHide.ImageUrl = "assets/images/minus.png";

            GridView grdPostBeat = row.FindControl("grdPostBeat") as GridView;
            GridView grdADP = row.FindControl("grdADP") as GridView;
            GridView grdMA = row.FindControl("grdMA") as GridView;
            GridView grdDeductionRelease = row.FindControl("grdDeductionRelease") as GridView;
            int ProjectWorkPkg_Id = Convert.ToInt32(row.Cells[0].Text.Trim().Replace("&nbsp;", ""));

            get_tbl_PackageInvoice(ProjectWorkPkg_Id, grdPostBeat);

            get_tbl_PackageInvoiceADP(ProjectWorkPkg_Id, grdADP);

            get_tbl_PackageInvoiceMA(ProjectWorkPkg_Id, grdMA);

            get_tbl_Package_DeductionRelease(ProjectWorkPkg_Id, grdDeductionRelease);
        }
        else
        {
            row.FindControl("pnlOrders").Visible = false;
            row.FindControl("pnlOrdersDiv").Visible = false;
            imgShowHide.CommandArgument = "Show";
            imgShowHide.ImageUrl = "assets/images/plus.png";
        }
    }
    private void get_tbl_Package_DeductionRelease(int Package_Id, GridView grdPostBeat)
    {
        DataSet ds = new DataSet();
        bool? isDefered = null;
        ds = (new DataLayer()).get_tbl_Package_DeductionRelease(0, "", 0, 0, 0, 0, Package_Id, "", "", 0, 0, true, "", "", 0, 0, 0, false, isDefered, 0);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPostBeat.DataSource = ds.Tables[0];
            grdPostBeat.DataBind();
        }
        else
        {
            grdPostBeat.DataSource = null;
            grdPostBeat.DataBind();
        }
    }
    private void get_tbl_PackageInvoiceMA(int Package_Id, GridView grdPostBeat)
    {
        DataSet ds = new DataSet();
        bool? isDefered = null;
        ds = (new DataLayer()).get_tbl_Package_MobilizationAdvance(0, "", 0, 0, 0, 0, Package_Id, "", "", 0, 0, true, "", "", 0, 0, 0, false, isDefered, 0);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPostBeat.DataSource = ds.Tables[0];
            grdPostBeat.DataBind();
        }
        else
        {
            grdPostBeat.DataSource = null;
            grdPostBeat.DataBind();
        }
    }
    private void get_tbl_PackageInvoiceADP(int Package_Id, GridView grdPostBeat)
    {
        DataSet ds = new DataSet();
        bool? isDefered = null;
        ds = (new DataLayer()).get_tbl_Package_ADP(0, "", 0, 0, 0, 0, Package_Id, "", "", 0, 0, true, "", "", 0, 0, 0, false, isDefered, 0);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPostBeat.DataSource = ds.Tables[0];
            grdPostBeat.DataBind();
        }
        else
        {
            grdPostBeat.DataSource = null;
            grdPostBeat.DataBind();
        }
    }
    private void get_tbl_PackageInvoice(int Package_Id, GridView grdPostBeat)
    {
        DataSet ds = new DataSet();
        bool? isDefered = null;
        ds = (new DataLayer()).get_tbl_PackageInvoice(Package_Id, 0, 0, 0, "", 0, 0, true, "", "", 0, 0, -1, false, -1, isDefered, "", 0, 0);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPostBeat.DataSource = ds.Tables[0];
            grdPostBeat.DataBind();
        }
        else
        {
            grdPostBeat.DataSource = null;
            grdPostBeat.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int personId = Convert.ToInt32(Session["Person_Id"].ToString());
        TextBox txtFinancialTarget = grdProjectInvoiceDetails.Rows[0].FindControl("txtFinancialTarget") as TextBox;
        TextBox txtPhysicalTarget = grdProjectInvoiceDetails.Rows[0].FindControl("txtPhysicalTarget") as TextBox;
        //TextBox txtTargetMonth = grdProjectInvoiceDetails.Rows[0].FindControl("txtTargetMonth") as TextBox;
        DropDownList ddlMonth = grdProjectInvoiceDetails.Rows[0].FindControl("ddlMonth") as DropDownList;
        TextBox txtTargetMonth = grdProjectInvoiceDetails.Rows[0].FindControl("txtTargetMonth") as TextBox;
        TextBox txtFinancialTargetA = grdProjectInvoiceDetails.Rows[0].FindControl("txtFinancialTargetA") as TextBox;
        TextBox txtTrialRunDate = grdProjectInvoiceDetails.Rows[0].FindControl("txtTrialRunDate") as TextBox;
        if (ddlMonth.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Target Month");
            return;
        }
        if (txtTargetMonth.Text.Trim() == "")
        {
            MessageBox.Show("Please Fill Target Year");
            return;
        }
        int month = Convert.ToInt32(ddlMonth.SelectedValue);
        int year = Convert.ToInt32(txtTargetMonth.Text);
        decimal physicalTarget = 0;
        decimal financialTarget = 0;
        decimal financialTargetA = 0;
        decimal physicalTargetPrev = 0;
        try
        {
            financialTarget = Convert.ToDecimal((grdProjectInvoiceDetails.Rows[0].FindControl("txtFinancialTarget") as TextBox).Text);
        }
        catch
        {
            financialTarget = 0;
        }
        try
        {
            financialTargetA = Convert.ToDecimal((grdProjectInvoiceDetails.Rows[0].FindControl("txtFinancialTargetA") as TextBox).Text);
        }
        catch
        {
            financialTargetA = 0;
        }
        try
        {
            physicalTargetPrev = Convert.ToDecimal(hf_PrePhysicalProgress.Value);
        }
        catch
        {
            physicalTargetPrev = 0;
        }
        try
        {
            physicalTarget = Convert.ToDecimal(txtPhysicalTarget.Text.Trim());
        }
        catch
        {
            physicalTarget = 0;
        }
        if (physicalTarget < physicalTargetPrev)
        {
            MessageBox.Show("Physical Target should be more then previous Entred Physical Target , For Less Target Contact to Admin !! ");
            return;
        }


        string FilePath = "";
        List<tbl_ProjectCentageReceived> obj_tbl_ProjectCentageReceived_Li = new List<tbl_ProjectCentageReceived>();

        for (int i = 0; i < grdCentageDetails.Rows.Count; i++)
        {
            TextBox txtCentageDate = grdCentageDetails.Rows[i].FindControl("txtCentageDate") as TextBox;
            TextBox txtRef_Number = grdCentageDetails.Rows[i].FindControl("txtRef_Number") as TextBox;
            TextBox txtAmount = grdCentageDetails.Rows[i].FindControl("txtAmount") as TextBox;
            FileUpload flUpload = grdCentageDetails.Rows[i].FindControl("flUpload") as FileUpload;
            FilePath = grdCentageDetails.Rows[i].Cells[1].Text.Trim();

            tbl_ProjectCentageReceived obj_tbl_ProjectCentageReceived = new tbl_ProjectCentageReceived();
            try
            {
                obj_tbl_ProjectCentageReceived.ProjectCentageReceived_Id = Convert.ToInt32(grdCentageDetails.Rows[i].Cells[0].Text);
            }
            catch
            {
                obj_tbl_ProjectCentageReceived.ProjectCentageReceived_Id = 0;
            }
            try
            {
                obj_tbl_ProjectCentageReceived.ProjectCentageReceived_Amount = Convert.ToDecimal(txtAmount.Text);
            }
            catch
            {
                obj_tbl_ProjectCentageReceived.ProjectCentageReceived_Amount = 0;
            }
            obj_tbl_ProjectCentageReceived.ProjectCentageReceived_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectCentageReceived.ProjectCentageReceived_Date = txtCentageDate.Text.Trim();
            obj_tbl_ProjectCentageReceived.ProjectCentageReceived_LetterNo = txtRef_Number.Text.Trim();
            obj_tbl_ProjectCentageReceived.ProjectCentageReceived_ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
            obj_tbl_ProjectCentageReceived.ProjectCentageReceived_Status = 1;
            if (flUpload.HasFile || FilePath.Replace("&nbsp;", "") != "")
            {
                if (txtRef_Number.Text.Trim() == "")
                {
                    MessageBox.Show("Please Fill Letter Number");
                    return;
                }
                if (FilePath.Replace("&nbsp;", "") == "")
                {
                    if (!flUpload.HasFile)
                    {
                        MessageBox.Show("Please Upload Centage Letter.");
                        return;
                    }
                }
                if (flUpload.HasFile)
                {
                    obj_tbl_ProjectCentageReceived.ProjectCentageReceived_Path_Bytes = flUpload.FileBytes;
                }
                else
                {
                    obj_tbl_ProjectCentageReceived.ProjectCentageReceived_Path_Bytes = null;
                }
                obj_tbl_ProjectCentageReceived_Li.Add(obj_tbl_ProjectCentageReceived);
            }
        }
        string FilePathLetter = "";
        string FilePathExcel = "";
        tbl_ProjectWorkFinancialDoc obj_tbl_ProjectWorkFinancialDoc = null;
        if ((hf_Scheme_Id.Value == "1013" || hf_Scheme_Id.Value == "1016") && grdFinancialDoc.Rows.Count > 0)
        {
            TextBox txtCompiledDate = grdFinancialDoc.Rows[0].FindControl("txtCompiledDate") as TextBox;
            TextBox txtCommentsFinancial = grdFinancialDoc.Rows[0].FindControl("txtCommentsFinancial") as TextBox;
            FileUpload flUploadLetter = grdFinancialDoc.Rows[0].FindControl("flUploadLetter") as FileUpload;
            FileUpload flUploadExcel = grdFinancialDoc.Rows[0].FindControl("flUploadExcel") as FileUpload;
            FilePathLetter = grdFinancialDoc.Rows[0].Cells[2].Text.Trim();
            FilePathExcel = grdFinancialDoc.Rows[0].Cells[3].Text.Trim();

            obj_tbl_ProjectWorkFinancialDoc = new tbl_ProjectWorkFinancialDoc();
            try
            {
                obj_tbl_ProjectWorkFinancialDoc.ProjectWorkFinancialDoc_Id = Convert.ToInt32(grdFinancialDoc.Rows[0].Cells[1].Text);
            }
            catch
            {
                obj_tbl_ProjectWorkFinancialDoc.ProjectWorkFinancialDoc_Id = 0;
            }
            try
            {
                obj_tbl_ProjectWorkFinancialDoc.ProjectWorkFinancialDoc_Work_Id = Convert.ToInt32(grdFinancialDoc.Rows[0].Cells[0].Text);
            }
            catch
            {
                obj_tbl_ProjectWorkFinancialDoc.ProjectWorkFinancialDoc_Work_Id = 0;
            }
            obj_tbl_ProjectWorkFinancialDoc.ProjectWorkFinancialDoc_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectWorkFinancialDoc.ProjectWorkFinancialDoc_Date = txtCompiledDate.Text.Trim();
            obj_tbl_ProjectWorkFinancialDoc.ProjectWorkFinancialDoc_Comments = txtCommentsFinancial.Text.Trim();
            obj_tbl_ProjectWorkFinancialDoc.ProjectWorkFinancialDoc_Work_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
            obj_tbl_ProjectWorkFinancialDoc.ProjectWorkFinancialDoc_Status = 1;
            if (flUploadExcel.HasFile || FilePathExcel.Replace("&nbsp;", "") != "")
            {
                if (flUploadExcel.HasFile)
                {
                    obj_tbl_ProjectWorkFinancialDoc.ProjectWorkFinancialDoc_PathExcel_Bytes = flUploadExcel.FileBytes;
                }
                else
                {
                    obj_tbl_ProjectWorkFinancialDoc.ProjectWorkFinancialDoc_PathExcel_Bytes = null;
                    obj_tbl_ProjectWorkFinancialDoc.ProjectWorkFinancialDoc_PathExcel = FilePathExcel.Replace("&nbsp;", "");
                }
                if (flUploadLetter.HasFile)
                {
                    obj_tbl_ProjectWorkFinancialDoc.ProjectWorkFinancialDoc_PathLetter_Bytes = flUploadLetter.FileBytes;
                }
                else
                {
                    obj_tbl_ProjectWorkFinancialDoc.ProjectWorkFinancialDoc_PathLetter_Bytes = null;
                    obj_tbl_ProjectWorkFinancialDoc.ProjectWorkFinancialDoc_PathLetter = FilePathLetter.Replace("&nbsp;", "");
                }
            }
        }
        string TargetMonth_Date = DateTime.DaysInMonth(year, month).ToString().PadLeft(2, '0') + "/" + month.ToString().PadLeft(2, '0') + "/" + year.ToString();
        int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());

        int PhysicalCompleted = 0;
        int FinancialCompletedAll = 0;
        int FinancialCompletedPartial = 0;

        if (chkPhysicalCompleted.Checked)
        {
            PhysicalCompleted = 1;
        }
        else
        {
            PhysicalCompleted = 0;
        }

        if (chkFinancialCompletedAll.Checked)
        {
            FinancialCompletedAll = 1;
        }
        else
        {
            FinancialCompletedAll = 0;
        }

        if (chkFinancialCompletedPartial.Checked)
        {
            FinancialCompletedPartial = 1;
        }
        else
        {
            FinancialCompletedPartial = 0;
        }
        if (FinancialCompletedAll == 1 && FinancialCompletedPartial == 1)
        {
            MessageBox.Show("Financial Progress Completed Option 2 and Option 3 can not be Selected at the same time.");
            return;
        }
        if (new DataLayer().Insert_TargetDetails(ProjectWork_Id, financialTarget, physicalTarget, month, year, personId, TargetMonth_Date, financialTargetA, obj_tbl_ProjectCentageReceived_Li, obj_tbl_ProjectWorkFinancialDoc, PhysicalCompleted, FinancialCompletedAll, FinancialCompletedPartial, txtTrialRunDate.Text.Trim()))
        {
            Response.Redirect("MasterProjectWorkMIS_4.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&Id=" + hf_Scheme_Id.Value);
        }
        else
        {
            MessageBox.Show("Error In Saving Details!");
            return;
        }
    }

    protected void grdProjectInvoiceDetails_PreRender(object sender, EventArgs e)
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

    protected void lnkView_Click(object sender, EventArgs e)
    {
        int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
        get_Package_and_Month_Wise_Invoice_Details(ProjectWork_Id);
        divInvoiceDetails.Visible = true;
        divPreInvoiceDetails.Visible = false;
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

    protected void grdPostBeat_PreRender(object sender, EventArgs e)
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

    protected void grdProjectInvoiceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            string Month_Name_Before = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.AddMonths(-2).Month);
            string Year_Before = DateTime.Now.AddMonths(-2).Year.ToString();

            string Month_Name_Prev = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.AddMonths(-1).Month);
            string Year_Prev = DateTime.Now.AddMonths(-1).Year.ToString();

            string Month_Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
            string Year = DateTime.Now.Year.ToString();

            e.Row.Cells[3].Text = "Financial Achivment Till " + Month_Name_Before + " " + Year_Before + " (Bills Raised) - [In Lakhs]";
            e.Row.Cells[4].Text = "Financial Achivment Till " + Month_Name_Before + " " + Year_Before + " (Bills Approved) - [In Lakhs]";

            e.Row.Cells[5].Text = "Financial Achivment " + Month_Name_Prev + " " + Year_Prev + " (Bills Raised) - [In Lakhs]";
            e.Row.Cells[6].Text = "Financial Achivment " + Month_Name_Prev + " " + Year_Prev + " (Bills Approved) - [In Lakhs]";

            e.Row.Cells[7].Text = "Financial Achivment " + Month_Name + " " + Year + " (Bills Raised) - [In Lakhs]";
            e.Row.Cells[8].Text = "Financial Achivment " + Month_Name + " " + Year + " (Bills Approved) - [In Lakhs]";
        }
    }

    protected void btnSkip_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterProjectWorkMIS_4.aspx?ProjectWork_Id=" + hf_ProjectWork_Id.Value + "&Id=" + hf_Scheme_Id.Value);
    }

    protected void btnViewDeduction_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Invoice_Id = 0;
        try
        {
            Invoice_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Invoice_Id = 0;
        }


        if (Invoice_Id > 0)
        {
            DataSet ds = new DataSet();
            ds = (new DataLayer()).get_tbl_Deduction(Invoice_Id, 0);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                mpDeduction.Show();
                grdDeductionHistory.DataSource = ds.Tables[0];
                grdDeductionHistory.DataBind();
            }
            else
            {
                grdDeductionHistory.DataSource = null;
                grdDeductionHistory.DataBind();
            }
        }
    }

    protected void lblPrevInvoiceTotal_Click(object sender, EventArgs e)
    {
        divPreInvoiceDetails.Visible = true;
        divInvoiceDetails.Visible = false;
    }

    protected void lnkViewLog_Click(object sender, EventArgs e)
    {
        int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Target_Log_ProjectWise(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            mpLog.Show();
            grdChangeLog.DataSource = ds.Tables[0];
            grdChangeLog.DataBind();
        }
        else
        {
            grdChangeLog.DataSource = null;
            grdChangeLog.DataBind();
        }
    }

    protected void grdCentageDetails_PreRender(object sender, EventArgs e)
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

    protected void grdCentageDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string filePath = e.Row.Cells[1].Text.Trim().Replace("&nbsp;", "");
            if (filePath.Trim() != "")
            {
                e.Row.Cells[3].BackColor = Color.LightGreen;
            }
            else
            {
                LinkButton lnkDownload = (LinkButton)e.Row.FindControl("lnkDownload");
                lnkDownload.Visible = false;
            }
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int ProjectCentageReceived_Id = 0;
        try
        {
            ProjectCentageReceived_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectCentageReceived_Id = 0;
        }
        if (ProjectCentageReceived_Id == 0)
        {
            MessageBox.Show("Nothing To Delete");
            return;
        }
        if (new DataLayer().Delete_tbl_ProjectCentageReceived(ProjectCentageReceived_Id, Convert.ToInt32(Session["Person_Id"].ToString())))
        {
            int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
            if (hf_Scheme_Id.Value == "1015")
            {
                get_Project_Centage_Received(ProjectWork_Id);
                divCentage.Visible = true;
            }
            else
            {
                divCentage.Visible = false;
                grdCentageDetails.DataSource = null;
                grdCentageDetails.DataBind();
            }
            MessageBox.Show("Deleted Successfully");
            return;
        }
        else
        {
            MessageBox.Show("Error");
            return;
        }
    }

    protected void btnRemove_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int index = gr.RowIndex;
        if (ViewState["dtCentageDetails"] != null)
        {
            DataTable dt = (DataTable)ViewState["dtCentageDetails"];
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
            grdCentageDetails.DataSource = dt;
            grdCentageDetails.DataBind();
            ViewState["dtCentageDetails"] = dt;
        }
    }
    private void get_Project_Centage_Received(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Project_Centage_Received(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["dtCentageDetails"] = ds.Tables[0];
            grdCentageDetails.DataSource = ds.Tables[0];
            grdCentageDetails.DataBind();
        }
        else
        {
            try
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["ProjectCentageReceived_Id"] = 0;
                dr["ProjectCentageReceived_Comments"] = "";
                dr["ProjectCentageReceived_ProjectWork_Id"] = 0;
                dr["ProjectCentageReceived_Amount"] = 0;
                dr["ProjectCentageReceived_Path"] = "";
                dr["ProjectCentageReceived_Date"] = "";
                dr["ProjectCentageReceived_LetterNo"] = "";

                ds.Tables[0].Rows.Add(dr);

                ViewState["dtCentageDetails"] = ds.Tables[0];
                grdCentageDetails.DataSource = ds.Tables[0];
                grdCentageDetails.DataBind();
            }
            catch
            {
                grdCentageDetails.DataSource = null;
                grdCentageDetails.DataBind();
            }
        }
    }

    private void get_ProjectWorkFinancialDoc(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_ProjectWorkFinancialDoc(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdFinancialDoc.DataSource = ds.Tables[0];
            grdFinancialDoc.DataBind();
        }
        else
        {
            grdFinancialDoc.DataSource = null;
            grdFinancialDoc.DataBind();
        }
    }

    protected void btnAdd_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtCentageDetails;
        if (ViewState["dtCentageDetails"] != null)
        {
            dtCentageDetails = (DataTable)(ViewState["dtCentageDetails"]);
            DataRow dr = dtCentageDetails.NewRow();
            dtCentageDetails.Rows.Add(dr);
            ViewState["dtCentageDetails"] = dtCentageDetails;

            grdCentageDetails.DataSource = dtCentageDetails;
            grdCentageDetails.DataBind();
        }
        else
        {
            dtCentageDetails = new DataTable();

            DataColumn dc_ProjectCentageReceived_Id = new DataColumn("ProjectCentageReceived_Id", typeof(int));
            DataColumn dc_ProjectCentageReceived_Comments = new DataColumn("ProjectCentageReceived_Comments", typeof(string));
            DataColumn dc_ProjectCentageReceived_Path = new DataColumn("ProjectCentageReceived_Path", typeof(string));
            DataColumn dc_ProjectCentageReceived_ProjectWork_Id = new DataColumn("ProjectCentageReceived_ProjectWork_Id", typeof(int));
            DataColumn dc_ProjectCentageReceived_Amount = new DataColumn("ProjectCentageReceived_Amount", typeof(decimal));
            DataColumn dc_ProjectCentageReceived_Date = new DataColumn("ProjectCentageReceived_Date", typeof(string));
            DataColumn dc_ProjectCentageReceived_LetterNo = new DataColumn("ProjectCentageReceived_LetterNo", typeof(string));

            dtCentageDetails.Columns.AddRange(new DataColumn[] { dc_ProjectCentageReceived_Id, dc_ProjectCentageReceived_Comments, dc_ProjectCentageReceived_Path, dc_ProjectCentageReceived_ProjectWork_Id, dc_ProjectCentageReceived_Amount, dc_ProjectCentageReceived_Date, dc_ProjectCentageReceived_LetterNo });

            DataRow dr = dtCentageDetails.NewRow();
            dtCentageDetails.Rows.Add(dr);
            ViewState["dtCentageDetails"] = dtCentageDetails;

            grdCentageDetails.DataSource = dtCentageDetails;
            grdCentageDetails.DataBind();
        }
    }

    protected void grdFinancialDoc_PreRender(object sender, EventArgs e)
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

    protected void grdFinancialDoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string filePathLetter = e.Row.Cells[2].Text.Trim().Replace("&nbsp;", "");
            string filePathExcel = e.Row.Cells[3].Text.Trim().Replace("&nbsp;", "");
            if (filePathLetter.Trim() != "")
            {
                e.Row.Cells[3].BackColor = Color.LightGreen;
            }
            else
            {
                LinkButton lnkDownloadLetter = (LinkButton)e.Row.FindControl("lnkDownloadLetter");
                lnkDownloadLetter.Visible = false;
            }

            if (filePathExcel.Trim() != "")
            {
                e.Row.Cells[3].BackColor = Color.LightGreen;
            }
            else
            {
                LinkButton lnkDownloadExcel = (LinkButton)e.Row.FindControl("lnkDownloadExcel");
                lnkDownloadExcel.Visible = false;
            }
        }
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string Month_Name_Before = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.AddMonths(-2).Month);
        string Year_Before = DateTime.Now.AddMonths(-2).Year.ToString();

        string Month_Name_Prev = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.AddMonths(-1).Month);
        string Year_Prev = DateTime.Now.AddMonths(-1).Year.ToString();

        string Month_Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
        string Year = DateTime.Now.Year.ToString();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblHInvTill") as Label).Text = "Invoice Till " + Month_Name_Before + " " + Year_Before + "";
            (e.Row.FindControl("lblHInvPrev") as Label).Text = "Invoice " + Month_Name_Prev + " " + Year_Prev + "";
            (e.Row.FindControl("lblHInvCurr") as Label).Text = "Invoice " + Month_Name + " " + Year + "";

            (e.Row.FindControl("lblHADPTill") as Label).Text = "Other Dept Till " + Month_Name_Before + " " + Year_Before + "";
            (e.Row.FindControl("lblHADPPrev") as Label).Text = "Other Dept " + Month_Name_Prev + " " + Year_Prev + "";
            (e.Row.FindControl("lblHADPCurr") as Label).Text = "Other Dept " + Month_Name + " " + Year + "";

            (e.Row.FindControl("lblHMATill") as Label).Text = "MA Till " + Month_Name_Before + " " + Year_Before + "";
            (e.Row.FindControl("lblHMAPrev") as Label).Text = "MA " + Month_Name_Prev + " " + Year_Prev + "";
            (e.Row.FindControl("lblHMACurr") as Label).Text = "MA " + Month_Name + " " + Year + "";

            (e.Row.FindControl("lblHDRTill") as Label).Text = "DR Till " + Month_Name_Before + " " + Year_Before + "";
            (e.Row.FindControl("lblHDRPrev") as Label).Text = "DR " + Month_Name_Prev + " " + Year_Prev + "";
            (e.Row.FindControl("lblHDRCurr") as Label).Text = "DR " + Month_Name + " " + Year + "";
        }
    }

    protected void grdADP_PreRender(object sender, EventArgs e)
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

    protected void grdMA_PreRender(object sender, EventArgs e)
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

    protected void grdDeductionRelease_PreRender(object sender, EventArgs e)
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

    protected void lnkOtherDocsI_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int UsageId = 0;
        string UsageType = "";
        try
        {
            UsageId = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            UsageId = 0;
        }
        UsageType = "I";

        if (UsageId > 0)
        {
            DataSet ds = new DataSet();
            ds = new DataLayer().get_tbl_Invoice_Document_Details(UsageType, UsageId);
            if (AllClasses.CheckDataSet(ds))
            {
                grdMultipleFiles.DataSource = ds.Tables[0];
                grdMultipleFiles.DataBind();
                mp1.Show();
            }
            else
            {
                grdMultipleFiles.DataSource = null;
                grdMultipleFiles.DataBind();
            }
        }
    }

    protected void lnkOtherDocsA_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int UsageId = 0;
        string UsageType = "";
        try
        {
            UsageId = Convert.ToInt32(gr.Cells[3].Text.Trim());
        }
        catch
        {
            UsageId = 0;
        }
        UsageType = "A";

        if (UsageId > 0)
        {
            DataSet ds = new DataSet();
            ds = new DataLayer().get_tbl_Invoice_Document_Details(UsageType, UsageId);
            if (AllClasses.CheckDataSet(ds))
            {
                grdMultipleFiles.DataSource = ds.Tables[0];
                grdMultipleFiles.DataBind();
                mp1.Show();
            }
            else
            {
                grdMultipleFiles.DataSource = null;
                grdMultipleFiles.DataBind();
            }
        }
    }

    protected void lnkOtherDocsM_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int UsageId = 0;
        string UsageType = "";
        try
        {
            UsageId = Convert.ToInt32(gr.Cells[3].Text.Trim());
        }
        catch
        {
            UsageId = 0;
        }
        UsageType = "M";

        if (UsageId > 0)
        {
            DataSet ds = new DataSet();
            ds = new DataLayer().get_tbl_Invoice_Document_Details(UsageType, UsageId);
            if (AllClasses.CheckDataSet(ds))
            {
                grdMultipleFiles.DataSource = ds.Tables[0];
                grdMultipleFiles.DataBind();
                mp1.Show();
            }
            else
            {
                grdMultipleFiles.DataSource = null;
                grdMultipleFiles.DataBind();
            }
        }
    }

    protected void lnkOtherDocsD_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int UsageId = 0;
        string UsageType = "";
        try
        {
            UsageId = Convert.ToInt32(gr.Cells[3].Text.Trim());
        }
        catch
        {
            UsageId = 0;
        }
        UsageType = "D";

        if (UsageId > 0)
        {
            DataSet ds = new DataSet();
            ds = new DataLayer().get_tbl_Invoice_Document_Details(UsageType, UsageId);
            if (AllClasses.CheckDataSet(ds))
            {
                grdMultipleFiles.DataSource = ds.Tables[0];
                grdMultipleFiles.DataBind();
                mp1.Show();
            }
            else
            {
                grdMultipleFiles.DataSource = null;
                grdMultipleFiles.DataBind();
            }
        }
    }

    protected void grdMultipleFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkDownload = (e.Row.FindControl("lnkDownload") as LinkButton);

            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkDownload);
        }
    }

    protected void rbtPhysicalClosureApplicable_SelectedIndexChanged(object sender, EventArgs e)
    {
        divHandoverNoteYes.Visible = false;
        divHandoverNoteNo.Visible = false;
        rbtHandoverNote.Items[0].Selected = false;
        rbtHandoverNote.Items[1].Selected = false;
        divHandOverDone.Visible = false;

        rbtHandOverDone.Items[0].Selected = false;
        rbtHandOverDone.Items[1].Selected = false;
        divHandOverDoneYes.Visible = false;
        divHandOverDoneNo.Visible = false;

        if (rbtPhysicalClosureApplicable.SelectedValue == "Y")
        {
            divHandoverNote.Visible = true;
        }
        else
        {
            divHandoverNote.Visible = false;
        }
    }

    protected void rbtHandoverNote_SelectedIndexChanged(object sender, EventArgs e)
    {
        rbtHandOverDone.Items[0].Selected = false;
        rbtHandOverDone.Items[1].Selected = false;
        divHandOverDoneYes.Visible = false;
        divHandOverDoneNo.Visible = false;
        if (rbtHandoverNote.SelectedValue == "Y")
        {
            divHandoverNoteYes.Visible = true;
            divHandoverNoteNo.Visible = false;
            divHandOverDone.Visible = true;
        }
        else
        {
            divHandoverNoteYes.Visible = false;
            divHandoverNoteNo.Visible = true;
            divHandOverDone.Visible = false;
            divHandOverDoneYes.Visible = false;
            divHandOverDoneNo.Visible = false;
        }
    }

    protected void rbtHandOverDone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtHandOverDone.SelectedValue == "Y")
        {
            divHandOverDoneYes.Visible = true;
            divHandOverDoneNo.Visible = false;
        }
        else
        {
            divHandOverDoneYes.Visible = false;
            divHandOverDoneNo.Visible = true;
        }
    }

    protected void rbtFinancialClosureApplicable_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtFinancialClosureApplicable.SelectedValue == "Y")
        {
            divFinancialClosureYes.Visible = true;
            divFinancialClosureNo.Visible = false;
        }
        else
        {
            divFinancialClosureYes.Visible = false;
            divFinancialClosureNo.Visible = true;
        }
    }

    protected void grdADP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[9].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[10].Text = Session["Default_Division"].ToString();
        }
    }

    protected void grdMA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[9].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[10].Text = Session["Default_Division"].ToString();
        }
    }

    protected void grdDeductionRelease_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[9].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[10].Text = Session["Default_Division"].ToString();
        }
    }
}
