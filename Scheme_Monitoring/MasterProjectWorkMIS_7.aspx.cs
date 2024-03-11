using Aspose.Pdf;
using ClosedXML.Excel;
using NPOI.SS.Formula.Functions;
using Obout.Ajax.UI.FileUpload;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterProjectWorkMIS_7 : System.Web.UI.Page
{
    private int PageSize = 10;
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
            if (Session["UserType"].ToString() == "1")
            {
                btnUpdate.Visible = true;
            }
            else
            {
                btnUpdate.Visible = false;
            }
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
            get_tbl_Unit();
            if (Request.QueryString.Count > 0)
            {
                int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
                hf_Scheme_Id.Value = Request.QueryString["Id"].ToString().Trim();
                PopulatePager(ProjectWork_Id, 0);
            }
            if (Session["UserType"].ToString() == "1")
            {
                divBOQUploadTool.Visible = true;
            }
            else if (Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()) == 9 || Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()) == 4)
            {
                divBOQUploadTool.Visible = true;
            }
            else
            {
                divBOQUploadTool.Visible = false;
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
    }

    protected void PopulatePager(int ProjectWork_Id, int ProjectWorkPkg_Id)
    {
        hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg(ProjectWork_Id, 0, 0, 0, 0, 0, 0, "", "", false, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            List<ListItem> pages = new List<ListItem>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                pages.Add(new ListItem(" " + ds.Tables[0].Rows[i]["ProjectWorkPkg_Code"].ToString() + " ", ds.Tables[0].Rows[i]["ProjectWorkPkg_Id"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i]["ProjectWorkPkg_Id"].ToString()) != ProjectWorkPkg_Id));
            }
            rptPager.DataSource = pages;
            rptPager.DataBind();
        }
        else
        {
            rptPager.DataSource = null;
            rptPager.DataBind();
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
            Label lblSpecification = e.Row.FindControl("lblSpecification") as Label;
            TextBox txtQtyM = e.Row.FindControl("txtQtyM") as TextBox;
            TextBox txtComments = e.Row.FindControl("txtComments") as TextBox;
            DropDownList ddlVariation = e.Row.FindControl("ddlVariation") as DropDownList;

            txtQtyM.Enabled = false;
            txtComments.Enabled = false;

            lblSpecification.Text = lblSpecification.Text.Replace("\n", "<br />");

            int PackageBOQVariation_Id = 0;
            try
            {
                PackageBOQVariation_Id = Convert.ToInt32(e.Row.Cells[1].Text.Trim());
            }
            catch
            {
                PackageBOQVariation_Id = 0;
            }
            if (PackageBOQVariation_Id > 0)
            {
                txtComments.Enabled = false;
                txtQtyM.Enabled = false;
                ddlVariation.SelectedValue = "Y";
                ddlVariation.Enabled = false;
            }
        }
    }
    private void GetCustomersPageWise(int ProjectWork_Id, int ProjectWorkPkg_Id)
    {
        hf_ProjectWorkPkg_Id.Value = ProjectWorkPkg_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageBOQ(0, ProjectWorkPkg_Id);
        if (ds != null && ds.Tables.Count > 0)
        {
            grdBOQ.DataSource = ds.Tables[0];
            grdBOQ.DataBind();
            this.PopulatePager(ProjectWork_Id, ProjectWorkPkg_Id);

            int Loop = 0;
            if (Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()) == 9)
            {
                Loop = 1;
            }
            else
            {
                Loop = 2;
            }
            hf_Loop.Value = Loop.ToString();

            get_ProcessConfig_Current(Convert.ToInt32(hf_Scheme_Id.Value), Loop.ToString());
        }
        else
        {
            //hf_ProjectWorkPkg_Id.Value = "0";
            divBOQUploadTool.Visible = false;
            grdBOQ.DataSource = null;
            grdBOQ.DataBind();
        }

        get_tbl_PackageBOQ(ProjectWorkPkg_Id);
    }
    protected void grdMultipleFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkDownload = (e.Row.FindControl("lnkDownload") as LinkButton);

            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkDownload);
        }
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
        int ProjectWorkPkg_Id = int.Parse((sender as LinkButton).CommandArgument);
        DataSet ds = new DataSet();
        hf_ProjectWorkPkg_Id.Value = ProjectWorkPkg_Id.ToString();
        ds = new DataLayer().get_PackageExtraItemApproval_Pending(ProjectWorkPkg_Id);
        if (!AllClasses.CheckDataSet(ds))
        {
            if (Session["UserType"].ToString() == "1")
            {
                divActionBOQ1.Visible = true;
                divActionBOQ2.Visible = true;

                GetCustomersPageWise(ProjectWork_Id, ProjectWorkPkg_Id);
            }
            else
            {
                string _Designation_Id = Session["PersonJuridiction_DesignationId"].ToString();

                if (Session["PersonJuridiction_DesignationId"].ToString() == "4" || Session["PersonJuridiction_DesignationId"].ToString() == "9" || Session["PersonJuridiction_DesignationId"].ToString() == "1056")
                {
                    _Designation_Id = "4, 9, 1056";
                }
                if (Session["PersonJuridiction_DesignationId"].ToString() == "1035" || Session["PersonJuridiction_DesignationId"].ToString() == "1040")
                {
                    _Designation_Id = "1035, 1040";
                }
                ds = new DataLayer().get_tbl_ProcessConfigVariationForDesignation(Convert.ToInt32(hf_Scheme_Id.Value), _Designation_Id, Convert.ToInt32(Session["PersonJuridiction_DepartmentId"].ToString()));
                if (AllClasses.CheckDataSet(ds))
                {
                    int Transfer_Allowed = 0;
                    try
                    {
                        Transfer_Allowed = Convert.ToInt32(ds.Tables[0].Rows[0]["ProcessConfigMaster_Transfer_Allowed"].ToString());
                    }
                    catch
                    {
                        Transfer_Allowed = 0;
                    }
                    if (Session["UserType"].ToString() == "1")
                    {
                        chkFinalAtZonal.Visible = true;
                    }
                    else if (Transfer_Allowed == 1)
                    {
                        chkFinalAtZonal.Visible = true;
                    }
                    else
                    {
                        chkFinalAtZonal.Visible = false;
                    }
                    if (Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()) == 4 || Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()) == 9)
                    {
                        divActionBOQ1.Visible = true;
                        divActionBOQ2.Visible = true;
                        hf_IsFirst.Value = "1";
                        GetCustomersPageWise(ProjectWork_Id, ProjectWorkPkg_Id);
                    }
                    else
                    {
                        hf_IsFirst.Value = "0";
                        divActionBOQ1.Visible = false;
                        divActionBOQ2.Visible = false;
                        MessageBox.Show("You Are Not Authorized To Initiate Variation Process!!");
                    }
                }
                else
                {
                    hf_IsFirst.Value = "0";
                    divActionBOQ1.Visible = false;
                    divActionBOQ2.Visible = false;
                    chkFinalAtZonal.Visible = false;
                    MessageBox.Show("You Are Not Authorized To Initiate / Approve Variation Process!!");
                }
            }
        }
        else
        {
            //Variation Generated Check Loop Designation and Open For Forwarding
            ds = new DataLayer().get_PackageExtraItemApproval_CurrentStatus(ProjectWorkPkg_Id);
            if (AllClasses.CheckDataSet(ds))
            {
                hf_IsFirst.Value = "0";
                int Designation_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["PackageExtraItemApproval_Next_Designation_Id"].ToString());
                int Organisation_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["PackageExtraItemApproval_Next_Organisation_Id"].ToString());

                string _Designation_Id = Session["PersonJuridiction_DesignationId"].ToString();

                if (Session["PersonJuridiction_DesignationId"].ToString() == "4" || Session["PersonJuridiction_DesignationId"].ToString() == "9" || Session["PersonJuridiction_DesignationId"].ToString() == "1056")
                {
                    _Designation_Id = "4, 9, 1056";
                }
                if (Session["PersonJuridiction_DesignationId"].ToString() == "1035" || Session["PersonJuridiction_DesignationId"].ToString() == "1040")
                {
                    _Designation_Id = "1035, 1040";
                }

                int Package_ExtraItem_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Package_ExtraItem_Id"].ToString());
                hf_Package_ExtraItem_Id.Value = Package_ExtraItem_Id.ToString();
                get_ProcessConfig_Current(Convert.ToInt32(hf_Scheme_Id.Value), ds.Tables[0].Rows[0]["Package_ExtraItem_Loop"].ToString());
                if (check_Designation_Valid(_Designation_Id, Designation_Id) && Organisation_Id == Convert.ToInt32(Session["PersonJuridiction_DepartmentId"].ToString()))
                {
                    DataSet dsV = new DataSet();
                    if (_Designation_Id == "4, 9, 1056")
                    {
                        dsV = new DataLayer().get_PackageExtra_Variation_Details2(Package_ExtraItem_Id, ProjectWorkPkg_Id);
                    }
                    else
                    {
                        dsV = new DataLayer().get_PackageExtra_Variation_Details(Package_ExtraItem_Id);
                    }
                    bool AdditionalFeildsAssigned = false;
                    if (dsV != null && dsV.Tables.Count > 0 && dsV.Tables[0].Rows.Count > 0)
                    {
                        divActionBOQ1.Visible = true;
                        divActionBOQ2.Visible = true;

                        if (AllClasses.CheckDt(dsV.Tables[0]))
                        {
                            grdBOQ.DataSource = dsV.Tables[0];
                            grdBOQ.DataBind();
                            this.PopulatePager(ProjectWork_Id, ProjectWorkPkg_Id);

                            txtTenderCostRevised.Text = ds.Tables[0].Rows[0]["Package_ExtraItem_NewTenderCost"].ToString();
                            hf_Path.Value = ds.Tables[0].Rows[0]["Package_ExtraItem_ApprovalFilePath"].ToString();
                            hf_Path1.Value = ds.Tables[0].Rows[0]["Package_ExtraItem_ExtraItemFilePath"].ToString();
                            AdditionalFeildsAssigned = true;
                        }
                        else
                        {
                            grdBOQ.DataSource = null;
                            grdBOQ.DataBind();
                        }
                    }
                    if (dsV != null && dsV.Tables.Count > 2 && dsV.Tables[2].Rows.Count > 0)
                    {
                        grdMultipleFiles.DataSource = dsV.Tables[2];
                        grdMultipleFiles.DataBind();
                    }
                    else
                    {
                        grdMultipleFiles.DataSource = null;
                        grdMultipleFiles.DataBind();
                    }
                    if (dsV != null && dsV.Tables.Count > 1 && dsV.Tables[1].Rows.Count > 0)
                    {
                        chkAddExtraItem.Checked = true;
                        divActionBOQExtraItem.Visible = true;
                        grdExtraItem.DataSource = dsV.Tables[1];
                        grdExtraItem.DataBind();
                        ViewState["PackageBOQ"] = dsV.Tables[1];
                        if (!AdditionalFeildsAssigned)
                        {
                            divActionBOQ1.Visible = true;
                            divActionBOQ2.Visible = true;

                            txtTenderCostRevised.Text = dsV.Tables[1].Rows[0]["Package_ExtraItem_NewTenderCost"].ToString();
                            hf_Path.Value = dsV.Tables[1].Rows[0]["Package_ExtraItem_ApprovalFilePath"].ToString();
                            hf_Path1.Value = dsV.Tables[1].Rows[0]["Package_ExtraItem_ExtraItemFilePath"].ToString();
                            AdditionalFeildsAssigned = true;
                        }
                    }
                    else
                    {
                        get_tbl_PackageBOQ(ProjectWorkPkg_Id);
                        if (_Designation_Id == "4, 9, 1056")
                        {
                            divActionBOQExtraItem.Visible = true;
                            get_tbl_PackageBOQ(ProjectWorkPkg_Id);
                        }
                        else
                        {
                            divActionBOQExtraItem.Visible = false;
                            grdExtraItem.DataSource = null;
                            grdExtraItem.DataBind();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Variation Approval is Pending at " + ds.Tables[0].Rows[0]["Designation_DesignationName"].ToString() + ". You can Track Progress In Variation Status Report.");
                    return;
                }
            }
            else
            {
                hf_IsFirst.Value = "0";
                divActionBOQ1.Visible = false;
                divActionBOQ2.Visible = false;
                MessageBox.Show("You Are Not Authorized To Initiate / Approve Variation Process!!");
            }
        }
        if (Session["UserType"].ToString() == "1")
        {
            divPartialSaving.Visible = true;
        }
        else if (hf_IsFirst.Value == "1")
        {
            divPartialSaving.Visible = true;
        }
        else
        {
            divPartialSaving.Visible = false;
        }
    }
    public bool CheckDataSet(DataSet ds)
    {
        bool rVal = false;
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            rVal = true;
        }
        if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            rVal = true;
        }
        return rVal;
    }
    private bool check_Designation_Valid(string _designation_Id, int designation_Id)
    {
        int currentDesignation = 0;
        bool rVal = false;
        string[] DesignationArr = _designation_Id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        if (DesignationArr != null && DesignationArr.Length > 0)
        {
            for (int i = 0; i < DesignationArr.Length; i++)
            {
                currentDesignation = Convert.ToInt32(DesignationArr[i].Trim());
                if (currentDesignation == designation_Id)
                {
                    rVal = true;
                    break;
                }
            }
        }
        return rVal;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int ProjectWork_Id = 0;
        int ProjectWorkPkg_Id = 0;
        int Package_ExtraItem_Id = 0;
        try
        {
            Package_ExtraItem_Id = Convert.ToInt32(hf_Package_ExtraItem_Id.Value);
        }
        catch
        {
            Package_ExtraItem_Id = 0;
        }
        try
        {
            ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
        }
        catch
        {
            ProjectWork_Id = 0;
        }
        try
        {
            ProjectWorkPkg_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
        }
        catch
        {
            ProjectWorkPkg_Id = 0;
        }
        if (ProjectWorkPkg_Id == 0)
        {
            MessageBox.Show("Please Select A Package Code");
            return;
        }
        tbl_PackageExtraItemApproval obj_tbl_PackageExtraItemApproval = new tbl_PackageExtraItemApproval();
        decimal TenderCost_R = 0;
        try
        {
            TenderCost_R = Convert.ToDecimal(txtTenderCostRevised.Text.Trim());
        }
        catch
        {
            TenderCost_R = 0;
        }
        if (!chkEnablePartialSaving.Checked)
        {
            if (!chkNoVariation.Checked)
            {
                if (Package_ExtraItem_Id == 0)
                {
                    if (!flUploadPDF.HasFile)
                    {
                        MessageBox.Show("Please Upload Approval File From SLTC");
                        return;
                    }
                    if (!flUploadExcel.HasFile)
                    {
                        MessageBox.Show("Please Upload BOQ Variation Excel File");
                        return;
                    }
                }
                if (TenderCost_R == 0)
                {
                    MessageBox.Show("Please Fill Revised Tender Cost");
                    return;
                }
                if (ddlStatus.SelectedValue == "0")
                {
                    MessageBox.Show("Please Select Status");
                    return;
                }
            }
        }
        tbl_Package_ExtraItem obj_tbl_Package_ExtraItem = new tbl_Package_ExtraItem();
        List<tbl_PackageBOQVariation> PackageBOQVariation_Li = new List<tbl_PackageBOQVariation>();
        if (!chkNoVariation.Checked)
        {
            try
            {
                obj_tbl_Package_ExtraItem.Package_ExtraItem_Id = Package_ExtraItem_Id;
            }
            catch
            {
                obj_tbl_Package_ExtraItem.Package_ExtraItem_Id = 0;
            }
            try
            {
                obj_tbl_Package_ExtraItem.Package_ExtraItem_ProjectWorkPkg_Id = ProjectWorkPkg_Id;
            }
            catch
            {
                obj_tbl_Package_ExtraItem.Package_ExtraItem_ProjectWorkPkg_Id = 0;
            }
            obj_tbl_Package_ExtraItem.Package_ExtraItem_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_Package_ExtraItem.Package_ExtraItem_Status = 1;
            obj_tbl_Package_ExtraItem.Package_ExtraItem_Loop = Convert.ToInt32(hf_Loop.Value);
            obj_tbl_Package_ExtraItem.Package_ExtraItem_NewTenderCost = TenderCost_R;
            obj_tbl_Package_ExtraItem.Package_ExtraItem_ProcessStatus = "Pending";
            if (flUploadExcel.HasFile)
            {
                obj_tbl_Package_ExtraItem.Package_ExtraItem_ExtraItemFilePath_Bytes = flUploadExcel.FileBytes;
                string[] fNameArr = flUploadExcel.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_Package_ExtraItem.Package_ExtraItem_ExtraItemFilePath_Extention = fNameArr[fNameArr.Length - 1];
            }
            if (flUploadPDF.HasFile)
            {
                obj_tbl_Package_ExtraItem.Package_ExtraItem_ApprovalFilePath_Bytes = flUploadPDF.FileBytes;
                string[] fNameArr1 = flUploadPDF.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                obj_tbl_Package_ExtraItem.Package_ExtraItem_ApprovalFilePath_Extention = fNameArr1[fNameArr1.Length - 1];
            }
            foreach (GridViewRow row in grdBOQ.Rows)
            {
                DropDownList Variation = (DropDownList)row.FindControl("ddlVariation");
                if (Variation.SelectedValue == "Y")
                {
                    tbl_PackageBOQVariation PackageBOQVariation = new tbl_PackageBOQVariation();
                    TextBox txtQtyM = (TextBox)row.FindControl("txtQtyM");
                    TextBox QtyVariation = (TextBox)row.FindControl("txtQtyV");
                    TextBox txtRE = (TextBox)row.FindControl("txtRE");
                    TextBox txtRQ = (TextBox)row.FindControl("txtRQ");
                    TextBox Comments = (TextBox)row.FindControl("txtComments");
                    int PackageBOQ_Id = Convert.ToInt32(row.Cells[0].Text);
                    //int BOQ_Qty = Convert.ToInt32(Convert.ToDecimal(row.Cells[6].Text));
                    try
                    {
                        PackageBOQVariation.PackageBOQVariation_Id = Convert.ToInt32(row.Cells[1].Text.Trim().Replace("&nbsp;", ""));
                    }
                    catch
                    {
                        PackageBOQVariation.PackageBOQVariation_Id = 0;
                    }
                    PackageBOQVariation.PackageBOQVariation_PackageBOQ_Id = PackageBOQ_Id;
                    PackageBOQVariation.PackageBOQVariation_PackageBOQ_Qty = Convert.ToDecimal(txtQtyM.Text);
                    PackageBOQVariation.PackageBOQVariation_RateEstimated = Convert.ToDecimal(txtRE.Text);
                    PackageBOQVariation.PackageBOQVariation_RateQuoted = Convert.ToDecimal(txtRQ.Text);
                    PackageBOQVariation.PackageBOQVariation_Qty = Convert.ToDecimal(QtyVariation.Text);
                    PackageBOQVariation.PackageBOQVariation_Comments = Comments.Text.Trim().Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("'", "");
                    PackageBOQVariation.PackageBOQVariation_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                    PackageBOQVariation.PackageBOQVariation_Package_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
                    PackageBOQVariation.PackageBOQVariation_PackageBOQ_Package_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
                    PackageBOQVariation_Li.Add(PackageBOQVariation);
                }
            }
            bool is_ExtraItemAdded = false;
            if (chkAddExtraItem.Checked)
            {
                for (int i = 0; i < grdExtraItem.Rows.Count; i++)
                {
                    tbl_PackageBOQVariation PackageBOQVariation = new tbl_PackageBOQVariation();
                    DropDownList ddlUnit = (grdExtraItem.Rows[i].FindControl("ddlUnit") as DropDownList);
                    //if (ddlUnit.SelectedValue == "0")
                    //{
                    //    MessageBox.Show("Please Select Unit");
                    //    ddlUnit.Focus();
                    //    return;
                    //}
                    TextBox txtSpecification = (grdExtraItem.Rows[i].FindControl("txtSpecification") as TextBox);
                    if (txtSpecification.Text.Trim().Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("'", "") == "")
                    {
                        MessageBox.Show("Please Provide Specification");
                        txtSpecification.Focus();
                        return;
                    }
                    TextBox txtQtyPaid = (grdExtraItem.Rows[i].FindControl("txtQtyPaid") as TextBox);
                    TextBox txtQty = (grdExtraItem.Rows[i].FindControl("txtQty") as TextBox);
                    if (txtQty.Text.Trim() == "")
                    {
                        MessageBox.Show("Please Provide Quantity");
                        txtQty.Focus();
                        return;
                    }
                    TextBox txtRateEstimate = (grdExtraItem.Rows[i].FindControl("txtRateEstimate") as TextBox);
                    if (txtRateEstimate.Text.Trim() == "")
                    {
                        MessageBox.Show("Please Provide Rate Estimate");
                        txtRateEstimate.Focus();
                        return;
                    }
                    TextBox txtRateQuoted = (grdExtraItem.Rows[i].FindControl("txtRateQuoted") as TextBox);
                    if (txtRateQuoted.Text.Trim() == "")
                    {
                        MessageBox.Show("Please Provide Rate Quoted");
                        txtRateQuoted.Focus();
                        return;
                    }
                    RadioButtonList rbtGSTType = (grdExtraItem.Rows[i].FindControl("rbtGSTType") as RadioButtonList);
                    DropDownList ddlGSTPercentage = (grdExtraItem.Rows[i].FindControl("ddlGSTPercentage") as DropDownList);

                    PackageBOQVariation.PackageBOQVariation_PackageBOQ_Id = 0;
                    PackageBOQVariation.PackageBOQVariation_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                    try
                    {
                        PackageBOQVariation.PackageBOQVariation_Id = Convert.ToInt32(grdExtraItem.Rows[i].Cells[2].Text.Trim().Replace("&nbsp;", ""));
                    }
                    catch
                    {
                        PackageBOQVariation.PackageBOQVariation_Id = 0;
                    }

                    try
                    {
                        PackageBOQVariation.PackageBOQVariation_RateEstimated = decimal.Parse(txtRateEstimate.Text.Trim());
                    }
                    catch
                    {
                        PackageBOQVariation.PackageBOQVariation_RateEstimated = 0;
                    }
                    try
                    {
                        PackageBOQVariation.PackageBOQVariation_RateQuoted = decimal.Parse(txtRateQuoted.Text.Trim());
                    }
                    catch
                    {
                        PackageBOQVariation.PackageBOQVariation_RateQuoted = 0;
                    }
                    try
                    {
                        PackageBOQVariation.PackageBOQVariation_Unit_Id = Convert.ToInt32(ddlUnit.SelectedValue);
                    }
                    catch
                    {
                        PackageBOQVariation.PackageBOQVariation_Unit_Id = 0;
                    }
                    try
                    {
                        PackageBOQVariation.PackageBOQVariation_Qty = decimal.Parse(txtQty.Text.Trim());
                    }
                    catch
                    {
                        PackageBOQVariation.PackageBOQVariation_Qty = 0;
                    }
                    try
                    {
                        PackageBOQVariation.PackageBOQVariation_QtyPaid = decimal.Parse(txtQtyPaid.Text.Trim());
                    }
                    catch
                    {
                        PackageBOQVariation.PackageBOQVariation_QtyPaid = 0;
                    }
                    try
                    {
                        PackageBOQVariation.PackageBOQVariation_AmountEstimated = PackageBOQVariation.PackageBOQVariation_Qty * PackageBOQVariation.PackageBOQVariation_RateEstimated;
                    }
                    catch
                    {
                        PackageBOQVariation.PackageBOQVariation_AmountEstimated = 0;
                    }
                    try
                    {
                        PackageBOQVariation.PackageBOQVariation_AmountQuoted = PackageBOQVariation.PackageBOQVariation_Qty * PackageBOQVariation.PackageBOQVariation_RateQuoted;
                    }
                    catch
                    {
                        PackageBOQVariation.PackageBOQVariation_AmountQuoted = 0;
                    }
                    PackageBOQVariation.PackageBOQVariation_Package_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
                    PackageBOQVariation.PackageBOQVariation_PackageBOQ_Package_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
                    PackageBOQVariation.PackageBOQVariation_Specification = txtSpecification.Text.Trim().Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("'", "");
                    PackageBOQVariation.PackageBOQVariation_GSTType = rbtGSTType.SelectedValue;
                    try
                    {
                        PackageBOQVariation.PackageBOQVariation_GST_Percentage = Convert.ToDecimal(ddlGSTPercentage.SelectedValue);
                    }
                    catch
                    {
                        PackageBOQVariation.PackageBOQVariation_GST_Percentage = 18;
                    }
                    PackageBOQVariation.PackageBOQVariation_Status = 1;
                    if (PackageBOQVariation.PackageBOQVariation_Specification != "")
                    {
                        is_ExtraItemAdded = true;
                        PackageBOQVariation_Li.Add(PackageBOQVariation);
                    }
                }
            }
            if (chkAddExtraItem.Checked && is_ExtraItemAdded == false)
            {
                MessageBox.Show("Please Add At least 1 Extra Item To Save");
                return;
            }

            obj_tbl_PackageExtraItemApproval.PackageExtraItemApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_PackageExtraItemApproval.PackageExtraItemApproval_Comments = "";
            obj_tbl_PackageExtraItemApproval.PackageExtraItemApproval_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            obj_tbl_PackageExtraItemApproval.PackageExtraItemApproval_Status = 1;
            obj_tbl_PackageExtraItemApproval.PackageExtraItemApproval_Status_Id = Convert.ToInt32(ddlStatus.SelectedValue);
            obj_tbl_PackageExtraItemApproval.PackageExtraItemApproval_Step_Count = 0;
            obj_tbl_PackageExtraItemApproval.PackageExtraItemApproval_SchemeId = Convert.ToInt32(hf_Scheme_Id.Value);
            try
            {
                obj_tbl_PackageExtraItemApproval.PackageExtraItemApproval_Loop = Convert.ToInt32(hf_Loop.Value);
            }
            catch
            {
                obj_tbl_PackageExtraItemApproval.PackageExtraItemApproval_Loop = 0;
            }
        }
        else
        {
            obj_tbl_Package_ExtraItem = null;
            PackageBOQVariation_Li = null;
        }
        if (!chkNoVariation.Checked)
        {
            if (PackageBOQVariation_Li.Count == 0)
            {
                MessageBox.Show("Please Update Variation If Any");
                return;
            }
        }
        bool isFirst = false;
        if (hf_IsFirst.Value == "1")
        {
            isFirst = true;
        }
        else
        {
            isFirst = false;
        }
        int EndVariationLifecycle = 0;
        if (chkFinalAtZonal.Visible && chkFinalAtZonal.Checked)
        {
            EndVariationLifecycle = 1;
        }
        else
        {
            EndVariationLifecycle = 0;
        }
        if (chkEnablePartialSaving.Checked)
        {
            if ((new DataLayer()).insert_tbl_PackageBOQVariationTemp(PackageBOQVariation_Li))
            {
                MessageBox.Show("Variation Details Saved Temperory..! Please Complete The Process Once Finalized...");
                return;
            }
            else
            {
                MessageBox.Show("Error In Saving Variation Details!");
                return;
            }
        }

        tbl_ExtraItemDocs obj_tbl_ExtraItemDocs = new tbl_ExtraItemDocs();
        if (flAppraisalNote.HasFile)
        {
            obj_tbl_ExtraItemDocs.ExtraItemDocs_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ExtraItemDocs.ExtraItemDocs_FileBytes = flAppraisalNote.FileBytes;
            string[] _fname = flAppraisalNote.FileName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            obj_tbl_ExtraItemDocs.ExtraItemDocs_FileName = _fname[_fname.Length - 1];
            obj_tbl_ExtraItemDocs.ExtraItemDocs_ExtraItem_Id = obj_tbl_Package_ExtraItem.Package_ExtraItem_Id;
            obj_tbl_ExtraItemDocs.ExtraItemDocs_Status = 1;
        }
        else
        {
            if (ddlStatus.SelectedValue == "2")
            {

            }
            else
            {
                if (!isFirst)
                {
                    MessageBox.Show("Please Upload Appraisal Note Document!");
                    return;
                }
            }
        }
        if ((new DataLayer()).insert_tbl_PackageBOQVariation(PackageBOQVariation_Li, obj_tbl_Package_ExtraItem, ProjectWork_Id, ProjectWorkPkg_Id, chkNoVariation.Checked, obj_tbl_PackageExtraItemApproval, Convert.ToInt32(hf_Scheme_Id.Value), Convert.ToInt32(hf_Loop.Value), Convert.ToInt32(Session["PersonJuridiction_DepartmentId"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), isFirst, EndVariationLifecycle, obj_tbl_ExtraItemDocs))
        {
            MessageBox.Show("Variation Details Saved Successfully!");
            grdExtraItem.DataSource = null;
            grdExtraItem.DataBind();
            ViewState["PackageBOQ"] = null;
            grdBOQ.DataSource = null;
            grdBOQ.DataBind();
            divActionBOQ1.Visible = false;
            divActionBOQ2.Visible = false;
            divActionBOQExtraItem.Visible = false;
            divTenderCostRevised.Visible = false;
            PopulatePager(ProjectWork_Id, 0);
        }
        else
        {
            MessageBox.Show("Error In Saving Variation Details!");
        }
    }
    private void get_tbl_InvoiceStatus(int ConfigMasterId, int Scheme_Id)
    {
        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_Variation(0, 0, 0, 0);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Package_Variation(Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), ConfigMasterId);
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
    private void get_ProcessConfig_Current(int Scheme_Id, string _loop)
    {
        if (Session["UserType"].ToString() == "1")
        {
            get_tbl_InvoiceStatus(0, Scheme_Id);
        }
        else
        {
            DataSet ds = new DataSet();
            int Loop = 0;
            try
            {
                Loop = Convert.ToInt32(_loop);
            }
            catch
            {
                Loop = 1;
            }
            ds = (new DataLayer()).get_ProcessConfig_Current(Scheme_Id, "Variation", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), Loop, Convert.ToInt32(hf_Package_ExtraItem_Id.Value));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int ConfigMaster_Id = 0;
                try
                {
                    ConfigMaster_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ProcessConfigMaster_Id"].ToString());
                    //get_tbl_TradeDocument(ConfigMaster_Id);
                    get_tbl_InvoiceStatus(ConfigMaster_Id, Scheme_Id);

                    get_ProcessConfigMaster_Last(ConfigMaster_Id, Scheme_Id, Loop);
                }
                catch
                {
                    //grdDocumentMaster.DataSource = null;
                    //grdDocumentMaster.DataBind();
                    ConfigMaster_Id = 0;
                    get_tbl_InvoiceStatus(ConfigMaster_Id, Scheme_Id);
                }
                btnSave.Visible = true;
            }
            else
            {
                btnSave.Visible = false;
            }
        }
    }
    private void get_ProcessConfigMaster_Last(int ProcessConfigMaster_Id_Current, int Scheme_Id, int Loop)
    {
        if (Session["UserType"].ToString() == "1")
        {
            btnSave.Visible = true;
        }
        else
        {
            DataSet ds = new DataSet();

            ds = (new DataLayer()).get_ProcessConfigMaster_Last_Variation(Scheme_Id, Loop, null, null);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ProcessConfigMaster_Id_Current == Convert.ToInt32(ds.Tables[0].Rows[0]["ProcessConfigMaster_Id"].ToString()))
                {
                    btnSave.Visible = true;
                }
                else
                {
                    btnSave.Visible = false;
                }
            }
            else
            {
                btnSave.Visible = false;
            }
        }
    }
    protected void btnSave_Click_Final(object sender, EventArgs e)
    {
        bool flag = false;
        int ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
        flag = (new DataLayer()).CompleteMISStep(ProjectWork_Id, Convert.ToInt32(Session["Person_Id"].ToString()));
        if (flag)
        {
            Response.Redirect("MasterProjectWorkMIS.aspx");
        }
        else
        {
            MessageBox.Show("Error In Saving Details!");
        }
    }
    protected void ddlVariation_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlVariation = (sender as DropDownList);
        GridViewRow gr = (sender as DropDownList).Parent.Parent as GridViewRow;

        TextBox txtRE = gr.FindControl("txtRE") as TextBox;
        TextBox txtRQ = gr.FindControl("txtRQ") as TextBox;
        TextBox txtQtyM = gr.FindControl("txtQtyM") as TextBox;
        TextBox txtComments = gr.FindControl("txtComments") as TextBox;
        if (ddlVariation.SelectedValue == "Y")
        {
            txtRE.Enabled = true;
            txtRQ.Enabled = true;
            txtQtyM.Enabled = true;
            txtComments.Enabled = true;
        }
        else
        {
            txtRE.Enabled = false;
            txtRQ.Enabled = false;
            txtQtyM.Enabled = false;
            txtComments.Enabled = false;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (hf_ProjectWorkPkg_Id.Value == "" || hf_ProjectWorkPkg_Id.Value == "0")
        {
            MessageBox.Show("Please Select A Package");
            return;
        }
        if (grdBOQ.Rows.Count == 0)
        {
            MessageBox.Show("Please Select A Package");
            return;
        }
        if (new DataLayer().update_BOQ_Variation(Convert.ToInt32(hf_ProjectWorkPkg_Id.Value)))
        {
            MessageBox.Show("BOQ Variation Updated Successfully");
            return;
        }
        else
        {
            MessageBox.Show("Error In Updation");
            return;
        }
    }

    protected void btnSkip_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterProjectWorkMIS.aspx");
    }

    protected void chkNoVariation_CheckedChanged(object sender, EventArgs e)
    {
        divTenderCostRevised.Visible = !chkNoVariation.Checked;
    }

    protected void chkAddExtraItem_CheckedChanged(object sender, EventArgs e)
    {
        divActionBOQExtraItem.Visible = chkAddExtraItem.Checked;
    }

    protected void grdExtraItem_PreRender(object sender, EventArgs e)
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
    private void get_tbl_PackageBOQ(int Package_Id)
    {
        DataTable dt = new DataTable();
        DataColumn dc0 = new DataColumn("PackageBOQVariation_Id", typeof(int));
        DataColumn dc1 = new DataColumn("PackageBOQ_Package_Id", typeof(int));
        DataColumn dc2 = new DataColumn("PackageBOQ_Unit_Id", typeof(int));
        DataColumn dc3 = new DataColumn("PackageBOQ_Specification", typeof(string));
        DataColumn dc4 = new DataColumn("PackageBOQ_Qty", typeof(decimal));
        DataColumn dc5 = new DataColumn("PackageBOQ_RateEstimated", typeof(decimal));
        DataColumn dc6 = new DataColumn("PackageBOQ_AmountEstimated", typeof(decimal));
        DataColumn dc7 = new DataColumn("PackageBOQ_RateQuoted", typeof(decimal));
        DataColumn dc8 = new DataColumn("PackageBOQ_AmountQuoted", typeof(decimal));
        DataColumn dc9 = new DataColumn("PackageBOQ_QtyPaid", typeof(decimal));
        DataColumn dc10 = new DataColumn("PackageBOQVariation_GSTType", typeof(string));
        DataColumn dc11 = new DataColumn("PackageBOQVariation_GST", typeof(decimal));

        dt.Columns.AddRange(new DataColumn[] { dc0, dc1, dc2, dc3, dc4, dc5, dc6, dc7, dc8, dc9, dc10, dc11 });

        DataRow dr = dt.NewRow();
        dr["PackageBOQ_Package_Id"] = Package_Id;
        dt.Rows.Add(dr);
        ViewState["PackageBOQ"] = dt;
        grdExtraItem.DataSource = dt;
        grdExtraItem.DataBind();
    }

    protected void btnAdd_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = new DataTable();
        if (ViewState["PackageBOQ"] != null)
        {
            dt = (DataTable)ViewState["PackageBOQ"];

            if (AllClasses.CheckDt(dt) && dt.Rows.Count == grdExtraItem.Rows.Count)
            {
                for (int i = 0; i < grdExtraItem.Rows.Count; i++)
                {
                    TextBox txtSpecification = (grdExtraItem.Rows[i].FindControl("txtSpecification") as TextBox);
                    TextBox txtQty = (grdExtraItem.Rows[i].FindControl("txtQty") as TextBox);
                    TextBox txtRateEstimate = (grdExtraItem.Rows[i].FindControl("txtRateEstimate") as TextBox);
                    TextBox txtRateQuoted = (grdExtraItem.Rows[i].FindControl("txtRateQuoted") as TextBox);
                    RadioButtonList rbtGSTType = (grdExtraItem.Rows[i].FindControl("rbtGSTType") as RadioButtonList);
                    DropDownList ddlUnit = (grdExtraItem.Rows[i].FindControl("ddlUnit") as DropDownList);
                    DropDownList ddlGSTPercentage = (grdExtraItem.Rows[i].FindControl("ddlGSTPercentage") as DropDownList);
                    TextBox txtQtyPaid = (grdExtraItem.Rows[i].FindControl("txtQtyPaid") as TextBox);

                    try
                    {
                        dt.Rows[i]["PackageBOQVariation_Id"] = Convert.ToInt32(grdExtraItem.Rows[i].Cells[2].Text.Trim());
                    }
                    catch
                    {
                        dt.Rows[i]["PackageBOQVariation_Id"] = 0;
                    }
                    try
                    {
                        dt.Rows[i]["PackageBOQ_Package_Id"] = Convert.ToInt32(grdExtraItem.Rows[i].Cells[0].Text.Trim());
                    }
                    catch
                    {
                        dt.Rows[i]["PackageBOQ_Package_Id"] = 0;
                    }
                    if (ddlUnit.SelectedValue == "0")
                    {
                        try
                        {
                            dt.Rows[i]["PackageBOQ_Unit_Id"] = Convert.ToInt32(grdExtraItem.Rows[i].Cells[1].Text.Trim());
                        }
                        catch
                        {
                            dt.Rows[i]["PackageBOQ_Unit_Id"] = 0;
                        }
                    }
                    else
                    {
                        try
                        {
                            dt.Rows[i]["PackageBOQ_Unit_Id"] = Convert.ToInt32(ddlUnit.SelectedValue);
                        }
                        catch
                        {
                            dt.Rows[i]["PackageBOQ_Unit_Id"] = 0;
                        }
                    }

                    dt.Rows[i]["PackageBOQ_Specification"] = txtSpecification.Text.Trim();
                    try
                    {
                        dt.Rows[i]["PackageBOQ_Qty"] = Convert.ToDecimal(txtQty.Text.Trim());
                    }
                    catch
                    {
                        dt.Rows[i]["PackageBOQ_Qty"] = 0;
                    }
                    try
                    {
                        dt.Rows[i]["PackageBOQ_RateEstimated"] = Convert.ToDecimal(txtRateEstimate.Text.Trim());
                    }
                    catch
                    {
                        dt.Rows[i]["PackageBOQ_RateEstimated"] = 0;
                    }
                    try
                    {
                        dt.Rows[i]["PackageBOQ_RateQuoted"] = Convert.ToDecimal(txtRateQuoted.Text.Trim());
                    }
                    catch
                    {
                        dt.Rows[i]["PackageBOQ_RateQuoted"] = 0;
                    }
                    try
                    {
                        dt.Rows[i]["PackageBOQ_QtyPaid"] = Convert.ToDecimal(txtQtyPaid.Text.Trim());
                    }
                    catch
                    {
                        dt.Rows[i]["PackageBOQ_QtyPaid"] = 0;
                    }
                    try
                    {
                        dt.Rows[i]["PackageBOQVariation_GSTType"] = rbtGSTType.SelectedValue;
                    }
                    catch
                    {
                        dt.Rows[i]["PackageBOQVariation_GSTType"] = "Exclude GST";
                    }
                    if (ddlGSTPercentage.SelectedValue == "0")
                    {
                        try
                        {
                            dt.Rows[i]["PackageBOQVariation_GST"] = Convert.ToInt32(grdExtraItem.Rows[i].Cells[4].Text.Trim());
                        }
                        catch
                        {
                            dt.Rows[i]["PackageBOQVariation_GST"] = 18;
                        }
                    }
                    else
                    {
                        try
                        {
                            dt.Rows[i]["PackageBOQVariation_GST"] = Convert.ToInt32(ddlGSTPercentage.SelectedValue);
                        }
                        catch
                        {
                            dt.Rows[i]["PackageBOQVariation_GST"] = 18;
                        }
                    }
                }
            }

            DataRow dr = dt.NewRow();
            dr["PackageBOQ_Package_Id"] = 0;
            dt.Rows.Add(dr);
            ViewState["PackageBOQ"] = dt;

            grdExtraItem.DataSource = dt;
            grdExtraItem.DataBind();
        }
    }

    protected void btnMinus_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = new DataTable();
        if (ViewState["PackageBOQ"] != null)
        {
            dt = (DataTable)ViewState["PackageBOQ"];
            if (AllClasses.CheckDt(dt) && dt.Rows.Count == grdExtraItem.Rows.Count)
            {
                for (int i = 0; i < grdExtraItem.Rows.Count; i++)
                {
                    TextBox txtSpecification = (grdExtraItem.Rows[i].FindControl("txtSpecification") as TextBox);
                    TextBox txtQty = (grdExtraItem.Rows[i].FindControl("txtQty") as TextBox);
                    TextBox txtRateEstimate = (grdExtraItem.Rows[i].FindControl("txtRateEstimate") as TextBox);
                    TextBox txtRateQuoted = (grdExtraItem.Rows[i].FindControl("txtRateQuoted") as TextBox);
                    RadioButtonList rbtGSTType = (grdExtraItem.Rows[i].FindControl("rbtGSTType") as RadioButtonList);
                    DropDownList ddlUnit = (grdExtraItem.Rows[i].FindControl("ddlUnit") as DropDownList);
                    DropDownList ddlGSTPercentage = (grdExtraItem.Rows[i].FindControl("ddlGSTPercentage") as DropDownList);
                    TextBox txtQtyPaid = (grdExtraItem.Rows[i].FindControl("txtQtyPaid") as TextBox);

                    try
                    {
                        dt.Rows[i]["PackageBOQVariation_Id"] = Convert.ToInt32(grdExtraItem.Rows[i].Cells[2].Text.Trim());
                    }
                    catch
                    {
                        dt.Rows[i]["PackageBOQVariation_Id"] = 0;
                    }
                    try
                    {
                        dt.Rows[i]["PackageBOQ_Package_Id"] = Convert.ToInt32(grdExtraItem.Rows[i].Cells[0].Text.Trim());
                    }
                    catch
                    {
                        dt.Rows[i]["PackageBOQ_Package_Id"] = 0;
                    }
                    if (ddlUnit.SelectedValue == "0")
                    {
                        try
                        {
                            dt.Rows[i]["PackageBOQ_Unit_Id"] = Convert.ToInt32(grdExtraItem.Rows[i].Cells[1].Text.Trim());
                        }
                        catch
                        {
                            dt.Rows[i]["PackageBOQ_Unit_Id"] = 0;
                        }
                    }
                    else
                    {
                        try
                        {
                            dt.Rows[i]["PackageBOQ_Unit_Id"] = Convert.ToInt32(ddlUnit.SelectedValue);
                        }
                        catch
                        {
                            dt.Rows[i]["PackageBOQ_Unit_Id"] = 0;
                        }
                    }

                    dt.Rows[i]["PackageBOQ_Specification"] = txtSpecification.Text.Trim();
                    try
                    {
                        dt.Rows[i]["PackageBOQ_Qty"] = Convert.ToDecimal(txtQty.Text.Trim());
                    }
                    catch
                    {
                        dt.Rows[i]["PackageBOQ_Qty"] = 0;
                    }
                    try
                    {
                        dt.Rows[i]["PackageBOQ_RateEstimated"] = Convert.ToDecimal(txtRateEstimate.Text.Trim());
                    }
                    catch
                    {
                        dt.Rows[i]["PackageBOQ_RateEstimated"] = 0;
                    }
                    try
                    {
                        dt.Rows[i]["PackageBOQ_RateQuoted"] = Convert.ToDecimal(txtRateQuoted.Text.Trim());
                    }
                    catch
                    {
                        dt.Rows[i]["PackageBOQ_RateQuoted"] = 0;
                    }
                    try
                    {
                        dt.Rows[i]["PackageBOQ_QtyPaid"] = Convert.ToDecimal(txtQtyPaid.Text.Trim());
                    }
                    catch
                    {
                        dt.Rows[i]["PackageBOQ_QtyPaid"] = 0;
                    }
                    try
                    {
                        dt.Rows[i]["PackageBOQVariation_GSTType"] = rbtGSTType.SelectedValue;
                    }
                    catch
                    {
                        dt.Rows[i]["PackageBOQVariation_GSTType"] = "Exclude GST";
                    }
                    if (ddlGSTPercentage.SelectedValue == "0")
                    {
                        try
                        {
                            dt.Rows[i]["PackageBOQVariation_GST"] = Convert.ToInt32(grdExtraItem.Rows[i].Cells[4].Text.Trim());
                        }
                        catch
                        {
                            dt.Rows[i]["PackageBOQVariation_GST"] = 18;
                        }
                    }
                    else
                    {
                        try
                        {
                            dt.Rows[i]["PackageBOQVariation_GST"] = Convert.ToInt32(ddlGSTPercentage.SelectedValue);
                        }
                        catch
                        {
                            dt.Rows[i]["PackageBOQVariation_GST"] = 18;
                        }
                    }
                }
            }
            if (dt.Rows.Count > 1)
            {
                dt.Rows.RemoveAt(dt.Rows.Count - 1);

                grdExtraItem.DataSource = dt;
                grdExtraItem.DataBind();
            }
        }
    }
    private void get_tbl_Unit()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Unit();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["Unit"] = ds.Tables[0];
        }
        else
        {

        }
    }
    protected void grdExtraItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlUnit = e.Row.FindControl("ddlUnit") as DropDownList;
            RadioButtonList rbtGSTType = e.Row.FindControl("rbtGSTType") as RadioButtonList;
            DropDownList ddlGSTPercentage = e.Row.FindControl("ddlGSTPercentage") as DropDownList;
            int Unit_Id = 0;
            try
            {
                Unit_Id = Convert.ToInt32(e.Row.Cells[1].Text.Trim());
            }
            catch
            {
                Unit_Id = 0;
            }
            string GSTType = "";
            GSTType = e.Row.Cells[3].Text.Trim();
            decimal GST = 0;
            try
            {
                GST = Convert.ToDecimal(e.Row.Cells[4].Text.Trim());
            }
            catch
            {
                GST = 0;
            }
            int PackageBOQVariation_Id = 0;
            try
            {
                PackageBOQVariation_Id = Convert.ToInt32(e.Row.Cells[2].Text.Trim());
            }
            catch
            {
                PackageBOQVariation_Id = 0;
            }
            try
            {
                ddlGSTPercentage.SelectedValue = GST.ToString();
            }
            catch
            {

            }
            try
            {
                rbtGSTType.SelectedValue = GSTType;
            }
            catch
            {

            }
            if (ViewState["Unit"] != null)
            {
                if (e.Row.RowIndex == 0)
                    AllClasses.FillDropDown((DataTable)ViewState["Unit"], ddlUnit, "Unit_Name", "Unit_Id");
                else
                    AllClasses.FillDropDown_WithOutSelect((DataTable)ViewState["Unit"], ddlUnit, "Unit_Name", "Unit_Id");
                try
                {
                    ddlUnit.SelectedValue = Unit_Id.ToString();
                }
                catch
                {

                }
            }
            TextBox txtSpecification = e.Row.FindControl("txtSpecification") as TextBox;
            TextBox txtQty = e.Row.FindControl("txtQty") as TextBox;
            TextBox txtRateEstimate = e.Row.FindControl("txtRateEstimate") as TextBox;
            TextBox txtRateQuoted = e.Row.FindControl("txtRateQuoted") as TextBox;
            TextBox txtQtyPaid = e.Row.FindControl("txtQtyPaid") as TextBox;

            if (PackageBOQVariation_Id > 0)
            {
                txtSpecification.Enabled = false;
                txtQty.Enabled = false;
                txtRateEstimate.Enabled = false;
                txtRateQuoted.Enabled = false;
                txtQtyPaid.Enabled = false;
            }
        }
    }

    protected void btnDeletePartialSavedData_Click(object sender, EventArgs e)
    {
        int WorkPkg_Id = 0;
        try
        {
            WorkPkg_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
        }
        catch
        {
            WorkPkg_Id = 0;
        }
        if (WorkPkg_Id > 0)
        {
            if (new DataLayer().Delete_tbl_PackageBOQVariationTemp(WorkPkg_Id))
            {
                MessageBox.Show("Temperory Data Deleted Successfully");
                return;
            }
            else
            {
                MessageBox.Show("Error In Deleting Temperory Data");
                return;
            }
        }
        else
        {
            MessageBox.Show("Temperory Data Not Found");
            return;
        }
    }

    protected void chkLoadFromPartialSavedData_CheckedChanged(object sender, EventArgs e)
    {
        int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
        int ProjectWorkPkg_Id = 0;
        try
        {
            ProjectWorkPkg_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
        }
        catch
        {
            ProjectWorkPkg_Id = 0;
        }
        DataSet dsV = new DataSet();
        dsV = new DataLayer().get_PackageExtra_Variation_Details_Temp(ProjectWorkPkg_Id);
        if (CheckDataSet(dsV))
        {
            divActionBOQ1.Visible = true;
            divActionBOQ2.Visible = true;
            if (AllClasses.CheckDt(dsV.Tables[0]))
            {
                grdBOQ.DataSource = dsV.Tables[0];
                grdBOQ.DataBind();
                this.PopulatePager(ProjectWork_Id, ProjectWorkPkg_Id);

                for (int i = 0; i < grdBOQ.Rows.Count; i++)
                {
                    DropDownList ddlVariation = (grdBOQ.Rows[i].FindControl("ddlVariation") as DropDownList);
                    TextBox txtRE = (grdBOQ.Rows[i].FindControl("txtRE") as TextBox);
                    TextBox txtRQ = (grdBOQ.Rows[i].FindControl("txtRQ") as TextBox);
                    TextBox txtQtyM = (grdBOQ.Rows[i].FindControl("txtQtyM") as TextBox);
                    TextBox txtComments = (grdBOQ.Rows[i].FindControl("txtComments") as TextBox);

                    int PackageBOQVariationTemp_Id = 0;
                    try
                    {
                        PackageBOQVariationTemp_Id = Convert.ToInt32(grdBOQ.Rows[i].Cells[11].Text.Trim());
                    }
                    catch
                    {
                        PackageBOQVariationTemp_Id = 0;
                    }
                    if (PackageBOQVariationTemp_Id > 0)
                    {
                        ddlVariation.SelectedValue = "Y";
                        txtRE.Enabled = true;
                        txtRQ.Enabled = true;
                        txtQtyM.Enabled = true;
                        txtComments.Enabled = true;
                    }
                }
            }
            else
            {
                grdBOQ.DataSource = null;
                grdBOQ.DataBind();
            }
            if (dsV != null && dsV.Tables.Count > 1 && dsV.Tables[1].Rows.Count > 0)
            {
                chkAddExtraItem.Checked = true;
                divActionBOQExtraItem.Visible = true;
                ViewState["PackageBOQ"] = dsV.Tables[1];
                grdExtraItem.DataSource = dsV.Tables[1];
                grdExtraItem.DataBind();
            }
            else
            {
                divActionBOQExtraItem.Visible = true;
                get_tbl_PackageBOQ(ProjectWorkPkg_Id);
            }
        }
        else
        {
            MessageBox.Show("Variation Details Not Found");
            return;
        }
    }

    protected void btnDownloadVariationBOQ_Click(object sender, EventArgs e)
    {
        int ProjectWorkPkg_Id = 0;
        try
        {
            ProjectWorkPkg_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
        }
        catch
        {
            ProjectWorkPkg_Id = 0;
        }
        if (ProjectWorkPkg_Id > 0)
        {
            DataSet ds = new DataSet();
            ds = (new DataLayer()).get_PackageBOQ(ProjectWorkPkg_Id);
            if (AllClasses.CheckDataSet(ds))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["PackageBOQ_Specification"] = AllClasses.SanitizeText(ds.Tables[0].Rows[i]["PackageBOQ_Specification"].ToString()).Trim();
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(ds.Tables[0], ProjectWorkPkg_Id.ToString());

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + ProjectWorkPkg_Id.ToString() + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            else
            {
                MessageBox.Show("No Records Found");
            }
        }
        else
        {
            MessageBox.Show("Please Select A Package To Download BOQ Sample Excel");
            return;
        }
    }
    public DataTable OnPostImportFromExcel(FileInfo flExcel)
    {
        DataTable dtrVal = null;
        string connString = "";
        if (flExcel.Extension.ToLower() == "xls")
        {
            //connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + flExcel.FullName + ";Extended Properties=Excel 8.0";
            connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + flExcel.FullName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";";
        }
        else
        {
            //connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + flExcel.FullName + ";Extended Properties=Excel 12.0";
            connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + flExcel.FullName + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";";
        }
        // Create the connection object
        OleDbConnection oledbConn = new OleDbConnection(connString);
        try
        {
            // Open connection
            oledbConn.Open();

            //Get Sheet Name
            DataTable dt = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            String[] excelSheets = new String[dt.Rows.Count];
            int i = 0;

            // Add the sheet name to the string array.
            foreach (DataRow row in dt.Rows)
            {
                excelSheets[i] = row["TABLE_NAME"].ToString();
                i++;
            }

            OleDbCommand cmd = null;
            // Create OleDbCommand object and select data from worksheet Sheet1
            cmd = new OleDbCommand("SELECT * FROM [" + excelSheets[0] + "]", oledbConn);

            // Create new OleDbDataAdapter
            OleDbDataAdapter oleda = new OleDbDataAdapter();

            oleda.SelectCommand = cmd;

            // Create a DataSet which will hold the data extracted from the worksheet.
            DataSet ds = new DataSet();

            // Fill the DataSet from the data extracted from the worksheet.
            oleda.Fill(ds);

            dtrVal = ds.Tables[0];
        }
        catch (Exception ee)
        {
            dtrVal = null;
            MessageBox.Show(ee.Message);
        }
        finally
        {
            // Close connection
            oledbConn.Close();
        }
        return dtrVal;
    }

    protected void btnUploadData_Click(object sender, EventArgs e)
    {
        int ProjectWork_Id = 0;
        int ProjectWorkPkg_Id = 0;
        int Package_ExtraItem_Id = 0;
        try
        {
            Package_ExtraItem_Id = Convert.ToInt32(hf_Package_ExtraItem_Id.Value);
        }
        catch
        {
            Package_ExtraItem_Id = 0;
        }
        try
        {
            ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
        }
        catch
        {
            ProjectWork_Id = 0;
        }
        try
        {
            ProjectWorkPkg_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
        }
        catch
        {
            ProjectWorkPkg_Id = 0;
        }
        if (ProjectWorkPkg_Id == 0)
        {
            MessageBox.Show("Please Select A Package Code");
            return;
        }
        List<tbl_PackageBOQVariation> PackageBOQVariation_Li = new List<tbl_PackageBOQVariation>();
        if (flUploadVariationEntry.HasFile)
        {
            string fileName = DateTime.Now.Ticks.ToString("x") + flUploadVariationEntry.FileName;
            string filePathFull = Server.MapPath(".") + "\\BOQ\\" + fileName;
            string filePath = "\\BOQ\\" + flUploadVariationEntry.FileName;
            string File = Path.GetFileNameWithoutExtension(flUploadVariationEntry.FileName);
            flUploadVariationEntry.SaveAs(filePathFull);

            FileInfo fl = new FileInfo(filePathFull);
            if (fl.Exists)
            {
                DataTable dt = OnPostImportFromExcel(fl);
                if (AllClasses.CheckDt(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tbl_PackageBOQVariation PackageBOQVariation = new tbl_PackageBOQVariation();
                        int PackageBOQ_Id = Convert.ToInt32(dt.Rows[i]["PackageBOQ_Id"].ToString());
                        PackageBOQVariation.PackageBOQVariation_Id = 0;
                        PackageBOQVariation.PackageBOQVariation_PackageBOQ_Id = PackageBOQ_Id;
                        try
                        {
                            PackageBOQVariation.PackageBOQVariation_PackageBOQ_Qty = Convert.ToDecimal(dt.Rows[i]["PackageBOQ_Qty"].ToString());
                        }
                        catch
                        {
                            PackageBOQVariation.PackageBOQVariation_PackageBOQ_Qty = 0;
                        }
                        try
                        {
                            PackageBOQVariation.PackageBOQVariation_RateEstimated = Convert.ToDecimal(dt.Rows[i]["Updated_RateEstimated_After_Variation"].ToString());
                            if (PackageBOQVariation.PackageBOQVariation_RateEstimated == 0)
                            {
                                try
                                {
                                    PackageBOQVariation.PackageBOQVariation_RateEstimated = Convert.ToDecimal(dt.Rows[i]["PackageBOQ_RateEstimated"].ToString());
                                }
                                catch
                                {

                                }
                            }
                        }
                        catch
                        {
                            continue;
                        }
                        try
                        {
                            PackageBOQVariation.PackageBOQVariation_RateQuoted = Convert.ToDecimal(dt.Rows[i]["Updated_RateQuoted_After_Variation"].ToString());
                            if (PackageBOQVariation.PackageBOQVariation_RateQuoted == 0)
                            {
                                try
                                {
                                    PackageBOQVariation.PackageBOQVariation_RateQuoted = Convert.ToDecimal(dt.Rows[i]["PackageBOQ_RateQuoted"].ToString());
                                }
                                catch
                                {

                                }
                            }
                        }
                        catch
                        {
                            continue;
                        }
                        try
                        {
                            PackageBOQVariation.PackageBOQVariation_Qty = Convert.ToDecimal(dt.Rows[i]["Updated_Qty_After_Variation"].ToString());
                        }
                        catch
                        {
                            continue;
                        }

                        PackageBOQVariation.PackageBOQVariation_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                        PackageBOQVariation.PackageBOQVariation_Package_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
                        PackageBOQVariation.PackageBOQVariation_PackageBOQ_Package_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
                        PackageBOQVariation_Li.Add(PackageBOQVariation);
                    }
                }
            }
        }

        if (dlUploadExtraItemEntry.HasFile)
        {
            string fileName = DateTime.Now.Ticks.ToString("x") + dlUploadExtraItemEntry.FileName;
            string filePathFull = Server.MapPath(".") + "\\BOQ\\" + fileName;
            string filePath = "\\BOQ\\" + dlUploadExtraItemEntry.FileName;
            string File = Path.GetFileNameWithoutExtension(dlUploadExtraItemEntry.FileName);
            dlUploadExtraItemEntry.SaveAs(filePathFull);

            FileInfo fl = new FileInfo(filePathFull);
            if (fl.Exists)
            {
                DataTable dt = OnPostImportFromExcel(fl);
                if (AllClasses.CheckDt(dt))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tbl_PackageBOQVariation PackageBOQVariation = new tbl_PackageBOQVariation();

                        PackageBOQVariation.PackageBOQVariation_PackageBOQ_Id = 0;
                        PackageBOQVariation.PackageBOQVariation_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                        PackageBOQVariation.PackageBOQVariation_Id = 0;

                        try
                        {
                            PackageBOQVariation.PackageBOQVariation_RateEstimated = decimal.Parse(dt.Rows[i]["Tender Rate"].ToString());
                        }
                        catch
                        {
                            PackageBOQVariation.PackageBOQVariation_RateEstimated = 0;
                        }
                        try
                        {
                            PackageBOQVariation.PackageBOQVariation_RateQuoted = decimal.Parse(dt.Rows[i]["Contractor Agreed Rate"].ToString());
                        }
                        catch
                        {
                            PackageBOQVariation.PackageBOQVariation_RateQuoted = 0;
                        }
                        PackageBOQVariation.PackageBOQVariation_Unit = dt.Rows[i]["Unit"].ToString();
                        try
                        {
                            PackageBOQVariation.PackageBOQVariation_Qty = decimal.Parse(dt.Rows[i]["Quantity"].ToString());
                        }
                        catch
                        {
                            PackageBOQVariation.PackageBOQVariation_Qty = 0;
                        }
                        PackageBOQVariation.PackageBOQVariation_QtyPaid = 0;
                        try
                        {
                            PackageBOQVariation.PackageBOQVariation_AmountEstimated = PackageBOQVariation.PackageBOQVariation_Qty * PackageBOQVariation.PackageBOQVariation_RateEstimated;
                        }
                        catch
                        {
                            PackageBOQVariation.PackageBOQVariation_AmountEstimated = 0;
                        }
                        try
                        {
                            PackageBOQVariation.PackageBOQVariation_AmountQuoted = PackageBOQVariation.PackageBOQVariation_Qty * PackageBOQVariation.PackageBOQVariation_RateQuoted;
                        }
                        catch
                        {
                            PackageBOQVariation.PackageBOQVariation_AmountQuoted = 0;
                        }
                        PackageBOQVariation.PackageBOQVariation_Package_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
                        PackageBOQVariation.PackageBOQVariation_PackageBOQ_Package_Id = Convert.ToInt32(hf_ProjectWorkPkg_Id.Value);
                        PackageBOQVariation.PackageBOQVariation_Specification = dt.Rows[i]["BOQ Description"].ToString().Trim().Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("'", "");
                        PackageBOQVariation.PackageBOQVariation_GSTType = dt.Rows[i]["GST Type"].ToString();
                        try
                        {
                            PackageBOQVariation.PackageBOQVariation_GST_Percentage = Convert.ToDecimal(dt.Rows[i]["GST"].ToString());
                        }
                        catch
                        {
                            PackageBOQVariation.PackageBOQVariation_GST_Percentage = 18;
                        }
                        PackageBOQVariation.PackageBOQVariation_Status = 1;
                        if (PackageBOQVariation.PackageBOQVariation_Specification != "")
                        {
                            PackageBOQVariation_Li.Add(PackageBOQVariation);
                        }
                    }
                }
            }
        }

        if ((new DataLayer()).insert_tbl_PackageBOQVariationTemp(PackageBOQVariation_Li))
        {
            MessageBox.Show("Variation Details Saved Temperory..! Please Complete The Process Once Finalized...");
            return;
        }
        else
        {
            MessageBox.Show("Error In Saving Variation Details!");
            return;
        }
    }
}
