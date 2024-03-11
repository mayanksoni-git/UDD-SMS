using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class MasterCorrection : System.Web.UI.Page
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
                div_SNA_Balance.Visible = true;
            }
            hide();
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        PostBackTrigger trg1 = new PostBackTrigger();
        trg1.ControlID = btnUpdateDoc.ID;
        up.Triggers.Add(trg1);

        PostBackTrigger trg2 = new PostBackTrigger();
        trg2.ControlID = btnApprovePayment.ID;
        up.Triggers.Add(trg2);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string Msg = "";
        if (txtMasterId.Text.Trim() == string.Empty)
        {
            Msg = "Give Master Id";
            txtMasterId.Focus();
            return;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Data_Correction(rbtType.SelectedValue, Convert.ToInt32(txtMasterId.Text));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            hf_Format.Value = ds.Tables[0].Rows[0]["Format"].ToString();
            hf_Scheme_Id.Value = ds.Tables[0].Rows[0]["ProjectWork_Project_Id"].ToString();
            if (DropDownList1.SelectedValue == "14")
            {
                get_tbl_Designation_Loop_Level_Wise(rbtType.SelectedValue, Convert.ToInt32(hf_Scheme_Id.Value));
            }
            MessageBox.Show(ds.Tables[0].Rows[0]["Format"].ToString());
        }
        else
        {
            hf_Format.Value = "";
            hf_Scheme_Id.Value = "";
            MessageBox.Show("Not Found");
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txtMasterId.Text.Trim() == string.Empty)
        {
            MessageBox.Show("Give Master Id/Invoice Id ");
            txtMasterId.Focus();
            return;
        }
        string to_Update = "";
        if (hf_Format.Value == "Old Format")
        {
            to_Update = "N";
        }
        else
        {
            to_Update = "";
        }

        if (new DataLayer().update_Data_Correction(rbtType.SelectedValue, Convert.ToInt32(txtMasterId.Text), to_Update))
        {
            MessageBox.Show("Updated Successfully! ");
            return;
        }
        else
        {
            MessageBox.Show("Error ! ");
            return;
        }
    }

    protected void btnUpdateQ_Click(object sender, EventArgs e)
    {
        if (txtMasterId.Text.Trim() == string.Empty)
        {
            MessageBox.Show("Give Master Id/Invoice Id ");
            txtMasterId.Focus();
            return;
        }

        decimal below_Val = 0;
        try
        {
            below_Val = Convert.ToDecimal(txtBelow.Text.Trim());
        }
        catch
        {
            below_Val = 0;
        }

        if (new DataLayer().update_Data_Correction_BelowVal(rbtType.SelectedValue, Convert.ToInt32(txtMasterId.Text), below_Val, 2017))
        {
            MessageBox.Show("Successfully Updated Quoted Rate! ");
            return;
        }
        else
        {
            MessageBox.Show("Error in updation of Quoted Rate ! ");
            return;
        }
    }

    protected void btnUpdateIA_Click(object sender, EventArgs e)
    {
        if (txtMasterId.Text.Trim() == string.Empty)
        {
            MessageBox.Show("Give Master Id/Invoice Id ");
            txtMasterId.Focus();
            return;
        }
        decimal Invoice_Amount = 0;
        try
        {
            Invoice_Amount = Convert.ToDecimal(txtIA.Text.Trim());
        }
        catch
        {
            Invoice_Amount = 0;
        }
        if (new DataLayer().update_Invoice_Amoint(Convert.ToInt32(txtMasterId.Text), Invoice_Amount))
        {
            MessageBox.Show("Successfully Updated Invoice Amount! ");
            return;
        }
        else
        {
            MessageBox.Show("Error in updation of Invoice Amount ! ");
            return;
        }
    }

    protected void btnUpdateDD_Click(object sender, EventArgs e)
    {
        if (txtMasterId.Text.Trim() == string.Empty)
        {
            MessageBox.Show("Give Master Id/Invoice Id ");
            txtMasterId.Focus();
            return;
        }

        decimal DD_Val = 0;
        try
        {
            DD_Val = Convert.ToDecimal(txtDD.Text.Trim());
        }
        catch
        {
            DD_Val = 0;
        }

        if (new DataLayer().update_Data_Correction_BelowVal(rbtType.SelectedValue, Convert.ToInt32(txtMasterId.Text), DD_Val, 2016))
        {
            MessageBox.Show("Successfully Updated Designe And Drawing! ");
            return;
        }
        else
        {
            MessageBox.Show("Error in updation of Designe And Drawing ! ");
            return;
        }

    }

    protected void btnUpdateGST_Click(object sender, EventArgs e)
    {
        if (txtMasterId.Text.Trim() == string.Empty)
        {
            MessageBox.Show("Give Master Id/Invoice Id ");
            txtMasterId.Focus();
            return;
        }

        if (new DataLayer().update_GST(Convert.ToInt32(txtMasterId.Text)))
        {
            MessageBox.Show("Successfully Applied GST! ");
            return;
        }
        else
        {
            MessageBox.Show("Error on Applying GST ! ");
            return;
        }
    }
    protected void btnUpdateBOQRate_Click(object sender, EventArgs e)
    {
        if (txtMasterId.Text.Trim() == string.Empty)
        {
            MessageBox.Show("Give Master Id/Invoice Id ");
            txtMasterId.Focus();
            return;
        }

        decimal BOQ_Rate = 0;
        int IID = Convert.ToInt32(txtIID.Text.Trim());
        try
        {
            BOQ_Rate = Convert.ToDecimal(txtBOQRate.Text.Trim());
        }
        catch
        {
            BOQ_Rate = 0;
        }

        if (new DataLayer().update_BOQ_Rate(rbtType.SelectedValue, Convert.ToInt32(txtMasterId.Text), IID, BOQ_Rate))
        {
            MessageBox.Show("Successfully Updated New BOQ Rate! ");
            return;
        }
        else
        {
            MessageBox.Show("Error in updation of BOQ Rate ! ");
            return;
        }

    }
    protected void btnUpdateDoc_Click(object sender, EventArgs e)
    {
        if (txtMasterId.Text.Trim() == string.Empty)
        {
            MessageBox.Show("Give Invoice Id ");
            txtMasterId.Focus();
            return;
        }
        if (ddlDoc.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Document Type");
            return;
        }
        if (!flUploadDoc.HasFiles)
        {
            MessageBox.Show("Please Upload document");
            return;
        }
        string Order_Number = txtON.Text.Trim();
        int invoic_Id = Convert.ToInt32(txtMasterId.Text);
        string extGO = "";
        string fileName = "";
        string path = "";
        Byte[] flName;
        if (rbtType.SelectedValue == "I")
        {
            if (flUploadDoc.HasFile)
            {
                flName = flUploadDoc.FileBytes;
                string[] _fname = flUploadDoc.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                extGO = _fname[_fname.Length - 1];
                if (!Directory.Exists(Server.MapPath(".") + "\\Downloads\\Invoice\\"))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + "\\Downloads\\Invoice\\");
                }
                if (!Directory.Exists(Server.MapPath(".") + "\\Downloads\\Invoice\\" + invoic_Id + "\\"))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + "\\Downloads\\Invoice\\" + invoic_Id + "\\");
                }
                fileName = DateTime.Now.Ticks.ToString("x") + "." + extGO;
                path = "\\Downloads\\Invoice\\" + invoic_Id + "\\" + fileName;
                File.WriteAllBytes(Server.MapPath(".") + "\\Downloads\\Invoice\\" + invoic_Id + "\\" + fileName, flName);
            }
            if (path == "")
            {
                MessageBox.Show("Document Unable to Upload !");
                txtMasterId.Focus();
                return;
            }
            if (new DataLayer().update_Upload_Document(Convert.ToInt32(txtMasterId.Text), Convert.ToInt32(ddlDoc.SelectedValue), path, Order_Number))
            {
                MessageBox.Show("Successfully Updated Document! ");
                return;
            }
            else
            {
                MessageBox.Show("Error in Uploading Document ! ");
                return;
            }
        }
        if (rbtType.SelectedValue == "DPR")
        {
            if (flUploadDoc.HasFile)
            {
                flName = flUploadDoc.FileBytes;
                string[] _fname = flUploadDoc.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                extGO = _fname[_fname.Length - 1];
                if (!Directory.Exists(Server.MapPath(".") + "\\Downloads\\DPR\\" + txtMasterId.Text))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + "\\Downloads\\DPR\\" + txtMasterId.Text);
                }
                fileName = DateTime.Now.Ticks.ToString("x") + "." + extGO;
                path = "\\Downloads\\DPR\\" + txtMasterId.Text + "\\" + fileName;
                File.WriteAllBytes(Server.MapPath(".") + "\\Downloads\\DPR\\" + txtMasterId.Text + "\\" + fileName, flName);
            }
            if (path == "")
            {
                MessageBox.Show("Document Unable to Upload !");
                txtMasterId.Focus();
                return;
            }
            if (new DataLayer().update_Upload_Document_DPR(Convert.ToInt32(txtMasterId.Text), Convert.ToInt32(ddlDoc.SelectedValue), path, Order_Number))
            {
                MessageBox.Show("Successfully Updated Document! ");
                return;
            }
            else
            {
                MessageBox.Show("Error in Uploading Document ! ");
                return;
            }
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedValue == "0")
        {
            hide();
            CheckFormate.Visible = true;
        }
        if (DropDownList1.SelectedValue == "1")
        {
            hide();
            oldNew.Visible = true;
        }
        if (DropDownList1.SelectedValue == "2")
        {
            hide();
            quatedRate.Visible = true;
        }
        if (DropDownList1.SelectedValue == "3")
        {
            hide();
            invoiceAmount.Visible = true;
        }
        if (DropDownList1.SelectedValue == "4")
        {
            hide();
            designeAndDrawing.Visible = true;
        }
        if (DropDownList1.SelectedValue == "5")
        {
            hide();
            gst.Visible = true;
        }
        if (DropDownList1.SelectedValue == "6")
        {
            hide();
            BOQ_Rate.Visible = true;
        }
        if (DropDownList1.SelectedValue == "7")
        {
            hide();
            UploadDoc.Visible = true;
            get_Document_Type();
        }
        if (DropDownList1.SelectedValue == "8")
        {
            hide();
            SincePrevious.Visible = true;
        }
        if (DropDownList1.SelectedValue == "9")
        {
            hide();
            divPaymentDone.Visible = true;
        }
        if (DropDownList1.SelectedValue == "10")
        {
            hide();
            divChageGST.Visible = true;
        }
        if (DropDownList1.SelectedValue == "11")
        {
            hide();
            divFundNA.Visible = true;
        }
        if (DropDownList1.SelectedValue == "12")
        {
            hide();
            divChangeRA.Visible = true;
        }
        if (DropDownList1.SelectedValue == "13")
        {
            hide();
            divChangeMB.Visible = true;
        }
        if (DropDownList1.SelectedValue == "14")
        {
            hide();
            divLoopChange.Visible = true;

        }
    }

    private void hide()
    {
        CheckFormate.Visible = true;
        oldNew.Visible = false;
        quatedRate.Visible = false;
        invoiceAmount.Visible = false;
        designeAndDrawing.Visible = false;
        gst.Visible = false;
        BOQ_Rate.Visible = false;
        UploadDoc.Visible = false;
        SincePrevious.Visible = false;
        divPaymentDone.Visible = false;
        divChageGST.Visible = false;
        divFundNA.Visible = false;
        divChangeRA.Visible = false;
        divChangeMB.Visible = false;
        divLoopChange.Visible = false;
    }

    private void get_Document_Type()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_TradeDocument();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDoc, "TradeDocument_Name", "TradeDocument_Id");
        }
        else
        {
            ddlDoc.Items.Clear();
        }
    }

    protected void btnSP_Click(object sender, EventArgs e)
    {
        if (txtSP.Text.Trim() == string.Empty)
        {
            MessageBox.Show("Give Package Id ");
            txtSP.Focus();
            return;
        }

        int Package_Id = Convert.ToInt32(txtSP.Text);

        if (new DataLayer().Fix_SincePrevious(Package_Id))
        {
            MessageBox.Show("Fixed sinceprevious Successfully ! ");
            return;
        }
        else
        {
            MessageBox.Show("Error ! ");
            return;
        }
    }

    protected void btnApprovePayment_Click(object sender, EventArgs e)
    {
        if (rbtType.SelectedValue == "I")
        {
            if (!flUploadOrder.HasFiles)
            {
                MessageBox.Show("Please Upload document");
                return;
            }
            int invoic_Id = Convert.ToInt32(txtMasterId.Text);
            string extGO = "";
            string fileName = "";
            string path = "";
            Byte[] flName;
            if (flUploadOrder.HasFile)
            {
                flName = flUploadOrder.FileBytes;
                string[] _fname = flUploadOrder.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                extGO = _fname[_fname.Length - 1];
                if (!Directory.Exists(Server.MapPath(".") + "\\Downloads\\Invoice\\"))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + "\\Downloads\\Invoice\\");
                }
                if (!Directory.Exists(Server.MapPath(".") + "\\Downloads\\Invoice\\" + invoic_Id + "\\"))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + "\\Downloads\\Invoice\\" + invoic_Id + "\\");
                }
                fileName = DateTime.Now.Ticks.ToString("x") + "." + extGO;
                path = "\\Downloads\\Invoice\\" + invoic_Id + "\\" + fileName;
                File.WriteAllBytes(Server.MapPath(".") + "\\Downloads\\Invoice\\" + invoic_Id + "\\" + fileName, flName);
            }
            if (path == "")
            {
                MessageBox.Show("Document Unable to Upload !");
                txtMasterId.Focus();
                return;
            }
            tbl_PackageInvoiceApproval obj_tbl_PackageInvoiceApproval = new tbl_PackageInvoiceApproval();
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_AddedBy = 1;
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Additional_Status_Id = 0;
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Comments = "Approved For Payment";
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Next_Designation_Id = 0;
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Next_Organisation_Id = 0;
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_PackageInvoice_Id = Convert.ToInt32(txtMasterId.Text);
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Package_Id = 0;
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Status = 1;
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Status_Id = 6;
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Step_Count = 0;
            obj_tbl_PackageInvoiceApproval.Scheme_Id = Convert.ToInt32(hf_Scheme_Id.Value);

            if (new DataLayer().update_PaymentDone(Convert.ToInt32(txtMasterId.Text), path, obj_tbl_PackageInvoiceApproval))
            {
                MessageBox.Show("Successfully Updated! ");
                return;
            }
            else
            {
                MessageBox.Show("Error in Uploading Document! ");
                return;
            }
        }
        else if (rbtType.SelectedValue == "A")
        {
            if (!flUploadOrder.HasFiles)
            {
                MessageBox.Show("Please Upload document");
                return;
            }
            int invoic_Id = Convert.ToInt32(txtMasterId.Text);
            string extGO = "";
            string fileName = "";
            string path = "";
            Byte[] flName;
            if (flUploadOrder.HasFile)
            {
                flName = flUploadOrder.FileBytes;
                string[] _fname = flUploadOrder.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                extGO = _fname[_fname.Length - 1];
                if (!Directory.Exists(Server.MapPath(".") + "\\Downloads\\ADP\\"))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + "\\Downloads\\ADP\\");
                }
                if (!Directory.Exists(Server.MapPath(".") + "\\Downloads\\ADP\\" + invoic_Id + "\\"))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + "\\Downloads\\ADP\\" + invoic_Id + "\\");
                }
                fileName = DateTime.Now.Ticks.ToString("x") + "." + extGO;
                path = "\\Downloads\\ADP\\" + invoic_Id + "\\" + fileName;
                File.WriteAllBytes(Server.MapPath(".") + "\\Downloads\\ADP\\" + invoic_Id + "\\" + fileName, flName);
            }
            if (path == "")
            {
                MessageBox.Show("Document Unable to Upload !");
                txtMasterId.Focus();
                return;
            }
            tbl_PackageADPApproval obj_tbl_PackageADPApproval = new tbl_PackageADPApproval();
            obj_tbl_PackageADPApproval.PackageADPApproval_AddedBy = 1;
            obj_tbl_PackageADPApproval.PackageADPApproval_Additional_Status_Id = 0;
            obj_tbl_PackageADPApproval.PackageADPApproval_Comments = "Approved For Payment";
            obj_tbl_PackageADPApproval.PackageADPApproval_Next_Designation_Id = 0;
            obj_tbl_PackageADPApproval.PackageADPApproval_Next_Organisation_Id = 0;
            obj_tbl_PackageADPApproval.PackageADPApproval_Package_ADP_Id = Convert.ToInt32(txtMasterId.Text);
            obj_tbl_PackageADPApproval.PackageADPApproval_Package_Id = 0;
            obj_tbl_PackageADPApproval.PackageADPApproval_Status = 1;
            obj_tbl_PackageADPApproval.PackageADPApproval_Status_Id = 6;
            obj_tbl_PackageADPApproval.PackageADPApproval_Step_Count = 0;
            obj_tbl_PackageADPApproval.Scheme_Id = Convert.ToInt32(hf_Scheme_Id.Value);

            if (new DataLayer().update_PaymentDoneADP(Convert.ToInt32(txtMasterId.Text), path, obj_tbl_PackageADPApproval))
            {
                MessageBox.Show("Successfully Updated! ");
                return;
            }
            else
            {
                MessageBox.Show("Error in Uploading Document! ");
                return;
            }
        }
        else if (rbtType.SelectedValue == "D")
        {
            if (!flUploadOrder.HasFiles)
            {
                MessageBox.Show("Please Upload document");
                return;
            }
            int invoic_Id = Convert.ToInt32(txtMasterId.Text);
            string extGO = "";
            string fileName = "";
            string path = "";
            Byte[] flName;
            if (flUploadOrder.HasFile)
            {
                flName = flUploadOrder.FileBytes;
                string[] _fname = flUploadOrder.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                extGO = _fname[_fname.Length - 1];
                if (!Directory.Exists(Server.MapPath(".") + "\\Downloads\\DeductionRelease\\"))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + "\\Downloads\\DeductionRelease\\");
                }
                if (!Directory.Exists(Server.MapPath(".") + "\\Downloads\\DeductionRelease\\" + invoic_Id + "\\"))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + "\\Downloads\\DeductionRelease\\" + invoic_Id + "\\");
                }
                fileName = DateTime.Now.Ticks.ToString("x") + "." + extGO;
                path = "\\Downloads\\DeductionRelease\\" + invoic_Id + "\\" + fileName;
                File.WriteAllBytes(Server.MapPath(".") + "\\Downloads\\DeductionRelease\\" + invoic_Id + "\\" + fileName, flName);
            }
            if (path == "")
            {
                MessageBox.Show("Document Unable to Upload !");
                txtMasterId.Focus();
                return;
            }
            tbl_Package_DeductionReleaseApproval obj_tbl_Package_DeductionReleaseApproval = new tbl_Package_DeductionReleaseApproval();
            obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_AddedBy = 1;
            obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Additional_Status_Id = 0;
            obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Comments = "Approved For Payment";
            obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Next_Designation_Id = 0;
            obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Next_Organisation_Id = 0;
            obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Package_DeductionRelease_Id = Convert.ToInt32(txtMasterId.Text);
            obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Package_Id = 0;
            obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Status = 1;
            obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Status_Id = 6;
            obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Step_Count = 0;
            obj_tbl_Package_DeductionReleaseApproval.Scheme_Id = Convert.ToInt32(hf_Scheme_Id.Value);

            if (new DataLayer().update_PaymentDoneDeductionRelease(Convert.ToInt32(txtMasterId.Text), path, obj_tbl_Package_DeductionReleaseApproval))
            {
                MessageBox.Show("Successfully Updated! ");
                return;
            }
            else
            {
                MessageBox.Show("Error in Uploading Document! ");
                return;
            }
        }
    }

    protected void btnUpdateBalance_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds = new DataLayer().get_SNA_Account_No(txtProjectCodeSNA.Text);
        if (AllClasses.CheckDataSet(ds))
        {
            if (ds.Tables[0].Rows.Count == 1)
            {
                int SNAAccountMaster_Id = 0;
                try
                {
                    SNAAccountMaster_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["SNAAccountMaster_Id"].ToString().Trim());
                }
                catch
                {
                    SNAAccountMaster_Id = 0;
                }

                int AssignedLimit = 0;
                try
                {
                    AssignedLimit = Convert.ToInt32(txtAmountSNA.Text.Trim());
                }
                catch
                {
                    AssignedLimit = 0;
                }
                string Account_No = ds.Tables[0].Rows[0]["SNAAccountMaster_ACCT_NO"].ToString().Trim();
                if (SNAAccountMaster_Id > 0 && AssignedLimit > 0 && Account_No != "")
                {
                    DP_Limit obj_DP_Limit = new DP_Limit();
                    obj_DP_Limit.AssignedLimit = AssignedLimit;
                    obj_DP_Limit.SNAAccountMaster_ACCT_NO = Account_No;
                    obj_DP_Limit.SNAAccountMaster_Id = SNAAccountMaster_Id;
                    string Msg = "";
                    if (new DataLayerSNA().setSNA_DP_Limit(obj_DP_Limit, ref Msg))
                    {
                        MessageBox.Show("Set Limit Successfull.");
                    }
                    else
                    {
                        if (Msg != "")
                        {
                            MessageBox.Show(Msg);
                        }
                        else
                        {
                            MessageBox.Show("Unable To Set Limit");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Unable To Set Limit");
                }
            }
            else
            {
                MessageBox.Show("Multiple Account Found! ");
                return;
            }
        }
        else
        {
            MessageBox.Show("Account No Not Found! ");
            return;
        }
    }

    protected void btnUpdateGSTPer_Click(object sender, EventArgs e)
    {
        if (txtMasterId.Text.Trim() == string.Empty)
        {
            MessageBox.Show("Give Invoice Id ");
            txtMasterId.Focus();
            return;
        }

        decimal DD_Val = 0;
        try
        {
            DD_Val = Convert.ToDecimal(ddlGST.SelectedValue.Trim());
        }
        catch
        {
            DD_Val = 0;
        }

        if (new DataLayer().update_GST_Val(Convert.ToInt32(txtMasterId.Text), DD_Val))
        {
            MessageBox.Show("Successfully Updated GST! ");
            return;
        }
        else
        {
            MessageBox.Show("Error in updation! ");
            return;
        }
    }

    protected void btnFundNA_Click(object sender, EventArgs e)
    {
        if (txtMasterId.Text.Trim() == string.Empty)
        {
            MessageBox.Show("Give Invoice Id ");
            txtMasterId.Focus();
            return;
        }

        decimal DD_Val = 0;
        try
        {
            DD_Val = Convert.ToDecimal(txtFundNAmount.Text.Trim());
        }
        catch
        {
            DD_Val = 0;
        }
        tbl_PackageInvoiceAdditional obj_tbl_PackageInvoiceAdditional = new tbl_PackageInvoiceAdditional();
        obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Comments = "";
        obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_Id = 2032;
        obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_isFlat = "1";
        obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_Value_Final = DD_Val;
        obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Deduction_Value_Master = 0;
        obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Invoice_Id = Convert.ToInt32(txtMasterId.Text);
        obj_tbl_PackageInvoiceAdditional.PackageInvoiceAdditional_Status = 1;

        if (new DataLayer().update_Fund_Not_Available(obj_tbl_PackageInvoiceAdditional))
        {
            MessageBox.Show("Successfully Updated! ");
            return;
        }
        else
        {
            MessageBox.Show("Error in updation! ");
            return;
        }
    }

    protected void btnUpdateRA_Click(object sender, EventArgs e)
    {
        if (txtMasterId.Text.Trim() == string.Empty)
        {
            MessageBox.Show("Give Master Id/Invoice Id ");
            txtMasterId.Focus();
            return;
        }
        string RA = txtRABillNo.Text.Trim();

        if (new DataLayer().update_RA_Bill(rbtType.SelectedValue, RA, Convert.ToInt32(txtMasterId.Text)))
        {
            MessageBox.Show("Successfully Updated! ");
            return;
        }
        else
        {
            MessageBox.Show("Error in updation ! ");
            return;
        }
    }

    protected void btnMBNo_Click(object sender, EventArgs e)
    {
        if (txtMasterId.Text.Trim() == string.Empty)
        {
            MessageBox.Show("Give Master Id");
            txtMasterId.Focus();
            return;
        }
        if (rbtType.SelectedValue != "E")
        {
            MessageBox.Show("Physical MB No Can Be Changed Only For EMB");
            txtMasterId.Focus();
            return;
        }
        string RA = txtMBNo.Text.Trim();

        if (new DataLayer().update_MB_No(RA, Convert.ToInt32(txtMasterId.Text)))
        {
            MessageBox.Show("Successfully Updated! ");
            return;
        }
        else
        {
            MessageBox.Show("Error in updation ! ");
            return;
        }
    }
    private void get_tbl_Designation_Loop_Level_Wise(string Type, int Scheme_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Designation_Loop_Level_Wise(Type, Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDesignation, "Designation_DesignationName", "ID_Bind");
        }
        else
        {
            ddlDesignation.Items.Clear();
        }
    }
    protected void btnUpdateLoop_Click(object sender, EventArgs e)
    {
        if (ddlDesignation.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Designation");
            return;
        }
        int invoic_Id = Convert.ToInt32(txtMasterId.Text);
        string[] _DataVal = ddlDesignation.SelectedValue.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        if (rbtType.SelectedValue == "I")
        {
            tbl_PackageInvoiceApproval obj_tbl_PackageInvoiceApproval = new tbl_PackageInvoiceApproval();
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_AddedBy = 1;
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Additional_Status_Id = 0;
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Comments = "";
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Next_Designation_Id = Convert.ToInt32(_DataVal[1]);
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Next_Organisation_Id = Convert.ToInt32(_DataVal[0]);
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_PackageInvoice_Id = Convert.ToInt32(txtMasterId.Text);
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Package_Id = 0;
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Status = 1;
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Status_Id = 1;
            obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Step_Count = Convert.ToInt32(_DataVal[2]);
            obj_tbl_PackageInvoiceApproval.Scheme_Id = Convert.ToInt32(hf_Scheme_Id.Value);

            if (new DataLayer().update_Invoice_Loop(obj_tbl_PackageInvoiceApproval))
            {
                MessageBox.Show("Successfully Updated! ");
                return;
            }
            else
            {
                MessageBox.Show("Error in Updation! ");
                return;
            }
        }
        else if (rbtType.SelectedValue == "A")
        {
            tbl_PackageADPApproval obj_tbl_PackageADPApproval = new tbl_PackageADPApproval();
            obj_tbl_PackageADPApproval.PackageADPApproval_AddedBy = 1;
            obj_tbl_PackageADPApproval.PackageADPApproval_Additional_Status_Id = 0;
            obj_tbl_PackageADPApproval.PackageADPApproval_Comments = "";
            obj_tbl_PackageADPApproval.PackageADPApproval_Next_Designation_Id = Convert.ToInt32(_DataVal[1]);
            obj_tbl_PackageADPApproval.PackageADPApproval_Next_Organisation_Id = Convert.ToInt32(_DataVal[0]);
            obj_tbl_PackageADPApproval.PackageADPApproval_Package_ADP_Id = Convert.ToInt32(txtMasterId.Text);
            obj_tbl_PackageADPApproval.PackageADPApproval_Package_Id = 0;
            obj_tbl_PackageADPApproval.PackageADPApproval_Status = 1;
            obj_tbl_PackageADPApproval.PackageADPApproval_Status_Id = 1;
            obj_tbl_PackageADPApproval.PackageADPApproval_Step_Count = Convert.ToInt32(_DataVal[2]);
            obj_tbl_PackageADPApproval.Scheme_Id = Convert.ToInt32(hf_Scheme_Id.Value);

            if (new DataLayer().update_ADP_Loop(obj_tbl_PackageADPApproval))
            {
                MessageBox.Show("Successfully Updated! ");
                return;
            }
            else
            {
                MessageBox.Show("Error in Updation! ");
                return;
            }
        }
        else if (rbtType.SelectedValue == "D")
        {
            tbl_Package_DeductionReleaseApproval obj_tbl_Package_DeductionReleaseApproval = new tbl_Package_DeductionReleaseApproval();
            obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_AddedBy = 1;
            obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Additional_Status_Id = 0;
            obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Comments = "";
            obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Next_Designation_Id = Convert.ToInt32(_DataVal[1]);
            obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Next_Organisation_Id = Convert.ToInt32(_DataVal[0]);
            obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Package_DeductionRelease_Id = Convert.ToInt32(txtMasterId.Text);
            obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Package_Id = 0;
            obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Status = 1;
            obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Status_Id = 1;
            obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Step_Count = Convert.ToInt32(_DataVal[2]);
            obj_tbl_Package_DeductionReleaseApproval.Scheme_Id = Convert.ToInt32(hf_Scheme_Id.Value);

            if (new DataLayer().update_DeductionRelease_Loop(obj_tbl_Package_DeductionReleaseApproval))
            {
                MessageBox.Show("Successfully Updated! ");
                return;
            }
            else
            {
                MessageBox.Show("Error in Updation! ");
                return;
            }
        }
    }
}
