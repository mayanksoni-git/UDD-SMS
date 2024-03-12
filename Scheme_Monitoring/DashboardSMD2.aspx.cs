using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DashboardSMD2 : System.Web.UI.Page
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
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();

            txtDateFrom.Text = "01" + Session["ServerDate"].ToString().Substring(2);
            txtDateTill.Text = Session["ServerDate"].ToString();
            get_tbl_Project();
            get_tbl_Zone();
            get_M_Jurisdiction();
            if (Session["SearchStorage"] != null)
            {
                SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
                txtDateFrom.Text = obj_SearchStorage.FromDate;
                txtDateTill.Text = obj_SearchStorage.TillDate;
                string[] List_Scheme = obj_SearchStorage.Scheme_Id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (List_Scheme.Length > 0)
                {
                    for (int i = 0; i < List_Scheme.Length; i++)
                    {
                        foreach (ListItem listItem in ddlScheme.Items)
                        {
                            if (List_Scheme[i].Trim() == listItem.Value)
                            {
                                listItem.Selected = true;
                                break;
                            }
                        }
                    }
                }
                rbtSearchBy.SelectedValue = obj_SearchStorage.Search_By;
                if (obj_SearchStorage.Zone_Id > 0)
                {
                    ddlZone.SelectedValue = obj_SearchStorage.Zone_Id.ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());
                }
                if (obj_SearchStorage.Circle_Id > 0)
                {
                    ddlCircle.SelectedValue = obj_SearchStorage.Circle_Id.ToString();
                    ddlCircle_SelectedIndexChanged(ddlCircle, new EventArgs());
                }
                if (obj_SearchStorage.Division_Id > 0)
                {
                    ddlDivision.SelectedValue = obj_SearchStorage.Division_Id.ToString();
                }
            }

            //if (Session["UserType"].ToString() != "1")
            //{
            //    try
            //    {
            //        if (Session["PersonJuridiction_Project_Id"].ToString() != "" && Session["PersonJuridiction_Project_Id"].ToString() != "0")
            //        {
            //            ddlScheme.SelectedValue = Session["PersonJuridiction_Project_Id"].ToString();
            //            ddlScheme.Enabled = false;
            //        }
            //    }
            //    catch
            //    {

            //    }
            //}
            if (Session["UserType"].ToString() == "4" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {//Zone
                try
                {
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZone.Enabled = false;
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {
                try
                {
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZone.Enabled = false;
                    if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircle_SelectedIndexChanged(ddlCircle, e);
                            ddlCircle.Enabled = false;
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
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZone.Enabled = false;
                    if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircle_SelectedIndexChanged(ddlCircle, e);
                            ddlCircle.Enabled = false;
                            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                            {//Circle
                                try
                                {
                                    ddlDivision.SelectedValue = Session["PersonJuridiction_DivisionId"].ToString();
                                    ddlDivision.Enabled = false;
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
            load_dashboard();

            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["T"] != null && Request.QueryString["T"].ToString() == "S")
                {
                    MessageBox.Show("Package EMB Marked Approved");
                }
                if (Request.QueryString["T"] != null && Request.QueryString["T"].ToString() == "B")
                {
                    MessageBox.Show("Package EMB Marked For Billing");
                }
                if (Request.QueryString["T"] != null && Request.QueryString["T"].ToString() == "A")
                {
                    MessageBox.Show("Invoice Details Approved Successfully");
                }
            }
        }
    }
    private void Set_Mark_Status(int PackageInvoice_Id, int Scheme_Id, CheckBox chkMark)
    {
        DataSet ds = new DataSet();
        int _Loop = 0;
        if (Session["UserType"].ToString() == "1")
        {
            _Loop = (new DataLayer()).get_Loop("Invoice", 0, 0, 0, null, null);
        }
        else
        {
            _Loop = (new DataLayer()).get_Loop("Invoice", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), Scheme_Id, null, null);
        }
        ds = (new DataLayer()).get_ProcessConfig_Current(Scheme_Id, "Invoice", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), _Loop, PackageInvoice_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            int ConfigMaster_Id = 0;
            try
            {
                ConfigMaster_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ProcessConfigMaster_Id"].ToString());
                if (get_tbl_InvoiceStatus(ConfigMaster_Id, "Invoice", Scheme_Id))
                {
                    chkMark.Visible = true;
                }
                else
                {
                    chkMark.Visible = false;
                }
            }
            catch
            { }
        }
    }
    private bool get_tbl_InvoiceStatus_DeductionRelease(int ConfigMasterId, int Scheme_Id)
    {
        bool rVal = false;

        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_DeductionRelease(0, 0, 0, 0);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Package_DeductionRelease(Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), ConfigMasterId);
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["InvoiceStatus_Id"].ToString() == "4" || ds.Tables[0].Rows[i]["InvoiceStatus_Id"].ToString() == "5")
                {
                    rVal = true;
                    break;
                }
            }
        }
        else
        {
            rVal = false;
        }
        return rVal;
    }
    private bool get_tbl_InvoiceStatus(int ConfigMasterId, string Process_Name, int Scheme_Id)
    {
        bool rVal = false;

        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_InvoiceStatus(0, 0, 0, 0, Process_Name);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_InvoiceStatus(Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), ConfigMasterId, Process_Name);
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["InvoiceStatus_Id"].ToString() == "4" || ds.Tables[0].Rows[i]["InvoiceStatus_Id"].ToString() == "5")
                {
                    rVal = true;
                    break;
                }
            }
        }
        else
        {
            rVal = false;
        }
        return rVal;
    }
    private bool get_tbl_InvoiceStatus_ADP(int ConfigMasterId, int Scheme_Id)
    {
        bool rVal = false;

        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_ADP(0, 0, 0, 0);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Package_ADP(Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), ConfigMasterId);
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["InvoiceStatus_Id"].ToString() == "4" || ds.Tables[0].Rows[i]["InvoiceStatus_Id"].ToString() == "5")
                {
                    rVal = true;
                    break;
                }
            }
        }
        else
        {
            rVal = false;
        }
        return rVal;
    }
    private void load_dashboard()
    {
        SearchStorage obj_SearchStorage = new SearchStorage();
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        obj_SearchStorage.Scheme_Id = Scheme_Id;
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.FromDate = txtDateFrom.Text.Trim();
        obj_SearchStorage.TillDate = txtDateTill.Text.Trim();
        obj_SearchStorage.Search_By = rbtSearchBy.SelectedValue;

        Session["SearchStorage"] = obj_SearchStorage;

        grdADPV.DataSource = null;
        grdADPV.DataBind();

        grdMAV.DataSource = null;
        grdMAV.DataBind();

        grdDRV.DataSource = null;
        grdDRV.DataBind();

        grdInvoiceDashV.DataSource = null;
        grdInvoiceDashV.DataBind();

        get_PackageInvoice_Summery();
        get_PackageADPInvoice_Summery();
        get_Package_MA_Invoice_Summery();
        get_Package_DR_Invoice_Summery();
        Data_Updation_Dashboard();

        get_tbl_PackageInvoice(true);
        get_tbl_PackageInvoiceMA(true);
        get_tbl_Package_DeductionRelease(true);
        get_tbl_PackageInvoiceADP(true);
        get_AnnextureSummery();
        set_Labels();
    }
    private void get_AnnextureSummery()
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Annexture_Dashboard_Details(Scheme_Id, Zone_Id, Circle_Id, Division_Id, District_Id, ULB_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            grdAnnextureDtls.DataSource = ds.Tables[0];
            grdAnnextureDtls.DataBind();
        }
        else
        {
            grdAnnextureDtls.DataSource = null;
            grdAnnextureDtls.DataBind();
        }
    }
    private void set_Labels()
    {
        sp_Invoice.InnerHtml = grdInvoice.Rows.Count.ToString();
        sp_OtherDept.InnerHtml = grdADP.Rows.Count.ToString();
        sp_OtherPayment.InnerHtml = grdMA.Rows.Count.ToString();
        sp_DeductionRelease.InnerHtml = grdDeductionRelease.Rows.Count.ToString();
    }

    private void Data_Updation_Dashboard()
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Data_Updation_Dashboard(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            grdDataUpdationStatusReport.DataSource = ds.Tables[0];
            grdDataUpdationStatusReport.DataBind();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkTotal_P_F") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Project)", "").ToString();
            grdDataUpdationStatusReport.FooterRow.Cells[4].Text = ds.Tables[0].Compute("sum(Total_Package)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkBOQ_NA_F") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_BOQ_Not_Available)", "").ToString();
            grdDataUpdationStatusReport.FooterRow.Cells[6].Text = ds.Tables[0].Compute("sum(Total_Freezed)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkUnFreezed_F") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Not_Freezed)", "").ToString();
            grdDataUpdationStatusReport.FooterRow.Cells[8].Text = ds.Tables[0].Compute("sum(Total_RA_Bills_Filled)", "").ToString() + " / " + ds.Tables[0].Compute("sum(Total_RA_Bills)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkStep_0_F") as LinkButton).Text = ds.Tables[0].Compute("sum(At_Step_0)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkStep_1_F") as LinkButton).Text = ds.Tables[0].Compute("sum(At_Step_1)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkStep_2_F") as LinkButton).Text = ds.Tables[0].Compute("sum(At_Step_2)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkStep_3_F") as LinkButton).Text = ds.Tables[0].Compute("sum(At_Step_3)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkStep_4_F") as LinkButton).Text = ds.Tables[0].Compute("sum(At_Step_4)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkStep_5_F") as LinkButton).Text = ds.Tables[0].Compute("sum(At_Step_5)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkStep_6_F") as LinkButton).Text = ds.Tables[0].Compute("sum(At_Step_6)", "").ToString();
            (grdDataUpdationStatusReport.FooterRow.FindControl("lnkStep_7_F") as LinkButton).Text = ds.Tables[0].Compute("sum(At_Step_7)", "").ToString();
        }
        else
        {
            grdDataUpdationStatusReport.DataSource = null;
            grdDataUpdationStatusReport.DataBind();
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
    private void get_tbl_Zone()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlZone, "Zone_Name", "Zone_Id");
        }
        else
        {
            ddlZone.Items.Clear();
        }
    }

    private void get_tbl_Circle(int Zone_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Circle(Zone_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlCircle, "Circle_Name", "Circle_Id");
        }
        else
        {
            ddlCircle.Items.Clear();
        }
    }
    private void get_tbl_Division(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division(Circle_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDivision, "Division_Name", "Division_Id");
        }
        else
        {
            ddlDivision.Items.Clear();
        }
    }

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlZone.SelectedValue == "0")
        {
            ddlCircle.Items.Clear();
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
        }
    }

    protected void ddlCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCircle.SelectedValue == "0")
        {
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue));
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

    private void get_PackageInvoice_Summery()
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_Invoice_TAT_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, 0, 0, false, "", "", false, District_Id, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_Invoice_TAT_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, "", "", false, District_Id, ULB_Id);
            }
        }
        else
        {
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_Invoice_TAT_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, 0, 0, false, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_Invoice_TAT_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
            }
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdSummery.DataSource = ds.Tables[0];
            grdSummery.DataBind();

            grdSummery.FooterRow.Cells[1].Text = "Total:";
            grdSummery.FooterRow.Cells[2].Text = ds.Tables[0].Compute("sum(Total_Invoice)", "").ToString();
            (grdSummery.FooterRow.FindControl("lnkTotalInvoicePendingF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Pending)", "").ToString();
            (grdSummery.FooterRow.FindControl("lnkTotalInvoiceMarkedF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Marked)", "").ToString();
            (grdSummery.FooterRow.FindControl("lnkTotalInvoiceDeEscelatedHOF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_DeEscelated_To_HO)", "").ToString();
            (grdSummery.FooterRow.FindControl("lnkTotalInvoicePaymentF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Approved)", "").ToString();
            (grdSummery.FooterRow.FindControl("lnkTotalInvoicePendingEF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Pending_E)", "").ToString();
            (grdSummery.FooterRow.FindControl("lnkTotalInvoiceRejectedF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Deferred)", "").ToString();
            (grdSummery.FooterRow.FindControl("lnkTotalDeEscelatedF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_DeEscelated)", "").ToString();
        }
        else
        {
            grdSummery.DataSource = null;
            grdSummery.DataBind();
        }
    }

    private void get_PackageADPInvoice_Summery()
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_OtherDept_TAT_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, 0, 0, false, "", "", false, District_Id, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_OtherDept_TAT_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, "", "", false, District_Id, ULB_Id);
            }
        }
        else
        {
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_OtherDept_TAT_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, 0, 0, false, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_OtherDept_TAT_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
            }
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdSummeryADP.DataSource = ds.Tables[0];
            grdSummeryADP.DataBind();

            grdSummeryADP.FooterRow.Cells[1].Text = "Total:";
            grdSummeryADP.FooterRow.Cells[2].Text = ds.Tables[0].Compute("sum(Total_Invoice)", "").ToString();
            (grdSummeryADP.FooterRow.FindControl("lnkTotalInvoicePendingADPF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Pending)", "").ToString();
            (grdSummeryADP.FooterRow.FindControl("lnkTotalInvoiceMarkedADPF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Marked)", "").ToString();
            (grdSummeryADP.FooterRow.FindControl("lnkTotalInvoicePaymentADPF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Approved)", "").ToString();
            (grdSummeryADP.FooterRow.FindControl("lnkTotalInvoicePendingADPEF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Pending_E)", "").ToString();
            (grdSummeryADP.FooterRow.FindControl("lnkTotalInvoiceRejectedADPF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Deferred)", "").ToString();
        }
        else
        {
            grdSummeryADP.DataSource = null;
            grdSummeryADP.DataBind();
        }
    }
    private void get_Package_MA_Invoice_Summery()
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_MA_TAT_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, 0, 0, false, "", "", false, District_Id, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_MA_TAT_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, "", "", false, District_Id, ULB_Id);
            }
        }
        else
        {
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_MA_TAT_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, 0, 0, false, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_MA_TAT_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
            }
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdSummeryMA.DataSource = ds.Tables[0];
            grdSummeryMA.DataBind();

            grdSummeryMA.FooterRow.Cells[1].Text = "Total:";
            grdSummeryMA.FooterRow.Cells[2].Text = ds.Tables[0].Compute("sum(Total_Invoice)", "").ToString();
            (grdSummeryMA.FooterRow.FindControl("lnkTotalInvoicePendingMAF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Pending)", "").ToString();
            (grdSummeryMA.FooterRow.FindControl("lnkTotalInvoiceMarkedMAF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Marked)", "").ToString();
            (grdSummeryMA.FooterRow.FindControl("lnkTotalInvoicePaymentMAF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Approved)", "").ToString();
            (grdSummeryMA.FooterRow.FindControl("lnkTotalInvoicePendingMAEF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Pending_E)", "").ToString();
            (grdSummeryMA.FooterRow.FindControl("lnkTotalInvoiceRejectedMAF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Deferred)", "").ToString();
        }
        else
        {
            grdSummeryMA.DataSource = null;
            grdSummeryMA.DataBind();
        }
    }

    private void get_Package_DR_Invoice_Summery()
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_DR_TAT_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, 0, 0, false, "", "", false, District_Id, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_DR_TAT_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, "", "", false, District_Id, ULB_Id);
            }
        }
        else
        {
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_DR_TAT_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, 0, 0, false, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_DR_TAT_Report(Zone_Id, Circle_Id, Division_Id, Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
            }
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdSummeryDR.DataSource = ds.Tables[0];
            grdSummeryDR.DataBind();

            grdSummeryDR.FooterRow.Cells[1].Text = "Total:";
            grdSummeryDR.FooterRow.Cells[2].Text = ds.Tables[0].Compute("sum(Total_Invoice)", "").ToString();

            (grdSummeryDR.FooterRow.FindControl("lnkTotalInvoicePendingDRF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Pending)", "").ToString();
            (grdSummeryDR.FooterRow.FindControl("lnkTotalInvoiceMarkedDRF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Marked)", "").ToString();
            (grdSummeryDR.FooterRow.FindControl("lnkTotalInvoicePaymentDRF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Approved)", "").ToString();
            (grdSummeryDR.FooterRow.FindControl("lnkTotalInvoicePendingDREF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Pending_E)", "").ToString();
            (grdSummeryDR.FooterRow.FindControl("lnkTotalInvoiceRejectedDRF") as LinkButton).Text = ds.Tables[0].Compute("sum(Total_Deferred)", "").ToString();
        }
        else
        {
            grdSummeryDR.DataSource = null;
            grdSummeryDR.DataBind();
        }
    }

    private void get_tbl_PackageInvoiceADP(bool is_Pendency)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        bool? isDefered = null;
        if (rbtSearchBy.SelectedValue == "1")
        {
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_tbl_Package_ADP(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", 0, 0, true, "", "", 0, 0, 0, false, isDefered, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_tbl_Package_ADP(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, "", "", 0, 0, 0, false, isDefered, ULB_Id);
            }
        }
        else
        {
            string fromDate = "";
            string tillDate = "";
            if (is_Pendency)
            {
                fromDate = "";
                tillDate = "";
            }
            else
            {
                fromDate = txtDateFrom.Text;
                tillDate = txtDateTill.Text;
            }
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_tbl_Package_ADP(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", 0, 0, true, fromDate, tillDate, 0, 0, 0, false, isDefered, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_tbl_Package_ADP(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, fromDate, tillDate, 0, 0, 0, false, isDefered, ULB_Id);
            }
        }

        if (AllClasses.CheckDataSet(ds))
        {
            btnMarkADP.Visible = false;
            grdADP.DataSource = ds.Tables[0];
            grdADP.DataBind();
            if (is_Pendency)
            {
                for (int i = 0; i < grdADP.Rows.Count; i++)
                {
                    CheckBox chkMark = grdADP.Rows[i].FindControl("chkMark") as CheckBox;
                    if (chkMark.Visible)
                    {
                        btnMarkADP.Visible = true;
                    }
                }
            }
        }
        else
        {
            btnMarkADP.Visible = false;
            grdADP.DataSource = null;
            grdADP.DataBind();
        }
    }

    private void get_tbl_PackageInvoiceMA(bool is_Pendency)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
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

        if (rbtSearchBy.SelectedValue == "1")
        {
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_tbl_Package_MobilizationAdvance(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", 0, 0, true, "", "", Expert_Person_Id, 0, 0, false, isDefered, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_tbl_Package_MobilizationAdvance(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, "", "", Expert_Person_Id, 0, 0, false, isDefered, ULB_Id);
            }
        }
        else
        {
            string fromDate = "";
            string tillDate = "";
            if (is_Pendency)
            {
                fromDate = "";
                tillDate = "";
            }
            else
            {
                fromDate = txtDateFrom.Text;
                tillDate = txtDateTill.Text;
            }
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_tbl_Package_MobilizationAdvance(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", 0, 0, true, fromDate, tillDate, Expert_Person_Id, 0, 0, false, isDefered, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_tbl_Package_MobilizationAdvance(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, fromDate, tillDate, Expert_Person_Id, 0, 0, false, isDefered, ULB_Id);
            }
        }

        if (AllClasses.CheckDataSet(ds))
        {
            btnMarkMA.Visible = false;
            grdMA.DataSource = ds.Tables[0];
            grdMA.DataBind();
            if (is_Pendency)
            {
                for (int i = 0; i < grdMA.Rows.Count; i++)
                {
                    CheckBox chkMark = grdMA.Rows[i].FindControl("chkMark") as CheckBox;
                    if (chkMark.Visible)
                    {
                        btnMarkMA.Visible = true;
                    }
                }
            }
        }
        else
        {
            btnMarkMA.Visible = false;
            grdMA.DataSource = null;
            grdMA.DataBind();
        }
    }

    private void get_tbl_Package_DeductionRelease(bool is_Pendency)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
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
        if (rbtSearchBy.SelectedValue == "1")
        {
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_tbl_Package_DeductionRelease(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", 0, 0, true, "", "", Expert_Person_Id, 0, 0, false, isDefered, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_tbl_Package_DeductionRelease(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, "", "", Expert_Person_Id, 0, 0, false, isDefered, ULB_Id);
            }
        }
        else
        {
            string fromDate = "";
            string tillDate = "";
            if (is_Pendency)
            {
                fromDate = "";
                tillDate = "";
            }
            else
            {
                fromDate = txtDateFrom.Text;
                tillDate = txtDateTill.Text;
            }
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_tbl_Package_DeductionRelease(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", 0, 0, true, fromDate, tillDate, Expert_Person_Id, 0, 0, false, isDefered, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_tbl_Package_DeductionRelease(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, fromDate, tillDate, Expert_Person_Id, 0, 0, false, isDefered, ULB_Id);
            }
        }

        if (AllClasses.CheckDataSet(ds))
        {
            btnMarkDR.Visible = false;
            grdDeductionRelease.DataSource = ds.Tables[0];
            grdDeductionRelease.DataBind();
            if (is_Pendency)
            {
                for (int i = 0; i < grdDeductionRelease.Rows.Count; i++)
                {
                    CheckBox chkMark = grdDeductionRelease.Rows[i].FindControl("chkMark") as CheckBox;
                    if (chkMark.Visible)
                    {
                        btnMarkDR.Visible = true;
                    }
                }
            }
        }
        else
        {
            btnMarkDR.Visible = false;
            grdDeductionRelease.DataSource = null;
            grdDeductionRelease.DataBind();
        }
    }

    private void get_tbl_PackageInvoice(bool is_Pendency)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        string ExpertType = "";
        if (is_Pendency)
        {
            ExpertType = "";
        }
        else
        {
            ExpertType = "SMD";
        }
        DataSet ds = new DataSet();
        bool? isDefered = null;
        if (rbtSearchBy.SelectedValue == "1")
        {
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_tbl_PackageInvoice(0, Zone_Id, Circle_Id, Division_Id, Scheme_Id, 0, 0, false, "", "", 0, 0, -1, false, -1, isDefered, ExpertType, District_Id, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_tbl_PackageInvoice(0, Zone_Id, Circle_Id, Division_Id, Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, "", "", 0, 0, -1, false, -1, isDefered, ExpertType, District_Id, ULB_Id);
            }
        }
        else
        {
            string fromDate = "";
            string tillDate = "";
            if (is_Pendency)
            {
                fromDate = "";
                tillDate = "";
            }
            else
            {
                fromDate = txtDateFrom.Text;
                tillDate = txtDateTill.Text;
            }
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_tbl_PackageInvoice(0, Zone_Id, Circle_Id, Division_Id, Scheme_Id, 0, 0, false, fromDate, tillDate, 0, 0, -1, false, -1, isDefered, ExpertType, District_Id, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_tbl_PackageInvoice(0, Zone_Id, Circle_Id, Division_Id, Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, fromDate, tillDate, 0, 0, -1, false, -1, isDefered, ExpertType, District_Id, ULB_Id);
            }
        }

        if (AllClasses.CheckDataSet(ds))
        {
            btnMark.Visible = false;
            grdInvoice.DataSource = ds.Tables[0];
            grdInvoice.DataBind();
            if (is_Pendency)
            {
                for (int i = 0; i < grdInvoice.Rows.Count; i++)
                {
                    CheckBox chkMark = grdInvoice.Rows[i].FindControl("chkMark") as CheckBox;
                    if (chkMark.Visible)
                    {
                        btnMark.Visible = true;
                    }
                }
            }
        }
        else
        {
            btnMark.Visible = false;
            grdInvoice.DataSource = null;
            grdInvoice.DataBind();
        }
    }
    protected void grdInvoiceDashV_PreRender(object sender, EventArgs e)
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
        string PackageInvoice_Type = gr.Cells[30].Text.Trim();

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

    protected void btnOpenInvoiceV_Click(object sender, ImageClickEventArgs e)
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
        string PackageInvoice_Type = gr.Cells[29].Text.Trim();
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
            ImageButton btnCover = e.Row.FindControl("btnCover") as ImageButton;
            ImageButton btnOpenInvoice = e.Row.FindControl("btnOpenInvoice") as ImageButton;
            CheckBox chkMark = e.Row.FindControl("chkMark") as CheckBox;
            int PackageInvoiceCover_Id = 0;

            int Designation_Id = 0;
            int Organization_Id = 0;
            int PackageInvoice_Id = 0;
            int Scheme_Id = 0;
            try
            {
                PackageInvoice_Id = Convert.ToInt32(e.Row.Cells[0].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                PackageInvoice_Id = 0;
            }
            try
            {
                Organization_Id = Convert.ToInt32(e.Row.Cells[3].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                Organization_Id = 0;
            }
            try
            {
                Designation_Id = Convert.ToInt32(e.Row.Cells[4].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                Designation_Id = 0;
            }
            try
            {
                Scheme_Id = Convert.ToInt32(e.Row.Cells[6].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                Scheme_Id = 0;
            }
            if (Session["UserType"].ToString() == "1")
            {
                btnOpenInvoice.Visible = true;
            }
            else
            {
                if (Session["Person_BranchOffice_Id"].ToString() == Organization_Id.ToString() && Session["PersonJuridiction_DesignationId"].ToString() == Designation_Id.ToString())
                {
                    btnOpenInvoice.Visible = true;
                }
                else
                {
                    btnOpenInvoice.Visible = false;
                }
            }

            if (Session["PersonJuridiction_DesignationId"].ToString() == "4" || Session["PersonJuridiction_DesignationId"].ToString() == "33" || Session["PersonJuridiction_DesignationId"].ToString() == "9" || Session["PersonJuridiction_DesignationId"].ToString() == "1056")
            {
                try
                {
                    PackageInvoiceCover_Id = Convert.ToInt32(e.Row.Cells[5].Text.Trim().Replace("&nbsp;", ""));
                }
                catch
                {
                    PackageInvoiceCover_Id = 0;
                }
                if (PackageInvoiceCover_Id > 0)
                {
                    btnOpenInvoice.Visible = true;
                }
                else
                {
                    btnOpenInvoice.Visible = false;
                }
                btnCover.Visible = true;
            }
            else
            {
                btnCover.Visible = false;
            }
            Set_Mark_Status(PackageInvoice_Id, Scheme_Id, chkMark);
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
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkMark = e.Row.FindControl("chkMark") as CheckBox;
            int _Loop = 0;
            int Package_ADP_Id = 0;
            int Scheme_Id = 0;
            try
            {
                _Loop = Convert.ToInt32(e.Row.Cells[4].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                _Loop = 0;
            }
            try
            {
                Package_ADP_Id = Convert.ToInt32(e.Row.Cells[3].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                Package_ADP_Id = 0;
            }
            try
            {
                Scheme_Id = Convert.ToInt32(e.Row.Cells[2].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                Scheme_Id = 0;
            }
            DataSet ds = new DataSet();
            ds = (new DataLayer()).get_ProcessConfig_Current(Scheme_Id, "PackageADP", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), _Loop, Package_ADP_Id);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int ConfigMaster_Id = 0;
                try
                {
                    ConfigMaster_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ProcessConfigMaster_Id"].ToString());
                    if (get_tbl_InvoiceStatus_ADP(ConfigMaster_Id, Scheme_Id))
                    {
                        chkMark.Visible = true;
                    }
                    else
                    {
                        chkMark.Visible = false;
                    }
                }
                catch
                { }
            }
            else
            {
                chkMark.Visible = false;
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        if (Scheme_Id == "")
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
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Comments = "";
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Status_Id = 4;
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_PackageInvoice_Id = PackageInvoice_Id;
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Status = 1;
                    obj_tbl_PackageInvoiceApproval.Scheme_Id = Convert.ToInt32(Convert.ToInt32(grdInvoice.Rows[i].Cells[6].Text.Trim()));
                    obj_tbl_PackageInvoiceApproval_Li.Add(obj_tbl_PackageInvoiceApproval);
                }
            }
        }
        if (obj_tbl_PackageInvoiceApproval_Li.Count == 0)
        {
            MessageBox.Show("Please Select Atleast One Invoice To Mark");
            return;
        }
        else
        {
            if (new DataLayer().Update_tbl_PackageInvoice_Mark(obj_tbl_PackageInvoiceApproval_Li, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString())))
            {
                MessageBox.Show("Invoice Marked Successfully");
                get_tbl_PackageInvoice(true);
                return;
            }
            else
            {
                MessageBox.Show("Unable To Mark Invoice");
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
        int _Loop = 0;
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
                    obj_tbl_PackageADPApproval.PackageADPApproval_Comments = "";
                    obj_tbl_PackageADPApproval.PackageADPApproval_Status_Id = 4;
                    obj_tbl_PackageADPApproval.PackageADPApproval_Package_ADP_Id = PackageADP_Id;
                    obj_tbl_PackageADPApproval.PackageADPApproval_Package_Id = Convert.ToInt32(grdADP.Rows[i].Cells[0].Text.Trim());
                    obj_tbl_PackageADPApproval.PackageADPApproval_Status = 1;
                    obj_tbl_PackageADPApproval.PackageADPApproval_Step_Count = 1;
                    obj_tbl_PackageADPApproval.Scheme_Id = Convert.ToInt32(grdADP.Rows[i].Cells[2].Text.Trim());
                    obj_tbl_PackageADPApproval.Loop = Convert.ToInt32(grdADP.Rows[i].Cells[4].Text.Trim());
                    obj_tbl_PackageADPApproval_Li.Add(obj_tbl_PackageADPApproval);
                }
            }
        }
        if (obj_tbl_PackageADPApproval_Li.Count == 0 || obj_tbl_PackageADPApproval_Li == null)
        {
            MessageBox.Show("Please Select At-least One Other Departmental To Mark");
            return;
        }
        else
        {
            if (new DataLayer().Update_tbl_Package_ADP_Item_Mark(obj_tbl_PackageADPApproval_Li, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString())))
            {
                MessageBox.Show("Other Departmental Marked Successfully");
                get_tbl_PackageInvoiceADP(true);
                return;
            }
            else
            {
                MessageBox.Show("Unable To Mark Other Departmental");
                return;
            }
        }
    }

    protected void btnCover_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        string Invoice_Id = gr.Cells[0].Text.Trim();
        string Package_Id = gr.Cells[1].Text.Trim();
        string ProcessedBy = gr.Cells[2].Text.Trim().Replace("&nbsp;", "");
        Response.Redirect("MasterGenerateCoverLetter?Invoice_Id=" + Invoice_Id);
    }

    protected void grdSummery_PreRender(object sender, EventArgs e)
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

    protected void chkMarkH_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkMarkH = sender as CheckBox;
        for (int i = 0; i < grdInvoice.Rows.Count; i++)
        {
            CheckBox chkMark = grdInvoice.Rows[i].FindControl("chkMark") as CheckBox;
            chkMark.Checked = chkMarkH.Checked;
        }
    }

    protected void lnkTotalInvoice_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummery.Rows.Count; i++)
        {
            grdSummery.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[10].Text.Trim());
        get_tbl_PackageInvoice("Total", TAT_Range);
    }

    protected void lnkTotalInvoicePending_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummery.Rows.Count; i++)
        {
            grdSummery.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[10].Text.Trim());
        get_tbl_PackageInvoice("Pending", TAT_Range);
    }

    protected void lnkTotalInvoiceMarked_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummery.Rows.Count; i++)
        {
            grdSummery.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[10].Text.Trim());
        get_tbl_PackageInvoice("Marked", TAT_Range);
    }

    protected void lnkTotalInvoicePayment_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummery.Rows.Count; i++)
        {
            grdSummery.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[10].Text.Trim());
        get_tbl_PackageInvoice("Payment", TAT_Range);
    }

    private void get_tbl_PackageInvoice(string Display_Data, int TAT_Range)
    {
        grdADPV.DataSource = null;
        grdADPV.DataBind();

        grdMAV.DataSource = null;
        grdMAV.DataBind();

        grdDRV.DataSource = null;
        grdDRV.DataBind();

        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageInvoice_SMD(Zone_Id, Circle_Id, Division_Id, Scheme_Id, Display_Data, TAT_Range, false, District_Id, ULB_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdInvoiceDashV.DataSource = ds.Tables[0];
            grdInvoiceDashV.DataBind();
        }
        else
        {
            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();
        }
    }

    protected void btnInfoExp_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummery.Rows.Count; i++)
        {
            grdSummery.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[10].Text.Trim());
        get_Invoice_On_Expert(TAT_Range, 0);
    }

    private void get_Invoice_On_Expert(int TAT_Range, int _mode)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_Invoice_On_Expert(Zone_Id, Circle_Id, Division_Id, Scheme_Id, TAT_Range, _mode, _mode, "", "", false, "SMD", District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Invoice_On_Expert(Zone_Id, Circle_Id, Division_Id, Scheme_Id, TAT_Range, _mode, _mode, txtDateFrom.Text, txtDateTill.Text, false, "SMD", District_Id, ULB_Id);
        }
        if (AllClasses.CheckDataSet(ds))
        {
            if (_mode == -1)
            {
                sp_HeaderText.InnerHtml = "Expert Wise Approved Invoice Count";
                hf_ClickMode.Value = "Expert Wise Approved";
            }
            else if (_mode == -2)
            {
                sp_HeaderText.InnerHtml = "Expert Wise Deferred Invoice Count";
                hf_ClickMode.Value = "Expert Wise Deferred";
            }
            else if (_mode == -3)
            {
                sp_HeaderText.InnerHtml = "Expert Wise Pending Invoice Count";
                hf_ClickMode.Value = "Expert Wise Pending";
            }
            else
            {
                sp_HeaderText.InnerHtml = "Expert Wise Marked Invoice Count";
                hf_ClickMode.Value = "Expert Wise Marked";
            }
            hf_TAT_Range.Value = TAT_Range.ToString();
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void grdExpertWiseDtls_PreRender(object sender, EventArgs e)
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

    protected void grdDataUpdationStatusReport_PreRender(object sender, EventArgs e)
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

    protected void lnkBOQ_NA_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;
        string Scheme_Id = "";
        int District_Id = 0;
        int ULB_Id = 0;


        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg_Dashboard(District_Id, Scheme_Id, Zone_Id, 0, 0, false, true, ULB_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPkgView.DataSource = ds.Tables[0];
            grdPkgView.DataBind();
            mpViewSummery.Show();
        }
        else
        {
            grdPkgView.DataSource = null;
            grdPkgView.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void lnkUnFreezed_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;
        string Scheme_Id = "";
        int District_Id = 0;
        int ULB_Id = 0;


        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg_Dashboard(District_Id, Scheme_Id, Zone_Id, 0, 0, true, false, ULB_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPkgView.DataSource = ds.Tables[0];
            grdPkgView.DataBind();
            mpViewSummery.Show();
        }
        else
        {
            grdPkgView.DataSource = null;
            grdPkgView.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void grdPkgView_PreRender(object sender, EventArgs e)
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

    protected void btnInfoExpA_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummery.Rows.Count; i++)
        {
            grdSummery.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[10].Text.Trim());
        get_Invoice_On_Expert(TAT_Range, -1);
    }

    protected void lnkBOQ_NA_F_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        string Scheme_Id = "";
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        int District_Id = 0;
        int ULB_Id = 0;


        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg_Dashboard(District_Id, Scheme_Id, Zone_Id, 0, 0, false, true, ULB_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPkgView.DataSource = ds.Tables[0];
            grdPkgView.DataBind();
            mpViewSummery.Show();
        }
        else
        {
            grdPkgView.DataSource = null;
            grdPkgView.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void lnkUnFreezed_F_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        string Scheme_Id = "";
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        int District_Id = 0;
        int ULB_Id = 0;


        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg_Dashboard(District_Id, Scheme_Id, Zone_Id, 0, 0, true, false, ULB_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPkgView.DataSource = ds.Tables[0];
            grdPkgView.DataBind();
            mpViewSummery.Show();
        }
        else
        {
            grdPkgView.DataSource = null;
            grdPkgView.DataBind();
            MessageBox.Show("No Records Found");
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
    protected void btnEditADP_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        string Scheme_Id = gr.Cells[2].Text.Trim();
        SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
        obj_SearchStorage.Scheme_Id = Scheme_Id;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        obj_SearchStorage.Zone_Id = Zone_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.FromDate = txtDateFrom.Text.Trim();
        obj_SearchStorage.TillDate = txtDateTill.Text.Trim();
        obj_SearchStorage.Search_By = rbtSearchBy.SelectedValue;

        Session["SearchStorage"] = obj_SearchStorage;
        int Invoice_Id = 0;
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

    protected void lnkTotalInvoiceRejected_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummery.Rows.Count; i++)
        {
            grdSummery.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[10].Text.Trim());

        get_tbl_PackageInvoice("Deferred", TAT_Range);
    }

    protected void chkMarkAH_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkMarkAH = sender as CheckBox;
        for (int i = 0; i < grdADP.Rows.Count; i++)
        {
            CheckBox chkMark = grdADP.Rows[i].FindControl("chkMark") as CheckBox;
            chkMark.Checked = chkMarkAH.Checked;
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

    protected void grdMA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[10].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[11].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[12].Text = Session["Default_Division"].ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkMark = e.Row.FindControl("chkMark") as CheckBox;
            int _Loop = 0;
            int MobilizationAdvance_Id = 0;
            int Scheme_Id = 0;
            try
            {
                _Loop = Convert.ToInt32(e.Row.Cells[4].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                _Loop = 0;
            }
            try
            {
                MobilizationAdvance_Id = Convert.ToInt32(e.Row.Cells[3].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                MobilizationAdvance_Id = 0;
            }
            try
            {
                Scheme_Id = Convert.ToInt32(e.Row.Cells[2].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                Scheme_Id = 0;
            }
            DataSet ds = new DataSet();
            ds = (new DataLayer()).get_ProcessConfig_Current(Scheme_Id, "Package_MobilizationAdvance", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), _Loop, MobilizationAdvance_Id);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int ConfigMaster_Id = 0;
                try
                {
                    ConfigMaster_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ProcessConfigMaster_Id"].ToString());
                    if (get_tbl_InvoiceStatus_MA(ConfigMaster_Id, Scheme_Id))
                    {
                        chkMark.Visible = true;
                    }
                    else
                    {
                        chkMark.Visible = false;
                    }
                }
                catch
                { }
            }
            else
            {
                chkMark.Visible = false;
            }
        }
    }
    private bool get_tbl_InvoiceStatus_MA(int ConfigMasterId, int Scheme_Id)
    {
        bool rVal = false;

        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_MA(0, 0, 0, 0);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Package_MA(Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), ConfigMasterId);
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["InvoiceStatus_Id"].ToString() == "4" || ds.Tables[0].Rows[i]["InvoiceStatus_Id"].ToString() == "5")
                {
                    rVal = true;
                    break;
                }
            }
        }
        else
        {
            rVal = false;
        }
        return rVal;
    }
    protected void btnEditMA_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        string Scheme_Id = gr.Cells[2].Text.Trim();
        SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
        obj_SearchStorage.Scheme_Id = Scheme_Id;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        obj_SearchStorage.Zone_Id = Zone_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.FromDate = txtDateFrom.Text.Trim();
        obj_SearchStorage.TillDate = txtDateTill.Text.Trim();
        obj_SearchStorage.Search_By = rbtSearchBy.SelectedValue;

        Session["SearchStorage"] = obj_SearchStorage;
        int MobilizationAdvance_Id = 0;
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
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Comments = "";
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Status_Id = 4;
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Package_MobilizationAdvance_Id = MobilizationAdvance_Id;
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Package_Id = Convert.ToInt32(grdMA.Rows[i].Cells[0].Text.Trim());
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Status = 1;
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Step_Count = 1;
                    obj_tbl_Package_MobilizationAdvanceApproval.Scheme_Id = Convert.ToInt32(grdMA.Rows[i].Cells[2].Text.Trim());
                    obj_tbl_Package_MobilizationAdvanceApproval.Loop = Convert.ToInt32(grdMA.Rows[i].Cells[4].Text.Trim());
                    obj_tbl_Package_MobilizationAdvanceApproval_Li.Add(obj_tbl_Package_MobilizationAdvanceApproval);
                }
            }
        }
        if (obj_tbl_Package_MobilizationAdvanceApproval_Li.Count == 0 || obj_tbl_Package_MobilizationAdvanceApproval_Li == null)
        {
            MessageBox.Show("Please Select Atleast One Mobilization Advance To Mark");
            return;
        }
        else
        {
            if (new DataLayer().Update_tbl_Package_MA_Item_Mark(obj_tbl_Package_MobilizationAdvanceApproval_Li, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString())))
            {
                MessageBox.Show("Mobilization Advance Marked Successfully");
                get_tbl_PackageInvoiceMA(true);
                return;
            }
            else
            {
                MessageBox.Show("Unable To Mark Mobilization Advance");
                return;
            }
        }
    }

    protected void chkMarkMA_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkMarkMA = sender as CheckBox;
        for (int i = 0; i < grdMA.Rows.Count; i++)
        {
            CheckBox chkMark = grdMA.Rows[i].FindControl("chkMark") as CheckBox;
            chkMark.Checked = chkMarkMA.Checked;
        }
    }

    protected void grdSummeryADP_PreRender(object sender, EventArgs e)
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

    protected void lnkTotalInvoiceADP_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryADP.Rows.Count; i++)
        {
            grdSummeryADP.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_tbl_PackageADPInvoice("Total", TAT_Range);
    }

    protected void lnkTotalInvoicePendingADP_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryADP.Rows.Count; i++)
        {
            grdSummeryADP.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_tbl_PackageADPInvoice("Pending", TAT_Range);
    }

    protected void lnkTotalInvoiceMarkedADP_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryADP.Rows.Count; i++)
        {
            grdSummeryADP.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_tbl_PackageADPInvoice("Marked", TAT_Range);
    }

    protected void btnInfoExpADP_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryADP.Rows.Count; i++)
        {
            grdSummeryADP.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_Invoice_ADP_On_Expert(TAT_Range, 0);
    }

    protected void lnkTotalInvoicePaymentADP_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryADP.Rows.Count; i++)
        {
            grdSummeryADP.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_tbl_PackageADPInvoice("Payment", TAT_Range);
    }

    protected void btnInfoExpAADP_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryADP.Rows.Count; i++)
        {
            grdSummeryADP.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_Invoice_ADP_On_Expert(TAT_Range, -1);
    }

    protected void lnkTotalInvoiceRejectedADP_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryADP.Rows.Count; i++)
        {
            grdSummeryADP.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_tbl_PackageADPInvoice("Deferred", TAT_Range);
    }

    private void get_tbl_PackageADPInvoice(string Display_Data, int TAT_Range)
    {
        grdInvoiceDashV.DataSource = null;
        grdInvoiceDashV.DataBind();

        grdMAV.DataSource = null;
        grdMAV.DataBind();

        grdDRV.DataSource = null;
        grdDRV.DataBind();

        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageInvoiceADP_SMD(Zone_Id, Circle_Id, Division_Id, Scheme_Id, Display_Data, TAT_Range, false, District_Id, ULB_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdADPV.DataSource = ds.Tables[0];
            grdADPV.DataBind();
        }
        else
        {
            grdADPV.DataSource = null;
            grdADPV.DataBind();
        }
    }

    private void get_Invoice_ADP_On_Expert(int TAT_Range, int _mode)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Invoice_ADP_On_Expert(Zone_Id, Circle_Id, Division_Id, Scheme_Id, TAT_Range, _mode, _mode, false, District_Id, ULB_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            if (_mode == -1)
            {
                sp_HeaderText.InnerHtml = "Expert Wise Approved Other Dept. Invoice Count";
                hf_ClickMode.Value = "Expert Wise Approved ADP";
            }
            else if (_mode == -2)
            {
                sp_HeaderText.InnerHtml = "Expert Wise Deferred Other Dept. Invoice Count";
                hf_ClickMode.Value = "Expert Wise Deferred ADP";
            }
            else if (_mode == -3)
            {
                sp_HeaderText.InnerHtml = "Expert Wise Pending Other Dept. Invoice Count";
                hf_ClickMode.Value = "Expert Wise Pending ADP";
            }
            else
            {
                sp_HeaderText.InnerHtml = "Expert Wise Marked Other Dept. Invoice Count";
                hf_ClickMode.Value = "Expert Wise Marked ADP";
            }
            hf_TAT_Range.Value = TAT_Range.ToString();
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void grdADPV_PreRender(object sender, EventArgs e)
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

    protected void grdDeductionRelease_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[10].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[11].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[12].Text = Session["Default_Division"].ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkMark = e.Row.FindControl("chkMark") as CheckBox;
            int _Loop = 0;
            int Package_DeductionRelease_Id = 0;
            int Scheme_Id = 0;
            try
            {
                _Loop = Convert.ToInt32(e.Row.Cells[4].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                _Loop = 0;
            }
            try
            {
                Package_DeductionRelease_Id = Convert.ToInt32(e.Row.Cells[3].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                Package_DeductionRelease_Id = 0;
            }
            try
            {
                Scheme_Id = Convert.ToInt32(e.Row.Cells[2].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                Scheme_Id = 0;
            }
            DataSet ds = new DataSet();
            ds = (new DataLayer()).get_ProcessConfig_Current(Scheme_Id, "PackageDeductionRelease", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), _Loop, Package_DeductionRelease_Id);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int ConfigMaster_Id = 0;
                try
                {
                    ConfigMaster_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ProcessConfigMaster_Id"].ToString());
                    if (get_tbl_InvoiceStatus_DeductionRelease(ConfigMaster_Id, Scheme_Id))
                    {
                        chkMark.Visible = true;
                    }
                    else
                    {
                        chkMark.Visible = false;
                    }
                }
                catch
                { }
            }
            else
            {
                chkMark.Visible = false;
            }
        }
    }

    protected void btnEditDR_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Package_DeductionRelease_Id = 0;
        string Scheme_Id = gr.Cells[2].Text.Trim();
        SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
        obj_SearchStorage.Scheme_Id = Scheme_Id;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        obj_SearchStorage.Zone_Id = Zone_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.FromDate = txtDateFrom.Text.Trim();
        obj_SearchStorage.TillDate = txtDateTill.Text.Trim();
        obj_SearchStorage.Search_By = rbtSearchBy.SelectedValue;

        Session["SearchStorage"] = obj_SearchStorage;
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

            CheckBox chkMark = grdDeductionRelease.Rows[i].FindControl("chkMark") as CheckBox;
            if (chkMark.Visible && chkMark.Checked)
            {
                if (Package_DeductionRelease_Id > 0)
                {
                    tbl_Package_DeductionReleaseApproval obj_tbl_Package_DeductionReleaseApproval = new tbl_Package_DeductionReleaseApproval();
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Comments = "";
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Status_Id = 4;
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Package_DeductionRelease_Id = Package_DeductionRelease_Id;
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Package_Id = Convert.ToInt32(grdDeductionRelease.Rows[i].Cells[0].Text.Trim());
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Status = 1;
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Step_Count = 1;
                    obj_tbl_Package_DeductionReleaseApproval.Scheme_Id = Convert.ToInt32(grdDeductionRelease.Rows[i].Cells[2].Text.Trim());
                    obj_tbl_Package_DeductionReleaseApproval.Loop = Convert.ToInt32(grdDeductionRelease.Rows[i].Cells[4].Text.Trim());
                    obj_tbl_Package_DeductionReleaseApproval_Li.Add(obj_tbl_Package_DeductionReleaseApproval);
                }
            }
        }
        if (obj_tbl_Package_DeductionReleaseApproval_Li.Count == 0 || obj_tbl_Package_DeductionReleaseApproval_Li == null)
        {
            MessageBox.Show("Please Select Atleast One Deduction Release To Mark");
            return;
        }
        else
        {
            if (new DataLayer().Update_tbl_Package_DeductionRelease_Item_Mark(obj_tbl_Package_DeductionReleaseApproval_Li, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString())))
            {
                MessageBox.Show("Deduction Release Marked Successfully");
                get_tbl_Package_DeductionRelease(true);
                return;
            }
            else
            {
                MessageBox.Show("Unable To Mark Deduction Release");
                return;
            }
        }
    }

    protected void chkMarkDR_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkMarkDR = sender as CheckBox;
        for (int i = 0; i < grdDeductionRelease.Rows.Count; i++)
        {
            CheckBox chkMark = grdDeductionRelease.Rows[i].FindControl("chkMark") as CheckBox;
            chkMark.Checked = chkMarkDR.Checked;
        }
    }

    protected void lnkZone_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;

        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        Response.Redirect("Report_Data_Updation_Status.aspx?Scheme_Id=" + Scheme_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id);
    }

    protected void lnkTotalInvoiceCount_Click(object sender, EventArgs e)
    {
        LinkButton lnkTotalInvoiceCount = sender as LinkButton;
        GridViewRow gr = lnkTotalInvoiceCount.Parent.Parent as GridViewRow;
        if (hf_ClickMode.Value == "0")
        {
            mpViewSummeryExpert.Show();
        }
        else
        {
            int Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
            int TAT_Range = Convert.ToInt32(hf_TAT_Range.Value);
            if (hf_ClickMode.Value == "Deferred Type Wise")
            {
                get_InvoiceDetails_According_ToPopup(-2, -2, Id, hf_ClickMode.Value, TAT_Range);
            }
            else if (hf_ClickMode.Value == "Expert Wise Deferred")
            {
                get_InvoiceDetails_According_ToPopup(-2, -2, Id, hf_ClickMode.Value, TAT_Range);
            }
            else if (hf_ClickMode.Value == "Expert Wise Approved")
            {
                get_InvoiceDetails_According_ToPopup(-1, -1, Id, hf_ClickMode.Value, TAT_Range);
            }
            else if (hf_ClickMode.Value == "Expert Wise Pending")
            {
                get_InvoiceDetails_According_ToPopup(2, 33, Id, hf_ClickMode.Value, TAT_Range);
            }
            else if (hf_ClickMode.Value == "Expert Wise Marked")
            {
                get_InvoiceDetails_According_ToPopup(0, 0, Id, hf_ClickMode.Value, TAT_Range);
            }

            if (hf_ClickMode.Value == "Deferred Type Wise ADP")
            {
                get_Invoice_ADP_Details_According_ToPopup(-2, -2, Id, hf_ClickMode.Value, TAT_Range);
            }
            else if (hf_ClickMode.Value == "Expert Wise Deferred ADP")
            {
                get_Invoice_ADP_Details_According_ToPopup(-2, -2, Id, hf_ClickMode.Value, TAT_Range);
            }
            else if (hf_ClickMode.Value == "Expert Wise Approved ADP")
            {
                get_Invoice_ADP_Details_According_ToPopup(-1, -1, Id, hf_ClickMode.Value, TAT_Range);
            }
            else if (hf_ClickMode.Value == "Expert Wise Pending ADP")
            {
                get_Invoice_ADP_Details_According_ToPopup(2, 33, Id, hf_ClickMode.Value, TAT_Range);
            }
            else if (hf_ClickMode.Value == "Expert Wise Marked ADP")
            {
                get_Invoice_ADP_Details_According_ToPopup(0, 0, Id, hf_ClickMode.Value, TAT_Range);
            }

            if (hf_ClickMode.Value == "Deferred Type Wise MA")
            {
                get_Invoice_MA_Details_According_ToPopup(-2, -2, Id, hf_ClickMode.Value, TAT_Range);
            }
            else if (hf_ClickMode.Value == "Expert Wise Deferred MA")
            {
                get_Invoice_MA_Details_According_ToPopup(-2, -2, Id, hf_ClickMode.Value, TAT_Range);
            }
            else if (hf_ClickMode.Value == "Expert Wise Approved MA")
            {
                get_Invoice_MA_Details_According_ToPopup(-1, -1, Id, hf_ClickMode.Value, TAT_Range);
            }
            else if (hf_ClickMode.Value == "Expert Wise Pending MA")
            {
                get_Invoice_MA_Details_According_ToPopup(2, 33, Id, hf_ClickMode.Value, TAT_Range);
            }
            else if (hf_ClickMode.Value == "Expert Wise Marked MA")
            {
                get_Invoice_MA_Details_According_ToPopup(0, 0, Id, hf_ClickMode.Value, TAT_Range);
            }

            if (hf_ClickMode.Value == "Deferred Type Wise DR")
            {
                get_Invoice_DR_Details_According_ToPopup(-2, -2, Id, hf_ClickMode.Value, TAT_Range);
            }
            else if (hf_ClickMode.Value == "Expert Wise Deferred DR")
            {
                get_Invoice_DR_Details_According_ToPopup(-2, -2, Id, hf_ClickMode.Value, TAT_Range);
            }
            else if (hf_ClickMode.Value == "Expert Wise Approved DR")
            {
                get_Invoice_DR_Details_According_ToPopup(-1, -1, Id, hf_ClickMode.Value, TAT_Range);
            }
            else if (hf_ClickMode.Value == "Expert Wise Pending DR")
            {
                get_Invoice_DR_Details_According_ToPopup(2, 33, Id, hf_ClickMode.Value, TAT_Range);
            }
            else if (hf_ClickMode.Value == "Expert Wise Marked DR")
            {
                get_Invoice_DR_Details_According_ToPopup(0, 0, Id, hf_ClickMode.Value, TAT_Range);
            }
        }
    }

    private void get_Invoice_DR_Details_According_ToPopup(int Org_Id, int Designation_Id, int Id, string DataMode, int TAT_Range)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        int Expert_Person_Id = 0;
        if (DataMode == "Expert Wise Deferred DR" || DataMode == "Expert Wise Approved DR" || DataMode == "Expert Wise Pending DR" || DataMode == "Expert Wise Marked DR")
        {
            Expert_Person_Id = Id;
        }
        else
        {
            Expert_Person_Id = 0;
        }
        int Additional_Status_Id = 0;
        if (DataMode == "Deferred Type Wise DR")
        {
            Additional_Status_Id = Id;
        }
        else
        {
            Additional_Status_Id = 0;
        }
        bool? isDefered = null;
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_DeductionRelease(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, true, "", "", Expert_Person_Id, Additional_Status_Id, TAT_Range, false, isDefered, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Package_DeductionRelease(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, true, txtDateFrom.Text, txtDateTill.Text, Expert_Person_Id, Additional_Status_Id, TAT_Range, false, isDefered, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdDRV.DataSource = ds.Tables[0];
            grdDRV.DataBind();

            grdADPV.DataSource = null;
            grdADPV.DataBind();

            grdMAV.DataSource = null;
            grdMAV.DataBind();

            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();
        }
        else
        {
            grdMAV.DataSource = null;
            grdMAV.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();

            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();

            grdADPV.DataSource = null;
            grdADPV.DataBind();
        }
    }
    private void get_Invoice_ADP_Details_According_ToPopup(int Org_Id, int Designation_Id, int Id, string DataMode, int TAT_Range)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        int Expert_Person_Id = 0;
        if (DataMode == "Expert Wise Deferred ADP" || DataMode == "Expert Wise Approved ADP" || DataMode == "Expert Wise Pending ADP" || DataMode == "Expert Wise Marked ADP")
        {
            Expert_Person_Id = Id;
        }
        else
        {
            Expert_Person_Id = 0;
        }
        int Additional_Status_Id = 0;
        if (DataMode == "Deferred Type Wise ADP")
        {
            Additional_Status_Id = Id;
        }
        else
        {
            Additional_Status_Id = 0;
        }
        bool? isDefered = null;
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_ADP(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, true, "", "", Expert_Person_Id, Additional_Status_Id, TAT_Range, false, isDefered, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Package_ADP(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, true, txtDateFrom.Text, txtDateTill.Text, Expert_Person_Id, Additional_Status_Id, TAT_Range, false, isDefered, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdADPV.DataSource = ds.Tables[0];
            grdADPV.DataBind();

            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();

            grdMAV.DataSource = null;
            grdMAV.DataBind();
        }
        else
        {
            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();

            grdADPV.DataSource = null;
            grdADPV.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();

            grdMAV.DataSource = null;
            grdMAV.DataBind();
        }
    }

    private void get_InvoiceDetails_According_ToPopup(int Org_Id, int Designation_Id, int Id, string DataMode, int TAT_Range)
    {
        grdADPV.DataSource = null;
        grdADPV.DataBind();

        grdMAV.DataSource = null;
        grdMAV.DataBind();

        grdDRV.DataSource = null;
        grdDRV.DataBind();

        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        int Expert_Person_Id = 0;
        if (DataMode == "Expert Wise Deferred" || DataMode == "Expert Wise Approved" || DataMode == "Expert Wise Pending" || DataMode == "Expert Wise Marked")
        {
            Expert_Person_Id = Id;
        }
        else
        {
            Expert_Person_Id = 0;
        }
        int Additional_Status_Id = 0;
        if (DataMode == "Deferred Type Wise")
        {
            Additional_Status_Id = Id;
        }
        else
        {
            Additional_Status_Id = 0;
        }

        bool? isDefered = null;
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_tbl_PackageInvoice(0, Zone_Id, Circle_Id, Division_Id, Scheme_Id, Org_Id, Designation_Id, false, "", "", Expert_Person_Id, Additional_Status_Id, TAT_Range, false, -1, isDefered, "SMD", District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_PackageInvoice(0, Zone_Id, Circle_Id, Division_Id, Scheme_Id, Org_Id, Designation_Id, false, txtDateFrom.Text, txtDateTill.Text, Expert_Person_Id, Additional_Status_Id, TAT_Range, false, -1, isDefered, "SMD", District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdInvoiceDashV.DataSource = ds.Tables[0];
            grdInvoiceDashV.DataBind();
        }
        else
        {
            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();
        }
    }

    private void get_Invoice_MA_Details_According_ToPopup(int Org_Id, int Designation_Id, int Id, string DataMode, int TAT_Range)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        int Expert_Person_Id = 0;
        if (DataMode == "Expert Wise Deferred MA" || DataMode == "Expert Wise Approved MA" || DataMode == "Expert Wise Pending MA" || DataMode == "Expert Wise Marked MA")
        {
            Expert_Person_Id = Id;
        }
        else
        {
            Expert_Person_Id = 0;
        }
        int Additional_Status_Id = 0;
        if (DataMode == "Deferred Type Wise MA")
        {
            Additional_Status_Id = Id;
        }
        else
        {
            Additional_Status_Id = 0;
        }
        bool? isDefered = null;
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_tbl_Package_MobilizationAdvance(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, true, "", "", Expert_Person_Id, Additional_Status_Id, TAT_Range, false, isDefered, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Package_MobilizationAdvance(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Org_Id, Designation_Id, true, txtDateFrom.Text, txtDateTill.Text, Expert_Person_Id, Additional_Status_Id, TAT_Range, false, isDefered, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            grdMAV.DataSource = ds.Tables[0];
            grdMAV.DataBind();

            grdADPV.DataSource = null;
            grdADPV.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();

            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();
        }
        else
        {
            grdMAV.DataSource = null;
            grdMAV.DataBind();

            grdInvoiceDashV.DataSource = null;
            grdInvoiceDashV.DataBind();

            grdADPV.DataSource = null;
            grdADPV.DataBind();

            grdDRV.DataSource = null;
            grdDRV.DataBind();
        }
    }

    protected void lnkInfoExpR_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummery.Rows.Count; i++)
        {
            grdSummery.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[10].Text.Trim());
        get_Invoice_On_Expert(TAT_Range, -2);
    }

    protected void lnkRejectionWise_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        LinkButton lnkTotalInvoiceCount = sender as LinkButton;
        GridViewRow gr = lnkTotalInvoiceCount.Parent.Parent as GridViewRow;
        int TAT_Range = Convert.ToInt32(gr.Cells[10].Text.Trim());
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_Invoice_On_Expert_Rejected_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id, TAT_Range, "", "", false, "SMD", District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Invoice_On_Expert_Rejected_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id, TAT_Range, txtDateFrom.Text, txtDateTill.Text, false, "SMD", District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = "Deferred Type Wise Invoice Count";
            hf_ClickMode.Value = "Deferred Type Wise";
            hf_TAT_Range.Value = TAT_Range.ToString();
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void btnOpenTimeline_Click(object sender, ImageClickEventArgs e)
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

    protected void lnkTotalInvoiceRejectedF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageInvoice("Deferred", 0);
    }

    protected void lnkInfoExpRF_Click(object sender, EventArgs e)
    {
        get_Invoice_On_Expert(0, -2);
    }

    protected void lnkRejectionWiseF_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        LinkButton lnkTotalInvoiceCount = sender as LinkButton;
        GridViewRow gr = lnkTotalInvoiceCount.Parent.Parent as GridViewRow;
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_Invoice_On_Expert_Rejected_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id, 0, "", "", false, "SMD", District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Invoice_On_Expert_Rejected_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id, 0, txtDateFrom.Text, txtDateTill.Text, false, "SMD", District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = "Deferred Type Wise Invoice Count";
            hf_ClickMode.Value = "Deferred Type Wise";
            hf_TAT_Range.Value = "0";
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void grdSummeryMA_PreRender(object sender, EventArgs e)
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
    protected void grdSummeryDR_PreRender(object sender, EventArgs e)
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
    protected void grdMAV_PreRender(object sender, EventArgs e)
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

    protected void grdDRV_PreRender(object sender, EventArgs e)
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
    protected void lnkTotalInvoiceMA_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryMA.Rows.Count; i++)
        {
            grdSummeryMA.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_tbl_PackageMAInvoice("Total", TAT_Range);
    }

    private void get_tbl_PackageMAInvoice(string Display_Data, int TAT_Range)
    {
        grdInvoiceDashV.DataSource = null;
        grdInvoiceDashV.DataBind();

        grdADPV.DataSource = null;
        grdADPV.DataBind();

        grdDRV.DataSource = null;
        grdDRV.DataBind();

        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageInvoice_MA_SMD(Zone_Id, Circle_Id, Division_Id, Scheme_Id, Display_Data, TAT_Range, District_Id, ULB_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdMAV.DataSource = ds.Tables[0];
            grdMAV.DataBind();
        }
        else
        {
            grdMAV.DataSource = null;
            grdMAV.DataBind();
        }
    }

    protected void lnkTotalInvoicePendingMA_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryMA.Rows.Count; i++)
        {
            grdSummeryMA.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_tbl_PackageMAInvoice("Pending", TAT_Range);
    }

    protected void lnkTotalInvoiceMarkedMA_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryMA.Rows.Count; i++)
        {
            grdSummeryMA.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_tbl_PackageMAInvoice("Marked", TAT_Range);
    }

    protected void btnInfoExpMA_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryMA.Rows.Count; i++)
        {
            grdSummeryMA.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_Invoice_MA_On_Expert(TAT_Range, 0);
    }
    private void get_Invoice_MA_On_Expert(int TAT_Range, int _mode)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Invoice_MA_On_Expert(Zone_Id, Circle_Id, Division_Id, Scheme_Id, TAT_Range, _mode, _mode, false, District_Id, ULB_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            if (_mode == -1)
            {
                sp_HeaderText.InnerHtml = "Expert Wise Approved Mob. Adv / Design Drawing Invoice Count";
                hf_ClickMode.Value = "Expert Wise Approved MA";
            }
            else if (_mode == -2)
            {
                sp_HeaderText.InnerHtml = "Expert Wise Deferred Mob. Adv / Design Drawing Invoice Count";
                hf_ClickMode.Value = "Expert Wise Deferred MA";
            }
            else if (_mode == -3)
            {
                sp_HeaderText.InnerHtml = "Expert Wise Pending Mob. Adv / Design Drawing Invoice Count";
                hf_ClickMode.Value = "Expert Wise Pending MA";
            }
            else
            {
                sp_HeaderText.InnerHtml = "Expert Wise Marked Mob. Adv / Design Drawing Invoice Count";
                hf_ClickMode.Value = "Expert Wise Marked MA";
            }
            hf_TAT_Range.Value = TAT_Range.ToString();
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }
    protected void lnkTotalInvoicePaymentMA_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryMA.Rows.Count; i++)
        {
            grdSummeryMA.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_tbl_PackageMAInvoice("Payment", TAT_Range);
    }

    protected void btnInfoExpAMA_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryMA.Rows.Count; i++)
        {
            grdSummeryMA.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_Invoice_MA_On_Expert(TAT_Range, -1);
    }

    protected void lnkTotalInvoiceRejectedMA_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryMA.Rows.Count; i++)
        {
            grdSummeryMA.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_tbl_PackageMAInvoice("Deferred", TAT_Range);
    }

    protected void btnInfoExpRMA_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryMA.Rows.Count; i++)
        {
            grdSummeryMA.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_Invoice_MA_On_Expert(TAT_Range, -2);
    }

    protected void lnkTotalInvoiceDR_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryDR.Rows.Count; i++)
        {
            grdSummeryDR.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_tbl_PackageDRInvoice("Total", TAT_Range);
    }
    private void get_tbl_PackageDRInvoice(string Display_Data, int TAT_Range)
    {
        grdInvoiceDashV.DataSource = null;
        grdInvoiceDashV.DataBind();

        grdADPV.DataSource = null;
        grdADPV.DataBind();

        grdMAV.DataSource = null;
        grdMAV.DataBind();

        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageInvoice_DR_SMD(Zone_Id, Circle_Id, Division_Id, Scheme_Id, Display_Data, TAT_Range, false, District_Id, ULB_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdDRV.DataSource = ds.Tables[0];
            grdDRV.DataBind();
        }
        else
        {
            grdDRV.DataSource = null;
            grdDRV.DataBind();
        }
    }
    protected void lnkTotalInvoicePendingDR_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryDR.Rows.Count; i++)
        {
            grdSummeryDR.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_tbl_PackageDRInvoice("Pending", TAT_Range);
    }

    protected void lnkTotalInvoiceMarkedDR_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryDR.Rows.Count; i++)
        {
            grdSummeryDR.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_tbl_PackageDRInvoice("Marked", TAT_Range);
    }

    protected void btnInfoExpDR_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryDR.Rows.Count; i++)
        {
            grdSummeryDR.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_Invoice_DR_On_Expert(TAT_Range, 0);
    }
    private void get_Invoice_DR_On_Expert(int TAT_Range, int _mode)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Invoice_DR_On_Expert(Zone_Id, Circle_Id, Division_Id, Scheme_Id, TAT_Range, _mode, _mode, false, District_Id, ULB_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            if (_mode == -1)
            {
                sp_HeaderText.InnerHtml = "Expert Wise Approved Deduction Release Count";
                hf_ClickMode.Value = "Expert Wise Approved DR";
            }
            else if (_mode == -2)
            {
                sp_HeaderText.InnerHtml = "Expert Wise Deferred Deduction Release Count";
                hf_ClickMode.Value = "Expert Wise Deferred DR";
            }
            else if (_mode == -3)
            {
                sp_HeaderText.InnerHtml = "Expert Wise Pending Deduction Release Count";
                hf_ClickMode.Value = "Expert Wise Pending DR";
            }
            else
            {
                sp_HeaderText.InnerHtml = "Expert Wise Marked Deduction Release Count";
                hf_ClickMode.Value = "Expert Wise Marked DR";
            }
            hf_TAT_Range.Value = TAT_Range.ToString();
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }
    protected void lnkTotalInvoicePendingF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageInvoice("Pending", 0);
    }
    protected void lnkTotalInvoicePaymentDR_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryDR.Rows.Count; i++)
        {
            grdSummeryDR.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_tbl_PackageDRInvoice("Payment", TAT_Range);
    }

    protected void btnInfoExpADR_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryDR.Rows.Count; i++)
        {
            grdSummeryDR.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_Invoice_DR_On_Expert(TAT_Range, -1);
    }

    protected void lnkTotalInvoiceRejectedDR_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryDR.Rows.Count; i++)
        {
            grdSummeryDR.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_tbl_PackageDRInvoice("Deferred", TAT_Range);
    }

    protected void btnInfoExpRDR_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryDR.Rows.Count; i++)
        {
            grdSummeryDR.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_Invoice_DR_On_Expert(TAT_Range, -2);
    }

    protected void lnkTotalInvoicePendingE_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummery.Rows.Count; i++)
        {
            grdSummery.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[10].Text.Trim());
        get_tbl_PackageInvoice("PendingE", TAT_Range);
    }

    protected void btnInfoExpP_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummery.Rows.Count; i++)
        {
            grdSummery.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[10].Text.Trim());
        get_Invoice_On_Expert(TAT_Range, -3);
    }

    protected void lnkTotalInvoicePendingEF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageInvoice("PendingE", 0);
    }

    protected void btnInfoExpPF_Click(object sender, ImageClickEventArgs e)
    {
        get_Invoice_On_Expert(0, -3);
    }

    protected void lnkTotalInvoicePaymentF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageInvoice("Payment", 0);
    }

    protected void btnInfoExpAF_Click(object sender, ImageClickEventArgs e)
    {
        get_Invoice_On_Expert(0, -1);
    }

    protected void lnkTotalInvoiceMarkedF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageInvoice("Marked", 0);
    }

    protected void btnInfoExpF_Click(object sender, ImageClickEventArgs e)
    {
        get_Invoice_On_Expert(0, 0);
    }

    protected void lnkTotalInvoicePendingADPE_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryADP.Rows.Count; i++)
        {
            grdSummeryADP.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_tbl_PackageADPInvoice("PendingE", TAT_Range);
    }

    protected void lnkTotalInvoicePendingADPF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageADPInvoice("Pending", 0);
    }

    protected void lnkTotalInvoiceMarkedADPF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageADPInvoice("Marked", 0);
    }

    protected void btnInfoExpADPF_Click(object sender, ImageClickEventArgs e)
    {
        get_Invoice_ADP_On_Expert(0, 0);
    }

    protected void lnkTotalInvoicePaymentADPF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageADPInvoice("Payment", 0);
    }

    protected void btnInfoExpAADPF_Click(object sender, ImageClickEventArgs e)
    {
        get_Invoice_ADP_On_Expert(0, -1);
    }

    protected void lnkTotalInvoiceRejectedADPF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageADPInvoice("Deferred", 0);
    }

    protected void btnInfoExpADPE_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryADP.Rows.Count; i++)
        {
            grdSummeryADP.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_Invoice_ADP_On_Expert(TAT_Range, -3);
    }

    protected void lnkTotalInvoicePendingADPEF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageADPInvoice("PendingE", 0);
    }

    protected void btnInfoExpADPEF_Click(object sender, ImageClickEventArgs e)
    {
        get_Invoice_ADP_On_Expert(0, -3);
    }

    protected void lnkTotalInvoicePendingMAE_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryMA.Rows.Count; i++)
        {
            grdSummeryMA.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_tbl_PackageMAInvoice("PendingE", TAT_Range);
    }

    protected void btnInfoExpMAE_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryMA.Rows.Count; i++)
        {
            grdSummeryMA.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_Invoice_MA_On_Expert(TAT_Range, -3);
    }

    protected void lnkTotalInvoicePendingMAEF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageMAInvoice("PendingE", 0);
    }

    protected void btnInfoExpMAEF_Click(object sender, ImageClickEventArgs e)
    {
        get_Invoice_MA_On_Expert(0, -3);
    }

    protected void lnkTotalInvoicePendingMAF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageMAInvoice("Pending", 0);
    }

    protected void lnkTotalInvoiceMarkedMAF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageMAInvoice("Marked", 0);
    }

    protected void btnInfoExpMAF_Click(object sender, ImageClickEventArgs e)
    {
        get_Invoice_MA_On_Expert(0, 0);
    }

    protected void lnkTotalInvoicePaymentMAF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageMAInvoice("Payment", 0);
    }

    protected void btnInfoExpAMAF_Click(object sender, ImageClickEventArgs e)
    {
        get_Invoice_MA_On_Expert(0, -1);
    }

    protected void lnkTotalInvoiceRejectedMAF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageMAInvoice("Deferred", 0);
    }

    protected void btnInfoExpRMAF_Click(object sender, ImageClickEventArgs e)
    {
        get_Invoice_MA_On_Expert(0, -2);
    }

    protected void lnkTotalInvoicePendingDRF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageDRInvoice("Pending", 0);
    }

    protected void lnkTotalInvoiceMarkedDRF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageDRInvoice("Marked", 0);
    }

    protected void btnInfoExpDRF_Click(object sender, ImageClickEventArgs e)
    {
        get_Invoice_DR_On_Expert(0, 0);
    }

    protected void lnkTotalInvoicePaymentDRF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageDRInvoice("Payment", 0);
    }

    protected void btnInfoExpADRF_Click(object sender, ImageClickEventArgs e)
    {
        get_Invoice_DR_On_Expert(0, -1);
    }

    protected void lnkTotalInvoicePendingDRE_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryDR.Rows.Count; i++)
        {
            grdSummeryDR.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_tbl_PackageDRInvoice("PendingE", TAT_Range);
    }

    protected void btnInfoExpDRE_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryDR.Rows.Count; i++)
        {
            grdSummeryDR.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_Invoice_DR_On_Expert(TAT_Range, -3);
    }

    protected void lnkTotalInvoicePendingDREF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageDRInvoice("PendingE", 0);
    }

    protected void btnInfoExpDREF_Click(object sender, ImageClickEventArgs e)
    {
        get_Invoice_DR_On_Expert(0, -3);
    }

    protected void lnkTotalInvoiceRejectedDRF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageDRInvoice("Deferred", 0);
    }

    protected void btnInfoExpRDRF_Click(object sender, ImageClickEventArgs e)
    {
        get_Invoice_DR_On_Expert(0, -2);
    }

    protected void btnOpenTimelineADP_Click(object sender, ImageClickEventArgs e)
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

    protected void btnOpenTimelineDR_Click(object sender, ImageClickEventArgs e)
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

    protected void btnOpenTimelineMA_Click(object sender, ImageClickEventArgs e)
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

    protected void lnkInfoExpADPR_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummeryADP.Rows.Count; i++)
        {
            grdSummeryADP.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        get_Invoice_ADP_On_Expert(TAT_Range, -2);
    }

    protected void lnkRejectionWiseADP_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        LinkButton lnkTotalInvoiceCount = sender as LinkButton;
        GridViewRow gr = lnkTotalInvoiceCount.Parent.Parent as GridViewRow;
        int TAT_Range = Convert.ToInt32(gr.Cells[8].Text.Trim());
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_Invoice_ADP_On_Expert_Rejected_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id, TAT_Range, "", "", false, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Invoice_ADP_On_Expert_Rejected_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id, TAT_Range, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = "Deferred Type Wise Other Dept Invoice Count";
            hf_ClickMode.Value = "Deferred Type Wise ADP";
            hf_TAT_Range.Value = TAT_Range.ToString();
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void lnkInfoExpRADPF_Click(object sender, EventArgs e)
    {
        get_Invoice_ADP_On_Expert(0, -2);
    }

    protected void lnkRejectionWiseADPF_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        LinkButton lnkTotalInvoiceCount = sender as LinkButton;
        GridViewRow gr = lnkTotalInvoiceCount.Parent.Parent as GridViewRow;
        int TAT_Range = 0;
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            ds = (new DataLayer()).get_Invoice_ADP_On_Expert_Rejected_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id, TAT_Range, "", "", false, District_Id, ULB_Id);
        }
        else
        {
            ds = (new DataLayer()).get_Invoice_ADP_On_Expert_Rejected_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id, TAT_Range, txtDateFrom.Text, txtDateTill.Text, false, District_Id, ULB_Id);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            sp_HeaderText.InnerHtml = "Deferred Type Wise Other Dept Invoice Count";
            hf_ClickMode.Value = "Deferred Type Wise ADP";
            hf_TAT_Range.Value = TAT_Range.ToString();
            grdExpertWiseDtls.DataSource = ds.Tables[0];
            grdExpertWiseDtls.DataBind();

            grdExpertWiseDtls.FooterRow.Cells[3].Text = ds.Tables[0].Compute("sum(Total_Invoices)", "").ToString();
            grdExpertWiseDtls.FooterRow.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Amount)", "").ToString(), "Int");
            mpViewSummeryExpert.Show();
        }
        else
        {
            grdExpertWiseDtls.DataSource = null;
            grdExpertWiseDtls.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void grdExpertWiseDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].Text = AllClasses.convert_To_Indian_No_Format(e.Row.Cells[4].Text.Trim().Replace("&nbsp;", ""), "Int");
        }
    }

    protected void btnRevert_Click(object sender, EventArgs e)
    {
        Response.Redirect("Revert_Invoice.aspx");
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

    protected void btnRevertADP_Click(object sender, EventArgs e)
    {
        Response.Redirect("Revert_Invoice_ADP.aspx");
    }

    protected void btnRevertMA_Click(object sender, EventArgs e)
    {

    }

    protected void btnRevertDR_Click(object sender, EventArgs e)
    {

    }

    protected void lnkTotalInvoiceDeEscelatedHO_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummery.Rows.Count; i++)
        {
            grdSummery.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[10].Text.Trim());
        get_tbl_PackageInvoice("DeEscelated_To_HO", TAT_Range);
    }

    protected void lnkTotalInvoiceDeEscelatedHOF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageInvoice("DeEscelated_To_HO", 0);
    }

    protected void lnkTotalDeEscelated_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        for (int i = 0; i < grdSummery.Rows.Count; i++)
        {
            grdSummery.Rows[i].BackColor = Color.Transparent;
        }
        gr.BackColor = Color.LightGreen;
        int TAT_Range = Convert.ToInt32(gr.Cells[10].Text.Trim());
        get_tbl_PackageInvoice("DeEscelated", TAT_Range);
    }
    protected void lnkStep_1_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }

        get_Step_Data(Zone_Id, 1);
    }

    private void get_Step_Data(int Zone_Id, int step_Count)
    {
        string Scheme_Id = "";
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);

        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_Step_Status(Scheme_Id, 0, Zone_Id, 0, 0, 0, step_Count);
        if (AllClasses.CheckDataSet(ds))
        {
            grdProjects.DataSource = ds.Tables[0];
            grdProjects.DataBind();
            mpMISStatus.Show();
        }
        else
        {
            grdProjects.DataSource = null;
            grdProjects.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void lnkStep_1_F_Click(object sender, EventArgs e)
    {
        get_Step_Data(0, 1);
    }

    protected void grdProjects_PreRender(object sender, EventArgs e)
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

    protected void lnkStep_2_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }

        get_Step_Data(Zone_Id, 2);
    }

    protected void lnkStep_2_F_Click(object sender, EventArgs e)
    {
        get_Step_Data(0, 2);
    }

    protected void lnkStep_3_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }

        get_Step_Data(Zone_Id, 3);
    }

    protected void lnkStep_3_F_Click(object sender, EventArgs e)
    {
        get_Step_Data(0, 3);
    }

    protected void lnkStep_4_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }

        get_Step_Data(Zone_Id, 4);
    }

    protected void lnkStep_4_F_Click(object sender, EventArgs e)
    {
        get_Step_Data(0, 4);
    }

    protected void lnkStep_5_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }

        get_Step_Data(Zone_Id, 5);
    }

    protected void lnkStep_5_F_Click(object sender, EventArgs e)
    {
        get_Step_Data(0, 5);
    }

    protected void lnkStep_6_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }

        get_Step_Data(Zone_Id, 6);
    }

    protected void lnkStep_6_F_Click(object sender, EventArgs e)
    {
        get_Step_Data(0, 6);
    }

    protected void lnkStep_7_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }

        get_Step_Data(Zone_Id, 7);
    }

    protected void lnkStep_7_F_Click(object sender, EventArgs e)
    {
        get_Step_Data(0, 7);
    }

    protected void lnkStep_0_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }

        get_Step_Data(Zone_Id, 0);
    }

    protected void lnkStep_0_F_Click(object sender, EventArgs e)
    {
        get_Step_Data(0, 0);
    }
    protected void btnMIS_Click(object sender, EventArgs e)
    {
        Response.Redirect("Report_PMIS_Dump.aspx");
    }
    protected void rbtChoose_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton rbtChoose = sender as RadioButton;
        for (int i = 0; i < grdDataUpdationStatusReport.Rows.Count; i++)
        {
            (grdDataUpdationStatusReport.Rows[i].FindControl("rbtChoose") as RadioButton).Checked = false;
        }
        rbtChoose.Checked = true;
    }
    protected void btnDownload_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = null;
        for (int i = 0; i < grdDataUpdationStatusReport.Rows.Count; i++)
        {
            RadioButton rbtChoose = grdDataUpdationStatusReport.Rows[i].FindControl("rbtChoose") as RadioButton;
            if (rbtChoose.Checked)
            {
                gr = grdDataUpdationStatusReport.Rows[i];
            }
        }

        int Zone_Id = 0;

        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }

        string Scheme_Id = "";
        string Scheme_Name = "";
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
                Scheme_Name += listItem.Text + "_";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        DateTime dt;
        DataSet ds = new DataSet();
        if (rbtSearchBy.SelectedValue == "1")
        {
            dt = DateTime.Now;
            ds = (new DataLayer()).get_tbl_ProjectWork_Data_Dump_MIS(Scheme_Id, 0, Zone_Id, 0, 0, 0, -1, false, "", "");
        }
        else
        {
            dt = DateTime.ParseExact(txtDateTill.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ds = (new DataLayer()).get_tbl_ProjectWork_Data_Dump_MIS(Scheme_Id, 0, Zone_Id, 0, 0, 0, -1, false, txtDateFrom.Text, txtDateTill.Text);
        }

        if (AllClasses.CheckDataSet(ds))
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ds.Tables[0].Rows[i]["Project Name"] = AllClasses.SanitizeText(ds.Tables[0].Rows[i]["Project Name"].ToString()).Trim();
                try
                {
                    ds.Tables[0].Rows[i]["Project Description"] = AllClasses.SanitizeText(ds.Tables[0].Rows[i]["Project Description"].ToString()).Trim();
                }
                catch
                { }
                try
                {
                    ds.Tables[0].Rows[i]["Issue"] = AllClasses.SanitizeText(ds.Tables[0].Rows[i]["Issue"].ToString()).Trim();
                }
                catch
                { }
            }
            string Name = "PMIS_" + Scheme_Name.Replace("State Sector", "State").Replace("DEPOSIT WORK", "DEPOSIT").Replace("1.0", "").Replace("2.0", "") + dt.Month.ToString().PadLeft(2, '0') + "_" + dt.Day.ToString().PadLeft(2, '0') + "_" + dt.Year.ToString() + "_" + DateTime.Now.Hour.ToString().PadLeft(2, '0') + "_" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
            if (Name.Length > 25)
            {
                Name = "PMIS_" + Scheme_Id.Replace(",", "_").Trim() + dt.Month.ToString().PadLeft(2, '0') + "_" + dt.Day.ToString().PadLeft(2, '0') + "_" + dt.Year.ToString() + "_" + DateTime.Now.Hour.ToString().PadLeft(2, '0') + "_" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(ds.Tables[0], Name);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + Name + ".xlsx");
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
    protected void lnkTotal_Project_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;

        int Zone_Id = 0;
        string Scheme_Id = "";
        int Circle_Id = 0;
        int Division_Id = 0;

        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        try
        {
            Zone_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Zone_Id = 0;
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork(Scheme_Id, 0, Zone_Id, Circle_Id, Division_Id, 0, "", 0, "", 0);
        if (AllClasses.CheckDataSet(ds))
        {
            GridproView.DataSource = ds.Tables[0];
            GridproView.DataBind();
            ProViewSummery.Show();
        }
        else
        {
            GridproView.DataSource = null;
            GridproView.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void lnkTotal_Project_F_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, "", 0, "", 0);
        if (AllClasses.CheckDataSet(ds))
        {
            GridproView.DataSource = ds.Tables[0];
            GridproView.DataBind();
            ProViewSummery.Show();
        }
        else
        {
            GridproView.DataSource = null;
            GridproView.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void grdProView_PreRender(object sender, EventArgs e)
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
    protected void btnInfoExpD_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void lnkTotalDeEscelatedF_Click(object sender, EventArgs e)
    {
        get_tbl_PackageInvoice("DeEscelated", 0);
    }

    protected void btnInfoExpDF_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void grdAnnextureDtls_PreRender(object sender, EventArgs e)
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

    protected void lnkProjectType_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ProjectType_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        try
        {
            ProjectType_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectType_Id = 0;
        }

        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        Response.Redirect("MasterProjectWorkUpload.aspx?ProjectType_Id=" + ProjectType_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Scheme_Id=" + Scheme_Id + "&Show=0");
    }

    protected void lnkReport_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ProjectType_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        try
        {
            ProjectType_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectType_Id = 0;
        }

        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        Response.Redirect("MasterProjectWorkUpload.aspx?ProjectType_Id=" + ProjectType_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Scheme_Id=" + Scheme_Id + "&Show=1");
    }

    protected void lnkAnnexture1_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ProjectType_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        try
        {
            ProjectType_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectType_Id = 0;
        }

        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        Response.Redirect("MasterProjectWorkUpload.aspx?ProjectType_Id=" + ProjectType_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Scheme_Id=" + Scheme_Id + "&Show=1");
    }

    protected void lnkAnnexture2_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ProjectType_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        try
        {
            ProjectType_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectType_Id = 0;
        }

        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        Response.Redirect("MasterProjectWorkUpload.aspx?ProjectType_Id=" + ProjectType_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Scheme_Id=" + Scheme_Id + "&Show=1");
    }

    protected void lnkAnnexture3_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ProjectType_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        try
        {
            ProjectType_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            ProjectType_Id = 0;
        }

        foreach (ListItem listItem in ddlScheme.Items)
        {
            if (listItem.Selected)
            {
                Scheme_Id += listItem.Value + ", ";
            }
        }
        Scheme_Id = Scheme_Id.Trim().Substring(0, Scheme_Id.Trim().Length - 1);
        try
        {
            District_Id = Convert.ToInt32(ddlDistrict.SelectedValue);
        }
        catch
        {
            District_Id = 0;
        }
        try
        {
            ULB_Id = Convert.ToInt32(ddlULB.SelectedValue);
        }
        catch
        {
            ULB_Id = 0;
        }
        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        Response.Redirect("MasterProjectWorkUpload.aspx?ProjectType_Id=" + ProjectType_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Scheme_Id=" + Scheme_Id + "&Show=1");
    }

    protected void rbtMappingWith_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtMappingWith.SelectedValue == "D")
        {
            divZone.Visible = true;
            divCircle.Visible = true;
            divDivision.Visible = true;

            divDistrict.Visible = false;
            divULB.Visible = false;
        }
        else
        {
            divZone.Visible = false;
            divCircle.Visible = false;
            divDivision.Visible = false;

            divDistrict.Visible = true;
            divULB.Visible = true;
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedValue == "0")
        {
            ddlULB.Items.Clear();
        }
        else
        {
            get_tbl_ULB(Convert.ToInt32(ddlDistrict.SelectedValue));
        }
    }

    private void get_tbl_ULB(int District_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ULB(District_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlULB, "ULB_Name", "ULB_Id");
        }
        else
        {
            ddlULB.Items.Clear();
        }
    }
    private void get_M_Jurisdiction()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_M_Jurisdiction(3, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDistrict, "Jurisdiction_Name_Eng_With_Parent", "M_Jurisdiction_Id");
        }
        else
        {
            ddlDistrict.Items.Clear();
        }
    }

    protected void grdInvoiceDashV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[12].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[13].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[14].Text = Session["Default_Division"].ToString();
        }
    }

    protected void grdADPV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[9].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[10].Text = Session["Default_Division"].ToString();
        }
    }

    protected void grdMAV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[8].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[9].Text = Session["Default_Division"].ToString();
        }
    }

    protected void grdDRV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[8].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[9].Text = Session["Default_Division"].ToString();
        }
    }

    protected void grdPkgView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[8].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[9].Text = Session["Default_Division"].ToString();
        }
    }

    protected void grdProjects_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[8].Text = Session["Default_Division"].ToString();
        }
    }

    protected void GridproView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[7].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[8].Text = Session["Default_Division"].ToString();
        }
    }
}