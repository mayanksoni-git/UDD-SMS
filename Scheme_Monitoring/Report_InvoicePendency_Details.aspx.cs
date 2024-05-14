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

public partial class Report_InvoicePendency_Details : System.Web.UI.Page
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
            set_Labels();
            txtDateFrom.Text = "01" + Session["ServerDate"].ToString().Substring(2);
            txtDateTill.Text = Session["ServerDate"].ToString();
            rbtSearchBy_SelectedIndexChanged(rbtSearchBy, e);
            get_tbl_Project();
            get_tbl_Zone();

            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();

            if (Session["UserType"].ToString() == "4" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {//Zone
                try
                {
                    ddlSearchZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlSearchZone_SelectedIndexChanged(ddlSearchZone, e);
                    ddlSearchZone.Enabled = false;
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {
                try
                {
                    ddlSearchZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlSearchZone_SelectedIndexChanged(ddlSearchZone, e);
                    ddlSearchZone.Enabled = false;
                    if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlSearchCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlSearchCircle_SelectedIndexChanged(ddlSearchCircle, e);
                            ddlSearchCircle.Enabled = false;
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {
                try
                {
                    ddlSearchZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlSearchZone_SelectedIndexChanged(ddlSearchZone, e);
                    ddlSearchZone.Enabled = false;
                    if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlSearchCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlSearchCircle_SelectedIndexChanged(ddlSearchCircle, e);
                            ddlSearchCircle.Enabled = false;
                            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                            {//Circle
                                try
                                {
                                    ddlsearchDivision.SelectedValue = Session["PersonJuridiction_DivisionId"].ToString();
                                    ddlsearchDivision.Enabled = false;
                                }
                                catch
                                { }
                            }
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
            //get_tbl_PackageInvoice();
            //get_tbl_PackageInvoiceADP();
            //get_tbl_PackageInvoiceMA();
            //get_tbl_Package_DeductionRelease();

            //set_Labels();
        }
    }
    protected void rbtSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtSearchBy.SelectedValue == "1")
        {
            divFromDate.Visible = false;
            divTillDate.Visible = false;
        }
        else
        {
            divFromDate.Visible = true;
            divTillDate.Visible = true;
        }
    }
    private void get_tbl_Project()
    {
        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_Project(0);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Project(Convert.ToInt32(Session["Person_Id"].ToString()));
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            bool is_Selected = false;
            ddlScheme.DataTextField = "Project_Name";
            ddlScheme.DataValueField = "Project_Id";
            ddlScheme.DataSource = ds.Tables[0];
            ddlScheme.DataBind();
            try
            {
                foreach (ListItem listItem in ddlScheme.Items)
                {
                    if (listItem.Value == Session["Default_Scheme"].ToString())
                    {
                        is_Selected = true;
                        listItem.Selected = true;
                    }
                }
            }
            catch
            {
                ddlScheme.Items[0].Selected = true;
            }
            if (is_Selected == false)
            {
                ddlScheme.Items[0].Selected = true;
            }
        }
        else
        {
            ddlScheme.Items.Clear();
        }
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (chkDisplayRecords.Items[0].Selected == true)
        {
            get_tbl_PackageInvoice();
        }
        else
        {
            grdInvoice.DataSource = null;
            grdInvoice.DataBind();
        }
        if (chkDisplayRecords.Items[1].Selected == true)
        {
            get_tbl_PackageInvoiceADP();
        }
        else
        {
            grdADP.DataSource = null;
            grdADP.DataBind();
        }
        if (chkDisplayRecords.Items[2].Selected == true)
        {
            get_tbl_PackageInvoiceMA();
        }
        else
        {
            grdMA.DataSource = null;
            grdMA.DataBind();
        }
        if (chkDisplayRecords.Items[3].Selected == true)
        {
            get_tbl_Package_DeductionRelease();
        }
        else
        {
            grdDeductionRelease.DataSource = null;
            grdDeductionRelease.DataBind();
        }
        set_Labels();
    }

    private void get_tbl_Zone()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlSearchZone, "Zone_Name", "Zone_Id");
        }
        else
        {
            ddlSearchZone.Items.Clear();
        }
    }

    private void get_tbl_Circle_Search(int Zone_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Circle(Zone_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlSearchCircle, "Circle_Name", "Circle_Id");
        }
        else
        {
            ddlSearchCircle.Items.Clear();
        }
    }
    private void get_tbl_Division_Search(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division(Circle_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlsearchDivision, "Division_Name", "Division_Id");
        }
        else
        {
            ddlsearchDivision.Items.Clear();
        }
    }

    protected void ddlSearchZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchZone.SelectedValue == "0")
        {
            ddlSearchCircle.Items.Clear();
            ddlsearchDivision.Items.Clear();
        }
        else
        {
            get_tbl_Circle_Search(Convert.ToInt32(ddlSearchZone.SelectedValue));
        }
    }

    protected void ddlSearchCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchCircle.SelectedValue == "0")
        {
            ddlsearchDivision.Items.Clear();
        }
        else
        {
            get_tbl_Division_Search(Convert.ToInt32(ddlSearchCircle.SelectedValue));
        }
    }

    protected void btnExport1_Click(object sender, EventArgs e)
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";
        int PendencyDays = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        if (Scheme_Id == "")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlScheme.Focus();
            return;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlSearchZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlSearchCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlsearchDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }

        try
        {
            PendencyDays = Convert.ToInt32(txtPendencyDays.Text.Trim());
        }
        catch
        {
            PendencyDays = 0;
        }
        string FromDate = "";
        string TillDate = "";
        if (rbtSearchBy.SelectedValue == "1")
        {
            FromDate = "";
            TillDate = "";
        }
        else
        {
            FromDate = txtDateFrom.Text.Trim();
            TillDate = txtDateTill.Text.Trim();
        }
        DataSet ds = new DataSet();
        ds = new DataLayer().get_Invoice_Pending_Summery_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, FromDate, TillDate, District_Id, ULB_Id, chkRemoveSelected.Checked, PendencyDays);
        if (AllClasses.CheckDataSet(ds))
        {
            string filePath = "\\Downloads\\";
            if (!Directory.Exists(Server.MapPath(".") + filePath))
            {
                Directory.CreateDirectory(Server.MapPath(".") + filePath);
            }

            string fileName = "InvoicePending_Summery.pdf";

            List<tbl_Invoice_Pending> obj_tbl_Invoice_Pending_Li = new List<tbl_Invoice_Pending>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tbl_Invoice_Pending obj_tbl_Invoice_Pending = new tbl_Invoice_Pending();
                obj_tbl_Invoice_Pending.Agra_Zone_Invoice_Count = Convert.ToInt32(ds.Tables[0].Rows[i]["Agra_Zone_Invoice_Count"].ToString());
                obj_tbl_Invoice_Pending.Agra_Zone_Invoice_Total = Convert.ToDecimal(ds.Tables[0].Rows[i]["Agra_Zone_Invoice_Total"].ToString());
                obj_tbl_Invoice_Pending.GZB_Zone_Invoice_Count = Convert.ToInt32(ds.Tables[0].Rows[i]["GZB_Zone_Invoice_Count"].ToString());
                obj_tbl_Invoice_Pending.GZB_Zone_Invoice_Total = Convert.ToDecimal(ds.Tables[0].Rows[i]["GZB_Zone_Invoice_Total"].ToString());
                obj_tbl_Invoice_Pending.Lucknow_Zone_Invoice_Count = Convert.ToInt32(ds.Tables[0].Rows[i]["Lucknow_Zone_Invoice_Count"].ToString());
                obj_tbl_Invoice_Pending.Lucknow_Zone_Invoice_Total = Convert.ToDecimal(ds.Tables[0].Rows[i]["Lucknow_Zone_Invoice_Total"].ToString());
                obj_tbl_Invoice_Pending.Prayagraj_Zone_Invoice_Count = Convert.ToInt32(ds.Tables[0].Rows[i]["Prayagraj_Zone_Invoice_Count"].ToString());
                obj_tbl_Invoice_Pending.Prayagraj_Zone_Invoice_Total = Convert.ToDecimal(ds.Tables[0].Rows[i]["Prayagraj_Zone_Invoice_Total"].ToString());
                obj_tbl_Invoice_Pending.Designation_DesignationName = ds.Tables[0].Rows[i]["Designation_DesignationName"].ToString();
                obj_tbl_Invoice_Pending.ProcessConfigMaster_Designation_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["ProcessConfigMaster_Designation_Id"].ToString());
                obj_tbl_Invoice_Pending.ProcessConfigMaster_OrgId = Convert.ToInt32(ds.Tables[0].Rows[i]["ProcessConfigMaster_OrgId"].ToString());
                obj_tbl_Invoice_Pending.Total_Invoice_Count = Convert.ToInt32(ds.Tables[0].Rows[i]["All_Zone_Invoice_Count"].ToString());
                obj_tbl_Invoice_Pending.Invoice_Total = Convert.ToDecimal(ds.Tables[0].Rows[i]["All_Zone_Invoice_Total"].ToString());
                obj_tbl_Invoice_Pending.Header_Text = "Pending Invoices of " + ddlScheme.SelectedItem.Text + " for more than " + txtPendencyDays.Text + " days.";
                obj_tbl_Invoice_Pending_Li.Add(obj_tbl_Invoice_Pending);
            }

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
            crystalReport.Load(Server.MapPath("~/Crystal/pmis/InvoicePending_Summery.rpt"));
            crystalReport.SetDataSource(obj_tbl_Invoice_Pending_Li);
            crystalReport.Refresh();
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
                MessageBox.Show("Unable To Generate Report.");
                return;
            }
        }
        else
        {
            MessageBox.Show("Unable To Generate Report.");
            return;
        }
    }

    protected void btnExport2_Click(object sender, EventArgs e)
    {
        DataTable ds = new DataTable();
        if (ViewState["InvoicePending"] != null)
        {
            ds = (DataTable)ViewState["InvoicePending"];
            if (AllClasses.CheckDt(ds))
            {
                string filePath = "\\Downloads\\";
                if (!Directory.Exists(Server.MapPath(".") + filePath))
                {
                    Directory.CreateDirectory(Server.MapPath(".") + filePath);
                }

                string fileName = "InvoicePending_Detail.pdf";

                List<tbl_Invoice_Pending_Detail> obj_tbl_Invoice_Pending_Detail_Li = new List<tbl_Invoice_Pending_Detail>();
                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    tbl_Invoice_Pending_Detail obj_tbl_Invoice_Pending_Detail = new tbl_Invoice_Pending_Detail();
                    obj_tbl_Invoice_Pending_Detail.Circle_Id = Convert.ToInt32(ds.Rows[i]["Circle_Id"].ToString());
                    obj_tbl_Invoice_Pending_Detail.Zone_Id = Convert.ToInt32(ds.Rows[i]["Zone_Id"].ToString());
                    obj_tbl_Invoice_Pending_Detail.Division_Id = Convert.ToInt32(ds.Rows[i]["Division_Id"].ToString());
                    obj_tbl_Invoice_Pending_Detail.Circle_Name = ds.Rows[i]["Circle_Name"].ToString();
                    obj_tbl_Invoice_Pending_Detail.Zone_Name = ds.Rows[i]["Zone_Name"].ToString();
                    obj_tbl_Invoice_Pending_Detail.Division_Name = ds.Rows[i]["Division_Name"].ToString();
                    obj_tbl_Invoice_Pending_Detail.ProjectWork_Name = ds.Rows[i]["ProjectWork_Name"].ToString();
                    obj_tbl_Invoice_Pending_Detail.ProjectWork_Code = ds.Rows[i]["ProjectWork_ProjectCode"].ToString();
                    obj_tbl_Invoice_Pending_Detail.Circle_Division_Name = ds.Rows[i]["Circle_Name"].ToString() + " - " + ds.Rows[i]["Division_Name"].ToString();
                    obj_tbl_Invoice_Pending_Detail.Header_Text = "Pending Invoices of " + ddlScheme.SelectedItem.Text + " for more than " + txtPendencyDays.Text + " days.";
                    obj_tbl_Invoice_Pending_Detail.Days_Since_Last_Action = ds.Rows[i]["PackageInvoice_ProcessedOn"].ToString();
                    try
                    {
                        obj_tbl_Invoice_Pending_Detail.Pendency_Days = Convert.ToInt32(ds.Rows[i]["Date_Diff_Action"].ToString());
                    }
                    catch
                    {
                        obj_tbl_Invoice_Pending_Detail.Pendency_Days = 0;
                    }
                    try
                    {
                        obj_tbl_Invoice_Pending_Detail.Total_Invoice_Amount = Convert.ToDecimal(ds.Rows[i]["Total_Amount_F"].ToString());
                    }
                    catch
                    {
                        obj_tbl_Invoice_Pending_Detail.Total_Invoice_Amount = 0;
                    }
                    obj_tbl_Invoice_Pending_Detail.Pending_At_Designation = ds.Rows[i]["Designation_DesignationName"].ToString();
                    obj_tbl_Invoice_Pending_Detail.Pending_Reason_If_Any = ds.Rows[i]["InvoiceAdditionalStatus"].ToString();
                    obj_tbl_Invoice_Pending_Detail_Li.Add(obj_tbl_Invoice_Pending_Detail);
                }

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
                crystalReport.Load(Server.MapPath("~/Crystal/pmis/Invoice_Pending_Detail.rpt"));
                crystalReport.SetDataSource(obj_tbl_Invoice_Pending_Detail_Li);
                crystalReport.Refresh();
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
                    MessageBox.Show("Unable To Generate Report.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Unable To Generate Report.");
                return;
            }
        }
    }

    private void set_Labels()
    {
        sp_Invoice.InnerHtml = grdInvoice.Rows.Count.ToString();
        sp_OtherDept.InnerHtml = grdADP.Rows.Count.ToString();
        sp_OtherPayment.InnerHtml = grdMA.Rows.Count.ToString();
        sp_DeductionRelease.InnerHtml = grdDeductionRelease.Rows.Count.ToString();
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
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";
        int PendencyDays = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        if (Scheme_Id == "")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlScheme.Focus();
            return;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlSearchZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlSearchCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlsearchDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }

        try
        {
            PendencyDays = Convert.ToInt32(txtPendencyDays.Text.Trim());
        }
        catch
        {
            PendencyDays = 0;
        }
        string FromDate = "";
        string TillDate = "";
        if (rbtSearchBy.SelectedValue == "1")
        {
            FromDate = "";
            TillDate = "";
        }
        else
        {
            FromDate = txtDateFrom.Text.Trim();
            TillDate = txtDateTill.Text.Trim();
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Package_ADP_Report(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, FromDate, TillDate, ULB_Id, chkRemoveSelected.Checked, PendencyDays, txtProjectCode.Text.Trim());

        if (AllClasses.CheckDataSet(ds))
        {
            grdADP.DataSource = ds.Tables[0];
            grdADP.DataBind();
            grdADP.FooterRow.Cells[19].Text = ds.Tables[0].Compute("sum(Package_ADP_ADPTotalAmount)", "").ToString();
        }
        else
        {
            grdADP.DataSource = null;
            grdADP.DataBind();
        }
    }

    private void get_tbl_PackageInvoiceMA()
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";
        int PendencyDays = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        if (Scheme_Id == "")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlScheme.Focus();
            return;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlSearchZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlSearchCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlsearchDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }

        try
        {
            PendencyDays = Convert.ToInt32(txtPendencyDays.Text.Trim());
        }
        catch
        {
            PendencyDays = 0;
        }
        string FromDate = "";
        string TillDate = "";
        if (rbtSearchBy.SelectedValue == "1")
        {
            FromDate = "";
            TillDate = "";
        }
        else
        {
            FromDate = txtDateFrom.Text.Trim();
            TillDate = txtDateTill.Text.Trim();
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Package_MobilizationAdvance_Report(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, FromDate, TillDate, ULB_Id, chkRemoveSelected.Checked, PendencyDays, txtProjectCode.Text.Trim());

        if (AllClasses.CheckDataSet(ds))
        {
            grdMA.DataSource = ds.Tables[0];
            grdMA.DataBind();
            grdMA.FooterRow.Cells[20].Text = ds.Tables[0].Compute("sum(Package_MobilizationAdvance_TotalAmount)", "").ToString();
        }
        else
        {
            grdMA.DataSource = null;
            grdMA.DataBind();
        }
    }

    private void get_tbl_Package_DeductionRelease()
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";
        int PendencyDays = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        if (Scheme_Id == "")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlScheme.Focus();
            return;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlSearchZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlSearchCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlsearchDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }

        try
        {
            PendencyDays = Convert.ToInt32(txtPendencyDays.Text.Trim());
        }
        catch
        {
            PendencyDays = 0;
        }
        string FromDate = "";
        string TillDate = "";
        if (rbtSearchBy.SelectedValue == "1")
        {
            FromDate = "";
            TillDate = "";
        }
        else
        {
            FromDate = txtDateFrom.Text.Trim();
            TillDate = txtDateTill.Text.Trim();
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Package_DeductionRelease_Report(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, FromDate, TillDate, ULB_Id, chkRemoveSelected.Checked, PendencyDays, txtProjectCode.Text.Trim());

        if (AllClasses.CheckDataSet(ds))
        {
            grdDeductionRelease.DataSource = ds.Tables[0];
            grdDeductionRelease.DataBind();
            grdDeductionRelease.FooterRow.Cells[24].Text = ds.Tables[0].Compute("sum(Package_DeductionRelease_TotalReleaseAmount)", "").ToString();
        }
        else
        {
            grdDeductionRelease.DataSource = null;
            grdDeductionRelease.DataBind();
        }
    }

    private void get_tbl_PackageInvoice()
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "";
        int PendencyDays = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        if (Scheme_Id == "")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlScheme.Focus();
            return;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlSearchZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlSearchCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlsearchDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }

        try
        {
            PendencyDays = Convert.ToInt32(txtPendencyDays.Text.Trim());
        }
        catch
        {
            PendencyDays = 0;
        }
        string FromDate = "";
        string TillDate = "";
        if (rbtSearchBy.SelectedValue == "1")
        {
            FromDate = "";
            TillDate = "";
        }
        else
        {
            FromDate = txtDateFrom.Text.Trim();
            TillDate = txtDateTill.Text.Trim();
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageInvoice_Report(0, Zone_Id, Circle_Id, Division_Id, Scheme_Id, FromDate, TillDate, District_Id, ULB_Id, chkRemoveSelected.Checked, PendencyDays, txtProjectCode.Text.Trim());

        if (AllClasses.CheckDataSet(ds))
        {
            ViewState["InvoicePending"] = ds.Tables[0];
            grdInvoice.DataSource = ds.Tables[0];
            grdInvoice.DataBind();
            grdInvoice.FooterRow.Cells[20].Text = ds.Tables[0].Compute("sum(Total_Amount)", "").ToString();
            grdInvoice.FooterRow.Cells[21].Text = ds.Tables[0].Compute("sum(Total_Amount_D)", "").ToString();
            grdInvoice.FooterRow.Cells[22].Text = ds.Tables[0].Compute("sum(Total_Amount_F)", "").ToString();
        }
        else
        {
            ViewState["InvoicePending"] = null;
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
            e.Row.Cells[12].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[13].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[14].Text = Session["Default_Division"].ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int Is_Chage = 0;
            try
            {
                Is_Chage = Convert.ToInt32(e.Row.Cells[29].Text);
            }
            catch
            {
                Is_Chage = 0;
            }
            if (Is_Chage > 0)
            {
                e.Row.Cells[7].BackColor = Color.Pink;
            }
        }
    }
    protected void grdADP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[9].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[10].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[11].Text = Session["Default_Division"].ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int Is_Chage = 0;
            try
            {
                Is_Chage = Convert.ToInt32(e.Row.Cells[25].Text);
            }
            catch
            {
                Is_Chage = 0;
            }
            if (Is_Chage > 0)
            {
                e.Row.Cells[5].BackColor = Color.Pink;
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
            e.Row.Cells[9].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[10].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[11].Text = Session["Default_Division"].ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int Is_Chage = 0;
            try
            {
                Is_Chage = Convert.ToInt32(e.Row.Cells[25].Text);
            }
            catch
            {
                Is_Chage = 0;
            }
            if (Is_Chage > 0)
            {
                e.Row.Cells[5].BackColor = Color.Pink;
            }
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
            e.Row.Cells[9].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[10].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[11].Text = Session["Default_Division"].ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int Is_Chage = 0;
            try
            {
                Is_Chage = Convert.ToInt32(e.Row.Cells[25].Text);
            }
            catch
            {
                Is_Chage = 0;
            }
            if (Is_Chage > 0)
            {
                e.Row.Cells[5].BackColor = Color.Pink;
            }
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

    protected void btnOpenTimelineADP1_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Package_ADP_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageADPApproval_History(Package_ADP_Id);

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

    protected void btnOpenTimelineMA1_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int MobilizationAdvance_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageMAApproval_History(MobilizationAdvance_Id);

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

    protected void btnOpenTimelineDR1_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int DeductionRelease_Id = Convert.ToInt32(gr.Cells[3].Text.Trim());
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageDRApproval_History(DeductionRelease_Id);

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
}