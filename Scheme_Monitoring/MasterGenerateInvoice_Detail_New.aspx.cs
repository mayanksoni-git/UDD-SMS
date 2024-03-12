using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterGenerateInvoice_Detail_New : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.MasterPageFile = SetMasterPage.ReturnPage();
    }
    private void get_tbl_InvoiceStatus(int ConfigMasterId, int Scheme_Id)
    {
        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_InvoiceStatus(0, 0, 0, 0, "Invoice");
        }
        else
        {
            ds = (new DataLayer()).get_tbl_InvoiceStatus(Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), ConfigMasterId, "Invoice");
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlStatus, "InvoiceStatus_Name", "InvoiceStatus_Id");
        }
        else
        {
            ddlStatus.Items.Clear();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Person_Id"] == null || Session["Login_Id"] == null)
        {
            Response.Redirect("Index.aspx");
        }
        if (!IsPostBack)
        {
            divAmountTransfered.Visible = false;
            divPPA.Visible = false;
            divPPAMessage.Visible = false;
            chkFilter.Checked = true;
            int Package_Id = 0;
            int Invoice_Id = 0;
            int Scheme_Id = 0;
            if (Request.QueryString.Count > 0)
            {
                try
                {
                    Package_Id = Convert.ToInt32(Request.QueryString["Package_Id"].ToString());
                }
                catch
                {
                    Package_Id = 0;
                }
                try
                {
                    Scheme_Id = Convert.ToInt32(Request.QueryString["Scheme_Id"].ToString());
                }
                catch
                {
                    Scheme_Id = 0;
                }
                try
                {
                    Invoice_Id = Convert.ToInt32(Request.QueryString["Invoice_Id"].ToString());
                }
                catch
                {
                    Invoice_Id = 0;
                }
            }
            else
            {
                Invoice_Id = 0;
                Package_Id = 0;
                Scheme_Id = 0;
            }
            if (Invoice_Id > 0)
            {
                divEntry.Visible = true;
                hf_Invoice_Id.Value = Invoice_Id.ToString();
                get_tbl_Invoice_Details(Invoice_Id, Scheme_Id, true);
                get_ProcessConfig_Current(Invoice_Id, Scheme_Id);
            }
            else if (Package_Id > 0)
            {
                hf_Invoice_Id.Value = "0";
                get_tbl_PackageInvoice(Package_Id);
            }
            if (Session["PersonJuridiction_DesignationId"].ToString() == "16")
            {
                btnSaveInvoice.Visible = true;
            }
            else
            {
                btnSaveInvoice.Visible = false;
            }
            if (Scheme_Id == 1013 || Scheme_Id == 12 || Scheme_Id == 1016)
            {
                if (Session["PersonJuridiction_DesignationId"].ToString() == "33" || Session["PersonJuridiction_DesignationId"].ToString() == "34" || Session["PersonJuridiction_DesignationId"].ToString() == "1035")
                {
                    btnRevert.Visible = true;
                }
                else
                {
                    btnRevert.Visible = false;
                }
            }
            else
            {
                btnRevert.Visible = false;
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
    }
    protected void get_tbl_ProjectWork_Assign_SNA_Limit(int ProjectWork_Id, int Scheme_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_Assign_SNA_Limit(Scheme_Id.ToString(), 0, 0, 0, 0, ProjectWork_Id, "", 0, "");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            divBalance.InnerHtml = ds.Tables[0].Compute("sum(SNAAccountAvailableLimit)", "").ToString();
        }
        else
        {
            divBalance.InnerHtml = "0.00";
        }
    }
    private void get_ProcessConfig_Current(int PackageInvoice_Id, int Scheme_Id)
    {
        divAdditionalReason.Visible = false;
        if (Session["UserType"].ToString() == "1")
        {
            btnApproveInvoice.Visible = true;
        }
        else
        {
            DataSet ds = new DataSet();
            int _Loop = 0;
            if (Session["UserType"].ToString() == "1")
            {
                _Loop = (new DataLayer()).get_Loop("Invoice", 0, 0, Scheme_Id, null, null);
            }
            else
            {
                _Loop = (new DataLayer()).get_Loop("Invoice", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), Scheme_Id, null, null);
            }
            hf_Loop.Value = _Loop.ToString();
            hf_Scheme_Id.Value = Scheme_Id.ToString();
            ds = (new DataLayer()).get_ProcessConfig_Current(Scheme_Id, "Invoice", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), _Loop, PackageInvoice_Id);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int ConfigMaster_Id = 0;
                try
                {
                    ConfigMaster_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ProcessConfigMaster_Id"].ToString());
                    get_tbl_TradeDocument(ConfigMaster_Id);

                    get_tbl_InvoiceStatus(ConfigMaster_Id, Scheme_Id);
                }
                catch
                {
                    grdDocumentMaster.DataSource = null;
                    grdDocumentMaster.DataBind();
                    ConfigMaster_Id = 0;
                    get_tbl_InvoiceStatus(ConfigMaster_Id, Scheme_Id);
                }
                if (ds.Tables[0].Rows[0]["ProcessConfigMaster_Deduction_Allowed"].ToString() == "1")
                {
                    for (int i = 0; i < grdDeductions.Rows.Count; i++)
                    {
                        grdDeductions.Rows[i].Enabled = true;
                    }
                    for (int i = 0; i < grdDeductions2.Rows.Count; i++)
                    {
                        grdDeductions2.Rows[i].Enabled = true;
                    }
                }
                else
                {
                    for (int i = 0; i < grdDeductions.Rows.Count; i++)
                    {
                        grdDeductions.Rows[i].Enabled = false;
                    }
                    for (int i = 0; i < grdDeductions2.Rows.Count; i++)
                    {
                        grdDeductions2.Rows[i].Enabled = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["ProcessConfigMaster_Qty_Editing_Allowed"].ToString() == "1")
                {
                    for (int i = 0; i < grdBOQ.Rows.Count; i++)
                    {
                        TextBox txtQty = grdBOQ.Rows[i].FindControl("txtQty") as TextBox;
                        TextBox lblPercentageToBeReleased = grdBOQ.Rows[i].FindControl("lblPercentageToBeReleased") as TextBox;
                        txtQty.Enabled = true;
                        lblPercentageToBeReleased.Enabled = true;
                    }
                }
                else
                {
                    for (int i = 0; i < grdBOQ.Rows.Count; i++)
                    {
                        TextBox txtQty = grdBOQ.Rows[i].FindControl("txtQty") as TextBox;
                        TextBox lblPercentageToBeReleased = grdBOQ.Rows[i].FindControl("lblPercentageToBeReleased") as TextBox;
                        txtQty.Enabled = false;
                        lblPercentageToBeReleased.Enabled = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["ProcessConfigMaster_Transfer_Allowed"].ToString() == "1")
                {
                    divAmountTransfered.Visible = true;
                    divAdditionalReason.Visible = true;
                    if (Scheme_Id == 1013)
                    {
                        divPPA.Visible = true;
                        divPPAMessage.Visible = true;
                    }
                    else if (Scheme_Id == 1016)
                    {
                        divPPA.Visible = true;
                        divPPAMessage.Visible = true;
                    }
                    else
                    {
                        divPPA.Visible = false;
                        divPPAMessage.Visible = false;
                    }
                }
                else
                {
                    divAdditionalReason.Visible = false;
                    divAmountTransfered.Visible = false;
                    divPPA.Visible = false;
                    divPPAMessage.Visible = false;
                }
                if (ds.Tables[0].Rows[0]["ProcessConfigMaster_Document_Updation_Allowed"].ToString() == "1")
                {
                    chkUpdateExisting.Visible = true;
                    chkUpdateExisting.Checked = true;
                }
                else
                {
                    chkUpdateExisting.Visible = false;
                    chkUpdateExisting.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["ProcessConfigMaster_Creation_Allowed"].ToString() == "1")
                {
                    btnApproveInvoice.Visible = false;
                }
                if (ds.Tables[0].Rows[0]["ProcessConfigMaster_Updation_Allowed"].ToString() == "1")
                {
                    btnApproveInvoice.Visible = true;
                }
                else
                {
                    btnApproveInvoice.Visible = false;
                    if (PackageInvoice_Id > 0)
                    {
                        btnApproveInvoice.Visible = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < grdDeductions.Rows.Count; i++)
                {
                    grdDeductions.Rows[i].Enabled = false;
                }
                for (int i = 0; i < grdDeductions2.Rows.Count; i++)
                {
                    grdDeductions2.Rows[i].Enabled = false;
                }
                btnApproveInvoice.Visible = false;
            }
        }
    }

    private void get_tbl_TradeDocument(int ConfigMaster_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_TradeDocument(ConfigMaster_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdDocumentMaster.DataSource = ds.Tables[0];
            grdDocumentMaster.DataBind();
        }
        else
        {
            grdDocumentMaster.DataSource = null;
            grdDocumentMaster.DataBind();
        }
    }
    private void get_tbl_PackageInvoiceAdditional2(int Invoice_Id, string InvoiceFinalAmount)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageInvoiceAdditional_2(Invoice_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            grdDeductionsM.DataSource = ds.Tables[0];
            grdDeductionsM.DataBind();
            grdDeductionsM.FooterRow.Cells[7].Text = InvoiceFinalAmount;
        }
        else
        {
            grdDeductionsM.DataSource = null;
            grdDeductionsM.DataBind();
        }
    }

    private void get_tbl_PackageInvoice(int Package_Id)
    {
        bool? isDefered = null;
        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_PackageInvoice(Package_Id, 0, 0, 0, "", 0, 0, false, "", "", 0, 0, -1, false, -1, isDefered, "", 0, 0);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_PackageInvoice(Package_Id, 0, 0, 0, "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, "", "", 0, 0, -1, false, -1, isDefered, "", 0, 0);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdInvoice.DataSource = ds.Tables[0];
            grdInvoice.DataBind();

            if (grdInvoice.Rows.Count == 1)
            {
                ImageButton btnOpenInvoice = grdInvoice.Rows[0].FindControl("btnOpenInvoice") as ImageButton;
                btnOpenInvoice_Click(btnOpenInvoice, new ImageClickEventArgs(0, 0));
            }
        }
        else
        {
            MessageBox.Show("No Invoice Generated!!");
            return;
        }
    }

    protected void btnOpenInvoice_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Invoice_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        int Package_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        int Work_Id = Convert.ToInt32(gr.Cells[2].Text.Trim());
        int Scheme_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        hf_PackageInvoice_ProcessType.Value = gr.Cells[4].Text.Trim();
        string EMBMaster_Id = gr.Cells[17].Text.Trim();

        divEntry.Visible = true;
        hf_Invoice_Id.Value = Invoice_Id.ToString();
        for (int i = 0; i < grdInvoice.Rows.Count; i++)
        {
            grdInvoice.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        get_tbl_Invoice_Details(Invoice_Id, Scheme_Id, false);
        getPackage_Details(Package_Id);
        get_Project_Status(Work_Id, Package_Id);
        get_PackageEMB_ExtraItemApprove(EMBMaster_Id);
        if (Scheme_Id == 1013 || Scheme_Id == 1016)
        {
            get_tbl_ProjectWork_Assign_SNA_Limit(Work_Id);
        }
    }
    protected void get_tbl_ProjectWork_Assign_SNA_Limit(int ProjectWork_Id)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        string Scheme_Id = hf_Scheme_Id.Value; ;

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_Assign_SNA_Limit(Scheme_Id, 0, Zone_Id, Circle_Id, Division_Id, ProjectWork_Id, "", 0, "");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            divBalance.InnerHtml = ds.Tables[0].Compute("sum(SNAAccountAvailableLimitRs)", "").ToString();
            divAccountNo.InnerHtml = "<b>Account No: " + ds.Tables[0].Rows[0]["SNAAccountMaster_ACCT_NO"].ToString() + "</b>";
        }
        else
        {
            divBalance.InnerHtml = "0";
            divAccountNo.InnerHtml = "";
        }
    }
    private void getPackage_Details(int Package_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg(0, 0, 0, 0, 0, 0, Package_Id, "", "", false, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            hf_Doc_Agreement.Value = ds.Tables[0].Rows[0]["ProjectWorkPkg_Agreement_Path"].ToString();
            hf_Doc_BG.Value = ds.Tables[0].Rows[0]["ProjectWorkPkg_BankGurantee_Path"].ToString();
            hf_Doc_Mobelization.Value = ds.Tables[0].Rows[0]["ProjectWorkPkg_Mobelization_Path"].ToString();
            hf_Doc_PerformanceSecurity.Value = ds.Tables[0].Rows[0]["ProjectWorkPkg_PerformanceSecurity_Path"].ToString();
            grdPackageDetails.DataSource = ds.Tables[0];
            grdPackageDetails.DataBind();
        }
        else
        {
            hf_Doc_Agreement.Value = "";
            hf_Doc_BG.Value = "";
            hf_Doc_Mobelization.Value = "";
            hf_Doc_PerformanceSecurity.Value = "";
            grdPackageDetails.DataSource = null;
            grdPackageDetails.DataBind();
        }
    }
    private DataTable Filter_Invoice(DataSet ds)
    {
        DataView dv = new DataView(ds.Tables[1]);
        dv.RowFilter = "CurrentInvoice_Total_Amount <> 0";
        return dv.ToTable();
    }
    private void get_tbl_InvoiceItem(int Invoice_Id, DataSet ds)
    {
        //DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        if (ds == null)
        {
            ds = (new DataLayer()).get_tbl_Invoice_Details(Invoice_Id);
        }
        if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            if (chkFilter.Checked)
            {
                dt = Filter_Invoice(ds);
                if (dt != null && dt.Rows.Count > 0)
                {
                    grdBOQ.DataSource = dt;
                    grdBOQ.DataBind();

                    grdBOQ.FooterRow.Cells[22].Text = dt.Compute("sum(Total_Tax)", "").ToString();
                    grdBOQ.FooterRow.Cells[23].Text = dt.Compute("sum(CurrentInvoice_Total_Amount)", "").ToString();
                }
                else
                {
                    grdBOQ.DataSource = null;
                    grdBOQ.DataBind();
                }
            }
            else
            {
                grdBOQ.DataSource = ds.Tables[1];
                grdBOQ.DataBind();

                grdBOQ.FooterRow.Cells[22].Text = ds.Tables[1].Compute("sum(Total_Tax)", "").ToString();
                grdBOQ.FooterRow.Cells[23].Text = ds.Tables[1].Compute("sum(CurrentInvoice_Total_Amount)", "").ToString();

            }

        }
        else
        {
            grdBOQ.DataSource = null;
            grdBOQ.DataBind();
        }
    }
    private void get_tbl_Invoice_Details(int Invoice_Id, int Scheme_Id, bool bind_grdInvoice)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        int PackageEMB_Master_Id = 0;
        string InvoiceFinal_Amount = "0";
        ds = (new DataLayer()).get_tbl_Invoice_Details(Invoice_Id);
        if (ds != null)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                PackageEMB_Master_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["PackageInvoice_PackageEMBMaster_Id"].ToString());
                InvoiceFinal_Amount = ds.Tables[0].Rows[0]["InvoiceAmount"].ToString();
                if (ds.Tables[0].Rows[0]["PackageInvoice_ProcessedBy"].ToString().Trim().Replace("&nbsp;", "") == "")
                {
                    btnApproveInvoice.Visible = false;
                }
                else
                {
                    btnApproveInvoice.Visible = true;
                }
            }
            get_tbl_InvoiceItem(Invoice_Id, ds);
            //if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            //{
            //    grdBOQ.DataSource = ds.Tables[1];
            //    grdBOQ.DataBind();

            //    grdBOQ.FooterRow.Cells[22].Text = ds.Tables[1].Compute("sum(Total_Tax)", "").ToString();
            //    grdBOQ.FooterRow.Cells[23].Text = ds.Tables[1].Compute("sum(CurrentInvoice_Total_Amount)", "").ToString();
            //}
            //else
            //{
            //    grdBOQ.DataSource = null;
            //    grdBOQ.DataBind();
            //}
            if (ds.Tables.Count > 4 && ds.Tables[4].Rows.Count > 0)
            {
                grdMultipleFiles.DataSource = ds.Tables[4];
                grdMultipleFiles.DataBind();
            }
            else
            {
                grdMultipleFiles.DataSource = null;
                grdMultipleFiles.DataBind();
            }
            if (bind_grdInvoice)
            {
                if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                {
                    grdInvoice.DataSource = ds.Tables[2];
                    grdInvoice.DataBind();

                    if (grdInvoice.Rows.Count == 1)
                    {
                        ImageButton btnOpenInvoice = grdInvoice.Rows[0].FindControl("btnOpenInvoice") as ImageButton;
                        btnOpenInvoice_Click(btnOpenInvoice, new ImageClickEventArgs(0, 0));
                    }
                }
                else
                {
                    grdInvoice.DataSource = null;
                    grdInvoice.DataBind();
                }
            }
        }
        else
        {
            MessageBox.Show("Server Error!!");
            return;
        }
        get_tbl_PackageInvoiceAdditional2(Invoice_Id, InvoiceFinal_Amount);
        get_tbl_Deduction(Invoice_Id, PackageEMB_Master_Id);
        Calculate_Total();
        get_ProcessConfig_Current(Invoice_Id, Scheme_Id);
    }

    private void Calculate_Total()
    {
        decimal invoice_total_deduction = 0;
        string ProcessType = "Normal";
        decimal DeductionValue = 0;
        decimal DeductionValueDD = 0;
        decimal SubTotal = 0;
        decimal SubTotal_GST = 0;
        int _index_Total = -1;

        CheckBox chkSelect = grdDeductionsM.Rows[0].FindControl("chkSelect") as CheckBox;
        TextBox txtDeductionValue = grdDeductionsM.Rows[0].FindControl("txtDeductionValue") as TextBox;
        if (chkSelect.Checked)
        {
            ProcessType = "Global";
            try
            {
                DeductionValue = decimal.Parse(txtDeductionValue.Text);
            }
            catch
            {
                DeductionValue = 0;
            }
            _index_Total = 0;
        }

        CheckBox chkSelectDD = grdDeductionsM.Rows[1].FindControl("chkSelect") as CheckBox;
        TextBox txtDeductionValueDD = grdDeductionsM.Rows[1].FindControl("txtDeductionValue") as TextBox;
        if (chkSelectDD.Checked)
        {
            ProcessType = "Global";
            try
            {
                DeductionValueDD = decimal.Parse(txtDeductionValueDD.Text);
            }
            catch
            {
                DeductionValueDD = 0;
            }
            _index_Total = 1;
        }
        hf_PackageInvoice_ProcessType.Value = ProcessType;

        decimal _GST_P = 0;
        for (int i = 0; i < grdBOQ.Rows.Count; i++)
        {
            decimal Rate_Estimated = 0;
            decimal Rate_Quoted = 0;
            decimal Qty_Previous = 0;
            decimal Amount_Previous_With_GST = 0;
            decimal Amount_Previous = 0;
            decimal Amount_Previous_GST = 0;

            decimal Qty_UptoDate = 0;
            decimal Amount_UpToDate = 0;

            decimal Qty_Current = 0;
            decimal Amount_Current = 0;

            decimal _GST_V = 0;

            decimal _CGST = 0;
            decimal _SGST = 0;

            decimal Percentage = 0;

            string GST_Type = grdBOQ.Rows[i].Cells[7].Text.Trim();
            TextBox txtQty = grdBOQ.Rows[i].FindControl("txtQty") as TextBox;
            TextBox lblPercentageToBeReleased = grdBOQ.Rows[i].FindControl("lblPercentageToBeReleased") as TextBox;
            if (txtQty.Text.Trim() != "")
            {
                try
                {
                    Qty_UptoDate = decimal.Parse(txtQty.Text.Trim());
                }
                catch
                {

                }
                try
                {
                    Percentage = decimal.Parse(lblPercentageToBeReleased.Text.Trim());
                }
                catch
                {

                }


                try
                {
                    Rate_Estimated = decimal.Parse(grdBOQ.Rows[i].Cells[14].Text.Trim());
                }
                catch
                {

                }
                try
                {
                    Rate_Quoted = decimal.Parse(grdBOQ.Rows[i].Cells[15].Text.Trim());
                }
                catch
                {

                }
                try
                {
                    _GST_P = decimal.Parse(grdBOQ.Rows[i].Cells[8].Text.Trim());
                }
                catch
                {

                }
                try
                {
                    Qty_Previous = decimal.Parse(grdBOQ.Rows[i].Cells[16].Text.Trim());
                }
                catch
                {

                }
                try
                {
                    Amount_Previous_With_GST = decimal.Parse(grdBOQ.Rows[i].Cells[17].Text.Trim());
                }
                catch
                {

                }
                try
                {
                    Amount_Previous_GST = decimal.Parse(grdBOQ.Rows[i].Cells[25].Text.Trim());
                }
                catch
                {

                }
                if (GST_Type == "Include GST")
                {
                    try
                    {
                        Amount_Previous = Amount_Previous_With_GST;  //- Amount_Previous_GST;
                    }
                    catch
                    {

                    }
                }
                else
                {
                    try
                    {
                        Amount_Previous = Amount_Previous_With_GST;
                    }
                    catch
                    {

                    }
                }
                if (ProcessType == "Global")
                {
                    Amount_UpToDate = decimal.Round((Rate_Estimated * Qty_UptoDate * Percentage) / 100, 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    Amount_UpToDate = decimal.Round((Rate_Quoted * Qty_UptoDate * Percentage) / 100, 2, MidpointRounding.AwayFromZero);
                }

                Qty_Current = Qty_UptoDate - Qty_Previous;
                Amount_Current = Amount_UpToDate - Amount_Previous;

                _GST_V = decimal.Round((Amount_Current * _GST_P) / 100, 2, MidpointRounding.AwayFromZero);

                if (ProcessType == "Global")
                {
                    _CGST = 0;
                    _SGST = 0;
                }
                else
                {
                    _CGST = _GST_V / 2;
                    _SGST = _GST_V / 2;
                }

                grdBOQ.Rows[i].Cells[20].Text = Amount_UpToDate.ToString();
                grdBOQ.Rows[i].Cells[21].Text = Qty_Current.ToString();
                if (ProcessType == "Global")
                {
                    grdBOQ.Rows[i].Cells[22].Text = "";
                }
                else
                {
                    grdBOQ.Rows[i].Cells[22].Text = _GST_V.ToString();
                }
                grdBOQ.Rows[i].Cells[23].Text = Amount_Current.ToString();

                SubTotal += Amount_Current;
                if (ProcessType == "Global")
                {
                    SubTotal_GST = 0;
                }
                else
                {
                    SubTotal_GST += _GST_V;
                }
            }
        }
        if (grdBOQ.FooterRow != null)
        {
            grdBOQ.FooterRow.Cells[22].Text = SubTotal_GST.ToString();
            grdBOQ.FooterRow.Cells[23].Text = SubTotal.ToString();
        }
        if (ProcessType == "Global")
        {
            grdDeductionsM.Rows[0].Cells[7].Text = decimal.Round(SubTotal + ((SubTotal * DeductionValue) / 100), 0, MidpointRounding.AwayFromZero).ToString();

            if (chkSelectDD.Checked)
            {
                decimal _amount = decimal.Parse(grdDeductionsM.Rows[0].Cells[7].Text);
                grdDeductionsM.Rows[1].Cells[7].Text = decimal.Round(_amount - ((_amount * DeductionValueDD) / 100), 0, MidpointRounding.AwayFromZero).ToString();

                grdDeductionsM.Rows[2].Cells[7].Text = decimal.Round((decimal.Parse(grdDeductionsM.Rows[1].Cells[7].Text) * (_GST_P / 2)) / 100, 0, MidpointRounding.AwayFromZero).ToString();
                (grdDeductionsM.Rows[2].FindControl("chkSelect") as CheckBox).Checked = true;
                (grdDeductionsM.Rows[2].FindControl("txtDeductionValue") as TextBox).Text = (_GST_P / 2).ToString();
                grdDeductionsM.Rows[3].Cells[7].Text = decimal.Round((decimal.Parse(grdDeductionsM.Rows[1].Cells[7].Text) * (_GST_P / 2)) / 100, 0, MidpointRounding.AwayFromZero).ToString();
                (grdDeductionsM.Rows[3].FindControl("chkSelect") as CheckBox).Checked = true;
                (grdDeductionsM.Rows[3].FindControl("txtDeductionValue") as TextBox).Text = (_GST_P / 2).ToString();

                grdDeductionsM.FooterRow.Cells[7].Text = (decimal.Parse(grdDeductionsM.Rows[1].Cells[7].Text) + decimal.Parse(grdDeductionsM.Rows[2].Cells[7].Text) + decimal.Parse(grdDeductionsM.Rows[3].Cells[7].Text)).ToString();
            }
            else
            {
                (grdDeductionsM.Rows[2].FindControl("chkSelect") as CheckBox).Checked = true;
                (grdDeductionsM.Rows[2].FindControl("txtDeductionValue") as TextBox).Text = (_GST_P / 2).ToString();
                grdDeductionsM.Rows[2].Cells[7].Text = decimal.Round((decimal.Parse(grdDeductionsM.Rows[0].Cells[7].Text) * (_GST_P / 2)) / 100, 0, MidpointRounding.AwayFromZero).ToString();
                (grdDeductionsM.Rows[3].FindControl("chkSelect") as CheckBox).Checked = true;
                (grdDeductionsM.Rows[3].FindControl("txtDeductionValue") as TextBox).Text = (_GST_P / 2).ToString();
                grdDeductionsM.Rows[3].Cells[7].Text = decimal.Round((decimal.Parse(grdDeductionsM.Rows[0].Cells[7].Text) * (_GST_P / 2)) / 100, 0, MidpointRounding.AwayFromZero).ToString();

                grdDeductionsM.FooterRow.Cells[7].Text = (decimal.Parse(grdDeductionsM.Rows[0].Cells[7].Text) + decimal.Parse(grdDeductionsM.Rows[2].Cells[7].Text) + decimal.Parse(grdDeductionsM.Rows[3].Cells[7].Text)).ToString();
            }
        }

        decimal invoice_total = 0;

        if (ProcessType == "Global")
        {
            if (grdDeductionsM.FooterRow != null)
            {
                try
                {
                    invoice_total = decimal.Parse(grdDeductionsM.FooterRow.Cells[7].Text.Trim());
                }
                catch
                {
                    invoice_total = 0;
                }
            }

            try
            {
                invoice_total_deduction = decimal.Parse(grdDeductionsM.Rows[_index_Total].Cells[7].Text.Trim());
            }
            catch
            {
                invoice_total_deduction = 0;
            }
        }
        else
        {
            if (grdBOQ.FooterRow != null)
            {
                decimal tax = 0;
                decimal amount = 0;
                try
                {
                    tax = decimal.Parse(grdBOQ.FooterRow.Cells[22].Text.Trim());
                }
                catch
                {
                    tax = 0;
                }
                try
                {
                    amount = decimal.Parse(grdBOQ.FooterRow.Cells[23].Text.Trim());
                }
                catch
                {
                    amount = 0;
                }
                invoice_total = amount + tax;
                invoice_total_deduction = amount;
            }
        }

        decimal DeductionValue_Total = 0;
        TextBox txtDeductionAmount = null;
        for (int i = 0; i < grdDeductions.Rows.Count; i++)
        {
            chkSelect = grdDeductions.Rows[i].FindControl("chkSelect") as CheckBox;
            txtDeductionAmount = grdDeductions.Rows[i].FindControl("txtDeductionAmount") as TextBox;
            txtDeductionValue = grdDeductions.Rows[i].FindControl("txtDeductionValue") as TextBox;
            CheckBox chkFlat = grdDeductions.Rows[i].FindControl("chkFlat") as CheckBox;
            if (chkSelect.Checked && !chkFlat.Checked)
            {
                DeductionValue = 0;
                try
                {
                    DeductionValue = Convert.ToDecimal(txtDeductionValue.Text.Trim());
                }
                catch
                {
                    DeductionValue = 0;
                }
                try
                {
                    txtDeductionAmount.Text = decimal.Round(((invoice_total_deduction * DeductionValue) / 100), 0, MidpointRounding.AwayFromZero).ToString();
                }
                catch
                {
                    txtDeductionAmount.Text = "0";
                }
            }
            else if (chkFlat.Checked && chkSelect.Checked)
            {
                decimal invoice_Total = 0;
                decimal DeductionAmount = 0;

                try
                {
                    invoice_Total = invoice_total_deduction;
                }
                catch
                {
                    invoice_Total = 0;
                }

                try
                {
                    DeductionAmount = Convert.ToDecimal(txtDeductionAmount.Text.Trim());
                }
                catch
                {
                    DeductionAmount = 0;
                }
                if (DeductionAmount > 0)
                {
                    try
                    {
                        txtDeductionValue.Text = decimal.Round((DeductionAmount * 100) / invoice_Total, 0, MidpointRounding.AwayFromZero).ToString();
                    }
                    catch
                    { }
                }
            }
            else
            {
                txtDeductionAmount.Text = "0";
            }

            txtDeductionAmount = grdDeductions.Rows[i].FindControl("txtDeductionAmount") as TextBox;
            try
            {
                DeductionValue_Total += Convert.ToDecimal(txtDeductionAmount.Text.Trim());
            }
            catch
            {
                DeductionValue_Total += 0;
            }
        }
        if (grdDeductions.FooterRow != null)
        {
            grdDeductions.FooterRow.Cells[10].Text = DeductionValue_Total.ToString();
        }
        decimal DeductionValue_Total2 = 0;
        for (int i = 0; i < grdDeductions2.Rows.Count; i++)
        {
            CheckBox chkSelect2 = grdDeductions2.Rows[i].FindControl("chkSelect2") as CheckBox;
            TextBox txtDeductionAmount2 = grdDeductions2.Rows[i].FindControl("txtDeductionAmount2") as TextBox;
            TextBox txtDeductionValue2 = grdDeductions2.Rows[i].FindControl("txtDeductionValue2") as TextBox;
            CheckBox chkFlat2 = grdDeductions2.Rows[i].FindControl("chkFlat2") as CheckBox;
            if (chkSelect2.Checked && !chkFlat2.Checked)
            {
                decimal DeductionValue2 = 0;
                try
                {
                    DeductionValue2 = Convert.ToDecimal(txtDeductionValue2.Text.Trim());
                }
                catch
                {
                    DeductionValue2 = 0;
                }
                try
                {
                    txtDeductionAmount2.Text = decimal.Round(((invoice_total_deduction * DeductionValue2) / 100), 0, MidpointRounding.AwayFromZero).ToString();
                }
                catch
                {
                    txtDeductionAmount2.Text = "0";
                }
            }
            else if (chkFlat2.Checked && chkSelect2.Checked)
            {
                decimal invoice_Total2 = 0;
                decimal DeductionAmount2 = 0;

                try
                {
                    invoice_Total2 = invoice_total_deduction;
                }
                catch
                {
                    invoice_Total2 = 0;
                }

                try
                {
                    DeductionAmount2 = Convert.ToDecimal(txtDeductionAmount2.Text.Trim());
                }
                catch
                {
                    DeductionAmount2 = 0;
                }
                if (DeductionAmount2 > 0)
                {
                    txtDeductionValue2.Text = decimal.Round((DeductionAmount2 * 100) / invoice_Total2, 0, MidpointRounding.AwayFromZero).ToString();
                }
            }
            else
            {
                txtDeductionAmount2.Text = "0";
            }

            txtDeductionAmount2 = grdDeductions2.Rows[i].FindControl("txtDeductionAmount2") as TextBox;
            try
            {
                DeductionValue_Total2 += Convert.ToDecimal(txtDeductionAmount2.Text.Trim());
            }
            catch
            {
                DeductionValue_Total2 += 0;
            }
        }
        if (grdDeductions2.FooterRow != null)
        {
            grdDeductions2.FooterRow.Cells[10].Text = DeductionValue_Total2.ToString();
        }

        decimal deduction_Value = 0;
        decimal addition_Value = 0;

        if (grdDeductions.FooterRow != null)
        {
            try
            {
                deduction_Value = decimal.Parse(grdDeductions.FooterRow.Cells[10].Text.Trim());
            }
            catch
            {
                deduction_Value = 0;
            }
        }
        if (grdDeductions2.FooterRow != null)
        {
            try
            {
                addition_Value = decimal.Parse(grdDeductions2.FooterRow.Cells[10].Text.Trim());
            }
            catch
            {
                addition_Value = 0;
            }
        }

        DeductionValue_Total = 0;
        txtDeductionAmount = null; ;
        for (int i = 0; i < grdDeductions.Rows.Count; i++)
        {
            string deduction_Type = grdDeductions.Rows[i].Cells[6].Text.Trim();
            if (deduction_Type == "Statutory Deductions")
            {

                txtDeductionAmount = grdDeductions.Rows[i].FindControl("txtDeductionAmount") as TextBox;
                try
                {
                    DeductionValue_Total += Convert.ToDecimal(txtDeductionAmount.Text.Trim());
                }
                catch
                {
                }
                DeductionValue_Total += 0;
            }
        }
        lblTotalAmount.Text = decimal.Round(invoice_total - deduction_Value + addition_Value, 0, MidpointRounding.AwayFromZero).ToString();
        txtFudTransfered.Text = lblTotalAmount.Text;
    }

    protected void grdInvoice_PreRender(object sender, EventArgs e)
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
    protected void grdBOQ_PreRender(object sender, EventArgs e)
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

    protected void grdBOQ_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }
    protected void grdDeductionsM_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int PackageEMBAdditional_Id = 0;
            try
            {
                PackageEMBAdditional_Id = Convert.ToInt32(e.Row.Cells[1].Text.Trim());
            }
            catch
            {
                PackageEMBAdditional_Id = 0;
            }
            if (PackageEMBAdditional_Id > 0)
            {
                (e.Row.FindControl("chkSelect") as CheckBox).Checked = true;
            }
        }
    }

    protected void grdDeductions_PreRender(object sender, EventArgs e)
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
    private DataTable Filter_Deduction(DataSet ds, string Deduction_Mode)
    {
        DataView dv = new DataView(ds.Tables[0]);
        dv.RowFilter = "Deduction_Mode='" + Deduction_Mode + "'";
        return dv.ToTable();
    }
    private void get_tbl_Deduction(int Invoice_Id, int PackageEMB_Master_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Deduction(Invoice_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataTable dt = Filter_Deduction(ds, "-");
            if (AllClasses.CheckDt(dt))
            {
                grdDeductions.DataSource = dt;
                grdDeductions.DataBind();
                ViewState["grdDeductions"] = dt;

                decimal DeductionValue_Total = 0;
                TextBox txtDeductionAmount;
                for (int i = 0; i < grdDeductions.Rows.Count; i++)
                {
                    txtDeductionAmount = grdDeductions.Rows[i].FindControl("txtDeductionAmount") as TextBox;
                    try
                    {
                        DeductionValue_Total += Convert.ToDecimal(txtDeductionAmount.Text.Trim());
                    }
                    catch
                    {
                        DeductionValue_Total += 0;
                    }
                }
                grdDeductions.FooterRow.Cells[10].Text = DeductionValue_Total.ToString();
            }
            else
            {
                grdDeductions.DataSource = null;
                grdDeductions.DataBind();
            }
            dt = new DataTable();
            dt = Filter_Deduction(ds, "/");
            if (AllClasses.CheckDt(dt))
            {
                grdDeductions2.DataSource = dt;
                grdDeductions2.DataBind();

                decimal DeductionValue_Total2 = 0;
                TextBox txtDeductionAmount2;
                for (int i = 0; i < grdDeductions2.Rows.Count; i++)
                {
                    txtDeductionAmount2 = grdDeductions2.Rows[i].FindControl("txtDeductionAmount2") as TextBox;
                    try
                    {
                        DeductionValue_Total2 += Convert.ToDecimal(txtDeductionAmount2.Text.Trim());
                    }
                    catch
                    {
                        DeductionValue_Total2 += 0;
                    }
                }
                grdDeductions2.FooterRow.Cells[10].Text = DeductionValue_Total2.ToString();
            }
            else
            {
                grdDeductions2.DataSource = null;
                grdDeductions2.DataBind();
            }
        }
        else
        {
            grdDeductions.DataSource = null;
            grdDeductions.DataBind();

            grdDeductions2.DataSource = null;
            grdDeductions2.DataBind();
        }
    }

    protected void grdDeductions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkSelect = e.Row.FindControl("chkSelect") as CheckBox;
            CheckBox chkFlat = e.Row.FindControl("chkFlat") as CheckBox;
            int PackageInvoiceAdditional_Id = 0;
            try
            {
                PackageInvoiceAdditional_Id = Convert.ToInt32(e.Row.Cells[1].Text.Trim());
            }
            catch
            {
                PackageInvoiceAdditional_Id = 0;
            }
            string Deduction_Type = e.Row.Cells[3].Text.Trim();
            if (Deduction_Type == "Percentage")
            {
                chkFlat.Checked = false;
            }
            else
            {
                chkFlat.Checked = true;
            }

            int Deduction_isFlat = 0;
            try
            {
                Deduction_isFlat = Convert.ToInt32(e.Row.Cells[2].Text.Trim());
            }
            catch
            {
                Deduction_isFlat = 0;
            }
            if (Deduction_isFlat == 1)
            {
                chkFlat.Checked = true;
            }
            else
            {
                chkFlat.Checked = false;
            }

            if (PackageInvoiceAdditional_Id > 0)
            {
                chkSelect.Checked = true;
                chkSelect_CheckedChanged(chkSelect, e);
            }
            else
            {
                chkSelect.Checked = false;
            }
        }
    }
    private void get_Project_Status(int Work_Id, int Package_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Project_Status(Work_Id, Package_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            grdProjectStatus.DataSource = ds.Tables[0];
            grdProjectStatus.DataBind();
        }
        else
        {
            grdProjectStatus.DataSource = null;
            grdProjectStatus.DataBind();
        }
    }

    protected void grdProjectStatus_PreRender(object sender, EventArgs e)
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

    protected void btnApproveInvoice_Click(object sender, EventArgs e)
    {
        Save_Data("Approve");
    }

    private void Save_Data(string Mode)
    {
        decimal Project_Total_Agreement_Amount = 0;
        decimal Invoice_Total_Amount = 0;
        try
        {
            Project_Total_Agreement_Amount = decimal.Parse(grdProjectStatus.Rows[1].Cells[5].Text.Trim());
        }
        catch
        {
            Project_Total_Agreement_Amount = 0;
        }
        try
        {
            Invoice_Total_Amount = decimal.Parse(grdProjectStatus.Rows[1].Cells[11].Text.Trim());
        }
        catch
        {
            Invoice_Total_Amount = 0;
        }
        decimal Total_Amount = 0;
        decimal Transfred_Amount = 0;
        decimal PPA_Amount = 0;
        string PPA_Status = "";
        string Debit_Account_No = "";
        if (divAmountTransfered.Visible)
        {
            try
            {
                Transfred_Amount = decimal.Parse(txtFudTransfered.Text.Trim());
            }
            catch
            {
                Transfred_Amount = 0;
            }
            if (Transfred_Amount == 0)
            {
                MessageBox.Show("Transfred Amount Should Be More Than Zero.");
                return;
            }

            try
            {
                Total_Amount = decimal.Parse(lblTotalAmount.Text.Trim());
            }
            catch
            {
                Total_Amount = 0;
            }
            //if (Transfred_Amount > Total_Amount)
            //{
            //    MessageBox.Show("Transfred Amount Should Be Less or Equal To Total Amount.");
            //    return;
            //}
        }
        if (divPPA.Visible)
        {
            if (hf_Scheme_Id.Value == "1013" || hf_Scheme_Id.Value == "1016")
            {
                if (txtPPANo.Text.Trim() == "")
                {
                    MessageBox.Show("Please Input A Valid PPA No.");
                    txtPPANo.Focus();
                    return;
                }
                if (divPPAVerification.Visible == false)
                {
                    MessageBox.Show("Please Provide A Valid PPA No");
                    txtPPANo.Focus();
                    return;
                }
                if (grdPPAVerification.Rows.Count == 0)
                {
                    MessageBox.Show("Please Provide A Valid PPA No");
                    txtPPANo.Focus();
                    return;
                }
                try
                {
                    PPA_Amount = decimal.Parse(grdPPAVerification.Rows[0].Cells[1].Text.Trim());
                }
                catch
                {
                    PPA_Amount = 0;
                }
                try
                {
                    PPA_Status = grdPPAVerification.Rows[0].Cells[5].Text.Trim();
                }
                catch
                {
                    PPA_Status = "";
                }
                try
                {
                    Debit_Account_No = grdPPAVerification.Rows[0].Cells[2].Text.Trim();
                }
                catch
                {
                    Debit_Account_No = "";
                }
                string _Debit_Account_No = divAccountNo.InnerHtml.Replace("<b>", "").Replace("</b>", "").Replace("Account No:", "").Trim();
                if (_Debit_Account_No != Debit_Account_No)
                {
                    MessageBox.Show("Provided PPA is not Linked With The Accounr Linked With The Project. Please Upload Correct PPA");
                    txtPPANo.Focus();
                    return;
                }
                if (PPA_Status != "Success")
                {
                    MessageBox.Show("Provided PPA is not Cleared From PNB. Please Upload Correct PPA");
                    txtPPANo.Focus();
                    return;
                }
                if (Transfred_Amount != PPA_Amount)
                {
                    MessageBox.Show("Amount Of PPA and Invoice Amount is Mismatch. Please Upload Correct PPA");
                    txtPPANo.Focus();
                    return;
                }
            }
        }
        if (Mode == "Approve")
        {
            if (ddlStatus.SelectedValue == "0" || ddlStatus.SelectedValue == "")
            {
                MessageBox.Show("Please Select Status");
                return;
            }
            if (ddlStatus.SelectedValue == "2" || ddlStatus.SelectedValue == "3")
            {
                if (divAmountTransfered.Visible)
                {
                    if (ddlAdditionalReason.SelectedValue == "0" && Session["PersonJuridiction_DesignationId"].ToString() == "33")
                    {
                        MessageBox.Show("Please Select Deferred Reason");
                        ddlAdditionalReason.Focus();
                        return;
                    }
                }
                if (txtComments.Text.Trim() == "")
                {
                    MessageBox.Show("Please Provide Comments / Reason");
                    return;
                }
            }
        }
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(grdInvoice.Rows[0].Cells[3].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        int ProjectWork_Id = 0;
        try
        {
            ProjectWork_Id = Convert.ToInt32(grdInvoice.Rows[0].Cells[2].Text.Trim());
        }
        catch
        {
            ProjectWork_Id = 0;
        }
        if (grdDeductions.Rows[0].Enabled == true)
        {
            decimal deduction = 0;
            try
            {
                deduction = decimal.Parse(grdDeductions.FooterRow.Cells[10].Text.Trim());
            }
            catch
            {
                deduction = 0;
            }
            if (ddlStatus.SelectedValue == "1" || ddlStatus.SelectedValue == "6" || ddlStatus.SelectedValue == "4" || ddlStatus.SelectedValue == "5")
            {
                if (deduction == 0)
                {
                    MessageBox.Show("Please Add Deductions!!");
                    return;
                }
            }
        }
        
        List<tbl_PackageInvoiceItem_Tax> obj_tbl_PackageInvoiceItem_Tax_Li = new List<tbl_PackageInvoiceItem_Tax>();
        tbl_PackageInvoiceItem_Tax obj_tbl_PackageInvoiceItem_Tax = new tbl_PackageInvoiceItem_Tax();

        tbl_PackageInvoice obj_tbl_PackageInvoice = new tbl_PackageInvoice();
        obj_tbl_PackageInvoice.PackageInvoice_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PackageInvoice.PackageInvoice_ApprovedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PackageInvoice.PackageInvoice_Id = Convert.ToInt32(grdBOQ.Rows[0].Cells[1].Text.Trim());
        obj_tbl_PackageInvoice.PackageInvoice_Status = 1;
        obj_tbl_PackageInvoice.PackageInvoice_Type = grdInvoice.Rows[0].Cells[22].Text.Trim();
        try
        {
            obj_tbl_PackageInvoice.DeducctionAmount = Convert.ToDecimal(grdDeductions.FooterRow.Cells[10].Text.Trim());
            obj_tbl_PackageInvoice.PackageInvoice_DeductionAmount = obj_tbl_PackageInvoice.DeducctionAmount.ToString();
        }
        catch
        { }
        obj_tbl_PackageInvoice.PackageInvoice_ProcessType = hf_PackageInvoice_ProcessType.Value;
        List<tbl_PackageInvoiceItem> obj_tbl_PackageInvoiceItem_Li = new List<tbl_PackageInvoiceItem>();
        List<tbl_PackageEMB> obj_tbl_PackageEMB_Li = new List<tbl_PackageEMB>();
        for (int i = 0; i < grdBOQ.Rows.Count; i++)
        {
            decimal Qty_Since_Previous = 0, PackageInvoice_Total_Amount = 0;
            try
            {
                Qty_Since_Previous = decimal.Parse(grdBOQ.Rows[i].Cells[16].Text.Trim());
            }
            catch
            {
                Qty_Since_Previous = 0;
            }
            decimal Total_Qty = 0;
            try
            {
                Total_Qty = Convert.ToDecimal((grdBOQ.Rows[i].Cells[13].Text));
            }
            catch
            {
                Total_Qty = 0;
            }

            TextBox txtRemarks = grdBOQ.Rows[i].FindControl("txtRemarks") as TextBox;
            TextBox txtQty = grdBOQ.Rows[i].FindControl("txtQty") as TextBox;
            TextBox lblPercentageToBeReleased = grdBOQ.Rows[i].FindControl("lblPercentageToBeReleased") as TextBox;
            tbl_PackageInvoiceItem obj_tbl_PackageInvoiceItem = new tbl_PackageInvoiceItem();
            tbl_PackageEMB obj_tbl_PackageEMB = new tbl_PackageEMB();
            obj_tbl_PackageInvoiceItem.PackageInvoiceItem_Id = Convert.ToInt32(grdBOQ.Rows[i].Cells[0].Text.Trim());
            try
            {
                obj_tbl_PackageInvoiceItem.PackageInvoiceItem_UpToDate = decimal.Parse(txtQty.Text.Trim());
                obj_tbl_PackageEMB.PackageEMB_Qty_UpToDate = decimal.Parse(txtQty.Text.Trim());
            }
            catch
            {
                obj_tbl_PackageInvoiceItem.PackageInvoiceItem_UpToDate = 0;
                obj_tbl_PackageEMB.PackageEMB_Qty_UpToDate = 0;
            }
            try
            {
                obj_tbl_PackageInvoiceItem.PackageInvoiceItem_Total_Qty_BOQ = obj_tbl_PackageInvoiceItem.PackageInvoiceItem_UpToDate - Qty_Since_Previous;
                obj_tbl_PackageEMB.PackageEMB_Qty = obj_tbl_PackageInvoiceItem.PackageInvoiceItem_UpToDate - Qty_Since_Previous;
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_Qty = 0;
                obj_tbl_PackageInvoiceItem.PackageInvoiceItem_Total_Qty_BOQ = 0;
            }
            try
            {
                obj_tbl_PackageEMB.PackageEMB_PercentageToBeReleased = Convert.ToDecimal(lblPercentageToBeReleased.Text.Trim());
                obj_tbl_PackageInvoiceItem.PackageInvoiceItem_PercentageToBeReleased = Convert.ToDecimal(lblPercentageToBeReleased.Text.Trim());
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_PercentageToBeReleased = 0;
                obj_tbl_PackageInvoiceItem.PackageInvoiceItem_PercentageToBeReleased = 0;
            }
            obj_tbl_PackageInvoiceItem.PackageInvoiceItem_QtyExtra = Total_Qty - obj_tbl_PackageInvoiceItem.PackageInvoiceItem_UpToDate;
            obj_tbl_PackageEMB.PackageEMB_QtyExtra = Total_Qty - obj_tbl_PackageInvoiceItem.PackageInvoiceItem_UpToDate;
            if (obj_tbl_PackageInvoiceItem.PackageInvoiceItem_QtyExtra > 0)
            {
                obj_tbl_PackageEMB.PackageEMB_QtyExtra = 0;
                obj_tbl_PackageInvoiceItem.PackageInvoiceItem_QtyExtra = 0;
            }
            obj_tbl_PackageInvoiceItem.PackageInvoiceItem_Remarks = txtRemarks.Text.Trim();

            decimal Total_Amount_Prev = 0;
            decimal Total_Amount_Prev_With_GST = 0;
            decimal Total_Amount_GST = 0;
            try
            {
                Total_Amount_Prev_With_GST = Convert.ToDecimal(grdBOQ.Rows[i].Cells[17].Text.Trim());
            }
            catch
            {
                Total_Amount_Prev_With_GST = 0;
            }
            try
            {
                Total_Amount_GST = Convert.ToDecimal(grdBOQ.Rows[i].Cells[25].Text.Trim());
            }
            catch
            {
                Total_Amount_GST = 0;
            }
            try
            {
                Total_Amount_Prev = Total_Amount_Prev_With_GST - Total_Amount_GST;
            }
            catch
            {
                Total_Amount_Prev = 0;
            }
            decimal Rate = 0;
            if (obj_tbl_PackageInvoice.PackageInvoice_ProcessType == "Global")
            {
                try
                {
                    Rate = Convert.ToDecimal(grdBOQ.Rows[i].Cells[14].Text.Trim());
                }
                catch
                {
                    Rate = 0;
                }
            }
            else
            {
                try
                {
                    Rate = Convert.ToDecimal(grdBOQ.Rows[i].Cells[15].Text.Trim());
                }
                catch
                {
                    Rate = 0;
                }
            }
            decimal Total_Amount_UpTo = 0;
            try
            {

                Total_Amount_UpTo = decimal.Round((Convert.ToDecimal(txtQty.Text.Trim()) * Rate * obj_tbl_PackageInvoiceItem.PackageInvoiceItem_PercentageToBeReleased) / 100, 2, MidpointRounding.AwayFromZero);
            }
            catch
            {
                Total_Amount_UpTo = 0;
            }
            obj_tbl_PackageEMB.PackageEMB_Amount = Total_Amount_UpTo - Total_Amount_Prev;
            obj_tbl_PackageInvoiceItem.Total_Rate = Rate;
            obj_tbl_PackageInvoiceItem.AmountWOTax = Total_Amount_UpTo - Total_Amount_Prev;
            try
            {
                obj_tbl_PackageInvoiceItem.Total_Tax = Convert.ToDecimal(grdBOQ.Rows[i].Cells[22].Text.Trim());
                obj_tbl_PackageEMB.PackageEMB_TotalGST = Convert.ToDecimal(grdBOQ.Rows[i].Cells[22].Text.Trim());
            }
            catch
            {
                obj_tbl_PackageInvoiceItem.Total_Tax = (Total_Amount_UpTo - Total_Amount_Prev) * 12 / 100;
                obj_tbl_PackageEMB.PackageEMB_TotalGST = (Total_Amount_UpTo - Total_Amount_Prev) * 12 / 100;
            }
            try
            {
                obj_tbl_PackageInvoiceItem.PackageInvoiceItem_GST = Convert.ToDecimal(grdBOQ.Rows[i].Cells[8].Text.Trim());
                obj_tbl_PackageEMB.PackageEMB_GSTPercenatge = Convert.ToInt32(obj_tbl_PackageInvoiceItem.PackageInvoiceItem_GST);
            }
            catch
            {
                obj_tbl_PackageEMB.PackageEMB_GSTPercenatge = 0;
                obj_tbl_PackageInvoiceItem.PackageInvoiceItem_GST = 0;
            }
            obj_tbl_PackageEMB.PackageEMB_TotalAmount = obj_tbl_PackageInvoiceItem.AmountWOTax + obj_tbl_PackageInvoiceItem.Total_Tax;
            obj_tbl_PackageInvoiceItem.Total_Amount = obj_tbl_PackageInvoiceItem.AmountWOTax + obj_tbl_PackageInvoiceItem.Total_Tax;
            PackageInvoice_Total_Amount = PackageInvoice_Total_Amount + obj_tbl_PackageInvoiceItem.Total_Amount;

            if (obj_tbl_PackageInvoice.PackageInvoice_ProcessType == "Normal")
            {
                decimal GST_P = 0;
                obj_tbl_PackageInvoiceItem_Tax_Li = new List<tbl_PackageInvoiceItem_Tax>();
                try
                {
                    GST_P = Convert.ToInt32(grdBOQ.Rows[i].Cells[8].Text.Trim());
                }
                catch
                {
                    GST_P = 0;
                }
                obj_tbl_PackageInvoiceItem_Tax = new tbl_PackageInvoiceItem_Tax();
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_PackageInvoiceItem_Id = Convert.ToInt32(grdBOQ.Rows[i].Cells[0].Text.Trim());
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_InvoiceId = Convert.ToInt32(grdBOQ.Rows[0].Cells[1].Text.Trim());
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Deduction_Id = 1013;
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Value = GST_P / 2;
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Amount = decimal.Round((obj_tbl_PackageInvoiceItem.AmountWOTax * GST_P / 2) / 100, 2, MidpointRounding.AwayFromZero);
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Status = 1;

                obj_tbl_PackageInvoiceItem_Tax_Li.Add(obj_tbl_PackageInvoiceItem_Tax);

                obj_tbl_PackageInvoiceItem_Tax = new tbl_PackageInvoiceItem_Tax();
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_PackageInvoiceItem_Id = Convert.ToInt32(grdBOQ.Rows[i].Cells[0].Text.Trim());
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_InvoiceId = Convert.ToInt32(grdBOQ.Rows[0].Cells[1].Text.Trim());
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Deduction_Id = 1014;
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Value = GST_P / 2;
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Amount = decimal.Round((obj_tbl_PackageInvoiceItem.AmountWOTax * GST_P / 2) / 100, 2, MidpointRounding.AwayFromZero);
                obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Status = 1;

                obj_tbl_PackageInvoiceItem_Tax_Li.Add(obj_tbl_PackageInvoiceItem_Tax);

                if (obj_tbl_PackageInvoiceItem_Tax_Li != null)
                {
                    obj_tbl_PackageInvoiceItem.obj_tbl_PackageInvoiceItem_Tax_Li = obj_tbl_PackageInvoiceItem_Tax_Li;
                }
            }
            obj_tbl_PackageEMB_Li.Add(obj_tbl_PackageEMB);
            obj_tbl_PackageInvoiceItem_Li.Add(obj_tbl_PackageInvoiceItem);
        }
        if (hf_PackageInvoice_ProcessType.Value == "Global")
        {
            if (grdDeductionsM.FooterRow != null)
            {
                try
                {
                    obj_tbl_PackageInvoice.PackageInvoice_InvoiceAmount = decimal.Parse(grdDeductionsM.FooterRow.Cells[7].Text.Trim());
                    obj_tbl_PackageInvoice.InvoiceAmount = decimal.Parse(grdDeductionsM.FooterRow.Cells[7].Text.Trim());
                }
                catch
                {
                    obj_tbl_PackageInvoice.PackageInvoice_InvoiceAmount = 0;
                    obj_tbl_PackageInvoice.InvoiceAmount = 0;
                }
            }
        }
        else
        {
            if (grdBOQ.FooterRow != null)
            {
                decimal tax = 0;
                decimal amount = 0;
                try
                {
                    tax = decimal.Parse(grdBOQ.FooterRow.Cells[22].Text.Trim());
                }
                catch
                {
                    tax = 0;
                }
                try
                {
                    amount = decimal.Parse(grdBOQ.FooterRow.Cells[23].Text.Trim());
                }
                catch
                {
                    amount = 0;
                }
                obj_tbl_PackageInvoice.PackageInvoice_InvoiceAmount = amount + tax;
                obj_tbl_PackageInvoice.InvoiceAmount = amount + tax;
            }
        }

        obj_tbl_PackageInvoiceItem_Tax_Li = new List<tbl_PackageInvoiceItem_Tax>();
        obj_tbl_PackageInvoiceItem_Tax = new tbl_PackageInvoiceItem_Tax();

        TextBox txtDeductionValue = grdDeductionsM.Rows[2].FindControl("txtDeductionValue") as TextBox;
        CheckBox chkSelect = grdDeductionsM.Rows[2].FindControl("chkSelect") as CheckBox;
        obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_PackageInvoiceItem_Id = 0;
        obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_InvoiceId = Convert.ToInt32(grdBOQ.Rows[0].Cells[1].Text.Trim());
        obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Deduction_Id = Convert.ToInt32(grdDeductionsM.Rows[2].Cells[0].Text.Trim());
        try
        {
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Value = Convert.ToDecimal(txtDeductionValue.Text);
        }
        catch
        {
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Value = 0;
        }
        try
        {
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Amount = Convert.ToDecimal(grdDeductionsM.Rows[2].Cells[7].Text.Trim());
        }
        catch
        {
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Amount = 0;
        }
        obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Status = 1;


        if (obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Deduction_Id > 0)
        {
            obj_tbl_PackageInvoiceItem_Tax_Li.Add(obj_tbl_PackageInvoiceItem_Tax);
        }

        obj_tbl_PackageInvoiceItem_Tax = new tbl_PackageInvoiceItem_Tax();
        txtDeductionValue = grdDeductionsM.Rows[3].FindControl("txtDeductionValue") as TextBox;
        chkSelect = grdDeductionsM.Rows[3].FindControl("chkSelect") as CheckBox;
        obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_PackageInvoiceItem_Id = 0;
        obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_InvoiceId = Convert.ToInt32(grdBOQ.Rows[0].Cells[1].Text.Trim());
        obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Deduction_Id = Convert.ToInt32(grdDeductionsM.Rows[3].Cells[0].Text.Trim());
        try
        {
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Value = Convert.ToDecimal(txtDeductionValue.Text);
        }
        catch
        {
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Value = 0;
        }
        try
        {
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Amount = Convert.ToDecimal(grdDeductionsM.Rows[3].Cells[7].Text.Trim());
        }
        catch
        {
            obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Amount = 0;
        }
        obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Status = 1;


        if (obj_tbl_PackageInvoiceItem_Tax.PackageInvoiceItem_Tax_Deduction_Id > 0)
        {
            obj_tbl_PackageInvoiceItem_Tax_Li.Add(obj_tbl_PackageInvoiceItem_Tax);
        }


        if (obj_tbl_PackageInvoiceItem_Li != null)
        {
            obj_tbl_PackageInvoiceItem_Li[0].obj_tbl_PackageInvoiceItem_Tax_Li = obj_tbl_PackageInvoiceItem_Tax_Li;
        }

        List<tbl_PackageInvoiceAdditional> obj_tbl_PackageInvoiceAdditional_Li = new List<tbl_PackageInvoiceAdditional>();
        for (int i = 0; i < grdDeductions.Rows.Count; i++)
        {
            decimal TotalAmt = 0;
            decimal amount = 0;
            if (hf_PackageInvoice_ProcessType.Value == "Global")
            {
                TotalAmt = Convert.ToDecimal(grdDeductionsM.FooterRow.Cells[7].Text);
            }
            else
            {
                decimal tax = 0;
                try
                {
                    tax = decimal.Parse(grdBOQ.FooterRow.Cells[22].Text.Trim());
                }
                catch
                {
                    tax = 0;
                }
                try
                {
                    amount = decimal.Parse(grdBOQ.FooterRow.Cells[23].Text.Trim());
                }
                catch
                {
                    amount = 0;
                }
                TotalAmt = amount + tax;
            }

            txtDeductionValue = grdDeductions.Rows[i].FindControl("txtDeductionValue") as TextBox;
            TextBox txtDeductionAmount = grdDeductions.Rows[i].FindControl("txtDeductionAmount") as TextBox;
            TextBox txtDeductionComments = grdDeductions.Rows[i].FindControl("txtDeductionComments") as TextBox;
            chkSelect = grdDeductions.Rows[i].FindControl("chkSelect") as CheckBox;
            CheckBox chkFlat = grdDeductions.Rows[i].FindControl("chkFlat") as CheckBox;
            decimal DeductionValue = 0;
            if (chkSelect.Checked && !chkFlat.Checked)
            {
                try
                {
                    DeductionValue = Convert.ToDecimal(txtDeductionValue.Text.Trim());
                }
                catch
                {
                    DeductionValue = 0;
                }
                try
                {
                    txtDeductionAmount.Text = decimal.Round(((amount * DeductionValue) / 100), 0, MidpointRounding.AwayFromZero).ToString();
                }
                catch
                {
                    txtDeductionAmount.Text = "0";
                }

                tbl_PackageInvoiceAdditional obj_tbl_PackageInvoiceAdditional = new tbl_PackageInvoiceAdditional();
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Invoice_Id = Convert.ToInt32(grdBOQ.Rows[0].Cells[1].Text.Trim());
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_Id = Convert.ToInt32(grdDeductions.Rows[i].Cells[0].Text.Trim());
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Comments = txtDeductionComments.Text.Trim();
                if (chkFlat.Checked)
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_isFlat = "1";
                }
                else
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_isFlat = "0";
                }
                try
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_Value_Final = Convert.ToDecimal(txtDeductionAmount.Text.Trim());
                }
                catch
                { }
                try
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_Value_Master = DeductionValue;
                }
                catch
                { }
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Status = 1;
                obj_tbl_PackageInvoiceAdditional_Li.Add(obj_tbl_PackageInvoiceAdditional);
            }
            else if (chkFlat.Checked && chkSelect.Checked)
            {
                try
                {
                    DeductionValue = Convert.ToDecimal(txtDeductionValue.Text.Trim());
                }
                catch
                {
                    DeductionValue = 0;
                }
                tbl_PackageInvoiceAdditional obj_tbl_PackageInvoiceAdditional = new tbl_PackageInvoiceAdditional();
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Invoice_Id = Convert.ToInt32(grdBOQ.Rows[0].Cells[1].Text.Trim());
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_Id = Convert.ToInt32(grdDeductions.Rows[i].Cells[0].Text.Trim());
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Comments = txtDeductionComments.Text.Trim();
                if (chkFlat.Checked)
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_isFlat = "1";
                }
                else
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_isFlat = "0";
                }
                try
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_Value_Final = Convert.ToDecimal(txtDeductionAmount.Text.Trim());
                }
                catch
                { }
                try
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_Value_Master = DeductionValue;
                }
                catch
                { }
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Status = 1;
                obj_tbl_PackageInvoiceAdditional_Li.Add(obj_tbl_PackageInvoiceAdditional);
            }
        }
        for (int i = 0; i < grdDeductions2.Rows.Count; i++)
        {
            TextBox txtDeductionValue2 = grdDeductions2.Rows[i].FindControl("txtDeductionValue2") as TextBox;
            TextBox txtDeductionAmount2 = grdDeductions2.Rows[i].FindControl("txtDeductionAmount2") as TextBox;
            TextBox txtDeductionComments2 = grdDeductions2.Rows[i].FindControl("txtDeductionComments2") as TextBox;
            CheckBox chkSelect2 = grdDeductions2.Rows[i].FindControl("chkSelect2") as CheckBox;
            CheckBox chkFlat2 = grdDeductions2.Rows[i].FindControl("chkFlat2") as CheckBox;
            decimal AdditionValue = 0;
            if (chkSelect2.Checked && !chkFlat2.Checked)
            {
                try
                {
                    AdditionValue = Convert.ToDecimal(txtDeductionValue2.Text.Trim());
                }
                catch
                {
                    AdditionValue = 0;
                }
                try
                {
                    txtDeductionAmount2.Text = decimal.Round(((Convert.ToDecimal(grdBOQ.FooterRow.Cells[16].Text.Trim()) * AdditionValue) / 100), 0, MidpointRounding.AwayFromZero).ToString();
                }
                catch
                {
                    txtDeductionAmount2.Text = "0";
                }

                tbl_PackageInvoiceAdditional obj_tbl_PackageInvoiceAdditional = new tbl_PackageInvoiceAdditional();
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Invoice_Id = Convert.ToInt32(grdBOQ.Rows[0].Cells[1].Text.Trim());
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_Id = Convert.ToInt32(grdDeductions2.Rows[i].Cells[0].Text.Trim());
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Comments = txtDeductionComments2.Text.Trim();
                if (chkFlat2.Checked)
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_isFlat = "1";
                }
                else
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_isFlat = "0";
                }
                try
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_Value_Final = Convert.ToDecimal(txtDeductionAmount2.Text.Trim());
                }
                catch
                { }
                try
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_Value_Master = AdditionValue;
                }
                catch
                { }
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Status = 1;
                obj_tbl_PackageInvoiceAdditional_Li.Add(obj_tbl_PackageInvoiceAdditional);
            }
            else if (chkFlat2.Checked && chkSelect2.Checked)
            {
                try
                {
                    AdditionValue = Convert.ToDecimal(txtDeductionValue2.Text.Trim());
                }
                catch
                {
                    AdditionValue = 0;
                }
                tbl_PackageInvoiceAdditional obj_tbl_PackageInvoiceAdditional = new tbl_PackageInvoiceAdditional();
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Invoice_Id = Convert.ToInt32(grdBOQ.Rows[0].Cells[1].Text.Trim());
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_Id = Convert.ToInt32(grdDeductions2.Rows[i].Cells[0].Text.Trim());
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Comments = txtDeductionComments2.Text.Trim();
                if (chkFlat2.Checked)
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_isFlat = "1";
                }
                else
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_isFlat = "0";
                }
                try
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_Value_Final = Convert.ToDecimal(txtDeductionAmount2.Text.Trim());
                }
                catch
                { }
                try
                {
                    obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_Value_Master = AdditionValue;
                }
                catch
                { }
                obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Status = 1;
                obj_tbl_PackageInvoiceAdditional_Li.Add(obj_tbl_PackageInvoiceAdditional);
            }
        }

        List<tbl_PackageInvoiceDocs> obj_tbl_PackageInvoiceDocs_Li = new List<tbl_PackageInvoiceDocs>();
        for (int i = 0; i < grdDocumentMaster.Rows.Count; i++)
        {
            FileUpload flUpload = grdDocumentMaster.Rows[i].FindControl("flUpload") as FileUpload;
            TextBox txtDocumentOrderNo = grdDocumentMaster.Rows[i].FindControl("txtDocumentOrderNo") as TextBox;
            TextBox txtDocumentComments = grdDocumentMaster.Rows[i].FindControl("txtDocumentComments") as TextBox;
            if (flUpload.HasFile)
            {
                tbl_PackageInvoiceDocs obj_tbl_PackageInvoiceDocs = new tbl_PackageInvoiceDocs();
                obj_tbl_PackageInvoiceDocs.PackageInvoiceDocs_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_PackageInvoiceDocs.PackageInvoiceDocs_FileBytes = flUpload.FileBytes;
                string[] _fname = flUpload.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_PackageInvoiceDocs.PackageInvoiceDocs_FileName = _fname[_fname.Length - 1];
                obj_tbl_PackageInvoiceDocs.PackageInvoiceDocs_Invoice_Id = obj_tbl_PackageInvoice.PackageInvoice_Id;
                obj_tbl_PackageInvoiceDocs.PackageInvoiceDocs_Status = 1;
                obj_tbl_PackageInvoiceDocs.PackageInvoiceDocs_OrderNo = txtDocumentOrderNo.Text.Trim();
                obj_tbl_PackageInvoiceDocs.PackageInvoiceDocs_Comments = txtDocumentComments.Text.Trim();
                obj_tbl_PackageInvoiceDocs.PackageInvoiceDocs_Type = Convert.ToInt32(grdDocumentMaster.Rows[i].Cells[1].Text.Trim());
                obj_tbl_PackageInvoiceDocs_Li.Add(obj_tbl_PackageInvoiceDocs);
            }
            else
            {

                if (Mode == "Approve")
                {
                    if (chkUpdateExisting.Visible && !chkUpdateExisting.Checked)
                    {
                        if (ddlStatus.SelectedValue == "1" || ddlStatus.SelectedValue == "6" || ddlStatus.SelectedValue == "4" || ddlStatus.SelectedValue == "5")
                        {
                            MessageBox.Show("Please Upload Document.");
                            return;
                        }
                    }
                    
                }
            }
        }
        if (Mode == "Approve")
        {
            if (Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()) == 16)
            {
                if (ddlStatus.SelectedValue == "1" || ddlStatus.SelectedValue == "6" || ddlStatus.SelectedValue == "4" || ddlStatus.SelectedValue == "5")
                {
                    if (Scheme_Id == 12)
                    {
                        if (grdDocumentMaster.Rows.Count == 0 && obj_tbl_PackageInvoiceDocs_Li.Count != 7)
                        {
                            MessageBox.Show("Please Upload Document");
                            return;
                        }
                        else if (grdDocumentMaster.Rows.Count != 0 && obj_tbl_PackageInvoiceDocs_Li.Count != grdDocumentMaster.Rows.Count)
                        {
                            MessageBox.Show("Please Upload All Document");
                            return;
                        }
                    }
                    else
                    {
                        if (grdDocumentMaster.Rows.Count == 0 && obj_tbl_PackageInvoiceDocs_Li.Count != 6)
                        {
                            MessageBox.Show("Please Upload Document");
                            return;
                        }
                        else if (grdDocumentMaster.Rows.Count != 0 && obj_tbl_PackageInvoiceDocs_Li.Count != grdDocumentMaster.Rows.Count)
                        {
                            MessageBox.Show("Please Upload All Document");
                            return;
                        }
                    }
                }
            }
        }
        tbl_PackageInvoiceApproval obj_tbl_PackageInvoiceApproval = null;
        if (Mode == "Approve")
        {
            obj_tbl_PackageInvoiceApproval = new tbl_PackageInvoiceApproval();
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Comments = txtComments.Text.Trim();
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Status_Id = Convert.ToInt32(ddlStatus.SelectedValue);
            try
            {
                obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Additional_Status_Id = Convert.ToInt32(ddlAdditionalReason.SelectedValue);
            }
            catch
            {
                obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Additional_Status_Id = 0;
            }
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_PackageInvoice_Id = obj_tbl_PackageInvoice.PackageInvoice_Id;
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Status = 1;
        }
        tbl_SNAAccountLimitUsed obj_tbl_SNAAccountLimitUsed = null;
        tbl_FinancialTrans obj_tbl_FinancialTrans = null;
        if (divAmountTransfered.Visible && obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Status_Id == 1 && Transfred_Amount != 0)
        {
            obj_tbl_FinancialTrans = new tbl_FinancialTrans();
            obj_tbl_FinancialTrans.FinancialTrans_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_FinancialTrans.FinancialTrans_Amount = Total_Amount;
            obj_tbl_FinancialTrans.FinancialTrans_Comments = txtComments.Text.Trim();
            obj_tbl_FinancialTrans.FinancialTrans_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            obj_tbl_FinancialTrans.FinancialTrans_EntryType = "C";
            obj_tbl_FinancialTrans.FinancialTrans_FinancialYear_Id = 0;
            obj_tbl_FinancialTrans.FinancialTrans_Invoice_Id = Convert.ToInt32(grdBOQ.Rows[0].Cells[1].Text.Trim());
            obj_tbl_FinancialTrans.FinancialTrans_Status = 1;
            obj_tbl_FinancialTrans.FinancialTrans_TransAmount = Transfred_Amount;
            obj_tbl_FinancialTrans.FinancialTrans_TransType = "C";
        }
        if (divPPA.Visible)
        {
            if (hf_Scheme_Id.Value == "1013" || hf_Scheme_Id.Value == "1016")
            {
               decimal AvailableBal = 0;
                try
                {
                    AvailableBal = decimal.Parse(divBalance.InnerHtml);
                }
                catch
                {
                    AvailableBal = 0;
                }
                if (AvailableBal < Transfred_Amount)
                {
                    MessageBox.Show("Available Balance is Less Than The Invoice Amount.");
                    return;
                }

                obj_tbl_SNAAccountLimitUsed = new tbl_SNAAccountLimitUsed();
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_BatchId = "";
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_PPAAmount = 0;
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_PPANumber = txtPPANo.Text.Trim();
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_PPAVerified = 0;
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_ProjectWotk_Id = ProjectWork_Id;
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_Status = 1;
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_UsageType = "I";
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_Usage_Id = Convert.ToInt32(grdBOQ.Rows[0].Cells[1].Text.Trim());
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_UsedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_UsedLimit = Transfred_Amount;
                obj_tbl_SNAAccountLimitUsed.SNAAccountLimitUsed_UsedOn = Session["ServerDate"].ToString();
            }
        }
        if (new DataLayer().Update_tbl_PackageInvoice(obj_tbl_PackageInvoice, obj_tbl_PackageInvoiceAdditional_Li, obj_tbl_PackageInvoiceDocs_Li, obj_tbl_PackageInvoiceItem_Li, obj_tbl_PackageEMB_Li, obj_tbl_FinancialTrans, "A", obj_tbl_PackageInvoiceApproval, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), Scheme_Id, chkUpdateExisting.Checked, obj_tbl_SNAAccountLimitUsed))
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "1")//Administrator
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "2")//District
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "3")//ULB
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "4")//Zone Officer
            {
                Response.Redirect("DashboardDept.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "5")//Contractor Officer
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "6")//Circle Officer
            {
                Response.Redirect("DashboardDept.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "7")//Division Officer
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "8")//Organizational Admin
            {
                if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "1")
                {
                    Response.Redirect("DashboardSMD.aspx?T=A");
                }
                if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "3")
                {
                    Response.Redirect("DashboardSMD.aspx?T=A");
                }
                else if (Session["Person_BranchOffice_Id"].ToString() == "2" && Session["PersonJuridiction_DesignationId"].ToString() == "33")
                {
                    Response.Redirect("DashboardSMD.aspx?T=A");
                }
                else if (Session["Person_BranchOffice_Id"].ToString() == "1" && Session["PersonJuridiction_DesignationId"].ToString() == "33")
                {
                    Response.Redirect("Dashboard.aspx?T=A");
                }
                else if (Session["Person_BranchOffice_Id"].ToString() == "1" && Session["PersonJuridiction_DesignationId"].ToString() == "1039")
                {
                    Response.Redirect("Dashboard.aspx?T=A");
                }
                else
                {
                    Response.Redirect("DashboardDept.aspx?T=A");
                }
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "9")//Operator
            {
                Response.Redirect("DashboardDept.aspx?T=A");
            }
            else
            {
                Response.Redirect("Dashboard.aspx?T=A");
            }
            return;
        }
        else
        {
            MessageBox.Show("Error In Approval of Invoice Details...");
            return;
        }
    }

    protected void grdDocumentMaster_PreRender(object sender, EventArgs e)
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

    protected void grdPackageDetails_PreRender(object sender, EventArgs e)
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

    protected void lnk_CoverLetter_ServerClick(object sender, EventArgs e)
    {
        List<Cover_Letter> obj_Cover_Letter_Li = new List<Cover_Letter>();

        Cover_Letter obj_Cover_Letter = new Cover_Letter();
        DataSet ds = new DataSet();
        int Invoice_Id = Convert.ToInt32(hf_Invoice_Id.Value);
        ds = (new DataLayer()).get_Report_Cover_Details(Invoice_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            obj_Cover_Letter.Contractor_Name = ds.Tables[0].Rows[0]["Vendor_Name"].ToString();
            obj_Cover_Letter.Project_Name = ds.Tables[0].Rows[0]["Project_Name"].ToString();

            try
            {
                obj_Cover_Letter.Total_Centre_Share = Convert.ToDecimal(ds.Tables[0].Rows[0]["Central_Share"].ToString());
            }
            catch
            { }
            try
            {
                obj_Cover_Letter.Total_State_Share = Convert.ToDecimal(ds.Tables[0].Rows[0]["State_Share"].ToString());
            }
            catch
            { }
            try
            {
                obj_Cover_Letter.Total_ULB_Share = Convert.ToDecimal(ds.Tables[0].Rows[0]["ULB_Share"].ToString());
            }
            catch
            { }

            obj_Cover_Letter.Work_Name = ds.Tables[0].Rows[0]["ProjectWork_Name"].ToString();
            obj_Cover_Letter.Project_Id = ds.Tables[0].Rows[0]["ProjectWork_ProjectCode"].ToString();
            obj_Cover_Letter.Project_Type = ds.Tables[0].Rows[0]["ProjectType_Name"].ToString();

            obj_Cover_Letter.Financial_Year = "2020";
            obj_Cover_Letter.Account_Holder_Name = ds.Tables[0].Rows[0]["Vendor_Name"].ToString();
            try
            {
                obj_Cover_Letter.Amount_Received_To_Implementing_Agency_Including_Diversion = Convert.ToDecimal(ds.Tables[0].Rows[0]["PackageInvoiceCover_Amount_Received_To_Implementing_Agency_Including_Diversion"].ToString());
            }
            catch
            {
                obj_Cover_Letter.Amount_Received_To_Implementing_Agency_Including_Diversion = 0;
            }

            try
            {
                obj_Cover_Letter.Amount_Released_To_Division = Convert.ToDecimal(ds.Tables[0].Rows[0]["PackageInvoiceCover_Amount_Released_To_Division"].ToString());
            }
            catch
            {
                obj_Cover_Letter.Amount_Released_To_Division = 0;
            }
            try
            {
                obj_Cover_Letter.Balance_Amount_As_In_Bank_Statement = Convert.ToDecimal(ds.Tables[0].Rows[0]["PackageInvoiceCover_Balance_Amount_As_In_Bank_Statement"].ToString());
            }
            catch
            {
                obj_Cover_Letter.Balance_Amount_As_In_Bank_Statement = 0;
            }

            try
            {
                obj_Cover_Letter.Centage = Convert.ToDecimal(ds.Tables[0].Rows[0]["PackageInvoiceCover_Centage"].ToString());
            }

            catch
            {
                obj_Cover_Letter.Centage = 0;
            }
            try
            {
                obj_Cover_Letter.Expenditure_Done_By_Implementing_Agency = Convert.ToDecimal(ds.Tables[0].Rows[0]["PackageInvoiceCover_Expenditure_Done_By_Implementing_Agency"].ToString());
            }
            catch
            {
                obj_Cover_Letter.Expenditure_Done_By_Implementing_Agency = 0;
            }

            try
            {
                obj_Cover_Letter.Expenditure_By_Division = Convert.ToDecimal(ds.Tables[0].Rows[0]["PackageInvoiceCover_Expenditure_By_Division"].ToString());
            }
            catch
            {
                obj_Cover_Letter.Expenditure_By_Division = 0;
            }

            //obj_Cover_Letter.Financial_Year = "";

            try
            {
                obj_Cover_Letter.Find_Received = Convert.ToDecimal(ds.Tables[0].Rows[0]["PackageInvoiceCover_DiversionIn"].ToString());
            }
            catch
            {
                obj_Cover_Letter.Find_Received = 0;
            }
            try
            {
                obj_Cover_Letter.Fund_Diverted = Convert.ToDecimal(ds.Tables[0].Rows[0]["PackageInvoiceCover_DiversionOut"].ToString());
            }
            catch
            {
                obj_Cover_Letter.Fund_Diverted = 0;
            }
            obj_Cover_Letter.General_Manager = "";
            obj_Cover_Letter.Place = "";
            //obj_Cover_Letter.Project_Id = "";
            obj_Cover_Letter.Project_Manager = "";
            //obj_Cover_Letter.Project_Type = "";

            try
            {
                obj_Cover_Letter.Release_To_Implementing_Agency = Convert.ToDecimal(ds.Tables[0].Rows[0]["PackageInvoiceCover_ReleaseTillDate"].ToString());
            }
            catch
            {
                obj_Cover_Letter.Release_To_Implementing_Agency = 0;
            }
            try
            {
                obj_Cover_Letter.Sanctioned_Amount_Without_Centage = Convert.ToDecimal(ds.Tables[0].Rows[0]["PackageInvoiceCover_SanctionedAmount"].ToString());
            }
            catch
            {
                obj_Cover_Letter.Sanctioned_Amount_Without_Centage = 0;
            }
            obj_Cover_Letter.Scheme_Name = "";
            try
            {
                obj_Cover_Letter.Tendred_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["PackageInvoiceCover_TenderAmount"].ToString());
            }
            catch
            {
                obj_Cover_Letter.Tendred_Amount = 0;
            }

            try
            {
                obj_Cover_Letter.Total_Amount_Paid_To_Contractor_Till_Date = Convert.ToDecimal(ds.Tables[0].Rows[0]["PackageInvoiceCover_PaymentTillDate"].ToString());
            }
            catch
            {
                obj_Cover_Letter.Total_Amount_Paid_To_Contractor_Till_Date = 0;
            }
            try
            {
                obj_Cover_Letter.Total_Invoice_Value = Convert.ToDecimal(ds.Tables[0].Rows[0]["PackageInvoiceCover_Total_Invoice_Value"].ToString());
            }
            catch
            {
                obj_Cover_Letter.Total_Invoice_Value = 0;
            }
            try
            {
                obj_Cover_Letter.Total_Mobelization_Advance = Convert.ToDecimal(ds.Tables[0].Rows[0]["PackageInvoiceCover_MoblizationAdvance"].ToString());
            }
            catch
            {
                obj_Cover_Letter.Total_Mobelization_Advance = 0;
            }

            try
            {
                obj_Cover_Letter.Total_Mobelization_Advance_Adjustment_Before_Bill = Convert.ToDecimal(ds.Tables[0].Rows[0]["PackageInvoiceCover_MoblizationAdvanceAdjustment"].ToString());
            }
            catch
            {
                obj_Cover_Letter.Total_Mobelization_Advance_Adjustment_Before_Bill = 0;
            }
            try
            {
                obj_Cover_Letter.Total_Mobelization_Advance_Adjustment_In_Current_Bill = Convert.ToDecimal(ds.Tables[0].Rows[0]["PackageInvoiceCover_Mobelization_Advance_Adjustment_In_Current_Bill"].ToString());
            }
            catch
            {
                obj_Cover_Letter.Total_Mobelization_Advance_Adjustment_In_Current_Bill = 0;
            }

            obj_Cover_Letter_Li.Add(obj_Cover_Letter);
            Session["Cover_Letter"] = obj_Cover_Letter_Li;
            mpViewCoverLetter.Show();
        }
        else
        {
            MessageBox.Show("Unable To View Report");
            return;
        }
    }

    protected void lnk_PaymentSummery_ServerClick(object sender, EventArgs e)
    {
        List<ProjectSummery> obj_ProjectSummery_Li = new List<ProjectSummery>();

        ProjectSummery obj_ProjectSummery = new ProjectSummery();
        DataSet ds = new DataSet();
        int Invoice_Id = Convert.ToInt32(hf_Invoice_Id.Value);
        ds = (new DataLayer()).get_Report_Summery_Details(Invoice_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            obj_ProjectSummery.Contractor_Name = ds.Tables[0].Rows[0]["Vendor_Name"].ToString();
            obj_ProjectSummery.Extra_Item_Condition = "No";
            obj_ProjectSummery.Installment_Condition = "";
            try
            {
                obj_ProjectSummery.Project_Cost = Convert.ToDecimal(ds.Tables[0].Rows[0]["ProjectWork_Budget"].ToString());
            }
            catch
            { }
            obj_ProjectSummery.Project_Name = ds.Tables[0].Rows[0]["Project_Name"].ToString();
            try
            {
                obj_ProjectSummery.Sactioned_Cost = Convert.ToDecimal(ds.Tables[0].Rows[0]["ProjectWorkPkg_AgreementAmount"].ToString());
            }
            catch
            { }
            //obj_ProjectSummery.Scheme_Name = ds.Tables[0].Rows[0][""].ToString();
            try
            {
                obj_ProjectSummery.Tender_Cost = Convert.ToDecimal(ds.Tables[0].Rows[0]["Tender_Cost"].ToString());
            }
            catch
            { }
            try
            {
                obj_ProjectSummery.Tender_Cost_Less = Convert.ToDecimal(ds.Tables[0].Rows[0]["Tender_Cost_Less"].ToString());
            }
            catch
            { }
            obj_ProjectSummery.Tender_Cost_Less_With_Text = "(80% of Tender Cost " + ds.Tables[0].Rows[0]["Tender_Cost_Less"].ToString() + ")";

            try
            {
                obj_ProjectSummery.Total_Bill_Raised = Convert.ToDecimal(ds.Tables[0].Rows[0]["Total_Amount"].ToString());
            }
            catch
            { }
            try
            {
                obj_ProjectSummery.Total_Centre_Share = Convert.ToDecimal(ds.Tables[0].Rows[0]["Central_Share"].ToString());
            }
            catch
            { }
            try
            {
                obj_ProjectSummery.Total_State_Share = Convert.ToDecimal(ds.Tables[0].Rows[0]["State_Share"].ToString());
            }
            catch
            { }
            try
            {
                obj_ProjectSummery.Total_ULB_Share = Convert.ToDecimal(ds.Tables[0].Rows[0]["ULB_Share"].ToString());
            }
            catch
            { }
            try
            {
                obj_ProjectSummery.Total_Calculated = obj_ProjectSummery.Total_Centre_Share + obj_ProjectSummery.Total_State_Share + obj_ProjectSummery.Total_ULB_Share;
            }
            catch
            { }

            try
            {
                obj_ProjectSummery.Total_Payment_Earlier = Convert.ToDecimal(ds.Tables[0].Rows[0]["PackageInvoiceCover_Total_Payment_Earlier"].ToString());
            }
            catch
            {
                obj_ProjectSummery.Total_Payment_Earlier = 0;
            }
            try
            {
                obj_ProjectSummery.Total_Proposed_Payment_Jal_Nigam = Convert.ToDecimal(ds.Tables[0].Rows[0]["PackageInvoiceCover_Total_Proposed_Payment_Jal_Nigam"].ToString());
            }
            catch
            {
                obj_ProjectSummery.Total_Proposed_Payment_Jal_Nigam = 0;
            }
            try
            {
                obj_ProjectSummery.Total_Release = Convert.ToDecimal(ds.Tables[0].Rows[0]["PackageInvoiceCover_Total_Release"].ToString());
            }
            catch
            {
                obj_ProjectSummery.Total_Release = 0;
            }

            try
            {
                obj_ProjectSummery.Total_With_Held_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["PackageInvoiceCover_Total_With_Held_Amount"].ToString());
            }
            catch
            {
                obj_ProjectSummery.Total_With_Held_Amount = 0;
            }
            try
            {
                obj_ProjectSummery.Work_Cost = Convert.ToDecimal(ds.Tables[0].Rows[0]["Work_Amount"].ToString());
            }
            catch
            {
                obj_ProjectSummery.Work_Cost = 0;
            }
            try
            {
                obj_ProjectSummery.Total_Balance = Convert.ToDecimal(ds.Tables[0].Rows[0]["PackageInvoiceCover_Total_Balance"].ToString());
            }
            catch
            {
                obj_ProjectSummery.Total_Balance = 0;
            }
            obj_ProjectSummery.Work_Name = ds.Tables[0].Rows[0]["ProjectWork_Name"].ToString();
            obj_ProjectSummery_Li.Add(obj_ProjectSummery);

            Session["ProjectSummery"] = obj_ProjectSummery_Li;
            mpViewSummery.Show();
        }
        else
        {
            MessageBox.Show("Unable To View Report");
            return;
        }

    }

    protected void grdDeductions2_PreRender(object sender, EventArgs e)
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

    protected void grdDeductions2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkSelect2 = e.Row.FindControl("chkSelect2") as CheckBox;
            CheckBox chkFlat2 = e.Row.FindControl("chkFlat2") as CheckBox;
            int PackageInvoiceAdditional_Id = 0;
            try
            {
                PackageInvoiceAdditional_Id = Convert.ToInt32(e.Row.Cells[1].Text.Trim());
            }
            catch
            {
                PackageInvoiceAdditional_Id = 0;
            }
            string Deduction_Type = e.Row.Cells[3].Text.Trim();
            if (Deduction_Type == "Percentage")
            {
                chkFlat2.Checked = false;
            }
            else
            {
                chkFlat2.Checked = true;
            }
            int Deduction_isFlat = 0;
            try
            {
                Deduction_isFlat = Convert.ToInt32(e.Row.Cells[2].Text.Trim());
            }
            catch
            {
                Deduction_isFlat = 0;
            }
            if (Deduction_isFlat == 1)
            {
                chkFlat2.Checked = true;
            }
            else
            {
                chkFlat2.Checked = false;
            }

            if (PackageInvoiceAdditional_Id > 0)
            {
                chkSelect2.Checked = true;
                chkSelect2_CheckedChanged(chkSelect2, e);
            }
            else
            {
                chkSelect2.Checked = false;
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
    protected void grdDocumentMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //FileUpload flUpload = e.Row.FindControl("flUpload") as FileUpload;
            //PostBackTrigger trigger = new PostBackTrigger();
            //trigger.ControlID = flUpload.ID;
            //up.Triggers.Add(trigger);
        }
    }

    protected void btnViewEMB_Click(object sender, ImageClickEventArgs e)
    {
        try
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
            Response.Redirect("ViewEMBDetails?Invoice_Id=" + Invoice_Id);
        }
        catch
        {

        }
    }

    protected void tnViewBOQ_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
            int Package_Id = 0;
            try
            {
                Package_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
            }
            catch
            {
                Package_Id = 0;
            }
            Response.Redirect("View_BOQ_Details?Package_Id=" + Package_Id);
        }
        catch
        {

        }
    }

    protected void link_PaymentOrder_ServerClick(object sender, EventArgs e)
    {
        List<tbl_Report_Payment_Order> obj_tbl_Report_Payment_Order_Li = new List<tbl_Report_Payment_Order>();

        tbl_Report_Payment_Order obj_tbl_Report_Payment_Order = new tbl_Report_Payment_Order();
        DataSet ds = new DataSet();
        int Invoice_Id = Convert.ToInt32(hf_Invoice_Id.Value);
        ds = (new DataLayer()).get_Report_PaymentOrder_Details(Invoice_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            obj_tbl_Report_Payment_Order.Invoice_Date = ds.Tables[0].Rows[0]["PackageInvoice_Date"].ToString();
            try
            {
                obj_tbl_Report_Payment_Order.Invoice_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["Total_Amount"].ToString());
            }
            catch
            { }

            obj_tbl_Report_Payment_Order.Invoice_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["PackageInvoice_Id"].ToString());
            obj_tbl_Report_Payment_Order.MB_No = ds.Tables[0].Rows[0]["List_EMBNo"].ToString();
            obj_tbl_Report_Payment_Order.PPA_No = "";
            obj_tbl_Report_Payment_Order.Content_Line_1 = "     अमृत योजनान्तर्गत सैप वर्ष के अन्तर्गत " + ds.Tables[0].Rows[0]["ProjectWork_Name"].ToString() + " (योजना कोड- " + ds.Tables[0].Rows[0]["ProjectWork_ProjectCode"].ToString() + ") संचालित योजना हेतु धनराशि रू0 " + obj_tbl_Report_Payment_Order.Invoice_Amount.ToString() + " का समायोजन अनुबन्ध संख्या " + ds.Tables[0].Rows[0]["ProjectWorkPkg_Agreement_No"].ToString() + " के बीजक संख्या " + ds.Tables[0].Rows[0]["PackageInvoice_VoucherNo"].ToString() + " रनिंग / फाइनल दिनांक " + ds.Tables[0].Rows[0]["PackageInvoice_Date"].ToString() + " के सापेक्ष मैसर्स " + ds.Tables[0].Rows[0]["Person_Name"].ToString() + " के भुगतान हेतु कार्यालय अधिशासी अभियन्ता / परियोजना प्रबन्धक " + ds.Tables[0].Rows[0]["Division_Name"].ToString() + " उ0प्र0 जल निगम (नगरीय)...........................द्वारा किया जा चुका है। अन्य विवरण निम्नानुसार है :-";
            obj_tbl_Report_Payment_Order_Li.Add(obj_tbl_Report_Payment_Order);

            //Session["Payment_Order"] = obj_tbl_Report_Payment_Order_Li;
            //mpViewPaymentOrder.Show();
            string filePath = "\\Downloads\\";
            string fileName = Invoice_Id.ToString() + ".pdf";

            string webURI = "";
            if (Page.Request.Url.Query.Trim() == "")
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "") + filePath + fileName).Replace("\\", "/");
            }
            else
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "") + filePath + fileName).Replace("\\", "/");
            }

            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Crystal/Payment_Order.rpt"));
            crystalReport.SetDataSource(obj_tbl_Report_Payment_Order_Li);
            //Session["rptDocI"] = crystalReport;
            //crystalReport.ReportSource = crystalReport;
            //crystalReport.RefreshReport();
            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

            FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
            if (fi.Exists)
            {
                new AllClasses().Render_PDF_Document(ltEmbed, filePath + fileName);
                mp1.Show();
            }
            else
            {
                MessageBox.Show("Unable To Download File.");
                return;
            }
        }
        else
        {
            MessageBox.Show("Unable To View Payment Order");
            return;
        }
    }

    protected void btnSaveInvoice_Click(object sender, EventArgs e)
    {
        Save_Data("Save");
    }

    protected void grdExtraItemApprove_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkDownload1 = (e.Row.FindControl("lnkAgreementFile2") as LinkButton);

            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkDownload1);
        }
    }
    protected void lnkAgreementFile2_Click(object sender, EventArgs e)
    {
        GridViewRow gr = ((sender) as LinkButton).Parent.Parent as GridViewRow;
        string File = gr.Cells[0].Text.Replace("&nbsp", "").ToString().Trim();
        if (File == "")
        {
            MessageBox.Show("File Not Find");
        }
        else
        {
            FileInfo fi = new FileInfo(Server.MapPath(".") + File);
            if (fi.Exists)
            {
                //Response.ClearContent();
                //Response.AddHeader("Content-Disposition", "attachment; filename=" + fi.Name);
                //Response.AddHeader("Content-Length", fi.Length.ToString());
                //string CId = Request["__EVENTTARGET"];
                //Response.TransmitFile(fi.FullName);
                //Response.End();
                new AllClasses().Render_PDF_Document(ltEmbed, File);
                mp1.Show();
            }
            else
            {
                MessageBox.Show("File Not Find");
            }
        }
    }

    private void get_PackageEMB_ExtraItemApprove(string PackageEMB_Master_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PackageEMB_ExtraItemApprove(PackageEMB_Master_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            grdExtraItemApprove.DataSource = ds.Tables[0];
            grdExtraItemApprove.DataBind();
        }
        else
        {
            grdExtraItemApprove.DataSource = null;
            grdExtraItemApprove.DataBind();
        }
    }
    protected void chkSelect2_CheckedChanged(object sender, EventArgs e)
    {
        Calculate_Total();
    }

    protected void chkFlat2_CheckedChanged(object sender, EventArgs e)
    {
        Calculate_Total();
    }

    protected void txtDeductionValue2_TextChanged(object sender, EventArgs e)
    {
        Calculate_Total();
    }

    protected void txtDeductionAmount2_TextChanged(object sender, EventArgs e)
    {
        Calculate_Total();
    }
    protected void txtDeductionValue_TextChanged1(object sender, EventArgs e)
    {
        Calculate_Total();
    }

    protected void chkSelect_CheckedChanged1(object sender, EventArgs e)
    {
        Calculate_Total();
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        Calculate_Total();
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        Calculate_Total();
    }
    protected void txtDeductionValue_TextChanged(object sender, EventArgs e)
    {
        Calculate_Total();
    }
    protected void chkFlat_CheckedChanged(object sender, EventArgs e)
    {
        Calculate_Total();
    }

    protected void txtDeductionAmount_TextChanged(object sender, EventArgs e)
    {
        Calculate_Total();
    }

    protected void btnRevert_Click(object sender, EventArgs e)
    {
        if (hf_Loop.Value == "" || hf_Loop.Value == "0")
        {
            MessageBox.Show("Please Select A Invoice");
            return;
        }
        if (hf_Scheme_Id.Value == "" || hf_Scheme_Id.Value == "0")
        {
            MessageBox.Show("Please Select A Invoice");
            return;
        }
        if (txtComments.Text == "")
        {
            MessageBox.Show("Please Provide Comments");
            return;
        }
        int Loop = 1;
        try
        {
            Loop = Convert.ToInt32(hf_Loop.Value);
        }
        catch
        {
            Loop = 1;
        }

        List<tbl_PackageInvoiceApproval> obj_tbl_PackageInvoiceApproval_Li = new List<tbl_PackageInvoiceApproval>();
        tbl_PackageInvoiceApproval obj_tbl_PackageInvoiceApproval = new tbl_PackageInvoiceApproval();
        obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Comments = txtComments.Text.Trim();
        obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
        obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Status_Id = 2;
        obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Next_Organisation_Id = 1;
        if (Loop == 1)
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Next_Designation_Id = 9;
        else
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Next_Designation_Id = 4;
        obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Step_Count = 4;
        obj_tbl_PackageInvoiceApproval.Scheme_Id = Convert.ToInt32(hf_Scheme_Id.Value);
        obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Additional_Status_Id = 0;
        obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_PackageInvoice_Id = Convert.ToInt32(hf_Invoice_Id.Value);
        obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Status = 1;
        obj_tbl_PackageInvoiceApproval_Li.Add(obj_tbl_PackageInvoiceApproval);
        if (new DataLayer().Update_tbl_PackageInvoice_Rejected(obj_tbl_PackageInvoiceApproval_Li))
        {
            MessageBox.Show("Updated Successfully.");
            hf_Invoice_Id.Value = "0";
            txtComments.Text = "";
            Response.Redirect("Dashboard.aspx");
            return;
        }
        else
        {
            MessageBox.Show("Error In Updation.");
            return;
        }
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue == "3")
        {
            divAdditionalReason.Visible = true;
        }
        else
        {
            divAdditionalReason.Visible = false;
        }
    }

    protected void btnOpenTimeline1_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Invoice_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_InvoiceApproval_History_Combined(Invoice_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            DateTime dtPrev;
            DateTime dtStart = DateTime.ParseExact(ds.Tables[0].Rows[0]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                    dtPrev = dtStart;
                else
                    dtPrev = DateTime.ParseExact(ds.Tables[0].Rows[i - 1]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dtCurrent = DateTime.ParseExact(ds.Tables[0].Rows[i]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                ds.Tables[0].Rows[i]["t1"] = dtCurrent.Subtract(dtStart).Days;
                ds.Tables[0].Rows[i]["t2"] = dtCurrent.Subtract(dtPrev).Days;
            }
            grdTimeLine.DataSource = ds.Tables[0];
            grdTimeLine.DataBind();
            mpTimeLine.Show();
        }
        else
        {
            grdTimeLine.DataSource = null;
            grdTimeLine.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void grdTimeLine_PreRender(object sender, EventArgs e)
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
    protected void grdPPAVerification_PreRender(object sender, EventArgs e)
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

    protected void txtPPANo_TextChanged(object sender, EventArgs e)
    {
        PPA_Details obj_PPA_Details = new PPA_Details();
        obj_PPA_Details.PPA_Number = txtPPANo.Text.Trim();
        obj_PPA_Details.PPA_Date = "";
        obj_PPA_Details.Req_Type = "Batch";
        obj_PPA_Details.Entity_Code = "AMRUT";

        List<PNB_GetPPADetails_Res> obj_PNB_GetPPADetails_Res = new DataLayerSNA().GetPPABatchDetails(obj_PPA_Details);
        if (obj_PNB_GetPPADetails_Res != null && obj_PNB_GetPPADetails_Res.Count > 0)
        {
            divPPAVerification.Visible = true;
            grdPPAVerification.DataSource = obj_PNB_GetPPADetails_Res;
            grdPPAVerification.DataBind();
        }
        else
        {
            divPPAVerification.Visible = false;
            grdPPAVerification.DataSource = null;
            grdPPAVerification.DataBind();
        }
    }
    protected void chkFilter_CheckedChanged(object sender, EventArgs e)
    {
        int Invoice_Id = Convert.ToInt32(hf_Invoice_Id.Value);
        get_tbl_InvoiceItem(Invoice_Id, null);
    }
}