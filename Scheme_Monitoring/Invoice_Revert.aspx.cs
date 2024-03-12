using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Invoice_Revert : System.Web.UI.Page
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
            get_tbl_Project();
            get_Branch_Office_Details();
        }
    }
    private void get_Branch_Office_Details()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Branch_Office_Details(0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlOrgnization, "OfficeBranch_Name", "OfficeBranch_Id");
        }
        else
        {
            ddlOrgnization.Items.Clear();
        }
    }
    private void get_tbl_Designation(int Scheme_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_DesignationTemp(Scheme_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDesignation, "Designation_DesignationName", "Designation_Id");
        }
        else
        {
            ddlDesignation.Items.Clear();
        }
    }
    private void load_dashboard()
    {
        get_tbl_PackageInvoice();
        get_tbl_PackageInvoiceADP();
        get_tbl_PackageInvoiceMA();
        get_tbl_Package_DeductionRelease();
        
        set_Labels();
    }
    private void set_Labels()
    {
        sp_Invoice.InnerHtml = grdInvoice.Rows.Count.ToString();
        sp_OtherDept.InnerHtml = grdADP.Rows.Count.ToString();
        sp_OtherPayment.InnerHtml = grdMA.Rows.Count.ToString();
        sp_DeductionRelease.InnerHtml = grdDeductionRelease.Rows.Count.ToString();
    }
    private void get_tbl_Project()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Project_Temp();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlScheme, "Project_Name", "Project_Id");
            try
            {
                ddlScheme.SelectedValue = Session["Default_Scheme"].ToString();
                ddlScheme_SelectedIndexChanged(ddlScheme, new EventArgs());
            }
            catch
            {

            }
        }
        else
        {
            ddlScheme.Items.Clear();
        }
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

    private void get_tbl_PackageInvoiceADP()
    {        
        int Designation_Id = 0;
        try
        {
            Designation_Id = Convert.ToInt32(ddlDesignation.SelectedValue);
        }
        catch
        {
            Designation_Id = 0;
        }
        int Organization_Id = 0;
        try
        {
            Organization_Id = Convert.ToInt32(ddlOrgnization.SelectedValue);
        }
        catch
        {
            Organization_Id = 0;
        }

        int Expert_Person_Id = 0;
        if (Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()) == 33)
        {
            Expert_Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        }
        else
        {
            Expert_Person_Id = 0;
        }
        bool? isDefered = null;
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Package_ADP(0, "1013", 0, 0, 0, 0, 0, "", "", Organization_Id, Designation_Id, false, "", "", Expert_Person_Id, 0, 0, false, isDefered, 0);

        if (AllClasses.CheckDataSet(ds))
        {
            grdADP.DataSource = ds.Tables[0];
            grdADP.DataBind();
        }
        else
        {
            grdADP.DataSource = null;
            grdADP.DataBind();
        }
    }

    private void get_tbl_PackageInvoiceMA()
    {        
        int Designation_Id = 0;
        try
        {
            Designation_Id = Convert.ToInt32(ddlDesignation.SelectedValue);
        }
        catch
        {
            Designation_Id = 0;
        }
        int Organization_Id = 0;
        try
        {
            Organization_Id = Convert.ToInt32(ddlOrgnization.SelectedValue);
        }
        catch
        {
            Organization_Id = 0;
        }
        bool? isDefered = null;
        DataSet ds = new DataSet();
        int Expert_Person_Id = 0;
        if (Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()) == 33)
        {
            Expert_Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        }
        else
        {
            Expert_Person_Id = 0;
        }
        ds = (new DataLayer()).get_tbl_Package_MobilizationAdvance(0, "1013", 0, 0, 0, 0, 0, "", "", Organization_Id, Designation_Id, false, "", "", Expert_Person_Id, 0, 0, false, isDefered, 0);

        if (AllClasses.CheckDataSet(ds))
        {
            grdMA.DataSource = ds.Tables[0];
            grdMA.DataBind();
        }
        else
        {
            grdMA.DataSource = null;
            grdMA.DataBind();
        }
    }

    private void get_tbl_Package_DeductionRelease()
    {        
        int Designation_Id = 0;
        try
        {
            Designation_Id = Convert.ToInt32(ddlDesignation.SelectedValue);
        }
        catch
        {
            Designation_Id = 0;
        }
        int Organization_Id = 0;
        try
        {
            Organization_Id = Convert.ToInt32(ddlOrgnization.SelectedValue);
        }
        catch
        {
            Organization_Id = 0;
        }
        int Expert_Person_Id = 0;
        if (Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()) == 33)
        {
            Expert_Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        }
        else
        {
            Expert_Person_Id = 0;
        }
        bool? isDefered = null;
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Package_DeductionRelease(0, "1013", 0, 0, 0, 0, 0, "", "", Organization_Id, Designation_Id, false, "", "", Expert_Person_Id, 0, 0, false, isDefered, 0);

        if (AllClasses.CheckDataSet(ds))
        {
            grdDeductionRelease.DataSource = ds.Tables[0];
            grdDeductionRelease.DataBind();
        }
        else
        {
            grdDeductionRelease.DataSource = null;
            grdDeductionRelease.DataBind();
        }
    }

    private void get_tbl_PackageInvoice()
    {
        int Designation_Id = 0;
        try
        {
            Designation_Id = Convert.ToInt32(ddlDesignation.SelectedValue);
        }
        catch
        {
            Designation_Id = 0;
        }
        int Organization_Id = 0;
        try
        {
            Organization_Id = Convert.ToInt32(ddlOrgnization.SelectedValue);
        }
        catch
        {
            Organization_Id = 0;
        }
        int Expert_Person_Id = 0;
        if (Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()) == 33)
        {
            Expert_Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        }
        else
        {
            Expert_Person_Id = 0;
        }
        bool? isDefered = null;
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageInvoice(0, 0, 0, 0, "1013", Organization_Id, Designation_Id, false, "", "", Expert_Person_Id, 0, -1, false, -1, isDefered, "", 0, 0);

        if (AllClasses.CheckDataSet(ds))
        {
            grdInvoice.DataSource = ds.Tables[0];
            grdInvoice.DataBind();
        }
        else
        {
            grdInvoice.DataSource = null;
            grdInvoice.DataBind();
        }
    }

    protected void btnOpenInvoice_Click(object sender, ImageClickEventArgs e)
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
        string Package_Id = gr.Cells[1].Text.Trim();
        string Scheme_Id = gr.Cells[6].Text.Trim();
        string PackageInvoice_Type = gr.Cells[31].Text.Trim();
        if (Invoice_Id == 0)
        {
            if (Session["Invoice_C"] == null)
            {
                if (PackageInvoice_Type == "N")
                {
                    Response.Redirect("MasterGenerateInvoice_Detail_New.aspx?Package_Id=" + Package_Id + "&Invoice_Id=" + Invoice_Id + "&Scheme_Id=" + Scheme_Id);
                }
                else
                {
                    Response.Redirect("MasterGenerateInvoice_Detail.aspx?Package_Id=" + Package_Id + "&Invoice_Id=" + Invoice_Id + "&Scheme_Id=" + Scheme_Id);
                }
            }
            else if (Session["Invoice_C"].ToString() == "1")
            {
                if (PackageInvoice_Type == "N")
                {
                    Response.Redirect("MasterGenerateInvoice_Detail_New.aspx?Package_Id=" + Package_Id + "&Invoice_Id=0&Scheme_Id=" + Scheme_Id);
                }
                else
                {
                    Response.Redirect("MasterGenerateInvoice_Detail.aspx?Package_Id=" + Package_Id + "&Invoice_Id=0&Scheme_Id=" + Scheme_Id);
                }
            }
            else
            {
                MessageBox.Show("Invoice Not Generated. Please Generate First.");
                return;
            }
        }
        else
        {
            if (PackageInvoice_Type == "N")
            {
                Response.Redirect("MasterGenerateInvoice_Detail_New.aspx?Package_Id=0&Invoice_Id=" + Invoice_Id + "&Scheme_Id=" + Scheme_Id);
            }
            else
            {
                Response.Redirect("MasterGenerateInvoice_Detail.aspx?Package_Id=0&Invoice_Id=" + Invoice_Id + "&Scheme_Id=" + Scheme_Id);
            }
        }
    }

    protected void grdInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[13].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[14].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[15].Text = Session["Default_Division"].ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }
    }
    protected void grdADP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[10].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[11].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[12].Text = Session["Default_Division"].ToString();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            return;
        }
        load_dashboard();
    }

    protected void btnMark_Click(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        List<tbl_PackageInvoiceApproval> obj_tbl_PackageInvoiceApproval_Li = new List<tbl_PackageInvoiceApproval>();
        for (int i = 0; i < grdInvoice.Rows.Count; i++)
        {
            int PackageInvoice_Id = 0;
            try
            {
                PackageInvoice_Id = Convert.ToInt32(grdInvoice.Rows[i].Cells[0].Text.Trim());
            }
            catch
            {
                PackageInvoice_Id = 0;
            }
            CheckBox chkMark = grdInvoice.Rows[i].FindControl("chkMark") as CheckBox;
            if (chkMark.Visible && chkMark.Checked)
            {
                if (PackageInvoice_Id > 0)
                {
                    tbl_PackageInvoiceApproval obj_tbl_PackageInvoiceApproval = new tbl_PackageInvoiceApproval();
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Comments = "Revert Back To Division Due To Implementation of SNA";
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Status_Id = 2;
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_PackageInvoice_Id = PackageInvoice_Id;
                    obj_tbl_PackageInvoiceApproval.Scheme_Id = Convert.ToInt32(Convert.ToInt32(grdInvoice.Rows[i].Cells[6].Text.Trim()));
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Status = 1;
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Next_Designation_Id = 9;
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Next_Organisation_Id = 1;
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Step_Count = 4;
                    obj_tbl_PackageInvoiceApproval_Li.Add(obj_tbl_PackageInvoiceApproval);
                }
            }
        }
        if (obj_tbl_PackageInvoiceApproval_Li.Count == 0)
        {
            MessageBox.Show("Please Select Atleast One Invoice To Rollback");
            return;
        }
        else
        {
            if (new DataLayer().Update_tbl_PackageInvoice_Rollback(obj_tbl_PackageInvoiceApproval_Li))
            {
                MessageBox.Show("Invoice Transffered Successfully");
                load_dashboard();
                return;
            }
            else
            {
                MessageBox.Show("Unable To Rollback Invoice");
                return;
            }
        }
    }
    protected void btnMarkADP_Click(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        List<tbl_PackageADPApproval> obj_tbl_PackageADPApproval_Li = new List<tbl_PackageADPApproval>();
        for (int i = 0; i < grdADP.Rows.Count; i++)
        {
            int PackageADP_Id = 0;
            try
            {
                PackageADP_Id = Convert.ToInt32(grdADP.Rows[i].Cells[3].Text.Trim());
            }
            catch
            {
                PackageADP_Id = 0;
            }

            CheckBox chkMark = grdADP.Rows[i].FindControl("chkMark") as CheckBox;
            if (chkMark.Visible && chkMark.Checked)
            {
                if (PackageADP_Id > 0)
                {
                    tbl_PackageADPApproval obj_tbl_PackageADPApproval = new tbl_PackageADPApproval();
                    obj_tbl_PackageADPApproval.PackageADPApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                    obj_tbl_PackageADPApproval.PackageADPApproval_Comments = "Revert Back To Division Due To Implementation of SNA";
                    obj_tbl_PackageADPApproval.PackageADPApproval_Status_Id = 2;
                    obj_tbl_PackageADPApproval.PackageADPApproval_Package_ADP_Id = PackageADP_Id;
                    obj_tbl_PackageADPApproval.PackageADPApproval_Package_Id = Convert.ToInt32(grdADP.Rows[i].Cells[0].Text.Trim());
                    obj_tbl_PackageADPApproval.PackageADPApproval_Status = 1;
                    obj_tbl_PackageADPApproval.PackageADPApproval_Next_Designation_Id = 9;
                    obj_tbl_PackageADPApproval.PackageADPApproval_Next_Organisation_Id = 1;
                    obj_tbl_PackageADPApproval.PackageADPApproval_Step_Count = 4;
                    obj_tbl_PackageADPApproval.Scheme_Id = Convert.ToInt32(grdADP.Rows[i].Cells[2].Text.Trim());
                    obj_tbl_PackageADPApproval.Loop = Convert.ToInt32(grdADP.Rows[i].Cells[4].Text.Trim());
                    obj_tbl_PackageADPApproval_Li.Add(obj_tbl_PackageADPApproval);
                }
            }
        }
        if (obj_tbl_PackageADPApproval_Li.Count == 0 || obj_tbl_PackageADPApproval_Li == null)
        {
            MessageBox.Show("Please Select Atleast One Other Departmental To Rollback");
            return;
        }
        else
        {
            if (new DataLayer().Update_tbl_PackageADP_Rollback(obj_tbl_PackageADPApproval_Li))
            {
                MessageBox.Show("Other Departmental Transffered Successfully");
                load_dashboard();
                return;
            }
            else
            {
                MessageBox.Show("Unable To Rollback Other Departmental");
                return;
            }
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

    protected void btnEditADP_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Invoice_Id = 0;
        string Scheme_Id = gr.Cells[2].Text.Trim();
        try
        {
            Invoice_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        }
        catch
        {
            Invoice_Id = 0;
        }
        Response.Redirect("PackageAdditionalDepartmentPaymentApproval.aspx?Invoice_Id=" + Invoice_Id.ToString() + "&Scheme_Id=" + Scheme_Id);
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

    protected void grdMA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[10].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[11].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[12].Text = Session["Default_Division"].ToString();
        }
    }

    protected void btnEditMA_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int MobilizationAdvance_Id = 0;
        string Scheme_Id = gr.Cells[2].Text.Trim();
        try
        {
            MobilizationAdvance_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        }
        catch
        {
            MobilizationAdvance_Id = 0;
        }
        Response.Redirect("ApprovalPackageMobilizationRelease.aspx?MobilizationAdvance_Id=" + MobilizationAdvance_Id.ToString() + "&Scheme_Id=" + Scheme_Id);
    }

    protected void btnMarkMA_Click(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        List<tbl_Package_MobilizationAdvanceApproval> obj_tbl_Package_MobilizationAdvanceApproval_Li = new List<tbl_Package_MobilizationAdvanceApproval>();
        for (int i = 0; i < grdMA.Rows.Count; i++)
        {
            int MobilizationAdvance_Id = 0;
            try
            {
                MobilizationAdvance_Id = Convert.ToInt32(grdMA.Rows[i].Cells[3].Text.Trim());
            }
            catch
            {
                MobilizationAdvance_Id = 0;
            }

            CheckBox chkMark = grdMA.Rows[i].FindControl("chkMark") as CheckBox;
            if (chkMark.Visible && chkMark.Checked)
            {
                if (MobilizationAdvance_Id > 0)
                {
                    tbl_Package_MobilizationAdvanceApproval obj_tbl_Package_MobilizationAdvanceApproval = new tbl_Package_MobilizationAdvanceApproval();
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Comments = "Revert Back To Division Due To Implementation of SNA";
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Status_Id = 2;
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Package_MobilizationAdvance_Id = MobilizationAdvance_Id;
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Package_Id = Convert.ToInt32(grdMA.Rows[i].Cells[0].Text.Trim());
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Status = 1;
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Step_Count = 4;
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Next_Designation_Id = 9;
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Next_Organisation_Id = 1;
                    obj_tbl_Package_MobilizationAdvanceApproval.Scheme_Id = Convert.ToInt32(grdMA.Rows[i].Cells[2].Text.Trim());
                    obj_tbl_Package_MobilizationAdvanceApproval.Loop = Convert.ToInt32(grdMA.Rows[i].Cells[4].Text.Trim());
                    obj_tbl_Package_MobilizationAdvanceApproval_Li.Add(obj_tbl_Package_MobilizationAdvanceApproval);
                }
            }
        }
        if (obj_tbl_Package_MobilizationAdvanceApproval_Li.Count == 0 || obj_tbl_Package_MobilizationAdvanceApproval_Li == null)
        {
            MessageBox.Show("Please Select Atleast One Mobilization Advance To Rollback");
            return;
        }
        else
        {
            if (new DataLayer().Update_tbl_PackageMobilizationAdvance_Rollback(obj_tbl_Package_MobilizationAdvanceApproval_Li))
            {
                MessageBox.Show("Mobilization Advance Transffered Successfully");
                load_dashboard();
                return;
            }
            else
            {
                MessageBox.Show("Unable To Rollback Mobilization Advance");
                return;
            }
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

    protected void grdDeductionRelease_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[10].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[11].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[12].Text = Session["Default_Division"].ToString();
        }
    }

    protected void btnEditDR_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Package_DeductionRelease_Id = 0;
        string Scheme_Id = gr.Cells[2].Text.Trim();
        try
        {
            Package_DeductionRelease_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        }
        catch
        {
            Package_DeductionRelease_Id = 0;
        }
        Response.Redirect("PackageDeductionReleaseApproval.aspx?Package_DeductionRelease_Id=" + Package_DeductionRelease_Id.ToString() + "&Scheme_Id=" + Scheme_Id);
    }

    protected void btnMarkDR_Click(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select Scheme");
            return;
        }
        List<tbl_Package_DeductionReleaseApproval> obj_tbl_Package_DeductionReleaseApproval_Li = new List<tbl_Package_DeductionReleaseApproval>();
        int _Loop = 0;
        for (int i = 0; i < grdDeductionRelease.Rows.Count; i++)
        {
            int Package_DeductionRelease_Id = 0;
            try
            {
                Package_DeductionRelease_Id = Convert.ToInt32(grdDeductionRelease.Rows[i].Cells[3].Text.Trim());
            }
            catch
            {
                Package_DeductionRelease_Id = 0;
            }
            try
            {
                _Loop = Convert.ToInt32(grdDeductionRelease.Rows[i].Cells[4].Text.Trim());
            }
            catch
            {
                _Loop = 0;
            }
            CheckBox chkMark = grdDeductionRelease.Rows[i].FindControl("chkMark") as CheckBox;
            if (chkMark.Visible && chkMark.Checked)
            {
                if (Package_DeductionRelease_Id > 0)
                {
                    tbl_Package_DeductionReleaseApproval obj_tbl_Package_DeductionReleaseApproval = new tbl_Package_DeductionReleaseApproval();
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Comments = "Revert Back To Division Due To Implementation of SNA";
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Status_Id = 2;
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Package_DeductionRelease_Id = Package_DeductionRelease_Id;
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Package_Id = Convert.ToInt32(grdDeductionRelease.Rows[i].Cells[0].Text.Trim());
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Status = 1;
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Step_Count = 4;
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Next_Designation_Id = 9;
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Next_Organisation_Id = 1;
                    obj_tbl_Package_DeductionReleaseApproval.Scheme_Id = Convert.ToInt32(grdDeductionRelease.Rows[i].Cells[2].Text.Trim());
                    obj_tbl_Package_DeductionReleaseApproval.Loop = Convert.ToInt32(grdDeductionRelease.Rows[i].Cells[4].Text.Trim());
                    obj_tbl_Package_DeductionReleaseApproval_Li.Add(obj_tbl_Package_DeductionReleaseApproval);
                }
            }
        }
        if (obj_tbl_Package_DeductionReleaseApproval_Li.Count == 0 || obj_tbl_Package_DeductionReleaseApproval_Li == null)
        {
            MessageBox.Show("Please Select Atleast One Deduction Release To Rollback");
            return;
        }
        else
        {
            if (new DataLayer().Update_tbl_PackageDeductionRelease_Rollback(obj_tbl_Package_DeductionReleaseApproval_Li))
            {
                MessageBox.Show("Deduction Release Transffered Successfully");
                load_dashboard();
                return;
            }
            else
            {
                MessageBox.Show("Unable To Rollback Deduction Release");
                return;
            }
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedValue == "0")
        {
            ddlDesignation.Items.Clear();
        }
        else
        {
            get_tbl_Designation(Convert.ToInt32(ddlScheme.SelectedValue));
        }
    }
}