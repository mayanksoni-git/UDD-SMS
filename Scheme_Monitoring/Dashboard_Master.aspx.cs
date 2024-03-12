using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

public partial class Dashboard_Master : System.Web.UI.Page
{
    public tbl_ePaymentModules obj_tbl_ePaymentModules = new tbl_ePaymentModules();
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
        obj_tbl_ePaymentModules = (tbl_ePaymentModules)Session["tbl_ePaymentModules"];
        if (!IsPostBack)
        {
            string Client = ConfigurationManager.AppSettings.Get("Client");
            
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();
            get_Year_Data();
            get_tbl_Project();
            get_tbl_Zone();

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
            if (Session["SearchStorage"] != null)
            {
                SearchStorage obj_SearchStorage = (SearchStorage)Session["SearchStorage"];
                string[] List_Scheme = obj_SearchStorage.Scheme_Id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (List_Scheme.Length > 0)
                {
                    foreach (ListItem listItem in ddlScheme.Items)
                    {
                        listItem.Selected = false;
                    }
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
            Load_Dashboard();
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

            if (Request.QueryString.Count > 0)
            {
                int Scheme_Id = 0;
                try
                {
                    Scheme_Id = Convert.ToInt32(Request.QueryString["Scheme_Id"].ToString());
                }
                catch
                {
                    Scheme_Id = 0;
                }
                if (Scheme_Id > 0)
                {
                    foreach (ListItem listItem in ddlScheme.Items)
                    {
                        if (Scheme_Id == Convert.ToInt32(listItem.Value))
                        {
                            listItem.Selected = true;
                        }
                    }
                }
            }
            else
            {
                try
                {
                    foreach (ListItem listItem in ddlScheme.Items)
                    {
                        listItem.Selected = true;
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
        mp1.Show();
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
        mp1.Show();
    }
    private void Load_Dashboard()
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

        string[] sanction_Year = ddlSanctionYear.SelectedValue.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year != null && sanction_Year.Length == 2)
        {
            FromDate = sanction_Year[0].Replace("-", "");
            TillDate = sanction_Year[1].Replace("-", "");
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
        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id;
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        obj_SearchStorage.FromDate = FromDate;
        obj_SearchStorage.TillDate = TillDate;
        Session["SearchStorage"] = obj_SearchStorage;
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_PMS_Scheme_Wise(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", "", -1, 0, "", FromDate, TillDate);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkTotalScheme.Text = ds.Tables[0].Rows.Count.ToString();
            lnkTotalProjects.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Count)", "").ToString(), "Int");
            lnkSanctioned.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Sanction)", "").ToString(), "Decimal");
            lnkTotalRelease.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Release)", "").ToString(), "Decimal");
            lnkTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Expenditure)", "").ToString(), "Decimal");
            lnkRemaining.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Remaining_Amount)", "").ToString(), "Decimal");
        }
        else
        {
            lnkTotalScheme.Text = "0";
            lnkTotalProjects.Text = "0.00";
            lnkSanctioned.Text = "0.00";
            lnkTotalExpenditure.Text = "0.00";
            lnkRemaining.Text = "0.00";
            lnkTotalRelease.Text = "0.00";
        }

        ds = (new DataLayer()).get_PMIS_Dashboard_Detailed(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", -1, FromDate, TillDate, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdScheme_Wise_Details.DataSource = ds.Tables[0];
            grdScheme_Wise_Details.DataBind();
        }
        else
        {
            grdScheme_Wise_Details.DataSource = null;
            grdScheme_Wise_Details.DataBind();
        }

        ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", "", -1, 0, "", FromDate, TillDate, "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            Project_Filter obj_Project_Filter = new Project_Filter();
            if (rbtGraphFor.SelectedValue == "A")
            {
                obj_Project_Filter.Data_Type = "Data For Total Projects";
                try
                {
                    obj_Project_Filter.Total_Projects = Convert.ToInt32(ds.Tables[0].Rows[0]["Total_Count"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Projects = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Sanction_Cost = Convert.ToDecimal(ds.Tables[0].Rows[0]["Total_Sanction"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Sanction_Cost = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Released_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["Total_Release"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Released_Amount = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Expenditure = Convert.ToDecimal(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Expenditure = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Remaining_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["Total_Remaining_Amount"].ToString());
                    if (obj_Project_Filter.Total_Remaining_Amount < 0)
                    {
                        obj_Project_Filter.Total_Remaining_Amount = 0;
                    }
                }
                catch
                {
                    obj_Project_Filter.Total_Remaining_Amount = 0;
                }
            }
            else if (rbtGraphFor.SelectedValue == "O")
            {
                obj_Project_Filter.Data_Type = "Data For OnGoing Projects";
                try
                {
                    obj_Project_Filter.Total_Projects = Convert.ToInt32(ds.Tables[0].Rows[0]["OnGoing_Count"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Projects = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Sanction_Cost = Convert.ToDecimal(ds.Tables[0].Rows[0]["OnGoing_Sanction"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Sanction_Cost = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Released_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["OnGoing_Release"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Released_Amount = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Expenditure = Convert.ToDecimal(ds.Tables[0].Rows[0]["OnGoing_Expenditure"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Expenditure = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Remaining_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["OnGoing_Remaining_Amount"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Remaining_Amount = 0;
                }
            }
            else if (rbtGraphFor.SelectedValue == "C")
            {
                obj_Project_Filter.Data_Type = "Data For Completed Projects";
                try
                {
                    obj_Project_Filter.Total_Projects = Convert.ToInt32(ds.Tables[0].Rows[0]["Completed_Count"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Projects = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Sanction_Cost = Convert.ToDecimal(ds.Tables[0].Rows[0]["Completed_Sanction"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Sanction_Cost = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Released_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["Completed_Release"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Released_Amount = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Expenditure = Convert.ToDecimal(ds.Tables[0].Rows[0]["Completed_Expenditure"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Expenditure = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Remaining_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["Completed_Remaining_Amount"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Remaining_Amount = 0;
                }
            }
            hf_Data_Filter.Value = Newtonsoft.Json.JsonConvert.SerializeObject(obj_Project_Filter);
        }
        else
        {
            hf_Data_Filter.Value = "[]";
        }
    }
    protected void rbtGraphFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string[] sanction_Year = ddlSanctionYear.SelectedValue.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

        string FromDate = "";
        string TillDate = "";

        if (sanction_Year != null && sanction_Year.Length == 2)
        {
            FromDate = sanction_Year[0].Replace("-", "");
            TillDate = sanction_Year[1].Replace("-", "");
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
        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", "", -1, 0, "", FromDate, TillDate, "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            Project_Filter obj_Project_Filter = new Project_Filter();
            if (rbtGraphFor.SelectedValue == "A")
            {
                obj_Project_Filter.Data_Type = "Data For Total Projects";
                try
                {
                    obj_Project_Filter.Total_Projects = Convert.ToInt32(ds.Tables[0].Rows[0]["Total_Count"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Projects = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Sanction_Cost = Convert.ToDecimal(ds.Tables[0].Rows[0]["Total_Sanction"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Sanction_Cost = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Released_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["Total_Release"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Released_Amount = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Expenditure = Convert.ToDecimal(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Expenditure = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Remaining_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["Total_Remaining_Amount"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Remaining_Amount = 0;
                }
            }
            else if (rbtGraphFor.SelectedValue == "O")
            {
                obj_Project_Filter.Data_Type = "Data For OnGoing Projects";
                try
                {
                    obj_Project_Filter.Total_Projects = Convert.ToInt32(ds.Tables[0].Rows[0]["OnGoing_Count"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Projects = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Sanction_Cost = Convert.ToDecimal(ds.Tables[0].Rows[0]["OnGoing_Sanction"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Sanction_Cost = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Released_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["OnGoing_Release"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Released_Amount = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Expenditure = Convert.ToDecimal(ds.Tables[0].Rows[0]["OnGoing_Expenditure"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Expenditure = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Remaining_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["OnGoing_Remaining_Amount"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Remaining_Amount = 0;
                }
            }
            else if (rbtGraphFor.SelectedValue == "C")
            {
                obj_Project_Filter.Data_Type = "Data For Completed Projects";
                try
                {
                    obj_Project_Filter.Total_Projects = Convert.ToInt32(ds.Tables[0].Rows[0]["Completed_Count"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Projects = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Sanction_Cost = Convert.ToDecimal(ds.Tables[0].Rows[0]["Completed_Sanction"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Sanction_Cost = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Released_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["Completed_Release"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Released_Amount = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Expenditure = Convert.ToDecimal(ds.Tables[0].Rows[0]["Completed_Expenditure"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Expenditure = 0;
                }
                try
                {
                    obj_Project_Filter.Total_Remaining_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["Completed_Remaining_Amount"].ToString());
                }
                catch
                {
                    obj_Project_Filter.Total_Remaining_Amount = 0;
                }
            }
            hf_Data_Filter.Value = Newtonsoft.Json.JsonConvert.SerializeObject(obj_Project_Filter);
        }
        else
        {
            hf_Data_Filter.Value = "[]";
        }
    }
    protected void lnkTotalScheme_Click(object sender, EventArgs e)
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
        
        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id;
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id);
    }

    protected void lnkTotalProjects_Click(object sender, EventArgs e)
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

        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id;
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id);
    }

    protected void lnkSanctioned_Click(object sender, EventArgs e)
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

        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id;
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id);
    }

    protected void lnkTotalRelease_Click(object sender, EventArgs e)
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

        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id;
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id);
    }

    protected void lnkTotalExpenditure_Click(object sender, EventArgs e)
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

        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id;
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id);
    }

    protected void lnkRemaining_Click(object sender, EventArgs e)
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

        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id;
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id);
    }

    protected void grdScheme_Wise_Details_PreRender(object sender, EventArgs e)
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

    protected void grdScheme_Wise_Details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Chart Chart1 = e.Row.FindControl("Chart1") as Chart;

            Button btnEMBDashbaord = e.Row.FindControl("btnEMBDashbaord") as Button;

            string Client = ConfigurationManager.AppSettings.Get("Client");
            if (Client == "JNUP" || Client == "SAR")
            {
                btnEMBDashbaord.Visible = true;
            }
            else if (Client == "ULB")
            {
                btnEMBDashbaord.Visible = false;
            }
            else
            {
                btnEMBDashbaord.Visible = false;
            }

            Button btnDistrictWiseDetails = e.Row.FindControl("btnDistrictWiseDetails") as Button;
            btnDistrictWiseDetails.Text = "Go To " + Session["Default_Circle"].ToString() + " Wise Details";

            LinkButton lnkTotal_Projects = e.Row.FindControl("lnkTotal_Projects") as LinkButton;
            LinkButton lnk_Total_Sanction_Cost = e.Row.FindControl("lnk_Total_Sanction_Cost") as LinkButton;
            LinkButton lnk_Total_Release = e.Row.FindControl("lnk_Total_Release") as LinkButton;
            LinkButton lnk_Total_Expenditure = e.Row.FindControl("lnk_Total_Expenditure") as LinkButton;
            LinkButton lnk_Total_Remaining = e.Row.FindControl("lnk_Total_Remaining") as LinkButton;

            List<Project_Filter_Exclude_Project_Count> obj_Project_Filter_Exclude_Project_Count_Li = new List<Project_Filter_Exclude_Project_Count>();
            Project_Filter_Exclude_Project_Count obj_Project_Filter_Exclude_Project_Count = new Project_Filter_Exclude_Project_Count();
            try
            {
                obj_Project_Filter_Exclude_Project_Count.Total_Sanction_Cost = Convert.ToDecimal(lnk_Total_Sanction_Cost.Text);
            }
            catch
            {
                obj_Project_Filter_Exclude_Project_Count.Total_Sanction_Cost = 0;
            }
            try
            {
                obj_Project_Filter_Exclude_Project_Count.Total_Released_Amount = Convert.ToDecimal(lnk_Total_Release.Text);
            }
            catch
            {
                obj_Project_Filter_Exclude_Project_Count.Total_Released_Amount = 0;
            }
            try
            {
                obj_Project_Filter_Exclude_Project_Count.Total_Expenditure = Convert.ToDecimal(lnk_Total_Expenditure.Text);
            }
            catch
            {
                obj_Project_Filter_Exclude_Project_Count.Total_Expenditure = 0;
            }
            try
            {
                obj_Project_Filter_Exclude_Project_Count.Total_Remaining_Amount = Convert.ToDecimal(lnk_Total_Remaining.Text);
            }
            catch
            {
                obj_Project_Filter_Exclude_Project_Count.Total_Remaining_Amount = 0;
            }
            obj_Project_Filter_Exclude_Project_Count_Li.Add(obj_Project_Filter_Exclude_Project_Count);
            //Chart1.Series[0].ChartType = SeriesChartType.Bar;
            Chart1.DataBindTable(obj_Project_Filter_Exclude_Project_Count_Li);
            Chart1.DataSource = obj_Project_Filter_Exclude_Project_Count_Li;
            Chart1.DataBind();
        }
    }

    protected void lnkTotal_Projects_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id.ToString();
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void lnkTotal_Projects_Ongoing_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id.ToString();
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void lnkTotal_Projects_Completed_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id.ToString();
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void lnk_Total_Sanction_Cost_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id.ToString();
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void lnk_Ongoing_Sanction_Cost_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id.ToString();
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void lnk_Completed_Sanction_Cost_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id.ToString();
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void lnk_Total_Release_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id.ToString();
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void lnk_Ongoing_Release_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id.ToString();
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void lnk_Completed_Release_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id.ToString();
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void lnk_Total_Expenditure_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id.ToString();
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void lnk_Ongoing_Expenditure_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id.ToString();
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void lnk_Completed_Expenditure_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id.ToString();
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void lnk_Total_Remaining_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id.ToString();
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void lnk_Ongoing_Remaining_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id.ToString();
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void lnk_Completed_Remaining_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id.ToString();
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void btnDetailedDash_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        GridViewRow gr = (sender as Button).Parent.Parent as GridViewRow;
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id.ToString();
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("Dashboard_PMIS.aspx?Scheme_Id=" + Scheme_Id.ToString());
    }

    protected void btnDistrictWiseDetails_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        GridViewRow gr = (sender as Button).Parent.Parent as GridViewRow;
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        if (Zone_Id == 0)
        {
            Response.Redirect("Report_Collection.aspx?Scheme_Id=" + Scheme_Id.ToString());
        }
        else if (Zone_Id > 0 && Circle_Id == 0)
        {
            Response.Redirect("Report_Collection_District.aspx?Scheme_Id=" + Scheme_Id.ToString() + "&Zone_Id=" + Zone_Id.ToString() + "&Zone_Name=");
        }
        else if (Zone_Id > 0 && Circle_Id > 0 && Division_Id == 0)
        {
            Response.Redirect("Report_Collection_Circle.aspx?Scheme_Id=" + Scheme_Id.ToString() + "&Zone_Id=" + Zone_Id.ToString() + "&Circle_Id=" + Circle_Id.ToString());
        }
        else
        {
            Response.Redirect("Report_Collection_Division.aspx?Scheme_Id=" + Scheme_Id.ToString() + "&Zone_Id=" + Zone_Id.ToString() + "&Circle_Id=" + Circle_Id.ToString() + "&Division_Id=" + Division_Id.ToString());
        }
    }

    protected void btnEMBDashbaord_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;

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
        GridViewRow gr = (sender as Button).Parent.Parent as GridViewRow;
        int Scheme_Id = 0;
        try
        {
            Scheme_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Scheme_Id = 0;
        }
        SearchStorage obj_SearchStorage = new SearchStorage();
        obj_SearchStorage.District_Id = District_Id;
        obj_SearchStorage.Circle_Id = Circle_Id;
        obj_SearchStorage.Division_Id = Division_Id;
        obj_SearchStorage.Scheme_Id = Scheme_Id.ToString();
        obj_SearchStorage.ULB_Id = ULB_Id;
        obj_SearchStorage.Zone_Id = Zone_Id;
        Session["SearchStorage"] = obj_SearchStorage;
        Response.Redirect("DashboardDept.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Load_Dashboard();
    }

    protected void chkSelectAllScheme_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem listItem in ddlScheme.Items)
        {
            listItem.Selected = chkSelectAllScheme.Checked;
        }
        mp1.Show();
    }

    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        mp1.Show();
    }

    protected void chkFY_Wise_CheckedChanged(object sender, EventArgs e)
    {
        get_Year_Data();
        mp1.Show();
    }

    private void get_Year_Data()
    {
        DataTable dt = new DataTable();
        DataColumn dc1 = new DataColumn("Year_Text", typeof(string));
        DataColumn dc2 = new DataColumn("Year_Value", typeof(string));
        dt.Columns.AddRange(new DataColumn[] { dc1, dc2 });
        DataRow dr = dt.NewRow();
        dr["Year_Text"] = "All Data";
        dr["Year_Value"] = "-|-";
        dt.Rows.Add(dr);
        if (chkFY_Wise.Checked)
        {
            dr = dt.NewRow();
            dr["Year_Text"] = "Before FY 2019-2020";
            dr["Year_Value"] = "-|31/03/2019";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Year_Text"] = "2019-2020";
            dr["Year_Value"] = "31/03/2019|31/03/2020";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Year_Text"] = "2020-2021";
            dr["Year_Value"] = "31/03/2020|31/03/2021";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Year_Text"] = "2021-2022";
            dr["Year_Value"] = "31/03/2021|31/03/2022";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Year_Text"] = "2022-2023";
            dr["Year_Value"] = "31/03/2022|31/03/2023";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Year_Text"] = "2023-2024";
            dr["Year_Value"] = "31/03/2023|31/03/2024";
            dt.Rows.Add(dr);
        }
        else
        {
            dr = dt.NewRow();
            dr["Year_Text"] = "Before 2020";
            dr["Year_Value"] = "-|31/12/2019";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Year_Text"] = "2020";
            dr["Year_Value"] = "01/01/2020|31/12/2020";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Year_Text"] = "2021";
            dr["Year_Value"] = "01/01/2021|31/12/2021";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Year_Text"] = "2022";
            dr["Year_Value"] = "01/01/2022|31/12/2022";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Year_Text"] = "2023";
            dr["Year_Value"] = "01/01/2023|31/12/2023";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Year_Text"] = "2024";
            dr["Year_Value"] = "01/01/2024|31/12/2024";
            dt.Rows.Add(dr);
        }

        ddlSanctionYear.DataTextField = "Year_Text";
        ddlSanctionYear.DataValueField = "Year_Value";
        ddlSanctionYear.DataSource = dt;
        ddlSanctionYear.DataBind();
    }
}