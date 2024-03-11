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

public partial class Dashboard_Sarovar : System.Web.UI.Page
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
            lnkSanctioned.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Sanction)", "").ToString(), "Decimal");
            lnkTotalRelease.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Release)", "").ToString(), "Decimal");
            lnkTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Expenditure)", "").ToString(), "Decimal");
            lnkRemaining.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Compute("sum(Total_Remaining_Amount)", "").ToString(), "Decimal");
        }
        else
        {
            lnkSanctioned.Text = "0.00";
            lnkTotalExpenditure.Text = "0.00";
            lnkRemaining.Text = "0.00";
            lnkTotalRelease.Text = "0.00";
        }

        ds = (new DataLayer()).get_PMIS_Dashboard_View(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkTarget_C.Text = ds.Tables[0].Rows[0]["Projects_Completing"].ToString();
            lnkTarget_N.Text = ds.Tables[0].Rows[0]["Projects_Completing_Next"].ToString();
            lnkCompleted.Text = ds.Tables[0].Rows[0]["Projects_Completed"].ToString();
            lnkOnGoing.Text = ds.Tables[0].Rows[0]["Projects_onGoing"].ToString();
            lnkLD.Text = ds.Tables[0].Rows[0]["LD_Likly"].ToString();
            lnkBondDateDelay.Text = ds.Tables[0].Rows[0]["Delay_Likly1"].ToString();
            lnkBondDateDelayNotExtended.Text = ds.Tables[0].Rows[0]["Delay_Likly2"].ToString();
        }
        else
        {
            lnkCompleted.Text = "0";
            lnkOnGoing.Text = "0";
            lnkTarget_C.Text = "0";
            lnkTarget_N.Text = "0";
            lnkLD.Text = "0";
            lnkBondDateDelay.Text = "0";
            lnkBondDateDelayNotExtended.Text = "0";
        }

        ds = (new DataLayer()).get_PMIS_Dashboard_Physical_Financial_Slab(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, -1, "", "", "", "");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            Project_Physical_Financial_Status obj_Project_Physical_Status = new Project_Physical_Financial_Status();
            Project_Physical_Financial_Status obj_Project_Financial_Status = new Project_Physical_Financial_Status();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["Data_Type"].ToString() == "Physical Progress")
                {
                    obj_Project_Physical_Status.Data_Type = "Physical Progress";
                    try
                    {
                        obj_Project_Physical_Status.Total = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Projects"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.Total = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.Zero = Convert.ToInt32(ds.Tables[0].Rows[i]["Zero"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.Zero = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.Less_10 = Convert.ToInt32(ds.Tables[0].Rows[i]["Less_10"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.Less_10 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_10_20 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_10_20"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_10_20 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_20_30 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_20_30"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_20_30 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_30_40 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_30_40"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_30_40 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_40_50 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_40_50"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_40_50 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_50_60 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_50_60"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_50_60 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_60_70 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_60_70"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_60_70 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_70_80 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_70_80"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_70_80 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_80_90 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_80_90"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_80_90 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.More_90 = Convert.ToInt32(ds.Tables[0].Rows[i]["More_90"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.More_90 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.More_100 = Convert.ToInt32(ds.Tables[0].Rows[i]["More_100"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.More_100 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_0_25 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_0_25"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_0_25 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_26_50 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_26_50"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_26_50 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_51_75 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_51_75"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_51_75 = 0;
                    }
                    try
                    {
                        obj_Project_Physical_Status.BW_76_99 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_76_99"].ToString());
                    }
                    catch
                    {
                        obj_Project_Physical_Status.BW_76_99 = 0;
                    }
                    if (obj_Project_Physical_Status.Total > 0)
                    {
                        obj_Project_Physical_Status.Percentage_Zero = (obj_Project_Physical_Status.Zero * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_Less_10 = (obj_Project_Physical_Status.Less_10 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_10_20 = (obj_Project_Physical_Status.BW_10_20 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_20_30 = (obj_Project_Physical_Status.BW_20_30 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_30_40 = (obj_Project_Physical_Status.BW_30_40 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_40_50 = (obj_Project_Physical_Status.BW_40_50 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_50_60 = (obj_Project_Physical_Status.BW_50_60 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_60_70 = (obj_Project_Physical_Status.BW_60_70 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_70_80 = (obj_Project_Physical_Status.BW_70_80 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_80_90 = (obj_Project_Physical_Status.BW_80_90 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_More_90 = (obj_Project_Physical_Status.More_90 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_More_100 = (obj_Project_Physical_Status.More_100 * 100) / obj_Project_Physical_Status.Total;

                        obj_Project_Physical_Status.Percentage_BW_0_25 = (obj_Project_Physical_Status.BW_0_25 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_26_50 = (obj_Project_Physical_Status.BW_26_50 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_51_75 = (obj_Project_Physical_Status.BW_51_75 * 100) / obj_Project_Physical_Status.Total;
                        obj_Project_Physical_Status.Percentage_BW_76_99 = (obj_Project_Physical_Status.BW_76_99 * 100) / obj_Project_Physical_Status.Total;
                    }
                }
                else if (ds.Tables[0].Rows[i]["Data_Type"].ToString() == "Financial Progress")
                {
                    obj_Project_Financial_Status.Data_Type = "Physical Progress";
                    try
                    {
                        obj_Project_Financial_Status.Total = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Projects"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.Total = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.Zero = Convert.ToInt32(ds.Tables[0].Rows[i]["Zero"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.Zero = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.Less_10 = Convert.ToInt32(ds.Tables[0].Rows[i]["Less_10"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.Less_10 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_10_20 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_10_20"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_10_20 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_20_30 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_20_30"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_20_30 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_30_40 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_30_40"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_30_40 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_40_50 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_40_50"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_40_50 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_50_60 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_50_60"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_50_60 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_60_70 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_60_70"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_60_70 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_70_80 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_70_80"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_70_80 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_80_90 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_80_90"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_80_90 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.More_90 = Convert.ToInt32(ds.Tables[0].Rows[i]["More_90"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.More_90 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.More_100 = Convert.ToInt32(ds.Tables[0].Rows[i]["More_100"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.More_100 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_0_25 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_0_25"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_0_25 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_26_50 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_26_50"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_26_50 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_51_75 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_51_75"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_51_75 = 0;
                    }
                    try
                    {
                        obj_Project_Financial_Status.BW_76_99 = Convert.ToInt32(ds.Tables[0].Rows[i]["BW_76_99"].ToString());
                    }
                    catch
                    {
                        obj_Project_Financial_Status.BW_76_99 = 0;
                    }
                    if (obj_Project_Financial_Status.Total > 0)
                    {
                        obj_Project_Financial_Status.Percentage_Zero = (obj_Project_Financial_Status.Zero * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_Less_10 = (obj_Project_Financial_Status.Less_10 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_10_20 = (obj_Project_Financial_Status.BW_10_20 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_20_30 = (obj_Project_Financial_Status.BW_20_30 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_30_40 = (obj_Project_Financial_Status.BW_30_40 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_40_50 = (obj_Project_Financial_Status.BW_40_50 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_50_60 = (obj_Project_Financial_Status.BW_50_60 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_60_70 = (obj_Project_Financial_Status.BW_60_70 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_70_80 = (obj_Project_Financial_Status.BW_70_80 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_80_90 = (obj_Project_Financial_Status.BW_80_90 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_More_90 = (obj_Project_Financial_Status.More_90 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_More_100 = (obj_Project_Financial_Status.More_100 * 100) / obj_Project_Financial_Status.Total;

                        obj_Project_Financial_Status.Percentage_BW_0_25 = (obj_Project_Financial_Status.BW_0_25 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_26_50 = (obj_Project_Financial_Status.BW_26_50 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_51_75 = (obj_Project_Financial_Status.BW_51_75 * 100) / obj_Project_Financial_Status.Total;
                        obj_Project_Financial_Status.Percentage_BW_76_99 = (obj_Project_Financial_Status.BW_76_99 * 100) / obj_Project_Financial_Status.Total;
                    }
                }
                else
                {

                }
            }
            hf_Physical_Progress_Filter.Value = Newtonsoft.Json.JsonConvert.SerializeObject(obj_Project_Physical_Status);
            hf_Financal_Progress_Filter.Value = Newtonsoft.Json.JsonConvert.SerializeObject(obj_Project_Financial_Status);
            grdPMISUpdation.DataSource = ds.Tables[0];
            grdPMISUpdation.DataBind();
        }
        else
        {
            grdPMISUpdation.DataSource = null;
            grdPMISUpdation.DataBind();
        }

        int ProjectType_Id = 0;
        ds = (new DataLayer()).get_PMIS_Dashboard_LD(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkLDImposed.Text = ds.Tables[0].Rows[0]["LD_Imposed"].ToString() + " / " + ds.Tables[0].Rows[0]["LD_Amount"].ToString();
            lnkLDWithdrawan.Text = ds.Tables[0].Rows[0]["LD_Withdrawan"].ToString() + " / " + ds.Tables[0].Rows[0]["LD_Withdrawan_Amount"].ToString(); ;
        }
        else
        {
            lnkLDImposed.Text = "0 / 0";
            lnkLDWithdrawan.Text = "0 / 0";
        }

        ds = (new DataLayer()).get_Financial_Closure_Pending(Scheme_Id, Zone_Id, Circle_Id, Division_Id, District_Id, ULB_Id, 180);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lnkFinancialClosure.Text = ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lnkFinancialClosure.Text = "0";
        }
        
        ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_Issue_Popup(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", "", -1);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            List<Project_Issue_Analysis> obj_Project_Issue_Analysis_Li = new List<Project_Issue_Analysis>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Project_Issue_Analysis obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = ds.Tables[0].Rows[i]["ProjectIssue_Name"].ToString();
                try
                {
                    obj_Project_Issue_Analysis.Total_Projects = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Projects_With_Issue"].ToString());
                }
                catch
                {
                    obj_Project_Issue_Analysis.Total_Projects = 0;
                }
                try
                {
                    obj_Project_Issue_Analysis.Total_Issues = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Isues"].ToString());
                }
                catch
                {
                    obj_Project_Issue_Analysis.Total_Issues = 0;
                }
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);
            }

            if (obj_Project_Issue_Analysis_Li.Count == 4)
            {
                Project_Issue_Analysis obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);
            }
            else if (obj_Project_Issue_Analysis_Li.Count == 3)
            {
                Project_Issue_Analysis obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

                obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);
            }
            else if (obj_Project_Issue_Analysis_Li.Count == 2)
            {
                Project_Issue_Analysis obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

                obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

                obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);
            }
            else if (obj_Project_Issue_Analysis_Li.Count == 1)
            {
                Project_Issue_Analysis obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

                obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

                obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);

                obj_Project_Issue_Analysis = new Project_Issue_Analysis();
                obj_Project_Issue_Analysis.Issue_Name = "";
                obj_Project_Issue_Analysis.Total_Issues = 0;
                obj_Project_Issue_Analysis.Total_Projects = 0;
                obj_Project_Issue_Analysis_Li.Add(obj_Project_Issue_Analysis);
            }
            hf_Issue_Analysis.Value = Newtonsoft.Json.JsonConvert.SerializeObject(obj_Project_Issue_Analysis_Li);
            grdIssueReportedGlobal.DataSource = ds.Tables[0];
            grdIssueReportedGlobal.DataBind();
        }
        else
        {
            hf_Issue_Analysis.Value = "[]";
            grdIssueReportedGlobal.DataSource = null;
            grdIssueReportedGlobal.DataBind();
        }
        ds = new DataSet();
        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", "", -1, 0, "", "", "", "", 0);
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

    protected void grdPMISUpdation_PreRender(object sender, EventArgs e)
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

    protected void lnkTotalSLTC_Click(object sender, EventArgs e)
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Month=0&Year=0&NodalDept_Id=0&Issue_Id=0&Dep_Id=0");
    }

    protected void lnkTotalGODone_Click(object sender, EventArgs e)
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Month=0&Year=0&NodalDept_Id=0&Issue_Id=0&Dep_Id=0");
    }

    protected void lnkSLTC_Click(object sender, EventArgs e)
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Month=0&Year=0&NodalDept_Id=0&Issue_Id=0&Dep_Id=0");
    }

    protected void lnkSHPSC_Click(object sender, EventArgs e)
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Month=0&Year=0&NodalDept_Id=0&Issue_Id=0&Dep_Id=0");
    }

    protected void lnkUnderTendering_Click(object sender, EventArgs e)
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Month=0&Year=0&NodalDept_Id=0&Issue_Id=0&Dep_Id=0");
    }

    protected void lnkLOAIssued_Click(object sender, EventArgs e)
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Month=0&Year=0&NodalDept_Id=0&Issue_Id=0&Dep_Id=0");
    }

    protected void lnkWorkStarted_Click(object sender, EventArgs e)
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Month=0&Year=0&NodalDept_Id=0&Issue_Id=0&Dep_Id=0");
    }

    protected void lnkWorkCompleted_Click(object sender, EventArgs e)
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Month=0&Year=0&NodalDept_Id=0&Issue_Id=0&Dep_Id=0");
    }

    protected void lnkTotalIssues_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "";

        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        try
        {
            Issue_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Issue_Id = 0;
        }
        try
        {
            Dependency_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        }
        catch
        {
            Dependency_Id = 0;
        }

        Scheme_Id = ddlScheme.SelectedValue;
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
        //string[] ToolTip_Data = lnkProjectStatusPopup.ToolTip.Trim().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        string NodalDept_Id = "0";
        Type_Id = "0";
        NodalDept_Id = "0";
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDept_Id.ToString() + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id);
    }

    protected void lblUpdation100_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=11&ProgressType=" + ProgressType);
    }

    protected void lblUpdation0_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=0&ProgressType=" + ProgressType);
    }

    protected void btnInfo10_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo10 = sender as ImageButton;
        GridViewRow gr = btnInfo10.Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.Cells[0].Text.Trim() == "Physical Progress")
        {
            ProgressType = "Physical";
        }
        else if (gr.Cells[0].Text.Trim() == "Financial Progress")
        {
            ProgressType = "Financial";
        }
        else
        {
            ProgressType = "";
        }
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 1, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfo20_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo20 = sender as ImageButton;
        GridViewRow gr = btnInfo20.Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.Cells[0].Text.Trim() == "Physical Progress")
        {
            ProgressType = "Physical";
        }
        else if (gr.Cells[0].Text.Trim() == "Financial Progress")
        {
            ProgressType = "Financial";
        }
        else
        {
            ProgressType = "";
        }
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 2, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfo30_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo30 = sender as ImageButton;
        GridViewRow gr = btnInfo30.Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.Cells[0].Text.Trim() == "Physical Progress")
        {
            ProgressType = "Physical";
        }
        else if (gr.Cells[0].Text.Trim() == "Financial Progress")
        {
            ProgressType = "Financial";
        }
        else
        {
            ProgressType = "";
        }
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 3, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfo40_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo40 = sender as ImageButton;
        GridViewRow gr = btnInfo40.Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.Cells[0].Text.Trim() == "Physical Progress")
        {
            ProgressType = "Physical";
        }
        else if (gr.Cells[0].Text.Trim() == "Financial Progress")
        {
            ProgressType = "Financial";
        }
        else
        {
            ProgressType = "";
        }
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 4, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfo50_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo50 = sender as ImageButton;
        GridViewRow gr = btnInfo50.Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.Cells[0].Text.Trim() == "Physical Progress")
        {
            ProgressType = "Physical";
        }
        else if (gr.Cells[0].Text.Trim() == "Financial Progress")
        {
            ProgressType = "Financial";
        }
        else
        {
            ProgressType = "";
        }
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 5, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfo60_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo60 = sender as ImageButton;
        GridViewRow gr = btnInfo60.Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.Cells[0].Text.Trim() == "Physical Progress")
        {
            ProgressType = "Physical";
        }
        else if (gr.Cells[0].Text.Trim() == "Financial Progress")
        {
            ProgressType = "Financial";
        }
        else
        {
            ProgressType = "";
        }
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 6, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfo70_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo70 = sender as ImageButton;
        GridViewRow gr = btnInfo70.Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.Cells[0].Text.Trim() == "Physical Progress")
        {
            ProgressType = "Physical";
        }
        else if (gr.Cells[0].Text.Trim() == "Financial Progress")
        {
            ProgressType = "Financial";
        }
        else
        {
            ProgressType = "";
        }
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 7, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfo80_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo80 = sender as ImageButton;
        GridViewRow gr = btnInfo80.Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.Cells[0].Text.Trim() == "Physical Progress")
        {
            ProgressType = "Physical";
        }
        else if (gr.Cells[0].Text.Trim() == "Financial Progress")
        {
            ProgressType = "Financial";
        }
        else
        {
            ProgressType = "";
        }
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 8, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfo90_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo90 = sender as ImageButton;
        GridViewRow gr = btnInfo90.Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.Cells[0].Text.Trim() == "Physical Progress")
        {
            ProgressType = "Physical";
        }
        else if (gr.Cells[0].Text.Trim() == "Financial Progress")
        {
            ProgressType = "Financial";
        }
        else
        {
            ProgressType = "";
        }
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 9, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfoMore90_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfoMore90 = sender as ImageButton;
        GridViewRow gr = btnInfoMore90.Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.Cells[0].Text.Trim() == "Physical Progress")
        {
            ProgressType = "Physical";
        }
        else if (gr.Cells[0].Text.Trim() == "Financial Progress")
        {
            ProgressType = "Financial";
        }
        else
        {
            ProgressType = "";
        }
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 10, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfo100_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo100 = sender as ImageButton;
        GridViewRow gr = btnInfo100.Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.Cells[0].Text.Trim() == "Physical Progress")
        {
            ProgressType = "Physical";
        }
        else if (gr.Cells[0].Text.Trim() == "Financial Progress")
        {
            ProgressType = "Financial";
        }
        else
        {
            ProgressType = "";
        }
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 11, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void btnInfo0_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnInfo0 = sender as ImageButton;
        GridViewRow gr = btnInfo0.Parent.Parent as GridViewRow;
        string ProgressType = "";
        if (gr.Cells[0].Text.Trim() == "Physical Progress")
        {
            ProgressType = "Physical";
        }
        else if (gr.Cells[0].Text.Trim() == "Financial Progress")
        {
            ProgressType = "Financial";
        }
        else
        {
            ProgressType = "";
        }
        string Scheme_Id = ddlScheme.SelectedValue;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        string NodalDepartment_Id = "";
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

        int Jurisdiction_In = -1;

        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", NodalDepartment_Id, Jurisdiction_In, 0, ProgressType, "", "", "", 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblSanctionedCost.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Sanction"].ToString(), "Decimal");
            lblTotalReleased.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Release"].ToString(), "Decimal");
            lblTotalExpenditure.Text = AllClasses.convert_To_Indian_No_Format(ds.Tables[0].Rows[0]["Total_Expenditure"].ToString(), "Decimal");
            mp2.Show();
        }
        else
        {
            MessageBox.Show("No Details Found");
            lblSanctionedCost.Text = "0";
            lblTotalReleased.Text = "0";
            lblTotalExpenditure.Text = "0";
        }
    }

    protected void lblUpdation10_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=1&ProgressType=" + ProgressType);
    }

    protected void lblUpdation20_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=2&ProgressType=" + ProgressType);
    }

    protected void lblUpdation30_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=3&ProgressType=" + ProgressType);
    }

    protected void lblUpdation40_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=4&ProgressType=" + ProgressType);
    }

    protected void lblUpdation50_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=5&ProgressType=" + ProgressType);
    }

    protected void lblUpdation60_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=6&ProgressType=" + ProgressType);
    }

    protected void lblUpdation70_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=7&ProgressType=" + ProgressType);
    }

    protected void lblUpdation80_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=8&ProgressType=" + ProgressType);
    }

    protected void lblUpdation90_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=9&ProgressType=" + ProgressType);
    }

    protected void lblUpdationMore90_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        string ProgressType = "";
        if (gr.RowIndex == 0)
        {
            ProgressType = "Physical";
        }
        else
        {
            ProgressType = "Financial";
        }
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Updation=10&ProgressType=" + ProgressType);
    }

    protected void lnkCompleted_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type=Completed");
    }

    protected void lnkOnGoing_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type=OnGoing");
    }

    protected void lnkTarget_C_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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

        DateTime dt = DateTime.Now;
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Month=" + dt.Month.ToString() + "&Year=" + dt.Year.ToString());
    }

    protected void lnkTarget_N_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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

        DateTime dt = DateTime.Now.AddMonths(1);
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Month=" + dt.Month.ToString() + "&Year=" + dt.Year.ToString());
    }

    protected void lnkLD_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&LD=0");
    }

    protected void lnkBondDateDelay_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&LD=1");
    }

    protected void lnkBondDateDelayNotExtended_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&LD=2");
    }

    protected void lnkTE_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&DocType=19");
    }

    protected void lnkLDImposed_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        Response.Redirect("Report_ProjectWorkPkg_LD_Imposed.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&LDWithdraw=0");
    }

    protected void lnkLDWithdrawan_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        Response.Redirect("Report_ProjectWorkPkg_LD_Imposed.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&LDWithdraw=1");
    }
    protected void rbtGraphFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        ds = (new DataLayer()).get_PMIS_Dashboard_CNDS_Filter(Zone_Id, Circle_Id, Division_Id, Scheme_Id, District_Id, ULB_Id, "", "", "", -1, 0, "", "", "", "", 0);
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
    protected void lnkFinancialClosure_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        Scheme_Id = ddlScheme.SelectedValue;
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
        Response.Redirect("Report_ProjectWork_Financial_Closure.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id);
    }
    protected void grdIssueReportedGlobal_PreRender(object sender, EventArgs e)
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
    protected void lnkTotalIssuesGlobal_Click(object sender, EventArgs e)
    {
        string Scheme_Id = "";
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int District_Id = 0;
        int ULB_Id = 0;
        int Issue_Id = 0;
        int Dependency_Id = 0;
        string Type_Id = "0";

        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        try
        {
            Issue_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Issue_Id = 0;
        }
        Scheme_Id = ddlScheme.SelectedValue;
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
        string NodalDept_Id = "0";

        Response.Redirect("MasterProjectWorkMISView.aspx?Scheme_Id=" + Scheme_Id + "&District_Id=" + District_Id + "&ULB_Id=" + ULB_Id + "&Zone_Id=" + Zone_Id + "&Circle_Id=" + Circle_Id + "&Division_Id=" + Division_Id + "&Type_Id=" + Type_Id + "&Month=0&Year=0&NodalDept_Id=" + NodalDept_Id.ToString() + "&Issue_Id=" + Issue_Id.ToString() + "&Dep_Id=" + Dependency_Id);
    }

    protected void lnkSanctioned_Click(object sender, EventArgs e)
    {

    }

    protected void lnkTotalRelease_Click(object sender, EventArgs e)
    {

    }

    protected void lnkTotalExpenditure_Click(object sender, EventArgs e)
    {

    }

    protected void lnkRemaining_Click(object sender, EventArgs e)
    {

    }

    protected void lnkTenderPublihed_Click(object sender, EventArgs e)
    {

    }
}