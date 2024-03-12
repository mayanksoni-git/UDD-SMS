using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DashboardReport : System.Web.UI.Page
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
            get_tbl_Designation(); 
            get_Branch_Office_Details();

            if (Session["UserType"].ToString() == "1")
            {
                rbtSearchBy.SelectedValue = "2";
            }
            else
            {
                rbtSearchBy.SelectedValue = "1";
            }
            if (Session["SearchStorage"] != null)
            {
                SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
                txtDateFrom.Text = obj_SearchStorage.FromDate;
                txtDateTill.Text = obj_SearchStorage.TillDate;
                ddlScheme.SelectedValue = obj_SearchStorage.Scheme_Id.ToString();
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

            rbtSearchBy_SelectedIndexChanged(rbtSearchBy, e);

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

    protected void get_tbl_ProjectWork_Assign_SNA_Limit()
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

        string Scheme_Id = "";

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
        divBalance1.Visible = true;
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_Assign_SNA_Limit(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", ULB_Id, "");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkProject.Text = ds.Tables[0].Rows.Count.ToString();
            lnkTotalAssigned.Text = ds.Tables[0].Compute("sum(SNAAccountLimit_AssignedLimit)", "").ToString();
            lnkUtilized.Text = ds.Tables[0].Compute("sum(SNAAccountLimitUsed_UsedLimit)", "").ToString();
            lnkAvailable.Text = ds.Tables[0].Compute("sum(SNAAccountAvailableLimit)", "").ToString();
        }
        else
        {
            lnkProject.Text = "0";
            lnkTotalAssigned.Text = "0";
            lnkUtilized.Text = "0";
            lnkAvailable.Text = "0";
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
    private void get_Branch_Office_Details()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Branch_Office_Details(0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlBranchOffice, "OfficeBranch_Name", "OfficeBranch_Id");
        }
        else
        {
            ddlBranchOffice.Items.Clear();
        }
    }
    private void get_tbl_Designation()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Designation();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDesignation, "Designation_DesignationName", "Designation_Id");
            if (ddlDesignation.Items.Count == 2)
            {
                ddlDesignation.SelectedIndex = 1;
            }
        }

        else
        {
            ddlDesignation.Items.Clear();
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

    private void get_tbl_PackageInvoiceADP()
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
                ds = (new DataLayer()).get_tbl_Package_ADP(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", 0, 0, true, "", "", Expert_Person_Id, 0, 0, false, isDefered, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_tbl_Package_ADP(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, "", "", Expert_Person_Id, 0, 0, false, isDefered, ULB_Id);
            }
        }
        else
        {
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_tbl_Package_ADP(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", 0, 0, true, txtDateFrom.Text, txtDateTill.Text, Expert_Person_Id, 0, 0, false, isDefered, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_tbl_Package_ADP(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, txtDateFrom.Text, txtDateTill.Text, Expert_Person_Id, 0, 0, false, isDefered, ULB_Id);
            }
        }

        if (AllClasses.CheckDataSet(ds))
        {
            btnMarkADP.Visible = false;
            string Designation_Id, Organization_Id;
            try
            {
                Organization_Id = ddlBranchOffice.SelectedValue;
            }
            catch
            {
                Organization_Id = "0";
            }
            try
            {
                Designation_Id = ddlDesignation.SelectedValue;
            }
            catch
            {
                Designation_Id = "0";
            }
            if (Organization_Id != "0")
            {
                ds = filter_Data(ds, "PackageADPApproval_Next_Organisation_Id", Organization_Id);
            }
            if (Designation_Id != "0")
            {
                ds = filter_Data(ds, "PackageADPApproval_Next_Designation_Id", Designation_Id);
            }
            grdADP.DataSource = ds.Tables[0];
            grdADP.DataBind();
            for (int i = 0; i < grdADP.Rows.Count; i++)
            {
                CheckBox chkMark = grdADP.Rows[i].FindControl("chkMark") as CheckBox;
                if (chkMark.Visible)
                {
                    btnMarkADP.Visible = true;
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

    private void get_tbl_PackageInvoiceMA()
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
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_tbl_Package_MobilizationAdvance(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", 0, 0, true, txtDateFrom.Text, txtDateTill.Text, Expert_Person_Id, 0, 0, false, isDefered, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_tbl_Package_MobilizationAdvance(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, txtDateFrom.Text, txtDateTill.Text, Expert_Person_Id, 0, 0, false, isDefered, ULB_Id);
            }
        }

        if (AllClasses.CheckDataSet(ds))
        {   
            btnMarkMA.Visible = false;
            string Designation_Id, Organization_Id;
            try
            {
                Organization_Id = ddlBranchOffice.SelectedValue;
            }
            catch
            {
                Organization_Id = "0";
            }
            try
            {
                Designation_Id = ddlDesignation.SelectedValue;
            }
            catch
            {
                Designation_Id = "0";
            }
            if (Organization_Id != "0")
            {
                ds = filter_Data(ds, "Package_MobilizationAdvanceApproval_Next_Organisation_Id", Organization_Id);
            }
            if (Designation_Id != "0")
            {
                ds = filter_Data(ds, "Package_MobilizationAdvanceApproval_Next_Designation_Id", Designation_Id);
            }
            grdMA.DataSource = ds.Tables[0];
            grdMA.DataBind();
            for (int i = 0; i < grdMA.Rows.Count; i++)
            {
                CheckBox chkMark = grdMA.Rows[i].FindControl("chkMark") as CheckBox;
                if (chkMark.Visible)
                {
                    btnMarkMA.Visible = true;
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

    private void get_tbl_Package_DeductionRelease()
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
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_tbl_Package_DeductionRelease(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", 0, 0, true, txtDateFrom.Text, txtDateTill.Text, Expert_Person_Id, 0, 0, false, isDefered, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_tbl_Package_DeductionRelease(0, Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, txtDateFrom.Text, txtDateTill.Text, Expert_Person_Id, 0, 0, false, isDefered, ULB_Id);
            }
        }

        if (AllClasses.CheckDataSet(ds))
        {
            btnMarkDR.Visible = false;
            string Designation_Id, Organization_Id;
            try
            {
                Organization_Id = ddlBranchOffice.SelectedValue;
            }
            catch
            {
                Organization_Id = "0";
            }
            try
            {
                Designation_Id = ddlDesignation.SelectedValue;
            }
            catch
            {
                Designation_Id = "0";
            }
            if (Organization_Id != "0")
            {
                ds = filter_Data(ds, "Package_DeductionReleaseApproval_Next_Organisation_Id", Organization_Id);
            }
            if (Designation_Id != "0")
            {
                ds = filter_Data(ds, "Package_DeductionReleaseApproval_Next_Designation_Id", Designation_Id);
            }
            grdDeductionRelease.DataSource = ds.Tables[0];
            grdDeductionRelease.DataBind();
            for (int i = 0; i < grdDeductionRelease.Rows.Count; i++)
            {
                CheckBox chkMark = grdDeductionRelease.Rows[i].FindControl("chkMark") as CheckBox;
                if (chkMark.Visible)
                {
                    btnMarkDR.Visible = true;
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
    private DataSet filter_Data(DataSet ds, string Filter_name, string Filter_Id)
    {
        int id = Convert.ToInt32(Filter_Id);
        DataSet dsF = new DataSet();
        DataView dv = new DataView(ds.Tables[0]);
        dv.RowFilter = Filter_name + "=" + Filter_Id;
        dsF.Tables.Add(dv.ToTable("FilteredTable"));
        return dsF;
    }
    private void get_tbl_PackageInvoice()
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
                ds = (new DataLayer()).get_tbl_PackageInvoice(0, Zone_Id, Circle_Id, Division_Id, Scheme_Id, 0, 0, false, "", "", Expert_Person_Id, 0, -1, false, -1, isDefered, "", District_Id, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_tbl_PackageInvoice(0, Zone_Id, Circle_Id, Division_Id, Scheme_Id, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, "", "", Expert_Person_Id, 0, -1, false, -1, isDefered, "", District_Id, ULB_Id);
            }
        }
        else
        {
            if (Session["UserType"].ToString() == "1")
            {
                ds = (new DataLayer()).get_tbl_PackageInvoice(0, Zone_Id, Circle_Id, Division_Id, Scheme_Id.ToString(), 0, 0, false, txtDateFrom.Text, txtDateTill.Text, Expert_Person_Id, 0, -1, false, -1, isDefered, "", District_Id, ULB_Id);
            }
            else
            {
                ds = (new DataLayer()).get_tbl_PackageInvoice(0, Zone_Id, Circle_Id, Division_Id, Scheme_Id.ToString(), Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), false, txtDateFrom.Text, txtDateTill.Text, Expert_Person_Id, 0, -1, false, -1, isDefered, "", District_Id, ULB_Id);
            }
        }
       
        if (AllClasses.CheckDataSet(ds))
        {
            btnMark.Visible = false;
            // filter by designation
            string Designation_Id, Organization_Id;
            try
            {
                Organization_Id = ddlBranchOffice.SelectedValue;
            }
            catch
            {
                Organization_Id = "0";
            }
            try
            {
                Designation_Id = ddlDesignation.SelectedValue;
            }
            catch
            {
                Designation_Id = "0";
            }
            if(Organization_Id != "0")
            {
                ds = filter_Data(ds, "PackageInvoiceApproval_Next_Organisation_Id", Organization_Id);
            }
            if(Designation_Id != "0")
            {
                ds = filter_Data(ds, "PackageInvoiceApproval_Next_Designation_Id", Designation_Id);
            }

            grdInvoice.DataSource = ds.Tables[0];
            grdInvoice.DataBind();

            for (int i = 0; i < grdInvoice.Rows.Count; i++)
            {
                CheckBox chkMark = grdInvoice.Rows[i].FindControl("chkMark") as CheckBox;
                if (chkMark.Visible)
                {
                    btnMark.Visible = true;
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
            ImageButton btnCover = e.Row.FindControl("btnCover") as ImageButton;
            ImageButton btnOpenInvoice = e.Row.FindControl("btnOpenInvoice") as ImageButton;
            CheckBox chkMark = e.Row.FindControl("chkMark") as CheckBox;
            int PackageInvoiceCover_Id = 0;
            int PackageInvoice_IsPrinted = 0;

            try
            {
                PackageInvoice_IsPrinted = Convert.ToInt32(e.Row.Cells[30].Text.Trim().Replace("&nbsp;", ""));
            }
            catch
            {
                PackageInvoice_IsPrinted = 0;
            }

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

            if (Session["PersonJuridiction_DesignationId"].ToString() == "4" || Session["PersonJuridiction_DesignationId"].ToString() == "9" || Session["PersonJuridiction_DesignationId"].ToString() == "1056")
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

            if (PackageInvoice_IsPrinted > 0)
            {
                e.Row.Cells[7].BackColor = Color.LightGreen;
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
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Next_Organisation_Id = 2;
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Next_Designation_Id = 9;
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Step_Count = 4;
                    obj_tbl_PackageInvoiceApproval.PackageInvoiceApproval_Additional_Status_Id = 0;
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
            if (new DataLayer().Update_tbl_PackageInvoice_Rejected(obj_tbl_PackageInvoiceApproval_Li))
            {
                MessageBox.Show("Invoice Forword to EXECUTIVE ENGINEER Successfully");
                load_dashboard();
                return;
            }
            else
            {
                MessageBox.Show("Unable To Forword Invoice");
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
                    obj_tbl_PackageADPApproval.PackageADPApproval_Comments = "";
                    obj_tbl_PackageADPApproval.PackageADPApproval_Status_Id = 4;
                    obj_tbl_PackageADPApproval.PackageADPApproval_Next_Organisation_Id = 1;
                    obj_tbl_PackageADPApproval.PackageADPApproval_Next_Designation_Id = 9;
                    obj_tbl_PackageADPApproval.PackageADPApproval_Step_Count = 7;
                    obj_tbl_PackageADPApproval.PackageADPApproval_Additional_Status_Id = 0;
                    
                    obj_tbl_PackageADPApproval.PackageADPApproval_Package_Id = Convert.ToInt32(grdADP.Rows[i].Cells[0].Text.Trim());
                    obj_tbl_PackageADPApproval.PackageADPApproval_Package_ADP_Id = PackageADP_Id;
                    obj_tbl_PackageADPApproval.PackageADPApproval_Status = 1;
                    obj_tbl_PackageADPApproval.Loop = Convert.ToInt32(grdADP.Rows[i].Cells[4].Text.Trim());
                    obj_tbl_PackageADPApproval.Scheme_Id = Convert.ToInt32(grdADP.Rows[i].Cells[2].Text.Trim());
                    obj_tbl_PackageADPApproval_Li.Add(obj_tbl_PackageADPApproval);
                }
            }
        }
        if (obj_tbl_PackageADPApproval_Li.Count == 0 || obj_tbl_PackageADPApproval_Li == null)
        {
            MessageBox.Show("Please Select Atleast One Other Departmental To Mark");
            return;
        }
        else
        {
            if (new DataLayer().Update_tbl_PackageADP_Rejected(obj_tbl_PackageADPApproval_Li))
            {
                MessageBox.Show("Other Departmental Marked Successfully");
                load_dashboard();
                return;
            }
            else
            {
                MessageBox.Show("Unable To Mark Other Departmental");
                return;
            }            
        }
    }

    protected void grdEMB_PreRender(object sender, EventArgs e)
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

    protected void btnOpenEMB_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        string PackageEMB_Master_Id = gr.Cells[0].Text.Trim();
        string PackageEMB_Master_Type = gr.Cells[4].Text.Trim();
        string Package_Id = gr.Cells[1].Text.Trim();
        string Scheme_Id = gr.Cells[5].Text.Trim();
        if (PackageEMB_Master_Type == "N")
        {
            Response.Redirect("MasterEMBApprove_New.aspx?Package_Id=" + Package_Id + "&EMB_Master_Id=" + PackageEMB_Master_Id + "&Scheme_Id=" + Scheme_Id);
        }
        else
        {
            Response.Redirect("MasterEMBApprove.aspx?Package_Id=" + Package_Id + "&EMB_Master_Id=" + PackageEMB_Master_Id + "&Scheme_Id=" + Scheme_Id);
        }
    }

    protected void lnkTotal_Click(object sender, EventArgs e)
    {

    }

    protected void lnkPending_Click(object sender, EventArgs e)
    {

    }

    protected void lnkApproved_Click(object sender, EventArgs e)
    {

    }

    protected void lnkDe_Escelated_Click(object sender, EventArgs e)
    {

    }

    //protected void btnCombineEMB_Click(object sender, EventArgs e)
    //{
    //    List<int> obj_PackageEMB_Master_Id_Li = new List<int>();
    //    string RA_Bill_NO = "";
    //    for (int i = 0; i < grdEMB.Rows.Count; i++)
    //    {
    //        CheckBox chkMarkMB = grdEMB.Rows[i].FindControl("chkMarkMB") as CheckBox;
    //        int PackageEMB_Master_Id = 0;
    //        try
    //        {
    //            PackageEMB_Master_Id = Convert.ToInt32(grdEMB.Rows[i].Cells[0].Text.Trim());
    //        }
    //        catch
    //        {
    //            PackageEMB_Master_Id = 0;
    //        }

    //        if (chkMarkMB.Visible && chkMarkMB.Checked)
    //        {
    //            if (RA_Bill_NO == "")
    //            {
    //                RA_Bill_NO = grdEMB.Rows[0].Cells[9].Text.Trim();
    //            }
    //            else
    //            {
    //                if (RA_Bill_NO != grdEMB.Rows[0].Cells[9].Text.Trim())
    //                {
    //                    MessageBox.Show("RA Bill No Always Same.");
    //                    return;
    //                }
    //            }
    //            if (PackageEMB_Master_Id > 0)
    //                obj_PackageEMB_Master_Id_Li.Add(PackageEMB_Master_Id);
    //        }
    //    }

    //    if (obj_PackageEMB_Master_Id_Li.Count == 0)
    //    {
    //        MessageBox.Show("Please Select Atleast One EMB To Mark");
    //        return;
    //    }
    //    else
    //    {
    //        List<tbl_PackageEMBApproval> obj_tbl_PackageEMBApproval_Li = new List<tbl_PackageEMBApproval>();
    //        for (int i = 0; i < obj_PackageEMB_Master_Id_Li.Count; i++)
    //        {
    //            tbl_PackageEMBApproval obj_tbl_PackageEMBApproval = new tbl_PackageEMBApproval();
    //            obj_tbl_PackageEMBApproval.PackageEMBApproval_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
    //            obj_tbl_PackageEMBApproval.PackageEMBApproval_Comments = "";
    //            obj_tbl_PackageEMBApproval.PackageEMBApproval_Date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
    //            obj_tbl_PackageEMBApproval.PackageEMBApproval_Status_Id = 4;
    //            obj_tbl_PackageEMBApproval.PackageEMBApproval_PackageEMBMaster_Id = obj_PackageEMB_Master_Id_Li[i];
    //            obj_tbl_PackageEMBApproval.PackageEMBApproval_Status = 1;
    //            obj_tbl_PackageEMBApproval_Li.Add(obj_tbl_PackageEMBApproval);
    //        }
    //        if (new DataLayer().Update_CombinedInvoiceGenerate(obj_tbl_PackageEMBApproval_Li, Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString())))
    //        {
    //            MessageBox.Show("EMB Marked Successfully");
    //            load_dashboard();
    //            return;
    //        }
    //        else
    //        {
    //            MessageBox.Show("Unable To Mark EMB");
    //            return;
    //        }
    //    }        
    //}

    protected void btnCover_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        string Invoice_Id = gr.Cells[0].Text.Trim();
        string Package_Id = gr.Cells[1].Text.Trim();
        string ProcessedBy = gr.Cells[2].Text.Trim().Replace("&nbsp;", "");
        Response.Redirect("MasterGenerateCoverLetter?Invoice_Id=" + Invoice_Id);
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
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Comments = "";
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Status_Id = 4;
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Next_Organisation_Id = 1;
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Next_Designation_Id = 9;
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Step_Count = 7;
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Additional_Status_Id = 0;
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Package_Id = Convert.ToInt32(grdMA.Rows[i].Cells[0].Text.Trim());
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Package_MobilizationAdvance_Id = MobilizationAdvance_Id;
                    obj_tbl_Package_MobilizationAdvanceApproval.Package_MobilizationAdvanceApproval_Status = 1;
                    obj_tbl_Package_MobilizationAdvanceApproval.Scheme_Id = Convert.ToInt32(grdMA.Rows[i].Cells[2].Text.Trim());
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
            if (new DataLayer().Update_tbl_PackageMobilizationAdvance_Rejected(obj_tbl_Package_MobilizationAdvanceApproval_Li))
            {
                MessageBox.Show("Mobilization Advance Marked Successfully");
                load_dashboard();
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
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Comments = "";
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Status_Id = 4;
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Next_Organisation_Id = 1;
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Next_Designation_Id = 9;
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Step_Count = 7;
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Additional_Status_Id = 0;
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Package_Id = Convert.ToInt32(grdDeductionRelease.Rows[i].Cells[0].Text.Trim());
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Package_DeductionRelease_Id = Package_DeductionRelease_Id;
                    obj_tbl_Package_DeductionReleaseApproval.Package_DeductionReleaseApproval_Status = 1;
                    obj_tbl_Package_DeductionReleaseApproval.Scheme_Id = Convert.ToInt32(grdDeductionRelease.Rows[i].Cells[2].Text.Trim());
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
            if (new DataLayer().Update_tbl_PackageDeductionRelease_Rejected(obj_tbl_Package_DeductionReleaseApproval_Li))
            {
                MessageBox.Show("Deduction Release Marked Successfully");
                load_dashboard();
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

    protected void lnkOtherDept_Click(object sender, EventArgs e)
    {

    }

    protected void lnkMA_Click(object sender, EventArgs e)
    {

    }

    protected void lnkDeduction_Click(object sender, EventArgs e)
    {

    }

    protected void btnMIS_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterProjectWorkMIS.aspx");
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

    protected void lnkProject_Click(object sender, EventArgs e)
    {
        get_tbl_ProjectWork_Assign_SNA_Balance();
    }

    protected void get_tbl_ProjectWork_Assign_SNA_Balance()
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        string Scheme_Id = "";
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
        if (Scheme_Id == "")
        {
            MessageBox.Show("Please Select A Scheme");
            ddlScheme.Focus();
            return;
        }
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
        divBalance1.Visible = true;
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_Assign_SNA_Limit(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, 0, "", ULB_Id, "");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            mp1.Show();
            grdSNAAccountDetails.DataSource = ds.Tables[0];
            grdSNAAccountDetails.DataBind();
        }
        else
        {
            MessageBox.Show("Details Not Found");
            grdSNAAccountDetails.DataSource = null;
            grdSNAAccountDetails.DataBind();
        }
        if (Scheme_Id.Contains("1013") || Scheme_Id.Contains("1016"))
        {
            divBalance1.Visible = true;
        }
        else
        {
            divBalance1.Visible = false;
        }
    }

    protected void grdSNAAccountDetails_PreRender(object sender, EventArgs e)
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
    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlBranchOffice_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}